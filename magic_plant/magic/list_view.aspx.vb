Public Class list_view
    Inherits System.Web.UI.Page
    Dim phone As String = "888"
    Dim my_id As String = "1"
    Dim chname As String = "" 'Server.UrlEncode("独活")
    Dim ldname As String = "" 'Server.UrlEncode("Heracleum")
    Dim family As String = "" 'Server.UrlEncode("伞形科")
    Dim pic_path As String = ""
    Dim count_name As Integer = 0
    Dim current_prize As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        my_id = CleanMagicStr(Request.QueryString("id"))
        If HttpContext.Current.Session("phone") Is Nothing = False Then
            If HttpContext.Current.Session("phone").ToString <> "" Then
                phone = HttpContext.Current.Session("phone").ToString
            End If
        End If

        If phone = "888" Then
            HyperLink1.Text = "您正在匿名访问，请点击此处返回登陆"
            HyperLink1.NavigateUrl = "./list.aspx"
        Else
            HyperLink1.Text = "上传照片"
            HyperLink1.NavigateUrl = "./upload.aspx?id=" & my_id
        End If
        ldname = "" 'Server.UrlDecode(Request.QueryString("ldname")).ToString.Replace("'", "")
        family = "" 'Server.UrlDecode(Request.QueryString("family")).ToString.Replace("'", "")
        If my_id = "" Then
            my_id = 1
        End If
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)

        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT ID,中文名,科名,学名 FROM magic_list where ID='" & my_id & "'", MySQL_con)
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While DataRead1.Read
            chname = DataRead1.GetValue(1).ToString
            family = DataRead1.GetValue(2).ToString
            ldname = DataRead1.GetValue(3).ToString
        End While
        MySQL_con.Close()
        SqlDataSource1.SelectCommand = "SELECT ID,分值,电话,中文名,学名,科名,图片,分布 FROM magic_upload where 中文名='" + chname + "' order by ID desc;"
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT COUNT(*) FROM magic_upload where 中文名='" + chname + "';", MySQL_con)
        count_name = cmd1.ExecuteScalar
        MySQL_con.Close()
        Select Case count_name
            Case 0
                current_prize = 15
            Case 1
                current_prize = 10
            Case 2
                current_prize = 6
            Case 3
                current_prize = 3
            Case Else
                If count_name > 10 Then
                    current_prize = 0
                Else
                    current_prize = 1
                End If
        End Select
        Label1.Text = family & chname & "已有" & count_name & "张照片, 当前计分为：" & current_prize
    End Sub

    Public Sub showmap()
        Dim my_info(1) As String
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT 图片,LIST_ID,经度,纬度,ID FROM magic_upload where 中文名='" + chname + "' and 经度 !='-10000';", MySQL_con)
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim pic_path As String = ""
        Dim list_id As String = ""
        While DataRead1.Read
            pic_path = DataRead1.GetValue(0).ToString
            list_id = DataRead1.GetValue(1).ToString
            my_info(0) = DataRead1.GetValue(2).ToString
            my_info(1) = DataRead1.GetValue(3).ToString
            Response.Write("y = " & my_info(1) & ";" & vbCrLf)
            Response.Write("x = " & my_info(0) & ";" & vbCrLf)
            Response.Write("var ggPoint" & DataRead1.GetValue(4).ToString & " = new BMap.Point(x, y);" & vbCrLf)
            Response.Write("pointArr.push(ggPoint" & DataRead1.GetValue(4).ToString & ");" & vbCrLf)
            Response.Write("bm.centerAndZoom(ggPoint" & DataRead1.GetValue(4).ToString & ", 15);" & vbCrLf)
        End While
        MySQL_con.Close()
    End Sub
End Class