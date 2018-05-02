Imports System.IO
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Net
<ComClass(Magic_Class.ClassId, Magic_Class.InterfaceId, Magic_Class.EventsId)>
Public Class Magic_Class
#Region "COM GUIDs"
    ' 这些 GUID 提供该类的 COM 标识及其 COM 接口。
    ' 如果您更改它们，现有的客户端将再也无法
    ' 访问该类。
    Public Const ClassId As String = "29641F37-8FA4-4ED9-9118-9DA8EFA306B9"
    Public Const InterfaceId As String = "06E4B037-2461-4F83-96BE-2A5D1CAAB0CE"
    Public Const EventsId As String = "802EBB14-2D4D-416E-BA26-E8ADCD480E26"
#End Region

    ' 可创建的 COM 类必须具有不带参数的 
    ' Public Sub New()，否则，该类将不会注册到 COM 注册表中，
    ' 而且不能通过 CreateObject 
    ' 来创建。
    Private myImage As Drawing.Bitmap
    Private syimg As Drawing.Bitmap
    Private syok As Boolean = False
    Private myok As Boolean = False
    Public Sub New()
        MyBase.New()
    End Sub
    Public WriteOnly Property bigImage() As String
        Set(ByVal Value As String)
            Try
                myImage = New Bitmap(Value)
                myok = True
            Catch e As IO.IOException
                myok = False
            End Try
        End Set
    End Property
    Public WriteOnly Property LogoImage() As String
        Set(ByVal Value As String)
            Try
                syimg = New Bitmap(Value)
                syok = True
            Catch ex As Exception
                syok = False
            End Try
        End Set
    End Property
    Public Function SaveAs(ByVal ToFile As String, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal nLogo As Boolean) As String
        Try
            If myok = False Then
                Return "err0"
                Exit Function
            End If
            If (myImage.Width - myImage.Height) * (nWidth - nHeight) < 0 Then
                Dim temp As Integer
                temp = nWidth
                nWidth = nHeight
                nHeight = temp
            End If

            Dim iX As Integer
            Dim iY As Integer
            Dim xMax As Integer
            Dim yMax As Integer
            'For iX = 0 To nWidth - 1
            '    For iY = 0 To nHeight - 1
            '        newbmp.SetPixel(iX, iY, Color.White)
            '    Next
            'Next
            If (myImage.Width - myImage.Height) * (nWidth - nHeight) < 0 Then
                Dim temp As Integer
                temp = nWidth
                nWidth = nHeight
                nHeight = temp
            End If

            If nWidth < myImage.Width Or nHeight < myImage.Height Then
                If myImage.Width / myImage.Height > nWidth / nHeight Then
                    xMax = nWidth
                    yMax = myImage.Height * nWidth \ myImage.Width
                Else
                    yMax = nHeight
                    xMax = myImage.Width * nHeight \ myImage.Height
                End If
            Else
                xMax = myImage.Width
                yMax = myImage.Height
            End If

            Dim newbmp As Bitmap = New Bitmap(xMax, yMax, Imaging.PixelFormat.Format16bppArgb1555)

            Dim tembmp As Bitmap = New Bitmap(myImage, xMax, yMax)
            xMax = (newbmp.Width - tembmp.Width) \ 2
            yMax = (newbmp.Height - tembmp.Height) \ 2
            For iX = 0 To tembmp.Width - 1
                For iY = 0 To tembmp.Height - 1
                    newbmp.SetPixel(iX + xMax, iY + yMax, tembmp.GetPixel(iX, iY))
                Next
            Next
            If syok And nLogo Then
                Dim cob As Color
                Dim coc As Color
                xMax = newbmp.Width - syimg.Width - 4
                yMax = newbmp.Height - syimg.Height - 3
                For iX = 0 To syimg.Width - 1
                    For iY = 0 To syimg.Height - 1
                        cob = syimg.GetPixel(iX, iY)
                        coc = newbmp.GetPixel(iX + xMax, iY + yMax)
                        newbmp.SetPixel(iX + xMax, iY + yMax, getnewco(cob, coc))
                    Next
                Next
            End If
            newbmp.Save(ToFile, Imaging.ImageFormat.Jpeg)
            newbmp.Dispose()
            tembmp.Dispose()
            newbmp = Nothing
            tembmp = Nothing
            Return "OK"
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function

    Public ReadOnly Property Width() As Integer
        Get
            Return myImage.Width
        End Get
    End Property
    Public ReadOnly Property Height() As Integer
        Get
            Return myImage.Height
        End Get
    End Property
    Public Sub Close()
        myImage.Dispose()
        'syimg.Dispose()
        myImage = Nothing
        'syimg = Nothing
    End Sub
    Private Function getnewco(ByVal c1 As Color, ByVal c2 As Color) As Color
        Dim a1 As Integer = c1.A
        Dim r1 As Integer = c1.R
        Dim g1 As Integer = c1.G
        Dim b1 As Integer = c1.B
        Dim a2 As Integer = c2.A
        Dim r2 As Integer = c2.R
        Dim g2 As Integer = c2.G
        Dim b2 As Integer = c2.B
        a2 = 255 - a1
        r1 = CInt((r1 * a1 / 255) + (r2 * a2 / 255))
        g1 = CInt((g1 * a1 / 255) + (g2 * a2 / 255))
        b1 = CInt((b1 * a1 / 255) + (b2 * a2 / 255))
        Return Color.FromArgb(a1, r1, g1, b1)
    End Function
End Class
Public Class AccessToken
    ' 调用getAccessToken()获取的 access_token建议根据expires_in 时间 设置缓存
    ' 返回token示例

    ' 百度云中开通对应服务应用的 API Key 建议开通应用的时候多选服务
    Private clientId As String = "NM5wC7gM9gGVTEuGlBWfyPvP"
    ' 百度云中开通对应服务应用的 Secret Key
    Private clientSecret As String = "Tp1O2AAVbAFeYFGFhLmeM2QrWs3XKAgc"

    Public Function getAccessToken()
        Dim authHost As String = "https://aip.baidubce.com/oauth/2.0/token?grant_type=client_credentials&client_id=" & clientId & "&client_secret=" & clientSecret
        Dim client As New WebClient()
        Dim data As Stream = client.OpenRead(authHost)
        Dim reader As New StreamReader(data)
        Dim s As String = reader.ReadToEnd()
        data.Close()
        reader.Close()
        Return s
    End Function
    Public Function getstring(ByVal url As String)
        Dim client As New WebClient()
        Dim data As Stream = client.OpenRead(url)
        Dim reader As New StreamReader(data)
        Dim s As String = reader.ReadToEnd()
        data.Close()
        reader.Close()
        Return s
    End Function
End Class
