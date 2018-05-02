<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="upload.aspx.vb" Inherits="bsms_net.magic_upload" %>

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
            <asp:Label ID="Label3" runat="server" Text="&lt;br /&gt;"></asp:Label>
            <asp:FileUpload ID="FileUpload1" runat="server" Width="62%" />
            <asp:Label ID="Label4" runat="server" Text="&lt;br /&gt;&lt;br /&gt;"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="拍摄地点(+2分)："></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server"
                Width="95%"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Text="&lt;br /&gt;&lt;br /&gt;"></asp:Label>
            <asp:Button ID="Button1" runat="server" Text="上传图片" />
            <asp:Label ID="Label6" runat="server" Text="&lt;br /&gt;&lt;br /&gt;"></asp:Label>

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
                AutoGenerateColumns="False" DataKeyNames="ID" Width="100%"
                AllowPaging="True">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True">
                        <ItemStyle Width="2.5em" />
                    </asp:CommandField>
                    <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID"
                        Visible="False">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="分值" HeaderText="分值" SortExpression="分值" ReadOnly="True">
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
                SelectCommand="SELECT * FROM magic_upload order by ID desc"
                UpdateCommand="UPDATE magic_upload SET 分布 = ? WHERE ID = ?">

                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Decimal" />
                </DeleteParameters>

            </asp:SqlDataSource>

        </div>

    </form>
    <script type="text/javascript"> 
function getLocation(cb) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(showPosition);
        } else {
            alert('该设备不支持获取地理位置...')
        }
        function showPosition(position) {
            var lat = position.coords.latitude,
                lng = position.coords.longitude;

            cb(lat + ',' + lng);
        }
    }

     getLocation();
    </script>
</body>
</html>
