<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="view.aspx.vb" Inherits="bsms_net.view" %>

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
    <form id="form1" runat="server">

        <div>

            <asp:DataList ID="DataList1" runat="server" DataKeyField="ID"
                DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    ID:
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                    <br />
                    分值:
                <asp:Label ID="分值Label" runat="server" Text='<%# Eval("分值") %>' />
                    <br />
                    中文名:
                <asp:Label ID="中文名Label" runat="server" Text='<%# Eval("中文名") %>' />
                    <br />
                    提交者:
                <asp:Label ID="电话Label" runat="server" Text='<%# Eval("手机号") %>' />
                    <br />
                    学名:
                <asp:Label ID="学名Label" runat="server" Text='<%# Eval("学名") %>' Font-Italic="true" />
                    <br />
                    科名:
                <asp:Label ID="科名Label" runat="server" Text='<%# Eval("科名") %>' />
                    <br />
                    <a href='<%#String.Format("./images/upload/{1}/{0}.jpg", Eval("图片"), Eval("LIST_ID"))%> '><asp:Image ID="图片Label" runat="server" ImageUrl='<%#String.Format("./images/upload/small/{1}/{0}.jpg", Eval("图片"), Eval("LIST_ID"))%> ' Width="100%" /></a>
                    <br />
                    分布:
                <asp:Label ID="分布Label" runat="server" Text='<%# Eval("分布") %>' />
                    <br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
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
        // 百度地图API功能
        //GPS坐标
        <%showmap()%>
        var ggPoint = new BMap.Point(x, y);

        //地图初始化
        var bm = new BMap.Map("allmap");
        bm.centerAndZoom(ggPoint, 15);
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
            var convertor = new BMap.Convertor();
            var pointArr = [];
            pointArr.push(ggPoint);
            convertor.translate(pointArr, 1, 5, translateCallback)
        }, 200);
    </script>
</body>
</html>
