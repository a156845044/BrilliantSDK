<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleMgr.aspx.cs" Inherits="Brilliant.DemoWeb.RoleMgr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <fieldset style="width: 300px">
        <legend>添加/修改</legend>
        <table style="width: 100%">
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
                    <asp:Button ID="btnAdd" runat="server" Text="添加" OnClick="btnAdd_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset style="width: 300px">
        <legend>角色列表</legend>
        <asp:GridView ID="gvRoleList" runat="server" AutoGenerateColumns="False" Width="100%"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="RoleId" HeaderText="角色编号" />
                <asp:BoundField DataField="RoleName" HeaderText="角色名称" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdi" runat="server" CommandName="Edi">编辑</asp:LinkButton>
                        |
                        <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton>
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
    </fieldset>
    </form>
</body>
</html>
