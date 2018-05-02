'增加缩略图
'增加植物学习功能
Public Class magic_upload
    Inherits System.Web.UI.Page
    Dim phone As String = "888"
    Dim my_id As String = "1"
    Dim chname As String = "" 'Server.UrlEncode("独活")
    Dim ldname As String = "" 'Server.UrlEncode("Heracleum")
    Dim family As String = "" 'Server.UrlEncode("伞形科")
    Dim pic_path As String = ""
    Dim count_name As Integer = 0
    Dim current_prize As Integer = 0
    Dim sImage As New Magic_Class
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
            HyperLink1.Text = "返回列表"
            HyperLink1.NavigateUrl = "./list.aspx"
        End If

        'chname = Server.UrlDecode(Request.QueryString("chname")).ToString.Replace("'", "")
        ldname = "" 'Server.UrlDecode(Request.QueryString("ldname")).ToString.Replace("'", "")
        family = "" 'Server.UrlDecode(Request.QueryString("family")).ToString.Replace("'", "")

        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT ID,中文名,科名,学名 FROM magic_list where ID='" & my_id & "'", MySQL_con)
        If my_id = "" Then
            Label2.Visible = False
            Label3.Visible = False
            Label4.Visible = False
            Label5.Visible = False
            Label6.Visible = False
            FileUpload1.Visible = False
            TextBox1.Visible = False
            Button1.Visible = False
            cmd.CommandText = "SELECT ID,中文名,科名,学名 FROM magic_list where ID='1'"
        End If
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While DataRead1.Read
            chname = DataRead1.GetValue(1).ToString
            family = DataRead1.GetValue(2).ToString
            ldname = DataRead1.GetValue(3).ToString
        End While
        MySQL_con.Close()
        If phone = "13812345678" Then
            SqlDataSource1.SelectCommand = "SELECT ID,分值,电话,中文名,学名,科名,图片,分布 FROM magic_upload where 中文名='" + chname + "' order by ID desc;"
        Else
            If my_id = "" Then
                SqlDataSource1.SelectCommand = "SELECT ID,分值,电话,中文名,学名,科名,图片,分布 FROM magic_upload where 电话='" + phone + "' order by ID desc;"
            Else
                SqlDataSource1.SelectCommand = "SELECT ID,分值,电话,中文名,学名,科名,图片,分布 FROM magic_upload where 电话='" + phone + "' and 中文名='" + chname + "' order by ID desc;"
            End If
        End If

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
        If my_id = "" Then
            Label1.Text = "本人上传的所有图片列表:"
        Else
            Label1.Text = family & "<a href='../search_st_mobile.aspx?code=2&guan_code=&chname=" & chname & "'>" & chname & "</a>已有" & count_name & "张照片, 当前计分为：" & current_prize
        End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If FileUpload1.FileName <> "" Then
            Dim date_now = CleanMagicStr(System.DateTime.Now.ToString.Replace("\", "-").Replace("/", "-").Replace(":", "-").Replace(" ", "-"))
            My.Computer.FileSystem.CreateDirectory(Server.MapPath("\magic\images\upload\") + my_id)
            pic_path = date_now + "-" + phone
            FileUpload1.SaveAs(Server.MapPath("\magic\images\upload\") + my_id + "\" + pic_path + ".jpg")
            Dim my_info(7) As String
            my_info = fnINFO(Server.MapPath("\magic\images\upload\") + my_id + "\" + pic_path + ".jpg")
            Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
            MySQL_con.Open()
            If my_info(4) <> "-10000" And count_name < 14 Then
                current_prize = current_prize + 2
            End If
            If TextBox1.Text <> "" And count_name < 14 Then
                current_prize = current_prize + 2
            End If
            Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("INSERT INTO magic_upload (分值,中文名,学名,科名,电话,图片,分布,LIST_ID,经度,纬度) VALUES ('" + current_prize.ToString + "', '" + chname + "', '" + ldname + "', '" + family + "', '" + phone + "', '" + pic_path + "', '" + TextBox1.Text + "','" + my_id + "','" + my_info(4) + "','" + my_info(2) + "');", MySQL_con)
            'Label1.Text = cmd1.CommandText
            cmd1.ExecuteNonQuery()
            cmd1.CommandText = "UPDATE magic_list SET 数量 = '" & (count_name + 1).ToString & "' WHERE id = '" & my_id & "';"
            cmd1.ExecuteNonQuery()
            MySQL_con.Close()

            My.Computer.FileSystem.CreateDirectory(Server.MapPath("\magic\images\upload\small\") + my_id + "\")
            sImage.bigImage = Server.MapPath("\magic\images\upload\") + my_id + "\" + pic_path + ".jpg"
            sImage.SaveAs(Server.MapPath("\magic\images\upload\small\") + my_id + "\" + pic_path + ".jpg", 1024, 768, False)
            sImage.Close()
            Response.Redirect(Request.Url.ToString())
        Else
            Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
            MySQL_con.Open()
            Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("UPDATE magic_list SET 数量 = '" & count_name.ToString & "' WHERE id = '" & my_id & "';", MySQL_con)
            cmd1.ExecuteNonQuery()
            MySQL_con.Close()
            Response.Redirect(Request.Url.ToString())
        End If

    End Sub


    Protected Sub SqlDataSource1_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class