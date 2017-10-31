using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTXSAPILib;

namespace Brilliant.Service.RTX
{
    public class RTXDept
    {
        private RTXSAPIDeptManager deptMgr;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RTXDept(RTXSAPIRootObj rootObj)
        {
            deptMgr = rootObj.DeptManager;
        }

        /// <summary>
        /// 判定单位是否存在
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <returns>true:存在 false:不存在</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Exists(string deptName)
        {
            try
            {
                return deptMgr.IsDeptExist(deptName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 添加单位（根节点）
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Add(string deptName)
        {
            return Add(deptName, String.Empty);
        }

        /// <summary>
        /// 添加单位（子节点）
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <param name="parentDeptName">上级单位名称</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Add(string deptName, string parentDeptName)
        {
            try
            {
                deptMgr.AddDept(deptName, parentDeptName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新单位
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <param name="newDeptName">新单位名称</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Update(string deptName, string newDeptName)
        {
            try
            {
                deptMgr.SetDeptName(deptName, newDeptName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 删除单位（包括所有子节点以及关联数据）
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Delete(string deptName)
        {
            return Delete(deptName, true);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="deptName">单位名称</param>
        /// <param name="complete">是否包含子节点和关联数据</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-12</remarks>
        public bool Delete(string deptName, bool complete)
        {
            try
            {
                deptMgr.DelDept(deptName, complete);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
