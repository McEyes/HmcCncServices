
using EZSocketNc.Extensions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmcCncServices
{
    public partial class Form1 : Form
    {
        private Timer Timer2;


        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Timer2 = new Timer();
            Timer2.Interval = 1000;
            Timer2.Tick += Timer2_Tick;
            Timer2.Enabled = false;
        }


        protected override void OnClosed(EventArgs e)
        {
            var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
            if (socket != null)
            {
                socket.StopMonitor();
                socket.Close();
            }
            base.OnClosed(e);
        }


        private void btnConn_Click(object sender, EventArgs e)
        {
            Conn(txtIp.Text.Trim(), textBox1.Text.Trim());

        }
        private void OpenStatus_Click(object sender, EventArgs e)
        {
            if (OpenStatus.Text == "启动")
            {
                var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
                socket.StartMonitor();
                OpenStatus.Text = socket.EquipmentInfo.RunStatus == EZSocketNc.EquipmentStatus.Running ? "运行" : "启动";
                Timer2.Start();
            }
            else
            {
                var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
                socket.StopMonitor();
                Timer2.Stop();
                OpenStatus.Text = "启动";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void Timer2_Tick(object sender, EventArgs e) // 低速定时器中断
        {
            var socket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
            txtProgramName.Text = socket.EquipmentInfo.ProgramName;
            PrgNo.Text = socket.EquipmentInfo.ProgramBlock;
            SequenceNo.Text = socket.EquipmentInfo.SequenceNumber.ToString();
            txtEquipmentInfo.Text = socket.EquipmentInfo.ToJSON();

            if (!string.IsNullOrWhiteSpace(socket.EquipmentInfo.ProgramBlock))
            {
                var PrgList = socket.EquipmentInfo.ProgramBlock.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                CurrentPrg.Items.Clear();
                foreach (var item in PrgList)
                    CurrentPrg.Items.Add(item);
            }

            // 获取当前坐标位置 CurentPosition
            txtX.Text = socket.EquipmentInfo.CurrentPosition.X.ToString("0.000");
            txtY.Text = socket.EquipmentInfo.CurrentPosition.Y.ToString("0.000");
            txtZ.Text = socket.EquipmentInfo.CurrentPosition.Z.ToString("0.000");
            txtWorkX.Text = socket.EquipmentInfo.WorkPosition.X.ToString("0.000");
            txtWorkY.Text = socket.EquipmentInfo.WorkPosition.Y.ToString("0.000");
            txtWorkZ.Text = socket.EquipmentInfo.WorkPosition.Z.ToString("0.000");
            txtMachineX.Text = socket.EquipmentInfo.MachinePosition.X.ToString("0.000");
            txtMachineY.Text = socket.EquipmentInfo.MachinePosition.Y.ToString("0.000");
            txtMachineZ.Text = socket.EquipmentInfo.MachinePosition.Z.ToString("0.000");

            while (socket.errMsg.TryTake(out string item))
            {
                listBox1.Items.Add(item);
            }
        }

        private EZSocketNc.EZNc.EZSocketConfig config;
        public void Conn(string ip, string hostname)
        {
            config = new EZSocketNc.EZNc.EZSocketConfig() { Ip = ip, HostName = hostname };
            var ezSocket = EZSocketNc.EZNc.EZSocketFactory.CreateEZSocket(config);
            var msg = ezSocket.Conn();
            if (!string.IsNullOrWhiteSpace(msg.Msg)) listBox1.Items.Add(msg.Msg);
            if (msg.Success)
            {
                btnConn.Text = "断开";
                listBox1.Items.Add($"链接成功：[{msg.Code}]{msg.Msg}");
                OpenStatus.Enabled = true;
            }
            else
            {
                ezSocket.StopMonitor();
                msg = ezSocket.Close();
                Timer2.Stop();
                if (!string.IsNullOrWhiteSpace(msg.Msg)) listBox1.Items.Add(msg.Msg);
                else listBox1.Items.Add($"链接失败：[{msg.Code}]{msg.Msg}");
                btnConn.Text = "链接";
                OpenStatus.Text = "启动";
                OpenStatus.Enabled = false;
            }
            while (ezSocket.errMsg.TryTake(out string item))
            {
                listBox1.Items.Add(item);
            }
            return;
        }

    }
}