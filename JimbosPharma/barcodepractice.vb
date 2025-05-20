Imports MessagingToolkit.Barcode

Public Class barcodepractice

    Private Sub barcodepractice_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btngenerate_Click(sender As Object, e As EventArgs) Handles btngenerate.Click

        Dim Generator As New MessagingToolkit.Barcode.BarcodeEncoder
        Generator.BackColor = Color.White
        Generator.LabelFont = New Font("Arial", 7, FontStyle.Regular)
        Generator.IncludeLabel = True
        Generator.CustomLabel = txt.Text
        Try
            picbarcode.Image = New Bitmap(Generator.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code93, txt.Text))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim sd As New SaveFileDialog
        SD.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        sd.FileName = txt.Text
        SD.SupportMultiDottedExtensions = True
        SD.AddExtension = True
        SD.Filter = "PNG File|*.png"
        If SD.ShowDialog() = DialogResult.OK Then
            Try
                picbarcode.Image.Save(sd.FileName, Imaging.ImageFormat.Png)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub picbarcode_Click(sender As Object, e As EventArgs) Handles picbarcode.Click

    End Sub
End Class