using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.CommandBars;
using Brilliant.Data;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 扩展包入口
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(WinDBMgr))]
    [ProvideToolWindow(typeof(WinTemplateMgr))]
    [Guid(GuidList.guidProjectStudioPkgString)]
    public sealed class ProjectStudioPackage : Package
    {
        private CommandBarEvents projectGenCode;

        /// <summary>
        /// 构造器
        /// </summary>
        public ProjectStudioPackage()
        {
            //Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }

        /// <summary>
        /// 初始化扩展包
        /// </summary>
        protected override void Initialize()
        {
            //Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            //根据ProjectStudio.vsct文件创建主菜单
            CreateMenu();

            //创建右键菜单
            CreateCommandBar();
        }

        /// <summary>
        /// 点击【对象资源管理器】菜单
        /// </summary>
        private void MenuItemDBMgr(object sender, EventArgs e)
        {
            CreateToolWindow(typeof(WinDBMgr));
        }

        /// <summary>
        /// 点击【模板资源管理器】菜单
        /// </summary>
        private void MenuItemTemplateMgr(object sender, EventArgs e)
        {
            CreateToolWindow(typeof(WinTemplateMgr));
        }

        /// <summary>
        /// 创建停靠窗体
        /// </summary>
        private void CreateToolWindow(Type winType)
        {
            ToolWindowPane window = this.FindToolWindow(winType, 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.ErrorCanNotCreateWin);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        /// <summary>
        /// 创建主菜单
        /// </summary>
        private void CreateMenu()
        {
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID mcidDBMgr = new CommandID(GuidList.guidProjectStudioCmdSet, (int)PkgCmdIDList.cmdidDBMgr);
                MenuCommand miDBMgr = new MenuCommand(MenuItemDBMgr, mcidDBMgr);
                mcs.AddCommand(miDBMgr);

                CommandID mcidTemplateMgr = new CommandID(GuidList.guidProjectStudioCmdSet, (int)PkgCmdIDList.cmdidTemplateMgr);
                MenuCommand miTemplateMgr = new MenuCommand(MenuItemTemplateMgr, mcidTemplateMgr);
                mcs.AddCommand(miTemplateMgr);
            }
        }

        /// <summary>
        /// 创建右键菜单
        /// </summary>
        private void CreateCommandBar()
        {
            string name = "创建代码(&C)";
            CommandBars cmdBars = Com.VS.CommandBars as CommandBars;
            CommandBar projectBar = cmdBars["Project"];
            CommandBarControl projectCmdBar = projectBar.Controls.Add(MsoControlType.msoControlButton, 1, "", 2, true);
            projectCmdBar.Tag = name;
            projectCmdBar.Caption = name;
            projectCmdBar.TooltipText = name;
            projectGenCode = Com.VS.Events.get_CommandBarEvents(projectCmdBar) as CommandBarEvents;
            projectGenCode.Click += new _dispCommandBarControlEvents_ClickEventHandler(genCode_Click);

            //CommandBar projectNodeBar = cmdBars["Project Node"];
            //CommandBarControl projectNodeCmdBar = projectNodeBar.Controls.Add(MsoControlType.msoControlButton);
            //projectNodeCmdBar.Tag = name;
            //projectNodeCmdBar.Caption = name;
            //projectNodeCmdBar.TooltipText = name;
            //projectNodeGenCode = Com.VS.Events.get_CommandBarEvents(projectNodeCmdBar) as CommandBarEvents;
            //projectNodeGenCode.Click += new _dispCommandBarControlEvents_ClickEventHandler(genCode_Click);
        }

        /// <summary>
        /// 右键菜单事件
        /// </summary>
        private void genCode_Click(object CommandBarControl, ref bool Handled, ref bool CancelDefault)
        {
            if (DBContext.CurrentConnection == null)
            {
                FrmDBLogin.Instance.ShowDialog();
                return;
            }
            FrmCreateFile frmCreateFile = new FrmCreateFile();
            frmCreateFile.ShowDialog();
        }
    }
}
