Imports System.IO

Public Class frmviewsupplie

    Private Sub frmviewsupplie_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub SetImageData(ByVal imageData As Byte())
        If imageData IsNot Nothing AndAlso imageData.Length > 0 Then
            Try
                ' Convert byte array to Image and set it to PictureBox
                Using ms As New MemoryStream(imageData)
                    picture.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                ' Handle any errors (e.g., invalid image format)
                MessageBox.Show("Error loading image: " & ex.Message)
                picture.Image = Nothing ' Optional: Set default image if error occurs
            End Try
        Else
            ' If no image data is available, clear the PictureBox
            picture.Image = Nothing
        End If
    End Sub

    Private Sub staffviewsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub
End Class