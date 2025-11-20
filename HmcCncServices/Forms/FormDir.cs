using EZSocketNc.Commons;
using EZSocketNc.EZNc;
using EZSocketNc.Extensions;

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using EZSocketNc.Configs;
using EZSocketNc.Interface;
using EZSocketNc.Siemens;

namespace HmcCncServices
{
    public partial class FormDir : Form
    {
        private EZSocketConfig CurrentConfig;
        private IEZSocket ezsocket;
        public FormDir()
        {
            InitializeComponent();
            listView1.View = View.Details;
            // 添加列
            listView1.Columns.Clear();
            listView1.Columns.Add("Name", 300);
            listView1.Columns.Add("Size", 50);
            listView1.Columns.Add("Date", 120);
            listView1.Columns.Add("Comment", 400);

            //listView1.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        //添加设备
        private void btnGetDrivce_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIp.Text))
            {
                MessageBox.Show("请输入IP地址");
                return;
            }
            var CncConfig = new EZSocketNc.EZNc.EZSocketConfig()
            {
                Ip = txtIp.Text.Trim(),
                SystemType = EZSocketNc.EZNc.EZSystemType.CNC_M800M | EZSocketNc.EZNc.EZSystemType.NC_SYS_MULTI
            };
            if (cbpSystemType.SelectedValue == "Siemens:18")
                CncConfig.SystemType = EZSocketNc.EZNc.EZSystemType.Siemens;
            ezsocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(CncConfig);
            TreeNode node = new TreeNode(ezsocket.SocketConfig.Ip);
            node.Tag = CurrentConfig = CncConfig;
            node.ToolTipText = "设备";
            this.treeView1.Nodes.Add(node);

