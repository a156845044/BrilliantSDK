using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Brilliant.ORM;

namespace Brilliant.DemoForm
{
    public partial class Form1 : Form
    {
        private PersonBiz personBiz = new PersonBiz();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindPersonList();
        }

        private void BindPersonList()
        {
            this.dgvResult.DataSource = personBiz.GetList();
            this.txtJson.Text = personBiz.GetJsonList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PersonInfo model = new PersonInfo();
            model.Age = Convert.ToInt32(this.txtAge.Text);
            model.Id = this.txtId.Text.Trim();
            model.Name = this.txtName.Text.Trim();
            model.Sex = this.txtSex.Text.Trim();
            if (personBiz.Add(model))
            {
                MessageBox.Show("添加成功!");
                BindPersonList();
            }
        }
    }
}
