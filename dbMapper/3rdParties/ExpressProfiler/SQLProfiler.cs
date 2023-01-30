//sample application for demonstrating Sql Server Profiling
//writen by Locky, 2009.

using DBMapper;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace ExpressProfiler
{
    public partial class SQLProfiler : UserControl
    {
        internal const string ApplicationName = "Express Profiler";
        internal const string versionString = ApplicationName + " v2.0";

        private class PerfInfo
        {
            internal int m_count;
            internal readonly DateTime m_date = DateTime.Now;
        }

        public class PerfColumn
        {
            public string  Caption;
            public int Column;
            public int Width;
            public string Format;
            public HorizontalAlignment Alignment = HorizontalAlignment.Left;
        }

        public enum ProfilingStateEnum { psStopped, psProfiling, psPaused }
        private RawTraceReader m_Rdr;

        private SqlConnection m_Conn;
        private readonly SqlCommand m_Cmd = new SqlCommand();
        private Thread m_Thr;
        private bool m_NeedStop = true;
        private ProfilingStateEnum m_ProfilingState ;
        private int m_EventCount;
        private readonly ProfilerEvent m_EventStarted = new ProfilerEvent();
        private readonly ProfilerEvent m_EventStopped = new ProfilerEvent();
        private readonly ProfilerEvent m_EventPaused = new ProfilerEvent();
        internal readonly List<ListViewItem> m_Cached = new List<ListViewItem>(1024);
        private readonly Dictionary<string,ListViewItem> m_itembysql = new Dictionary<string, ListViewItem>();
        //private string m_servername = "";
        //private string m_username = "";
        //private string m_userpassword = "";
        internal int lastpos = -1;
        internal string lastpattern = "";
        private ListViewNF lvEvents;
        Queue<ProfilerEvent> m_events = new Queue<ProfilerEvent>(10);
        private bool dontUpdateSource;
        private Exception m_profilerexception;
        private readonly Queue<PerfInfo> m_perf = new Queue<PerfInfo>();
        private PerfInfo m_first, m_prev;
        internal TraceProperties.TraceSettings m_currentsettings;
        private readonly List<PerfColumn> m_columns = new List<PerfColumn>();
        internal bool matchCase = false;
        internal bool wholeWord = false;

        public SQLProfiler()
        {
            InitializeComponent();
            tbStart.DefaultItem = tbRun;
            Text = versionString;
            //m_servername = Properties.Settings.Default.ServerName;
            //m_username = Properties.Settings.Default.UserName;
            m_currentsettings = GetDefaultSettings();
            InitLV();
            UpdateButtons();
            mnuQuickFilterHostCurrent.Text = Environment.MachineName;
            QuerySeparator = "";
            cbQuerySeparator.SelectedIndex = 3;
            gridData.AutoGenerateColumns = true;
        }

        private string connectionString;
        public string QuerySeparator { get; set; }
        public void InitConnection(string connString, List<string> mappedDbs)
        {
            StopProfiling();
            m_currentsettings = GetDefaultSettings();
            connectionString = connString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("select name from sys.databases order by name", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        for (var i = mnuQuickFilterDb.DropDownItems.Count - 1; i > 0; i--)
                        {
                            mnuQuickFilterDb.DropDownItems.RemoveAt(i);
                        }
                        while (reader.Read())
                        {
                            var dbname = reader[0].ToString();
                            if (mappedDbs != null && !mappedDbs.Contains(dbname)) continue;
                            var dbitem = mnuQuickFilterDb.DropDownItems.Add(dbname);
                            dbitem.Click += (s, e) => SetQuickDbFilter(s);
                        }
                    }
                }
            }
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(DataObjectView.GetConnectionString(connString, "master"));
            mnuQuickFilterLoginSystem.Text = "%" + Environment.UserName + "%";
            mnuQuickFilterLoginCurrent.Text = builder.UserID;
            mnuQuickFilterLoginCurrent.Visible = !String.IsNullOrEmpty(builder.UserID);
        }

        private TraceProperties.TraceSettings GetDefaultSettings()
        {
            TraceProperties.TraceSettings settings = null;
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(TraceProperties.TraceSettings));
                using (StringReader sr = new StringReader(global::DBMapper.Properties.Settings.Default.TraceSettings))
                {
                    settings = (TraceProperties.TraceSettings)x.Deserialize(sr);                    
                }
            }
            catch (Exception)
            {
                settings = null;
            }
            if (settings == null) settings = TraceProperties.TraceSettings.GetDefaultSettings();
            toggleSpResetConnection(settings);
            return settings;
        }


        private void tbStart_Click(object sender, EventArgs e)
        {

            if (!TraceProperties.AtLeastOneEventSelected(m_currentsettings))
            {
                MessageBox.Show("You should select at least 1 event", "Starting trace", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RunProfiling(true);
            }
            {
                RunProfiling(false);
            }
        }

        private void UpdateButtons()
        {
            btnQuickFilter.Enabled = m_ProfilingState == ProfilingStateEnum.psStopped;
            tbStart.Enabled = m_ProfilingState == ProfilingStateEnum.psStopped || m_ProfilingState == ProfilingStateEnum.psPaused;
            tbRun.Enabled = tbStart.Enabled;
            tbRunWithFilters.Enabled = ProfilingStateEnum.psStopped==m_ProfilingState;
            tbStop.Enabled = m_ProfilingState==ProfilingStateEnum.psPaused||m_ProfilingState==ProfilingStateEnum.psProfiling;
            tbPause.Enabled = m_ProfilingState == ProfilingStateEnum.psProfiling;
            timer1.Enabled = m_ProfilingState == ProfilingStateEnum.psProfiling;
        }


        private void InitLV()
        {
            lvEvents = new ListViewNF
                           {
                               Dock = DockStyle.Fill,
                               Location = new System.Drawing.Point(0, 0),
                               Name = "lvEvents",
                               Size = new System.Drawing.Size(979, 297),
                               TabIndex = 0,
                               VirtualMode = true,
                               UseCompatibleStateImageBehavior = false,
                               BorderStyle = BorderStyle.None,
                               FullRowSelect = true,
                               View = View.Details,
                               GridLines = true,
                               HideSelection = false,
                               MultiSelect = true,
                               AllowColumnReorder = false
                           };
            lvEvents.RetrieveVirtualItem += lvEvents_RetrieveVirtualItem;
            lvEvents.KeyDown += lvEvents_KeyDown;
            lvEvents.ItemSelectionChanged += listView1_ItemSelectionChanged_1;
            lvEvents.ColumnClick += lvEvents_ColumnClick;
            lvEvents.SelectedIndexChanged += lvEvents_SelectedIndexChanged;
            lvEvents.VirtualItemsSelectionRangeChanged += LvEventsOnVirtualItemsSelectionRangeChanged;
            lvEvents.ContextMenuStrip = contextMenuStrip1;
            splitContainer1.Panel1.Controls.Add(lvEvents);
            InitColumns();
            InitGridColumns();
        }

        private void InitColumns()
        {
            m_columns.Clear();
            m_columns.Add(new PerfColumn{ Caption = "Event Class", Column = ProfilerEventColumns.EventClass,Width = 122});
            m_columns.Add(new PerfColumn { Caption = "Text Data", Column = ProfilerEventColumns.TextData, Width = 255});
            m_columns.Add(new PerfColumn { Caption = "Login Name", Column = ProfilerEventColumns.LoginName, Width = 79 });
            m_columns.Add(new PerfColumn { Caption = "CPU", Column = ProfilerEventColumns.CPU, Width = 82, Alignment = HorizontalAlignment.Right, Format = "#,0" });
            m_columns.Add(new PerfColumn { Caption = "Reads", Column = ProfilerEventColumns.Reads, Width = 78, Alignment = HorizontalAlignment.Right, Format = "#,0" });
            m_columns.Add(new PerfColumn { Caption = "Writes", Column = ProfilerEventColumns.Writes, Width = 78, Alignment = HorizontalAlignment.Right, Format = "#,0" });
            m_columns.Add(new PerfColumn { Caption = "Duration, ms", Column = ProfilerEventColumns.Duration, Width = 82, Alignment = HorizontalAlignment.Right, Format = "#,0" });
            m_columns.Add(new PerfColumn { Caption = "SPID", Column = ProfilerEventColumns.SPID, Width = 50, Alignment = HorizontalAlignment.Right });

            if (m_currentsettings.EventsColumns.StartTime) m_columns.Add(new PerfColumn { Caption = "Start time", Column = ProfilerEventColumns.StartTime, Width = 140, Format = "yyyy-MM-dd hh:mm:ss.ffff" });
            if (m_currentsettings.EventsColumns.EndTime) m_columns.Add(new PerfColumn { Caption = "End time", Column = ProfilerEventColumns.EndTime, Width = 140, Format = "yyyy-MM-dd hh:mm:ss.ffff" });
            if (m_currentsettings.EventsColumns.DatabaseName) m_columns.Add(new PerfColumn { Caption = "DatabaseName", Column = ProfilerEventColumns.DatabaseName, Width = 70 });
            if (m_currentsettings.EventsColumns.ObjectName) m_columns.Add(new PerfColumn { Caption = "Object name", Column = ProfilerEventColumns.ObjectName, Width = 70 });
            if (m_currentsettings.EventsColumns.ApplicationName) m_columns.Add(new PerfColumn { Caption = "Application name", Column = ProfilerEventColumns.ApplicationName, Width = 70 });
            if (m_currentsettings.EventsColumns.HostName) m_columns.Add(new PerfColumn { Caption = "Host name", Column = ProfilerEventColumns.HostName, Width = 70 });

            m_columns.Add(new PerfColumn { Caption = "#", Column = -1, Width = 53, Alignment = HorizontalAlignment.Right});
        }

        private void InitGridColumns()
        {
            InitColumns();
            lvEvents.BeginUpdate();
            try
            {
                lvEvents.Columns.Clear();
                foreach (PerfColumn pc in m_columns)
                {
                    var l = lvEvents.Columns.Add(pc.Caption, pc.Width);
                    l.TextAlign = pc.Alignment;
                }
            }
            finally
            {
                lvEvents.EndUpdate();
            }
        }

        private void LvEventsOnVirtualItemsSelectionRangeChanged(object sender, ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs)
        {
            UpdateSourceBox();
        }

        void lvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSourceBox();
        }

        void lvEvents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }


        private string GetEventCaption(ProfilerEvent evt)
        {
            if (evt == m_EventStarted)
            {
                return "Trace started";
            }
            if (evt == m_EventPaused)
            {
                return "Trace paused";
            }
            if (evt == m_EventStopped)
            {
                return "Trace stopped";
            }
            return ProfilerEvents.Names[evt.EventClass];
        }

        private string GetFormattedValue(ProfilerEvent evt,int column,string format)
        {
            return ProfilerEventColumns.Duration == column ? (evt.Duration / 1000).ToString(format) : evt.GetFormattedData(column,format);
        }

        private void NewEventArrived(ProfilerEvent evt,bool last)
        {
            {
                ListViewItem current = (lvEvents.SelectedIndices.Count > 0) ? m_Cached[lvEvents.SelectedIndices[0]] : null;
                m_EventCount++;
                string caption = GetEventCaption(evt);
                ListViewItem lvi = new ListViewItem(caption);
                string []items = new string[m_columns.Count];
                for (int i = 1; i < m_columns.Count;i++ )
                {
                    PerfColumn pc = m_columns[i];
                    items[i - 1] = pc.Column == -1 ? m_EventCount.ToString("#,0") : GetFormattedValue(evt,pc.Column, pc.Format) ?? "";
                }
                lvi.SubItems.AddRange(items);
                lvi.Tag = evt;
                m_Cached.Add(lvi);
                if (last)
                {
                    lvEvents.VirtualListSize = m_Cached.Count;
                    lvEvents.SelectedIndices.Clear();
                    FocusLVI(tbScroll.Checked ? lvEvents.Items[m_Cached.Count - 1] : current, tbScroll.Checked);
                    lvEvents.Invalidate(lvi.Bounds);
                }
            }
        }

        internal void FocusLVI(ListViewItem lvi,bool ensure)
        {
            if (null != lvi)
            {
                lvi.Focused = true;
                lvi.Selected = true;
                listView1_ItemSelectionChanged_1(lvEvents, null);
                if (ensure)
                {
                    lvEvents.EnsureVisible(lvEvents.Items.IndexOf(lvi));
                }
            }
        }

        private void ProfilerThread(Object state)
        {
            try
            {
                while (!m_NeedStop && m_Rdr.TraceIsActive)
                {
                    ProfilerEvent evt = m_Rdr.Next();
                    if (evt != null)
                    {
                        lock (this)
                        {
                            m_events.Enqueue(evt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                lock (this)
                {
                    if (!m_NeedStop && m_Rdr.TraceIsActive)
                    {
                        m_profilerexception = e;
                    }
                }
            }
        }

        private  SqlConnection GetConnection()
        {
            var conn =  new SqlConnection
                       {
                           ConnectionString = DataObjectView.GetConnectionString(connectionString, "master", ApplicationName)
                       };
            return conn;
        }

        public ProfilingStateEnum ProfilingState
        {
            get { return m_ProfilingState; }
        }

        public void StartProfiling()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                m_perf.Clear();
                m_first = null;
                m_prev = null;
                if (m_ProfilingState == ProfilingStateEnum.psPaused)
                {
                    ResumeProfiling();
                    return;
                }
                if (m_Conn != null && m_Conn.State == ConnectionState.Open)
                {
                    m_Conn.Close();
                }
                InitGridColumns();
                m_EventCount = 0;
                m_Conn = GetConnection();
                m_Conn.Open();
                m_Rdr = new RawTraceReader(m_Conn);

                m_Rdr.CreateTrace();
                if (true)
                {
                    if (m_currentsettings.EventsColumns.LoginLogout)
                    {
                        m_Rdr.SetEvent(ProfilerEvents.SecurityAudit.AuditLogin,
                                       ProfilerEventColumns.TextData,
                                       ProfilerEventColumns.LoginName,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime
                            );
                        m_Rdr.SetEvent(ProfilerEvents.SecurityAudit.AuditLogout,
                                       ProfilerEventColumns.CPU,
                                       ProfilerEventColumns.Reads,
                                       ProfilerEventColumns.Writes,
                                       ProfilerEventColumns.Duration,
                                       ProfilerEventColumns.LoginName,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime,
                                       ProfilerEventColumns.ApplicationName,
                                       ProfilerEventColumns.HostName
                            );
                    }

                    if (m_currentsettings.EventsColumns.ExistingConnection)
                    {
                        m_Rdr.SetEvent(ProfilerEvents.Sessions.ExistingConnection,
                                       ProfilerEventColumns.TextData,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime,
                                       ProfilerEventColumns.ApplicationName,
                                       ProfilerEventColumns.HostName
                            );
                    }
                    if (m_currentsettings.EventsColumns.BatchCompleted)
                    {
                        m_Rdr.SetEvent(ProfilerEvents.TSQL.SQLBatchCompleted,
                                       ProfilerEventColumns.TextData,
                                       ProfilerEventColumns.LoginName,
                                       ProfilerEventColumns.CPU,
                                       ProfilerEventColumns.Reads,
                                       ProfilerEventColumns.Writes,
                                       ProfilerEventColumns.Duration,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime,
                                       ProfilerEventColumns.DatabaseName,
                                       ProfilerEventColumns.ApplicationName,
                                       ProfilerEventColumns.HostName
                            );
                    }
                    if (m_currentsettings.EventsColumns.BatchStarting)
                    {
                        m_Rdr.SetEvent(ProfilerEvents.TSQL.SQLBatchStarting,
                                       ProfilerEventColumns.TextData,
                                       ProfilerEventColumns.LoginName,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime,
                                       ProfilerEventColumns.DatabaseName,
                                       ProfilerEventColumns.ApplicationName,
                                       ProfilerEventColumns.HostName
                            );
                    }
                    if (m_currentsettings.EventsColumns.RPCStarting)
                    {
                        m_Rdr.SetEvent(ProfilerEvents.StoredProcedures.RPCStarting,
                                       ProfilerEventColumns.TextData,
                                       ProfilerEventColumns.LoginName,
                                       ProfilerEventColumns.SPID,
                                       ProfilerEventColumns.StartTime,
                                       ProfilerEventColumns.EndTime,
                                       ProfilerEventColumns.DatabaseName,
                                       ProfilerEventColumns.ObjectName,
                                       ProfilerEventColumns.ApplicationName,
                                       ProfilerEventColumns.HostName
                            );
                    }

                }
                if (m_currentsettings.EventsColumns.RPCCompleted)
                {
                    m_Rdr.SetEvent(ProfilerEvents.StoredProcedures.RPCCompleted,
                                   ProfilerEventColumns.TextData, ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU, ProfilerEventColumns.Reads,
                                   ProfilerEventColumns.Writes, ProfilerEventColumns.Duration,
                                   ProfilerEventColumns.SPID
                                   , ProfilerEventColumns.StartTime, ProfilerEventColumns.EndTime
                                   , ProfilerEventColumns.DatabaseName
                                   , ProfilerEventColumns.ObjectName
                                   , ProfilerEventColumns.ApplicationName
                                   , ProfilerEventColumns.HostName
                        );
                }
                if (m_currentsettings.EventsColumns.SPStmtCompleted)
                {
                    m_Rdr.SetEvent(ProfilerEvents.StoredProcedures.SPStmtCompleted,
                                   ProfilerEventColumns.TextData, ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU, ProfilerEventColumns.Reads,
                                   ProfilerEventColumns.Writes, ProfilerEventColumns.Duration,
                                   ProfilerEventColumns.SPID
                                   , ProfilerEventColumns.StartTime, ProfilerEventColumns.EndTime
                                   , ProfilerEventColumns.DatabaseName
                                   , ProfilerEventColumns.ObjectName
                                   , ProfilerEventColumns.ObjectID
                                   , ProfilerEventColumns.ApplicationName
                                   , ProfilerEventColumns.HostName
                        );
                }
                if (m_currentsettings.EventsColumns.SPStmtStarting)
                {
                    m_Rdr.SetEvent(ProfilerEvents.StoredProcedures.SPStmtStarting,
                                   ProfilerEventColumns.TextData, ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU, ProfilerEventColumns.Reads,
                                   ProfilerEventColumns.Writes, ProfilerEventColumns.Duration,
                                   ProfilerEventColumns.SPID
                                   , ProfilerEventColumns.StartTime, ProfilerEventColumns.EndTime
                                   , ProfilerEventColumns.DatabaseName
                                   , ProfilerEventColumns.ObjectName
                                   , ProfilerEventColumns.ObjectID
                                   , ProfilerEventColumns.ApplicationName
                                   , ProfilerEventColumns.HostName
                        );
                }
                if (m_currentsettings.EventsColumns.UserErrorMessage)
                {
                    m_Rdr.SetEvent(ProfilerEvents.ErrorsAndWarnings.UserErrorMessage,
                                   ProfilerEventColumns.TextData,
                                   ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU,
                                   ProfilerEventColumns.SPID,
                                   ProfilerEventColumns.StartTime,
                                   ProfilerEventColumns.DatabaseName,
                                   ProfilerEventColumns.ApplicationName,
                                   ProfilerEventColumns.HostName
                        );
                }
                if (m_currentsettings.EventsColumns.BlockedProcessPeport)
                {
                    m_Rdr.SetEvent(ProfilerEvents.ErrorsAndWarnings.Blockedprocessreport,
                                   ProfilerEventColumns.TextData,
                                   ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU,
                                   ProfilerEventColumns.SPID,
                                   ProfilerEventColumns.StartTime,
                                   ProfilerEventColumns.DatabaseName,
                                   ProfilerEventColumns.ApplicationName,
                                   ProfilerEventColumns.HostName
                        );

                }

                if (m_currentsettings.EventsColumns.SQLStmtStarting)
                {
                    m_Rdr.SetEvent(ProfilerEvents.TSQL.SQLStmtStarting,
                                   ProfilerEventColumns.TextData, ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU, ProfilerEventColumns.Reads,
                                   ProfilerEventColumns.Writes, ProfilerEventColumns.Duration,
                                   ProfilerEventColumns.SPID
                                   , ProfilerEventColumns.StartTime, ProfilerEventColumns.EndTime
                                   , ProfilerEventColumns.DatabaseName
                                   , ProfilerEventColumns.ApplicationName
                                   , ProfilerEventColumns.HostName
                        );
                }
                if (m_currentsettings.EventsColumns.SQLStmtCompleted)
                {
                    m_Rdr.SetEvent(ProfilerEvents.TSQL.SQLStmtCompleted,
                                   ProfilerEventColumns.TextData, ProfilerEventColumns.LoginName,
                                   ProfilerEventColumns.CPU, ProfilerEventColumns.Reads,
                                   ProfilerEventColumns.Writes, ProfilerEventColumns.Duration,
                                   ProfilerEventColumns.SPID
                                   , ProfilerEventColumns.StartTime, ProfilerEventColumns.EndTime
                                   , ProfilerEventColumns.DatabaseName
                                   , ProfilerEventColumns.ApplicationName
                                   , ProfilerEventColumns.HostName
                        );
                }

                if (null != m_currentsettings.Filters.Duration)
                {
                    SetIntFilter(m_currentsettings.Filters.Duration*1000,
                                 m_currentsettings.Filters.DurationFilterCondition, ProfilerEventColumns.Duration);
                }
                SetIntFilter(m_currentsettings.Filters.Reads, m_currentsettings.Filters.ReadsFilterCondition,ProfilerEventColumns.Reads);
                SetIntFilter(m_currentsettings.Filters.Writes, m_currentsettings.Filters.WritesFilterCondition,ProfilerEventColumns.Writes);
                SetIntFilter(m_currentsettings.Filters.CPU, m_currentsettings.Filters.CpuFilterCondition,ProfilerEventColumns.CPU);
                SetIntFilter(m_currentsettings.Filters.SPID, m_currentsettings.Filters.SPIDFilterCondition, ProfilerEventColumns.SPID);

                SetStringFilter(m_currentsettings.Filters.LoginName, m_currentsettings.Filters.LoginNameFilterCondition,ProfilerEventColumns.LoginName);
                SetStringFilter(m_currentsettings.Filters.DatabaseName,m_currentsettings.Filters.DatabaseNameFilterCondition, ProfilerEventColumns.DatabaseName);
                SetStringFilter(m_currentsettings.Filters.TextData, m_currentsettings.Filters.TextDataFilterCondition,ProfilerEventColumns.TextData);
                SetStringFilter(m_currentsettings.Filters.ApplicationName, m_currentsettings.Filters.ApplicationNameFilterCondition, ProfilerEventColumns.ApplicationName);
                SetStringFilter(m_currentsettings.Filters.HostName, m_currentsettings.Filters.HostNameFilterCondition, ProfilerEventColumns.HostName);
                SetStringFilter(m_currentsettings.Filters.ObjectName, m_currentsettings.Filters.ObjectNameFilterCondition, ProfilerEventColumns.ObjectName);


                m_Cmd.Connection = m_Conn;
                m_Cmd.CommandTimeout = 0;
                m_Rdr.SetFilter(ProfilerEventColumns.ApplicationName, LogicalOperators.AND, ComparisonOperators.NotLike, ApplicationName);
                m_Cached.Clear();
                m_events.Clear();
                m_itembysql.Clear();
                lvEvents.VirtualListSize = 0;
                StartProfilerThread();
                //m_servername = edServer.Text;
                //m_username = edUser.Text;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                UpdateButtons();
                Cursor = Cursors.Default;
            }
        }


        private void SetIntFilter(int? value, TraceProperties.IntFilterCondition condition, int column)
        {
            int[] com = new[] { ComparisonOperators.Equal, ComparisonOperators.NotEqual, ComparisonOperators.GreaterThan, ComparisonOperators.LessThan};
            if ((null != value))
            {
                long? v = value;
                m_Rdr.SetFilter(column, LogicalOperators.AND, com[(int)condition], v);
            }
        }

        private void SetStringFilter(string value,TraceProperties.StringFilterCondition condition,int column)
        {
            if (!String.IsNullOrEmpty(value))
            {
                m_Rdr.SetFilter(column, LogicalOperators.AND
                    , condition == TraceProperties.StringFilterCondition.Like ? ComparisonOperators.Like : ComparisonOperators.NotLike
                    , value
                    );
            }

        }

        private void StartProfilerThread()
        { 
            if(m_Rdr!=null)
            {
                m_Rdr.Close();
            }
            m_Rdr.StartTrace();
            m_Thr = new Thread(ProfilerThread) {IsBackground = true, Priority = ThreadPriority.Lowest};
            m_NeedStop = false;
            m_ProfilingState = ProfilingStateEnum.psProfiling;
            NewEventArrived(m_EventStarted,true);
            m_Thr.Start();
        }

        public void ResumeProfiling()
        {
            StartProfilerThread();
            UpdateButtons();
        }

        private void tbStop_Click(object sender, EventArgs e)
        {
            StopProfiling();
        }

        public void StopProfiling()
        {
            if (m_ProfilingState != ProfilingStateEnum.psPaused && m_ProfilingState != ProfilingStateEnum.psProfiling) return;
            tbStop.Enabled = false;
            using (SqlConnection cn = GetConnection())
            {
                cn.Open();
                m_Rdr.StopTrace(cn);
                m_Rdr.CloseTrace(cn);
                cn.Close();
            }
            m_NeedStop = true;
            if (m_Thr.IsAlive)
            {
                m_Thr.Abort();
            }
            m_Thr.Join();
            m_ProfilingState = ProfilingStateEnum.psStopped;
            NewEventArrived(m_EventStopped,true);
            UpdateButtons();
        }

        private void listView1_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {

            UpdateSourceBox();
        }

        private void UpdateSourceBox()
        {
            if (dontUpdateSource) return;
            StringBuilder sb = new StringBuilder();
            reTextData.Tag = null;
            foreach (int i in lvEvents.SelectedIndices)
            {
                ListViewItem lv = m_Cached[i];
                var evt = (ProfilerEvent)lv.Tag;
                if (lv.SubItems[1].Text != "")
                {
                    sb.AppendFormat("{0}{1}\r\n", lv.SubItems[1].Text, QuerySeparator.Replace("\\r\\n", "\r\n").Replace("\\n", "\r\n"));
                    if (!string.IsNullOrEmpty(evt.DatabaseName)) reTextData.Tag = evt.DatabaseName;
                }
            }
            reTextData.Text = sb.ToString();
        }

        private void lvEvents_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = m_Cached[e.ItemIndex];
        }

        private void tbPause_Click(object sender, EventArgs e)
        {
            PauseProfiling();
        }

        public void PauseProfiling()
        {
            using (SqlConnection cn = GetConnection())
            {
                cn.Open();
                m_Rdr.StopTrace(cn);
                cn.Close();
            }
            m_ProfilingState = ProfilingStateEnum.psPaused;
            NewEventArrived(m_EventPaused,true);
            UpdateButtons();
        }


        internal void SelectAllEvents(bool select)
        {
            lock (m_Cached)
            {
                lvEvents.BeginUpdate();
                dontUpdateSource = true;
                try
                {

                    foreach (ListViewItem lv in m_Cached)
                    {
                        lv.Selected = select;
                    }
                }
                finally
                {
                    dontUpdateSource = false;
                    UpdateSourceBox();
                    lvEvents.EndUpdate();
                }
            }
        }

        private void lvEvents_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Queue<ProfilerEvent> saved;
            Exception exc;
            lock (this)
            {
                saved = m_events;
                m_events = new Queue<ProfilerEvent>(10);
                exc = m_profilerexception;
                m_profilerexception = null;
            }
            if (null != exc)
            {
                using (ThreadExceptionDialog dlg = new ThreadExceptionDialog(exc))
                {
                    dlg.ShowDialog();
                }
            }
            lock (m_Cached)
            {
                while (0 != saved.Count)
                {
                    NewEventArrived(saved.Dequeue(), 0 == saved.Count);
                }
                if (m_Cached.Count > m_currentsettings.Filters.MaximumEventCount)
                {
                    while (m_Cached.Count > m_currentsettings.Filters.MaximumEventCount)
                    {
                        m_Cached.RemoveAt(0);
                    }
                    lvEvents.VirtualListSize = m_Cached.Count;
                    lvEvents.Invalidate();
                }

                if ((null == m_prev) || (DateTime.Now.Subtract(m_prev.m_date).TotalSeconds >= 1))
                {
                    PerfInfo curr = new PerfInfo {m_count = m_EventCount};
                    if (m_perf.Count >= 60)
                    {
                        m_first = m_perf.Dequeue();
                    }
                    if (null == m_first) m_first = curr;
                    if (null == m_prev) m_prev = curr;

                    DateTime now = DateTime.Now;
                    double d1 = now.Subtract(m_prev.m_date).TotalSeconds;
                    double d2 = now.Subtract(m_first.m_date).TotalSeconds;
                    slEPS.Text = String.Format("{0} / {1} EPS(last/avg for {2} second(s))",
                        (Math.Abs(d1 - 0) > 0.001 ? ((curr.m_count - m_prev.m_count)/d1).ToString("#,0.00") : ""),
                                 (Math.Abs(d2 - 0) > 0.001 ? ((curr.m_count - m_first.m_count) / d2).ToString("#,0.00") : ""), d2 .ToString("0"));

                    m_perf.Enqueue(curr);
                    m_prev = curr;
                }

            }
        }

        private void tbAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void ClearTrace()
        {
            lock (lvEvents)
            {
                m_Cached.Clear();
                m_itembysql.Clear();
                lvEvents.VirtualListSize = 0;
                listView1_ItemSelectionChanged_1(lvEvents, null);
                lvEvents.Invalidate();
            }
        }

        private void tbClear_Click(object sender, EventArgs e)
        {
            ClearTrace();
        }

        private void NewAttribute(XmlNode node, string name, string value)
        {
            XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
        }
        private void NewAttribute(XmlNode node, string name, string value, string namespaceURI)
        {
            XmlAttribute attr = node.OwnerDocument.CreateAttribute("ss", name, namespaceURI);
            attr.Value = value;
            node.Attributes.Append(attr);
        }

        private void copyAllToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyEventsToClipboard(false);
        }

        private void CopyEventsToClipboard(bool copySelected)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("events");
            lock (m_Cached)
            {
                if (copySelected)
                {
                    foreach (int i in lvEvents.SelectedIndices)
                    {
                        CreateEventRow((ProfilerEvent)(m_Cached[i]).Tag, root);
                    }
                }
                else
                {
                    foreach (var i in m_Cached)
                    {
                        CreateEventRow((ProfilerEvent)i.Tag, root);
                    }
                }
            }
            doc.AppendChild(root);
            doc.PreserveWhitespace = true;
            using (StringWriter writer = new StringWriter())
            {
                XmlTextWriter textWriter = new XmlTextWriter(writer) {Formatting = Formatting.Indented};
                doc.Save(textWriter);
                Clipboard.SetText(writer.ToString());
            }
            MessageBox.Show("Event(s) data copied to clipboard", "Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void CreateEventRow(ProfilerEvent evt, XmlNode root)
        {
            XmlNode row = root.OwnerDocument.CreateElement("event");
            NewAttribute(row, "EventClass", evt.EventClass.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "CPU", evt.CPU.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Reads", evt.Reads.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Writes", evt.Writes.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Duration", evt.Duration.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "SPID", evt.SPID.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "LoginName", evt.LoginName);
            NewAttribute(row, "DatabaseName", evt.DatabaseName);
            NewAttribute(row, "ObjectName", evt.ObjectName);
            NewAttribute(row, "ApplicationName", evt.ApplicationName);
            NewAttribute(row, "HostName", evt.HostName);
            row.InnerText = evt.TextData;
            root.AppendChild(row);
        }

        private void copySelectedToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyEventsToClipboard(true);
        }

        private void clearTraceWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearTrace();
        }

        private void extractAllEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyEventsToClipboard(false);
        }

        private void extractSelectedEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyEventsToClipboard(true);
        }


        private void pauseTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PauseProfiling();
        }

        private void stopTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopProfiling();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFind();
        }

        private void DoFind()
        {
            if (m_ProfilingState == ProfilingStateEnum.psProfiling)
            {
                MessageBox.Show("You cannot find when trace is running", "ExpressProfiler", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            using (FindForm f = new FindForm(this))
            {
                f.ShowDialog();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvEvents.Focused && (m_ProfilingState!=ProfilingStateEnum.psProfiling))
            {
                SelectAllEvents(true);
            }
            else
            if (reTextData.Focused)
            {
                reTextData.SelectAll();
            }
        }

        internal void PerformFind(bool forwards)
        {
            if(String.IsNullOrEmpty(lastpattern)) return;

            if (forwards)
            {
                for (int i = lastpos = lvEvents.Items.IndexOf(lvEvents.FocusedItem) + 1; i < m_Cached.Count; i++)
                {
                    if (FindText(i))
                    {
                        return;
                    }
                }
            }
            else
            {
                for (int i = lastpos = lvEvents.Items.IndexOf(lvEvents.FocusedItem) - 1; i > 0; i--)
                {
                    if (FindText(i))
                    {
                        return;
                    }
                }
            }
            MessageBox.Show(String.Format("Failed to find \"{0}\". Searched to the end of data. ", lastpattern), "ExpressProfiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool FindText(int i)
        {
            ListViewItem lvi = m_Cached[i];
            ProfilerEvent evt = (ProfilerEvent) lvi.Tag;
            string pattern = (wholeWord ? "\\b" + lastpattern + "\\b" : lastpattern);
            if (Regex.IsMatch(evt.TextData, pattern, (matchCase ? RegexOptions.None : RegexOptions.IgnoreCase)))
            {
                lvi.Focused = true;
                lastpos = i;
                SelectAllEvents(false);
                FocusLVI(lvi, true);
                return true;
            }

            return false;
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_ProfilingState == ProfilingStateEnum.psProfiling)
            {
                MessageBox.Show("You cannot find when trace is running", "ExpressProfiler", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }
            PerformFind(true);
        }

        internal void RunProfiling(bool showfilters)
        {
            if (showfilters)
            {
                TraceProperties.TraceSettings ts = m_currentsettings.GetCopy();
                using (TraceProperties frm = new TraceProperties())
                {
                    frm.SetSettings(ts);
                    if (DialogResult.OK != frm.ShowDialog()) return;
                    m_currentsettings = frm.m_currentsettings.GetCopy();
                }
            }
            StartProfiling();
        }

        private void tbRunWithFilters_Click(object sender, EventArgs e)
        {
            RunProfiling(true);
        }

        private void copyToXlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyForExcel();
        }

        private void CopyForExcel()
        {

            XmlDocument doc = new XmlDocument();
            XmlProcessingInstruction pi = doc.CreateProcessingInstruction("mso-application", "progid='Excel.Sheet'");
            doc.AppendChild(pi); 
            const string urn = "urn:schemas-microsoft-com:office:spreadsheet";
            XmlNode root = doc.CreateElement("ss","Workbook",urn);
            NewAttribute(root, "xmlns:ss", urn);
            doc.AppendChild(root);

            XmlNode styles = doc.CreateElement("ss","Styles", urn);
            root.AppendChild(styles);
            XmlNode style = doc.CreateElement("ss","Style", urn);
            styles.AppendChild(style);
            NewAttribute(style,"ID","s62",urn);
            XmlNode font = doc.CreateElement("ss","Font",urn);
            style.AppendChild(font);
            NewAttribute(font, "Bold", "1", urn);


            XmlNode worksheet = doc.CreateElement("ss", "Worksheet", urn);
            root.AppendChild(worksheet);
            NewAttribute(worksheet, "Name", "Sql Trace", urn);
            XmlNode table = doc.CreateElement("ss", "Table", urn);
            worksheet.AppendChild(table);
            NewAttribute(table, "ExpandedColumnCount",m_columns.Count.ToString(CultureInfo.InvariantCulture),urn);

            foreach (ColumnHeader lv in lvEvents.Columns)
            {
                XmlNode r = doc.CreateElement("ss","Column", urn);
                NewAttribute(r, "AutoFitWidth","0",urn);
                NewAttribute(r, "Width", lv.Width.ToString(CultureInfo.InvariantCulture), urn);
                table.AppendChild(r);
            }

            XmlNode row = doc.CreateElement("ss","Row", urn);
            table.AppendChild(row);
            foreach (ColumnHeader lv in lvEvents.Columns)
            {
                XmlNode cell = doc.CreateElement("ss","Cell", urn);
                row.AppendChild(cell);
                NewAttribute(cell, "StyleID","s62",urn);
                XmlNode data = doc.CreateElement("ss","Data", urn);
                cell.AppendChild(data);
                NewAttribute(data, "Type","String",urn);
                data.InnerText = lv.Text;
            }

            lock (m_Cached)
            {
                foreach (ListViewItem lvi in m_Cached)
                {
                    row = doc.CreateElement("ss", "Row", urn);
                    table.AppendChild(row);
                    for (int i = 0; i < m_columns.Count; i++)
                    {
                        PerfColumn pc = m_columns[i];
                        if(pc.Column!=-1)
                        {
                        XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                        row.AppendChild(cell);
                        XmlNode data = doc.CreateElement("ss", "Data", urn);
                        cell.AppendChild(data);
                            string dataType;
                            switch (ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column])
                            {
                                    case ProfilerColumnDataType.Int:
                                    case ProfilerColumnDataType.Long:
                                        dataType = "Number";
                                    break;
                                    case ProfilerColumnDataType.DateTime:
                                        dataType = "String";
                                    break;
                                default:
                                        dataType = "String";
                                    break;
                            }
                        if (ProfilerEventColumns.EventClass == pc.Column) dataType = "String";
                        NewAttribute(data, "Type", dataType, urn);
                        if (ProfilerEventColumns.EventClass == pc.Column)
                        {
                            data.InnerText = GetEventCaption(((ProfilerEvent) (lvi.Tag)));
                        }
                        else
                        {
                            data.InnerText = pc.Column == -1
                                                 ? ""
                                                 : GetFormattedValue((ProfilerEvent)(lvi.Tag),pc.Column,ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column]==ProfilerColumnDataType.DateTime?pc.Format:"") ??
                                                   "";
                        }
                            }
                    }
                }
            }
            using (StringWriter writer = new StringWriter())
            {
                XmlTextWriter textWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented,Namespaces = true};
                doc.Save(textWriter);
                string xml = writer.ToString();
                MemoryStream xmlStream = new MemoryStream();
                xmlStream.Write(System.Text.Encoding.UTF8.GetBytes(xml), 0, xml.Length);
                Clipboard.SetData("XML Spreadsheet", xmlStream);
            }
            MessageBox.Show("Event(s) data copied to clipboard", "Information", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        }

        private void mnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(versionString+"\nhttps://expressprofiler.codeplex.com/", "About", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void tbStayOnTop_Click(object sender, EventArgs e)
        {
            SetStayOnTop();
        }

        private void SetStayOnTop()
        {
            tbStayOnTop.Checked = !tbStayOnTop.Checked;
            ParentForm.TopMost = tbStayOnTop.Checked;
            if (parentInit) ParentForm_Activated(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SetTransparent();
        }

        private bool parentInit;
        private void SetTransparent()
        {
            tbTransparent.Checked = !tbTransparent.Checked;
            ParentForm.Opacity = tbTransparent.Checked ? 0.50 : 1;
            if (!parentInit)
            {
                ParentForm.Activated += ParentForm_Activated;
                ParentForm.Deactivate += ParentForm_Deactivate;
                parentInit = true;
            }
        }

        private void ParentForm_Deactivate(object sender, EventArgs e)
        {
            ParentForm.Opacity = tbTransparent.Checked ? 0.50 : 1;
        }

        private void ParentForm_Activated(object sender, EventArgs e)
        {
            ParentForm.Opacity = !tbStayOnTop.Checked && tbTransparent.Checked ? 0.50 : 1; ;
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStayOnTop();
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetTransparent();
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = lvEvents.SelectedIndices.Count-1; i >= 0; i--)
            {
                m_Cached.RemoveAt(lvEvents.SelectedIndices[i]);
            }
            lvEvents.VirtualListSize = m_Cached.Count;
            lvEvents.SelectedIndices.Clear();
        }

        private void keepSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = m_Cached.Count - 1; i >= 0; i--)
            {
                if (!lvEvents.SelectedIndices.Contains(i))
                {
                    m_Cached.RemoveAt(i);
                }
            }
            lvEvents.VirtualListSize = m_Cached.Count;
            lvEvents.SelectedIndices.Clear();
        }

        private void splitContainer1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawGrabHandle(e.Graphics, splitContainer1.SplitterRectangle, true, true);
        }

        private void mnuQuickFilterDbAny_Click(object sender, EventArgs e)
        {
            SetQuickDbFilter(null);
        }

        private void SetQuickDbFilter(object sender)
        {
            var mnu = sender as ToolStripItem;
            m_currentsettings.Filters.DatabaseName = mnu == null ? "" : mnu.Text;
            m_currentsettings.Filters.DatabaseNameFilterCondition = TraceProperties.StringFilterCondition.Like;
        }

        private void mnuQuickFilterLoginAny_Click(object sender, EventArgs e)
        {
            mnuQuickFilterLoginCurrent_Click(null, null);
        }

        private void mnuQuickFilterLoginCurrent_Click(object sender, EventArgs e)
        {
            var mnu = sender as ToolStripItem;
            m_currentsettings.Filters.LoginName = mnu == null ? "" : mnu.Text;
            m_currentsettings.Filters.LoginNameFilterCondition = TraceProperties.StringFilterCondition.Like;
        }

        private void mnuQuickFilterHostAny_Click(object sender, EventArgs e)
        {
            mnuQuickFilterHostCurrent_Click(null, null);
        }

        private void mnuQuickFilterHostCurrent_Click(object sender, EventArgs e)
        {
            var mnu = sender as ToolStripItem;
            m_currentsettings.Filters.HostName = mnu == null ? "" : mnu.Text;
            m_currentsettings.Filters.HostNameFilterCondition = TraceProperties.StringFilterCondition.Like;
        }

        public bool IsHotKeyActive
        {
            get { return btnHotkeyActive.Checked; }
        }
        public event EventHandler HotKeyActiveChanged;
        private void btnHotkeyActive_Click(object sender, EventArgs e)
        {
            if (HotKeyActiveChanged != null) HotKeyActiveChanged(this, EventArgs.Empty);
        }

        private void reTextDataSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reTextData.SelectAll();
        }
        public FastColoredTextBox TextDataEditor
        {
            get { return reTextData; }
        }

        private void notSpresetconnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleSpResetConnection(m_currentsettings);
        }

        private void toggleSpResetConnection(TraceProperties.TraceSettings settings)
        {
            if (settings.Filters.ObjectName == "sp_reset_connection")
            {
                settings.Filters.ObjectName = null;
                settings.Filters.ObjectNameFilterCondition = TraceProperties.StringFilterCondition.Like;
            }
            else
            {
                settings.Filters.ObjectName = "sp_reset_connection";
                settings.Filters.ObjectNameFilterCondition = TraceProperties.StringFilterCondition.NotLike;
            }
        }

        private void btnQuickFilter_Click(object sender, EventArgs e)
        {
            btnQuickFilter.ShowDropDown();
        }

        private void cbQuerySeparator_TextChanged(object sender, EventArgs e)
        {
            QuerySeparator = cbQuerySeparator.Text;
        }

        private void cbQuerySeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySeparator = cbQuerySeparator.Text;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(reTextData.Text) || string.IsNullOrEmpty(connectionString)) return;
            var profilingActive = m_ProfilingState == ProfilingStateEnum.psProfiling;
            if (profilingActive) PauseProfiling();
            var connStr = $"{reTextData.Tag}";
            connStr = string.IsNullOrEmpty(connStr) ? connectionString : DataObjectView.GetConnectionString(connectionString, connStr);
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlDataAdapter(reTextData.Text, conn))
                    {
                        var table = new DataTable();
                        try
                        {
                            cmd.Fill(table);
                            var oldTable = sourceQuery.DataSource is DataTable ? sourceQuery.DataSource as DataTable : null;
                            sourceQuery.DataSource = table;
                            if (oldTable != null) oldTable.Dispose();
                        }
                        catch { }
                    }
                }
            }
            finally
            {
                if (profilingActive) ResumeProfiling();
            }
        }

        private void splitContainer2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawGrabHandle(e.Graphics, splitContainer2.SplitterRectangle, true, true);
        }

        private void btnQuickFilter_DropDownOpening(object sender, EventArgs e)
        {
            notSpresetconnectionToolStripMenuItem.Checked =
                m_currentsettings.Filters.ObjectName == "sp_reset_connection" &&
                m_currentsettings.Filters.ObjectNameFilterCondition == TraceProperties.StringFilterCondition.NotLike;
        }
    }
}
