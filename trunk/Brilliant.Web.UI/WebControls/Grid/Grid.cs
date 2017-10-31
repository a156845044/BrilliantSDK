using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.GridDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:Grid runat=\"server\"></{0}:Grid>")]
    [ToolboxBitmap(typeof(Grid), "Resource.Grid.bmp")]
    [Description("数据网格控件")]
    public class Grid : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [Description("表格标题")]
        public string Title
        {
            get { return (string)JsonState["title"]; }
            set { JsonState["title"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("auto")]
        [Description("宽度值,支持百分比")]
        public string Width
        {
            get { return (string)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("auto")]
        [Description("高度值,支持百分比")]
        public string Height
        {
            get { return (string)JsonState["height"]; }
            set { JsonState["height"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("默认列宽度")]
        public int? ColumnWidth
        {
            get { return (int)JsonState["columnWidth"]; }
            set { JsonState["columnWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("table是否可伸缩")]
        public bool Resizable
        {
            get { return (bool)JsonState["resizable"]; }
            set { JsonState["resizable"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("ajax url")]
        public string Url
        {
            get { return (string)JsonState["url"]; }
            set { JsonState["url"] = value; }
        }

        //public object data;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否分页")]
        public bool UsePager
        {
            get { return (bool)JsonState["usePager"]; }
            set { JsonState["usePager"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(1)]
        [Description("当前页")]
        public int? PageNumber
        {
            get { return (int)JsonState["page"]; }
            set { JsonState["page"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(10)]
        [Description("每页默认的结果数")]
        public int? PageSize
        {
            get { return (int)JsonState["pageSize"]; }
            set { JsonState["pageSize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(new int[] { 10, 20, 30, 40, 50 })]
        [Description("可选择设定的每页结果数")]
        public int[] PageSizeOptions
        {
            get { return (int[])JsonState["pageSizeOptions"]; }
            set { JsonState["pageSizeOptions"] = value; }
        }

        //public object parms;

        //public BaseCollection<T> columns;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(1)]
        [Description("")]
        public int? MinColToggle
        {
            get { return (int)JsonState["minColToggle"]; }
            set { JsonState["minColToggle"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("server")]
        [Description("提交数据的方式")]
        public string DataAction
        {
            get { return (string)JsonState["dataAction"]; }
            set { JsonState["dataAction"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否显示'显示隐藏Grid'按钮")]
        public bool ShowTableToggleBtn
        {
            get { return (bool)JsonState["showTableToggleBtn"]; }
            set { JsonState["showTableToggleBtn"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("切换每页记录数是否应用ComboBox")]
        public bool SwitchPageSizeApplyComboBox
        {
            get { return (bool)JsonState["switchPageSizeApplyComboBox"]; }
            set { JsonState["switchPageSizeApplyComboBox"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许调整列宽")]
        public bool AllowAdjustColWidth
        {
            get { return (bool)JsonState["allowAdjustColWidth"]; }
            set { JsonState["allowAdjustColWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否显示复选框")]
        public bool Checkbox
        {
            get { return (bool)JsonState["checkbox"]; }
            set { JsonState["checkbox"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示'切换列层'按钮")]
        public bool AllowHideColumn
        {
            get { return (bool)JsonState["allowHideColumn"]; }
            set { JsonState["allowHideColumn"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许编辑")]
        public bool EnabledEdit
        {
            get { return (bool)JsonState["enabledEdit"]; }
            set { JsonState["enabledEdit"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("设置为false时将不会显示滚动条，高度自适应")]
        public bool IsScroll
        {
            get { return (bool)JsonState["isScroll"]; }
            set { JsonState["isScroll"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("yyyy-MM-dd")]
        [Description("默认时间显示格式")]
        public string DateFormat
        {
            get { return (string)JsonState["dateFormat"]; }
            set { JsonState["dateFormat"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否以窗口的高度为准height设置为百分比时可用")]
        public bool InWindow
        {
            get { return (bool)JsonState["inWindow"]; }
            set { JsonState["inWindow"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("__status")]
        [Description("状态名")]
        public string StatusName
        {
            get { return (string)JsonState["statusName"]; }
            set { JsonState["statusName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("post")]
        [Description("服务器提交方式")]
        public string Method
        {
            get { return (string)JsonState["method"]; }
            set { JsonState["method"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否异步")]
        public bool Async
        {
            get { return (bool)JsonState["async"]; }
            set { JsonState["async"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否固定单元格的高度")]
        public bool FixedCellHeight
        {
            get { return (bool)JsonState["fixedCellHeight"]; }
            set { JsonState["fixedCellHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(0)]
        [Description("高度补差")]
        public int? HeightDiff
        {
            get { return (int)JsonState["heightDiff"]; }
            set { JsonState["heightDiff"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("附加给Grid的类名")]
        public string CssClass
        {
            get { return (string)JsonState["cssClass"]; }
            set { JsonState["cssClass"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("Rows")]
        [Description("数据源字段名")]
        public string Root
        {
            get { return (string)JsonState["root"]; }
            set { JsonState["root"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("Total")]
        [Description("数据源记录数字段名")]
        public string Record
        {
            get { return (string)JsonState["record"]; }
            set { JsonState["record"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("page")]
        [Description("页索引参数名")]
        public string PageParmName
        {
            get { return (string)JsonState["pageParmName"]; }
            set { JsonState["pageParmName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("pagesize")]
        [Description("页记录数参数名")]
        public string PagesizeParmName
        {
            get { return (string)JsonState["pagesizeParmName"]; }
            set { JsonState["pagesizeParmName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("sortname")]
        [Description("页排序列名")]
        public string SortnameParmName
        {
            get { return (string)JsonState["sortnameParmName"]; }
            set { JsonState["sortnameParmName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("sortorder")]
        [Description("页排序方向")]
        public string SortorderParmName
        {
            get { return (string)JsonState["sortorderParmName"]; }
            set { JsonState["sortorderParmName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许取消选择行")]
        public bool AllowUnSelectRow
        {
            get { return (bool)JsonState["allowUnSelectRow"]; }
            set { JsonState["allowUnSelectRow"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否附加奇偶行效果行")]
        public bool AlternatingRow
        {
            get { return (bool)JsonState["alternatingRow"]; }
            set { JsonState["alternatingRow"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("l-grid-row-over")]
        [Description("鼠标经过行时的样式")]
        public string MouseoverRowCssClass
        {
            get { return (string)JsonState["mouseoverRowCssClass"]; }
            set { JsonState["mouseoverRowCssClass"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许排序")]
        public bool EnabledSort
        {
            get { return (bool)JsonState["enabledSort"]; }
            set { JsonState["enabledSort"] = value; }
        }

        //public function rowAttrRender;

        [Category(CategoryName.OPTIONS)]
        [Description("分组列名")]
        public string GroupColumnName
        {
            get { return (string)JsonState["groupColumnName"]; }
            set { JsonState["groupColumnName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("分组列显示名字")]
        public string GroupColumnDisplay
        {
            get { return (string)JsonState["groupColumnDisplay"]; }
            set { JsonState["groupColumnDisplay"] = value; }
        }

        //public function groupRender;

        [Category(CategoryName.OPTIONS)]
        [Description("统计行(全部数据)")]
        public string TotalRender
        {
            get { return (string)JsonState["totalRender"]; }
            set { JsonState["totalRender"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("初始化是是否不加载")]
        public bool DelayLoad
        {
            get { return (bool)JsonState["delayLoad"]; }
            set { JsonState["delayLoad"] = value; }
        }

        //public function where;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("复选框模式时，是否只允许点击复选框才能选择行")]
        public bool SelectRowButtonOnly
        {
            get { return (bool)JsonState["selectRowButtonOnly"]; }
            set { JsonState["selectRowButtonOnly"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("右击行时是否选中")]
        public bool WhenRClickToSelect
        {
            get { return (bool)JsonState["whenRClickToSelect"]; }
            set { JsonState["whenRClickToSelect"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("Ajax contentType参数")]
        public string ContentType
        {
            get { return (string)JsonState["contentType"]; }
            set { JsonState["contentType"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(27)]
        [Description("复选框列宽度")]
        public int? CheckboxColWidth
        {
            get { return (int)JsonState["checkboxColWidth"]; }
            set { JsonState["checkboxColWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(29)]
        [Description("明细列宽度")]
        public int? DetailColWidth
        {
            get { return (int)JsonState["detailColWidth"]; }
            set { JsonState["detailColWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("单元格编辑状态")]
        public bool ClickToEdit
        {
            get { return (bool)JsonState["clickToEdit"]; }
            set { JsonState["clickToEdit"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("明细编辑状态")]
        public bool DetailToEdit
        {
            get { return (bool)JsonState["detailToEdit"]; }
            set { JsonState["detailToEdit"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(80)]
        [Description("最小列宽")]
        public int? MinColumnWidth
        {
            get { return (int)JsonState["minColumnWidth"]; }
            set { JsonState["minColumnWidth"] = value; }
        }

        //public object tree;

        //public funcrion isChecked;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("冻结列状态")]
        public bool Frozen
        {
            get { return (bool)JsonState["frozen"]; }
            set { JsonState["frozen"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("明细按钮是否在固定列中")]
        public bool FrozenDetail
        {
            get { return (bool)JsonState["frozenDetail"]; }
            set { JsonState["frozenDetail"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("复选框按钮是否在固定列中")]
        public bool FrozenCheckbox
        {
            get { return (bool)JsonState["frozenCheckbox"]; }
            set { JsonState["frozenCheckbox"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(26)]
        [Description("序号列宽")]
        public int? RownumbersColWidth
        {
            get { return (int)JsonState["rownumbersColWidth"]; }
            set { JsonState["rownumbersColWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许表头拖拽")]
        public bool ColDraggable
        {
            get { return (bool)JsonState["colDraggable"]; }
            set { JsonState["colDraggable"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许行拖拽")]
        public bool RowDraggable
        {
            get { return (bool)JsonState["rowDraggable"]; }
            set { JsonState["rowDraggable"] = value; }
        }

        //public function rowDraggingRender

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否自动选中子节点")]
        public bool AutoCheckChildren
        {
            get { return (bool)JsonState["autoCheckChildren"]; }
            set { JsonState["autoCheckChildren"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(22)]
        [Description("行默认的高度")]
        public int? RowHeight
        {
            get { return (int)JsonState["rowHeight"]; }
            set { JsonState["rowHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(23)]
        [Description("表头行的高度")]
        public int? HeaderRowHeight
        {
            get { return (int)JsonState["headerRowHeight"]; }
            set { JsonState["headerRowHeight"] = value; }
        }

        //public object toolbar;

        [Category(CategoryName.OPTIONS)]
        [Description("表格头部图标")]
        public string HeaderImg
        {
            get { return (string)JsonState["headerImg"]; }
            set { JsonState["headerImg"] = value; }
        }

        //public function isSelected;

        //public object detail;

        //public funcvtion isShowDetailToggle;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("工具条显示在左边")]
        public bool ToolbarShowInLeft
        {
            get { return (bool)JsonState["toolbarShowInLeft"]; }
            set { JsonState["toolbarShowInLeft"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("不设置validate")]
        public bool UnSetValidateAttr
        {
            get { return (bool)JsonState["unSetValidateAttr"]; }
            set { JsonState["unSetValidateAttr"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("编辑器位置误差调整")]
        public int? EditorTopDiff
        {
            get { return (int)JsonState["editorTopDiff"]; }
            set { JsonState["editorTopDiff"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("编辑器位置误差调整")]
        public int? EditorLeftDiff
        {
            get { return (int)JsonState["editorLeftDiff"]; }
            set { JsonState["editorLeftDiff"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("编辑器高度误差调整")]
        public int? EditorHeightDiff
        {
            get { return (int)JsonState["editorHeightDiff"]; }
            set { JsonState["editorHeightDiff"] = value; }
        }

        //public object urlParms;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否隐藏刷新按钮")]
        public bool HideLoadButton
        {
            get { return (bool)JsonState["hideLoadButton"]; }
            set { JsonState["hideLoadButton"] = value; }
        }

        //public function pageRender;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("复选框选择的时候是否单选模式")]
        public bool IsSingleCheck
        {
            get { return (bool)JsonState["isSingleCheck"]; }
            set { JsonState["isSingleCheck"] = value; }
        }

        //public function rowClsRender;

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否可选择")]
        public bool Selectable
        {
            get { return (bool)JsonState["selectable"]; }
            set { JsonState["selectable"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否可选择")]
        public bool RowSelectable
        {
            get { return (bool)JsonState["rowSelectable"]; }
            set { JsonState["rowSelectable"] = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string script = String.Format("$(\"#{0}\").ligerGrid({1});", this.ClientID, JsonState.Serialize());
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
