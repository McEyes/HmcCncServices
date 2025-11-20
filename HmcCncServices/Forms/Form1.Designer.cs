
namespace HmcCncServices
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConn = new System.Windows.Forms.Button();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbpSystemType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OpenStatus = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEquipmentInfo = new System.Windows.Forms.TextBox();
            this.SequenceNo = new System.Windows.Forms.TextBox();
            this.PrgNo = new System.Windows.Forms.TextBox();
            this.txtProgramName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CurrentPrg = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblProgram = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gboxMachinePosition = new System.Windows.Forms.GroupBox();
            this.txtMachineZ = new System.Windows.Forms.TextBox();
            this.txtMachineY = new System.Windows.Forms.TextBox();
            this.txtMachineX = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWorkPostion = new System.Windows.Forms.GroupBox();
            this.txtWorkZ = new System.Windows.Forms.TextBox();
            this.txtWorkY = new System.Windows.Forms.TextBox();
            this.txtWorkX = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gboxCurrentPostion = new System.Windows.Forms.GroupBox();
            this.txtZ = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gboxMachinePosition.SuspendLayout();
            this.txtWorkPostion.SuspendLayout();
            this.gboxCurrentPostion.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(731, 10);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 31);
            this.btnConn.TabIndex = 0;
            this.btnConn.Text = "链接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(78, 16);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(111, 22);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "172.22.151.6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "设备名称/IP:";
            // 
            // cbpSystemType
            // 
            this.cbpSystemType.FormattingEnabled = true;
            this.cbpSystemType.Items.AddRange(new object[] {
            "CNCC80:10",
            "MELDAS800M:9",
            "MELDAS800L:8",
            "MELDASC70:7",
            "MELDAS700M:6",
            "MELDAS700L:5",
            "MELDASC6C64:4",
            "MELDAS600M(M6x5M):3",
            "MELDAS600L(M6x5L):2",
            "MELDASMAGIC64:1",
            "MELDASMAGIC Card64Ⅱ:0"});
            this.cbpSystemType.Location = new System.Drawing.Point(256, 15);
            this.cbpSystemType.Name = "cbpSystemType";
            this.cbpSystemType.Size = new System.Drawing.Size(159, 24);
            this.cbpSystemType.TabIndex = 5;
            this.cbpSystemType.Text = "MELDAS800M:9";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "设备类型：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(893, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 31);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清除日志";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OpenStatus);
            this.panel1.Controls.Add(this.btnConn);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.cbpSystemType);
            this.panel1.Controls.Add(this.txtIp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 50);
            this.panel1.TabIndex = 6;
            // 
            // OpenStatus
            // 
            this.OpenStatus.Enabled = false;
            this.OpenStatus.Location = new System.Drawing.Point(812, 9);
            this.OpenStatus.Name = "OpenStatus";
            this.OpenStatus.Size = new System.Drawing.Size(75, 31);
            this.OpenStatus.TabIndex = 0;
            this.OpenStatus.Text = "启动";
            this.OpenStatus.UseVisualStyleBackColor = true;
            this.OpenStatus.Click += new System.EventHandler(this.OpenStatus_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(497, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "EZNC_LOCALHOST";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(421, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 17);
            this.label16.TabIndex = 4;
            this.label16.Text = "HostName：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(769, 378);
            this.panel2.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEquipmentInfo);
            this.groupBox2.Controls.Add(this.SequenceNo);
            this.groupBox2.Controls.Add(this.PrgNo);
            this.groupBox2.Controls.Add(this.txtProgramName);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.lblProgram);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 378);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "程序信息";
            // 
            // txtEquipmentInfo
            // 
            this.txtEquipmentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEquipmentInfo.Location = new System.Drawing.Point(15, 136);
            this.txtEquipmentInfo.Multiline = true;
            this.txtEquipmentInfo.Name = "txtEquipmentInfo";
            this.txtEquipmentInfo.Size = new System.Drawing.Size(433, 221);
            this.txtEquipmentInfo.TabIndex = 5;
            // 
            // SequenceNo
            // 
            this.SequenceNo.Location = new System.Drawing.Point(89, 93);
            this.SequenceNo.Name = "SequenceNo";
            this.SequenceNo.Size = new System.Drawing.Size(253, 22);
            this.SequenceNo.TabIndex = 2;
            // 
            // PrgNo
            // 
            this.PrgNo.Location = new System.Drawing.Point(89, 61);
            this.PrgNo.Name = "PrgNo";
            this.PrgNo.Size = new System.Drawing.Size(253, 22);
            this.PrgNo.TabIndex = 2;
            // 
            // txtProgramName
            // 
            this.txtProgramName.Location = new System.Drawing.Point(89, 26);
            this.txtProgramName.Name = "txtProgramName";
            this.txtProgramName.Size = new System.Drawing.Size(253, 22);
            this.txtProgramName.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 17);
            this.label15.TabIndex = 1;
            this.label15.Text = "序号编码：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CurrentPrg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(472, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 357);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "程序步骤";
            // 
            // CurrentPrg
            // 
            this.CurrentPrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentPrg.FormattingEnabled = true;
            this.CurrentPrg.ItemHeight = 16;
            this.CurrentPrg.Location = new System.Drawing.Point(3, 18);
            this.CurrentPrg.Name = "CurrentPrg";
            this.CurrentPrg.Size = new System.Drawing.Size(288, 336);
            this.CurrentPrg.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 17);
            this.label14.TabIndex = 1;
            this.label14.Text = "程序序号：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 66);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 17);
            this.label13.TabIndex = 1;
            this.label13.Text = "程序名称：";
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(12, 29);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(71, 17);
            this.lblProgram.TabIndex = 1;
            this.lblProgram.Text = "程序名称：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gboxMachinePosition);
            this.splitContainer1.Panel2.Controls.Add(this.txtWorkPostion);
            this.splitContainer1.Panel2.Controls.Add(this.gboxCurrentPostion);
            this.splitContainer1.Size = new System.Drawing.Size(994, 378);
            this.splitContainer1.SplitterDistance = 769;
            this.splitContainer1.TabIndex = 8;
            // 
            // gboxMachinePosition
            // 
            this.gboxMachinePosition.Controls.Add(this.txtMachineZ);
            this.gboxMachinePosition.Controls.Add(this.txtMachineY);
            this.gboxMachinePosition.Controls.Add(this.txtMachineX);
            this.gboxMachinePosition.Controls.Add(this.label10);
            this.gboxMachinePosition.Controls.Add(this.label11);
            this.gboxMachinePosition.Controls.Add(this.label12);
            this.gboxMachinePosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gboxMachinePosition.Location = new System.Drawing.Point(0, 248);
            this.gboxMachinePosition.Name = "gboxMachinePosition";
            this.gboxMachinePosition.Size = new System.Drawing.Size(221, 124);
            this.gboxMachinePosition.TabIndex = 0;
            this.gboxMachinePosition.TabStop = false;
            this.gboxMachinePosition.Text = "机械位置";
            // 
            // txtMachineZ
            // 
            this.txtMachineZ.Location = new System.Drawing.Point(44, 87);
            this.txtMachineZ.Name = "txtMachineZ";
            this.txtMachineZ.Size = new System.Drawing.Size(122, 22);
            this.txtMachineZ.TabIndex = 1;
            // 
            // txtMachineY
            // 
            this.txtMachineY.Location = new System.Drawing.Point(44, 58);
            this.txtMachineY.Name = "txtMachineY";
            this.txtMachineY.Size = new System.Drawing.Size(122, 22);
            this.txtMachineY.TabIndex = 1;
            // 
            // txtMachineX
            // 
            this.txtMachineX.Location = new System.Drawing.Point(44, 29);
            this.txtMachineX.Name = "txtMachineX";
            this.txtMachineX.Size = new System.Drawing.Size(122, 22);
            this.txtMachineX.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Z：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Y：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "X：";
            // 
            // txtWorkPostion
            // 
            this.txtWorkPostion.Controls.Add(this.txtWorkZ);
            this.txtWorkPostion.Controls.Add(this.txtWorkY);
            this.txtWorkPostion.Controls.Add(this.txtWorkX);
            this.txtWorkPostion.Controls.Add(this.label7);
            this.txtWorkPostion.Controls.Add(this.label8);
            this.txtWorkPostion.Controls.Add(this.label9);
            this.txtWorkPostion.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtWorkPostion.Location = new System.Drawing.Point(0, 124);
            this.txtWorkPostion.Name = "txtWorkPostion";
            this.txtWorkPostion.Size = new System.Drawing.Size(221, 124);
            this.txtWorkPostion.TabIndex = 0;
            this.txtWorkPostion.TabStop = false;
            this.txtWorkPostion.Text = "工作坐标";
            // 
            // txtWorkZ
            // 
            this.txtWorkZ.Location = new System.Drawing.Point(44, 87);
            this.txtWorkZ.Name = "txtWorkZ";
            this.txtWorkZ.Size = new System.Drawing.Size(122, 22);
            this.txtWorkZ.TabIndex = 1;
            // 
            // txtWorkY
            // 
            this.txtWorkY.Location = new System.Drawing.Point(44, 58);
            this.txtWorkY.Name = "txtWorkY";
            this.txtWorkY.Size = new System.Drawing.Size(122, 22);
            this.txtWorkY.TabIndex = 1;
            // 
            // txtWorkX
            // 
            this.txtWorkX.Location = new System.Drawing.Point(44, 29);
            this.txtWorkX.Name = "txtWorkX";
            this.txtWorkX.Size = new System.Drawing.Size(122, 22);
            this.txtWorkX.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Z：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "Y：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "X：";
            // 
            // gboxCurrentPostion
            // 
            this.gboxCurrentPostion.Controls.Add(this.txtZ);
            this.gboxCurrentPostion.Controls.Add(this.txtY);
            this.gboxCurrentPostion.Controls.Add(this.txtX);
            this.gboxCurrentPostion.Controls.Add(this.label6);
            this.gboxCurrentPostion.Controls.Add(this.label5);
            this.gboxCurrentPostion.Controls.Add(this.label4);
            this.gboxCurrentPostion.Dock = System.Windows.Forms.DockStyle.Top;
            this.gboxCurrentPostion.Location = new System.Drawing.Point(0, 0);
            this.gboxCurrentPostion.Name = "gboxCurrentPostion";
            this.gboxCurrentPostion.Size = new System.Drawing.Size(221, 124);
            this.gboxCurrentPostion.TabIndex = 0;
            this.gboxCurrentPostion.TabStop = false;
            this.gboxCurrentPostion.Text = "当前坐标";
            // 
            // txtZ
            // 
            this.txtZ.Location = new System.Drawing.Point(44, 87);
            this.txtZ.Name = "txtZ";
            this.txtZ.Size = new System.Drawing.Size(122, 22);
            this.txtZ.TabIndex = 1;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(44, 58);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(122, 22);
            this.txtY.TabIndex = 1;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(44, 29);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(122, 22);
            this.txtX.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Z：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Y：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "X：";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 428);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(994, 175);
            this.panel3.TabIndex = 9;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(994, 175);
            this.listBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 603);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gboxMachinePosition.ResumeLayout(false);
            this.gboxMachinePosition.PerformLayout();
            this.txtWorkPostion.ResumeLayout(false);
            this.txtWorkPostion.PerformLayout();
            this.gboxCurrentPostion.ResumeLayout(false);
            this.gboxCurrentPostion.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbpSystemType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gboxCurrentPostion;
        private System.Windows.Forms.TextBox txtZ;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProgramName;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.GroupBox txtWorkPostion;
        private System.Windows.Forms.TextBox txtWorkZ;
        private System.Windows.Forms.TextBox txtWorkY;
        private System.Windows.Forms.TextBox txtWorkX;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox gboxMachinePosition;
        private System.Windows.Forms.TextBox txtMachineZ;
        private System.Windows.Forms.TextBox txtMachineY;
        private System.Windows.Forms.TextBox txtMachineX;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox CurrentPrg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox PrgNo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox SequenceNo;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button OpenStatus;
        private System.Windows.Forms.TextBox txtEquipmentInfo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label16;
    }
}

