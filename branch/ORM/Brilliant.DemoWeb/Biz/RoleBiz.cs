using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Brilliant.ORM;
using DB_Test.Entity;

namespace DB_Test.BLL
{
    /// <summary>
    /// 业务逻辑
    /// </summary>
    public class RolesBiz
    {
        /// <summary>
        /// 根据主键判断该记录是否存在
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>true：存在，false：不存在</returns>
        public bool Exists(string roleId)
        {
            SQL sql = SQL.Build("SELECT COUNT(*) FROM Roles WHERE RoleId=?", roleId);
            return Convert.ToInt32(SqlMap<RolesEntity>.ParseSql(sql).First()) > 0;
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity">待添加的实体对象</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        public bool Add(RolesEntity entity)
        {
            SQL sql = SQL.Build("INSERT INTO Roles(RoleId,RoleName) VALUES(?,?)", entity.RoleId, entity.RoleName);
            return SqlMap<RolesEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量添加记录
        /// </summary>
        /// <param name="list">待添加的实体对象列表</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        public bool Add(List<RolesEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (RolesEntity entity in list)
            {
                SQL sql = SQL.Build("INSERT INTO Roles(RoleId,RoleName) VALUES(?,?)", entity.RoleId, entity.RoleName);
                sqlList.Add(sql);
            }
            return SqlMap<RolesEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">待更新的实体对象</param>
        /// <returns>true:更新成功 false:更新失败</returns>
        public bool Update(RolesEntity entity)
        {
            SQL sql = SQL.Build("UPDATE Roles SET RoleName=? WHERE RoleId=?", entity.RoleName, entity.RoleId);
            return SqlMap<RolesEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <param name="list">待更新的实体对象列表</param>
        /// <returns>true:更新成功 false:更新失败</returns>
        public bool Update(List<RolesEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (RolesEntity entity in list)
            {
                SQL sql = SQL.Build("UPDATE Roles SET RoleName=? WHERE RoleId=?", entity.RoleName, entity.RoleId);
                sqlList.Add(sql);
            }
            return SqlMap<RolesEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(string roleId)
        {
            SQL sql = SQL.Build("DELETE FROM Roles WHERE RoleId=?", roleId);
            return SqlMap<RolesEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">待删除的实体对象</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(RolesEntity entity)
        {
            SQL sql = SQL.Build("DELETE FROM Roles WHERE RoleId=?", entity.RoleId);
            return SqlMap<PersonsEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="list">待删除的实体对象列表</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(List<RolesEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (RolesEntity entity in list)
            {
                SQL sql = SQL.Build("DELETE FROM Roles WHERE RoleId=?", entity.RoleId);
                sqlList.Add(sql);
            }
            return SqlMap<RolesEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 根据主键获取实体对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>实体对象</returns>
        public RolesEntity GetModel(string roleId)
        {
            SQL sql = SQL.Build("SELECT * FROM Roles WHERE RoleId=?", roleId);
            return SqlMap<RolesEntity>.ParseSql(sql).ToObject();
        }

        /// <summary>
        /// 根据主键获取Json对象
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns>Json对象</returns>
        public string GetJsonModel(string roleId)
        {
            SQL sql = SQL.Build("SELECT * FROM Roles WHERE RoleId=?", roleId);
            return SqlMap<RolesEntity>.ParseSql(sql).ToJsonObject();
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <returns>对象列表</returns>
        public List<RolesEntity> GetList()
        {
            SQL sql = SQL.Build("SELECT * FROM Roles");
            return SqlMap<RolesEntity>.ParseSql(sql).ToList();
        }

        /// <summary>
        /// 获取Json对象列表
        /// </summary>
        /// <returns>Json对象列表</returns>
        public string GetJsonList()
        {
            SQL sql = SQL.Build("SELECT * FROM Roles");
            return SqlMap<RolesEntity>.ParseSql(sql).ToJsonList();
        }

        /// <summary>
        /// 获取分页后对象列表
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns>分页后对象列表</returns>
        public List<RolesEntity> GetList(int pageSize, int pageNumber, out int recordCount)
        {
            SQL sql = SQL.Build("SELECT * FROM Roles").Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<RolesEntity>.ParseSql(sql).ToList();
        }

        /// <summary>
        /// 获取分页后Json对象列表
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns>页后Json对象列表</returns>
        public string GetJsonList(int pageSize, int pageNumber, out int recordCount)
        {
            SQL sql = SQL.Build("SELECT * FROM Roles").Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<RolesEntity>.ParseSql(sql).ToJsonList();
        }

    }
}