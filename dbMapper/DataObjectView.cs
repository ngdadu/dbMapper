using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FastColoredTextBoxNS;
using System.Xml;
using System.IO;
using System.Globalization;

namespace DBMapper
{
    public partial class DataObjectView : UserControl
    {
        private List<DataTable> dataTables = new List<DataTable>();
        private int tableIndex;

        [Browsable(false)]
        public int TableIndex
        {
            get { return tableIndex; }
            set
            {
                if (value < 0) value = 0;
                if (value >= dataTables.Count) value = dataTables.Count - 1;
                if (tableIndex == value) return;
                tableIndex = value;
                ChangeDataSource(tableIndex < 0 ? null: dataTables[tableIndex]);
            }
        }

        private void ChangeDataSource(DataTable table)
        {
            fullTextFoundColumns.Clear();
            cboDataColumnFrozen.Items.Clear();
            if (table == null) return;
            sourceData.DataSource = table;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                cboDataColumnFrozen.Items.Add(table.Columns[i].ColumnName);
                if (!String.IsNullOrEmpty(fulltextSearch) && table.Columns[i].ColumnName.ToUpper().Contains(fulltextSearch.ToUpper()))
                    fullTextFoundColumns.Add(i);
            }
            sourceData.ResetBindings(true);
        }

        [Browsable(false)]
        [DefaultValue("Data")]
        public string DataCaption
        {
            get
            {
                return lblDataCaption.Text;
            }
            set
            {
                lblDataCaption.Text = value;
            }
        }

        public DataObjectView()
        {
            InitializeComponent();
            bindingNavigator1.Visible = true;
            gridData.AutoGenerateColumns = true;
            gridData.DisableGridViewError();
            tableIndex = -1;
            FieldDescriptions = null;
            containerScript.SplitterDistance = containerScript.ClientSize.Height - containerScript.SplitterWidth - 1;
            cbTextType.SelectedIndex = 0;
        }

        public ContextMenuStrip ScriptContextMenu
        {
            get { return txtObjScript.ContextMenuStrip; }
            set { txtObjScript.ContextMenuStrip = txtObjScript2.ContextMenuStrip = value; }
        }

        private bool scriptAvailable = true;
        [DefaultValue(true)]
        public bool ScriptAvailable
        {
            get { return scriptAvailable; }
            set
            {
                if (scriptAvailable == value) return;
                scriptAvailable = value;
                if (value && !tabMain.TabPages.Contains(pageScript))
                    tabMain.TabPages.Insert(0, pageScript);
                else if (!value && tabMain.TabPages.Contains(pageScript))
                    tabMain.TabPages.Remove(pageScript);
            }
        }

        public bool ScriptActive => tabMain.TabPages.Contains(pageScript) && tabMain.SelectedTab == pageScript && !gridData.Focused;

        private bool dataAvailable = true;
        [DefaultValue(true)]
        public bool DataAvailable
        {
            get { return dataAvailable; }
            set
            {
                if (dataAvailable == value) return;
                dataAvailable = value;
                if (value && !tabMain.TabPages.Contains(pageFields))
                    tabMain.TabPages.Add(pageFields);
                else if (!value && tabMain.TabPages.Contains(pageFields))
                    tabMain.TabPages.Remove(pageFields);
                if (value && !tabMain.TabPages.Contains(pageCode))
                    tabMain.TabPages.Add(pageCode);
                else if (!value && tabMain.TabPages.Contains(pageCode))
                    tabMain.TabPages.Remove(pageCode);
                splitContainer1.Panel2Collapsed = !value;
            }
        }

        [DefaultValue("")]
        public string ScriptSource
        {
            get { return txtObjScript.Text; }
            set
            {
                txtObjScript.Text = value;
                tabMain.SelectedTab = pageScript;
            }
        }

        [Browsable(false)]
        public string ConnectionString { get; set; }

