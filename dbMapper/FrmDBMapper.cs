using Com.StellmanGreene.CSVReader;
using DBMapper.Properties;
using FastColoredTextBoxNS;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace DBMapper
{
    public partial class FrmDBMapper : Form
    {
        private const int ProfilerHotKeyId = 0x2304;

        private TreeListView objectsTree;
        public static readonly int DATATOPROWS = Settings.Default.TopRowsPreview;
        public FrmDBMapper()
        {
            InitializeComponent();
            contextMenuQueryEditor_Opening(null, null); //dummy to initialize
            dataViewCurrentObject.DataCaption = String.Format("Top {0}", DATATOPROWS);
            txtDsResultSQL.Dock = DockStyle.Fill;
            txtDsResultSQL.Visible = false;
            objectsTree = new TreeListView();
            panObjects.Controls.Add(objectsTree, 0, 1);
            panObjects.SetColumnSpan(objectsTree, panObjects.ColumnStyles.Count);
            objectsTree.Dock = DockStyle.Fill;
            objectsTree.Columns.Add("Name").Width = 200;
            objectsTree.Columns.Add("Modified").Width = 150;
            objectsTree.Columns.Add("Created").Width = 150;
            objectsTree.ShowPlusMinus = true;
            objectsTree.FullRowSelect = true;
            objectsTree.HideSelection = false;
            dataViewCurrentObject.ScriptAvailable = dataViewQuery.ScriptAvailable = false;
            objectsTree.LargeImageList = objectsTree.SmallImageList = ilObjects;
            objectsTree.Click += (s, e) =>
            {
                objectsTree_ItemActivate(s, e);
                ActiveControl = objectsTree;
                objectsTree.DrawPlusMinusItems();
            };
            objectsTree.KeyUp += (s, e) =>
            {
                var fcItem = objectsTree.FocusedItem;
                var objData = fcItem == null ? null : fcItem.Tag as SQLObjectData;
                objectsTree.DrawPlusMinusItems();
                switch (e.KeyCode)
                {
                    case Keys.Subtract:
                        {
                            if (e.Control) fcItem.CollapseAll(); else fcItem.Collapse();
                        }
                        break;
                    case Keys.Add:
                        if (objData != null)
                        {
                            objectsTree_ItemActivate(fcItem, EventArgs.Empty);
                            ActiveControl = objectsTree;
                        }
                        else if (fcItem != null)
                        {
                            if (e.Control) fcItem.ExpandAll(); else fcItem.Expand();
                        }
                        break;
                    case Keys.PageUp:
                        if ((!objectsTree.Scrollable || e.Control) && fcItem != null && fcItem.Parent != null)
                        {
                            fcItem.Selected = false;
                            fcItem.Parent.EnsureVisible();
                            fcItem.Parent.Focused = true;
                            fcItem.Parent.Selected = true;
                        }
                        else
                            if (objectsTree.Scrollable)
                            objectsTree.Scroll(0, -(objectsTree.ClientSize.Height - 30));
                        break;
                    case Keys.PageDown:
                        if (objectsTree.Scrollable)
                            objectsTree.Scroll(0, objectsTree.ClientSize.Height - 30);
                        break;
                    case Keys.Enter:
                        if (fcItem != null)
                        {
                            objectsTree_ItemActivate(fcItem, EventArgs.Empty);
                        }
                        break;
                }
            };
            objectsTree.ContextMenuStrip = menuObjectTreeItem;
            registerHotkey();
            sqlProfiler.HotKeyActiveChanged += sqlProfiler_HotKeyActiveChanged;
            var mitem = sqlProfiler.TextDataEditor.ContextMenuStrip.Items.Add("Selected text to query box");
            mitem.Click += (s, e) =>
            {
                ChangeTxtSQL(sqlProfiler.TextDataEditor.SelectedText, false);
            };
            mitem = sqlProfiler.TextDataEditor.ContextMenuStrip.Items.Add("Search DB objects for selected text");
            mitem.Click += (s, e) =>
            {
                tabMain.SelectedTab = pageObjects;
                txtObjFilter.Text = sqlProfiler.TextDataEditor.SelectedText;
                RefreshObjects(sqlProfiler.TextDataEditor.SelectedText, false);
            };
            var ctypes = new List<string>();
            foreach (var v in Enum.GetValues(typeof(CompareType))) ctypes.Add(CompareValue.CompareTypeText((CompareType)v));
            cbDsSchema.Items.Clear(); cbDsSchema.Items.AddRange(ctypes.ToArray());
            cbDsObject.Items.Clear(); cbDsObject.Items.AddRange(ctypes.ToArray());
            cbDsColName.Items.Clear(); cbDsColName.Items.AddRange(ctypes.ToArray());
            cbDsColType.Items.Clear(); cbDsColType.Items.AddRange(ctypes.ToArray());
            cbDsContent.Items.Clear(); cbDsContent.Items.AddRange(ctypes.ToArray());
            cbDsSchema.SelectedIndex = cbDsObject.SelectedIndex =
                cbDsColName.SelectedIndex = cbDsContent.SelectedIndex = (int)CompareType.Like;
            cbDsColType.SelectedIndex = (int)CompareType.Equals;
            foreach (var item in string.Format("{0}, {1}, {2}, {3}, ",
                DataSearchColumn.DataTypes_String, DataSearchColumn.DataTypes_Datetime, DataSearchColumn.DataTypes_Numbers, DataSearchColumn.DataTypes_Binary)
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).OrderBy(s => s))
                cbDsValueColType.Items.Add(item);
        }

        private void registerHotkey()
        {
            hotkeyRegistered = this.RegisterHotKey(ProfilerHotKeyId, Hotkey.KeyModifier.Control | Hotkey.KeyModifier.Alt, Keys.P);
        }

        private void unregisterHotkey()
        {
            if (hotkeyRegistered) this.UnregisterHotKey(ProfilerHotKeyId);
            hotkeyRegistered = false;
        }

        void sqlProfiler_HotKeyActiveChanged(object sender, EventArgs e)
        {
            if (sqlProfiler.IsHotKeyActive)
            {
                if (!hotkeyRegistered) registerHotkey();
            }
            else
            {
                if (hotkeyRegistered) unregisterHotkey();
            }
        }

        private bool hotkeyRegistered;

        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);
            }
            catch
            {

            }
            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                                // The key of the hotkey that was pressed.
                Hotkey.KeyModifier modifier = (Hotkey.KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int keyId = m.WParam.ToInt32();                                                   // The id of the hotkey that was pressed.

                if (keyId == ProfilerHotKeyId)
                {
                    tabMain.SelectedTab = pageProfiler;
                    switchProfilingState();
                }
            }

        }

        private void switchProfilingState()
        {
            switch (sqlProfiler.ProfilingState)
            {
                case ExpressProfiler.SQLProfiler.ProfilingStateEnum.psPaused:
                    sqlProfiler.ResumeProfiling();
                    break;
                case ExpressProfiler.SQLProfiler.ProfilingStateEnum.psProfiling:
                    sqlProfiler.PauseProfiling();
                    break;
                default:
                    sqlProfiler.StartProfiling();
                    break;
            }
        }

        private string connString;
        public string ConnectionString
        {
            get { return connString; }
            set
            {
                if (connString == value || !SetServerName(value)) return;
                objectsTree.Items.Clear();
                var dbname = GetInitialCatalog(value).ToUpper();
                int dbidx = -1;
                using (SqlConnection conn = new SqlConnection(value))
                {
                    try
                    {
                        conn.Open();
                        connString = value;
                        dbidx = FillDatabases(conn, dbname);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Error on connecting server {0}: {1}\r\n{2}",
                            CurrentServerName, ex.Message, ex.InnerException == null ? "" : ex.InnerException.Message));
                        return;
                    }
                }
                if (dbidx >= 0) listDsDb.SelectedIndex = cbObjectsDB.SelectedIndex = cbDatabases.SelectedIndex = dbidx;
                dataViewCurrentObject.ConnectionString = dataViewQuery.ConnectionString = value;
            }
        }

        private int FillDatabases(SqlConnection conn, string dbname = null)
        {
            var dbidx = -1;
            cbDatabases.Items.Clear();
            cbObjectsDB.Items.Clear();
            listDsDb.Items.Clear();
            List<string> mappedDbs = null;
            if (chkLoginMappingsOnly.Checked)
            {
                mappedDbs = new List<string>();
                using (var cmd = new SqlCommand(@"declare @tempww table (
    LoginName nvarchar(max),
    DBname nvarchar(max),
    Username nvarchar(max), 
    AliasName nvarchar(max)
)
INSERT INTO @tempww EXEC master..sp_msloginmappings 
select DBName from @tempww where LoginName = ORIGINAL_LOGIN() order by DBname;
", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mappedDbs.Add(reader[0].ToString());
                        }
                    }
                }
            }
            using (var cmd = new SqlCommand("select name from sys.databases order by name", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader[0].ToString();
                        if (mappedDbs != null && !mappedDbs.Contains(name)) continue;
                        if (name.ToUpper() == dbname) dbidx = cbDatabases.Items.Count;
                        cbDatabases.Items.Add(name);
                        cbObjectsDB.Items.Add(name);
                        listDsDb.Items.Add(name);
                    }
                }
            }
            sqlProfiler.InitConnection(connString, mappedDbs);
            return dbidx;
        }

        public string CurrentServerName
        {
            get;
            private set;
        }
        private bool SetServerName(string connString)
        {
            try
            {
                if (string.IsNullOrEmpty(connString)) return false;
                var csb = new SqlConnectionStringBuilder(connString);
                CurrentServerName = csb.DataSource;
                Text = String.Format("DBMapper - [{0}]", CurrentServerName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Connection Error: {0}\r\n{1}", ex.Message, ex.InnerException == null ? "" : ex.InnerException.Message));
                return false;
            }
        }

        void objectsTree_ItemActivate(object sender, EventArgs e)
        {
            if (objectsTree.FocusedItem == null) return;
            var objData = objectsTree.FocusedItem.Tag as SQLObjectData;
            var currentItem = objectsTree.FocusedItem;
            if (currentItem.IsExpanded)
                currentItem.Collapse();
            else if (currentItem.ChildrenCount > 0)
                currentItem.Expand();
            var currentdb = cbObjectsDB.Text;
            while (currentItem != null)
            {
                if (currentItem.ImageIndex == 6)
                {
                    string dbname = currentItem.Text;
                    if (dbname != currentdb)
                    {
                        for (int i = 0; i < cbObjectsDB.Items.Count; i++)
                        {
                            if (cbObjectsDB.Items[i].ToString() == dbname)
                            {
                                cbObjectsDB.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    break;
                }
                currentItem = currentItem.Parent;
            }
            if (objData == null) return;
            dataViewCurrentObject.ScriptAvailable = !String.IsNullOrEmpty(objData.Script);
            dataViewCurrentObject.ScriptSource = objData.Script;
            dataViewCurrentObject.DataAvailable = objData.ObjectType == SQLObjectType.Table || objData.ObjectType == SQLObjectType.View;
            //View or Table
            if (objData.ObjectType == SQLObjectType.Table || objData.ObjectType == SQLObjectType.View)
            {
                dataViewCurrentObject.QueryData(objData.DbName
                    , String.Format("select TOP {2} * from [{0}].[{1}]", objData.Schema, objData.Name, DATATOPROWS)
                    , 0, objData.ObjectType == SQLObjectType.Table ? objData.Schema : ""
                    , objData.Name);
            }
        }

        private void Query_Click(object sender, EventArgs e)
        {
            Query.Enabled = false;
            Cursor = Cursors.WaitCursor;
            try
            {
                var sql = String.IsNullOrEmpty(txtSQL.SelectedText) ? txtSQL.Text : txtSQL.SelectedText;
                dataViewQuery.QueryData(cbDatabases.Text, sql, 2);
            }
            finally
            {
                Query.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                txtSQL.Text = File.ReadAllText(openFileDialog1.FileName);
                saveFileDialog1.FileName = openFileDialog1.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, txtSQL.Text);
                openFileDialog1.FileName = saveFileDialog1.FileName;
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabMain.SelectedTab == pageObjects && objectsTree.ItemsCount == 0) RefreshObjects("");
        }

        private string fullTextSearch;

        private void RefreshObjects(string filter, bool fullText)
        {
            filter = (filter ?? "").Trim();
            if (String.IsNullOrEmpty(filter))
                fullText = false;
            else
            {
                var txtfound = false;
                foreach (var txt in txtObjFilter.Items)
                {
                    if (txt.ToString() == filter)
                    {
                        txtfound = true;
                        break;
                    }
                }
                if (!txtfound) txtObjFilter.Items.Insert(0, filter);
            }
            var schema = "";
            var filterObjName = filter;
            int p = -1;
            if (!String.IsNullOrEmpty(filter) && (p = filter.LastIndexOf('.')) > 0)
            {
                schema = filter.Substring(0, p);
                filterObjName = p >= filter.Length - 1 ? "" : filter.Substring(p + 1);
            }
            var dbName = "";
            if (!String.IsNullOrEmpty(schema) && (p = schema.LastIndexOf('.')) > 0)
            {
                dbName = schema.Substring(0, p);
                schema = p >= schema.Length - 1 ? "" : schema.Substring(p + 1);
            }
            schema = ProgUtils.SimplifySQLName(schema);
            if (!string.IsNullOrEmpty(dbName))
                for (var i = 0; i < cbObjectsDB.Items.Count; i++)
                    if (cbObjectsDB.Items[i].ToString().ToUpper() == dbName)
                    {
                        cbObjectsDB.SelectedIndex = i;
                        Application.DoEvents();
                        break;
                    }
            var defFilter = "";
            var colFilter = "";
            var synFilter = "";
            if (!fullText)
            {
                dataViewCurrentObject.ClearMarkerText();
                filterObjName = string.Join("%' OR o.name like '%", ProgUtils.SimplifySQLName(filterObjName).Split(';'));
            }
            else
            { 
                filter = ProgUtils.SimplifySQLName(filter);
                var flist = ProgUtils.SimplifySQLName(filterObjName).Split(';').ToList();
                if (flist[0] != filter) flist.Insert(0, filter);
                defFilter = string.Join("%' OR m.definition like '%", flist);
                colFilter = string.Join("%' OR name like '%", ProgUtils.SimplifySQLName(filterObjName).Split(';'));
                synFilter = string.Join("%' OR base_object_name like '%", ProgUtils.SimplifySQLName(filterObjName).Split(';'));
            }
            string objectsSQL = @"select 
o.type_desc, s.name as schemaname, o.name, isnull(m.definition, syn.base_object_name) as definition, o.modify_date, o.create_date, o.object_id
from sys.objects o 
left join sys.sql_modules m on o.object_id=m.object_id
left join sys.synonyms syn on syn.object_id=o.object_id
inner join sys.schemas s on s.schema_id=o.schema_id
where (o.type in ('P', 'U', 'V', 'SN') or o.type_desc like '%_function') 
and ((o.name like '%" + filterObjName + "%') " +
                     (String.IsNullOrEmpty(schema) ? "" : "and s.name like '%" + schema + "%' ") +
                     (fullText ? "or (m.definition like '%" + defFilter +
                     "%') or (o.object_id in (select object_id from sys.columns where name like '%" + colFilter +
                     "%') or (o.object_id in (select object_id from sys.synonyms where base_object_name like '%" + synFilter +
                     "%')))" : "")
                     + @")
order by o.type_desc, s.name, o.name";

            Cursor = Cursors.WaitCursor;
            btnObjFilter.Enabled = false;
            var dbname = cbObjectsDB.Text;
            TreeListViewItem dbItem = null;
            foreach (TreeListViewItem item in objectsTree.Items)
            {
                if (item.Text == dbname)
                {
                    dbItem = item;
                    break;
                }
            }
            if (dbItem == null)
            {
                dbItem = objectsTree.Items.Add(dbname);
                dbItem.SubItems.Add("");
                dbItem.ImageIndex = 7;
            }
            else
            {
                dbItem.Items.Clear();
            }
            dbItem.SubItems[1].Text = dataViewCurrentObject.FulltextSearch = fullTextSearch = fullText ? filter : "";
            try
            {
                using (SqlConnection conn = new SqlConnection(DataObjectView.GetConnectionString(ConnectionString, dbname)))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(objectsSQL, conn))
                    {
                        TreeListViewItem lastTypeNode = null;
                        TreeListViewItem lastSchemaNode = null;
                        var NULLDate = new DateTime();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var typename = (string)GetData(reader, 0, "");
                                var objData = new SQLObjectData()
                                {
                                    DbName = dbname,
                                    Schema = (string)GetData(reader, 1, ""),
                                    Name = (string)GetData(reader, 2, ""),
                                    Script = (string)GetData(reader, 3, ""),
                                    LastModified = (DateTime)GetData(reader, 4, NULLDate),
                                    Created = (DateTime)GetData(reader, 5, NULLDate)
                                };
                                if (lastTypeNode == null || lastTypeNode.Text != typename)
                                {
                                    lastTypeNode = dbItem.Items.Add(typename);
                                    lastTypeNode.ImageIndex = 5;
                                    lastSchemaNode = null;
                                    dbItem.Expand();
                                }
                                if (lastSchemaNode == null || lastSchemaNode.Text != objData.Schema)
                                {
                                    Application.DoEvents();
                                    lastSchemaNode = lastTypeNode.Items.Add(objData.Schema);
                                    lastSchemaNode.ImageIndex = 6;
                                }
                                var objNode = lastSchemaNode.Items.Add(objData.Name);
                                objNode.Tag = objData;
                                objNode.SubItems.Add(objData.LastModified.ToString("yyyy.MM.dd HH:mm:ss"));
                                objNode.SubItems.Add(objData.Created.ToString("yyyy.MM.dd HH:mm:ss"));
                                if (typename.Contains("VIEW")) objData.ObjectType = SQLObjectType.View;
                                else if (typename.Contains("PROCEDURE")) objData.ObjectType = SQLObjectType.Procedure;
                                else if (typename.Contains("FUNCTION")) objData.ObjectType = SQLObjectType.Function;
                                else if (typename.Contains("SYNONYM")) objData.ObjectType = SQLObjectType.Synonym;
                                else objData.ObjectType = SQLObjectType.Table;
                                objNode.ImageIndex = (int)objData.ObjectType;
                                Application.DoEvents();
                            }
                            if (dbItem.Items.Count == 1)
                                dbItem.Items[0].ExpandAll();
                            else
                                dbItem.Expand();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error on get db-objects:\n{0}", ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
                btnObjFilter.Enabled = true;
            }
            objectsTree.DrawPlusMinusItems();
        }

        public object GetData(SqlDataReader reader, int column, object NullValue)
        {
            if (reader.IsDBNull(column)) return NullValue;
            return reader[column];
        }

        private void btnObjFilter_Click(object sender, EventArgs e)
        {
            var p = btnObjFilter.PointToScreen(new Point(0, btnObjFilter.Height));
            //RefreshObjects(txtObjFilter.Text, true);
            menuObjFilter.Show(p);
        }

        private void txtObjFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = e.Handled = true;
                if (e.Alt)
                    mnuObjFilterFulltextSearch_Click(null, null);
                else
                    mnuObjFilterNamesSearch_Click(null, null);
            }
        }

        private void txtObjScript_DoubleClick(object sender, EventArgs e)
        {
            if (mnuScriptTextDblClick.Checked)
                ShowScriptWindow2();
        }

        private void ShowScriptWindow2()
        {
            mnuScriptTextShowWindow2.Checked = dataViewCurrentObject.ShowScriptWindow2();
        }

        private void ScriptEditor_Enter(object sender, EventArgs e)
        {
            menuObjScriptText.Tag = sender;
        }

        private void mnuObjToClipboard_Click(object sender, EventArgs e)
        {
            if (objectsTree.FocusedItem == null || objectsTree.FocusedItem.Tag == null) return;
            var objData = objectsTree.FocusedItem.Tag as SQLObjectData;
            Clipboard.SetText(objData.GetFullName(mnuObjDBPrefix.Checked, true));
        }

        private void mnuObjNoBraceToClipboard_Click(object sender, EventArgs e)
        {
            if (objectsTree.FocusedItem == null || objectsTree.FocusedItem.Tag == null) return;
            var objData = objectsTree.FocusedItem.Tag as SQLObjectData;
            var paramstr = "";
            if (objData.ObjectType == SQLObjectType.Procedure || objData.ObjectType == SQLObjectType.Function)
            {
                paramstr = GetSPParameters(objData.ObjectType, objData.DbName, objData.Schema, objData.Name);
            }
            Clipboard.SetText(objData.GetFullName(mnuObjDBPrefix.Checked, false) + paramstr);
        }

        private void mnuObjToQuery_Click(object sender, EventArgs e)
        {
            if (objectsTree.FocusedItem == null || objectsTree.FocusedItem.Tag == null) return;
            var objData = objectsTree.FocusedItem.Tag as SQLObjectData;
            var paramstr = "";
            if (objData.ObjectType == SQLObjectType.Procedure || objData.ObjectType == SQLObjectType.Function)
            {
                paramstr = GetSPParameters(objData.ObjectType, objData.DbName, objData.Schema, objData.Name);
            }
            string name = objData.GetFullName(mnuObjDBPrefix.Checked, true) + paramstr;
            if (String.IsNullOrEmpty(txtSQL.Text) || e == null)
            {
                ChangeTxtSQL((objData.ObjectType == SQLObjectType.Procedure ? "exec " : "select * from ") + name, e == null);
            }
            else
            {
                ChangeTxtSQL(name, e == null);
            }
        }

        private String GetSPParameters(SQLObjectType objType, String dbname, String schemaname, String spname)
        {
            var parameters = GetSPParameters(dbname, schemaname, spname).Where(p => p.Position > 0).Select(p => p.Name + "=").ToArray();
            if (parameters == null || parameters.Length == 0) return "";
            var paramstr = "\r\n    " + String.Join(",\r\n    ", parameters);
            if (objType == SQLObjectType.Function)
            {
                paramstr = "(" + paramstr + ")";
            }
            return paramstr;
        }
        private List<SPParameter> GetSPParameters(String dbname, String schemaname, String spname)
        {
            var result = new List<SPParameter>();
            var paramQuery = String.Format(@"exec sp_procedure_params_rowset @procedure_schema='{0}', @procedure_name='{1}'", schemaname, spname);
            using (SqlConnection conn = new SqlConnection(DataObjectView.GetConnectionString(ConnectionString, dbname)))
            {
                conn.Open();
                using (var cmd = new SqlCommand(paramQuery, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new SPParameter
                            {
                                Name = (string)GetData(reader, 3, ""),
                                TypeName = (string)GetData(reader, 15, ""),
                                Position = (Int16)GetData(reader, 4, 0)
                            });
                        }
                    }
                }
            }
            return result;
        }

        private void mnuObjAppendToQuery_Click(object sender, EventArgs e)
        {
            mnuObjToQuery_Click(sender, null);
        }

        private void ChangeTxtSQL(string txt, bool appending)
        {
            if (String.IsNullOrEmpty(txt)) return;
            if (String.IsNullOrEmpty(txtSQL.Text))
            {
                txtSQL.Text = txt;
                txtSQL.SelectionStart = 0;
            }
            else
            {
                var sb = new StringBuilder(txtSQL.Text);
                var p = txtSQL.SelectionStart;
                if (appending)
                {
                    p = sb.Length;
                    txt = "\r\n---\r\n" + txt;
                    sb.Append(txt);
                }
                else
                {
                    if (txtSQL.SelectionLength > 0 && p >= 0) sb.Remove(p, txtSQL.SelectionLength);
                    sb.Insert(p, txt);
                }
                txtSQL.Text = sb.ToString();
                txtSQL.SelectionStart = p;
            }
            txtSQL.SelectionLength = txt.Length;
            tabMain.SelectedTab = pageQuery;
            ActiveControl = txtSQL;
        }

        private void menuObjectTreeItem_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = objectsTree.FocusedItem == null;
            if (e.Cancel) return;
            if (objectsTree.FocusedItem.Tag == null)
            {
                mnuObjCollapseAll.Visible = mnuObjExpandAll.Visible = true;
                mnuObjDBPrefix.Visible = mnuObjToClipboard.Visible = mnuObjNoBraceToClipboard.Visible = mnuObjAppendToQuery.Visible = mnuObjToQuery.Visible = false;
            }
            else
            {
                mnuObjCollapseAll.Visible = mnuObjExpandAll.Visible = false;
                mnuObjDBPrefix.Visible = mnuObjToClipboard.Visible = mnuObjNoBraceToClipboard.Visible = mnuObjAppendToQuery.Visible = mnuObjToQuery.Visible = true;
                var objData = objectsTree.FocusedItem.Tag as SQLObjectData;
                mnuObjDBPrefix.Text = String.Format("    with DB-Prefix [{0}]", objData.DbName);
                string name = objData.GetFullName(mnuObjDBPrefix.Checked, true);
                mnuObjToClipboard.Text = name + " to Clipboard";
                mnuObjNoBraceToClipboard.Text = String.Format("{0} to Clipboard", objData.GetFullName(mnuObjDBPrefix.Checked, false));
                mnuObjToQuery.Text = name + " to QueryBox";
                mnuObjAppendToQuery.Text = "append " + name + " to QueryBox";
            }
        }

        private void txtSQL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                Query.PerformClick();
            }
        }

        private void menuObjScriptText_Opening(object sender, CancelEventArgs e)
        {
            var editor = menuObjScriptText.Tag as FastColoredTextBoxNS.FastColoredTextBox;
            if (editor == null || !dataViewCurrentObject.ScriptActive)
            {
                e.Cancel = true;
                return;
            }
            string txt = editor.SelectedText;
            if (!String.IsNullOrEmpty(txt))
            {
                mnuScriptTextSearch.Enabled = true;
                mnuScriptTextSearch.Tag = (txt ?? "").Trim();
                txt = txt.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("  ", " ").Trim();
                int cnt = txt.Length;
                if (cnt > 54) txt = txt.Substring(0, 25) + " .. " + txt.Substring(cnt - 25, 25);
                mnuScriptTextSearch.Text = String.Format("Search DB objects for \"{0}\"", txt);
                mnuScriptAppendToQueryBox.Text = String.Format("Append \"{0}\" to QueryBox", txt);
            }
            else
            {
                mnuScriptTextSearch.Enabled = false;
                mnuScriptTextSearch.Text = "Search DB objects";
                mnuScriptAppendToQueryBox.Text = "Append to QueryBox";
            }
            mnuScriptTextWordWrap.Checked = editor.WordWrap;
        }

        private void mnuScriptTextWordWrap_Click(object sender, EventArgs e)
        {
            var editor = menuObjScriptText.Tag as FastColoredTextBoxNS.FastColoredTextBox;
            editor.WordWrap = mnuScriptTextWordWrap.Checked;
            editor.ShowScrollBars = !editor.WordWrap;
        }

        private void mnuScriptTextSearch_Click(object sender, EventArgs e)
        {
            //var editor = menuObjScriptText.Tag as FastColoredTextBoxNS.FastColoredTextBox;
            if (mnuScriptTextSearch.Tag == null) return;
            txtObjFilter.Text = mnuScriptTextSearch.Tag.ToString();
            btnObjFilter.PerformClick();
        }

        private string GetInitialCatalog(string connString)
        {
            var csb = new SqlConnectionStringBuilder(connString);
            return csb.InitialCatalog;
        }

        private void FrmObjectCreator_Load(object sender, EventArgs e)
        {
        }

        private void FrmDBMapper_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\dbMapper", false);
            }
            catch { }
            if (key != null && (global::DBMapper.Properties.Settings.Default.UseConnectionRegistryFirst > 0
                                || string.IsNullOrEmpty(global::DBMapper.Properties.Settings.Default.PCPConnectionString))
               )
                try
                {
                    chkLoginMappingsOnly.Checked = key.GetValue("MappingsDbOnly", 0).ToString() == "1";
                    RefreshConnectionsList(key, "");
                    return;
                }
                catch { }
            ConnectionString = global::DBMapper.Properties.Settings.Default.PCPConnectionString;
            if (string.IsNullOrEmpty(connString))
            {
                btnADODB.PerformClick();
            }
        }


        private void mnuObjCollapseAll_Click(object sender, EventArgs e)
        {
            var item = objectsTree.FocusedItem;
            if (item != null)
            {
                collapseAll(item);
                item.Expand();
            }
        }

        private void collapseAll(TreeListViewItem item)
        {
            TreeListViewItem parent = item == null ? null : item.Parent;
            if (parent == null) return;
            foreach (TreeListViewItem pitem in parent.Items) if (pitem != item) pitem.CollapseAll();
            collapseAll(parent);
        }

        private void mnuObjExpandAll_Click(object sender, EventArgs e)
        {
            var item = objectsTree.FocusedItem;
            if (item != null) item.ExpandAll();
        }

        private void btnNewInstance_Click(object sender, EventArgs e)
        {
            var appPath = Assembly.GetExecutingAssembly().Location;
            var proc = new ProcessStartInfo(appPath);
            Process.Start(proc);
        }

        private void mnuObjFilterNoFilter_Click(object sender, EventArgs e)
        {
            RefreshObjects("", false);
        }

        private void mnuObjFilterNamesSearch_Click(object sender, EventArgs e)
        {
            RefreshObjects(txtObjFilter.Text, false);
        }

        private void mnuObjFilterFulltextSearch_Click(object sender, EventArgs e)
        {
            RefreshObjects(txtObjFilter.Text, true);
        }

        private void menuObjFilter_Opening(object sender, CancelEventArgs e)
        {
            mnuObjFilterFulltextSearch.Enabled = mnuObjFilterNamesSearch.Enabled = !String.IsNullOrEmpty(txtObjFilter.Text);
        }

        private void mnuScriptTextShowWindow2_Click(object sender, EventArgs e)
        {
            ShowScriptWindow2();
        }

        private void mnuScriptAppendToQueryBox_Click(object sender, EventArgs e)
        {
            var editor = menuObjScriptText.Tag as FastColoredTextBoxNS.FastColoredTextBox;
            string txt = editor == null ? null : editor.SelectedText;
            if (!String.IsNullOrEmpty(txt))
            {
                ChangeTxtSQL(txt, true);
            }
        }

        private void contextMenuQueryEditor_Opening(object sender, CancelEventArgs e)
        {
            contextMenuQueryEditor.Items.Clear();
            var csvFileName = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".csv");
            if (!File.Exists(csvFileName)) return;
            var csvData = CSVReader.ReadCSVFile(csvFileName, true);
            var dbname = cbDatabases.Text;
            foreach (DataRow row in csvData.Rows)
            {
                var ctxName = row[0].ToString().ReplaceEnvironmentMacros(CurrentServerName, dbname);
                if (String.IsNullOrEmpty(ctxName)) continue;
                if (ctxName == "-")
                {
                    var ctxSep = new ToolStripSeparator();
                    contextMenuQueryEditor.Items.Add(ctxSep);
                }
                else
                {
                    var ctxItem = new ToolStripMenuItem(ctxName)
                    {
                        Tag = row[1].ToString().ReplaceEnvironmentMacros(CurrentServerName, dbname).Replace("\\\r", "\r").Replace("\\\n", "\n").Replace("\\\t", "\t")
                    };
                    ctxItem.Click += ctxItemQueryEditor_Click;
                    contextMenuQueryEditor.Items.Add(ctxItem);
                }
            }
        }


        void ctxItemQueryEditor_Click(object sender, EventArgs e)
        {
            var ctxItem = sender as ToolStripMenuItem;
            if (ctxItem == null || ctxItem.Tag == null) return;
            ChangeTxtSQL(ctxItem.Tag.ToString(), false);
        }

        private void FrmDBMapper_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Q:
                        tabMain.SelectedTab = pageQuery;
                        ActiveControl = txtSQL;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.S:
                        tabMain.SelectedTab = pageDataSearch;
                        ActiveControl = cbDsValueContent;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.D:
                        tabMain.SelectedTab = pageObjects;
                        ActiveControl = txtObjFilter;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.N:
                        btnNewInstance_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.P:
                        tabMain.SelectedTab = pageProfiler;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        Application.DoEvents();
                        switchProfilingState();
                        break;
                }
            }
        }

        private void paintSplitter(object sender, PaintEventArgs e)
        {
            var splitter = sender as SplitContainer;
            ControlPaint.DrawGrabHandle(e.Graphics, splitter.SplitterRectangle, true, true);
        }

        private void FrmDBMapper_FormClosing(object sender, FormClosingEventArgs e)
        {
            unregisterHotkey();
            sqlProfiler.StopProfiling();
        }

        private void btnADODB_Click(object sender, EventArgs e)
        {
            var conn = new ADODB.Connection();
            var dialog = new MSDASC.DataLinks();
            var obj = (object)conn;
            if (dialog.PromptEdit(ref obj))
            {
                var items = conn.ConnectionString.Split(';');
                var connItems = new List<string>();
                foreach (var item in items)
                {
                    if (!item.StartsWith("Provider", StringComparison.OrdinalIgnoreCase)
                        && !item.StartsWith("Server SPN", StringComparison.OrdinalIgnoreCase)
                        )
                        connItems.Add(item);
                }
                var cstring = String.Join(";", connItems);
                ConnectionString = cstring;
                if (ConnectionString == cstring)
                {
                    RegistryKey key = null;
                    try
                    {
                        key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\dbMapper", true);
                    }
                    catch { }
                    if (key == null) key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\dbMapper");
                    using (SqlConnection nconn = new SqlConnection(cstring))
                    {
                        var serverName = $"{nconn.DataSource}";
                        var dbName = $"{nconn.Database}";
                        if (serverName == ".") serverName = "localhost";
                        if (!string.IsNullOrEmpty(dbName)) dbName = "." + dbName;
                        var connName = string.Format("Connection:{0}{1}", serverName, dbName);
                        key.SetValue(connName, EncodeBase64(cstring), RegistryValueKind.String);
                        key.SetValue("Connection", "@" + connName, RegistryValueKind.String);
                        RefreshConnectionsList(key, connName);
                    }
                }
            }
        }

        private void btnDsGo_Click(object sender, EventArgs e)
        {
            if (btnDsGo.Text != "Go")
            {
                StopSearchData();
            }
            else
            {
                btnDsGo.Text = "Stop";
                saveCbValue(cbDsValueSchema);
                saveCbValue(cbDsValueObject);
                saveCbValue(cbDsValueColName);
                saveCbValue(cbDsValueColType);
                saveCbValue(cbDsValueContent);
                Application.DoEvents();
                SearchData();
            }
        }

        private void saveCbValue(ComboBox cb)
        {
            if (!string.IsNullOrEmpty(cb.Text) && !cb.Items.Contains(cb.Text)) cb.Items.Insert(0, cb.Text);
        }

        private void treeDsResult_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            containerDataResult.Visible = node.Tag != null;
            if (node.Tag == null)
            {
                if (node.Nodes.Count > 0 && !node.IsExpanded) node.Expand();
                return;
            }
            if (node.Tag is DataSearchObject)
                showSearchResult(node.Tag as DataSearchObject, null);
            else
                showSearchResult(node.Parent.Tag as DataSearchObject, node.Tag as DataSearchColumn);
        }

        DataSet dsResultData;
        List<int> dsResultHiliteColumns = new List<int>();
        private void showSearchResult(DataSearchObject searchObject, DataSearchColumn searchColumn)
        {
            dsResultHiliteColumns.Clear();
            dataGridViewDsResult.AutoGenerateColumns = true;
            dataGridViewDsResult.DisableGridViewError();
            var columnsWhere = new List<DataSearchColumn>();
            if (searchColumn != null)
            {
                dsResultHiliteColumns.Add(searchColumn.Index);
                columnsWhere.Add(searchColumn);
            }
            else
            {
                foreach (var column in searchObject.FoundColumns)
                {
                    dsResultHiliteColumns.Add(column.Index);
                    columnsWhere.Add(column);
                }
            }
            var alias = searchObject.Schema.Substring(0, 1) + searchObject.Name.Substring(0, 1);
            var where = columnsWhere.Select(c => searchObject.Parent.BuildContentQuery(c, alias));
            if (where.Count() > 0)
            {
                var querystring = string.Format(@"select <<TOP>>
    {2}.*
from [{3}].{0} {2}
where
    {1}",
                    searchObject,
                    string.Join("\r\n    OR ", where),
                    alias, searchObject.Parent.DbName);
                dataGridViewDsResult.Tag = querystring;
                bindingNavigatorDsResult.Tag = searchObject;
                lblDsResultTable.Text = (searchObject.IsView ? "View: " : "Table: ") + searchObject.Parent.DbName + "." + searchObject;
                bindSearchResultQuery();
            }
        }

        private void bindSearchResultQuery()
        {
            SetTxtDsResultSQLVisible(false);
            bindingSourceDsResult.DataSource = null;
            bindingSourceDsResult.Filter = "";
            if (dsResultData != null) dsResultData.Dispose();
            dsResultData = new DataSet();
            string querystring = getQueryFromGridSearchResult();
            var searcher = bindingNavigatorDsResult.Tag as DataSearchObject;
            using (var conn = new SqlConnection(searcher.ConnectionString))
            {
                conn.Open();
                searcher.FillAllColumns(conn);
                using (var adapter = new SqlDataAdapter(querystring, conn))
                {
                    try
                    {
                        adapter.Fill(dsResultData, "Data");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("{0}:\r\n\r\n{1}", ex.Message, querystring));
                        return;
                    }
                    var table = dsResultData.Tables[0];
                    bindingSourceDsResult.DataSource = table;
                    listDsScriptWhere.Items.Clear();
                    foreach (var column in searcher.AllColumns)
                    {
                        var item = listDsScriptWhere.Items.Add(column.Name);
                        item.SubItems.Add(column.FullTypeName);
                        item.Tag = column;
                    }
                    Application.DoEvents();
                    var showImage = btnShowImage.Checked;
                    foreach (DataColumn column in dsResultData.Tables[0].Columns)
                    {
                        if (!showImage && column.DataType.Name.ToLower() == "byte[]")
                        {
                            dataGridViewDsResult.Columns[column.Ordinal].Visible = false;
                        }
                    }
                }
            }
        }

        private string getQueryFromGridSearchResult()
        {
            var tease = cbDsResultTop.Text.ToInt();
            var querystring = dataGridViewDsResult.Tag == null ? "" : dataGridViewDsResult.Tag.ToString();
            return querystring.Replace("<<TOP>>", tease > 0 ? "TOP " + tease : "");
        }

        private void dataGridViewDsResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataObjectView.GridCellFormatting(bindingSourceDsResult, e);
        }

        private void dataGridViewDsResult_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (dsResultHiliteColumns.Contains(e.ColumnIndex + 1))
            {
                e.CellStyle.BackColor = SystemColors.Info;
                e.CellStyle.ForeColor = SystemColors.InfoText;
                var searcher = bindingNavigatorDsResult.Tag as DataSearchObject;
                var content = searcher.Parent.Options.CompareContent.Value.ToLower();
                var value = e.FormattedValue == null ? "" : e.FormattedValue.ToString().ToLower();
                //TODO: check for compare options LIKE, IN, BETWEEN ...
                if (!string.IsNullOrEmpty(content) && e.Value != null && !e.Value.Equals(DBNull.Value) &&
                    (value.Contains(content) || content.Contains(value))
                    )
                {
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
        }

        private void btnShowImage_CheckedChanged(object sender, EventArgs e)
        {
            var showImage = btnShowImage.Checked;
            foreach (DataColumn column in dsResultData.Tables[0].Columns)
            {
                if (column.DataType.Name.ToLower() == "byte[]")
                {
                    dataGridViewDsResult.Columns[column.Ordinal].Visible = showImage;
                }
            }
        }

        private void cbDsContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSearchTooltip.Visible = cbDsContent.Text == "LIKES" || "IN;BETWEEN".IndexOf(cbDsContent.Text) >= 0;
        }

        private void btnDsResultFilter_Click(object sender, EventArgs e)
        {
            try
            {
                var filter = txtDsResultFilter.Text;
                bindingSourceDsResult.Filter = filter;
                if (txtDsResultFilter.Items.IndexOf(filter) < 0) txtDsResultFilter.Items.Insert(0, filter);
                SetTxtDsResultSQLVisible(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, String.Format("FILTER ERROR {0}:\r\n{1}", txtDsResultFilter.Text, ex.Message), "FILTER ERROR");
                bindingSourceDsResult.Filter = "";
            }
        }

        private void SetTxtDsResultSQLVisible(bool visible)
        {
            txtDsResultGenerateSQL.Checked = visible;
            txtDsResultSQL.Visible = visible;
            containerDsResult.Visible = !visible;
        }

        private void txtDsResultGenerateSQL_Click(object sender, EventArgs e)
        {
            if (dataGridViewDsResult.Tag != null)
            {
                var sql = getQueryFromGridSearchResult().Split(new string[] { "\r\n" }, 3, StringSplitOptions.None);
                var prefix = sql[1].Substring(0, sql[1].Length - 1) + "[";
                var suffix = "]";
                var table = (DataTable)bindingSourceDsResult.DataSource;
                var columns = table.Columns.OfType<DataColumn>().Select(c => prefix + c.ColumnName + suffix).ToList();
                sql[1] = string.Join(",\r\n", columns);
                txtDsResultSQL.Text = string.Join("\r\n", sql);
                SetTxtDsResultSQLVisible(txtDsResultGenerateSQL.Checked);
            }
        }

        private void cbDsResultTop_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                e.SuppressKeyPress = true;
                if (!cbDsResultTop.Items.Contains(cbDsResultTop.Text)) cbDsResultTop.Items.Add(cbDsResultTop.Text);
                bindSearchResultQuery();
            }
        }

        private void listDsScriptWhere_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var sql = @"declare @parameters table
