using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.Web.UI
{
    public class TabItem
    {
        private JsonState _jsonState;

        public TabItem()
        {
            _jsonState = new JsonState();
        }

        public string TabID
        {
            get { return (string)_jsonState["tabid"]; }
            set { _jsonState["tabid"] = value; }
        }

        public string Text
        {
            get { return (string)_jsonState["text"]; }
            set { _jsonState["text"] = value; }
        }

        public string Url
        {
            get { return (string)_jsonState["url"]; }
            set { _jsonState["url"] = value; }
        }

        public string Serialize()
        {
            return this._jsonState.Serialize();
        }
    }
}
