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
    public class PersonsBiz
    {
        /// <summary>
        /// 根据主键判断该记录是否存在
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>true：存在，false：不存在</returns>
        public bool Exists(string id)
        {
            SQL sql = SQL.Build("SELECT COUNT(*) FROM Persons WHERE Id=?", id);
            return Convert.ToInt32(SqlMap<PersonsEntity>.ParseSql(sql).First()) > 0;
        }

        /// <summary>
        /// 添加一条记录
        /// </summary>
        /// <param name="entity">待添加的实体对象</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        public bool Add(PersonsEntity entity)
        {
            SQL sql = SQL.Build("INSERT INTO Persons(Id,Name,Sex,Age,RoleId) VALUES(?,?,?,?,?)", entity.Id, entity.Name, entity.Sex, entity.Age, entity.RoleId);
            return SqlMap<PersonsEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量添加记录
        /// </summary>
        /// <param name="list">待添加的实体对象列表</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        public bool Add(List<PersonsEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonsEntity entity in list)
            {
                SQL sql = SQL.Build("INSERT INTO Persons(Id,Name,Sex,Age,RoleId) VALUES(?,?,?,?,?)", entity.Id, entity.Name, entity.Sex, entity.Age, entity.RoleId);
                sqlList.Add(sql);
            }
            return SqlMap<PersonsEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="entity">待更新的实体对象</param>
        /// <returns>true:更新成功 false:更新失败</returns>
        public bool Update(PersonsEntity entity)
        {
            SQL sql = SQL.Build("UPDATE Persons SET Name=?,Sex=?,Age=?,RoleId=? WHERE Id=?", entity.Name, entity.Sex, entity.Age, entity.RoleId, entity.Id);
            return SqlMap<PersonsEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量更新记录
        /// </summary>
        /// <param name="list">待更新的实体对象列表</param>
        /// <returns>true:更新成功 false:更新失败</returns>
        public bool Update(List<PersonsEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonsEntity entity in list)
            {
                SQL sql = SQL.Build("UPDATE Persons SET Name=?,Sex=?,Age=?,RoleId=? WHERE Id=?", entity.Name, entity.Sex, entity.Age, entity.RoleId, entity.Id);
                sqlList.Add(sql);
            }
            return SqlMap<PersonsEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(string id)
        {
            SQL sql = SQL.Build("DELETE FROM Persons WHERE Id=?", id);
            return SqlMap<PersonsEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">待删除的实体对象</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(PersonsEntity entity)
        {
            SQL sql = SQL.Build("DELETE FROM Persons WHERE Id=?", entity.Id);
            return SqlMap<PersonsEntity>.ParseSql(sql).Execute() > 0;
        }

        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="list">待删除的实体对象列表</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete(List<PersonsEntity> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonsEntity entity in list)
            {
                SQL sql = SQL.Build("DELETE FROM Persons WHERE Id=?", entity.Id);
                sqlList.Add(sql);
            }
            return SqlMap<PersonsEntity>.ParseSql(sqlList).Execute() > 0;
        }

        /// <summary>
        /// 根据主键获取实体对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public PersonsEntity GetModel(string id)
        {
            SQL sql = SQL.Build("SELECT * FROM Persons WHERE Id=?", id);
            return SqlMap<PersonsEntity>.ParseSql(sql).ToObject();
        }

        /// <summary>
        /// 根据主键获取Json对象
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>Json对象</returns>
        public string GetJsonModel(string id)
        {
            SQL sql = SQL.Build("SELECT * FROM Persons WHERE Id=?", id);
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonObject();
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <returns>对象列表</returns>
        public List<PersonsEntity> GetList()
        {
            SQL sql = SQL.Build("SELECT * FROM Persons");
            return SqlMap<PersonsEntity>.ParseSql(sql).ToList();
        }

        /// <summary>
        /// 获取Json对象列表
        /// </summary>
        /// <returns>Json对象列表</returns>
        public string GetJsonList()
        {
            SQL sql = SQL.Build("SELECT * FROM Persons");
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonList();
        }

        /// <summary>
        /// 获取分页后对象列表
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns>分页后对象列表</returns>
        public List<PersonsEntity> GetList(int pageSize, int pageNumber, out int recordCount)
        {
            SQL sql = SQL.Build("SELECT * FROM Persons").Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<PersonsEntity>.ParseSql(sql).ToList();
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
            SQL sql = SQL.Build("SELECT * FROM Persons").Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonList();
        }

        /// <summary>
        /// 级联添加
        /// </summary>
        /// <param name="entity">待添加的实体对象</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        public bool Add_FK(PersonsEntity entity)
        {
            try
            {
                DBHelper.BeginTransaction();
                new RolesBiz().Add(entity.RolesModel);
                this.Add(entity);
                DBHelper.Commit();
                return true;
            }
            catch
            {
                DBHelper.Rollback();
                return false;
            }
        }

        /// <summary>
        /// 级联更新
        /// </summary>
        /// <param name="entity">待更新的实体对象</param>
        /// <returns>true:更新成功 false:更新失败</returns>
        public bool Update_FK(PersonsEntity entity)
        {
            try
            {
                DBHelper.BeginTransaction();
                new RolesBiz().Update(entity.RolesModel);
                this.Update(entity);
                DBHelper.Commit();
                return true;
            }
            catch
            {
                DBHelper.Rollback();
                return false;
            }
        }

        /// <summary>
        /// 级联删除
        /// </summary>
        /// <param name="entity">待删除的实体对象</param>
        /// <returns>true:删除成功 false:删除失败</returns>
        public bool Delete_FK(PersonsEntity entity)
        {
            try
            {
                DBHelper.BeginTransaction();
                this.Delete(entity);
                new RolesBiz().Delete(entity.RolesModel);
                DBHelper.Commit();
                return true;
            }
            catch
            {
                DBHelper.Rollback();
                return false;
            }
        }

        /// <summary>
        /// 根据主键获取实体对象(级联查询)
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public PersonsEntity GetModel_FK(string id)
        {
            StringBuilder sbSQL = new StringBuilder("SELECT * FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            sbSQL.Append("WHERE Id=?");
            SQL sql = SQL.Build(sbSQL.ToString(), id);
            return SqlMap<PersonsEntity>.ParseSql(sql).ToObject();
        }

        /// <summary>
        /// 根据主键获取Json对象(级联查询)
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>Json对象</returns>
        public string GetJsonModel_FK(string id)
        {
            StringBuilder sbSQL = new StringBuilder("SELECT * FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            sbSQL.Append("WHERE Id=?");
            SQL sql = SQL.Build(sbSQL.ToString(), id);
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonObject();
        }

        /// <summary>
        /// 获取对象列表(级联查询)
        /// </summary>
        /// <returns>对象列表</returns>
        public List<PersonsEntity> GetList_FK()
        {
            StringBuilder sbSQL = new StringBuilder("SELECT * FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            SQL sql = SQL.Build(sbSQL.ToString());
            return SqlMap<PersonsEntity>.ParseSql(sql).ToList();
        }

        /// <summary>
        /// 获取Json对象列表(级联查询)
        /// </summary>
        /// <returns>Json对象列表</returns>
        public string GetJsonList_FK()
        {
            StringBuilder sbSQL = new StringBuilder("SELECT * FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            SQL sql = SQL.Build(sbSQL.ToString());
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonList();
        }

        /// <summary>
        /// 获取分页后对象列表(级联查询)
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns>分页后对象列表</returns>
        public List<PersonsEntity> GetList_FK(int pageSize, int pageNumber, out int recordCount)
        {
            StringBuilder sbSQL = new StringBuilder("SELECT Persons.Id,Persons.Name,Persons.Sex,Persons.Age,Roles.* FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            SQL sql = SQL.Build(sbSQL.ToString()).Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<PersonsEntity>.ParseSql(sql).ToList();
        }

        /// <summary>
        /// 获取分页后Json对象列表(级联查询)
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns>页后Json对象列表</returns>
        public string GetJsonList_FK(int pageSize, int pageNumber, out int recordCount)
        {
            StringBuilder sbSQL = new StringBuilder("SELECT Persons.Id,Persons.Name,Persons.Sex,Persons.Age,Roles.* FROM Persons ");
            sbSQL.Append("INNER JOIN Roles ON Persons.RoleId=Roles.RoleId ");
            SQL sql = SQL.Build(sbSQL.ToString()).Limit(pageSize, pageNumber);
            recordCount = sql.RecordCount;
            return SqlMap<PersonsEntity>.ParseSql(sql).ToJsonList();
        }
    }
}