using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.CheckBoxListDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("RowSize")]
    [ToolboxData("<{0}:CheckBoxList runat=\"server\"></{0}:CheckBoxList>")]
    [ToolboxBitmap(typeof(CheckBoxList), "Resource.CheckBoxList.bmp")]
    [Description("复选框列表控件")]
    public class CheckBoxList : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(3)]
        [Description("每行显示表单数")]
        public int RowSize
        {
            get { return (int)JsonState["rowSize"]; }
            set { JsonState["rowSize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("id")]
        [Description("值成员字段名")]
        public string DataValueField
        {
            get { return (string)JsonState["valueField"]; }
            set { JsonState["valueField"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("text")]
        [Description("显示成员字段名")]
        public string DataTextField
        {
            get { return (string)JsonState["textField"]; }
            set { JsonState["textField"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("值保存表单隐藏域")]
        public string DataValueFieldID
        {
            get { return (string)JsonState["valueFieldID"]; }
            set { JsonState["valueFieldID"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("隐藏域css")]
        public string DataValueFieldCssClass
        {
            get { return (string)JsonState["valueFieldCssClass"]; }
            set { JsonState["valueFieldCssClass"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("复选框name")]
        public string Name
        {
            get { return (string)JsonState["name"]; }
            set { JsonState["name"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(";")]
        [Description("分隔符")]
        public string Split
        {
            get { return (string)JsonState["split"]; }
            set { JsonState["split"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("异步数据请求参数")]
        public string Parms
        {
            get { return (string)JsonState["parms"]; }
            set { JsonState["parms"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("异步数据请求地址")]
        public string Url
        {
            get { return (string)JsonState["url"]; }
            set { JsonState["url"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("附加className")]
        public string CssClass
        {
            get { return (string)JsonState["css"]; }
            set { JsonState["css"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("初始化值")]
        public string Value
        {
            get { return (string)JsonState["value"]; }
            set { JsonState["value"] = value; }
        }

        private object _dataSource;

        [DefaultValue(null)]
        [Description("数据源")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            set
            {
                _dataSource = value;
            }
            get
            {
                return _dataSource;
            }
        }

        public override void DataBind()
        {
            JsonState["data"] = _dataSource;
            base.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerCheckBoxList({1});", this.ClientID, JsonState.Serialize());
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
