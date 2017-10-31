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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL sql = SQL.Build("SELECT * FROM Person").Limit(10, 2);
            this.richTextBox1.Text = sql.ToString();
        }
    }
}
