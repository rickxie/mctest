namespace MCTest.WinformTest
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.interestCalcBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colStartDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCalcD = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDeadlineDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInterest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInterestDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCalc = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.interestCalcBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 261);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(732, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "利息计算器";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAmount,
            this.colDeadlineDay,
            this.colRate,
            this.colInterest,
            this.colInterestDay,
            this.colCalc});
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView3.Location = new System.Drawing.Point(2, 2);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 26;
            this.dataGridView3.Size = new System.Drawing.Size(728, 231);
            this.dataGridView3.TabIndex = 1;
            this.dataGridView3.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            this.dataGridView3.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(732, 235);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "日期计算器";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(728, 231);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 8;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStartDay,
            this.colEndDay,
            this.colDays,
            this.colCalcD});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 26;
            this.dataGridView2.Size = new System.Drawing.Size(448, 231);
            this.dataGridView2.TabIndex = 7;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 231);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Size = new System.Drawing.Size(732, 235);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "估值查询";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // interestCalcBindingSource
            // 
            this.interestCalcBindingSource.DataSource = typeof(MCTest.WinformTest.InterestCalc);
            // 
            // colStartDay
            // 
            this.colStartDay.DataPropertyName = "TxtStartDate";
            this.colStartDay.FillWeight = 78.39195F;
            this.colStartDay.Frozen = true;
            this.colStartDay.HeaderText = "起始时间(年/月/日)";
            this.colStartDay.Name = "colStartDay";
            this.colStartDay.Width = 123;
            // 
            // colEndDay
            // 
            this.colEndDay.DataPropertyName = "TxtEndDate";
            this.colEndDay.FillWeight = 78.39195F;
            this.colEndDay.Frozen = true;
            this.colEndDay.HeaderText = "到期时间(年/月/日)";
            this.colEndDay.Name = "colEndDay";
            this.colEndDay.Width = 124;
            // 
            // colDays
            // 
            this.colDays.DataPropertyName = "TxtTotalDays";
            this.colDays.FillWeight = 60F;
            this.colDays.Frozen = true;
            this.colDays.HeaderText = "天数";
            this.colDays.Name = "colDays";
            this.colDays.Width = 80;
            // 
            // colCalcD
            // 
            this.colCalcD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "计算";
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            this.colCalcD.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCalcD.FillWeight = 60F;
            this.colCalcD.Frozen = true;
            this.colCalcD.HeaderText = "操作";
            this.colCalcD.Name = "colCalcD";
            this.colCalcD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCalcD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colCalcD.Text = "计算";
            this.colCalcD.UseColumnTextForButtonValue = true;
            this.colCalcD.Width = 60;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "TxtAmount";
            this.colAmount.HeaderText = "金额(亿)";
            this.colAmount.Name = "colAmount";
            // 
            // colDeadlineDay
            // 
            this.colDeadlineDay.DataPropertyName = "TxtDueDate";
            this.colDeadlineDay.HeaderText = "期限";
            this.colDeadlineDay.Name = "colDeadlineDay";
            // 
            // colRate
            // 
            this.colRate.DataPropertyName = "TxtRate";
            this.colRate.HeaderText = "利率(%)";
            this.colRate.Name = "colRate";
            // 
            // colInterest
            // 
            this.colInterest.DataPropertyName = "TxtInterest";
            this.colInterest.HeaderText = "利息(元)";
            this.colInterest.Name = "colInterest";
            this.colInterest.Width = 150;
            // 
            // colInterestDay
            // 
            this.colInterestDay.DataPropertyName = "TxtInterestDay";
            this.colInterestDay.HeaderText = "计息";
            this.colInterestDay.Name = "colInterestDay";
            // 
            // colCalc
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "计算";
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.colCalc.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCalc.HeaderText = "操作";
            this.colCalc.Name = "colCalc";
            this.colCalc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colCalc.Text = "计算";
            this.colCalc.ToolTipText = "计算";
            this.colCalc.UseColumnTextForButtonValue = true;
            this.colCalc.Width = 60;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 261);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "工具箱";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.interestCalcBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.BindingSource interestCalcBindingSource;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDays;
        private System.Windows.Forms.DataGridViewButtonColumn colCalcD;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDeadlineDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInterest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInterestDay;
        private System.Windows.Forms.DataGridViewButtonColumn colCalc;
    }
}

