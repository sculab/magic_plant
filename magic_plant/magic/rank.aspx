<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rank.aspx.vb" Inherits="bsms_net.rank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
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

        /* GLOBALS */
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
    <form id="form1" runat="server">
        <div>
            <h1>神奇植物<asp:Label ID="Label1" runat="server" Text=""></asp:Label>排行榜</h1>
            <h4><a href="rank.aspx">查看总榜</a> <a href="list.aspx">返回列表</a>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></h4>
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="排名" HeaderText="排名" SortExpression="排名">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="手机号" HeaderText="手机" SortExpression="手机号">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="总分" HeaderText="总分" SortExpression="总分">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="物种数" HeaderText="照片" SortExpression="物种数">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:magicConnectionString %>"
                ProviderName="<%$ ConnectionStrings:magicConnectionString.ProviderName %>"
                SelectCommand="select  right(max(电话),4) as 手机尾号, sum(分值) as 总分, count(分值) as 物种数 from magic_upload group by 电话 order by 总分 desc;"></asp:SqlDataSource>

        </div>
    </form>
</body>
</html>