            ShowErrorMsg(ezsocket);
        }


        private void ShowErrorMsg(IEZSocket ezsocket)
        {
            while (ezsocket.errMsg.TryTake(out string msg))
                textBox1.AppendText(msg + "\r\n");
        }


        protected override void OnClosed(EventArgs e)
        {
            foreach (TreeNode item in treeView1.Nodes)
            {
                var config = item.Tag as CncDeviceConfig;
                if (config != null)
                {
                    var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);// as EZSocket;
                    if (socket != null)
                    {
                        socket.StopMonitor();
                        socket.Close();
                    }
                }
            }
            base.OnClosed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        bool isNext = false;
        //读取
        private void button2_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                if (!ezsocket.IsOpen)
                {
                    ezsocket.Conn();
                    ezsocket.ReadEquipmentState();
                }
                if (!ezsocket.IsOpen)
                {
                    ShowErrorMsg(ezsocket);
                    return;
                }
                IResult<EZNcFileInfo> dirs = null;
                if (!isNext) dirs = ((EZSocket)ezsocket).FindDir2(txtDir.Text.Trim(), EZFileType.EZNC_DISK_DIRTYPE | EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT);
                else dirs = ((EZSocket)ezsocket).FindNextDir2();
                if (dirs.Success)
                {
                    isNext = true;
                    if (dirs.Data == null || string.IsNullOrWhiteSpace(dirs.Data.Name))
                    {
                        isNext = false;
                        textBox1.AppendText("获取目录数据为空\r\n");
                    }
                    else
                    {
                        textBox1.AppendText($"获取目录:{dirs.Data.Name}\t{dirs.Data.Size}\t{dirs.Data.Date}\t{dirs.Data.Comment}\r\n");
                        if (dirs.Data.IsDir)
                        {
                            TreeNode sNode = new TreeNode(treeView1.SelectedNode.Text + dirs.Data.Name + "\\");
                            sNode.Name = dirs.Data.Name;
                            sNode.Tag = CurrentConfig;
                            sNode.ToolTipText = "{dirs.Data.Name}\t{dirs.Data.Size}\t{dirs.Data.Date}\t{dirs.Data.Comment}";
                            treeView1.SelectedNode.Nodes.Add(sNode);
                        }

                        ListViewItem dir = new ListViewItem(dirs.Data.Name);
                        dir.Name = dirs.Data.Name;
                        dir.Tag = dirs.Data;
                        dir.ImageIndex = 0;
                        dir.SubItems.Add(dirs.Data.Size);
                        dir.SubItems.Add(dirs.Data.Date);
                        dir.SubItems.Add(dirs.Data.Comment);
                        dir.ToolTipText = "{dirs.Data.Name}\t{dirs.Data.Size}\t{dirs.Data.Date}\t{dirs.Data.Comment}";
                        listView1.Items.Add(dir);
                    }
                }
                else
                {
                    isNext = false;
                    textBox1.AppendText($"获取目录失败:{txtDir.Text.Trim()}\r\n{dirs.Msg}");
                }
                ShowErrorMsg(ezsocket);
            }
            else if (ezsocket != null)
            {
                MessageBox.Show("非三菱设备不支持该操作");
            }
            else
            {
                MessageBox.Show("请选择设备！" + ezsocket);
            }
        }

        //重置
        private void button3_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                if (!ezsocket.IsOpen)
                {
                    ezsocket.Conn();
                    ezsocket.ReadEquipmentState();
                }
                if (!ezsocket.IsOpen)
                {
                    ShowErrorMsg(ezsocket);
                    return;
                }
                ((EZSocket)ezsocket).ResetDir();
                isNext = false;
                ShowErrorMsg(ezsocket);
            }
            else if (ezsocket != null)
            {
                MessageBox.Show("非三菱设备不支持该操作");
            }
            else
            {
                MessageBox.Show("button3请选择设备！" + ezsocket);
            }
        }

        //下一个
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                if (!ezsocket.IsOpen)
                {
                    ezsocket.Conn();
                    ezsocket.ReadEquipmentState();
                }
                if (!ezsocket.IsOpen)
                {
                    ShowErrorMsg(ezsocket);
                    return;
                }
                var dirs = ((EZSocket)ezsocket).FindNextDir2();
                if (dirs.Success)
                {
                    isNext = true;
                    if (dirs.Data == null)
                    {
                        isNext = false;
                        textBox1.AppendText("获取目录数据为空\r\n");
                    }
                    else
                        textBox1.AppendText($"获取目录:{dirs.Data.Name}\t{dirs.Data.Size}\t{dirs.Data.Date}\t{dirs.Data.Comment}\r\n");
                }
                else
                {
                    isNext = false;
                    textBox1.AppendText($"获取目录失败:{txtDir.Text.Trim()}\r\n{dirs.Msg}");
                }
                ShowErrorMsg(ezsocket);
            }
            else if (ezsocket != null)
            {
                MessageBox.Show("非三菱设备不支持该操作");
            }
            else
            {
                MessageBox.Show("button2请选择设备！" + ezsocket);
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                //if (ezsocket.EquipmentInfo.RunStatus == EZSocketNc.EquipmentStatus.Running)
                //{
                //    MessageBox.Show("设备运行中，禁止读写！");
                //    return;
                //}
                var node = treeView1.SelectedNode;
                if (node.ToolTipText == "设备")
                {
                    if (!ezsocket.IsOpen)
                    {
                        ezsocket.Conn();
                        ezsocket.ReadEquipmentState();
                    }
                    if (!ezsocket.IsOpen)
                    {
                        node.Text = $"{CurrentConfig.Key}(无法访问)";
                        ShowErrorMsg(ezsocket);
                        return;
                    }
                    ShowErrorMsg(ezsocket);
                    var dirs = ((EZSocket)ezsocket).GetDriveInformation();
                    if (dirs.Success)
                    {
                        var dirStr = "PRG;PRM;LAD;DAT;LOG;PLCMSG;IC1;FNET;LOGEX;";
                        var subDirs = dirStr.Split(';');
                        foreach (var item in dirs.Data)
                        {
                            TreeNode subNode = new TreeNode(item + "\\");
                            subNode.Name = item;
                            subNode.Tag = CurrentConfig;
                            subNode.ToolTipText = "驱动器";

                            node.Nodes.Add(subNode);
                            foreach (var sitem in subDirs)
                            {
                                TreeNode sNode = new TreeNode(subNode.Text + sitem + "\\");
                                sNode.Name = item;
                                sNode.Tag = CurrentConfig;
                                sNode.ToolTipText = "根目录";
                                subNode.Nodes.Add(sNode);
                            }
                        }
                    }
                    ShowErrorMsg(ezsocket);
                }
                //else if (node.Text.EndsWith("\\"))
                //{
                //    var list = ezsocket.GetAllDirs(node.Text);
                //    foreach (var item in list.Data)
                //    {
                //        TreeNode subNode = new TreeNode(node.Text + item.Name + "\\");
                //        subNode.Name = item.Name;
                //        subNode.Tag = CurrentConfig;
                //        subNode.ToolTipText = $"目录{item.Name}:{item.Size}:{item.Comment}";
                //        node.Nodes.Add(subNode);
                //    }
                //    ShowErrorMsg(ezsocket);
                //    var files = ezsocket.GetAllFiles(node.Text);
                //    foreach (var item in files.Data)
                //    {
                //        ListViewItem dir = new ListViewItem(item.Name);
                //        dir.Name = item.Name;
                //        dir.Tag = item;
                //        dir.ImageIndex = 1;
                //        dir.SubItems.Add(item.Size);
                //        dir.SubItems.Add(item.Date);
                //        dir.SubItems.Add(item.Comment);
                //        dir.ToolTipText = $"{item.Name}\t{item.Size}\t{item.Date}\t{item.Comment}";
                //        listView1.Items.Add(dir);
                //    }
                //    ShowErrorMsg(ezsocket);
                //}
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                CurrentConfig = e.Node.Tag as EZSocketConfig;
                ezsocket = EZSocketFactory.CreateEZSocket(CurrentConfig);

                txtDir.Text = e.Node.Text;
                listView1.Items.Clear();

                if (ezsocket != null && ezsocket.DeviceType == "melsec")
                {
                    if (!ezsocket.IsOpen)
                    {
                        ezsocket.Conn();
                        ezsocket.ReadEquipmentState();
                    }
                    if (!ezsocket.IsOpen)
                    {
                        ShowErrorMsg(ezsocket);
                        return;
                    }
                    var dirs = ((EZSocket)ezsocket).GetAllDirs(e.Node.Text);
                    var files = ((EZSocket)ezsocket).GetAllFiles(e.Node.Text);

                    var list = new List<EZNcFileInfo>();
                    if (dirs.Success && dirs.Data.Length > 0)
                        list.AddRange(dirs.Data);
                    if (files.Success && files.Data.Length > 0)
                        list.AddRange(files.Data);

                    foreach (var item in list)
                    {
                        ListViewItem sNode = new ListViewItem(item.Name);
                        sNode.Name = item.Name;
                        sNode.Tag = dirs.Data;
                        sNode.ImageIndex = item.IsDir ? 0 : 1;
                        sNode.SubItems.Add(item.Size);
                        sNode.SubItems.Add(item.Date);
                        sNode.SubItems.Add(item.Comment);
                        sNode.ToolTipText = $"{item.Name}\t{item.Size}\t{item.Date}\t{item.Comment}";
                        listView1.Items.Add(sNode);
                        if (e.Node.Nodes.Count == 0 && item.IsDir)
                        {
                            TreeNode tNode = new TreeNode(treeView1.SelectedNode.Text + item.Name + "\\");
                            tNode.Name = item.Name;
                            tNode.Tag = CurrentConfig;
                            tNode.ToolTipText = "{dirs.Data.Name}\t{dirs.Data.Size}\t{dirs.Data.Date}\t{dirs.Data.Comment}";
                            treeView1.SelectedNode.Nodes.Add(tNode);
                        }
                    }
                    ShowErrorMsg(ezsocket);
                }
                else
                {
                    MessageBox.Show("treeView1请选择设备！" + ezsocket);
                }
            }
        }


        private void btnCreateDir_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).CreateDir(txtDir.Text.Trim());
                if (result.Success)
                {
                    textBox1.AppendText($"{txtDir.Text}目录创建成功!\r\n");
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).FindDir2(txtWriteFile.Text.Trim(), EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT);
                if (result.Success)
                {
                    textBox1.AppendText(result.Data.ToJSON());
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = openFileDialog1.FileName;
                if (string.IsNullOrWhiteSpace(txtWriteFile.Text))
                {
                    if (ezsocket.DeviceType == "siemens")
                        txtWriteFile.Text = @"F:\dh\spf.dir\" + System.IO.Path.GetFileName(openFileDialog1.FileName);
                    else
                        txtWriteFile.Text = treeView1.SelectedNode.Text + System.IO.Path.GetFileName(openFileDialog1.FileName);
                }
                if (txtWriteFile.Text.ContainsChinese())
                {
                    MessageBox.Show($"写入文件名不能含有中文！");
                    return;
                }

                if (ezsocket != null)
                {
                    if (ezsocket.DeviceType == "melsec")
                    {
                        var result = ((EZSocket)ezsocket).OpenFile4(txtWriteFile.Text.Trim(), 3);
                        if (!result.Success)
                        {
                            ShowErrorMsg(ezsocket);
                            return;
                        }
                        var offset = 0;
                        var bufferSize = 2048;
                        using (var stream = openFileDialog1.OpenFile())
                        {
                            var length = (int)stream.Length;
                            var count = bufferSize;
                            while (offset < length)
                            {
                                count = Math.Min(bufferSize, length - offset); // 确保不超过剩余长度
                                var buffer = new byte[count];
                                stream.Read(buffer, 0, count);
                                offset += bufferSize;
                                result = ((EZSocket)ezsocket).WriteFile(buffer);
                                if (!result.Success)
                                {
                                    ShowErrorMsg(ezsocket);
                                    ((EZSocket)ezsocket).CloseFile3();
                                    return;
                                }
                            }
                        }
                        ShowErrorMsg(ezsocket);
                        if (result.Success)
                        {
                            textBox1.AppendText($"{txtDir.Text}文件写入成功!\r\n");
                            treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
                        }
                        ((EZSocket)ezsocket).CloseFile3();
                    }
                    else if (ezsocket.DeviceType == "siemens")
                    {
                        var fileName = txtDir.Text;
                        var savePath = txtWriteFile.Text.Trim();
                        var fileBuffer = System.IO.File.ReadAllBytes(fileName);
                        ezsocket.WriteFile(savePath, fileBuffer);
                        ShowErrorMsg(ezsocket);
                    }
                }
                else
                {
                    MessageBox.Show("请选择设备！");
                }
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).CloseFile3();
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void btnDelFile_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).Delete(txtWriteFile.Text.Trim());
                if (result.Success)
                {
                    treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
                    textBox1.AppendText($"{txtWriteFile.Text}文件删除成功!\r\n");
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void BtnCopyFile_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).Copy(txtDir.Text.Trim(), txtWriteFile.Text.Trim());
                if (result.Success)
                {
                    treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
                    textBox1.AppendText($"{txtDir.Text}文件复制成功!\r\n");
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).Rename(txtDir.Text.Trim(), txtWriteFile.Text.Trim());
                if (result.Success)
                {
                    treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
                    textBox1.AppendText($"{txtDir.Text}文件重命名成功!\r\n");
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void btnDelDir_Click(object sender, EventArgs e)
        {

            if (ezsocket != null && ezsocket.DeviceType == "melsec")
            {
                var result = ((EZSocket)ezsocket).DeleteDir(txtDir.Text.Trim());
                if (result.Success)
                {
                    textBox1.AppendText($"{txtDir.Text}目录删除成功!\r\n");
                }
                ShowErrorMsg(ezsocket);
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (ezsocket != null && ezsocket.DeviceType == "melsec" && listView1.SelectedItems != null & listView1.SelectedItems.Count > 0)
            {
                txtWriteFile.Text = treeView1.SelectedNode.Text + listView1.SelectedItems[0].Text;
                var result = ((EZSocket)ezsocket).FindDir2(txtWriteFile.Text.Trim(), EZFileType.EZNC_DISK_SIZE | EZFileType.EZNC_DISK_DATE | EZFileType.EZNC_DISK_COMMENT);
                if (result.Success)
                {
                    textBox1.AppendText(result.Data.ToJSON());
                }
                ShowErrorMsg(ezsocket);
            }
            //else
            //{
            //    MessageBox.Show("请选择文件！");
            //}
        }
    }
}
