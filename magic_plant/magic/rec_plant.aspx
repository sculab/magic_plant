<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rec_plant.aspx.vb" Inherits="bsms_net.rec_plant" %>

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

            <h1>神奇植物是什么</h1>
            <h4>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="./list.aspx">返回列表</asp:HyperLink>
            </h4>

            <asp:FileUpload ID="FileUpload1" runat="server" Width="62%" />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="鉴定植物" />

            &nbsp;<br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="" Font-Bold="True"
                Font-Size="1em"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="" Font-Bold="True"
                Font-Size="1em"></asp:Label>
            <br />
            <h4>鉴定结果基于百度AI图像识别与深度学习
            </h4>
        </div>

    </form>
</body>
</html>
