using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.Web.UI
{
    public class JsonState
    {
        private Dictionary<string, object> _states = new Dictionary<string, object>();

        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public object this[string key]
        {
            get
            {
                if (!_states.ContainsKey(key))
                {
                    return null;
                }
                else
                {
                    return _states[key];
                }
            }
            set
            {
                _states[key] = value;
            }
        }

        public string Serialize()
        {
            string json = JsonSerializer.JSSerialize(_states);
            if (_properties.Count <= 0)
            {
                return json;
            }
            StringBuilder sb = new StringBuilder(json.TrimEnd('}'));
            int i = 0;
            foreach (var item in _properties)
            {
                sb.AppendFormat("{0}\"{1}\":{2}", i == 0 && _states.Count <= 0 ? "" : ",", item.Key, item.Value);
                i++;
            }
            sb.Append("}");
            return sb.ToString();
        }

        public void AddProperty(string key, string value)
        {
            this._properties.Add(key, value);
        }

        public void AddEvent(EventArgument eventArgument)
        {
            StringBuilder sbFunc = new StringBuilder();
            string arg = eventArgument.Serialize();
            string param = String.Join(",", eventArgument.Argument.Keys.ToArray());
            sbFunc.AppendFormat("function({0}){{ var arg=JSON.stringify({1});__doCallBack(this,arg);}}", param, arg);
            this._properties.Add(eventArgument.Handler, sbFunc.ToString());
        }

        public void RemoveEvent(string key)
        {
            this._properties.Remove(key);
        }
    }
}
