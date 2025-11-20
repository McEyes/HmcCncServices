using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmcCncServices
{
    public partial class AddEzCnc : Form
    {
        public EZSocketNc.EZNc.EZSocketConfig CncConfig;
        public AddEzCnc()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIp.Text))
            {
                MessageBox.Show("请输入IP地址");
                return;
            }
            CncConfig = new EZSocketNc.EZNc.EZSocketConfig()
            {
                Ip = txtIp.Text.Trim(),
                SystemType = EZSocketNc.EZNc.EZSystemType.CNC_M800M | EZSocketNc.EZNc.EZSystemType.NC_SYS_MULTI,
                MachineNo = int.Parse(txtMachine.Text.Trim())
            };
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
