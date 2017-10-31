using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("BrilliantUI.Design.TabPanelDesigner, BrilliantUI.Design")]
    [ToolboxData("<{0}:TabPanel Title=\"TabPanel\" runat=\"server\"></{0}:TabPanel>")]
    [ToolboxBitmap(typeof(TabPanel), "Resource.TabPanel.bmp")]
    [Description("标签选项卡面板")]
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class TabPanel : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue("AccordionPanel")]
        [Description("标题")]
        public string Title
        {
            get { return (string)JsonState["title"]; }
            set { JsonState["title"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("AccordionPanel")]
        [Description("高度")]
        public string Height
        {
            get { return (string)JsonState["height"]; }
            set { JsonState["height"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否显示关闭按钮")]
        public bool? ShowClose
        {
            get { return (bool?)JsonState["showClose"]; }
            set { JsonState["showClose"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height);
            writer.AddAttribute("showClose", this.ShowClose == false ? "" : this.ShowClose.ToString().ToLower());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
