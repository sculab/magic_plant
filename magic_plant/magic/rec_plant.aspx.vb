Imports Newtonsoft.Json.Linq
Imports System.Drawing
Imports System.IO
Imports System.Net

Public Class rec_plant
    Inherits System.Web.UI.Page
    Dim my_token As New AccessToken
    Dim token_result As JObject
    Dim sImage As New Magic_Class
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        token_result = JObject.Parse(my_token.getAccessToken)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If FileUpload1.FileName <> "" Then
            Dim date_now = CleanMagicStr(System.DateTime.Now.ToString.Replace("\", "-").Replace("/", "-").Replace(":", "-").Replace(" ", "-"))
            FileUpload1.SaveAs(Server.MapPath("\magic\images\rec_plant\") + date_now + ".jpg")
            Dim out_file As String = Server.MapPath("\magic\images\rec_plant\") + date_now + "-out.jpg"
            sImage.bigImage = Server.MapPath("\magic\images\rec_plant\") + date_now + ".jpg"
            sImage.SaveAs(out_file, 1024, 768, False)
            sImage.Close()

            'Dim main_obj_result As JObject = JObject.Parse(detect(out_file))
            'Dim img As New Bitmap(out_file)
            'Dim org_width As Integer = img.Width
            'Dim org_height As Integer = img.Height
            'Dim rc As Rectangle = New Rectangle(CInt(main_obj_result("result")("left")), CInt(main_obj_result("result")("top")), CInt(main_obj_result("result")("width")), CInt(main_obj_result("result")("height")))   '起点和长宽
            'Dim newImg As Bitmap = img.Clone(rc, Imaging.PixelFormat.Format32bppArgb)
            'img.Dispose()
            'img = Nothing
            'newImg.Save(out_file + ".tmp", Imaging.ImageFormat.Jpeg)
            'newImg.Dispose()
            'newImg = Nothing

            Dim customize_result As String = ""
            customize_result = customize_detect(out_file, "https://aip.baidubce.com/rpc/2.0/ai_custom/v1/classification/scuplantv1")
            customize_result = customize_result.Substring(customize_result.IndexOf(",") + 1)
            customize_result = "{" + customize_result
            Dim get_result As JObject = JObject.Parse(customize_result)
            Label1.Text = "鉴定结果：<br />"
            For Each i In get_result("results")
                Dim MySQL_con As New MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings("magicConnectionString").ConnectionString)
                Dim cmd As New MySql.Data.MySqlClient.MySqlCommand("SELECT 中文名 from magic_list where ID='" & i("name").ToString & "'", MySQL_con)
                MySQL_con.Open()
                Dim DataRead1 As MySql.Data.MySqlClient.MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                Dim temp_name As String = ""
                While DataRead1.Read
                    temp_name = DataRead1.GetValue(0).ToString
                End While
                MySQL_con.Close()
                Label1.Text += "<a href='./list_view.aspx?id=" + i("name").ToString + "'>" + temp_name + "</a>，可靠度" + (CSng(i("score")) * 100).ToString("F2") + "%<br />"
            Next

            If File.Exists(out_file) Then
                File.Delete(out_file)
            End If

            'If File.Exists(out_file + ".tmp") Then
            '    File.Delete(out_file + ".tmp")
            'End If
        End If
    End Sub

    Public Function customize_detect(ByVal input_file As String, ByVal address As String) As String
        Dim token As String = token_result("access_token").ToString
        Dim host As String = address & "?access_token=" & token
        Dim encoding As Encoding = Encoding.Default
        Dim request As HttpWebRequest = CType(WebRequest.Create(host), HttpWebRequest)
        request.Method = "post"
        request.KeepAlive = True
        request.ContentType = "application/json"
        ' 图片的base64编码
        Dim base64 As String = Convert.ToBase64String(IO.File.ReadAllBytes(input_file))
        Dim str As String = "{" + """" + "top_num" + """" + ": 5," + """" + "image" + """" + ":" + """" + base64 + """" + "}"
        Dim buffer() As Byte = encoding.GetBytes(str)
        request.ContentLength = buffer.Length
        request.GetRequestStream().Write(buffer, 0, buffer.Length)
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.Default)
        Dim result As String = reader.ReadToEnd()
        Return result
    End Function

    Public Function detect(ByVal input_file As String) As String
        Dim token As String = token_result("access_token").ToString
        Dim host As String = "https://aip.baidubce.com/rest/2.0/image-classify/v1/object_detect?access_token=" + token
        Dim encoding As Encoding = Encoding.Default
        Dim request As HttpWebRequest = CType(WebRequest.Create(host), HttpWebRequest)
        request.Method = "post"
        request.KeepAlive = True
        ' 图片的base64编码
        Dim base64 As String = Convert.ToBase64String(IO.File.ReadAllBytes(input_file))
        Dim str As String = "image=" + HttpUtility.UrlEncode(base64) + "&with_face=" + "0"
        Dim buffer() As Byte = encoding.GetBytes(str)
        request.ContentLength = buffer.Length
        request.GetRequestStream().Write(buffer, 0, buffer.Length)
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        Dim reader As StreamReader = New StreamReader(response.GetResponseStream(), Encoding.Default)
        Dim result As String = reader.ReadToEnd()
        Return result
    End Function
    'Public Sub rec_plant(ByVal file_path As String)
    '    Dim APP_ID As String = "10994405"
    '    Dim API_KEY As String = "NM5wC7gM9gGVTEuGlBWfyPvP"
    '    Dim SECRET_KEY As String = "Tp1O2AAVbAFeYFGFhLmeM2QrWs3XKAgc"
    '    Dim client As Object = New Baidu.Aip.ImageClassify.ImageClassify(API_KEY, SECRET_KEY)
    '    Dim image As Object = System.IO.File.ReadAllBytes(file_path)

    '    Dim result As Newtonsoft.Json.Linq.JObject = client.PlantDetect(image)

    '    Label1.Text = "鉴定结果：<br />"
    '    For Each i As Newtonsoft.Json.Linq.JObject In result("result")
    '        Label1.Text += "<a href='../search_st_mobile.aspx?code=2&guan_code=&chname=" + i("name").ToString + "'>" + i("name").ToString + "</a>，可靠度" + (CSng(i("score")) * 100).ToString("F2") + "%<br />"
    '    Next

    'End Sub
End Class