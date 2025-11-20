using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Xml.Linq;

using EZSocketNc.Extensions;
using EZSocketNc.EZNc;
using EZSocketNc.Interface;

namespace HmcCncServices.Forms
{
    public partial class SemensDir : Form
    {
        private List<IEZSocket> EzCncList;
        public SemensDir(List<IEZSocket> ezCncList)
        {
            InitializeComponent();
            EzCncList = ezCncList;
            this.cbxCNC.DataSource = ezCncList;
            this.cbxCNC.DisplayMember = "Name";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtSourceFile.Text = this.openFileDialog1.FileName;
                this.txtSaveFile.Text = System.IO.Path.GetFileName(openFileDialog1.FileName);
                this.progressBar1.Value = 0;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxCNC.Text))
            {
                MessageBox.Show($"请选择设备！");
                return;
            }
            if (txtSaveFile.Text.ContainsChinese())
            {
                MessageBox.Show($"写入文件名不能含有中文！");
                return;
            }


            var ip = cbxCNC.Text.Split('_')[1].Split(':')[0];
            var CncConfig = new EZSocketNc.EZNc.EZSocketConfig()
            {
                Ip = ip,
                SystemType = EZSocketNc.EZNc.EZSystemType.CNC_M800M | EZSocketNc.EZNc.EZSystemType.NC_SYS_MULTI,
            };
            if (cbxCNC.Text.Contains("西门子"))
                CncConfig.SystemType = EZSocketNc.EZNc.EZSystemType.Siemens;
            var ezsocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(CncConfig);
            if (ezsocket != null)
            {
                Task.Run(() =>
                {
                    if (ezsocket.DeviceType == "melsec")
                    {
                        if (!ezsocket.IsOpen)
                        {
                            ezsocket.Conn();
                            ezsocket.ReadEquipmentState();
                        }
                        if (!ezsocket.IsOpen)
                        {
                            MessageBox.Show($"{cbxCNC.Text}设备无法访问！");
                            ShowErrorMsg(ezsocket);
                            return;
                        }
                        ShowErrorMsg(ezsocket);
                        var dirResult = ((EZSocket)ezsocket).GetDriveInformation();
                        if (!dirResult.Success)
                        {
                            MessageBox.Show(dirResult.Msg);
                            ShowErrorMsg(ezsocket);
                            return;
                        }
                        var filePath = dirResult.Data[0] + "\\IC1\\" + txtSaveFile.Text.Trim();
                        var result = ((EZSocket)ezsocket).OpenFile4(filePath, 3);
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
                            this.progressBar1.Value = 100;
                            txtErrorMsg.AppendText($"{txtSaveFile.Text}文件写入成功!\r\n");
                        }
                        ((EZSocket)ezsocket).CloseFile3();
                    }
                    else if (ezsocket.DeviceType == "siemens")
                    {
                        var fileName = txtSourceFile.Text;
                        var savePath = "f:\\dh\\spf.dir\\" + txtSaveFile.Text.Trim();
                        var fileBuffer = System.IO.File.ReadAllBytes(fileName);
                        var result = ezsocket.WriteFile(savePath, fileBuffer);
                        ShowErrorMsg(ezsocket);
                        if (result.Success)
                        {
                            this.progressBar1.Value = 100;
                        }
                        else
                        {
                            txtErrorMsg.AppendText(result.Msg + "\r\n");
                        }
                    }
                });
            }
            else
            {
                MessageBox.Show("请选择设备！");
            }
        }

        int maxRows = 100;
        int txtRows = 0;
        private void ShowErrorMsg(IEZSocket ezsocket)
        {
            Task.Run(() =>
            {
                while (ezsocket.errMsg.TryTake(out string msg))
                {
                    if (txtErrorMsg.InvokeRequired)
                    {
                        txtErrorMsg.Invoke(new MethodInvoker(() =>
                        {
                            txtErrorMsg.AppendText(msg + "\r\n");
                        }));
                    }
                    else
                    {
                        txtErrorMsg.AppendText(msg + "\r\n");
                    }
                    txtRows++;
                    if (txtRows >= maxRows)
                    {
                        if (txtErrorMsg.InvokeRequired)
                        {
                            txtErrorMsg.Invoke(new MethodInvoker(() =>
                            {
                                txtErrorMsg.Text = txtErrorMsg.Text.Substring(txtErrorMsg.Text.IndexOf("\n"));
                            }));
                        }
                        else
                        {
                            txtErrorMsg.Text = txtErrorMsg.Text.Substring(txtErrorMsg.Text.IndexOf("\n"));
                        }
                    }
                }
            });
        }
    }
}
