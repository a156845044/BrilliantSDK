using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Brilliant.Web.UI
{
    public class TreeNode
    {
        private JsonState _jsonState;
        private Tree _tree;
        private TreeNode _parentNode;

        public TreeNode()
        {
            _jsonState = new JsonState();
        }

        public Tree Tree
        {
            get { return _tree; }
            set { _tree = value; }
        }

        public TreeNode ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }

        [DefaultValue("0")]
        public string ID
        {
            get { return (string)_jsonState["id"]; }
            set { _jsonState["id"] = value; }
        }

        [DefaultValue("节点")]
        public string Text
        {
            get { return (string)_jsonState["text"]; }
            set { _jsonState["text"] = value; }
        }

        [DefaultValue(false)]
        public bool IsExpand
        {
            get { return (bool)_jsonState["isexpand"]; }
            set { _jsonState["isexpand"] = value; }
        }

        [DefaultValue(false)]
        public bool IsChecked
        {
            get { return (bool)_jsonState["ischecked"]; }
            set { _jsonState["ischecked"] = value; }
        }

        [DefaultValue("folder")]
        public string Icon
        {
            get { return (string)_jsonState["icon"]; }
            set { _jsonState["icon"] = value; }
        }

        public string Url
        {
            get { return (string)_jsonState["url"]; }
            set { _jsonState["url"] = value; }
        }

        private TreeNodeCollection _nodes;

        public TreeNodeCollection Nodes
        {
            get
            {
                if (_nodes == null)
                {
                    _nodes = new TreeNodeCollection(_tree, this);
                }
                return _nodes;
            }
        }

        public string Serialize()
        {
            return _jsonState.Serialize().TrimStart('{').TrimEnd('}');
        }
    }
}
