using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliant.ORM;
using System.Data;

namespace Brilliant.DemoForm
{
    public class PersonBiz
    {
        public bool Add(PersonInfo model)
        {
            SQL sql = SQL.Build("INSERT INTO Person VALUES(?,?,?,?)", model.Id, model.Name, model.Sex, model.Age);
            return SqlMap<PersonInfo>.ParseSql(sql).Execute() > 0;
        }

        public bool Add(List<PersonInfo> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonInfo model in list)
            {
                sqlList.Add(SQL.Build("INSERT INTO Person VALUES(?,?,?,?)", model.Id, model.Name, model.Sex, model.Age));
            }
            return SqlMap<PersonInfo>.ParseSql(sqlList).Execute() > 0;
        }

        public bool Update(PersonInfo model)
        {
            SQL sql = SQL.Build("UPDATE Person SET Name=?,Sex=?,Age=? WHERE Id=?", model.Name, model.Sex, model.Age, model.Id);
            return SqlMap<PersonInfo>.ParseSql(sql).Execute() > 0;
        }

        public bool Update(List<PersonInfo> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonInfo model in list)
            {
                sqlList.Add(SQL.Build("UPDATE Person SET Name=?,Sex=?,Age=? WHERE Id=?", model.Name, model.Sex, model.Age, model.Id));
            }
            return SqlMap<PersonInfo>.ParseSql(sqlList).Execute() > 0;
        }

        public bool Delete(string id)
        {
            SQL sql = SQL.Build("DELETE FROM Person WHERE Id=?", id);
            return SqlMap<PersonInfo>.ParseSql(sql).Execute() > 0;
        }

        public bool Delete(List<PersonInfo> list)
        {
            List<SQL> sqlList = new List<SQL>();
            foreach (PersonInfo model in list)
            {
                sqlList.Add(SQL.Build("DELETE FROM Person WHERE Id=?", model.Id));
            }
            return SqlMap<PersonInfo>.ParseSql(sqlList).Execute() > 0;
        }

        public PersonInfo GetModel(string id)
        {
            SQL sql = SQL.Build("SELECT * FROM Person WHERE Id=?", id);
            return SqlMap<PersonInfo>.ParseSql(sql).ToObject();
        }

        public List<PersonInfo> GetList()
        {
            SQL sql = SQL.Build("SELECT * FROM Person");
            return SqlMap<PersonInfo>.ParseSql(sql).ToList();
        }

        public string GetJsonList()
        {
            SQL sql = SQL.Build("SELECT * FROM Person");
            return SqlMap<PersonInfo>.ParseSql(sql).ToJsonList();
        }
    }
}
