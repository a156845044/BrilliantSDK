using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace Brilliant.Web.UI
{
    public class ScriptManager
    {
        private Page _page;

        public Page Page
        {
            get { return _page; }
        }

        private List<ScriptBlock> _startupScriptBlockList = new List<ScriptBlock>();
        public List<ScriptBlock> StartupScriptBlockList
        {
            get { return _startupScriptBlockList; }
            set { _startupScriptBlockList = value; }
        }

        public ScriptManager()
        {
            _page = HttpContext.Current.Handler as Page;
            _page.PreRenderComplete += new EventHandler(Page_PreRenderComplete);
        }

        protected void Page_PreRenderComplete(object sender, EventArgs e)
        {
            string script = GetScripts();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "page_startup_script", script, true);
        }

        public static ScriptManager Instance
        {
            get
            {
                ScriptManager sm = HttpContext.Current.Items["ScriptManager"] as ScriptManager;
                if (sm == null)
                {
                    sm = new ScriptManager();
                    HttpContext.Current.Items["ScriptManager"] = sm;
                }

                return sm;
            }
        }

        public bool IsStartupScriptExist(Control control)
        {
            foreach (ScriptBlock cs in _startupScriptBlockList)
            {
                if (cs.Control == control)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddStartupScript(Control control, string script)
        {
            ScriptBlock sb = new ScriptBlock() { Control = control, Script = script };
            _startupScriptBlockList.Add(sb);
        }

        public void AddExtraScript(string script)
        {
            AddStartupScript(null, script);
        }

        public void RemoveStartupScript(Control control)
        {
            for (int i = 0; i < _startupScriptBlockList.Count; i++)
            {
                if (_startupScriptBlockList[i].Control == control)
                {
                    _startupScriptBlockList.RemoveAt(i);
                    break;
                }
            }
        }

        public ScriptBlock GetStartupScript(Control control)
        {
            foreach (ScriptBlock cs in _startupScriptBlockList)
            {
                if (cs.Control == control)
                {
                    return cs;
                }
            }

            return null;
        }

        public string Rerender()
        {
            List<Control> list = new List<Control>();
            FindControl(list, Page.Controls);
            ControlBase cb = null;
            foreach (Control ctrl in list)
            {
                cb = ctrl as ControlBase;
                cb.OnSerialize();
            }
            return GetScripts();
        }

        private string GetScripts()
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("$(function (){");
            //sbScript.Append("__initPage();");
            foreach (ScriptBlock sb in _startupScriptBlockList)
            {
                sbScript.Append(sb.Script);
            }
            sbScript.Append("});");
            return sbScript.ToString();
        }

        private void FindControl(List<Control> list, ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                if (ctrl.Controls.Count > 0)
                {
                    FindControl(list, ctrl.Controls);
                }
                else
                {
                    if (ctrl is ControlBase)
                    {
                        list.Add(ctrl);
                    }
                }
            }
        }
    }
}
