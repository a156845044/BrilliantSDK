using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("BrilliantUI.Design.TreeDesigner, BrilliantUI.Design")]
    [DefaultProperty("TextFieldName")]
    [ToolboxData("<{0}:Tree runat=\"server\"></{0}:Tree>")]
    [ToolboxBitmap(typeof(Tree), "Resource.Tree.bmp")]
    [Description("树形控件")]
    public class Tree : ControlBase, ICallbackEventHandler
    {
        [Category(CategoryName.OPTIONS)]
        [Description("Ajax请求地址")]
        public string Url
        {
            get { return (string)JsonState["url"]; }
            set { JsonState["url"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示复选框")]
        public bool Checkbox
        {
            get { return (bool)JsonState["checkbox"]; }
            set { JsonState["checkbox"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示复选框")]
        public bool AutoCheckboxEven
        {
            get { return (bool)JsonState["autoCheckboxEven"]; }
            set { JsonState["autoCheckboxEven"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("folder")]
        [Description("父节点图标")]
        public string ParentIcon
        {
            get { return (string)JsonState["parentIcon"]; }
            set { JsonState["parentIcon"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("leaf")]
        [Description("子节点图标")]
        public string ChildIcon
        {
            get { return (string)JsonState["childIcon"]; }
            set { JsonState["childIcon"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("text")]
        [Description("文本字段名称")]
        public string TextFieldName
        {
            get { return (string)JsonState["textFieldName"]; }
            set { JsonState["textFieldName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(new string[] { "id", "url" })]
        [Description("属性")]
        public string[] Attribute
        {
            get { return (string[])JsonState["textFieldName"]; }
            set { JsonState["textFieldName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否显示line")]
        public bool TreeLine
        {
            get { return (bool)JsonState["treeLine"]; }
            set { JsonState["treeLine"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(90)]
        [Description("节点宽度")]
        public int NodeWidth
        {
            get { return (int)JsonState["nodeWidth"]; }
            set { JsonState["nodeWidth"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("__status")]
        [Description("状态名称")]
        public bool StatusName
        {
            get { return (bool)JsonState["statusName"]; }
            set { JsonState["statusName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否单选")]
        public bool Single
        {
            get { return (bool)JsonState["single"]; }
            set { JsonState["single"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("已选的是否需要取消操作")]
        public bool NeedCancel
        {
            get { return (bool)JsonState["needCancel"]; }
            set { JsonState["needCancel"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("id")]
        [Description("ID字段名称")]
        public string IDFieldName
        {
            get { return (string)JsonState["idFieldName"]; }
            set { JsonState["idFieldName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("父节点ID字段名称")]
        public string ParentIDFieldName
        {
            get { return (string)JsonState["parentIDFieldName"]; }
            set { JsonState["parentIDFieldName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(0)]
        [Description("根节点ID字段值")]
        public int TopParentIDValue
        {
            get { return (int)JsonState["topParentIDValue"]; }
            set { JsonState["topParentIDValue"] = value; }
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
        [DefaultValue("icon")]
        [Description("图标字段名称")]
        public string IconFieldName
        {
            get { return (string)JsonState["iconFieldName"]; }
            set { JsonState["iconFieldName"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否允许拖拽")]
        public bool NodeDraggable
        {
            get { return (bool)JsonState["nodeDraggable"]; }
            set { JsonState["nodeDraggable"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(true)]
        [Description("是否点击展开/收缩")]
        public bool BtnClickToToggleOnly
        {
            get { return (bool)JsonState["btnClickToToggleOnly"]; }
            set { JsonState["btnClickToToggleOnly"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue("post")]
        [Description("Ajax请求类型")]
        public string AjaxType
        {
            get { return (string)JsonState["ajaxType"]; }
            set { JsonState["ajaxType"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("是否展开(可以是true/false或者层级)")]
        public object IsExpand
        {
            get { return (object)JsonState["isExpand"]; }
            set { JsonState["isExpand"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("是否延迟加载(可以是true/false、层级、Url、数组)")]
        public object Delay
        {
            get { return (object)JsonState["delay"]; }
            set { JsonState["delay"] = value; }
        }

        private TreeNodeCollection _nodes;

        [Category(CategoryName.OPTIONS)]
        [NotifyParentProperty(true)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("树节点集合")]
        public TreeNodeCollection Nodes
        {
            get
            {
                if (_nodes == null)
                {
                    _nodes = new TreeNodeCollection(this, null);
                }
                return _nodes;
            }
        }

        [Category(CategoryName.ACTION)]
        [Description("控件点击事件")]
        public event TreeEventHandler Select
        {
            add
            {
                EventArgument arg = new EventArgument(EventType.onSelect);
                arg["node"] = "node.data";
                AddEvent(EventType.onSelect, value, arg);
            }
            remove
            {
                RemoveEvent(EventType.onSelect, value);
            }
        }

        public override void OnSerialize()
        {
            base.OnSerialize();
            JsonState.AddProperty("data", Nodes.Serialize());
            string script = String.Format("$(\"#{0}\").ligerTree({1});", this.ID, JsonState.Serialize());
            AddStartupScript(script);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                OnSerialize();
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ID);
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            base.Render(writer);
            writer.RenderEndTag();
        }

        public string GetCallbackResult()
        {
            return ScriptManager.Instance.Rerender();
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            EventArgument arg = JsonSerializer.JSDeSerialize<EventArgument>(eventArgument);
            EventType et = (EventType)Enum.Parse(typeof(EventType), arg.Handler);
            TreeEventHandler eventHandler = Events[et] as TreeEventHandler;
            if (eventHandler != null)
            {
                Dictionary<string, object> dic = arg["node"] as Dictionary<string, object>;
                TreeNode node = new TreeNode();
                node.ID = dic["treedataindex"].ToString();
                node.Text = dic["text"].ToString();
                node.Url = dic["url"].ToString();
                TreeEventArgs e = new TreeEventArgs();
                e.Node = node;
                eventHandler(this, e);
            }
        }
        
    }

    public delegate void TreeEventHandler(object sender, TreeEventArgs e);

    public class TreeEventArgs : EventArgs
    {
        public TreeNode Node { get; set; }
    }
}
