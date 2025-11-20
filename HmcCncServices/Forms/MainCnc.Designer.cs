
namespace HmcCncServices
{
    partial class MainCnc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainCnc));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.form1测试界面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.三菱目录测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刀库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.西门子上传文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.监听ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取设备目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.获取设备目录ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDreves = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunLight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProgramName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlarmMsgStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem,
            this.设备ToolStripMenuItem,
            this.form1测试界面ToolStripMenuItem,
            this.三菱目录测试ToolStripMenuItem,
            this.刀库ToolStripMenuItem,
            this.清除日志ToolStripMenuItem,
            this.西门子上传文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1310, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.文件目录ToolStripMenuItem});
            this.菜单ToolStripMenuItem.Enabled = false;
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.测试ToolStripMenuItem.Text = "测试";
            this.测试ToolStripMenuItem.Click += new System.EventHandler(this.测试ToolStripMenuItem_Click);
            // 
            // 文件目录ToolStripMenuItem
            // 
            this.文件目录ToolStripMenuItem.Name = "文件目录ToolStripMenuItem";
            this.文件目录ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.文件目录ToolStripMenuItem.Text = "文件目录";
            this.文件目录ToolStripMenuItem.Click += new System.EventHandler(this.文件目录ToolStripMenuItem_Click);
            // 
            // 设备ToolStripMenuItem
            // 
            this.设备ToolStripMenuItem.Enabled = false;
            this.设备ToolStripMenuItem.Name = "设备ToolStripMenuItem";
            this.设备ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.设备ToolStripMenuItem.Text = "添加设备";
            this.设备ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // form1测试界面ToolStripMenuItem
            // 
            this.form1测试界面ToolStripMenuItem.Enabled = false;
            this.form1测试界面ToolStripMenuItem.Name = "form1测试界面ToolStripMenuItem";
            this.form1测试界面ToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.form1测试界面ToolStripMenuItem.Text = "Form1测试界面";
            this.form1测试界面ToolStripMenuItem.Click += new System.EventHandler(this.form1测试界面ToolStripMenuItem_Click);
            // 
            // 三菱目录测试ToolStripMenuItem
            // 
            this.三菱目录测试ToolStripMenuItem.Name = "三菱目录测试ToolStripMenuItem";
            this.三菱目录测试ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.三菱目录测试ToolStripMenuItem.Text = "三菱目录测试";
            this.三菱目录测试ToolStripMenuItem.Click += new System.EventHandler(this.目录测试ToolStripMenuItem_Click);
            // 
            // 刀库ToolStripMenuItem
            // 
            this.刀库ToolStripMenuItem.Enabled = false;
            this.刀库ToolStripMenuItem.Name = "刀库ToolStripMenuItem";
            this.刀库ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.刀库ToolStripMenuItem.Text = "刀库";
            // 
            // 清除日志ToolStripMenuItem
            // 
            this.清除日志ToolStripMenuItem.Name = "清除日志ToolStripMenuItem";
            this.清除日志ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.清除日志ToolStripMenuItem.Text = "清除日志";
            this.清除日志ToolStripMenuItem.Click += new System.EventHandler(this.清除日志ToolStripMenuItem_Click);
            // 
            // 西门子上传文件ToolStripMenuItem
            // 
            this.西门子上传文件ToolStripMenuItem.Name = "西门子上传文件ToolStripMenuItem";
            this.西门子上传文件ToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.西门子上传文件ToolStripMenuItem.Text = "西门子上传文件";
            this.西门子上传文件ToolStripMenuItem.Click += new System.EventHandler(this.西门子上传文件ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 552);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1310, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1310, 524);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.lblDreves);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1310, 524);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 30);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(191, 494);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.监听ToolStripMenuItem,
            this.停止ToolStripMenuItem,
            this.获取设备目录ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 76);
            // 
            // 监听ToolStripMenuItem
            // 
            this.监听ToolStripMenuItem.Name = "监听ToolStripMenuItem";
            this.监听ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.监听ToolStripMenuItem.Text = "监听";
            this.监听ToolStripMenuItem.Click += new System.EventHandler(this.监听ToolStripMenuItem_Click);
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.停止ToolStripMenuItem.Text = "停止";
            this.停止ToolStripMenuItem.Click += new System.EventHandler(this.停止ToolStripMenuItem_Click);
            // 
            // 获取设备目录ToolStripMenuItem
            // 
            this.获取设备目录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.获取设备目录ToolStripMenuItem1});
            this.获取设备目录ToolStripMenuItem.Name = "获取设备目录ToolStripMenuItem";
            this.获取设备目录ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.获取设备目录ToolStripMenuItem.Text = "目录";
            // 
            // 获取设备目录ToolStripMenuItem1
            // 
            this.获取设备目录ToolStripMenuItem1.Name = "获取设备目录ToolStripMenuItem1";
            this.获取设备目录ToolStripMenuItem1.Size = new System.Drawing.Size(182, 26);
            this.获取设备目录ToolStripMenuItem1.Text = "获取设备目录";
            // 
            // lblDreves
            // 
            this.lblDreves.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDreves.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDreves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDreves.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDreves.Location = new System.Drawing.Point(0, 0);
            this.lblDreves.Name = "lblDreves";
            this.lblDreves.Size = new System.Drawing.Size(191, 30);
            this.lblDreves.TabIndex = 1;
            this.lblDreves.Text = " 设备列表";
            this.lblDreves.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1115, 524);
            this.splitContainer2.SplitterDistance = 400;
            this.splitContainer2.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ip,
            this.RunStatus,
            this.RunLight,
            this.ProgramName,
            this.CurrentPosition,
            this.TotalRunTime,
            this.Column1,
            this.FeedSpeed,
            this.FeedRate,
            this.AlarmMsgStr});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1115, 400);
            this.dataGridView1.TabIndex = 0;
            // 
            // Ip
            // 
            this.Ip.DataPropertyName = "Name";
            this.Ip.Frozen = true;
            this.Ip.HeaderText = "设备名称";
            this.Ip.MinimumWidth = 6;
            this.Ip.Name = "Ip";
            this.Ip.ReadOnly = true;
            this.Ip.Width = 90;
            // 
            // RunStatus
            // 
            this.RunStatus.DataPropertyName = "RunStatus";
            this.RunStatus.FillWeight = 50F;
            this.RunStatus.HeaderText = "状态";
            this.RunStatus.MinimumWidth = 6;
            this.RunStatus.Name = "RunStatus";
            this.RunStatus.ReadOnly = true;
            this.RunStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.RunStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RunStatus.Width = 60;
            // 
            // RunLight
            // 
            this.RunLight.DataPropertyName = "RunLight";
            this.RunLight.FillWeight = 80F;
            this.RunLight.HeaderText = "灯状态";
            this.RunLight.MinimumWidth = 6;
            this.RunLight.Name = "RunLight";
            this.RunLight.ReadOnly = true;
            this.RunLight.Width = 70;
            // 
            // ProgramName
            // 
            this.ProgramName.DataPropertyName = "ProgramName";
            this.ProgramName.FillWeight = 120F;
            this.ProgramName.HeaderText = "加工程序";
            this.ProgramName.MinimumWidth = 6;
            this.ProgramName.Name = "ProgramName";
            this.ProgramName.ReadOnly = true;
            this.ProgramName.Width = 150;
            // 
            // CurrentPosition
            // 
            this.CurrentPosition.DataPropertyName = "CurrentPosition";
            this.CurrentPosition.FillWeight = 120F;
            this.CurrentPosition.HeaderText = "当前坐标";
            this.CurrentPosition.MinimumWidth = 6;
            this.CurrentPosition.Name = "CurrentPosition";
            this.CurrentPosition.ReadOnly = true;
            this.CurrentPosition.Width = 180;
            // 
            // TotalRunTime
            // 
            this.TotalRunTime.DataPropertyName = "TotalRunTime";
            this.TotalRunTime.HeaderText = "总加工时间";
            this.TotalRunTime.MinimumWidth = 6;
            this.TotalRunTime.Name = "TotalRunTime";
            this.TotalRunTime.ReadOnly = true;
            this.TotalRunTime.Width = 125;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CurrentDateTime";
            this.Column1.HeaderText = "设备时间";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // FeedSpeed
            // 
            this.FeedSpeed.DataPropertyName = "FeedSpeed";
            this.FeedSpeed.FillWeight = 80F;
            this.FeedSpeed.HeaderText = "主轴转速";
            this.FeedSpeed.MinimumWidth = 6;
            this.FeedSpeed.Name = "FeedSpeed";
            this.FeedSpeed.ReadOnly = true;
            this.FeedSpeed.Width = 80;
            // 
            // FeedRate
            // 
            this.FeedRate.DataPropertyName = "FeedRate";
            this.FeedRate.FillWeight = 80F;
            this.FeedRate.HeaderText = "主轴倍率";
            this.FeedRate.MinimumWidth = 6;
            this.FeedRate.Name = "FeedRate";
            this.FeedRate.ReadOnly = true;
            this.FeedRate.Width = 80;
            // 
            // AlarmMsgStr
            // 
            this.AlarmMsgStr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AlarmMsgStr.DataPropertyName = "AlarmMsgStr";
            this.AlarmMsgStr.HeaderText = "报警信息";
            this.AlarmMsgStr.MinimumWidth = 6;
            this.AlarmMsgStr.Name = "AlarmMsgStr";
            this.AlarmMsgStr.ReadOnly = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(1115, 120);
            this.textBox1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "双击打开Cnc";
            this.notifyIcon1.BalloonTipTitle = "打开Cnc";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "CNC";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // MainCnc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 574);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainCnc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cnc采集服务";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainCnc_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblDreves;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 监听ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 获取设备目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 获取设备目录ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 文件目录ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem form1测试界面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 三菱目录测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 西门子上传文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刀库ToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunLight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProgramName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRunTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeedSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeedRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlarmMsgStr;
    }
}