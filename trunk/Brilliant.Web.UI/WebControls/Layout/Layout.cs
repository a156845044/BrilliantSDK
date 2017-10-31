using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.LayoutDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Width")]
    [ToolboxData("<{0}:Layout runat=\"server\"></{0}:Layout>")]
    [ToolboxBitmap(typeof(Layout), "Resource.Layout.bmp")]
    [Description("布局控件")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Layout : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue(50)]
        [Description("顶部区域高度")]
        public int TopHeight
        {
            get { return (int)JsonState["topHeight"]; }
            set { JsonState["topHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(50)]
        [Description("底部区域高度")]
        public int BottomHeight
        {
            get { return (int)JsonState["bottomHeight"]; }
            set { JsonState["bottomHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(110)]
        [Description("左边区域宽度")]
        public int LeftWidth
        {
            get { return (int)JsonState["leftWidth"]; }
            set { JsonState["leftWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(300)]
        [Description("中间区域宽度")]
        public int CenterWidth
        {
            get { return (int)JsonState["centerWidth"]; }
            set { JsonState["centerWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(300)]
        [Description("右边区域宽度")]
        public int RightWidth
        {
            get { return (int)JsonState["rightWidth"]; }
            set { JsonState["rightWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(300)]
        [Description("中下部区域宽度")]
        public int CenterBottomHeight
        {
            get { return (int)JsonState["centerBottomHeight"]; }
            set { JsonState["centerBottomHeight"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许中下部区域调整大小")]
        public bool AllowCenterBottomResize
        {
            get { return (bool)JsonState["allowCenterBottomResize"]; }
            set { JsonState["allowCenterBottomResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否以窗口的高度为准")]
        public bool InWindow
        {
            get { return (bool)JsonState["inWindow"]; }
            set { JsonState["inWindow"] = value; }
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
        [DefaultValue("100%")]
        [Description("高度")]
        public string Height
        {
            get { return (string)JsonState["height"]; }
            set { JsonState["height"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("初始化时左边是否隐藏")]
        public bool IsLeftCollapse
        {
            get { return (bool)JsonState["isLeftCollapse"]; }
            set { JsonState["isLeftCollapse"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("初始化时右边是否隐藏")]
        public bool IsRightCollapse
        {
            get { return (bool)JsonState["isRightCollapse"]; }
            set { JsonState["isRightCollapse"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许左边可以隐藏")]
        public bool AllowLeftCollapse
        {
            get { return (bool)JsonState["allowLeftCollapse"]; }
            set { JsonState["allowLeftCollapse"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许右边可以隐藏")]
        public bool AllowRightCollapse
        {
            get { return (bool)JsonState["allowRightCollapse"]; }
            set { JsonState["allowRightCollapse"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许左边可以调整大小")]
        public bool AllowLeftResize
        {
            get { return (bool)JsonState["allowLeftResize"]; }
            set { JsonState["allowLeftResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许右边可以调整大小")]
        public bool AllowRightResize
        {
            get { return (bool)JsonState["allowRightResize"]; }
            set { JsonState["allowRightResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许顶部可以调整大小")]
        public bool AllowTopResize
        {
            get { return (bool)JsonState["allowTopResize"]; }
            set { JsonState["allowTopResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否允许底部可以调整大小")]
        public bool AllowBottomResize
        {
            get { return (bool)JsonState["allowBottomResize"]; }
            set { JsonState["allowBottomResize"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(3)]
        [Description("面板之间间隔")]
        public int Space
        {
            get { return (int)JsonState["space"]; }
            set { JsonState["space"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(80)]
        [Description("调整左侧宽度时的最小允许宽度")]
        public int MinLeftWidth
        {
            get { return (int)JsonState["minLeftWidth"]; }
            set { JsonState["minLeftWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(80)]
        [Description("调整右侧宽度时的最小允许宽度")]
        public int MinRightWidth
        {
            get { return (int)JsonState["minRightWidth"]; }
            set { JsonState["minRightWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("100%")]
        [Description("宽度")]
        public string Width
        {
            get { return (string)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        private LayoutPanelCollection _panels;

        [Category(CategoryName.OPTIONS)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public LayoutPanelCollection Panels
        {
            get
            {
                if (_panels == null)
                {
                    _panels = new LayoutPanelCollection(this);
                }
                return _panels;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                string json = JsonState.Serialize();
                json = json.TrimEnd('}');
                json = String.Format("{0},\"onHeightChanged\":f_heightChanged}}", json);
                string script = String.Format("$(\"#{0}\").ligerLayout({1});", this.ID, json);
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }
    }
}
