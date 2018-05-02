
Imports System.IO
Public Class view
    Inherits System.Web.UI.Page
    Dim my_id As String = "1"
    Dim sImage As New Magic_Class
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        my_id = CleanMagicStr(Request.QueryString("id"))
        If my_id = "" Then
            my_id = 1
        End If
        SqlDataSource1.SelectCommand = "SELECT *, CONCAT(left(电话,3),'****',right(电话,4)) as 手机号 FROM magic_upload where id ='" & my_id & "';"
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT 图片,LIST_ID FROM magic_upload where id ='" & my_id & "';", MySQL_con)
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim pic_path As String = ""
        Dim list_id As String = ""
        While DataRead1.Read
            pic_path = DataRead1.GetValue(0).ToString
            list_id = DataRead1.GetValue(1).ToString
        End While
        MySQL_con.Close()
        If File.Exists(Server.MapPath("\magic\images\upload\small\") + list_id + "\" + pic_path + ".jpg") = False Then
            sImage.bigImage = Server.MapPath("\magic\images\upload\") + list_id + "\" + pic_path + ".jpg"
            My.Computer.FileSystem.CreateDirectory(Server.MapPath("\magic\images\upload\small\") + list_id + "\")
            sImage.SaveAs(Server.MapPath("\magic\images\upload\small\") + list_id + "\" + pic_path + ".jpg", 1024, 768, False)
            sImage.Close()
        End If
    End Sub
    Public Function hidephone(ByVal phone As String)
        Dim r As String = phone.Substring(0, 3) & "****" & phone.Substring(8, 4)
        Return r
    End Function
    Public Sub showmap()
        Dim my_info(1) As String
        Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
        Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT 图片,LIST_ID,经度,纬度 FROM magic_upload where id ='" & my_id & "';", MySQL_con)
        MySQL_con.Open()
        Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim pic_path As String = ""
        Dim list_id As String = ""
        While DataRead1.Read
            pic_path = DataRead1.GetValue(0).ToString
            list_id = DataRead1.GetValue(1).ToString
            my_info(0) = DataRead1.GetValue(2).ToString
            my_info(1) = DataRead1.GetValue(3).ToString
        End While
        MySQL_con.Close()
        Response.Write("var x = " & my_info(0) & ";")
        Response.Write("var y = " & my_info(1) & ";")
    End Sub

    Protected Sub DataList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DataList1.SelectedIndexChanged

    End Sub

    Protected Sub SqlDataSource1_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting

    End Sub
End Class