        public static string GetConnectionString(string connString, string dbname = null, string appname = null)
        {
            var csb = new SqlConnectionStringBuilder(connString);
            if (!String.IsNullOrEmpty(dbname)) csb.InitialCatalog = dbname;
            if (!String.IsNullOrEmpty(appname)) csb.ApplicationName = appname;
            return csb.ToString();
        }

        private Dictionary<string, string> fieldDescriptions;

        public Dictionary<string, string> FieldDescriptions
        {
            get { return fieldDescriptions; }
            set
            {
                fieldDescriptions = value;
                columnDescription.Text = value == null ? "" : "Description";
                columnDescription.Width = value == null ? 0 : 160;
            }
        }

        private string fulltextSearch;

        public string FulltextSearch
        {
            get { return fulltextSearch; }
            set
            {
                fulltextSearch = value;
                txtResult.SetMarkerText(fulltextSearch);
                txtObjScript.SetMarkerText(fulltextSearch);
                txtObjScript.SetFindText(fulltextSearch);
                txtObjScript2.SetMarkerText(fulltextSearch);
            }
        }

        private readonly List<int> fullTextFoundColumns = new List<int>();

        public void QueryData(string dbName, string queryText, int tabPageIndex, string tableSchemaName="", string tableName="")
        {
            tabMain.SelectedIndex = tabPageIndex;
            sourceData.DataSource = null;
            int resultset = -1;
            tableIndex = -1;
            cbDataSet.Items.Clear();
            dataTables.Clear();
            lvResult.Items.Clear();
            lvResult.Groups.Clear();
            txtResult.Text = "";
            lvResult.Tag = string.IsNullOrEmpty(tableName) ? null : string.IsNullOrEmpty(tableSchemaName) ? tableName : $"{tableSchemaName}.{tableName}";
            fieldDescriptions = null;
            string resultText = "";
            var columnNames = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DataObjectView.GetConnectionString(ConnectionString, dbName)))
                {
                    conn.Open();
                    if (string.IsNullOrEmpty(tableSchemaName))
                        FieldDescriptions = null;
                    else
                    {
                        try
                        {
                            FieldDescriptions = new Dictionary<string, string>();
                            using (var cmd = new SqlCommand(
                                String.Format("SELECT name, value FROM fn_listextendedproperty(NULL, 'schema', '{0}', 'table', '{1}', 'column', default)", tableSchemaName, tableName)
                                , conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read()) fieldDescriptions.Add(reader[0].ToString(), reader[1].ToString());
                                }
                            }
                        }
                        catch { }
                        //if (fieldDescriptions.Count == 0) FieldDescriptions = null;
                    }
                    listDsScriptWhere.Items.Clear();
                    using (var cmd = new SqlCommand(queryText, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            var columnErrors = new List<string>();
                            do
                            {
                                ListViewGroup group = null;
                                if (++resultset > 0)
                                {
                                    resultText += String.Format("\r\n//  Resultset #{0}\r\n\r\n", resultset);
                                    group = lvResult.Groups.Add(resultset.ToString(), "Resultset #" + resultset);
                                }
                                int columnOrder = -1;
                                var table = reader.GetSchemaTable();
                                if (table != null)
                                {
                                    var tableData = new DataTable();
                                    dataTables.Add(tableData);
                                    foreach (DataRow row in table.Rows)
                                    {
                                        var cname = row["ColumnName"].ToString();
                                        var dcname = cname;
                                        int dccount=0;
                                        while (tableData.Columns.Contains(dcname))
                                        {
                                            dcname=String.Format("{0}__DUP{1}", cname, ++dccount);
                                        }
                                        if (dccount > 0)
                                        {
                                            columnErrors.Add(String.Format("Column {0} exists already in dataset {1}: renamed to {2}", cname, resultset, dcname));
                                            cname = dcname;
                                        }
                                        var columnData = new DataColumn(dcname);
                                        var sqltype = row["DataTypeName"].ToString();
                                        var sqlbasetype = sqltype;
                                        var tname = sqltype.ToLower();
                                        string nullableSuffix = "?";
                                        switch (tname)
                                        {
                                            case "char":
                                            case "varchar":
                                            case "nvarchar":
                                                int csize = (int)row["ColumnSize"];
                                                sqltype = String.Format("{0}({1})", sqltype, csize < 0 || csize >= 0x7FFFFFFF ? "max" : csize.ToString());
                                                goto case "text";
                                            case "text":
                                            case "ntext":
                                            case "xml":
                                                tname = "string";
                                                columnData.DataType = typeof(string);
                                                nullableSuffix = "";
                                                break;
                                            case "money":
                                                tname = "decimal";
                                                columnData.DataType = typeof(decimal);
                                                break;
                                            case "image":
                                            case "binary":
                                            case "varbinary":
                                                tname = "byte[]";
                                                columnData.DataType = typeof(byte[]);
                                                nullableSuffix = "";
                                                break;
                                            case "timestamp":
                                                tname = "byte[]";
                                                columnData.DataType = typeof(object);
                                                nullableSuffix = "";
                                                break;
                                            case "bigint":
                                                tname = "long";
                                                columnData.DataType = typeof(long);
                                                break;
                                            case "smallint":
                                                if (cname.StartsWith("flag_", StringComparison.OrdinalIgnoreCase)) goto case "bit";
                                                tname = "short";
                                                columnData.DataType = typeof(short);
                                                break;
                                            case "tinyint":
                                                if (cname.StartsWith("flag_", StringComparison.OrdinalIgnoreCase)) goto case "bit";
                                                tname = "byte";
                                                columnData.DataType = typeof(byte);
                                                break;
                                            case "int":
                                                if (cname.StartsWith("flag_", StringComparison.OrdinalIgnoreCase)) goto case "bit";
                                                columnData.DataType = typeof(Int32);
                                                break;
                                            case "bit":
                                                tname = "bool";
                                                columnData.DataType = typeof(bool);
                                                break;
                                            case "uniqueidentifier":
                                                tname = "Guid";
                                                columnData.DataType = typeof(Guid);
                                                break;
                                            case "datetime":
                                            case "date":
                                            case "time":
                                                tname = "DateTime";
                                                columnData.DataType = typeof(DateTime);
                                                break;
                                        }
                                        bool canNull = (bool)row["AllowDBNull"];
                                        columnData.AllowDBNull = canNull;
                                        tname += canNull ? nullableSuffix : "";
                                        var item = new ListViewItem(cname);
                                        columnNames.Add(cname);
                                        if (!String.IsNullOrEmpty(fulltextSearch) && cname.IndexOf(fulltextSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                                        {
                                            item.BackColor = SystemColors.Info;
                                            item.ForeColor = SystemColors.InfoText;
                                        }
                                        item.SubItems.Add(tname);
                                        item.SubItems.Add(canNull ? "" : "x");
                                        item.SubItems.Add((++columnOrder).ToString());
                                        item.SubItems.Add(sqltype);
                                        string descr = null;
                                        string cCode = String.Format("    public {0} {1} {{ get; set; }}\r\n", tname, cname);
                                        if (fieldDescriptions != null && fieldDescriptions.ContainsKey(cname))
                                        {
                                            descr = fieldDescriptions[cname];
                                            item.SubItems.Add(descr);
                                            cCode = String.Format(@"

/// <summary>
/// {0}
/// </summary>
{1}", descr, cCode);
                                        }
                                        if (group != null) item.Group = group;
                                        lvResult.Items.Add(item);
                                        resultText += cCode;
                                        item.Tag = cCode;
                                        item.Checked = true;

                                        var datitem = listDsScriptWhere.Items.Add(cname);
                                        datitem.SubItems.Add(tname);
                                        datitem.Tag = new DataSearchColumn
                                        {
                                            Name = cname,
                                            TypeName = sqlbasetype,
                                            MaxLength = -1,
                                            Index = datitem.Index + 1,
                                            RowsCount = -1
                                        };
                                        try
                                        {
                                            tableData.Columns.Add(columnData);
                                        }
                                        catch (Exception ex)
                                        {
                                            if (dccount > 0) columnErrors.Add(String.Format("Column-Error {0} in dataset {1}: {2}", columnData.ColumnName, resultset, ex.Message));
                                        }
                                    }
                                    while (reader.Read())
                                    {
                                        var row = tableData.NewRow();
                                        var items = new object[columnOrder + 1];
                                        for (int i = 0; i <= columnOrder; i++) if (!reader.IsDBNull(i))
                                                try
                                                {
                                                    object v = reader[i];
                                                    if (tableData.Columns[i].DataType == typeof(bool) && v.GetType() == typeof(int)) v = (int)v != 0;
                                                    items[i] = v;
                                                }
                                                catch { }
                                        row.ItemArray = items;
                                        tableData.Rows.Add(row);
                                    }
                                }
                            } while (reader.NextResult());
                            if (columnErrors.Count > 0)
                            {
                                MessageBox.Show(String.Format("{0} column errors:\r\n* {1}", columnErrors.Count, String.Join("\r\n* ", columnErrors.ToArray())));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error on query:\n{0}", ex.Message));
            }
            lvResult.ShowGroups = resultset > 0;
            var headerText = lvResult.Tag == null ? "": $"//  object:  {lvResult.Tag}\r\n";
            txtResult.Text = $"{headerText}//  columns: {string.Join(", ", columnNames)}\r\n\r\n{resultText}";
            txtResult.SelectAll();
            if (dataTables.Count > 0)
            {
                for (int i = 0; i < dataTables.Count; i++) cbDataSet.Items.Add(String.Format("Resultset #{0}", i));
                TableIndex = cbDataSet.SelectedIndex = 0;
                cbDataSet.Visible = dataTables.Count > 1;
            }
        }

        private void btnPrevSet_Click(object sender, EventArgs e)
        {
            TableIndex--;
        }

        private void btnNextSet_Click(object sender, EventArgs e)
        {
            TableIndex++;
        }

        private void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            GridCellFormatting(sourceData, e);
        }

        public static void GridCellFormatting(BindingSource sourceData, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            var type = (sourceData.DataSource as DataTable).Columns[e.ColumnIndex].DataType;
            switch (type.Name.ToLower())
            {
                case "decimal":
                case "int32":
                case "int64":
                case "long":
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    break;
                case "object":
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (e.Value != null)
                    {
                        var sb = new StringBuilder();
                        sb.Append("0x");
                        var v = (byte[])e.Value;
                        for (int i = 0; i < v.Length; i++) sb.Append(String.Format("{0:X2}", v[i]));
                        e.Value = sb.ToString();
                    }
                    break;
            }
        }

        private void cbDataSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableIndex = cbDataSet.SelectedIndex;
        }

        private void tabMain_Selected(object sender, TabControlEventArgs e)
        {
            if (tabMain.SelectedTab == pageCode)
            {
                var resultText = "";
                var columnNames = new List<string>();
                foreach (ListViewItem item in lvResult.Items)
                    if (item.Checked && item.Group == null)
                    {
                        resultText += item.Tag.ToString();
                        columnNames.Add(item.Text);
                    }
                int resultset = 0;
                foreach (ListViewGroup group in lvResult.Groups)
                {
                    var groupText = "";
                    foreach (ListViewItem groupitem in group.Items)
                        if (groupitem.Checked)
                        {
                            groupText += groupitem.Tag.ToString();
                            columnNames.Add(groupitem.Text);
                        }
                    if (!String.IsNullOrEmpty(groupText))
                    {
                        if (resultset == 0) resultText = String.Format("//  Resultset #0\r\n\r\n{0}", resultText);
                        groupText = String.Format("\r\n//  Resultset #{0}\r\n\r\n{1}", ++resultset, groupText);
                    }
                    resultText += groupText;
                }
                txtResult.Text = resultText;
                var headerText = lvResult.Tag == null ? "" : $"//  object:  {lvResult.Tag}\r\n";
                txtResult.Text = $"{headerText}//  columns: {string.Join(", ", columnNames)}\r\n\r\n{resultText}";
            }
            else if (tabMain.SelectedTab == pageScript)
            {
                foreach (ListViewItem item in lvResult.Items)
                {
                    var column = gridData.Columns[item.Text];
                    if (column != null) column.Visible = item.Checked && (btnShowImage.Checked || item.SubItems[1].Text != "byte[]");
                }
            }
        }

        private void gridData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (fullTextFoundColumns.Contains(e.ColumnIndex))
            {
                e.CellStyle.BackColor = SystemColors.Info;
                e.CellStyle.ForeColor = SystemColors.InfoText;
            }
        }

        private void txtResult_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(fulltextSearch))
                (sender as FastColoredTextBox).SetMarkerText(fulltextSearch);
        }

        private void paintSplitter(object sender, PaintEventArgs e)
        {
            var splitter = sender as SplitContainer;
            ControlPaint.DrawGrabHandle(e.Graphics, splitter.SplitterRectangle, true, true);
        }

        public void ClearMarkerText()
        {
            txtResult.ClearMarkerText();
            txtObjScript.ClearMarkerText();
            txtObjScript2.ClearMarkerText();
        }

        public bool ShowScriptWindow2()
        {
            var y = containerScript.ClientSize.Height - containerScript.SplitterDistance;
            if (y > 5)
            {
                containerScript.SplitterDistance = containerScript.ClientSize.Height - 5;
                return false;
            }
            else
            {
                containerScript.SplitterDistance = containerScript.ClientSize.Height / 2;
                return true;
            }
        }

        public event EventHandler ScriptEditorEnter;
        private void ScriptEditor_Enter(object sender, EventArgs e)
        {
            mapObjScript.Target = (sender as FastColoredTextBoxNS.FastColoredTextBox);
            ScriptEditorEnter?.Invoke(sender, e);
        }

        private void gridData_SelectionChanged(object sender, EventArgs e)
        {
            var cell = gridData.SelectedCells.OfType<DataGridViewCell>().FirstOrDefault();
            if (cell == null)
            {
                lblDataFieldName.Text = "";
                txtDataField.Text = "";
                return;
            }
            lblDataFieldName.Text = $"[{cell.ColumnIndex}:{cell.RowIndex}] {(cell.ColumnIndex < 0 || cell.ColumnIndex >= gridData.ColumnCount ? "" : gridData.Columns[cell.ColumnIndex].HeaderText)}";
            txtDataField.Text = txtDataField.Language == Language.XML ? FormatXml($"{cell.Value}") : btnFormattedText.Checked ? cell.FormattedValue.ToString() : $"{cell.Value}";
        }
        public static string FormatXml(string xml) {
            if (string.IsNullOrEmpty(xml) || xml.IndexOf('<') < 0 || xml.IndexOf('>') < 0) return xml;
            try
            {
                var doc = new XmlDocument();
                var guid = "___" + Guid.NewGuid().ToString("N");
                doc.LoadXml($"<{guid}>{xml}</{guid}>");
                var sb = new StringBuilder();
                var settings =
                    new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "\t",
                        NewLineChars = Environment.NewLine,
                        NewLineHandling = NewLineHandling.Replace,
                        OmitXmlDeclaration = true,
                        Encoding = Encoding.UTF8
                    };

                using (var writer = XmlWriter.Create(sb, settings))
                {
                    if (doc.ChildNodes[0] is XmlProcessingInstruction)
                    {
                        doc.RemoveChild(doc.ChildNodes[0]);
                    }

                    doc.Save(writer);
                    var result = sb.ToString().Substring(guid.Length + 2 + $"{settings.NewLineChars}{settings.IndentChars}".Length);
                    return result.Substring(0, result.Length - (guid.Length + 3 + $"{settings.NewLineChars}".Length))
                        .Replace($"{settings.NewLineChars}{settings.IndentChars}", $"{settings.NewLineChars}");
                }
            }
            catch
            {
                return xml;
            }
        }

        public static Language GetLanguageFromIndex(string index)
        {
            switch ((index ?? "").ToLower())
            {
                case "sql": return Language.SQL;
                case "c#": return Language.CSharp;
                case "html": return Language.HTML;
                case "js": return Language.JS;
                case "lua": return Language.Lua;
                case "php": return Language.PHP;
                case "vb": return Language.VB;
                case "xml": return Language.XML;
            }
            return Language.Custom;
        }

        private void cbTextType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDataField.Language = GetLanguageFromIndex(cbTextType.Text);
            gridData_SelectionChanged(null, null);
        }

        private void btnWordWrap_Click(object sender, EventArgs e)
        {
            txtDataField.WordWrap = btnWordWrap.Checked;
        }

        private void lvResult_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var item = e.Item;
            //foreach (ListViewItem item in lvResult.Items)
            {
                var column = gridData.Columns[item.Text];
                if (column != null) column.Visible = item.Checked && (btnShowImage.Checked || item.SubItems[1].Text != "byte[]");
            }
        }

        private void cboDataColumnFrozen_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maxFrozen = cboDataColumnFrozen.SelectedIndex;
            for (var i = gridData.Columns.Count - 1; i > maxFrozen && i>=0; i--)
            {
                gridData.Columns[i].Frozen = false;
            }
            for (var i = 0; i <= maxFrozen; i++)
            {
                gridData.Columns[i].Frozen = true;
            }
            gridData.Invalidate();
        }

        private void btnSaveCell_Click(object sender, EventArgs e)
        {
            if (gridData.CurrentCell == null) return;
            if (saveFileDialog1.ShowDialog(ParentForm) != DialogResult.OK) return;
            using (var file = new StreamWriter(saveFileDialog1.FileName, false))
            {
                file.Write(gridData.CurrentCell.Value.ToString());
            }
            MessageBox.Show(String.Format("{0} written to file: {1}", gridData.CurrentCell.OwningColumn.HeaderText, saveFileDialog1.FileName));
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
            if (columns.Count == 0 || sourceData.Count == 0)
            {
                txtDsScriptWhere.Text = "";
                return;
            }
            var columnsheader = string.Join("\r\n    ,", columns.OrderBy(c => c.Index).Select(c => c.Name + " \t" + c.FullTypeName).ToArray());
            var colValues = new string[columns.Count];
            var rowValues = new List<string>();
            var table = sourceData.DataSource as DataTable;
            for (var row = 0; row < table.Rows.Count; row++)
            {
                if (row > 0 && row % 1000 == 0)
                {
                    rowValues.Add("insert into @parameters values");
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
                            var chars = value.ToString().ToCharArray();
                            for (var chi = 0; chi < chars.Length; chi++)
                            {
                                var c = chars[chi];
                                if (c < 32)
                                {
                                    var cp = chi > 0 ? chars[chi - 1] : '0';
                                    var cn = chi < chars.Length - 1 ? chars[chi + 1] : '0';
                                    if (cp >= 32) sb.Append("'"); 
                                    sb.Append(" + CHAR(").Append((int)c);
                                    sb.Append(cn < 32 ? ")" : ") + '");
                                }
                                else if (c == '\'')
                                {
                                    sb.Append("''");
                                }
                                else
                                {
                                    sb.Append(c);
                                }
                            }
                        }
                        else
                        {
                            sb.Append(value.ToString().Replace("'", "''"));
                        }
                        colValues[col] = string.Format("{0}{1}{0}", delim, sb.ToString());
                    }
                }
                rowValues.Add((row % 1000 == 0 ? "     " : "    ,") + "(" + string.Join(", ", colValues) + ")" + (row < table.Rows.Count -1 && (row + 1) % 1000 == 0 ? ";" : ""));
            }
            txtDsScriptWhere.Text = string.Format(sql, columnsheader, string.Join("\r\n", rowValues));
        }
    }
}
