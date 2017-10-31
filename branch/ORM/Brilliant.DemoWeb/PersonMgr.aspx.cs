using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DB_Test.BLL;
using DB_Test.Entity;

namespace Brilliant.DemoWeb
{
    public partial class PersonMgr : System.Web.UI.Page
    {
        //人员业务逻辑
        private PersonsBiz personBiz = new PersonsBiz();

        /// <summary>
        /// 当前页索引
        /// </summary>
        private int PageNum
        {
            get { return Convert.ToInt32(ViewState["PageNum"]); }
            set { ViewState["PageNum"] = value; }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.PageNum = 1;
                BindPersonList();
            }
        }

        /// <summary>
        /// 绑定人员列表
        /// </summary>
        private void BindPersonList()
        {
            int recordCount = 0;
            this.gvPersonList.DataSource = personBiz.GetList_FK(this.AspNetPager1.PageSize, this.PageNum, out recordCount);
            this.gvPersonList.DataBind();
            this.AspNetPager1.RecordCount = recordCount;

            this.txtJson.Text = personBiz.GetJsonList_FK(this.AspNetPager1.PageSize, this.PageNum, out recordCount);
        }

        /// <summary>
        /// 点击添加
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            PersonsEntity person = new PersonsEntity();
            person.Age = Convert.ToInt32(this.txtAge.Text);
            person.Id = this.txtId.Text;
            person.Name = this.txtName.Text;
            person.Sex = this.txtSex.Text;
            person.RoleId = this.txtRoleId.Text;
            person.RolesModel.RoleId = this.txtRoleId.Text;
            person.RolesModel.RoleName = this.txtRoleName.Text;
            if (personBiz.Add_FK(person))
            {
                Brilliant.Utility.MsgBoxHelper.ShowMsgBox("添加成功!", this.Page);
                BindPersonList();
            }
            else
            {
                Brilliant.Utility.MsgBoxHelper.ShowMsgBox("添加失败!", this.Page);
            }
        }

        /// <summary>
        /// 行绑定事件
        /// </summary>
        protected void gvPersonList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                PersonsEntity person = new PersonsEntity();
                person.Id = this.gvPersonList.DataKeys[index].Values["Id"].ToString();
                person.RolesModel.RoleId = this.gvPersonList.DataKeys[index].Values["RoleId"].ToString();
                if (personBiz.Delete_FK(person))
                {
                    Brilliant.Utility.MsgBoxHelper.ShowMsgBox("删除成功!", this.Page);
                    BindPersonList();
                }
                else
                {
                    Brilliant.Utility.MsgBoxHelper.ShowMsgBox("删除失败!", this.Page);
                }
            }

            if (e.CommandName == "Edi")
            {
                PersonsEntity model = personBiz.GetModel_FK(e.CommandArgument.ToString());
                this.txtAge.Text = model.Age.ToString();
                this.txtId.Text = model.Id;
                this.txtName.Text = model.Name;
                this.txtSex.Text = model.Sex;
                this.txtRoleId.Text = model.RolesModel.RoleId;
                this.txtRoleName.Text = model.RolesModel.RoleName;
            }
        }

        /// <summary>
        /// 点击分页索引
        /// </summary>
        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.PageNum = e.NewPageIndex;
            BindPersonList();
        }
    }
}