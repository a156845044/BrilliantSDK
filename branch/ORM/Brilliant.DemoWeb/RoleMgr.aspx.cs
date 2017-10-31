using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DB_Test.BLL;
using DB_Test.Entity;
using Brilliant.Utility;

namespace Brilliant.DemoWeb
{
    public partial class RoleMgr : System.Web.UI.Page
    {
        private RolesBiz rolesBiz = new RolesBiz();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRoleList();
            }
        }

        /// <summary>
        /// 绑定角色列表
        /// </summary>
        private void BindRoleList()
        {
            this.gvRoleList.DataSource = rolesBiz.GetList();
            this.gvRoleList.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RolesEntity entity = new RolesEntity();
            entity.RoleId = this.txtRoleId.Text;
            entity.RoleName = this.txtRoleName.Text;
            if (rolesBiz.Add(entity))
            {
                BindRoleList();
                MsgBoxHelper.ShowMsgBox("添加成功!", this.Page);
            }
            else
            {
                MsgBoxHelper.ShowMsgBox("添加失败!", this.Page);
            }
        }
    }
}