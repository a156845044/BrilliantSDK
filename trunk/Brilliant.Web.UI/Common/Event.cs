using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.Web.UI
{
    public sealed class EventHandlerList<T> : IDisposable
    {
        ListEntry head;

        public EventHandlerList()
        { }

        public Delegate this[T key]
        {
            get
            {
                ListEntry e = Find(key);
                return e == null ? null : e._handler;
            }
            set
            {
                ListEntry e = Find(key);
                if (e != null)
                {
                    e._handler = value;
                }
                else
                {
                    head = new ListEntry(key, value, head);
                }
            }
        }

        public void AddHandler(T key, Delegate value)
        {
            ListEntry e = Find(key);
            if (e != null)
            {
                e._handler = Delegate.Combine(e._handler, value);
            }
            else
            {
                head = new ListEntry(key, value, head);
            }
        }

        public void AddHandlers(EventHandlerList<T> listToAddFrom)
        {
            ListEntry currentListEntry = listToAddFrom.head;
            while (currentListEntry != null)
            {
                AddHandler(currentListEntry._key, currentListEntry._handler);
                currentListEntry = currentListEntry._next;
            }
        }

        public void Dispose()
        {
            head = null;
        }

        private ListEntry Find(T key)
        {
            ListEntry found = head;
            while (found != null)
            {
                if (found._key.Equals(key))
                {
                    break;
                }
                found = found._next;
            }
            return found;
        }

        public void RemoveHandler(T key, Delegate value)
        {
            ListEntry e = Find(key);
            if (e != null)
            {
                e._handler = Delegate.Remove(e._handler, value);
            }
        }

        private sealed class ListEntry
        {
            internal ListEntry _next;
            internal T _key;
            internal Delegate _handler;

            public ListEntry(T key, Delegate handler, ListEntry next)
            {
                _next = next;
                _key = key;
                _handler = handler;
            }
        }
    }

    public enum EventType
    {
        click,
        onBeforeExpand,
        onContextmenu,
        onExpand,
        onBeforeCollapse,
        onCollapse,
        onBeforeSelect,
        onSelect,
        onBeforeCancelSelect,
        onCancelselect,
        onCheck,
        onSuccess,
        onError,
        onClick,
        onBeforeAppend,
        onAppend,
        onAfterAppend
    }

    public class EventArgument
    {
        public EventArgument()
        {
            this.Argument = new Dictionary<string, object>();
        }

        public EventArgument(EventType handler)
            : this()
        {
            this.Handler = handler.ToString();
        }

        public string Handler { get; set; }

        public Dictionary<string, object> Argument { get; set; }

        public object this[string key]
        {
            get
            {
                if (!Argument.ContainsKey(key))
                {
                    return null;
                }
                else
                {
                    return Argument[key];
                }
            }
            set
            {
                Argument[key] = value;
            }
        }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("\"Handler\":\"{0}\",", Handler);
            sb.Append("\"Argument\":{");
            foreach (var item in Argument)
            {
                sb.AppendFormat("\"{0}\":{1},", item.Key, item.Value);
            }
            string json = sb.ToString().TrimEnd(',');
            json = json + "}";
            return "{" + json + "}";
        }
    }
}
