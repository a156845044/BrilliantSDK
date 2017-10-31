using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Security.Permissions;

namespace Brilliant.Web.UI
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public abstract class ControlBase : Control, INamingContainer
    {
        protected readonly new EventHandlerList<EventType> Events = new EventHandlerList<EventType>();

        private JsonState _jsonState = null;

        protected JsonState JsonState
        {
            get
            {
                return _jsonState;
            }
            set
            {
                _jsonState = value;
            }
        }

        public ControlBase()
        {
            _jsonState = new JsonState();
        }

        protected void AddStartupScript(string scriptContent)
        {
            if (ScriptManager.Instance.IsStartupScriptExist(this))
            {
                scriptContent = ScriptManager.Instance.GetStartupScript(this).Script + scriptContent;
                ScriptManager.Instance.RemoveStartupScript(this);
            }

            if (Visible)
            {
                ScriptManager.Instance.AddStartupScript(this, scriptContent);
            }
        }

        protected void AddEvent(EventType eventType, Delegate value, params string[] parameters)
        {
            EventArgument arg = new EventArgument();
            arg.Handler = eventType.ToString();
            foreach (string param in parameters)
            {
                arg[param] = param;
            }
            AddEvent(eventType, value, arg);
        }

        protected void AddEvent(EventType eventType, Delegate value, EventArgument arg)
        {
            JsonState.AddEvent(arg);
            Events.AddHandler(eventType, value);
        }

        protected void RemoveEvent(EventType eventType, Delegate value)
        {
            JsonState.RemoveEvent(eventType.ToString());
            Events.RemoveHandler(eventType, value);
        }

        public virtual void OnSerialize()
        {
            JsonState["uniqueID"] = this.UniqueID;
        }
    }
}
