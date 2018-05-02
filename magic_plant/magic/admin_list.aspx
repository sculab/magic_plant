<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="admin_list.aspx.vb" Inherits="bsms_net.admin_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>神奇植物在哪里</title>
    <style type="text/css">
        body {
            font: normal 80% Helvetica, Arial, sans-serif;
        }

        a {
            color: #34a34f;
            text-decoration: none;
        }

        h1 {
            font-family: Verdana;
            color: #5b7da3;
            text-align: center
        }

        h4 {
            font-family: Verdana;
            color: #555;
            text-align: center
        }
        /* GLOBALS */
        thead, tr {
            border-top-width: 0.1em;
            border-top-style: solid;
            border-top-color: #a8bfde;
        }

        table {
            border-bottom-width: 0.1em;
            border-bottom-style: solid;
            border-bottom-color: #a8bfde;
        }

        /* Padding and font style */
        td, th {
            padding: 0.5em 1em;
            font-size: 1em;
            font-family: Verdana;
            color: #5b7da3;
        }

        /* Alternating background colors */
        tr:nth-child(even) {
            background: #d3dfed
        }

        tr:nth-child(odd) {
            background: #FFF
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">

        <div>
            <h1>神奇植物在哪里</h1>
            <h4>
                <asp:HyperLink ID="HyperLink1" runat="server">神奇植物排行榜</asp:HyperLink>
                &nbsp;
                <asp:HyperLink
                    ID="HyperLink2" runat="server">退出登录</asp:HyperLink>&nbsp;<asp:HyperLink
                        ID="HyperLink3" runat="server" NavigateUrl="list.aspx">返回列表</asp:HyperLink>
            </h4>

            <div style="text-align: right">
                <asp:TextBox ID="TextBox1" runat="server" Width="8em"
                    Style="text-align: justify"></asp:TextBox>
                &nbsp;<asp:Button ID="Button5" runat="server" Text="查 找" Width="5em" />
                &nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="8em"
                    Style="text-align: justify"></asp:TextBox>
                &nbsp;<asp:Button ID="Button6" runat="server" Text="插 入" Width="5em" />
                &nbsp;<asp:TextBox ID="TextBox3" runat="server" Width="8em"
                    Style="text-align: justify">手机号</asp:TextBox>
                &nbsp;<asp:Button ID="Button1" runat="server" Text="清空密码" Width="5em" />
            </div>
            <br />

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
                AllowPaging="True" PageSize="20" AllowSorting="True">
                <Columns>
                    <asp:CommandField InsertVisible="False" ShowDeleteButton="True"
                        ShowEditButton="True" />
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"
                        SortExpression="ID" />
                    <asp:BoundField DataField="中文名" HeaderText="中文名" SortExpression="中文名" />
                    <asp:BoundField DataField="学名" HeaderText="学名" SortExpression="学名" />
                    <asp:BoundField DataField="属名" HeaderText="属名" SortExpression="属名" />
                    <asp:BoundField DataField="科名" HeaderText="科名" SortExpression="科名" />
                    <asp:BoundField DataField="数量" HeaderText="数量" SortExpression="数量" />
                    <asp:HyperLinkField DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="./list_view.aspx?id={0}" DataTextField=""
                        HeaderText="查看" SortExpression="" Text="查看">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2.5em" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:HyperLinkField>
                </Columns>
                <PagerSettings Mode="Numeric" FirstPageText="首页" LastPageText="末页" NextPageText="下一页"
                    PageButtonCount="6" PreviousPageText="上一页" />

            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:magicConnectionString %>"
                DeleteCommand="DELETE FROM magic_list WHERE ID = ?"
                InsertCommand="INSERT INTO magic_list (中文名, 学名, 科名, 属名, 数量) VALUES (?, ?, ?, ?, ?)"
                ProviderName="<%$ ConnectionStrings:magicConnectionString.ProviderName %>"
                SelectCommand="SELECT ID, 中文名, 学名, 科名, 属名, 数量 FROM magic_list order by ID"
                UpdateCommand="UPDATE magic_list SET 中文名 = ?, 学名 = ?, 科名 = ?, 属名 = ?, 数量 = ? WHERE ID = ?">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                    <asp:Parameter Name="中文名" Type="String" />
                    <asp:Parameter Name="学名" Type="String" />
                    <asp:Parameter Name="科名" Type="String" />
                    <asp:Parameter Name="属名" Type="String" />
                    <asp:Parameter Name="数量" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                    <asp:Parameter Name="中文名" Type="String" />
                    <asp:Parameter Name="学名" Type="String" />
                    <asp:Parameter Name="科名" Type="String" />
                    <asp:Parameter Name="属名" Type="String" />
                    <asp:Parameter Name="数量" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </div>

    </form>
</body>
</html>
