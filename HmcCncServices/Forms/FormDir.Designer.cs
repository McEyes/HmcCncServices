
namespace HmcCncServices
{
    partial class FormDir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDir));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.cbpSystemType = new System.Windows.Forms.ComboBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetDrivce = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lblDreves = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelDir = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnRename = new System.Windows.Forms.Button();
            this.BtnCopyFile = new System.Windows.Forms.Button();
            this.btnCreateDir = new System.Windows.Forms.Button();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.btnWriteFile = new System.Windows.Forms.Button();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnResetDirRead = new System.Windows.Forms.Button();
            this.btnReadDir = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtWriteFile = new System.Windows.Forms.TextBox();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbpSystemType
            // 
            this.cbpSystemType.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbpSystemType.FormattingEnabled = true;
            this.cbpSystemType.Items.AddRange(new object[] {
            "Siemens:18",
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
            this.cbpSystemType.Location = new System.Drawing.Point(465, 8);
            this.cbpSystemType.Name = "cbpSystemType";
            this.cbpSystemType.Size = new System.Drawing.Size(178, 26);
            this.cbpSystemType.TabIndex = 15;
            this.cbpSystemType.Text = "MELDAS800M:9";
            // 
            // txtIp
            // 
            this.txtIp.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIp.Location = new System.Drawing.Point(156, 7);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(178, 28);
            this.txtIp.TabIndex = 12;
            this.txtIp.Text = "172.22.151.6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(350, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "设备类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 19);
            this.label1.TabIndex = 14;
            this.label1.Text = "设备名称/IP：";
            // 
            // btnGetDrivce
            // 
            this.btnGetDrivce.Location = new System.Drawing.Point(667, 5);
            this.btnGetDrivce.Name = "btnGetDrivce";
            this.btnGetDrivce.Size = new System.Drawing.Size(73, 33);
            this.btnGetDrivce.TabIndex = 16;
            this.btnGetDrivce.Text = "添加";
            this.btnGetDrivce.UseVisualStyleBackColor = true;
            this.btnGetDrivce.Click += new System.EventHandler(this.btnGetDrivce_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 122);
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
            this.splitContainer1.Size = new System.Drawing.Size(1403, 468);
            this.splitContainer1.SplitterDistance = 211;
            this.splitContainer1.TabIndex = 17;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 36);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(211, 432);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Elite.ico");
            this.imageList1.Images.SetKeyName(1, "Flap.ico");
            // 
            // lblDreves
            // 
            this.lblDreves.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDreves.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDreves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDreves.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDreves.Location = new System.Drawing.Point(0, 0);
            this.lblDreves.Name = "lblDreves";
            this.lblDreves.Size = new System.Drawing.Size(211, 36);
            this.lblDreves.TabIndex = 3;
            this.lblDreves.Text = "驱动器列表";
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
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Size = new System.Drawing.Size(1188, 468);
            this.splitContainer2.SplitterDistance = 236;
            this.splitContainer2.TabIndex = 2;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1188, 236);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.StateImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1188, 228);
            this.textBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelDir);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnRename);
            this.panel1.Controls.Add(this.BtnCopyFile);
            this.panel1.Controls.Add(this.btnCreateDir);
            this.panel1.Controls.Add(this.btnDelFile);
            this.panel1.Controls.Add(this.btnWriteFile);
            this.panel1.Controls.Add(this.btnReadFile);
            this.panel1.Controls.Add(this.btnResetDirRead);
            this.panel1.Controls.Add(this.btnReadDir);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnGetDrivce);
            this.panel1.Controls.Add(this.txtWriteFile);
            this.panel1.Controls.Add(this.txtDir);
            this.panel1.Controls.Add(this.txtIp);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbpSystemType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1403, 122);
            this.panel1.TabIndex = 18;
            // 
            // btnDelDir
            // 
            this.btnDelDir.Location = new System.Drawing.Point(830, 39);
            this.btnDelDir.Name = "btnDelDir";
            this.btnDelDir.Size = new System.Drawing.Size(107, 33);
            this.btnDelDir.TabIndex = 21;
            this.btnDelDir.Text = "删除目录";
            this.btnDelDir.UseVisualStyleBackColor = true;
            this.btnDelDir.Click += new System.EventHandler(this.btnDelDir_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1075, 80);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 33);
            this.button2.TabIndex = 20;
            this.button2.Text = "关闭文件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(965, 80);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(110, 33);
            this.btnRename.TabIndex = 20;
            this.btnRename.Text = "重命名";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // BtnCopyFile
            // 
            this.BtnCopyFile.Location = new System.Drawing.Point(855, 80);
            this.BtnCopyFile.Name = "BtnCopyFile";
            this.BtnCopyFile.Size = new System.Drawing.Size(110, 33);
            this.BtnCopyFile.TabIndex = 20;
            this.BtnCopyFile.Text = "复制文件";
            this.BtnCopyFile.UseVisualStyleBackColor = true;
            this.BtnCopyFile.Click += new System.EventHandler(this.BtnCopyFile_Click);
            // 
            // btnCreateDir
            // 
            this.btnCreateDir.Location = new System.Drawing.Point(717, 39);
            this.btnCreateDir.Name = "btnCreateDir";
            this.btnCreateDir.Size = new System.Drawing.Size(107, 33);
            this.btnCreateDir.TabIndex = 20;
            this.btnCreateDir.Text = "创建目录";
            this.btnCreateDir.UseVisualStyleBackColor = true;
            this.btnCreateDir.Click += new System.EventHandler(this.btnCreateDir_Click);
            // 
            // btnDelFile
            // 
            this.btnDelFile.Location = new System.Drawing.Point(745, 80);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(110, 33);
            this.btnDelFile.TabIndex = 20;
            this.btnDelFile.Text = "删除文件";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(635, 80);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(110, 33);
            this.btnWriteFile.TabIndex = 20;
            this.btnWriteFile.Text = "写入文件";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(525, 80);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(110, 33);
            this.btnReadFile.TabIndex = 20;
            this.btnReadFile.Text = "读取文件信息";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnResetDirRead
            // 
            this.btnResetDirRead.Location = new System.Drawing.Point(638, 39);
            this.btnResetDirRead.Name = "btnResetDirRead";
            this.btnResetDirRead.Size = new System.Drawing.Size(73, 33);
            this.btnResetDirRead.TabIndex = 18;
            this.btnResetDirRead.Text = "重置";
            this.btnResetDirRead.UseVisualStyleBackColor = true;
            this.btnResetDirRead.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnReadDir
            // 
            this.btnReadDir.Location = new System.Drawing.Point(525, 39);
            this.btnReadDir.Name = "btnReadDir";
            this.btnReadDir.Size = new System.Drawing.Size(107, 33);
            this.btnReadDir.TabIndex = 17;
            this.btnReadDir.Text = "读取目录信息";
            this.btnReadDir.UseVisualStyleBackColor = true;
            this.btnReadDir.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(746, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 33);
            this.button1.TabIndex = 16;
            this.button1.Text = "清除日志";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtWriteFile
            // 
            this.txtWriteFile.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWriteFile.Location = new System.Drawing.Point(116, 82);
            this.txtWriteFile.Name = "txtWriteFile";
            this.txtWriteFile.Size = new System.Drawing.Size(403, 28);
            this.txtWriteFile.TabIndex = 12;
            // 
            // txtDir
            // 
            this.txtDir.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDir.Location = new System.Drawing.Point(116, 41);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(403, 28);
            this.txtDir.TabIndex = 12;
            this.txtDir.Text = "MFF:\\PRG";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 19);
            this.label4.TabIndex = 14;
            this.label4.Text = "写入文件：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "读取目录：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormDir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 590);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "FormDir";
            this.Text = "FormDir";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbpSystemType;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetDrivce;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label lblDreves;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReadDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResetDirRead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWriteFile;
        private System.Windows.Forms.Button BtnCopyFile;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.Button btnWriteFile;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDelDir;
        private System.Windows.Forms.Button btnCreateDir;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ImageList imageList1;
    }
}