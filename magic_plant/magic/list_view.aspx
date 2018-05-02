<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="list_view.aspx.vb" Inherits="bsms_net.list_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=EpRArK2Fob4psBKaEspiwYmcs3jj4PWg"></script>
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
            text-align: left
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

            <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
            <br />
            <h4>
                <asp:Label ID="Label1" runat="server" Text="当前："></asp:Label></h4>
            <br />
            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" DataKeyNames="ID" Width="100%">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="分值" HeaderText="分值" SortExpression="分值"
                        Visible="False">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="2.5em" />
                    </asp:BoundField>
                    <asp:BoundField DataField="电话" HeaderText="电话" SortExpression="电话" Visible="false" />
                    <asp:BoundField DataField="学名" HeaderText="拉丁学名" SortExpression="拉丁学名" Visible="false" />
                    <asp:BoundField DataField="科名" HeaderText="科名" SortExpression="科名"
                        Visible="False">
                        <ItemStyle Width="4em" />
                    </asp:BoundField>
                    <asp:BoundField DataField="图片" HeaderText="图片" SortExpression="图片" Visible="false" />
                    <asp:HyperLinkField DataNavigateUrlFormatString="./view.aspx?id={0}"
                        DataTextFormatString="{0}" DataNavigateUrlFields="ID"
                        DataTextField="中文名" HeaderText="中文名" />
                    <asp:BoundField DataField="分布" HeaderText="分布" SortExpression="分布" />
                </Columns>
                <PagerSettings Mode="Numeric" FirstPageText="首页" LastPageText="末页" NextPageText="下一页"
                    PageButtonCount="6" PreviousPageText="上一页" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:magicConnectionString %>"
                DeleteCommand="DELETE FROM magic_upload WHERE ID = ?"
                ProviderName="<%$ ConnectionStrings:magicConnectionString.ProviderName %>"
                SelectCommand="SELECT * FROM magic_upload order by ID desc">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                </DeleteParameters>

            </asp:SqlDataSource>

        </div>
        <div id="allmap" style="width: 100%; height: 20em"></div>
    </form>
    <script type="text/javascript">
        //地图初始化
        var bm = new BMap.Map("allmap");
        var x = -1;
        var y = -1;
        var pointArr = [];
        <%showmap()%>
        bm.addControl(new BMap.NavigationControl());

        //坐标转换完之后的回调函数
        translateCallback = function (data) {
            if (data.status === 0) {
                var marker = new BMap.Marker(data.points[0]);
                bm.addOverlay(marker);
                bm.setCenter(data.points[0]);
            }
        }

        setTimeout(function () {
        for (i=0;i<pointArr.length;i++)
        {
            var convertor = new BMap.Convertor();
            var pointArr_convert = [];
            pointArr_convert.push(pointArr[i]);
            convertor.translate(pointArr_convert, 1, 5, translateCallback)
        }
        }, 200);
    </script>
</body>
</html>
