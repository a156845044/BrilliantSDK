using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.CheckBoxDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Disabled")]
    [ToolboxData("<{0}:CheckBox runat=\"server\"></{0}:CheckBox>")]
    [ToolboxBitmap(typeof(CheckBox), "Resource.CheckBox.bmp")]
    [Description("复选框控件")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class CheckBox : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否禁用按钮")]
        public bool Disabled
        {
            get { return (bool)JsonState["disabled"]; }
            set { JsonState["disabled"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否只读")]
        public bool Readonly
        {
            get { return (bool)JsonState["readonly"]; }
            set { JsonState["readonly"] = value; }
        }

        public string Text
        {
            get { return (string)JsonState["text"]; }
            set { JsonState["text"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerCheckBox({1});", this.ClientID, JsonState.Serialize());
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write(this.Text);
            base.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
