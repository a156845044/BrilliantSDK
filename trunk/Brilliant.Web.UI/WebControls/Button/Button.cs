using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing;

namespace Brilliant.Web.UI
{
    [Designer("Brilliant.Web.UI.Design.ButtonDesigner, Brilliant.Web.UI.Design")]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Button Text=\"Button\" runat=\"server\"></{0}:Button>")]
    [ToolboxBitmap(typeof(Button), "Resource.Button.bmp")]
    [Description("按钮控件")]
    [DefaultEvent("Click")]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public class Button : ControlBase, ICallbackEventHandler
    {
        [Category(CategoryName.OPTIONS)]
        [DefaultValue("Button")]
        [Description("显示文本")]
        public string Text
        {
            get { return (string)JsonState["text"]; }
            set { JsonState["text"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [Description("图标")]
        public string Icon
        {
            get { return (string)JsonState["icon"]; }
            set { JsonState["icon"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(60)]
        [Description("按钮宽度")]
        public int Width
        {
            get { return (int)JsonState["width"]; }
            set { JsonState["width"] = value; }
        }

        [Category(CategoryName.OPTIONS)]
        [DefaultValue(false)]
        [Description("是否禁用按钮")]
        public bool Disabled
        {
            get { return (bool)JsonState["disabled"]; }
            set { JsonState["disabled"] = value; }
        }

        [Category(CategoryName.ACTION)]
        [Description("按钮点击事件")]
        public event EventHandler Click
        {
            add
            {
                AddEvent(EventType.click, value);
            }
            remove
            {
                RemoveEvent(EventType.click, value);
            }
        }

        public override void OnSerialize()
        {
            base.OnSerialize();
            string script = String.Format("$(\"#{0}\").ligerButton({1});", this.ID, JsonState.Serialize());
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
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
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
            EventHandler eventHandler = Events[et] as EventHandler;
            if (eventHandler != null)
            {
                eventHandler(this, new EventArgs() { });
            }
        }
    }
}
