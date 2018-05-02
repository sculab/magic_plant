Public Class admin_list
    Inherits System.Web.UI.Page
    Dim magic_str As String = ""
    Dim action As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        action = CleanMagicStr(Request.QueryString("action"))
        If action = "logout" Then
            HttpContext.Current.Session("phone") = ""
            HyperLink2.Visible = False
            Response.Redirect("./list.aspx")
        End If
        If HttpContext.Current.Session("phone") <> "13812345678" Then
            Response.Redirect("./list.aspx")
        Else
            HyperLink2.NavigateUrl = "list.aspx?action=logout"
        End If
        Dim USER_AGENT As String = Request.ServerVariables("HTTP_USER_AGENT").ToLower
        Dim date_now = CleanMagicStr(System.DateTime.Now.ToString.Replace("\", "-").Replace("/", "-").Replace(":", "-").Replace(" ", "-"))
        HyperLink1.Text = "管理上传"
        HyperLink1.NavigateUrl = "admin_upload.aspx"
        If USER_AGENT.Contains("micromessenger") Then
            GridView1.Visible = False
            TextBox1.Visible = False
            Button5.Visible = False
        Else
            If (Request.QueryString("str") Is Nothing) = False Then
                magic_str = Server.UrlDecode(Request.QueryString("str")).ToString.Replace("'", "")
            End If
            If magic_str = "" Then
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_list order by id desc;"
            Else
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_list where 中文名 like '%" & magic_str & "%' order by id desc;"
            End If

         
        End If

    End Sub


    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        Response.Redirect("./admin_list.aspx?str=" & Server.UrlDecode(TextBox1.Text))
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("INSERT INTO magic_list (中文名, 学名, 科名, 属名, 数量) VALUES ('" & TextBox2.Text & "', '', '', '', 0)", MySQL_con)
        cmd1.ExecuteNonQuery()
        MySQL_con.Close()
        Response.Redirect(Request.Url.ToString())
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("UPDATE magic_user SET password = '' WHERE username = '" & TextBox3.Text & "';", MySQL_con)
        cmd1.ExecuteNonQuery()
        MySQL_con.Close()
        Response.Redirect(Request.Url.ToString())
    End Sub
End Class