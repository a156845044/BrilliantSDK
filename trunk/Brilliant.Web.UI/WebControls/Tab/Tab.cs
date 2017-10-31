using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    [Designer("BrilliantUI.Design.TabDesigner, BrilliantUI.Design")]
    [DefaultProperty("Height")]
    [ToolboxData("<{0}:Tab runat=\"server\"></{0}:Tab>")]
    [ToolboxBitmap(typeof(Tab), "Resource.Tab.bmp")]
    [Description("标签选项卡")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Tab : ControlBase
    {
        [Category(CategoryName.OPTIONS)]
        [Description("高度")]
        public int Height
        {
            get { return (int)JsonState["height"]; }
            set { JsonState["height"] = value; }
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
        [DefaultValue(true)]
        [Description("是否启用右键菜单")]
        public bool Contextmenu
        {
            get { return (bool)JsonState["contextmenu"]; }
            set { JsonState["contextmenu"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否双击时关闭")]
        public bool DblClickToClose
        {
            get { return (bool)JsonState["dblClickToClose"]; }
            set { JsonState["dblClickToClose"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许拖动时改变tab项的位置")]
        public bool DragToMove
        {
            get { return (bool)JsonState["dragToMove"]; }
            set { JsonState["dragToMove"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("显示切换窗口按钮")]
        public bool ShowSwitch
        {
            get { return (bool)JsonState["showSwitch"]; }
            set { JsonState["showSwitch"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("切换窗口按钮显示在最后一项")]
        public bool ShowSwitchInTab
        {
            get { return (bool)JsonState["showSwitchInTab"]; }
            set { JsonState["showSwitchInTab"] = value; }
        }

        private TabPanelCollection _panels;

        [Category(CategoryName.OPTIONS)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TabPanelCollection Panels
        {
            get
            {
                if (_panels == null)
                {
                    _panels = new TabPanelCollection(this);
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
                string script = String.Format("$(\"#{0}\").ligerTab({1});", this.ID, json);
                AddStartupScript(script);
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
        }


        public void AddTabItem(string tabId, string text, string url)
        {
            TabItem item = new TabItem() { TabID = tabId, Text = text, Url = url };
            AddTabItem(item);
        }

        public void AddTabItem(TabItem item)
        {
            string script = String.Format("liger.get(\"{0}\").addTabItem({1});", this.ID, item.Serialize());
            ScriptManager.Instance.AddExtraScript(script);
        }
    }
}
