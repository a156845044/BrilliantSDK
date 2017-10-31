using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("BrilliantUI.Design.LayoutPanelDesigner, BrilliantUI.Design")]
    [ToolboxData("<{0}:LayoutPanel Title=\"LayoutPanel\" runat=\"server\"></{0}:LayoutPanel>")]
    [ToolboxBitmap(typeof(AccordionPanel), "Resource.LayoutPanel.bmp")]
    [Description("布局面板")]
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class LayoutPanel : ControlBase
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
        [DefaultValue(LayoutPosition.Center)]
        [Description("面板位置")]
        public LayoutPosition Position
        {
            get { return JsonState["position"] == null ? LayoutPosition.Center : (LayoutPosition)JsonState["position"]; }
            set { JsonState["position"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
            writer.AddAttribute("position", this.Position.ToString().ToLower());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
