namespace DBMapper
{
    partial class FrmDBMapper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDBMapper));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Query = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pageQuery = new System.Windows.Forms.TabPage();
            this.splitContainerQuery = new System.Windows.Forms.SplitContainer();
            this.contextMenuQueryEditor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnADODB = new System.Windows.Forms.Button();
            this.cbConnections = new System.Windows.Forms.ComboBox();
            this.chkLoginMappingsOnly = new System.Windows.Forms.CheckBox();
            this.cbDatabases = new System.Windows.Forms.ComboBox();
            this.btnNewInstance = new System.Windows.Forms.Button();
            this.pageDataSearch = new System.Windows.Forms.TabPage();
            this.containerData = new System.Windows.Forms.SplitContainer();
            this.containerDataCockpit = new System.Windows.Forms.SplitContainer();
            this.listDsDb = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkDsViews = new System.Windows.Forms.CheckBox();
            this.cbDsContent = new System.Windows.Forms.ComboBox();
            this.cbDsColType = new System.Windows.Forms.ComboBox();
            this.cbDsColName = new System.Windows.Forms.ComboBox();
            this.cbDsObject = new System.Windows.Forms.ComboBox();
            this.cbDsValueContent = new System.Windows.Forms.ComboBox();
            this.cbDsValueColType = new System.Windows.Forms.ComboBox();
            this.cbDsValueColName = new System.Windows.Forms.ComboBox();
            this.cbDsValueObject = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDsValueSchema = new System.Windows.Forms.ComboBox();
            this.cbDsSchema = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDsNOTSchema = new System.Windows.Forms.CheckBox();
            this.chkDsNOTObject = new System.Windows.Forms.CheckBox();
            this.chkDsNOTColName = new System.Windows.Forms.CheckBox();
            this.chkDsNOTColType = new System.Windows.Forms.CheckBox();
            this.chkDsNOTContent = new System.Windows.Forms.CheckBox();
            this.chkDsTable = new System.Windows.Forms.CheckBox();
            this.lblRunningCount = new System.Windows.Forms.Label();
            this.treeDsResult = new System.Windows.Forms.TreeView();
            this.menuTreeDsResult = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTreeDsResult2Excel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeDsResultData2Excel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeDsResultCopySelectedTables = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeDsResultCopySelectedBranch = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.progressDsRunning = new System.Windows.Forms.ProgressBar();
            this.lblSearchTooltip = new System.Windows.Forms.Label();
            this.listRunningTasks = new System.Windows.Forms.ListBox();
            this.panelDsGo = new System.Windows.Forms.FlowLayoutPanel();
            this.btnDsGo = new System.Windows.Forms.Button();
            this.containerDataResult = new System.Windows.Forms.SplitContainer();
            this.containerDsResult = new System.Windows.Forms.SplitContainer();
            this.dataGridViewDsResult = new System.Windows.Forms.DataGridView();
            this.bindingNavigatorDsResult = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.lblDsResultTop = new System.Windows.Forms.ToolStripLabel();
            this.cbDsResultTop = new System.Windows.Forms.ToolStripComboBox();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtDsResultFilter = new System.Windows.Forms.ToolStripComboBox();
            this.cbTextType = new System.Windows.Forms.ToolStripComboBox();
            this.lblDsResultTable = new System.Windows.Forms.ToolStripLabel();
            this.tabDsResultOperations = new System.Windows.Forms.TabControl();
            this.pageDsScriptWhere = new System.Windows.Forms.TabPage();
            this.containerDsScriptWhere = new System.Windows.Forms.SplitContainer();
            this.listDsScriptWhere = new System.Windows.Forms.ListView();
            this.columnDsScriptWhereName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDsScriptWhereType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pageObjects = new System.Windows.Forms.TabPage();
            this.containerDbobjects = new System.Windows.Forms.SplitContainer();
            this.panObjects = new System.Windows.Forms.TableLayoutPanel();
            this.cbObjectsDB = new System.Windows.Forms.ComboBox();
            this.txtObjFilter = new System.Windows.Forms.ComboBox();
            this.menuObjFilter = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuObjFilterNoFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjFilterNamesSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjFilterFulltextSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.btnObjFilter = new System.Windows.Forms.Button();
            this.menuObjScriptText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuScriptTextSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptAppendToQueryBox = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScriptTextWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptTextDblClick = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScriptTextShowWindow2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pageProfiler = new System.Windows.Forms.TabPage();
            this.ilObjects = new System.Windows.Forms.ImageList(this.components);
            this.menuObjectTreeItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuObjDBPrefix = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjNoBraceToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjToQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjAppendToQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuObjCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSQL = new FastColoredTextBoxNS.FastColoredTextBox();
            this.dataViewQuery = new DBMapper.DataObjectView();
            this.txtDsResultSQL = new FastColoredTextBoxNS.FastColoredTextBox();
            this.txtDataField = new FastColoredTextBoxNS.FastColoredTextBox();
            this.txtDsScriptWhere = new FastColoredTextBoxNS.FastColoredTextBox();
            this.dataViewCurrentObject = new DBMapper.DataObjectView();
            this.sqlProfiler = new ExpressProfiler.SQLProfiler();
            this.btnDsOpen = new System.Windows.Forms.Button();
            this.btnDsSave = new System.Windows.Forms.Button();
            this.bindingSourceDsResult = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.btnShowImage = new System.Windows.Forms.ToolStripButton();
            this.btnDsResultFilter = new System.Windows.Forms.ToolStripButton();
            this.btnWordWrap = new System.Windows.Forms.ToolStripButton();
            this.btnFormattedText = new System.Windows.Forms.ToolStripButton();
            this.txtDsResultGenerateSQL = new System.Windows.Forms.ToolStripButton();
            this.btnDsGridExport = new System.Windows.Forms.ToolStripButton();
            this.tabMain.SuspendLayout();
            this.pageQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerQuery)).BeginInit();
            this.splitContainerQuery.Panel1.SuspendLayout();
            this.splitContainerQuery.Panel2.SuspendLayout();
            this.splitContainerQuery.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pageDataSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerData)).BeginInit();
            this.containerData.Panel1.SuspendLayout();
            this.containerData.Panel2.SuspendLayout();
            this.containerData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerDataCockpit)).BeginInit();
            this.containerDataCockpit.Panel1.SuspendLayout();
            this.containerDataCockpit.Panel2.SuspendLayout();
            this.containerDataCockpit.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menuTreeDsResult.SuspendLayout();
            this.panelDsGo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerDataResult)).BeginInit();
            this.containerDataResult.Panel1.SuspendLayout();
            this.containerDataResult.Panel2.SuspendLayout();
            this.containerDataResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerDsResult)).BeginInit();
            this.containerDsResult.Panel1.SuspendLayout();
            this.containerDsResult.Panel2.SuspendLayout();
            this.containerDsResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDsResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorDsResult)).BeginInit();
            this.bindingNavigatorDsResult.SuspendLayout();
            this.tabDsResultOperations.SuspendLayout();
            this.pageDsScriptWhere.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerDsScriptWhere)).BeginInit();
            this.containerDsScriptWhere.Panel1.SuspendLayout();
            this.containerDsScriptWhere.Panel2.SuspendLayout();
            this.containerDsScriptWhere.SuspendLayout();
            this.pageObjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.containerDbobjects)).BeginInit();
            this.containerDbobjects.Panel1.SuspendLayout();
            this.containerDbobjects.Panel2.SuspendLayout();
            this.containerDbobjects.SuspendLayout();
            this.panObjects.SuspendLayout();
            this.menuObjFilter.SuspendLayout();
            this.menuObjScriptText.SuspendLayout();
            this.pageProfiler.SuspendLayout();
            this.menuObjectTreeItem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsResultSQL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsScriptWhere)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDsResult)).BeginInit();
            this.SuspendLayout();
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(796, 5);
            this.Query.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(112, 32);
            this.Query.TabIndex = 2;
            this.Query.Text = "qu&ery";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(916, 5);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(112, 32);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "&open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1036, 5);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.Filter = "text files|*.txt|all files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "txt";
            this.saveFileDialog1.Filter = "text files|*.txt";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pageQuery);
            this.tabMain.Controls.Add(this.pageDataSearch);
            this.tabMain.Controls.Add(this.pageObjects);
            this.tabMain.Controls.Add(this.pageProfiler);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1578, 868);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            // 
            // pageQuery
            // 
            this.pageQuery.Controls.Add(this.splitContainerQuery);
            this.pageQuery.Controls.Add(this.flowLayoutPanel1);
            this.pageQuery.Location = new System.Drawing.Point(4, 29);
            this.pageQuery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageQuery.Name = "pageQuery";
            this.pageQuery.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageQuery.Size = new System.Drawing.Size(1570, 835);
            this.pageQuery.TabIndex = 0;
            this.pageQuery.Text = "Query";
            this.pageQuery.UseVisualStyleBackColor = true;
            // 
            // splitContainerQuery
            // 
            this.splitContainerQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerQuery.Location = new System.Drawing.Point(4, 53);
            this.splitContainerQuery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainerQuery.Name = "splitContainerQuery";
            this.splitContainerQuery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerQuery.Panel1
            // 
            this.splitContainerQuery.Panel1.Controls.Add(this.txtSQL);
            // 
            // splitContainerQuery.Panel2
            // 
            this.splitContainerQuery.Panel2.Controls.Add(this.dataViewQuery);
            this.splitContainerQuery.Size = new System.Drawing.Size(1562, 777);
            this.splitContainerQuery.SplitterDistance = 384;
            this.splitContainerQuery.SplitterWidth = 6;
            this.splitContainerQuery.TabIndex = 7;
            this.splitContainerQuery.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // contextMenuQueryEditor
            // 
            this.contextMenuQueryEditor.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuQueryEditor.Name = "contextMenuQueryEditor";
            this.contextMenuQueryEditor.Size = new System.Drawing.Size(61, 4);
            this.contextMenuQueryEditor.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuQueryEditor_Opening);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.btnADODB);
            this.flowLayoutPanel1.Controls.Add(this.cbConnections);
            this.flowLayoutPanel1.Controls.Add(this.chkLoginMappingsOnly);
            this.flowLayoutPanel1.Controls.Add(this.cbDatabases);
            this.flowLayoutPanel1.Controls.Add(this.Query);
            this.flowLayoutPanel1.Controls.Add(this.btnOpen);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnNewInstance);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1562, 48);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // btnADODB
            // 
            this.btnADODB.Location = new System.Drawing.Point(4, 5);
            this.btnADODB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnADODB.Name = "btnADODB";
            this.btnADODB.Size = new System.Drawing.Size(112, 32);
            this.btnADODB.TabIndex = 7;
            this.btnADODB.Text = "connection";
            this.btnADODB.UseVisualStyleBackColor = true;
            this.btnADODB.Click += new System.EventHandler(this.btnADODB_Click);
            // 
            // cbConnections
            // 
            this.cbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConnections.DropDownWidth = 200;
            this.cbConnections.FormattingEnabled = true;
            this.cbConnections.Location = new System.Drawing.Point(124, 5);
            this.cbConnections.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbConnections.Name = "cbConnections";
            this.cbConnections.Size = new System.Drawing.Size(232, 28);
            this.cbConnections.TabIndex = 9;
            this.cbConnections.SelectedIndexChanged += new System.EventHandler(this.cbConnections_SelectedIndexChanged);
            // 
            // chkLoginMappingsOnly
            // 
            this.chkLoginMappingsOnly.AutoSize = true;
            this.chkLoginMappingsOnly.Location = new System.Drawing.Point(364, 5);
            this.chkLoginMappingsOnly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkLoginMappingsOnly.Name = "chkLoginMappingsOnly";
            this.chkLoginMappingsOnly.Size = new System.Drawing.Size(184, 24);
            this.chkLoginMappingsOnly.TabIndex = 8;
            this.chkLoginMappingsOnly.Text = "only login mapped db";
            this.chkLoginMappingsOnly.UseVisualStyleBackColor = true;
            this.chkLoginMappingsOnly.CheckedChanged += new System.EventHandler(this.chkLoginMappingsOnly_CheckedChanged);
            // 
            // cbDatabases
            // 
            this.cbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabases.DropDownWidth = 200;
            this.cbDatabases.FormattingEnabled = true;
            this.cbDatabases.Location = new System.Drawing.Point(556, 5);
            this.cbDatabases.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDatabases.Name = "cbDatabases";
            this.cbDatabases.Size = new System.Drawing.Size(232, 28);
            this.cbDatabases.TabIndex = 1;
            // 
            // btnNewInstance
            // 
            this.btnNewInstance.Location = new System.Drawing.Point(1156, 5);
            this.btnNewInstance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNewInstance.Name = "btnNewInstance";
            this.btnNewInstance.Size = new System.Drawing.Size(112, 32);
            this.btnNewInstance.TabIndex = 6;
            this.btnNewInstance.Text = "&new";
            this.btnNewInstance.UseVisualStyleBackColor = true;
            this.btnNewInstance.Click += new System.EventHandler(this.btnNewInstance_Click);
            // 
            // pageDataSearch
            // 
            this.pageDataSearch.Controls.Add(this.containerData);
            this.pageDataSearch.Location = new System.Drawing.Point(4, 29);
            this.pageDataSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageDataSearch.Name = "pageDataSearch";
            this.pageDataSearch.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageDataSearch.Size = new System.Drawing.Size(1570, 835);
            this.pageDataSearch.TabIndex = 3;
            this.pageDataSearch.Text = "dataSearch";
            this.pageDataSearch.UseVisualStyleBackColor = true;
            // 
            // containerData
            // 
            this.containerData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerData.Location = new System.Drawing.Point(4, 5);
            this.containerData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerData.Name = "containerData";
            // 
            // containerData.Panel1
            // 
            this.containerData.Panel1.Controls.Add(this.containerDataCockpit);
            // 
            // containerData.Panel2
            // 
            this.containerData.Panel2.Controls.Add(this.containerDataResult);
            this.containerData.Size = new System.Drawing.Size(1562, 825);
            this.containerData.SplitterDistance = 483;
            this.containerData.SplitterWidth = 6;
            this.containerData.TabIndex = 0;
            this.containerData.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // containerDataCockpit
            // 
            this.containerDataCockpit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDataCockpit.Location = new System.Drawing.Point(0, 0);
            this.containerDataCockpit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerDataCockpit.Name = "containerDataCockpit";
            this.containerDataCockpit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerDataCockpit.Panel1
            // 
            this.containerDataCockpit.Panel1.Controls.Add(this.listDsDb);
            // 
            // containerDataCockpit.Panel2
            // 
            this.containerDataCockpit.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.containerDataCockpit.Size = new System.Drawing.Size(483, 825);
            this.containerDataCockpit.SplitterDistance = 161;
            this.containerDataCockpit.SplitterWidth = 6;
            this.containerDataCockpit.TabIndex = 0;
            this.containerDataCockpit.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // listDsDb
            // 
            this.listDsDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDsDb.FormattingEnabled = true;
            this.listDsDb.ItemHeight = 20;
            this.listDsDb.Location = new System.Drawing.Point(0, 0);
            this.listDsDb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listDsDb.Name = "listDsDb";
            this.listDsDb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listDsDb.Size = new System.Drawing.Size(483, 161);
            this.listDsDb.TabIndex = 0;
            this.listDsDb.SelectedValueChanged += new System.EventHandler(this.listDsDb_SelectedValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeDsResult, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.progressDsRunning, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblSearchTooltip, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listRunningTasks, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelDsGo, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(483, 658);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.chkDsViews, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbDsContent, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.cbDsColType, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbDsColName, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbDsObject, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbDsValueContent, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this.cbDsValueColType, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.cbDsValueColName, 3, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbDsValueObject, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.cbDsValueSchema, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbDsSchema, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkDsNOTSchema, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkDsNOTObject, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.chkDsNOTColName, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkDsNOTColType, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.chkDsNOTContent, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.chkDsTable, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblRunningCount, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(475, 224);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // chkDsViews
            // 
            this.chkDsViews.AutoSize = true;
            this.chkDsViews.Location = new System.Drawing.Point(296, 5);
            this.chkDsViews.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsViews.Name = "chkDsViews";
            this.chkDsViews.Size = new System.Drawing.Size(77, 24);
            this.chkDsViews.TabIndex = 1;
            this.chkDsViews.Text = "Views";
            this.chkDsViews.UseVisualStyleBackColor = true;
            // 
            // cbDsContent
            // 
            this.cbDsContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDsContent.FormattingEnabled = true;
            this.cbDsContent.Items.AddRange(new object[] {
            "=",
            "Like",
            "Start",
            "End",
            "!=",
            "!Like",
            "!Start",
            "!End"});
            this.cbDsContent.Location = new System.Drawing.Point(167, 191);
            this.cbDsContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsContent.Name = "cbDsContent";
            this.cbDsContent.Size = new System.Drawing.Size(121, 28);
            this.cbDsContent.TabIndex = 17;
            this.cbDsContent.SelectedIndexChanged += new System.EventHandler(this.cbDsContent_SelectedIndexChanged);
            // 
            // cbDsColType
            // 
            this.cbDsColType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsColType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDsColType.FormattingEnabled = true;
            this.cbDsColType.Items.AddRange(new object[] {
            "=",
            "Like",
            "Start",
            "End",
            "!=",
            "!Like",
            "!Start",
            "!End"});
            this.cbDsColType.Location = new System.Drawing.Point(167, 153);
            this.cbDsColType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsColType.Name = "cbDsColType";
            this.cbDsColType.Size = new System.Drawing.Size(121, 28);
            this.cbDsColType.TabIndex = 16;
            // 
            // cbDsColName
            // 
            this.cbDsColName.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsColName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDsColName.FormattingEnabled = true;
            this.cbDsColName.Items.AddRange(new object[] {
            "=",
            "Like",
            "Start",
            "End",
            "!=",
            "!Like",
            "!Start",
            "!End"});
            this.cbDsColName.Location = new System.Drawing.Point(167, 115);
            this.cbDsColName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsColName.Name = "cbDsColName";
            this.cbDsColName.Size = new System.Drawing.Size(121, 28);
            this.cbDsColName.TabIndex = 15;
            // 
            // cbDsObject
            // 
            this.cbDsObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDsObject.FormattingEnabled = true;
            this.cbDsObject.Items.AddRange(new object[] {
            "=",
            "Like",
            "Start",
            "End",
            "!=",
            "!Like",
            "!Start",
            "!End"});
            this.cbDsObject.Location = new System.Drawing.Point(167, 77);
            this.cbDsObject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsObject.Name = "cbDsObject";
            this.cbDsObject.Size = new System.Drawing.Size(121, 28);
            this.cbDsObject.TabIndex = 14;
            // 
            // cbDsValueContent
            // 
            this.cbDsValueContent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDsValueContent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDsValueContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsValueContent.FormattingEnabled = true;
            this.cbDsValueContent.Location = new System.Drawing.Point(296, 191);
            this.cbDsValueContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsValueContent.Name = "cbDsValueContent";
            this.cbDsValueContent.Size = new System.Drawing.Size(175, 28);
            this.cbDsValueContent.TabIndex = 13;
            this.cbDsValueContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbDsValueContent_KeyPress);
            this.cbDsValueContent.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbDsValueContent_KeyUp);
            // 
            // cbDsValueColType
            // 
            this.cbDsValueColType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDsValueColType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDsValueColType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsValueColType.FormattingEnabled = true;
            this.cbDsValueColType.Location = new System.Drawing.Point(296, 153);
            this.cbDsValueColType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsValueColType.Name = "cbDsValueColType";
            this.cbDsValueColType.Size = new System.Drawing.Size(175, 28);
            this.cbDsValueColType.TabIndex = 11;
            // 
            // cbDsValueColName
            // 
            this.cbDsValueColName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDsValueColName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDsValueColName.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsValueColName.FormattingEnabled = true;
            this.cbDsValueColName.Location = new System.Drawing.Point(296, 115);
            this.cbDsValueColName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsValueColName.Name = "cbDsValueColName";
            this.cbDsValueColName.Size = new System.Drawing.Size(175, 28);
            this.cbDsValueColName.TabIndex = 9;
            // 
            // cbDsValueObject
            // 
            this.cbDsValueObject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDsValueObject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDsValueObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsValueObject.FormattingEnabled = true;
            this.cbDsValueObject.Location = new System.Drawing.Point(296, 77);
            this.cbDsValueObject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsValueObject.Name = "cbDsValueObject";
            this.cbDsValueObject.Size = new System.Drawing.Size(175, 28);
            this.cbDsValueObject.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schema";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Object";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "ColName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "ColType";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 186);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Content";
            // 
            // cbDsValueSchema
            // 
            this.cbDsValueSchema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbDsValueSchema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbDsValueSchema.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsValueSchema.FormattingEnabled = true;
            this.cbDsValueSchema.Location = new System.Drawing.Point(296, 39);
            this.cbDsValueSchema.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsValueSchema.Name = "cbDsValueSchema";
            this.cbDsValueSchema.Size = new System.Drawing.Size(175, 28);
            this.cbDsValueSchema.TabIndex = 5;
            // 
            // cbDsSchema
            // 
            this.cbDsSchema.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbDsSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDsSchema.FormattingEnabled = true;
            this.cbDsSchema.Items.AddRange(new object[] {
            "=",
            "Like",
            "Start",
            "End",
            "!=",
            "!Like",
            "!Start",
            "!End"});
            this.cbDsSchema.Location = new System.Drawing.Point(167, 39);
            this.cbDsSchema.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDsSchema.Name = "cbDsSchema";
            this.cbDsSchema.Size = new System.Drawing.Size(121, 28);
            this.cbDsSchema.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "Search in:";
            // 
            // chkDsNOTSchema
            // 
            this.chkDsNOTSchema.AutoSize = true;
            this.chkDsNOTSchema.Location = new System.Drawing.Point(92, 39);
            this.chkDsNOTSchema.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsNOTSchema.Name = "chkDsNOTSchema";
            this.chkDsNOTSchema.Size = new System.Drawing.Size(67, 24);
            this.chkDsNOTSchema.TabIndex = 19;
            this.chkDsNOTSchema.Text = "NOT";
            this.chkDsNOTSchema.UseVisualStyleBackColor = true;
            // 
            // chkDsNOTObject
            // 
            this.chkDsNOTObject.AutoSize = true;
            this.chkDsNOTObject.Location = new System.Drawing.Point(92, 77);
            this.chkDsNOTObject.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsNOTObject.Name = "chkDsNOTObject";
            this.chkDsNOTObject.Size = new System.Drawing.Size(67, 24);
            this.chkDsNOTObject.TabIndex = 20;
            this.chkDsNOTObject.Text = "NOT";
            this.chkDsNOTObject.UseVisualStyleBackColor = true;
            // 
            // chkDsNOTColName
            // 
            this.chkDsNOTColName.AutoSize = true;
            this.chkDsNOTColName.Location = new System.Drawing.Point(92, 115);
            this.chkDsNOTColName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsNOTColName.Name = "chkDsNOTColName";
            this.chkDsNOTColName.Size = new System.Drawing.Size(67, 24);
            this.chkDsNOTColName.TabIndex = 21;
            this.chkDsNOTColName.Text = "NOT";
            this.chkDsNOTColName.UseVisualStyleBackColor = true;
            // 
            // chkDsNOTColType
            // 
            this.chkDsNOTColType.AutoSize = true;
            this.chkDsNOTColType.Location = new System.Drawing.Point(92, 153);
            this.chkDsNOTColType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsNOTColType.Name = "chkDsNOTColType";
            this.chkDsNOTColType.Size = new System.Drawing.Size(67, 24);
            this.chkDsNOTColType.TabIndex = 22;
            this.chkDsNOTColType.Text = "NOT";
            this.chkDsNOTColType.UseVisualStyleBackColor = true;
            // 
            // chkDsNOTContent
            // 
            this.chkDsNOTContent.AutoSize = true;
            this.chkDsNOTContent.Location = new System.Drawing.Point(92, 191);
            this.chkDsNOTContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsNOTContent.Name = "chkDsNOTContent";
            this.chkDsNOTContent.Size = new System.Drawing.Size(67, 24);
            this.chkDsNOTContent.TabIndex = 23;
            this.chkDsNOTContent.Text = "NOT";
            this.chkDsNOTContent.UseVisualStyleBackColor = true;
            // 
            // chkDsTable
            // 
            this.chkDsTable.AutoSize = true;
            this.chkDsTable.Checked = true;
            this.chkDsTable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDsTable.Location = new System.Drawing.Point(167, 5);
            this.chkDsTable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDsTable.Name = "chkDsTable";
            this.chkDsTable.Size = new System.Drawing.Size(82, 24);
            this.chkDsTable.TabIndex = 0;
            this.chkDsTable.Text = "Tables";
            this.chkDsTable.UseVisualStyleBackColor = true;
            // 
            // lblRunningCount
            // 
            this.lblRunningCount.AutoSize = true;
            this.lblRunningCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRunningCount.Location = new System.Drawing.Point(92, 0);
            this.lblRunningCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRunningCount.Name = "lblRunningCount";
            this.lblRunningCount.Size = new System.Drawing.Size(67, 34);
            this.lblRunningCount.TabIndex = 24;
            this.lblRunningCount.Text = "0";
            this.lblRunningCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeDsResult
            // 
            this.treeDsResult.ContextMenuStrip = this.menuTreeDsResult;
            this.treeDsResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDsResult.HideSelection = false;
            this.treeDsResult.ImageIndex = 0;
            this.treeDsResult.ImageList = this.imageListTree;
            this.treeDsResult.Location = new System.Drawing.Point(4, 365);
            this.treeDsResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeDsResult.Name = "treeDsResult";
            this.treeDsResult.SelectedImageIndex = 0;
            this.treeDsResult.Size = new System.Drawing.Size(475, 162);
            this.treeDsResult.TabIndex = 4;
            this.treeDsResult.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDsResult_AfterSelect);
            // 
            // menuTreeDsResult
            // 
            this.menuTreeDsResult.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuTreeDsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTreeDsResult2Excel,
            this.mnuTreeDsResultData2Excel,
            this.mnuTreeDsResultCopySelectedTables,
            this.mnuTreeDsResultCopySelectedBranch});
            this.menuTreeDsResult.Name = "menuTreeDsResult";
            this.menuTreeDsResult.Size = new System.Drawing.Size(443, 132);
            // 
            // mnuTreeDsResult2Excel
            // 
            this.mnuTreeDsResult2Excel.Name = "mnuTreeDsResult2Excel";
            this.mnuTreeDsResult2Excel.Size = new System.Drawing.Size(442, 32);
            this.mnuTreeDsResult2Excel.Text = "export tree to Excel";
            this.mnuTreeDsResult2Excel.Click += new System.EventHandler(this.mnuTreeDsResult2Excel_Click);
            // 
            // mnuTreeDsResultData2Excel
            // 
            this.mnuTreeDsResultData2Excel.Name = "mnuTreeDsResultData2Excel";
            this.mnuTreeDsResultData2Excel.Size = new System.Drawing.Size(442, 32);
            this.mnuTreeDsResultData2Excel.Text = "export data of selected branch tables to Excel";
            this.mnuTreeDsResultData2Excel.Click += new System.EventHandler(this.mnuTreeDsResultData2Excel_Click);
            // 
            // mnuTreeDsResultCopySelectedTables
            // 
            this.mnuTreeDsResultCopySelectedTables.Name = "mnuTreeDsResultCopySelectedTables";
            this.mnuTreeDsResultCopySelectedTables.Size = new System.Drawing.Size(442, 32);
            this.mnuTreeDsResultCopySelectedTables.Text = "copy selected branch tables to clipboard";
            this.mnuTreeDsResultCopySelectedTables.Click += new System.EventHandler(this.mnuTreeDsResultCopySelectedTables_Click);
            // 
            // mnuTreeDsResultCopySelectedBranch
            // 
            this.mnuTreeDsResultCopySelectedBranch.Name = "mnuTreeDsResultCopySelectedBranch";
            this.mnuTreeDsResultCopySelectedBranch.Size = new System.Drawing.Size(442, 32);
            this.mnuTreeDsResultCopySelectedBranch.Text = "copy selected branch columns to clipboard";
            this.mnuTreeDsResultCopySelectedBranch.Click += new System.EventHandler(this.mnuTreeDsResultCopySelectedBranch_Click);
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "data.png");
            this.imageListTree.Images.SetKeyName(1, "cabinet.png");
            this.imageListTree.Images.SetKeyName(2, "table_sql.png");
            this.imageListTree.Images.SetKeyName(3, "table_view.png");
            this.imageListTree.Images.SetKeyName(4, "column.png");
            // 
            // progressDsRunning
            // 
            this.progressDsRunning.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressDsRunning.Location = new System.Drawing.Point(4, 631);
            this.progressDsRunning.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressDsRunning.Name = "progressDsRunning";
            this.progressDsRunning.Size = new System.Drawing.Size(475, 22);
            this.progressDsRunning.Step = 1;
            this.progressDsRunning.TabIndex = 5;
            this.progressDsRunning.Visible = false;
            // 
            // lblSearchTooltip
            // 
            this.lblSearchTooltip.AutoSize = true;
            this.lblSearchTooltip.BackColor = System.Drawing.SystemColors.Info;
            this.lblSearchTooltip.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSearchTooltip.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lblSearchTooltip.Location = new System.Drawing.Point(4, 234);
            this.lblSearchTooltip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTooltip.Name = "lblSearchTooltip";
            this.lblSearchTooltip.Size = new System.Drawing.Size(475, 80);
            this.lblSearchTooltip.TabIndex = 6;
            this.lblSearchTooltip.Text = "IN, BETWEEN, LIKES => multiple values separated by semicolon (;)\r\nIN, BETWEEN wit" +
    "h empty content => IS NULL\r\nSHIFT+CTRL+C or +V: convert \"\\r\\n\" <--> \";\" for clip" +
    "board";
            this.lblSearchTooltip.Visible = false;
            // 
            // listRunningTasks
            // 
            this.listRunningTasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listRunningTasks.FormattingEnabled = true;
            this.listRunningTasks.ItemHeight = 20;
            this.listRunningTasks.Location = new System.Drawing.Point(4, 537);
            this.listRunningTasks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listRunningTasks.Name = "listRunningTasks";
            this.listRunningTasks.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listRunningTasks.Size = new System.Drawing.Size(475, 84);
            this.listRunningTasks.TabIndex = 7;
            this.listRunningTasks.TabStop = false;
            this.listRunningTasks.Visible = false;
            // 
            // panelDsGo
            // 
            this.panelDsGo.Controls.Add(this.btnDsOpen);
            this.panelDsGo.Controls.Add(this.btnDsSave);
            this.panelDsGo.Controls.Add(this.btnDsGo);
            this.panelDsGo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDsGo.Location = new System.Drawing.Point(0, 314);
            this.panelDsGo.Margin = new System.Windows.Forms.Padding(0);
            this.panelDsGo.Name = "panelDsGo";
            this.panelDsGo.Size = new System.Drawing.Size(483, 46);
            this.panelDsGo.TabIndex = 8;
            this.panelDsGo.Resize += new System.EventHandler(this.panelDsGo_Resize);
            // 
            // btnDsGo
            // 
            this.btnDsGo.Location = new System.Drawing.Point(100, 5);
            this.btnDsGo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDsGo.Name = "btnDsGo";
            this.btnDsGo.Size = new System.Drawing.Size(369, 35);
            this.btnDsGo.TabIndex = 4;
            this.btnDsGo.Text = "Go";
            this.btnDsGo.UseVisualStyleBackColor = true;
            this.btnDsGo.Click += new System.EventHandler(this.btnDsGo_Click);
            // 
            // containerDataResult
            // 
            this.containerDataResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDataResult.Location = new System.Drawing.Point(0, 0);
            this.containerDataResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerDataResult.Name = "containerDataResult";
            this.containerDataResult.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // containerDataResult.Panel1
            // 
            this.containerDataResult.Panel1.Controls.Add(this.txtDsResultSQL);
            this.containerDataResult.Panel1.Controls.Add(this.containerDsResult);
            this.containerDataResult.Panel1.Controls.Add(this.bindingNavigatorDsResult);
            // 
            // containerDataResult.Panel2
            // 
            this.containerDataResult.Panel2.Controls.Add(this.tabDsResultOperations);
            this.containerDataResult.Size = new System.Drawing.Size(1073, 825);
            this.containerDataResult.SplitterDistance = 556;
            this.containerDataResult.SplitterWidth = 6;
            this.containerDataResult.TabIndex = 0;
            this.containerDataResult.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // containerDsResult
            // 
            this.containerDsResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDsResult.Location = new System.Drawing.Point(0, 34);
            this.containerDsResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerDsResult.Name = "containerDsResult";
            // 
            // containerDsResult.Panel1
            // 
            this.containerDsResult.Panel1.Controls.Add(this.dataGridViewDsResult);
            // 
            // containerDsResult.Panel2
            // 
            this.containerDsResult.Panel2.Controls.Add(this.txtDataField);
            this.containerDsResult.Size = new System.Drawing.Size(1073, 522);
            this.containerDsResult.SplitterDistance = 629;
            this.containerDsResult.SplitterWidth = 6;
            this.containerDsResult.TabIndex = 3;
            this.containerDsResult.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // dataGridViewDsResult
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.dataGridViewDsResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDsResult.AutoGenerateColumns = false;
            this.dataGridViewDsResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDsResult.DataSource = this.bindingSourceDsResult;
            this.dataGridViewDsResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDsResult.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewDsResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewDsResult.Name = "dataGridViewDsResult";
            this.dataGridViewDsResult.ReadOnly = true;
            this.dataGridViewDsResult.RowHeadersWidth = 62;
            this.dataGridViewDsResult.Size = new System.Drawing.Size(629, 522);
            this.dataGridViewDsResult.TabIndex = 1;
            this.dataGridViewDsResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewDsResult_CellFormatting);
            this.dataGridViewDsResult.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewDsResult_CellPainting);
            this.dataGridViewDsResult.SelectionChanged += new System.EventHandler(this.dataGridViewDsResult_SelectionChanged);
            // 
            // bindingNavigatorDsResult
            // 
            this.bindingNavigatorDsResult.AddNewItem = null;
            this.bindingNavigatorDsResult.BindingSource = this.bindingSourceDsResult;
            this.bindingNavigatorDsResult.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigatorDsResult.DeleteItem = null;
            this.bindingNavigatorDsResult.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.bindingNavigatorDsResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblDsResultTop,
            this.cbDsResultTop,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.btnShowImage,
            this.bindingNavigatorSeparator2,
            this.txtDsResultFilter,
            this.btnDsResultFilter,
            this.cbTextType,
            this.btnWordWrap,
            this.btnFormattedText,
            this.txtDsResultGenerateSQL,
            this.btnDsGridExport,
            this.lblDsResultTable});
            this.bindingNavigatorDsResult.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorDsResult.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorDsResult.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorDsResult.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorDsResult.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorDsResult.Name = "bindingNavigatorDsResult";
            this.bindingNavigatorDsResult.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.bindingNavigatorDsResult.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorDsResult.Size = new System.Drawing.Size(1073, 34);
            this.bindingNavigatorDsResult.TabIndex = 0;
            this.bindingNavigatorDsResult.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(54, 29);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // lblDsResultTop
            // 
            this.lblDsResultTop.Name = "lblDsResultTop";
            this.lblDsResultTop.Size = new System.Drawing.Size(48, 29);
            this.lblDsResultTop.Text = "TOP:";
            // 
            // cbDsResultTop
            // 
            this.cbDsResultTop.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbDsResultTop.Items.AddRange(new object[] {
            "0",
            "10",
            "100",
            "1000",
            "10000"});
            this.cbDsResultTop.Name = "cbDsResultTop";
            this.cbDsResultTop.Size = new System.Drawing.Size(110, 34);
            this.cbDsResultTop.Text = "100";
            this.cbDsResultTop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbDsResultTop_KeyUp);
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(73, 31);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // txtDsResultFilter
            // 
            this.txtDsResultFilter.Name = "txtDsResultFilter";
            this.txtDsResultFilter.Size = new System.Drawing.Size(300, 34);
            // 
            // cbTextType
            // 
            this.cbTextType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextType.Items.AddRange(new object[] {
            "Text",
            "XML",
            "HTML",
            "JS",
            "C#",
            "VB",
            "SQL",
            "PHP",
            "Lua"});
            this.cbTextType.Name = "cbTextType";
            this.cbTextType.Size = new System.Drawing.Size(121, 34);
            this.cbTextType.SelectedIndexChanged += new System.EventHandler(this.cbTextType_SelectedIndexChanged);
            // 
            // lblDsResultTable
            // 
            this.lblDsResultTable.Name = "lblDsResultTable";
            this.lblDsResultTable.Size = new System.Drawing.Size(19, 25);
            this.lblDsResultTable.Text = "-";
            this.lblDsResultTable.ToolTipText = "object name";
            // 
            // tabDsResultOperations
            // 
            this.tabDsResultOperations.Controls.Add(this.pageDsScriptWhere);
            this.tabDsResultOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDsResultOperations.Location = new System.Drawing.Point(0, 0);
            this.tabDsResultOperations.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabDsResultOperations.Name = "tabDsResultOperations";
            this.tabDsResultOperations.SelectedIndex = 0;
            this.tabDsResultOperations.Size = new System.Drawing.Size(1073, 263);
            this.tabDsResultOperations.TabIndex = 0;
            // 
            // pageDsScriptWhere
            // 
            this.pageDsScriptWhere.Controls.Add(this.containerDsScriptWhere);
            this.pageDsScriptWhere.Location = new System.Drawing.Point(4, 29);
            this.pageDsScriptWhere.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageDsScriptWhere.Name = "pageDsScriptWhere";
            this.pageDsScriptWhere.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageDsScriptWhere.Size = new System.Drawing.Size(1065, 230);
            this.pageDsScriptWhere.TabIndex = 0;
            this.pageDsScriptWhere.Text = "@Parameters";
            this.pageDsScriptWhere.UseVisualStyleBackColor = true;
            // 
            // containerDsScriptWhere
            // 
            this.containerDsScriptWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDsScriptWhere.Location = new System.Drawing.Point(4, 5);
            this.containerDsScriptWhere.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerDsScriptWhere.Name = "containerDsScriptWhere";
            // 
            // containerDsScriptWhere.Panel1
            // 
            this.containerDsScriptWhere.Panel1.Controls.Add(this.listDsScriptWhere);
            // 
            // containerDsScriptWhere.Panel2
            // 
            this.containerDsScriptWhere.Panel2.Controls.Add(this.txtDsScriptWhere);
            this.containerDsScriptWhere.Size = new System.Drawing.Size(1057, 220);
            this.containerDsScriptWhere.SplitterDistance = 252;
            this.containerDsScriptWhere.SplitterWidth = 6;
            this.containerDsScriptWhere.TabIndex = 0;
            this.containerDsScriptWhere.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // listDsScriptWhere
            // 
            this.listDsScriptWhere.CheckBoxes = true;
            this.listDsScriptWhere.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDsScriptWhereName,
            this.columnDsScriptWhereType});
            this.listDsScriptWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDsScriptWhere.FullRowSelect = true;
            this.listDsScriptWhere.HideSelection = false;
            this.listDsScriptWhere.Location = new System.Drawing.Point(0, 0);
            this.listDsScriptWhere.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listDsScriptWhere.Name = "listDsScriptWhere";
            this.listDsScriptWhere.Size = new System.Drawing.Size(252, 220);
            this.listDsScriptWhere.TabIndex = 0;
            this.listDsScriptWhere.UseCompatibleStateImageBehavior = false;
            this.listDsScriptWhere.View = System.Windows.Forms.View.Details;
            this.listDsScriptWhere.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listDsScriptWhere_ItemChecked);
            // 
            // columnDsScriptWhereName
            // 
            this.columnDsScriptWhereName.Text = "Name";
            this.columnDsScriptWhereName.Width = 136;
            // 
            // columnDsScriptWhereType
            // 
            this.columnDsScriptWhereType.Text = "Type";
            this.columnDsScriptWhereType.Width = 100;
            // 
            // pageObjects
            // 
            this.pageObjects.Controls.Add(this.containerDbobjects);
            this.pageObjects.Location = new System.Drawing.Point(4, 29);
            this.pageObjects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageObjects.Name = "pageObjects";
            this.pageObjects.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageObjects.Size = new System.Drawing.Size(1570, 835);
            this.pageObjects.TabIndex = 1;
            this.pageObjects.Text = "Dbobjects";
            this.pageObjects.UseVisualStyleBackColor = true;
            // 
            // containerDbobjects
            // 
            this.containerDbobjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerDbobjects.Location = new System.Drawing.Point(4, 5);
            this.containerDbobjects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.containerDbobjects.Name = "containerDbobjects";
            // 
            // containerDbobjects.Panel1
            // 
            this.containerDbobjects.Panel1.Controls.Add(this.panObjects);
            // 
            // containerDbobjects.Panel2
            // 
            this.containerDbobjects.Panel2.Controls.Add(this.dataViewCurrentObject);
            this.containerDbobjects.Size = new System.Drawing.Size(1562, 825);
            this.containerDbobjects.SplitterDistance = 520;
            this.containerDbobjects.SplitterWidth = 6;
            this.containerDbobjects.TabIndex = 0;
            this.containerDbobjects.Paint += new System.Windows.Forms.PaintEventHandler(this.paintSplitter);
            // 
            // panObjects
            // 
            this.panObjects.ColumnCount = 3;
            this.panObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.panObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panObjects.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panObjects.Controls.Add(this.cbObjectsDB, 0, 0);
            this.panObjects.Controls.Add(this.txtObjFilter, 1, 0);
            this.panObjects.Controls.Add(this.btnObjFilter, 2, 0);
            this.panObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panObjects.Location = new System.Drawing.Point(0, 0);
            this.panObjects.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panObjects.Name = "panObjects";
            this.panObjects.RowCount = 2;
            this.panObjects.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panObjects.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panObjects.Size = new System.Drawing.Size(520, 825);
            this.panObjects.TabIndex = 0;
            // 
            // cbObjectsDB
            // 
            this.cbObjectsDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbObjectsDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbObjectsDB.DropDownWidth = 200;
            this.cbObjectsDB.FormattingEnabled = true;
            this.cbObjectsDB.Location = new System.Drawing.Point(4, 5);
            this.cbObjectsDB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbObjectsDB.Name = "cbObjectsDB";
            this.cbObjectsDB.Size = new System.Drawing.Size(172, 28);
            this.cbObjectsDB.TabIndex = 2;
            // 
            // txtObjFilter
            // 
            this.txtObjFilter.ContextMenuStrip = this.menuObjFilter;
            this.txtObjFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtObjFilter.Location = new System.Drawing.Point(184, 5);
            this.txtObjFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtObjFilter.Name = "txtObjFilter";
            this.txtObjFilter.Size = new System.Drawing.Size(278, 28);
            this.txtObjFilter.TabIndex = 0;
            this.txtObjFilter.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtObjFilter_KeyUp);
            // 
            // menuObjFilter
            // 
            this.menuObjFilter.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuObjFilter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuObjFilterNoFilter,
            this.mnuObjFilterNamesSearch,
            this.mnuObjFilterFulltextSearch});
            this.menuObjFilter.Name = "menuObjFilter";
            this.menuObjFilter.Size = new System.Drawing.Size(284, 100);
            this.menuObjFilter.Opening += new System.ComponentModel.CancelEventHandler(this.menuObjFilter_Opening);
            // 
            // mnuObjFilterNoFilter
            // 
            this.mnuObjFilterNoFilter.Name = "mnuObjFilterNoFilter";
            this.mnuObjFilterNoFilter.Size = new System.Drawing.Size(283, 32);
            this.mnuObjFilterNoFilter.Text = "No Filter";
            this.mnuObjFilterNoFilter.Click += new System.EventHandler(this.mnuObjFilterNoFilter_Click);
            // 
            // mnuObjFilterNamesSearch
            // 
            this.mnuObjFilterNamesSearch.Name = "mnuObjFilterNamesSearch";
            this.mnuObjFilterNamesSearch.ShortcutKeyDisplayString = "Enter";
            this.mnuObjFilterNamesSearch.Size = new System.Drawing.Size(283, 32);
            this.mnuObjFilterNamesSearch.Text = "Names Search";
            this.mnuObjFilterNamesSearch.Click += new System.EventHandler(this.mnuObjFilterNamesSearch_Click);
            // 
            // mnuObjFilterFulltextSearch
            // 
            this.mnuObjFilterFulltextSearch.Name = "mnuObjFilterFulltextSearch";
            this.mnuObjFilterFulltextSearch.ShortcutKeyDisplayString = "Alt+Enter";
            this.mnuObjFilterFulltextSearch.Size = new System.Drawing.Size(283, 32);
            this.mnuObjFilterFulltextSearch.Text = "Fulltext Search";
            this.mnuObjFilterFulltextSearch.Click += new System.EventHandler(this.mnuObjFilterFulltextSearch_Click);
            // 
            // btnObjFilter
            // 
            this.btnObjFilter.Location = new System.Drawing.Point(470, 5);
            this.btnObjFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnObjFilter.Name = "btnObjFilter";
            this.btnObjFilter.Size = new System.Drawing.Size(46, 35);
            this.btnObjFilter.TabIndex = 1;
            this.btnObjFilter.Text = ">";
            this.btnObjFilter.UseVisualStyleBackColor = true;
            this.btnObjFilter.Click += new System.EventHandler(this.btnObjFilter_Click);
            // 
            // menuObjScriptText
            // 
            this.menuObjScriptText.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuObjScriptText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScriptTextSearch,
            this.mnuScriptAppendToQueryBox,
            this.toolStripMenuItem1,
            this.mnuScriptTextWordWrap,
            this.mnuScriptTextDblClick,
            this.mnuScriptTextShowWindow2});
            this.menuObjScriptText.Name = "menuObjScriptText";
            this.menuObjScriptText.Size = new System.Drawing.Size(330, 170);
            this.menuObjScriptText.Opening += new System.ComponentModel.CancelEventHandler(this.menuObjScriptText_Opening);
            // 
            // mnuScriptTextSearch
            // 
            this.mnuScriptTextSearch.Name = "mnuScriptTextSearch";
            this.mnuScriptTextSearch.Size = new System.Drawing.Size(329, 32);
            this.mnuScriptTextSearch.Text = "mnuScriptTextSearch";
            this.mnuScriptTextSearch.Click += new System.EventHandler(this.mnuScriptTextSearch_Click);
            // 
            // mnuScriptAppendToQueryBox
            // 
            this.mnuScriptAppendToQueryBox.Name = "mnuScriptAppendToQueryBox";
            this.mnuScriptAppendToQueryBox.Size = new System.Drawing.Size(329, 32);
            this.mnuScriptAppendToQueryBox.Text = "mnuScriptAppendToQueryBox";
            this.mnuScriptAppendToQueryBox.Click += new System.EventHandler(this.mnuScriptAppendToQueryBox_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(326, 6);
            // 
            // mnuScriptTextWordWrap
            // 
            this.mnuScriptTextWordWrap.Checked = true;
            this.mnuScriptTextWordWrap.CheckOnClick = true;
            this.mnuScriptTextWordWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuScriptTextWordWrap.Name = "mnuScriptTextWordWrap";
            this.mnuScriptTextWordWrap.Size = new System.Drawing.Size(329, 32);
            this.mnuScriptTextWordWrap.Text = "Word Wrap";
            this.mnuScriptTextWordWrap.Click += new System.EventHandler(this.mnuScriptTextWordWrap_Click);
            // 
            // mnuScriptTextDblClick
            // 
            this.mnuScriptTextDblClick.CheckOnClick = true;
            this.mnuScriptTextDblClick.Name = "mnuScriptTextDblClick";
            this.mnuScriptTextDblClick.Size = new System.Drawing.Size(329, 32);
            this.mnuScriptTextDblClick.Text = "DoubleClick to show window 2";
            // 
            // mnuScriptTextShowWindow2
            // 
            this.mnuScriptTextShowWindow2.CheckOnClick = true;
            this.mnuScriptTextShowWindow2.Name = "mnuScriptTextShowWindow2";
            this.mnuScriptTextShowWindow2.Size = new System.Drawing.Size(329, 32);
            this.mnuScriptTextShowWindow2.Text = "Show script window 2";
            this.mnuScriptTextShowWindow2.Click += new System.EventHandler(this.mnuScriptTextShowWindow2_Click);
            // 
            // pageProfiler
            // 
            this.pageProfiler.Controls.Add(this.sqlProfiler);
            this.pageProfiler.Location = new System.Drawing.Point(4, 29);
            this.pageProfiler.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageProfiler.Name = "pageProfiler";
            this.pageProfiler.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pageProfiler.Size = new System.Drawing.Size(1570, 835);
            this.pageProfiler.TabIndex = 2;
            this.pageProfiler.Text = "Profiler";
            this.pageProfiler.UseVisualStyleBackColor = true;
            // 
            // ilObjects
            // 
            this.ilObjects.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilObjects.ImageStream")));
            this.ilObjects.TransparentColor = System.Drawing.Color.Transparent;
            this.ilObjects.Images.SetKeyName(0, "Table");
            this.ilObjects.Images.SetKeyName(1, "View");
            this.ilObjects.Images.SetKeyName(2, "Procedure");
            this.ilObjects.Images.SetKeyName(3, "Function");
            this.ilObjects.Images.SetKeyName(4, "Synonym");
            this.ilObjects.Images.SetKeyName(5, "ObjectType");
            this.ilObjects.Images.SetKeyName(6, "Schema");
            this.ilObjects.Images.SetKeyName(7, "Database");
            // 
            // menuObjectTreeItem
            // 
            this.menuObjectTreeItem.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuObjectTreeItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuObjDBPrefix,
            this.mnuObjToClipboard,
            this.mnuObjNoBraceToClipboard,
            this.mnuObjToQuery,
            this.mnuObjAppendToQuery,
            this.mnuObjExpandAll,
            this.mnuObjCollapseAll});
            this.menuObjectTreeItem.Name = "objectMenu";
            this.menuObjectTreeItem.Size = new System.Drawing.Size(247, 228);
            this.menuObjectTreeItem.Opening += new System.ComponentModel.CancelEventHandler(this.menuObjectTreeItem_Opening);
            // 
            // mnuObjDBPrefix
            // 
            this.mnuObjDBPrefix.CheckOnClick = true;
            this.mnuObjDBPrefix.Name = "mnuObjDBPrefix";
            this.mnuObjDBPrefix.Size = new System.Drawing.Size(246, 32);
            this.mnuObjDBPrefix.Text = "DB Prefix";
            // 
            // mnuObjToClipboard
            // 
            this.mnuObjToClipboard.Name = "mnuObjToClipboard";
            this.mnuObjToClipboard.Size = new System.Drawing.Size(246, 32);
            this.mnuObjToClipboard.Text = "To Clipboard";
            this.mnuObjToClipboard.Click += new System.EventHandler(this.mnuObjToClipboard_Click);
            // 
            // mnuObjNoBraceToClipboard
            // 
            this.mnuObjNoBraceToClipboard.Name = "mnuObjNoBraceToClipboard";
            this.mnuObjNoBraceToClipboard.Size = new System.Drawing.Size(246, 32);
            this.mnuObjNoBraceToClipboard.Text = "NoBraceToClipboard";
            this.mnuObjNoBraceToClipboard.Click += new System.EventHandler(this.mnuObjNoBraceToClipboard_Click);
            // 
            // mnuObjToQuery
            // 
            this.mnuObjToQuery.Name = "mnuObjToQuery";
            this.mnuObjToQuery.Size = new System.Drawing.Size(246, 32);
            this.mnuObjToQuery.Text = "To Query";
            this.mnuObjToQuery.Click += new System.EventHandler(this.mnuObjToQuery_Click);
            // 
            // mnuObjAppendToQuery
            // 
            this.mnuObjAppendToQuery.Name = "mnuObjAppendToQuery";
            this.mnuObjAppendToQuery.Size = new System.Drawing.Size(246, 32);
            this.mnuObjAppendToQuery.Text = "AppendToQuery";
            this.mnuObjAppendToQuery.Click += new System.EventHandler(this.mnuObjAppendToQuery_Click);
            // 
            // mnuObjExpandAll
            // 
            this.mnuObjExpandAll.Name = "mnuObjExpandAll";
            this.mnuObjExpandAll.Size = new System.Drawing.Size(246, 32);
            this.mnuObjExpandAll.Text = "Expand All";
            this.mnuObjExpandAll.Click += new System.EventHandler(this.mnuObjExpandAll_Click);
            // 
            // mnuObjCollapseAll
            // 
            this.mnuObjCollapseAll.Name = "mnuObjCollapseAll";
            this.mnuObjCollapseAll.Size = new System.Drawing.Size(246, 32);
            this.mnuObjCollapseAll.Text = "Collapse All But This";
            this.mnuObjCollapseAll.Click += new System.EventHandler(this.mnuObjCollapseAll_Click);
            // 
            // txtSQL
            // 
            this.txtSQL.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtSQL.AutoIndentCharsPatterns = "";
            this.txtSQL.AutoScrollMinSize = new System.Drawing.Size(35, 22);
            this.txtSQL.BackBrush = null;
            this.txtSQL.CharHeight = 22;
            this.txtSQL.CharWidth = 12;
            this.txtSQL.CommentPrefix = "--";
            this.txtSQL.ContextMenuStrip = this.contextMenuQueryEditor;
            this.txtSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.IsReplaceMode = false;
            this.txtSQL.Language = FastColoredTextBoxNS.Language.SQL;
            this.txtSQL.LeftBracket = '(';
            this.txtSQL.Location = new System.Drawing.Point(0, 0);
            this.txtSQL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Paddings = new System.Windows.Forms.Padding(0);
            this.txtSQL.RightBracket = ')';
            this.txtSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSQL.Size = new System.Drawing.Size(1562, 384);
            this.txtSQL.TabIndex = 0;
            this.txtSQL.Zoom = 100;
            this.txtSQL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSQL_KeyUp);
            // 
            // dataViewQuery
            // 
            this.dataViewQuery.ConnectionString = null;
            this.dataViewQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataViewQuery.FieldDescriptions = null;
            this.dataViewQuery.FieldKeys = null;
            this.dataViewQuery.FulltextSearch = null;
            this.dataViewQuery.Location = new System.Drawing.Point(0, 0);
            this.dataViewQuery.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dataViewQuery.Name = "dataViewQuery";
            this.dataViewQuery.ScriptContextMenu = null;
            this.dataViewQuery.Size = new System.Drawing.Size(1562, 387);
            this.dataViewQuery.TabIndex = 5;
            this.dataViewQuery.TableIndex = -1;
            // 
            // txtDsResultSQL
            // 
            this.txtDsResultSQL.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtDsResultSQL.AutoIndentCharsPatterns = "";
            this.txtDsResultSQL.AutoScrollMinSize = new System.Drawing.Size(35, 22);
            this.txtDsResultSQL.BackBrush = null;
            this.txtDsResultSQL.CharHeight = 22;
            this.txtDsResultSQL.CharWidth = 12;
            this.txtDsResultSQL.CommentPrefix = "--";
            this.txtDsResultSQL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDsResultSQL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtDsResultSQL.IsReplaceMode = false;
            this.txtDsResultSQL.Language = FastColoredTextBoxNS.Language.SQL;
            this.txtDsResultSQL.LeftBracket = '(';
            this.txtDsResultSQL.Location = new System.Drawing.Point(141, 203);
            this.txtDsResultSQL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDsResultSQL.Name = "txtDsResultSQL";
            this.txtDsResultSQL.Paddings = new System.Windows.Forms.Padding(0);
            this.txtDsResultSQL.RightBracket = ')';
            this.txtDsResultSQL.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtDsResultSQL.Size = new System.Drawing.Size(792, 211);
            this.txtDsResultSQL.TabIndex = 2;
            this.txtDsResultSQL.Visible = false;
            this.txtDsResultSQL.Zoom = 100;
            // 
            // txtDataField
            // 
            this.txtDataField.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtDataField.AutoScrollMinSize = new System.Drawing.Size(35, 22);
            this.txtDataField.BackBrush = null;
            this.txtDataField.CharHeight = 22;
            this.txtDataField.CharWidth = 12;
            this.txtDataField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDataField.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtDataField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDataField.IsReplaceMode = false;
            this.txtDataField.Location = new System.Drawing.Point(0, 0);
            this.txtDataField.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDataField.Name = "txtDataField";
            this.txtDataField.Paddings = new System.Windows.Forms.Padding(0);
            this.txtDataField.ReadOnly = true;
            this.txtDataField.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtDataField.Size = new System.Drawing.Size(438, 522);
            this.txtDataField.TabIndex = 1;
            this.txtDataField.Zoom = 100;
            // 
            // txtDsScriptWhere
            // 
            this.txtDsScriptWhere.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtDsScriptWhere.AutoIndentCharsPatterns = "";
            this.txtDsScriptWhere.AutoScrollMinSize = new System.Drawing.Size(35, 22);
            this.txtDsScriptWhere.BackBrush = null;
            this.txtDsScriptWhere.CharHeight = 22;
            this.txtDsScriptWhere.CharWidth = 12;
            this.txtDsScriptWhere.CommentPrefix = "--";
            this.txtDsScriptWhere.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDsScriptWhere.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtDsScriptWhere.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDsScriptWhere.IsReplaceMode = false;
            this.txtDsScriptWhere.Language = FastColoredTextBoxNS.Language.SQL;
            this.txtDsScriptWhere.LeftBracket = '(';
            this.txtDsScriptWhere.Location = new System.Drawing.Point(0, 0);
            this.txtDsScriptWhere.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDsScriptWhere.Name = "txtDsScriptWhere";
            this.txtDsScriptWhere.Paddings = new System.Windows.Forms.Padding(0);
            this.txtDsScriptWhere.RightBracket = ')';
            this.txtDsScriptWhere.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtDsScriptWhere.Size = new System.Drawing.Size(799, 220);
            this.txtDsScriptWhere.TabIndex = 0;
            this.txtDsScriptWhere.Zoom = 100;
            // 
            // dataViewCurrentObject
            // 
            this.dataViewCurrentObject.ConnectionString = null;
            this.dataViewCurrentObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataViewCurrentObject.FieldDescriptions = null;
            this.dataViewCurrentObject.FieldKeys = null;
            this.dataViewCurrentObject.FulltextSearch = null;
            this.dataViewCurrentObject.Location = new System.Drawing.Point(0, 0);
            this.dataViewCurrentObject.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dataViewCurrentObject.Name = "dataViewCurrentObject";
            this.dataViewCurrentObject.ScriptContextMenu = this.menuObjScriptText;
            this.dataViewCurrentObject.Size = new System.Drawing.Size(1036, 825);
            this.dataViewCurrentObject.TabIndex = 0;
            this.dataViewCurrentObject.TableIndex = -1;
            this.dataViewCurrentObject.ScriptEditorEnter += new System.EventHandler(this.ScriptEditor_Enter);
            // 
            // sqlProfiler
            // 
            this.sqlProfiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlProfiler.Location = new System.Drawing.Point(4, 5);
            this.sqlProfiler.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.sqlProfiler.Name = "sqlProfiler";
            this.sqlProfiler.QuerySeparator = ";\\r\\n";
            this.sqlProfiler.Size = new System.Drawing.Size(1562, 825);
            this.sqlProfiler.TabIndex = 0;
            // 
            // btnDsOpen
            // 
            this.btnDsOpen.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDsOpen.Image = global::DBMapper.Properties.Resources.document_find;
            this.btnDsOpen.Location = new System.Drawing.Point(4, 5);
            this.btnDsOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDsOpen.Name = "btnDsOpen";
            this.btnDsOpen.Size = new System.Drawing.Size(40, 35);
            this.btnDsOpen.TabIndex = 0;
            this.btnDsOpen.UseVisualStyleBackColor = true;
            this.btnDsOpen.Click += new System.EventHandler(this.btnDsOpen_Click);
            // 
            // btnDsSave
            // 
            this.btnDsSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDsSave.Image = global::DBMapper.Properties.Resources.disk_blue;
            this.btnDsSave.Location = new System.Drawing.Point(52, 5);
            this.btnDsSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDsSave.Name = "btnDsSave";
            this.btnDsSave.Size = new System.Drawing.Size(40, 35);
            this.btnDsSave.TabIndex = 1;
            this.btnDsSave.UseVisualStyleBackColor = true;
            this.btnDsSave.Click += new System.EventHandler(this.btnDsSave_Click);
            // 
            // bindingSourceDsResult
            // 
            this.bindingSourceDsResult.AllowNew = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(34, 29);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(34, 29);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(34, 29);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(34, 29);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // btnShowImage
            // 
            this.btnShowImage.CheckOnClick = true;
            this.btnShowImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowImage.Image = ((System.Drawing.Image)(resources.GetObject("btnShowImage.Image")));
            this.btnShowImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowImage.Name = "btnShowImage";
            this.btnShowImage.Size = new System.Drawing.Size(34, 29);
            this.btnShowImage.Text = "show image";
            this.btnShowImage.ToolTipText = "show image or byte[]";
            this.btnShowImage.CheckedChanged += new System.EventHandler(this.btnShowImage_CheckedChanged);
            // 
            // btnDsResultFilter
            // 
            this.btnDsResultFilter.Image = global::DBMapper.Properties.Resources.funnel;
            this.btnDsResultFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDsResultFilter.Name = "btnDsResultFilter";
            this.btnDsResultFilter.Size = new System.Drawing.Size(78, 29);
            this.btnDsResultFilter.Text = "Filter";
            this.btnDsResultFilter.Click += new System.EventHandler(this.btnDsResultFilter_Click);
            // 
            // btnWordWrap
            // 
            this.btnWordWrap.CheckOnClick = true;
            this.btnWordWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnWordWrap.Image = ((System.Drawing.Image)(resources.GetObject("btnWordWrap.Image")));
            this.btnWordWrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWordWrap.Name = "btnWordWrap";
            this.btnWordWrap.Size = new System.Drawing.Size(103, 29);
            this.btnWordWrap.Text = "WordWrap";
            this.btnWordWrap.Click += new System.EventHandler(this.btnWordWrap_Click);
            // 
            // btnFormattedText
            // 
            this.btnFormattedText.CheckOnClick = true;
            this.btnFormattedText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFormattedText.Image = ((System.Drawing.Image)(resources.GetObject("btnFormattedText.Image")));
            this.btnFormattedText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFormattedText.Name = "btnFormattedText";
            this.btnFormattedText.Size = new System.Drawing.Size(129, 29);
            this.btnFormattedText.Text = "FormattedText";
            // 
            // txtDsResultGenerateSQL
            // 
            this.txtDsResultGenerateSQL.CheckOnClick = true;
            this.txtDsResultGenerateSQL.Image = global::DBMapper.Properties.Resources.text_code;
            this.txtDsResultGenerateSQL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtDsResultGenerateSQL.Name = "txtDsResultGenerateSQL";
            this.txtDsResultGenerateSQL.Size = new System.Drawing.Size(72, 29);
            this.txtDsResultGenerateSQL.Text = "SQL";
            this.txtDsResultGenerateSQL.Click += new System.EventHandler(this.txtDsResultGenerateSQL_Click);
            // 
            // btnDsGridExport
            // 
            this.btnDsGridExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDsGridExport.Image = global::DBMapper.Properties.Resources.document_out;
            this.btnDsGridExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDsGridExport.Name = "btnDsGridExport";
            this.btnDsGridExport.Size = new System.Drawing.Size(34, 28);
            this.btnDsGridExport.Text = "Export";
            this.btnDsGridExport.ToolTipText = "Export to Excel";
            this.btnDsGridExport.Click += new System.EventHandler(this.btnDsGridExport_Click);
            // 
            // FrmDBMapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 868);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmDBMapper";
            this.Text = "DBMapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDBMapper_FormClosing);
            this.Load += new System.EventHandler(this.FrmObjectCreator_Load);
            this.Shown += new System.EventHandler(this.FrmDBMapper_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDBMapper_KeyUp);
            this.tabMain.ResumeLayout(false);
            this.pageQuery.ResumeLayout(false);
            this.pageQuery.PerformLayout();
            this.splitContainerQuery.Panel1.ResumeLayout(false);
            this.splitContainerQuery.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerQuery)).EndInit();
            this.splitContainerQuery.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pageDataSearch.ResumeLayout(false);
            this.containerData.Panel1.ResumeLayout(false);
            this.containerData.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerData)).EndInit();
            this.containerData.ResumeLayout(false);
            this.containerDataCockpit.Panel1.ResumeLayout(false);
            this.containerDataCockpit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerDataCockpit)).EndInit();
            this.containerDataCockpit.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menuTreeDsResult.ResumeLayout(false);
            this.panelDsGo.ResumeLayout(false);
            this.containerDataResult.Panel1.ResumeLayout(false);
            this.containerDataResult.Panel1.PerformLayout();
            this.containerDataResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerDataResult)).EndInit();
            this.containerDataResult.ResumeLayout(false);
            this.containerDsResult.Panel1.ResumeLayout(false);
            this.containerDsResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerDsResult)).EndInit();
            this.containerDsResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDsResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorDsResult)).EndInit();
            this.bindingNavigatorDsResult.ResumeLayout(false);
            this.bindingNavigatorDsResult.PerformLayout();
            this.tabDsResultOperations.ResumeLayout(false);
            this.pageDsScriptWhere.ResumeLayout(false);
            this.containerDsScriptWhere.Panel1.ResumeLayout(false);
            this.containerDsScriptWhere.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerDsScriptWhere)).EndInit();
            this.containerDsScriptWhere.ResumeLayout(false);
            this.pageObjects.ResumeLayout(false);
            this.containerDbobjects.Panel1.ResumeLayout(false);
            this.containerDbobjects.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.containerDbobjects)).EndInit();
            this.containerDbobjects.ResumeLayout(false);
            this.panObjects.ResumeLayout(false);
            this.menuObjFilter.ResumeLayout(false);
            this.menuObjScriptText.ResumeLayout(false);
            this.pageProfiler.ResumeLayout(false);
            this.menuObjectTreeItem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsResultSQL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDsScriptWhere)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDsResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtSQL;
        private System.Windows.Forms.Button Query;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pageQuery;
        private System.Windows.Forms.TabPage pageObjects;
        private System.Windows.Forms.SplitContainer containerDbobjects;
        private System.Windows.Forms.ImageList ilObjects;
        private System.Windows.Forms.TableLayoutPanel panObjects;
        private System.Windows.Forms.ComboBox txtObjFilter;
        private System.Windows.Forms.Button btnObjFilter;
        private DataObjectView dataViewQuery;
        private DataObjectView dataViewCurrentObject;
        private System.Windows.Forms.ContextMenuStrip menuObjectTreeItem;
        private System.Windows.Forms.ToolStripMenuItem mnuObjToClipboard;
        private System.Windows.Forms.ToolStripMenuItem mnuObjToQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuObjNoBraceToClipboard;
        private System.Windows.Forms.ContextMenuStrip menuObjScriptText;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptTextSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptTextWordWrap;
        private System.Windows.Forms.ComboBox cbDatabases;
        private System.Windows.Forms.ComboBox cbObjectsDB;
        private System.Windows.Forms.ToolStripMenuItem mnuObjExpandAll;
        private System.Windows.Forms.ToolStripMenuItem mnuObjCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem mnuObjDBPrefix;
        private System.Windows.Forms.Button btnNewInstance;
        private System.Windows.Forms.ContextMenuStrip menuObjFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuObjFilterNoFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuObjFilterNamesSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuObjFilterFulltextSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptTextDblClick;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptTextShowWindow2;
        private System.Windows.Forms.ToolStripMenuItem mnuObjAppendToQuery;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptAppendToQueryBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuQueryEditor;
        private System.Windows.Forms.TabPage pageProfiler;
        private ExpressProfiler.SQLProfiler sqlProfiler;
        private System.Windows.Forms.SplitContainer splitContainerQuery;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnADODB;
        private System.Windows.Forms.TabPage pageDataSearch;
        private System.Windows.Forms.SplitContainer containerData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listDsDb;
        private System.Windows.Forms.CheckBox chkDsViews;
        private System.Windows.Forms.CheckBox chkDsTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cbDsContent;
        private System.Windows.Forms.ComboBox cbDsColType;
        private System.Windows.Forms.ComboBox cbDsColName;
        private System.Windows.Forms.ComboBox cbDsObject;
        private System.Windows.Forms.ComboBox cbDsValueContent;
        private System.Windows.Forms.ComboBox cbDsValueColType;
        private System.Windows.Forms.ComboBox cbDsValueColName;
        private System.Windows.Forms.ComboBox cbDsValueObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDsValueSchema;
        private System.Windows.Forms.ComboBox cbDsSchema;
        private System.Windows.Forms.TreeView treeDsResult;
        private System.Windows.Forms.SplitContainer containerDataResult;
        private System.Windows.Forms.SplitContainer containerDataCockpit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDsNOTSchema;
        private System.Windows.Forms.CheckBox chkDsNOTObject;
        private System.Windows.Forms.CheckBox chkDsNOTColName;
        private System.Windows.Forms.CheckBox chkDsNOTColType;
        private System.Windows.Forms.CheckBox chkDsNOTContent;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.DataGridView dataGridViewDsResult;
        private System.Windows.Forms.BindingSource bindingSourceDsResult;
        private System.Windows.Forms.BindingNavigator bindingNavigatorDsResult;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripComboBox txtDsResultFilter;
        private System.Windows.Forms.ToolStripButton btnDsResultFilter;
        private System.Windows.Forms.ToolStripLabel lblDsResultTop;
        private System.Windows.Forms.ToolStripComboBox cbDsResultTop;
        private System.Windows.Forms.ProgressBar progressDsRunning;
        private System.Windows.Forms.Label lblRunningCount;
        private System.Windows.Forms.ToolStripButton btnShowImage;
        private System.Windows.Forms.Label lblSearchTooltip;
        private System.Windows.Forms.ListBox listRunningTasks;
        private System.Windows.Forms.ToolStripButton txtDsResultGenerateSQL;
        private System.Windows.Forms.TabControl tabDsResultOperations;
        private System.Windows.Forms.TabPage pageDsScriptWhere;
        private System.Windows.Forms.SplitContainer containerDsScriptWhere;
        private FastColoredTextBoxNS.FastColoredTextBox txtDsScriptWhere;
        private System.Windows.Forms.ListView listDsScriptWhere;
        private System.Windows.Forms.ColumnHeader columnDsScriptWhereName;
        private System.Windows.Forms.ColumnHeader columnDsScriptWhereType;
        private System.Windows.Forms.ContextMenuStrip menuTreeDsResult;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeDsResult2Excel;
        private System.Windows.Forms.ToolStripButton btnDsGridExport;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeDsResultCopySelectedBranch;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeDsResultCopySelectedTables;
        private System.Windows.Forms.ToolStripLabel lblDsResultTable;
        private FastColoredTextBoxNS.FastColoredTextBox txtDsResultSQL;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeDsResultData2Excel;
        private System.Windows.Forms.FlowLayoutPanel panelDsGo;
        private System.Windows.Forms.Button btnDsOpen;
        private System.Windows.Forms.Button btnDsSave;
        private System.Windows.Forms.Button btnDsGo;
        private System.Windows.Forms.CheckBox chkLoginMappingsOnly;
        private System.Windows.Forms.ComboBox cbConnections;
        private System.Windows.Forms.SplitContainer containerDsResult;
        private FastColoredTextBoxNS.FastColoredTextBox txtDataField;
        private System.Windows.Forms.ToolStripComboBox cbTextType;
        private System.Windows.Forms.ToolStripButton btnWordWrap;
        private System.Windows.Forms.ToolStripButton btnFormattedText;
    }
}

