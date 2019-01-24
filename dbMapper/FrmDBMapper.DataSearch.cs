using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMapper
{
    partial class FrmDBMapper
    {
        private volatile int runningCount;
        private CancellationTokenSource cancelToken;
        private List<DataSearchObject> dataSearchers;
        public void SearchData()
        {
            containerDataResult.Visible = false;
            buildDataSearchers();
            Application.DoEvents();
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            cancelToken = new CancellationTokenSource();
            runningCount = dataSearchers.Count;
            lblRunningCount.Text = runningCount.ToString();
            progressDsRunning.Value = 0;
            progressDsRunning.Visible = true;
            if (runningCount > 0) progressDsRunning.Maximum = runningCount;
            Application.DoEvents();
            foreach (var search in dataSearchers)
            {
                var task = Task.Factory.StartNew((obj) =>
                {
                    var searcher = (DataSearchObject)obj;
                    searcher.Search();
                }, search, cancelToken.Token);

                task.ContinueWith(tsk =>
                {
                    runningCount--;
                    if (progressDsRunning.Value < progressDsRunning.Maximum)
                    {
                        progressDsRunning.BeginInvoke((Action)(() =>
                        {
                            progressDsRunning.PerformStep();
                        }));
                    }
                    var searcher = (DataSearchObject)tsk.AsyncState;
                    var node = searcher.UIObject as TreeNode;
                    if (node.ImageIndex < 0) return;
                    if (!searcher.AnyFound && !cancelToken.IsCancellationRequested)
                    {
                        treeDsResult.BeginInvoke((Action)(() =>
                        {
                            try
                            {
                                var p = node.Parent;
                                node.Remove();
                                while (p != null && p.Nodes.Count == 0)
                                {
                                    node = p.Parent;
                                    p.Remove();
                                    p = node;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }));
                    }
                    else
                    {
                        foreach (var column in searcher.FoundColumns)
                        {
                            if (node.ImageIndex < 0 || cancelToken.IsCancellationRequested) break;
                            treeDsResult.BeginInvoke((Action)(() =>
                            {
                                try
                                {
                                    var colNode = node.Nodes.Add(string.Format("{0} ({1})", column.Name, column.RowsCount));
                                    colNode.SelectedImageIndex = colNode.ImageIndex = 4;
                                    colNode.Tag = column;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }));
                        }
                    }
                    this.BeginInvoke((Action)(() =>
                    {
                        lblRunningCount.Text = runningCount.ToString();
                        var item = string.Format("{0}:{1}:{2}", searcher.Parent.DbName, searcher.IsView?"V":"U", searcher.ToString());
                        if (listRunningTasks.Items.Contains(item)) listRunningTasks.Items.Remove(item);
                        if (listRunningTasks.Items.Count == 0) runningCount = 0;
                        if (runningCount <= 0)
                        {
                            cancelToken = null;
                            btnDsGo.Text = "Go";
                            btnDsGo.Enabled = true;
                            progressDsRunning.Value = 0;
                            progressDsRunning.Visible = false;
                            lblRunningCount.Text = "";
                            listRunningTasks.Visible = false;
                        }
                    }));
                }, CancellationToken.None, TaskContinuationOptions.LongRunning | TaskContinuationOptions.OnlyOnRanToCompletion, ui);
                task.ContinueWith(tsk =>
                {
                    runningCount = 0;
                    this.BeginInvoke((Action)(() =>
                    {
                        progressDsRunning.Value = progressDsRunning.Maximum;
                        foreach (var searcher in dataSearchers)
                        {
                            if (progressDsRunning.Value > 0) progressDsRunning.Value--;
                            lblRunningCount.Text = progressDsRunning.Value.ToString();
                            if (!searcher.AnyFound && searcher.UIObject != null)
                            {
                                searcher.Cancelling = true;
                                try
                                {
                                    var node = searcher.UIObject as TreeNode;
                                    var p = node.Parent;
                                    node.ImageIndex = -1;
                                    node.Remove();
                                    while (p != null && p.Nodes.Count == 0)
                                    {
                                        node = p.Parent;
                                        //p.ImageIndex = -1;
                                        p.Remove();
                                        p = node;
                                    }
                                    //if (p != null && p.Nodes.Count == 0)
                                    //{
                                    //    p.ImageIndex = -1;
                                    //    p.Remove();
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            var item = searcher.Parent.DbName + "::" + searcher.ToString();
                            if (listRunningTasks.Items.Contains(item)) listRunningTasks.Items.Remove(item);
                        }
                        lblRunningCount.Text = "";
                        cancelToken = null;
                        btnDsGo.Text = "Go";
                        btnDsGo.Enabled = true;
                        progressDsRunning.Value = 0;
                        progressDsRunning.Visible = false;
                        listRunningTasks.Visible = false;
                    }));
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, ui);
            }
        }
        public void StopSearchData()
        {
            if (cancelToken != null && !cancelToken.IsCancellationRequested)
            {
                btnDsGo.Text = "Stopping..";
                btnDsGo.Enabled = false;
                cancelToken.Cancel();
            }
        }

        void buildDataSearchers()
        {
            DataSearchOptions options = getSearchOptionsFromUI();
            dataSearchers = new List<DataSearchObject>();
            listRunningTasks.Items.Clear();
            treeDsResult.Nodes.Clear();
            foreach (string item in options.Databases)
            {
                var dbNode = treeDsResult.Nodes.Add(item);
                dbNode.SelectedImageIndex = dbNode.ImageIndex = 0;
                string lastSchema = null;
                TreeNode schemaNode = null;
                var search = new DataSearch
                {
                    DbName = item,
                    Options = options
                };
                search.BuildObjects(DataObjectView.GetConnectionString(ConnectionString, item),
                    (obj) =>
                    {
                        Application.DoEvents();
                        if (schemaNode == null || lastSchema != obj.Schema)
                        {
                            schemaNode = dbNode.Nodes.Add(obj.Schema);
                            schemaNode.SelectedImageIndex = schemaNode.ImageIndex = 1;
                            lastSchema = obj.Schema;
                        }
                        var onode = schemaNode.Nodes.Add(obj.Name);
                        obj.UIObject = onode;
                        onode.Tag = obj;
                        onode.SelectedImageIndex = onode.ImageIndex = obj.IsView ? 3 : 2;
                        listRunningTasks.Items.Add(string.Format("{0}:{1}:{2}", item, obj.IsView ? "V" : "U", obj));

                        return true;
                    });
                if (search.Objects.Count > 0)
                {
                    dataSearchers.AddRange(search.Objects);
                    dbNode.Expand();
                }
                else
                {
                    dbNode.Remove();
                }
            }
            listRunningTasks.Visible = true;
        }

        public DataSearchOptions getSearchOptionsFromUI()
        {
            var delimetertype = string.IsNullOrEmpty(cbDsColType.Text) ||
                DataSearchColumn.DataTypes_String.IndexOf(cbDsColType.Text.ToLower()) >= 0 || DataSearchColumn.DataTypes_Datetime.IndexOf(cbDsColType.Text.ToLower()) >= 0;
            
            var options = new DataSearchOptions
            {
                Databases = listDsDb.SelectedItems.Cast<string>().ToList(),
                SearchInTables = chkDsTable.Checked,
                SearchInViews = chkDsViews.Checked,
                CompareSchema = new CompareValue
                {
                    Name = "Schema",
                    HasDelimeter = true,
                    NOT = chkDsNOTSchema.Checked,
                    Compare = (CompareType)cbDsSchema.SelectedIndex,
                    Value = cbDsValueSchema.Text
                },
                CompareObject = new CompareValue
                {
                    Name = "Object",
                    HasDelimeter = true,
                    NOT = chkDsNOTObject.Checked,
                    Compare = (CompareType)cbDsObject.SelectedIndex,
                    Value = cbDsValueObject.Text
                },
                CompareColName = new CompareValue
                {
                    Name = "ColName",
                    HasDelimeter = true,
                    NOT = chkDsNOTColName.Checked,
                    Compare = (CompareType)cbDsColName.SelectedIndex,
                    Value = cbDsValueColName.Text
                },
                CompareColType = new CompareValue
                {
                    Name = "ColType",
                    HasDelimeter = true,
                    NOT = chkDsNOTColType.Checked,
                    Compare = (CompareType)cbDsColType.SelectedIndex,
                    Value = cbDsValueColType.Text
                },
                CompareContent = new CompareValue
                {
                    Name = "Content",
                    HasDelimeter = delimetertype,
                    NOT = chkDsNOTContent.Checked,
                    Compare = (CompareType)cbDsContent.SelectedIndex,
                    Value = cbDsValueContent.Text
                }
            };
            return options;
        }

        public void setSearchOptionsToUI(DataSearchOptions options)
        {
            ignoreListDsDb_SelectedValueChanged = true;
            try
            {
                listDsDb.SelectedItems.Clear();
                for (var i = 0; i < listDsDb.Items.Count; i++)
                {
                    if (options.Databases.Contains(listDsDb.Items[i].ToString(), StringComparer.OrdinalIgnoreCase))
                        listDsDb.SetSelected(i, true);
                }
            }finally
            {
                ignoreListDsDb_SelectedValueChanged = false;
                listDsDb_SelectedValueChanged(null, null);
            }
            chkDsTable.Checked = options.SearchInTables;
            chkDsViews.Checked = options.SearchInViews;
            chkDsNOTSchema.Checked = options.CompareSchema.NOT;
            cbDsSchema.SelectedIndex = (int)options.CompareSchema.Compare;
            cbDsValueSchema.Text = options.CompareSchema.Value;
            chkDsNOTObject.Checked = options.CompareObject.NOT;
            cbDsObject.SelectedIndex = (int)options.CompareObject.Compare;
            cbDsValueObject.Text = options.CompareObject.Value;
            chkDsNOTColName.Checked = options.CompareColName.NOT;
            cbDsColName.SelectedIndex = (int)options.CompareColName.Compare;
            cbDsValueColName.Text = options.CompareColName.Value;
            chkDsNOTColType.Checked = options.CompareColType.NOT;
            cbDsColType.SelectedIndex = (int)options.CompareColType.Compare;
            cbDsValueColType.Text = options.CompareColType.Value;
            chkDsNOTContent.Checked = options.CompareContent.NOT;
            cbDsContent.SelectedIndex = (int)options.CompareContent.Compare;
            cbDsValueContent.Text = options.CompareContent.Value;
        }
    }
}
