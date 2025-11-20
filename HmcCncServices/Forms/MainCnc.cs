using AoiAdapter.Handlers;

using EZSocketNc.Commons;
using EZSocketNc.Configs;
using EZSocketNc.Extensions;
using EZSocketNc.EZNc;
using EZSocketNc.Mqtts;
using EZSocketNc.Mqtts.Dtos;

using HmcCncServices.Configs;
using EZSocketNc.Interface;
using EZSocketNc.Siemens;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HmcCncServices.Forms;
using System.Net.Sockets;

namespace HmcCncServices
{
    public partial class MainCnc : Form
    {
        private readonly string CurrNTID = Environment.UserName;
        private readonly string MachineName = Environment.MachineName;
        /// <summary>
        /// 是否退出系统
        /// </summary>
        private bool _quitFlag = false;
        public List<IEZSocket> EzCncList;
        JAgentServiceProtocol.JAgentContext jagentContext;
        private List<CncServiceConfig> _adapterConfig;
        public MainCnc(JAgentServiceProtocol.JAgentContext jagentContext, List<CncServiceConfig> adapterConfig)
        {
            InitializeComponent();
            EzCncList = new List<IEZSocket>();
            this.jagentContext = jagentContext;
            _adapterConfig = adapterConfig;
            InitSocket();
        }

        public MainCnc()
        {
            InitializeComponent();
            EzCncList = new List<IEZSocket>();
            GetEtcd();
        }

        private void GetEtcd()
        {
            var etcdPath = AppDomain.CurrentDomain.BaseDirectory + "etcd.json";
            if (System.IO.File.Exists(etcdPath))
            {
                _adapterConfig = System.IO.File.ReadAllText(etcdPath).FromJSON<List<CncServiceConfig>>();
                EZSocketNc.Utils.LogHelper.Info($"通过etcd.json获取到服务配置信息：{_adapterConfig.ToJSON()}");
                InitSocket(true);
            }
            else
            {
                EZSocketNc.Utils.LogHelper.Error(etcdPath);
            }
        }

        private void InitSocket(bool startmonitor = false)
        {
            foreach (var config in _adapterConfig)
            {
                foreach (var item in config.CncDevices)
                {
                    if (item.Enable == true && item.Socket != null)
                    {
                        item.Mqtt = config.Mqtt.Clone();
                        item.Mqtt.Id = item.Id;
                        item.Mqtt.Kind = item.Kind;
                        item.Mqtt.HostName = item.HostName;
                        var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(item);
                        EzCncList.Add(socket);

                        TreeNode node = new TreeNode(item.Socket.Ip);
                        node.Tag = socket.SocketConfig;
                        this.treeView1.Nodes.Add(node);
                    }
                }
            }
            if (startmonitor)
            {
                UpdateAdapter();
            }
        }

