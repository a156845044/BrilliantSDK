using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 对象资源管理器窗体容器
    /// </summary>
    [Guid("b31fc0e7-11a2-4a46-a80d-b93cabf5c661")]
    public class WinDBMgr : ToolWindowPane
    {
        private FrmDBMgr frmDBMgr;

        public WinDBMgr()
            : base(null)
        {
            this.Caption = Resources.FrmDBmgrTitle;
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;
            this.frmDBMgr = FrmDBMgr.Instance;
        }

        public override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                return frmDBMgr;
            }
        }
    }

    /// <summary>
    /// 模板资源管理器窗体容器
    /// </summary>
    [Guid("B6BFAA2E-732E-411E-AF02-A4F587F5ADD6")]
    public class WinTemplateMgr : ToolWindowPane
    {
        private FrmTemplateMgr frmTemplateMgr;

        public WinTemplateMgr()
            : base(null)
        {
            this.Caption = Resources.FrmTemplateMgrTitle;
            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;
            this.frmTemplateMgr = FrmTemplateMgr.Instance;
        }

        public override System.Windows.Forms.IWin32Window Window
        {
            get
            {
                return frmTemplateMgr;
            }
        }
    }
}
