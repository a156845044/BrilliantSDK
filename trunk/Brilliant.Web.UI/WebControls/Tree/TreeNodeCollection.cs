using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Brilliant.Web.UI
{
    public class TreeNodeCollection : Collection<TreeNode>
    {
        private Tree _treeInstance;
        private TreeNode _parentNode;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tree">树实例</param>
        /// <param name="parentNode">父节点</param>
        public TreeNodeCollection(Tree tree, TreeNode parentNode)
        {
            _treeInstance = tree;
            _parentNode = parentNode;
        }

        /// <summary>
        /// 插入树节点
        /// </summary>
        /// <param name="index">插入索引位置</param>
        /// <param name="item">树节点实例</param>
        protected override void InsertItem(int index, TreeNode item)
        {
            if (_treeInstance != null)
            {
                ResolveTreeNode(item);
            }
            item.ParentNode = _parentNode;
            base.InsertItem(index, item);
        }


        /// <summary>
        /// 设置每个节点的Tree实例
        /// </summary>
        /// <param name="node"></param>
        private void ResolveTreeNode(TreeNode node)
        {
            node.Tree = _treeInstance;
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode subNode in node.Nodes)
                {
                    ResolveTreeNode(subNode);
                }
            }
        }

        public string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            SerializeItem(sb, this.Items);
            return sb.ToString();
        }

        private void SerializeItem(StringBuilder sb, IList<TreeNode> nodes)
        {
            sb.Append("[");
            foreach (TreeNode node in nodes)
            {
                sb.Append("{");
                sb.Append(node.Serialize());
                if (node.Nodes.Count > 0)
                {
                    sb.Append(",\"children\":");
                    SerializeItem(sb, node.Nodes);
                }
                sb.Append("},");
            }
            if (sb.Length > 1)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
        }
    }
}
