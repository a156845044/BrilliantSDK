using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    public class BaseCollection<T> : Collection<T> where T : Control
    {
        private ControlBase _parent;
        private string _groupName;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parentControl">父控件实例</param>
        public BaseCollection(ControlBase parentControl)
        {
            _parent = parentControl;
            _groupName = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 向集合中插入一个元素
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            //item.CollectionGroupName = _groupName;
            //item.RenderWrapperNode = false;

            //int startIndex = GetStartIndex();
            _parent.Controls.AddAt(index, item);

            base.InsertItem(index, item);
        }

        /// <summary>
        /// 删除集合中的一个元素
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            //int startIndex = GetStartIndex();
            _parent.Controls.RemoveAt(index);

            base.RemoveItem(index);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        protected override void ClearItems()
        {
            //int startIndex = GetStartIndex();
            // We should only remove this collection related controls
            // Note we must loop from the last element(Count-1) to the first one(0)
            for (int i = Count - 1; i >= 0; i--)
            {
                _parent.Controls.RemoveAt(i);
            }

            base.ClearItems();
        }


        /// <summary>
        /// 获取类型 T 在父控件子集中的开始位置
        /// </summary>
        /// <returns></returns>
        private int GetStartIndex()
        {
            int startIndex = 0;

            //foreach (Control control in _parent.Controls)
            //{
            //    if (control is ControlBase && (control as ControlBase).CollectionGroupName == _groupName)
            //    {
            //        break;
            //    }
            //    startIndex++;
            //}

            return startIndex;
        }
    }
}
