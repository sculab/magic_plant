Public Class rank
    Inherits System.Web.UI.Page
    Dim my_date As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        my_date = CleanMagicStr(Request.QueryString("date"))
        If my_date <> "" Then
            If my_date.Split("-").Length = 4 Then
                Label1.Text = "今日"
            Else
                Label1.Text = my_date.Split("-")(1) & "月"
            End If
        Else
            Label1.Text = "总"
        End If

        SqlDataSource1.SelectCommand = "select @rownum := @rownum + 1  as 排名, a.手机号, a.总分 , a.物种数 from ( select CONCAT(left(max(电话),3),'****',right(max(电话),4)) as 手机号, sum(分值) as 总分, count(分值) as 物种数 from magic_upload where 图片 like '" & my_date & "%' and 电话 !='13812345678' and 电话 !='888' group by 电话 order by 总分 desc) a, (select @rownum :=0) b;"

        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        MySQL_con.Open()
        Dim cmd1 As New MySql.Data.MySqlClient.MySqlCommand("SELECT COUNT(*) FROM magic_upload where 图片 like '" & my_date & "%';", MySQL_con)
        Label2.Text = "共计" & cmd1.ExecuteScalar.ToString & "张图片"
        MySQL_con.Close()

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class