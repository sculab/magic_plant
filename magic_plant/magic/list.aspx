<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="list.aspx.vb" Inherits="bsms_net.magic_list" %>

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

        <div style="text-align: right">
            <asp:TextBox ID="Username" runat="server" Width="8em">手机号</asp:TextBox>
            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="8em">密码/设定密码</asp:TextBox>
            <asp:Button ID="Button6" runat="server" Text="注册/登陆" Width="7em" />
            <asp:Label ID="Label1" runat="server" Text="Label" Width="90%"></asp:Label>
        </div>
        <div>
            <h1>神奇植物在哪里</h1>
            <h4>
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="./rec_plant.aspx">&nbsp鉴定植物&nbsp</asp:HyperLink>
                <asp:HyperLink
                    ID="HyperLink2" runat="server">&nbsp退出登录&nbsp</asp:HyperLink>
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="./admin_list.aspx"
                    Visible="False">&nbsp管理名录&nbsp</asp:HyperLink>
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="./admin_upload.aspx"
                    Visible="False">&nbsp管理上传&nbsp</asp:HyperLink>
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="./upload.aspx">&nbsp我的图片&nbsp</asp:HyperLink>
                <asp:HyperLink ID="HyperLink1" runat="server">&nbsp月榜&nbsp</asp:HyperLink>
                <asp:HyperLink ID="HyperLink7" runat="server">&nbsp日榜&nbsp</asp:HyperLink>
            </h4>

            <div style="text-align: right">
                <asp:TextBox ID="SearchBox" runat="server" Width="65%"
                    Style="text-align: justify"></asp:TextBox>
                <asp:Button ID="Button5" runat="server" Text=" 查 找 " Width="7em" />
            </div>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
                AllowPaging="True" PageSize="20" AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True"
                        SortExpression="ID" Visible="False" />
                    <asp:BoundField DataField="学名" HeaderText="学名" SortExpression="学名"
                        Visible="False" />
                    <asp:BoundField DataField="属名" HeaderText="属名" SortExpression="属名"
                        Visible="False" />
                    <asp:HyperLinkField DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="./upload.aspx?id={0}" DataTextField="中文名"
                        DataTextFormatString="{0}" HeaderText="中文名" SortExpression="中文名">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="./upload.aspx?id={0}" DataTextField="科名"
                        DataTextFormatString="{0}" HeaderText="科名" Visible="False" SortExpression="科名">
                        <ItemStyle Width="5em" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="科名" DataFormatString="{0}" HeaderText="科名" SortExpression="科名">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="5em" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="ID"
                        DataNavigateUrlFormatString="./list_view.aspx?id={0}" DataTextField="数量"
                        DataTextFormatString="{0}" HeaderText="找到" SortExpression="数量">
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2.5em" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:HyperLinkField>
                </Columns>
                <PagerSettings Mode="Numeric" FirstPageText="首页" LastPageText="末页" NextPageText="下一页"
                    PageButtonCount="6" PreviousPageText="上一页" />

            </asp:GridView>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:magicConnectionString %>"
                ProviderName="<%$ ConnectionStrings:magicConnectionString.ProviderName %>"
                SelectCommand="SELECT * FROM magic_list order by id"></asp:SqlDataSource>

        </div>

    </form>
</body>
</html>
