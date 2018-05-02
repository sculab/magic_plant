<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="admin_upload.aspx.vb" Inherits="bsms_net.admin_upload" %>

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
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server" Width="8em"
                    Style="text-align: justify">手机号</asp:TextBox>
                &nbsp;<asp:Button ID="Button1" runat="server" Text="清空密码" Width="5em" />
                &nbsp;<asp:Button ID="Button2" runat="server" Text="刷新数量" Width="5em" />
            </div>
            <br />

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
                AllowPaging="True" PageSize="20" AllowSorting="True">
                <Columns>
                    <asp:CommandField InsertVisible="False" ShowDeleteButton="True"
                        ShowEditButton="True" />
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"
                        SortExpression="ID" Visible="False" />
                    <asp:BoundField DataField="分值" HeaderText="分值" SortExpression="分值" />
                    <asp:BoundField DataField="中文名" HeaderText="中文名" SortExpression="中文名" />
                    <asp:BoundField DataField="电话" HeaderText="电话" SortExpression="电话" />
                    <asp:BoundField DataField="学名" HeaderText="学名" SortExpression="学名" />
                    <asp:BoundField DataField="科名" HeaderText="科名" SortExpression="科名" />
                    <asp:BoundField DataField="图片" HeaderText="图片" SortExpression="图片" />
                    <asp:BoundField DataField="分布" HeaderText="分布" SortExpression="分布" />
                    <asp:BoundField DataField="经度" HeaderText="经度" SortExpression="经度" />
                    <asp:BoundField DataField="纬度" HeaderText="纬度" SortExpression="纬度" />
                    <asp:HyperLinkField DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="./view.aspx?id={0}" DataTextField=""
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
                DeleteCommand="DELETE FROM magic_upload WHERE ID = ?"
                InsertCommand="INSERT INTO magic_upload (分值,中文名, 电话, 学名, 科名, 图片, 分布, 经度, 纬度) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)"
                ProviderName="<%$ ConnectionStrings:magicConnectionString.ProviderName %>"
                SelectCommand="SELECT ID, 分值,中文名, 电话, 学名, 科名, 图片, 分布, 经度, 纬度 FROM magic_upload order by ID"
                UpdateCommand="UPDATE magic_upload SET 分值 =?, 中文名 = ?, 电话 = ?, 学名 = ?, 科名 = ?, 图片 = ?, 分布 = ?, 经度 = ?, 纬度 = ? WHERE ID = ?">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                    <asp:Parameter Name="分值" Type="String" />
                    <asp:Parameter Name="中文名" Type="String" />
                    <asp:Parameter Name="电话" Type="String" />
                    <asp:Parameter Name="学名" Type="String" />
                    <asp:Parameter Name="科名" Type="String" />
                    <asp:Parameter Name="图片" Type="String" />
                    <asp:Parameter Name="分布" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                    <asp:Parameter Name="分值" Type="String" />
                    <asp:Parameter Name="中文名" Type="String" />
                    <asp:Parameter Name="电话" Type="String" />
                    <asp:Parameter Name="学名" Type="String" />
                    <asp:Parameter Name="科名" Type="String" />
                    <asp:Parameter Name="图片" Type="String" />
                    <asp:Parameter Name="分布" Type="String" />
                </UpdateParameters>
            </asp:SqlDataSource>

        </div>

    </form>
</body>
</html>
