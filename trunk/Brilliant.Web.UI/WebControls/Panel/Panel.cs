using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.PanelDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:Panel runat=\"server\"></{0}:Panel>")]
    [ToolboxBitmap(typeof(Panel), "Resource.Panel.bmp")]
    [Description("面板控件")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Panel : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(300)]
        [Description("高度")]
        public int? Height
        {
            get { return (int)JsonState["height"]; }
            set { JsonState["height"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(400)]
        [Description("宽度")]
        public int? Width
        {
            get { return (int)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("Panel")]
        [Description("标题")]
        public string Title
        {
            get { return (string)JsonState["title"]; }
            set { JsonState["title"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("内容")]
        public string Content
        {
            get { return (string)JsonState["content"]; }
            set { JsonState["content"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("远程内容Url")]
        public string Url
        {
            get { return (string)JsonState["url"]; }
            set { JsonState["url"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("创建iframe时,作为iframe的name和id")]
        public string FrameName
        {
            get { return (string)JsonState["frameName"]; }
            set { JsonState["frameName"] = value; }
        }

        //public object data;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否显示关闭按钮")]
        public bool ShowClose
        {
            get { return (bool)JsonState["showClose"]; }
            set { JsonState["showClose"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("")]
        public bool ShowToggle
        {
            get { return (bool)JsonState["showToggle"]; }
            set { JsonState["showToggle"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("左侧按钮")]
        public string Icon
        {
            get { return (string)JsonState["icon"]; }
            set { JsonState["icon"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("url传承(支持函数)")]
        public string UrlParms
        {
            get { return (string)JsonState["urlParms"]; }
            set { JsonState["urlParms"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("显示刷新按钮")]
        public bool ShowRefresh
        {
            get { return (bool)JsonState["showRefresh"]; }
            set { JsonState["showRefresh"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerPanel({1});", this.ClientID, JsonState.Serialize());
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
