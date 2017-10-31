using Brilliant.Service.WX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WXDemo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnGetDept_Click(object sender, EventArgs e)
        {
            this.cbDept.DataSource = WXAPI.GetDepts();
            this.cbDept.DisplayMember = "name";
            this.cbDept.ValueMember = "id";
        }

        public void BindDeptUserList()
        {
            int deptId = (this.cbDept.SelectedItem as DeptInfo).id;
            this.lbUsers.DataSource = WXAPI.GetDeptUsers(deptId);
            this.lbUsers.DisplayMember = "name";
            this.lbUsers.ValueMember = "userid";
        }

        private void cbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDeptUserList();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }
    }
}