(
     {0}
);
insert into @parameters values
     {1};";
            var columns = listDsScriptWhere.CheckedItems.OfType<ListViewItem>().Select(i => i.Tag as DataSearchColumn).ToList();
            if (columns.Count == 0 || bindingSourceDsResult.Count == 0)
            {
                txtDsScriptWhere.Text = "";
                return;
            }
            var columnsheader = string.Join("\r\n    ,", columns.OrderBy(c => c.Index).Select(c => c.Name + " \t" + c.FullTypeName).ToArray());
            var colValues = new string[columns.Count];
            var rowValues = new List<string>();
            var table = bindingSourceDsResult.DataSource as DataTable;
            for (var row = 0; row < table.Rows.Count; row++)
            {
                if (row > 0 && row % 1000 == 0)
                {
                    rowValues.Add(@"insert into @parameters values");
                }
                var datarow = table.Rows[row];
                for (var col = 0; col < columns.Count; col++)
                {
                    var column = columns[col];
                    var value = datarow[column.Index - 1];
                    if (value == null || DBNull.Value.Equals(value))
                    {
                        colValues[col] = "NULL";
                    }
                    else if (value.GetType() == typeof(DateTime) || value.GetType() == typeof(DateTime?))
                    {
                        colValues[col] = string.Format("'{0:s}.{1:000}'", value, ((DateTime)value).Millisecond);
                    }
                    else if (column.TypeName == "bit")
                    {
                        colValues[col] = (bool)value ? "1" : "0";
                    }
                    else if (DataSearchColumn.DataTypes_Numbers.IndexOf(column.TypeName) >= 0)
                    {
                        colValues[col] = string.Format(CultureInfo.InvariantCulture.NumberFormat, "{0}", value); //.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                    }
                    else
                    {
                        var sb = new StringBuilder();
                        var delim = column.HasDelimeter ? "'" : "";
                        if (column.HasDelimeter)
                        {
                            foreach (char c in value.ToString())
                                if (c < 32) sb.Append("' + CHAR(").Append((int)c).Append(") + '"); else if (c == '\'') sb.Append("''"); else sb.Append(c);
                        }
                        else
                        {
                            sb.Append(value.ToString().Replace("'", "''"));
                        }
                        colValues[col] = string.Format("{0}{1}{0}", delim, sb.ToString());
                    }
                }
                rowValues.Add((row % 1000 == 0 ? "     " : "    ,") + "(" + string.Join(", ", colValues) + ")" + ((row + 1) % 1000 == 0 ? ";" : ""));
            }
            txtDsScriptWhere.Text = string.Format(sql, columnsheader, string.Join("\r\n", rowValues));
        }

        private void cbDsValueContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13 && btnDsGo.Enabled && btnDsGo.Text == "Go") btnDsGo.PerformClick();
        }

        private void mnuTreeDsResult2Excel_Click(object sender, EventArgs e)
        {
            using (var excel = new ExcelExport())
            {
                var sheet = excel.Book.ActiveSheet;
                var row = 2;
                excel.SetCellValue(sheet, 1, 1, "DataSearch " + CurrentServerName);
                excel.SetCellValue(sheet, 2, 1, "Db");
                excel.SetCellValue(sheet, 2, 2, "Schema");
                excel.SetCellValue(sheet, 2, 3, "Type");
                excel.SetCellValue(sheet, 2, 4, "Object");
                excel.SetCellValue(sheet, 2, 5, "Column");
                excel.SetCellValue(sheet, 2, 6, "Found");
                foreach (var search in dataSearchers.Where(s => s.AnyFound))
                {
                    foreach (var column in search.Columns.Where(c => c.RowsCount > 0))
                    {
                        row++;
                        excel.SetCellValue(sheet, row, 1, search.Parent.DbName);
                        excel.SetCellValue(sheet, row, 2, search.Schema);
                        excel.SetCellValue(sheet, row, 3, search.IsView ? "V" : "U");
                        excel.SetCellValue(sheet, row, 4, search.Name);
                        excel.SetCellValue(sheet, row, 5, column.Name);
                        excel.SetCellValue(sheet, row, 6, column.RowsCount);
                    }
                }
            }
        }

        private void btnDsGridExport_Click(object sender, EventArgs e)
        {
            using (var excel = new ExcelExport())
            {
                var sheet = excel.Book.ActiveSheet;
                var ds = bindingNavigatorDsResult.Tag as DataSearchObject;
                exportSearchResultToExcel(excel, sheet, ds);
            }
        }

        private void exportSearchResultToExcel(ExcelExport excel, dynamic sheet, DataSearchObject ds)
        {
            excel.SetCellValue(sheet, 1, 1, "DataSearch " + CurrentServerName + ": " + ds.ToString());
            var c = 0;
            foreach (DataGridViewColumn column in dataGridViewDsResult.Columns)
            {
                excel.SetCellValue(sheet, 2, ++c, column.HeaderText);
            }
            for (var rowIndex = 0; rowIndex < dataGridViewDsResult.Rows.Count; rowIndex++)
            {
                var dataRow = dataGridViewDsResult.Rows[rowIndex];
                for (c = 0; c < dataGridViewDsResult.Columns.Count; c++)
                {
                    if (dataGridViewDsResult.Columns[c].Visible)
                    {
                        var format = dataRow.Cells[c].ValueType.Name.ToLower();
                        format = format.IndexOf("guid") >= 0 || format.IndexOf("string") >= 0 ? "@" : "";
                        excel.SetCellValue(sheet, rowIndex + 3, c + 1, dataRow.Cells[c].FormattedValue, format);
                    }
                }
                if (rowIndex % 10 == 0) Application.DoEvents();
            }
        }

        private void mnuTreeDsResultCopySelectedBranch_Click(object sender, EventArgs e)
        {
            var node = treeDsResult.SelectedNode;
            if (node == null) return;
            var txt = new List<string>();
            Action<TreeNode> nodeAction = null;
            nodeAction = (nd) =>
            {
                if (nd.Tag != null && nd.Tag is DataSearchColumn)
                {
                    txt.Add("[" + nd.Parent.FullPath.Replace("\\", "].[") + "].[" + ((DataSearchColumn)nd.Tag).Name + "]");
                }
                else foreach (TreeNode cnode in nd.Nodes) nodeAction(cnode);
            };
            nodeAction(node);
            Clipboard.SetText(string.Join("\r\n", txt));
        }

        private void mnuTreeDsResultCopySelectedTables_Click(object sender, EventArgs e)
        {
            var node = treeDsResult.SelectedNode;
            if (node == null) return;
            var txt = new List<string>();
            Action<TreeNode> nodeAction = null;
            nodeAction = (nd) =>
            {
                if (nd.Tag != null && nd.Tag is DataSearchColumn)
                {
                    var t = "[" + nd.Parent.FullPath.Replace("\\", "].[") + "]";
                    if (!txt.Contains(t)) txt.Add(t);
                }
                else foreach (TreeNode cnode in nd.Nodes) nodeAction(cnode);
            };
            nodeAction(node);
            Clipboard.SetText(string.Join("\r\n", txt));
        }

        private void cbDsValueContent_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && "BETWEEN;IN".IndexOf(cbDsContent.Text) >= 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.C:
                        Clipboard.SetText(cbDsValueContent.SelectedText.Replace(";", "\r\n"));
                        e.Handled = true;
                        break;
                    case Keys.V:
                        cbDsValueContent.Text = Clipboard.GetText().Replace("\r\n", ";").Replace("\r", ";").Replace("\n", ";");
                        cbDsValueContent.SelectAll();
                        e.Handled = true;
                        break;
                }
            }
        }

        bool ignoreListDsDb_SelectedValueChanged;
        private void listDsDb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ignoreListDsDb_SelectedValueChanged) return;
            try
            {
                listDsDb.Enabled = false;
                Cursor = Cursors.WaitCursor;
                clearCbItems(cbDsValueSchema);
                clearCbItems(cbDsValueObject);
                clearCbItems(cbDsValueColName);
                if (listDsDb.SelectedItems.Count == 0) return;
                var schemas = new List<string>();
                var objects = new List<string>();
                var columns = new List<string>();
                var sqlTemplate = @"select s.name as schemaname, o.name as objectname, c.name as columnname 
    from {0}.sys.columns c 
    inner join {0}.sys.objects o on o.object_id = c.object_id 
    inner join {0}.sys.schemas s on s.schema_id=o.schema_id 
    where o.type in ('U', 'V') and s.name <> 'sys'";
                var sql = new List<string>();
                foreach (var dbname in listDsDb.SelectedItems.Cast<string>()) sql.Add(string.Format(sqlTemplate, dbname));
                sqlTemplate = string.Join("\r\nUNION\r\n", sql);
                using (SqlConnection conn = new SqlConnection(DataObjectView.GetConnectionString(ConnectionString)))
                {
                    conn.Open();
                    using (var dataset = new DataSet())
                    {
                        using (var adapter = new SqlDataAdapter(sqlTemplate, conn))
                        {
                            adapter.Fill(dataset);
                        }
                        Application.DoEvents();
                        foreach (DataRow row in dataset.Tables[0].Rows)
                        {
                            if (!schemas.Contains(row["schemaname"].ToString())) schemas.Add(row["schemaname"].ToString());
                            if (!objects.Contains(row["objectname"].ToString())) objects.Add(row["objectname"].ToString());
                            if (!columns.Contains(row["columnname"].ToString())) columns.Add(row["columnname"].ToString());
                        }
                    }
                }
                Application.DoEvents();
                schemas.Sort();
                objects.Sort();
                columns.Sort();
                cbDsValueSchema.Items.AddRange(schemas.ToArray());
                cbDsValueObject.Items.AddRange(objects.ToArray());
                cbDsValueColName.Items.AddRange(columns.ToArray());
            }
            catch//(Exception ex)
            {
                //
            }
            finally
            {
                listDsDb.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        private void clearCbItems(ComboBox box)
        {
            var txt = box.Text;
            box.Items.Clear();
            box.Text = txt;
        }

        private void mnuTreeDsResultData2Excel_Click(object sender, EventArgs e)
        {
            var node = treeDsResult.SelectedNode;
            if (node == null) return;
            var processedObjects = new List<string>();

            using (var excel = new ExcelExport())
            {
                dynamic sheet = null;
                Action<TreeNode> nodeAction = null;
                var longNameCount = 0;
                nodeAction = (nd) =>
                {
                    if (nd.Tag != null && nd.Tag is DataSearchColumn)
                    {
                        var objectName = string.Format("{0}.{1}", nd.Parent.Parent.Text, nd.Parent.Text);
                        if (objectName.Length > 31)
                        {
                            var x = string.Format("_{0}", longNameCount);
                            objectName = objectName.Substring(0, 31 - x.Length) + x;
                        }
                        if (!processedObjects.Contains(objectName))
                        {
                            processedObjects.Add(objectName);
                            var ds = nd.Parent.Tag as DataSearchObject;
                            showSearchResult(ds, null);
                            sheet = sheet == null ? excel.Book.ActiveSheet : excel.Book.Worksheets.Add(After: sheet);
                            sheet.Name = objectName;
                            exportSearchResultToExcel(excel, sheet, ds);
                        }
                    }
                    else
                        foreach (TreeNode cnode in nd.Nodes) nodeAction(cnode);
                };
                nodeAction(node);
            }
        }

        private void panelDsGo_Resize(object sender, EventArgs e)
        {
            btnDsGo.Width = panelDsGo.ClientSize.Width - btnDsOpen.Width - btnDsSave.Width - 6 * 3;
        }

        private void btnDsOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var json = File.ReadAllText(openFileDialog1.FileName);
                    var js = new JavaScriptSerializer();
                    var options = js.Deserialize<DataSearchOptions>(json);
                    setSearchOptionsToUI(options);
                    MessageBox.Show(string.Format("The search options are loaded from {0}.", openFileDialog1.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("ERROR on loading search options from {0}:\r\n{1}", openFileDialog1.FileName, ex.Message));
                }
            }
        }

        private void btnDsSave_Click(object sender, EventArgs e)
        {
            var options = getSearchOptionsFromUI();
            var js = new JavaScriptSerializer();
            var json = FormatJsonText(js.Serialize(options));
            try
            {
                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, json, Encoding.UTF8);
                    MessageBox.Show(string.Format("The search options are saved in {0}.", saveFileDialog1.FileName));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("ERROR on saving search options to {0}:\r\n{1}", saveFileDialog1.FileName, ex.Message));
            }
        }

        public static string FormatJsonText(string inputText)
        {
            bool escaped = false;
            bool inquotes = false;
            int column = 0;
            int indentation = 0;
            Stack<int> indentations = new Stack<int>();
            int TABBING = 4;
            StringBuilder sb = new StringBuilder();
            foreach (char x in inputText)
            {
                sb.Append(x);
                column++;
                if (escaped)
                {
                    escaped = false;
                }
                else
                {
                    if (x == '\\')
                    {
                        escaped = true;
                    }
                    else if (x == '\"')
                    {
                        inquotes = !inquotes;
                    }
                    else if (!inquotes)
                    {
                        if (x == ',')
                        {
                            // if we see a comma, go to next line, and indent to the same depth
                            sb.Append("\r\n");
                            column = 0;
                            for (int i = 0; i < indentation; i++)
                            {
                                sb.Append(" ");
                                column++;
                            }
                        }
                        else if (x == '[' || x == '{')
                        {
                            // if we open a bracket or brace, indent further (push on stack)
                            indentations.Push(indentation);
                            indentation = column;
                        }
                        else if (x == ']' || x == '}')
                        {
                            // if we close a bracket or brace, undo one level of indent (pop)
                            indentation = indentations.Pop();
                        }
                        else if (x == ':')
                        {
                            // if we see a colon, add spaces until we get to the next
                            // tab stop, but without using tab characters!
                            while ((column % TABBING) != 0)
                            {
                                sb.Append(' ');
                                column++;
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }


        private string EncodeBase64(string text)
        {
            byte[] textAsBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textAsBytes);
        }
        private string DecodeBase64(string text)
        {
            try
            {
                byte[] textAsBytes = System.Convert.FromBase64String(text);
                return System.Text.Encoding.UTF8.GetString(textAsBytes);
            }
            catch
            {
                return String.Empty;
            }
        }

        private void chkLoginMappingsOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(connString)) return;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    FillDatabases(conn, "");
                    RegistryKey key = null;
                    try
                    {
                        key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\dbMapper", true);
                    }
                    catch { }
                    if (key == null) key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\dbMapper");
                    key.SetValue("MappingsDbOnly", chkLoginMappingsOnly.Checked ? 1 : 0, RegistryValueKind.DWord);
                }
                catch
                {
                }
            }
        }

        private void RefreshConnectionsList(RegistryKey key, string connName)
        {
            ignoreConnectionsListEvent = true;
            try
            {
                if (string.IsNullOrEmpty(connName)) connName = "Connection";
                do
                {
                    var cstring = $"{key.GetValue(connName)}";
                    if (!string.IsNullOrEmpty(cstring))
                    {
                        if (cstring.StartsWith("@Connection:"))
                        {
                            connName = cstring.Substring(1);
                        }
                        else
                        {
                            ConnectionString = DecodeBase64(cstring);
                            break;
                        }
                    }
                    else
                        break;
                } while (!string.IsNullOrEmpty(connName));
                cbConnections.Items.Clear();
                var currIndex = -1;
                foreach (var name in key.GetValueNames())
                {
                    var p = name.IndexOf(":");
                    if (p > 0)
                    {
                        var value = $"{key.GetValue(name)}";
                        if (!value.StartsWith("@")) value = DecodeBase64(value);
                        if (name == connName) currIndex = cbConnections.Items.Count;
                        cbConnections.Items.Add(new ConnectionItem
                        {
                            Name = name.Substring(p + 1),
                            Value = value
                        });
                    }
                }
                if (currIndex >= 0) cbConnections.SelectedIndex = currIndex;
            }
            finally
            {
                ignoreConnectionsListEvent = false;
            }
        }
        private class ConnectionItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public override string ToString() => Name;
        }

        bool ignoreConnectionsListEvent;
        private void cbConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ignoreConnectionsListEvent) return;
            var connItem = cbConnections.SelectedItem as ConnectionItem;
            if (connItem != null) ConnectionString = connItem.Value;
        }

        private void dataGridViewDsResult_SelectionChanged(object sender, EventArgs e)
        {
            var cell = dataGridViewDsResult.SelectedCells.OfType<DataGridViewCell>().FirstOrDefault();
            if (cell == null)
            {
                txtDataField.Text = "";
                return;
            }
            txtDataField.Text = txtDataField.Language == Language.XML ? DataObjectView.FormatXml($"{cell.Value}") : btnFormattedText.Checked ? cell.FormattedValue.ToString() : $"{cell.Value}";
        }
        private void cbTextType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataField.Language = DataObjectView.GetLanguageFromIndex(cbTextType.Text);
            dataGridViewDsResult_SelectionChanged(null, null);
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            txtDataField.WordWrap = btnWordWrap.Checked;
        }
    }

    public enum SQLObjectType
    {
        Table,
        View,
        Procedure,
        Function,
        Synonym
    }
    public class SQLObjectData
    {
        public SQLObjectType ObjectType { get; set; }
        public string DbName { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Script { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string GetFullName(bool withDbPrefix, bool withBraces)
        {
            string fmt = withBraces ? "[{1}].[{2}]" : "{1}.{2}";
            if (withDbPrefix)
            {
                fmt = (withBraces ? "[{0}]." : "{0}.") + fmt;
            }
            return String.Format(fmt, DbName, Schema, Name);
        }
        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", DbName, Schema, Name);
        }
    }

    public class SPParameter
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public int Position { get; set; }
        //TODO: default?
        public bool HasDefault { get; set; }
        public object DefaultValue { get; set; }
    }
}
