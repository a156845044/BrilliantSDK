<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonMgr.aspx.cs" Inherits="Brilliant.DemoWeb.PersonMgr" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset style="width: 500px">
            <legend>添加/修改</legend>
            <table>
                <tr>
                    <td>
                        编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        姓名：
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别：
                    </td>
                    <td>
                        <asp:TextBox ID="txtSex" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        年龄：
                    </td>
                    <td>
                        <asp:TextBox ID="txtAge" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        角色编号：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleId" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        角色名称：
                    </td>
                    <td>
                        <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnAdd" runat="server" Text="保存" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <br />
        <fieldset style="width: 500px">
            <legend>数据列表</legend>
            <asp:GridView ID="gvPersonList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" Width="100%" OnRowCommand="gvPersonList_RowCommand"
                DataKeyNames="Id,RoleId">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="编号" />
                    <asp:BoundField DataField="Name" HeaderText="姓名" />
                    <asp:BoundField DataField="Sex" HeaderText="性别" />
                    <asp:BoundField DataField="Age" HeaderText="年龄" />
                    <asp:BoundField DataField="RoleId" HeaderText="角色编号" />
                    <asp:TemplateField HeaderText="角色名称">
                        <ItemTemplate>
                            <%# Eval("RolesModel.RoleName")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" CommandName="Del" CommandArgument='<%# Container.DataItemIndex %>'
                                runat="server">删除</asp:LinkButton>
                            |
                            <asp:LinkButton ID="lbtnEdit" CommandName="Edi" CommandArgument='<%# Eval("Id") %>'
                                runat="server">编辑</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CssClass="pager_default" CurrentPageButtonClass="current"
                FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" OnPageChanging="AspNetPager1_PageChanging"
                PrevPageText="前一页" ShowPageIndexBox="Always" SubmitButtonText="跳转">
            </webdiyer:AspNetPager>
        </fieldset>
        <br />
        <fieldset style="width: 500px">
            <legend>Json数据</legend>
            <asp:TextBox ID="txtJson" runat="server" TextMode="MultiLine" Width="100%" Height="150px"></asp:TextBox>
        </fieldset>
    </div>
    </form>
</body>
</html>
