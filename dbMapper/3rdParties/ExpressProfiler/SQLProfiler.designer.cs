namespace ExpressProfiler
{
    partial class SQLProfiler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLProfiler));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slEPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbScroll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnQuickFilter = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuQuickFilterDb = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterDbAny = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterLoginAny = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterLoginCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterLoginSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterHost = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterHostAny = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuickFilterHostCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.notSpresetconnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbStart = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRun = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRunWithFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPause = new System.Windows.Forms.ToolStripButton();
            this.tbStop = new System.Windows.Forms.ToolStripButton();
            this.tbStayOnTop = new System.Windows.Forms.ToolStripButton();
            this.tbTransparent = new System.Windows.Forms.ToolStripButton();
            this.btnHotkeyActive = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStripTextData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToXlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbQuerySeparator = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.reTextData = new FastColoredTextBoxNS.FastColoredTextBox();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripTextData.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.reTextData)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slEPS});
            this.statusStrip1.Location = new System.Drawing.Point(0, 488);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(979, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slEPS
            // 
            this.slEPS.Name = "slEPS";
            this.slEPS.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbClear,
            this.toolStripSeparator5,
            this.tbScroll,
            this.toolStripSeparator1,
            this.btnQuickFilter,
            this.tbStart,
            this.tbPause,
            this.tbStop,
            this.tbStayOnTop,
            this.tbTransparent,
            this.btnHotkeyActive,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cbQuerySeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(979, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbClear
            // 
            this.tbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbClear.Image = global::DBMapper.Properties.Resources.imClear;
            this.tbClear.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbClear.Name = "tbClear";
            this.tbClear.Size = new System.Drawing.Size(23, 22);
            this.tbClear.Text = "Clear trace";
            this.tbClear.ToolTipText = "Clear trace\r\nCtrl+Shift+Del";
            this.tbClear.Click += new System.EventHandler(this.tbClear_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tbScroll
            // 
            this.tbScroll.Checked = true;
            this.tbScroll.CheckOnClick = true;
            this.tbScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbScroll.Image = global::DBMapper.Properties.Resources.imScroll;
            this.tbScroll.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbScroll.Name = "tbScroll";
            this.tbScroll.Size = new System.Drawing.Size(23, 22);
            this.tbScroll.Text = "Auto scroll window";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnQuickFilter
            // 
            this.btnQuickFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuickFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuickFilterDb,
            this.mnuQuickFilterLogin,
            this.mnuQuickFilterHost,
            this.notSpresetconnectionToolStripMenuItem});
            this.btnQuickFilter.Image = global::DBMapper.Properties.Resources.preferences;
            this.btnQuickFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuickFilter.Name = "btnQuickFilter";
            this.btnQuickFilter.Size = new System.Drawing.Size(32, 22);
            this.btnQuickFilter.Text = "Set quick filter";
            this.btnQuickFilter.DropDownOpening += new System.EventHandler(this.btnQuickFilter_DropDownOpening);
            this.btnQuickFilter.Click += new System.EventHandler(this.btnQuickFilter_Click);
            // 
            // mnuQuickFilterDb
            // 
            this.mnuQuickFilterDb.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuickFilterDbAny});
            this.mnuQuickFilterDb.Name = "mnuQuickFilterDb";
            this.mnuQuickFilterDb.Size = new System.Drawing.Size(204, 22);
            this.mnuQuickFilterDb.Text = "DB name like";
            // 
            // mnuQuickFilterDbAny
            // 
            this.mnuQuickFilterDbAny.Name = "mnuQuickFilterDbAny";
            this.mnuQuickFilterDbAny.Size = new System.Drawing.Size(121, 22);
            this.mnuQuickFilterDbAny.Text = "-- Any --";
            this.mnuQuickFilterDbAny.Click += new System.EventHandler(this.mnuQuickFilterDbAny_Click);
            // 
            // mnuQuickFilterLogin
            // 
            this.mnuQuickFilterLogin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuickFilterLoginAny,
            this.mnuQuickFilterLoginCurrent,
            this.mnuQuickFilterLoginSystem});
            this.mnuQuickFilterLogin.Name = "mnuQuickFilterLogin";
            this.mnuQuickFilterLogin.Size = new System.Drawing.Size(204, 22);
            this.mnuQuickFilterLogin.Text = "LoginName like";
            // 
            // mnuQuickFilterLoginAny
            // 
            this.mnuQuickFilterLoginAny.Name = "mnuQuickFilterLoginAny";
            this.mnuQuickFilterLoginAny.Size = new System.Drawing.Size(135, 22);
            this.mnuQuickFilterLoginAny.Text = "-- Any --";
            this.mnuQuickFilterLoginAny.Click += new System.EventHandler(this.mnuQuickFilterLoginAny_Click);
            // 
            // mnuQuickFilterLoginCurrent
            // 
            this.mnuQuickFilterLoginCurrent.Name = "mnuQuickFilterLoginCurrent";
            this.mnuQuickFilterLoginCurrent.Size = new System.Drawing.Size(135, 22);
            this.mnuQuickFilterLoginCurrent.Text = "Current";
            this.mnuQuickFilterLoginCurrent.Click += new System.EventHandler(this.mnuQuickFilterLoginCurrent_Click);
            // 
            // mnuQuickFilterLoginSystem
            // 
            this.mnuQuickFilterLoginSystem.Name = "mnuQuickFilterLoginSystem";
            this.mnuQuickFilterLoginSystem.Size = new System.Drawing.Size(135, 22);
            this.mnuQuickFilterLoginSystem.Text = "SystemUser";
            this.mnuQuickFilterLoginSystem.Click += new System.EventHandler(this.mnuQuickFilterLoginCurrent_Click);
            // 
            // mnuQuickFilterHost
            // 
            this.mnuQuickFilterHost.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuickFilterHostAny,
            this.mnuQuickFilterHostCurrent});
            this.mnuQuickFilterHost.Name = "mnuQuickFilterHost";
            this.mnuQuickFilterHost.Size = new System.Drawing.Size(204, 22);
            this.mnuQuickFilterHost.Text = "HostName like";
            // 
            // mnuQuickFilterHostAny
            // 
            this.mnuQuickFilterHostAny.Name = "mnuQuickFilterHostAny";
            this.mnuQuickFilterHostAny.Size = new System.Drawing.Size(121, 22);
            this.mnuQuickFilterHostAny.Text = "-- Any --";
            this.mnuQuickFilterHostAny.Click += new System.EventHandler(this.mnuQuickFilterHostAny_Click);
            // 
            // mnuQuickFilterHostCurrent
            // 
            this.mnuQuickFilterHostCurrent.Name = "mnuQuickFilterHostCurrent";
            this.mnuQuickFilterHostCurrent.Size = new System.Drawing.Size(121, 22);
            this.mnuQuickFilterHostCurrent.Text = "Current";
            this.mnuQuickFilterHostCurrent.Click += new System.EventHandler(this.mnuQuickFilterHostCurrent_Click);
            // 
            // notSpresetconnectionToolStripMenuItem
            // 
            this.notSpresetconnectionToolStripMenuItem.Name = "notSpresetconnectionToolStripMenuItem";
            this.notSpresetconnectionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.notSpresetconnectionToolStripMenuItem.Text = "Not sp_reset_connection";
            this.notSpresetconnectionToolStripMenuItem.Click += new System.EventHandler(this.notSpresetconnectionToolStripMenuItem_Click);
            // 
            // tbStart
            // 
            this.tbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRun,
            this.tbRunWithFilters});
            this.tbStart.Image = global::DBMapper.Properties.Resources.imStart;
            this.tbStart.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(32, 22);
            this.tbStart.Text = "Start trace";
            // 
            // tbRun
            // 
            this.tbRun.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tbRun.Name = "tbRun";
            this.tbRun.Size = new System.Drawing.Size(153, 22);
            this.tbRun.Text = "Run";
            this.tbRun.Click += new System.EventHandler(this.tbStart_Click);
            // 
            // tbRunWithFilters
            // 
            this.tbRunWithFilters.Name = "tbRunWithFilters";
            this.tbRunWithFilters.Size = new System.Drawing.Size(153, 22);
            this.tbRunWithFilters.Text = "Run with filters";
            this.tbRunWithFilters.Click += new System.EventHandler(this.tbRunWithFilters_Click);
            // 
            // tbPause
            // 
            this.tbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPause.Image = global::DBMapper.Properties.Resources.imPause;
            this.tbPause.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbPause.Name = "tbPause";
            this.tbPause.Size = new System.Drawing.Size(23, 22);
            this.tbPause.Text = "Pause trace";
            this.tbPause.Click += new System.EventHandler(this.tbPause_Click);
            // 
            // tbStop
            // 
            this.tbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStop.Image = global::DBMapper.Properties.Resources.imStop;
            this.tbStop.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbStop.Name = "tbStop";
            this.tbStop.Size = new System.Drawing.Size(23, 22);
            this.tbStop.Text = "Stop trace";
            this.tbStop.Click += new System.EventHandler(this.tbStop_Click);
            // 
            // tbStayOnTop
            // 
            this.tbStayOnTop.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbStayOnTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStayOnTop.Image = ((System.Drawing.Image)(resources.GetObject("tbStayOnTop.Image")));
            this.tbStayOnTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbStayOnTop.Name = "tbStayOnTop";
            this.tbStayOnTop.Size = new System.Drawing.Size(23, 22);
            this.tbStayOnTop.Text = "Stay on top";
            this.tbStayOnTop.Click += new System.EventHandler(this.tbStayOnTop_Click);
            // 
            // tbTransparent
            // 
            this.tbTransparent.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbTransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbTransparent.Image = ((System.Drawing.Image)(resources.GetObject("tbTransparent.Image")));
            this.tbTransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbTransparent.Name = "tbTransparent";
            this.tbTransparent.Size = new System.Drawing.Size(23, 22);
            this.tbTransparent.Text = "Transparent";
            this.tbTransparent.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnHotkeyActive
            // 
            this.btnHotkeyActive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnHotkeyActive.Checked = true;
            this.btnHotkeyActive.CheckOnClick = true;
            this.btnHotkeyActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnHotkeyActive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHotkeyActive.Image = global::DBMapper.Properties.Resources.keyboard_key;
            this.btnHotkeyActive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHotkeyActive.Name = "btnHotkeyActive";
            this.btnHotkeyActive.Size = new System.Drawing.Size(23, 22);
            this.btnHotkeyActive.Text = "Hotkey active";
            this.btnHotkeyActive.ToolTipText = "Hotkey [Ctrl+Alt+P] active";
            this.btnHotkeyActive.Click += new System.EventHandler(this.btnHotkeyActive_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reTextData);
            this.splitContainer1.Size = new System.Drawing.Size(979, 463);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            // 
            // contextMenuStripTextData
            // 
            this.contextMenuStripTextData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem2});
            this.contextMenuStripTextData.Name = "contextMenuStripTextData";
            this.contextMenuStripTextData.Size = new System.Drawing.Size(123, 32);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.reTextDataSelectAllToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(119, 6);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToClipboardToolStripMenuItem,
            this.copySelectedToClipboardToolStripMenuItem,
            this.copyToXlsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(253, 120);
            // 
            // copyAllToClipboardToolStripMenuItem
            // 
            this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
            this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.copyAllToClipboardToolStripMenuItem.Text = "Copy all events to clipboard";
            this.copyAllToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyAllToClipboardToolStripMenuItem_Click);
            // 
            // copySelectedToClipboardToolStripMenuItem
            // 
            this.copySelectedToClipboardToolStripMenuItem.Name = "copySelectedToClipboardToolStripMenuItem";
            this.copySelectedToClipboardToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.copySelectedToClipboardToolStripMenuItem.Text = "Copy selected events to clipboard";
            this.copySelectedToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToClipboardToolStripMenuItem_Click);
            // 
            // copyToXlsToolStripMenuItem
            // 
            this.copyToXlsToolStripMenuItem.Name = "copyToXlsToolStripMenuItem";
            this.copyToXlsToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.copyToXlsToolStripMenuItem.Text = "Copy all for Excel";
            this.copyToXlsToolStripMenuItem.Click += new System.EventHandler(this.copyToXlsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(249, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
            this.findNextToolStripMenuItem.Text = "Find next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
            // 
            // cbQuerySeparator
            // 
            this.cbQuerySeparator.Items.AddRange(new object[] {
            "",
            ";",
            "\\r\\n",
            ";\\r\\n",
            "\\r\\nGO"});
            this.cbQuerySeparator.Name = "cbQuerySeparator";
            this.cbQuerySeparator.Size = new System.Drawing.Size(121, 25);
            this.cbQuerySeparator.SelectedIndexChanged += new System.EventHandler(this.cbQuerySeparator_SelectedIndexChanged);
            this.cbQuerySeparator.TextChanged += new System.EventHandler(this.cbQuerySeparator_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "separator:";
            // 
            // reTextData
            // 
            this.reTextData.AutoCompleteBracketsList = new char[] {
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
            this.reTextData.AutoIndentCharsPatterns = "";
            this.reTextData.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.reTextData.BackBrush = null;
            this.reTextData.CharHeight = 14;
            this.reTextData.CharWidth = 8;
            this.reTextData.CommentPrefix = "--";
            this.reTextData.ContextMenuStrip = this.contextMenuStripTextData;
            this.reTextData.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.reTextData.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.reTextData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reTextData.IsReplaceMode = false;
            this.reTextData.Language = FastColoredTextBoxNS.Language.SQL;
            this.reTextData.LeftBracket = '(';
            this.reTextData.Location = new System.Drawing.Point(0, 0);
            this.reTextData.Name = "reTextData";
            this.reTextData.Paddings = new System.Windows.Forms.Padding(0);
            this.reTextData.ReadOnly = true;
            this.reTextData.RightBracket = ')';
            this.reTextData.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.reTextData.Size = new System.Drawing.Size(979, 163);
            this.reTextData.TabIndex = 4;
            this.reTextData.WordWrap = true;
            this.reTextData.Zoom = 100;
            // 
            // SQLProfiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "SQLProfiler";
            this.Size = new System.Drawing.Size(979, 510);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripTextData.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.reTextData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbStop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox reTextData;
        private System.Windows.Forms.ToolStripButton tbScroll;
        private System.Windows.Forms.ToolStripButton tbPause;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tbClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton tbStart;
        private System.Windows.Forms.ToolStripMenuItem tbRun;
        private System.Windows.Forms.ToolStripMenuItem tbRunWithFilters;
        private System.Windows.Forms.ToolStripStatusLabel slEPS;
        private System.Windows.Forms.ToolStripMenuItem copyToXlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton tbStayOnTop;
        private System.Windows.Forms.ToolStripButton tbTransparent;
        private System.Windows.Forms.ToolStripSplitButton btnQuickFilter;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterDb;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterLogin;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterHost;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterDbAny;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterLoginAny;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterLoginCurrent;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterHostAny;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterHostCurrent;
        private System.Windows.Forms.ToolStripButton btnHotkeyActive;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTextData;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem notSpresetconnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuQuickFilterLoginSystem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbQuerySeparator;
    }
}

