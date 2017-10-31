using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.WindowDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:Window runat=\"server\"></{0}:Window>")]
    [ToolboxBitmap(typeof(Window), "Resource.Window.bmp")]
    [Description("数据网格控件")]
    public class Window : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示关闭按钮")]
        public bool ShowClose
        {
            get { return (bool)JsonState["showClose"]; }
            set { JsonState["showClose"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示最大化按钮")]
        public bool ShowMax
        {
            get { return (bool)JsonState["showMax"]; }
            set { JsonState["showMax"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示折叠按钮")]
        public bool ShowToggle
        {
            get { return (bool)JsonState["showToggle"]; }
            set { JsonState["showToggle"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示最小化按钮")]
        public bool ShowMin
        {
            get { return (bool)JsonState["showMin"]; }
            set { JsonState["showMin"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("window")]
        [Description("标题")]
        public string Title
        {
            get { return (string)JsonState["title"]; }
            set { JsonState["title"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("load方式")]
        public bool IsLoad
        {
            get { return (bool)JsonState["load"]; }
            set { JsonState["load"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否模态窗口")]
        public bool IsModal
        {
            get { return (bool)JsonState["modal"]; }
            set { JsonState["modal"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerWindow({1});", this.ClientID, JsonState.Serialize());
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
