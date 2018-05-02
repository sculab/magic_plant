Public Class admin_upload
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
        HyperLink1.Text = "管理名录"
        HyperLink1.NavigateUrl = "admin_list.aspx"
        If USER_AGENT.Contains("micromessenger") Then
            GridView1.Visible = False
            TextBox1.Visible = False
            Button5.Visible = False
        Else
            If (Request.QueryString("str") Is Nothing) = False Then
                magic_str = Server.UrlDecode(Request.QueryString("str")).ToString.Replace("'", "")
            End If
            If magic_str = "" Then
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_upload order by id desc;"
            Else
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_upload where 中文名 like '%" & magic_str & "%' or 电话 like '%" & magic_str & "%' order by id desc;"
            End If


        End If

    End Sub


    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        Response.Redirect("./admin_upload.aspx?str=" & Server.UrlDecode(TextBox1.Text))
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("UPDATE magic_user SET password = '' WHERE username = '" & TextBox3.Text & "';", MySQL_con)
        cmd1.ExecuteNonQuery()
        MySQL_con.Close()
        Response.Redirect(Request.Url.ToString())
    End Sub
    Public Sub refresh_count(ByVal my_name As String)
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT COUNT(*) FROM magic_upload where 中文名='" + my_name + "';", MySQL_con)
        Dim my_count As String = cmd1.ExecuteScalar
        cmd1.CommandText = "UPDATE magic_list SET 数量 = '" & my_count & "' WHERE 中文名 = '" & my_name & "';"
        cmd1.ExecuteNonQuery()
        MySQL_con.Close()
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)

        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT 中文名 FROM magic_list", MySQL_con)
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        While DataRead1.Read
            refresh_count(DataRead1.GetValue(0).ToString)
        End While
        MySQL_con.Close()
    End Sub

    Protected Sub SqlDataSource1_Selecting(sender As Object, e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting

    End Sub
End Class