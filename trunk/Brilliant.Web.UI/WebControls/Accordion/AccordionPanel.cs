using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("BrilliantUI.Design.AccordionPanelDesigner, BrilliantUI.Design")]
    [ToolboxData("<{0}:AccordionPanel Title=\"AccordionPanel\" runat=\"server\"></{0}:AccordionPanel>")]
    [ToolboxBitmap(typeof(AccordionPanel), "Resource.Accordion.bmp")]
    [Description("手风琴面板控件")]
    [ParseChildren(false)]
    [PersistChildren(true)]
    public class AccordionPanel : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue("AccordionPanel")]
        [Description("标题")]
        public string Title
        {
            get { return (string)JsonState["title"]; }
            set { JsonState["title"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
