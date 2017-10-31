using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;

namespace Brilliant.Web.UI
{
    /// <summary>
    /// 折叠面板
    /// </summary>
    [Designer("Brilliant.Web.UI.Design.AccordionDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Height")]
    [ToolboxData("<{0}:Accordion runat=\"server\"></{0}:Accordion>")]
    [ToolboxBitmap(typeof(Accordion), "Resource.Accordion.bmp")]
    [Description("手风琴折叠面板")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Accordion : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(300)]
        [Description("初始高度")]
        public int Height
        {
            get { return (int)JsonState["height"]; }
            set { JsonState["height"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("normal")]
        [Description("动画速度")]
        public string Speed
        {
            get { return (string)JsonState["speed"]; }
            set { JsonState["speed"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("自动调整高度")]
        public bool ChangeHeightOnResize
        {
            get { return (bool)JsonState["changeHeightOnResize"]; }
            set { JsonState["changeHeightOnResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(0)]
        [Description("高度补差")]
        public int HeightDiff
        {
            get { return (int)JsonState["heightDiff"]; }
            set { JsonState["heightDiff"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(160)]
        [Description("宽度")]
        public string Width
        {
            get { return (string)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        private AccordionPanelCollection _panels;

        [Category(CategoryName.OPTIONS)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public AccordionPanelCollection Panels
        {
            get
            {
                if (_panels == null)
                {
                    _panels = new AccordionPanelCollection(this);
                }
                return _panels;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerAccordion({1});", this.ID, JsonState.Serialize());
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, Width);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
