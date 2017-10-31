using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.ComboBoxDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("InitText")]
    [ToolboxData("<{0}:ComboBox runat=\"server\"></{0}:ComboBox>")]
    [ToolboxBitmap(typeof(ComboBox), "Resource.ComboBox.bmp")]
    [Description("下拉框控件")]
    public class ComboBox : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否调整大小")]
        public bool Resize
        {
            get { return (bool)JsonState["resize"]; }
            set { JsonState["resize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否多选")]
        public bool IsMultiSelect
        {
            get { return (bool)JsonState["isMultiSelect"]; }
            set { JsonState["isMultiSelect"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否显示复选框")]
        public bool IsShowCheckBox
        {
            get { return (bool)JsonState["isShowCheckBox"]; }
            set { JsonState["isShowCheckBox"] = value; }
        }

        private ComboBoxColumnCollection _columns;

        [Category(CategoryName.OPTIONS)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("表格参数")]
        public ComboBoxColumnCollection Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = new ComboBoxColumnCollection();
                }
                return _columns;
            }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("下拉框宽度")]
        public int? SelectBoxWidth
        {
            get { return (int)JsonState["selectBoxWidth"]; }
            set { JsonState["selectBoxWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("下拉框高度")]
        public int? SelectBoxHeight
        {
            get { return (int)JsonState["selectBoxHeight"]; }
            set { JsonState["selectBoxHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("初始化值")]
        public string InitValue
        {
            get { return (string)JsonState["initValue"]; }
            set { JsonState["initValue"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("初始化文本")]
        public string InitText
        {
            get { return (string)JsonState["initText"]; }
            set { JsonState["initText"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("id")]
        [Description("值字段名")]
        public string ValueField
        {
            get { return (string)JsonState["valueField"]; }
            set { JsonState["valueField"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("text")]
        [Description("文本字段名")]
        public string TextField
        {
            get { return (string)JsonState["textField"]; }
            set { JsonState["textField"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(null)]
        [Description("隐藏域元素的ID")]
        public string ValueFieldID
        {
            get { return (string)JsonState["valueFieldID"]; }
            set { JsonState["valueFieldID"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否以动画的形式显示")]
        public bool Slide
        {
            get { return (bool)JsonState["slide"]; }
            set { JsonState["slide"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(";")]
        [Description("分隔符")]
        public string Split
        {
            get { return (string)JsonState["split"]; }
            set { JsonState["split"] = value; }
        }

        //public object Data { get; set; }

        //public object Tree { get; set; }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("只对树叶节点有效")]
        public bool TreeLeafOnly
        {
            get { return (bool)JsonState["treeLeafOnly"]; }
            set { JsonState["treeLeafOnly"] = value; }
        }

        //public object Grid { get; set; }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("失去焦点时隐藏")]
        public bool HideOnLoseFocus
        {
            get { return (bool)JsonState["hideOnLoseFocus"]; }
            set { JsonState["hideOnLoseFocus"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("数据源URL(需返回JSON)")]
        public string Url
        {
            get { return (string)JsonState["url"]; }
            set { JsonState["url"] = value; }
        }

        //public object Render { get; set; }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("选择框是否在附加到body,并绝对定位")]
        public bool Absolute
        {
            get { return (bool)JsonState["absolute"]; }
            set { JsonState["absolute"] = value; }
        }

        //public object condition { get; set; }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否取消选择")]
        public bool Cancelable
        {
            get { return (bool)JsonState["cancelable"]; }
            set { JsonState["cancelable"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("附加className")]
        public string Css
        {
            get { return (string)JsonState["css"]; }
            set { JsonState["css"] = value; }
        }

        //public object Parms { get; set; }

        //public function RenderItem{ get; set;}

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("自动完成")]
        public bool Autocomplete
        {
            get { return (bool)JsonState["autocomplete"]; }
            set { JsonState["autocomplete"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否只读")]
        public bool Readonly
        {
            get { return (bool)JsonState["readonly"]; }
            set { JsonState["readonly"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("post")]
        [Description("ajax Type")]
        public string AjaxType
        {
            get { return (string)JsonState["ajaxType"]; }
            set { JsonState["ajaxType"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("隐藏域css")]
        public string ValueFieldCssClass
        {
            get { return (string)JsonState["valueFieldCssClass"]; }
            set { JsonState["valueFieldCssClass"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("失去焦点(表格)时隐藏")]
        public bool HideGridOnLoseFocus
        {
            get { return (bool)JsonState["hideGridOnLoseFocus"]; }
            set { JsonState["hideGridOnLoseFocus"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("下拉框总是显示在上方")]
        public bool AlwayShowInTop
        {
            get { return (bool)JsonState["alwayShowInTop"]; }
            set { JsonState["alwayShowInTop"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("(空)")]
        [Description("空行的数据项")]
        public string EmptyText
        {
            get { return (string)JsonState["emptyText"]; }
            set { JsonState["emptyText"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("新增")]
        [Description("新增按钮的显示文本")]
        public string AddRowButton
        {
            get { return (string)JsonState["addRowButton"]; }
            set { JsonState["addRowButton"] = value; }
        }

        //public function AddRowButtonClick{ get; set;}

        [Category(CategoryName.OPTIONS)]
        [Description("右侧图标图片")]
        public string TriggerIcon
        {
            get { return (string)JsonState["triggerIcon"]; }
            set { JsonState["triggerIcon"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("自动完成是否匹配字符高亮显示")]
        public bool HighLight
        {
            get { return (bool)JsonState["highLight"]; }
            set { JsonState["highLight"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerComboBox({1});", this.ClientID, JsonState.Serialize());
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Select);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
