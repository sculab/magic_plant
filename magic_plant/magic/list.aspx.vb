Public Class magic_list
    Inherits System.Web.UI.Page
    Dim magic_str As String = ""
    Dim action As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        action = CleanMagicStr(Request.QueryString("action"))
        If action = "logout" Then
            HttpContext.Current.Session("phone") = ""
            HyperLink2.Visible = False
        End If
        If HttpContext.Current.Session("phone") = "13812345678" Then
            HyperLink3.Visible = True
            HyperLink4.Visible = True
            HyperLink6.Visible = False
        End If

        HyperLink1.Text = System.DateTime.Now.Month.ToString + "月榜"
        HyperLink1.NavigateUrl = "rank.aspx?date=" & System.DateTime.Now.Year.ToString & "-" & System.DateTime.Now.Month.ToString & "-"
        HyperLink7.Text = "今日榜"
        HyperLink7.NavigateUrl = "rank.aspx?date=" & System.DateTime.Now.Year.ToString & "-" & System.DateTime.Now.Month.ToString & "-" & System.DateTime.Now.Day.ToString & "-"

        Dim USER_AGENT As String = Request.ServerVariables("HTTP_USER_AGENT").ToLower
        If USER_AGENT.Contains("micromessenger") Then
            Label1.Text = "请点击右上角菜单，选择在浏览器打开！"
            Label2.Visible = False
            Username.Visible = False
            Password.Visible = False
            Button6.Visible = False
            Label1.Visible = True
            GridView1.Visible = False
            SearchBox.Visible = False
            Button5.Visible = False
            HyperLink5.Visible = False
            HyperLink6.Visible = False
        Else
            Label1.Visible = False
            If (Request.QueryString("str") Is Nothing) = False Then
                magic_str = Server.UrlDecode(Request.QueryString("str")).ToString.Replace("'", "")
            End If
            If magic_str = "" Then
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_list order by CONVERT(科名 USING gbk) DESC;"
            Else
                SqlDataSource1.SelectCommand = "SELECT * FROM magic_list where 中文名 like '%" & magic_str & "%' or 科名 like '%" & magic_str & "%' order by CONVERT(科名 USING gbk) DESC;"
            End If
            If HttpContext.Current.Session("phone") <> "" Then
                Username.Visible = False
                Button6.Visible = False
                Password.Visible = False
                HyperLink2.Visible = True
                HyperLink2.NavigateUrl = "list.aspx?action=logout"
            Else
                Password.Visible = True
                Username.Visible = True
                Button6.Visible = True
                HyperLink2.Visible = False
            End If
            Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
            Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT CONCAT('***',right(a.电话,4)),a.中文名, a.ID, a.分布 FROM magic_upload a WHERE a.ID in (SELECT max(ID) FROM magic_upload where 电话 !='888' and 电话 !='13812345678' and 分布!='' GROUP BY 电话) order by a.ID desc;", MySQL_con)
            MySQL_con.Open()
            Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Label2.Text = "<marquee scrollAmount=2 width='100%'>"
            While DataRead1.Read
                If DataRead1.GetValue(3).ToString <> "" Then
                    Label2.Text = Label2.Text + DataRead1.GetValue(0).ToString & "在" & DataRead1.GetValue(3).ToString & "发现了<a href='./view.aspx?id=" & DataRead1.GetValue(2).ToString & "'>" & DataRead1.GetValue(1).ToString & "</a> "
                Else
                    Label2.Text = Label2.Text + DataRead1.GetValue(0).ToString & "发现了<a href='./view.aspx?id=" & DataRead1.GetValue(2).ToString & "'>" & DataRead1.GetValue(1).ToString & "</a> "

                End If
            End While
            Label2.Text = Label2.Text + "</marquee>"
            MySQL_con.Close()
        End If
    End Sub


    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        Response.Redirect("./list.aspx?str=" & Server.UrlDecode(SearchBox.Text))
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If IsNumeric(Username.Text) And Username.Text.Length = 11 Then
            If Username.Text.StartsWith("1") Then
                Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
                Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT ID,username,password from magic_user where username='" & Username.Text & "'", MySQL_con)
                MySQL_con.Open()
                Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                Dim temp_uname As String = ""
                Dim temp_pword As String = ""
                While DataRead1.Read
                    temp_uname = DataRead1.GetValue(1).ToString
                    temp_pword = DataRead1.GetValue(2).ToString
                End While
                MySQL_con.Close()

                If temp_uname = "" Then
                    Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("INSERT INTO magic_user (username,password) VALUES ('" + Username.Text + "', '" + Password.Text + "');", MySQL_con)
                    MySQL_con.Open()
                    cmd1.ExecuteNonQuery()
                    MySQL_con.Close()
                    HttpContext.Current.Session("phone") = Username.Text
                    HttpContext.Current.Session.Timeout = 6000
                    Response.Redirect(Request.Url.ToString())
                Else
                    If temp_pword = Password.Text Then
                        HttpContext.Current.Session("phone") = Username.Text
                        HttpContext.Current.Session.Timeout = 6000
                        Response.Redirect("./list.aspx")
                    ElseIf temp_pword = "" Then
                        MySQL_con.Open()
                        Dim cmd2 As New MySql.Data.MySqlClient.MySqlCommand("UPDATE magic_user SET password = '" & Password.Text & "' WHERE username = '" & Username.Text & "';", MySQL_con)
                        cmd2.ExecuteNonQuery()
                        MySQL_con.Close()
                        HttpContext.Current.Session("phone") = Username.Text
                        HttpContext.Current.Session.Timeout = 6000
                        Response.Redirect("./list.aspx")
                    Else
                        Username.Text = "用户名/密码错误"
                    End If
                End If


            End If
        Else
            Username.Text = "请输入正确的手机号！"
        End If

    End Sub

End Class