        private List<HmcCncHandler> _adapterList = new List<HmcCncHandler>();
        private void UpdateAdapter()
        {
            try
            {
                EZSocketNc.Utils.LogHelper.Info($"[{Name}] HmcCncAdapterService UpdateAdapter");
                _adapterList.ForEach(q =>
                {
                    EZSocketNc.Utils.LogHelper.Info($"[{Name}] HmcCncAdapterService UpdateAdapter item");
                    q.Stop();
                });
                _adapterList.Clear();
                if (_adapterConfig != null)
                {
                    _adapterConfig = _adapterConfig.Where(f => f.Enable == true && "HmcCnc".Equals(f.Kind, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    _adapterConfig.ForEach(item =>
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                EZSocketNc.Utils.LogHelper.Info($"[{Generics.HostName}][HmcCnc]启用适配器服务：{item.Name}");
                                HmcCncHandler lm = new HmcCncHandler(item);
                                _adapterList.Add(lm);
                                lm.Start();
                            }
                            catch (Exception ex)
                            {
                                EZSocketNc.Utils.LogHelper.Info($"[{Name}] HmcCncAdapterService UpdateAdapter error:{ex.Message},\r\n\t{ex.StackTrace}");
                            }
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Info($"[{Name}] HmcCncAdapterService UpdateAdapter error:{ex.Message},\r\n\t{ex.StackTrace}");
            }
        }


        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var addDialog = new AddEzCnc();
            if (addDialog.ShowDialog() == DialogResult.OK)
            {
                var ezsocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(addDialog.CncConfig);
                //ezsocket.StartMonitor();
                if (!EzCncList.Any(f => f.SocketConfig.Key == ezsocket.SocketConfig.Key))
                {
                    EzCncList.Add(ezsocket);

                    TreeNode node = new TreeNode(ezsocket.SocketConfig.Ip);
                    node.Tag = ezsocket.SocketConfig;
                    this.treeView1.Nodes.Add(node);

                    //BindDrives(EzCncList.Select(f => f.SocketConfig).ToList(), this.treeView1.Nodes);

                    LoadProducts(EzCncList.Select(f => f.EquipmentInfo).ToList());

                    while (ezsocket.errMsg.TryTake(out string msg))
                    {
                        if (!textBox1.InvokeRequired)
                        {
                            if (rowAmount > 100) textBox1.Text = textBox1.Text.Substring(textBox1.Text.IndexOf("\n"));
                            textBox1.AppendText(msg + "\r\n");
                        }
                        else
                        {
                            textBox1.BeginInvoke(new MethodInvoker(() =>
                            {
                                if (rowAmount > 100) textBox1.Text = textBox1.Text.Substring(textBox1.Text.IndexOf("\n"));
                                textBox1.AppendText(msg + "\r\n");
                            }));
                        }
                        rowAmount++;
                    }
                }
                else if (!EzCncList.Any(f => f == ezsocket))
                {
                    var data = EzCncList.FirstOrDefault(f => f.SocketConfig.Key == ezsocket.SocketConfig.Key);
                    if (data != null)
                    {
                        data.StopMonitor();
                        EzCncList.Remove(data);
                    }
                    EzCncList.Add(ezsocket);
                }
            }
        }


        private void LoadProducts(List<EZCncInfo> list)
        {
            if (!dataGridView1.InvokeRequired)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = list; // 重新绑定数据源
            }
            else
            {
                dataGridView1.BeginInvoke(new MethodInvoker(() =>
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = list;
                }));
            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void 监听ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                var config = treeView1.SelectedNode.Tag as EZSocketConfig;
                var ezSocket = EZSocketFactory.CreateEZSocket(config);
                var result = ezSocket.Conn();
                if (result.Success)
                {
                    Task.Factory.StartNew(() =>
                    {
                        ezSocket.StartMonitor();
                    });
                }
                textBox1.Text = string.Join("\r\n", ezSocket.errMsg);
            }
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                Task.Factory.StartNew(() =>
                {
                    var config = treeView1.SelectedNode.Tag as EZSocketConfig;
                    var ezSocket = EZSocketFactory.CreateEZSocket(config);
                    ezSocket.StopMonitor();
                });
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                //var config = treeView1.SelectedNode.Tag as CncDeviceConfig;
                //if (config != null)
                //{
                //    var ezSocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
                //    //停止ToolStripMenuItem.Enabled = ezSocket.EquipmentInfo.RunStatus;
                //    //监听ToolStripMenuItem.Enabled = !ezSocket.EquipmentInfo.RunStatus;
                //}
            }
        }
        int rowAmount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (TreeNode item in treeView1.Nodes)
            {
                var config = item.Tag as EZSocketConfig;
                if (config != null)
                {
                    var ezSocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
                    if (!EzCncList.Any(f => f == ezSocket))
                    {
                        var data = EzCncList.FirstOrDefault(f => f.SocketConfig.Key == ezSocket.SocketConfig.Key);
                        if (data != null)
                        {
                            data.StopMonitor();
                            EzCncList.Remove(data);
                        }
                        EzCncList.Add(ezSocket);
                    }
                    LoadProducts(EzCncList.Select(f => f.EquipmentInfo).ToList());
                    while (ezSocket.errMsg.TryTake(out string msg))
                    {
                        if (!textBox1.InvokeRequired)
                        {
                            if (rowAmount > 100) textBox1.Text = textBox1.Text.Substring(textBox1.Text.IndexOf("\n"));
                            textBox1.AppendText(msg + "\r\n");
                        }
                        else
                        {
                            textBox1.BeginInvoke(new MethodInvoker(() =>
                            {
                                if (rowAmount > 100) textBox1.Text = textBox1.Text.Substring(textBox1.Text.IndexOf("\n"));
                                textBox1.AppendText(msg + "\r\n");
                            }));
                        }
                        rowAmount++;
                    }
                }
            }
        }

        private void 文件目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDir().Show();
        }

        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            添加ToolStripMenuItem_Click(sender, e);
        }

        private void form1测试界面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().Show();
        }

        private void 目录测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            new FormDir().Show();
        }

        private void 清除日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rowAmount = 0;
            textBox1.Text = "";
        }

        private void 西门子上传文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dir = new SemensDir(this.EzCncList);
            dir.Show();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            监听ToolStripMenuItem_Click(sender, e);
        }

        public void CloseWin()
        {
            _quitFlag = true;
            _adapterList.ForEach(adapter => {
                adapter.Stop();
            });
            foreach (var item in EzCncList)
            {
                try
                {
                    item.StopMonitor();
                    item.Close();
                    item.Dispose();
                }
                catch (Exception ex)
                {
                    EZSocketNc.Utils.LogHelper.Info($"[{Name}][{item.SocketConfig.Key}] stop error:{ex.Message}");
                }
            }
            if (this.InvokeRequired && !this.IsDisposed)
                this.Invoke(new MethodInvoker(() =>
                {
                    if (!this.IsDisposed)
                    {
                        this.Close();
                        this.Dispose();
                    }
                }));
            else if (!this.IsDisposed)
            {
                this.Close();
                this.Dispose();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool result = false;
            try
            {
                _quitFlag = (keyData == (Keys.Control | Keys.Shift | Keys.Q));
                if (_quitFlag)
                {
                    timer1.Stop();
                    timer1.Interval = 100000;
                    result = true;
                    CloseWin();
                }
                else
                {
                    result = base.ProcessCmdKey(ref msg, keyData);
                }
            }
            catch (Exception ex)
            {
                EZSocketNc.Utils.LogHelper.Error($"[{MachineName}]-[{CurrNTID}]ProcessCmdKey Error:{ex.Message},{ex.StackTrace}");
            }
            return result;
        }

        private void MainCnc_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_quitFlag)
            {
                e.Cancel = true;
                Hide();
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
                timer1.Dispose();
                timer1 = null;
                _adapterList.ForEach(adapter => {
                    adapter.Stop();
                });
                foreach (var item in EzCncList)
                {
                    try
                    {
                        item.StopMonitor();
                        item.Close();
                        item.Dispose();
                    }
                    catch (Exception ex)
                    {
                        EZSocketNc.Utils.LogHelper.Info($"[{Name}][{item.SocketConfig.Key}] stop error:{ex.Message}");
                    }
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            this.CenterToScreen();
            Show();
            this.TopMost = false;
        }



    }
}
