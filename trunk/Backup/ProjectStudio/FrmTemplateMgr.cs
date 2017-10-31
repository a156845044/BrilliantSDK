using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.CommandBars;

namespace Brilliant.ProjectStudio
{
    public partial class FrmTemplateMgr : Form
    {
        private static readonly FrmTemplateMgr instance = new FrmTemplateMgr(); //当前窗体实例

        public static FrmTemplateMgr Instance
        {
            get { return FrmTemplateMgr.instance; }
        }

        public FrmTemplateMgr()
        {
            InitializeComponent();
        }

        private void FrmTemplateMgr_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            CommandBars bars = Com.VS.CommandBars as CommandBars;
            List<Info> list = new List<Info>();
            foreach (CommandBar bar in bars)
            {
                Info info = new Info();
                info.Name = bar.Name;
                info.Text = bar.NameLocal;
                list.Add(info);
            }
            List<Info> infoList = list.OrderBy(item => item.Name).ToList();
            foreach (Info info in infoList)
            {
                this.richTextBox1.AppendText(String.Format("{0}:{1}\r\n", info.Name, info.Text));
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            CommandBars cmdBars = Com.VS.CommandBars as CommandBars;
            CommandBar projectBar = cmdBars["Project and Solution Context Menus"];
            foreach (CommandBarControl ctrl in projectBar.Controls)
            {
                this.richTextBox1.AppendText(ctrl.accName + "\r\n");
            }
        }
    }

    public class Info
    {
        public string Name { get; set; }

        public string Text { get; set; }
    }
}
