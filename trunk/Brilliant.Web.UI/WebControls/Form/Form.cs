using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.FormDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("InputWidth")]
    [ToolboxData("<{0}:Form runat=\"server\"></{0}:Form>")]
    [ToolboxBitmap(typeof(Form), "Resource.Form.bmp")]
    [Description("表单控件")]
    public class Form : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(180)]
        [Description("控件宽度")]
        public int? InputWidth
        {
            get { return (int)JsonState["inputWidth"]; }
            set { JsonState["inputWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(90)]
        [Description("标签宽度")]
        public int? LabelWidth
        {
            get { return (int)JsonState["labelWidth"]; }
            set { JsonState["labelWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(40)]
        [Description("间隔宽度")]
        public int? Space
        {
            get { return (int)JsonState["space"]; }
            set { JsonState["space"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("：")]
        [Description("标签和控件的分隔符")]
        public string RightToken
        {
            get { return (string)JsonState["rightToken"]; }
            set { JsonState["rightToken"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("left")]
        [Description("标签对齐方式")]
        public string LabelAlign
        {
            get { return (string)JsonState["labelAlign"]; }
            set { JsonState["labelAlign"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("left")]
        [Description("控件对齐方式")]
        public string Align
        {
            get { return (string)JsonState["align"]; }
            set { JsonState["align"] = value; }
        }

        //public string[] Fields { get; set; }

        [Category(CategoryName.OPTIONS)]
        [Description("创建的表单元素是否附加ID")]
        public string AppendID
        {
            get { return (string)JsonState["appendID"]; }
            set { JsonState["appendID"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("")]
        public string PrefixID
        {
            get { return (string)JsonState["prefixID"]; }
            set { JsonState["prefixID"] = value; }
        }

        //public function ToJSON

        [Category(CategoryName.OPTIONS)]
        [Description("Label样式")]
        public string LabelCss
        {
            get { return (string)JsonState["labelCss"]; }
            set { JsonState["labelCss"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("Field样式")]
        public string FieldCss
        {
            get { return (string)JsonState["fieldCss"]; }
            set { JsonState["fieldCss"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("Space样式")]
        public string SpaceCss
        {
            get { return (string)JsonState["spaceCss"]; }
            set { JsonState["spaceCss"] = value; }
        }

        //public string[] buttons

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否只读")]
        public bool Readonly
        {
            get { return (bool)JsonState["readonly"]; }
            set { JsonState["readonly"] = value; }
        }

        //public object editors

        //public object validate

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("不设置validate")]
        public bool UnSetValidateAttr
        {
            get { return (bool)JsonState["unSetValidateAttr"]; }
            set { JsonState["unSetValidateAttr"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(0)]
        [Description("宽度")]
        public int? Width
        {
            get { return (int)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        //public object Tab

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerForm({1});", this.ClientID, JsonState.Serialize());
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
