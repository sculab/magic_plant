Imports System.Collections
Imports System.ComponentModel
Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Module Magic_Module

    Public Function fnINFO(ByVal jpg_path As String) As String()
        Dim s_GPS(7) As String
        '载入图片   
        Dim objImage As System.Drawing.Image = System.Drawing.Image.FromFile(jpg_path)
        '取得所有的属性  
        Dim propertyItems As Object = objImage.PropertyItems
        '暂定纬度N(北纬)   
        Dim chrGPSLatitudeRef As Char = "N"c
        Dim Latitude_value As Double = -10000
        '暂定经度为E(东经)
        Dim chrGPSLongitudeRef As Char = "E"c
        Dim Longitude_value As Double = -10000
        '海拔
        Dim Altitude_value As Double = -10000
        '拍摄时间
        Dim get_time As String = ""
        Dim ascii As Encoding = Encoding.ASCII
        Dim objItem As PropertyItem
        Try
            For Each objItem In propertyItems
                Select Case objItem.Id
                    Case 1
                        chrGPSLatitudeRef = BitConverter.ToChar(objItem.Value, 0)
                    Case 2
                        Dim d As Double = BitConverter.ToUInt32(objItem.Value, 0) * 1D / BitConverter.ToUInt32(objItem.Value, 4)
                        Dim m As Double = BitConverter.ToUInt32(objItem.Value, 8) * 1D / BitConverter.ToUInt32(objItem.Value, 12)
                        Dim s As Double = BitConverter.ToUInt32(objItem.Value, 16) * 1D / BitConverter.ToUInt32(objItem.Value, 20)
                        Latitude_value = d + m / 60 + s / 3600

                    Case 3
                        chrGPSLongitudeRef = BitConverter.ToChar(objItem.Value, 0)
                    Case 4
                        Dim d As Double = BitConverter.ToUInt32(objItem.Value, 0) * 1D / BitConverter.ToUInt32(objItem.Value, 4)
                        Dim m As Double = BitConverter.ToUInt32(objItem.Value, 8) * 1D / BitConverter.ToUInt32(objItem.Value, 12)
                        Dim s As Double = BitConverter.ToUInt32(objItem.Value, 16) * 1D / BitConverter.ToUInt32(objItem.Value, 20)
                        Longitude_value = d + m / 60 + s / 3600
                    Case 6
                        Altitude_value = BitConverter.ToUInt32(objItem.Value, 0) * 1D / BitConverter.ToUInt32(objItem.Value, 4)
                    Case 306
                        get_time = ascii.GetString(objItem.Value)
                End Select
            Next
        Catch ex As Exception
            Return s_GPS
        End Try
        s_GPS(0) = jpg_path.Replace("/", "\").Substring(jpg_path.Replace("/", "\").LastIndexOf("\") + 1)
        s_GPS(1) = chrGPSLatitudeRef
        s_GPS(2) = Latitude_value
        s_GPS(3) = chrGPSLongitudeRef
        s_GPS(4) = Longitude_value
        s_GPS(5) = Altitude_value
        If get_time.Length >= 10 Then
            If get_time.Contains(":") Then
                s_GPS(6) = get_time.Substring(0, 10).Replace(":", "-")
                s_GPS(7) = get_time.Substring(11)
            End If
        End If
        Return s_GPS
    End Function
    Public Function CleanMagicStr(ByVal input As Object) As String
        If (input Is Nothing) = False Then
            If input.length >= 2 Then
                Return input.Replace("'", "").Replace(";", "").Replace("(", "").Replace(")", "").Replace("=", "")
            End If
        End If
        Return input
    End Function
End Module
