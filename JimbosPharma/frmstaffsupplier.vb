Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing


Public Class frmstaffsupplier
    ' Helper function to convert byte array to Image and resize it



    Private Function ByteArrayToImage(ByVal byteArray As Byte()) As Image
        ' Check if byteArray is valid and has data
        If byteArray Is Nothing OrElse byteArray.Length = 0 Then
            Return Nothing
        End If

        Try
            Using ms As New MemoryStream(byteArray)
                Dim originalImage As Image = Image.FromStream(ms)

                ' Resize the image to specific dimensions, e.g., 100x100 pixels
                Dim resizedImage As New Bitmap(100, 130) ' Adjust the width and height as needed
                Using g As Graphics = Graphics.FromImage(resizedImage)
                    g.DrawImage(originalImage, 0, 0, 100, 100) ' Draw the image at specified size
                End Using

                Return resizedImage
            End Using
        Catch ex As ArgumentException
            ' Handle invalid image format
            Return Nothing
        End Try
    End Function

    Sub LoadUserList()
        Dim i As Integer = 0
        dataprod.Rows.Clear()

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            ' Open the connection and execute the query
            con.Open()

            Using cmd As New SqlCommand("Select * from tblSupplier order by CompanyName", con)
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        i += 1

                        ' Retrieve and convert the ImageSpl column
                        Dim img As Image = Nothing
                        If Not IsDBNull(dr("ImageSpl")) Then
                            Dim imgData As Byte() = DirectCast(dr("ImageSpl"), Byte())
                            img = ByteArrayToImage(imgData)
                        Else
                            ' Optional: Set a placeholder image if ImageSpl is NULL
                            img = Nothing ' Use your own placeholder image here
                        End If

                        ' Add row to DataGridView
                        dataprod.Rows.Add(
                            dr("SupplierID").ToString(),
                            i,
                            dr("CompanyName").ToString(),
                            dr("Address").ToString(),
                            img,
                            dr("Contact_Person").ToString(),
                            dr("Mobile_no").ToString(),
                            dr("Tel_no").ToString()
                        )
                    End While
                End Using
            End Using

            ' Update record count label
            rc1.Text = " Record Found.(" & dataprod.RowCount & ") "

        Catch ex As Exception
            MsgBox("Error loading supplier list: " & ex.Message, vbCritical)
        Finally
            ' Close the connection in case it's still open
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub


    Private Sub dataprod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataprod.CellContentClick

        Try
            Dim colname As String = dataprod.Columns(e.ColumnIndex).Name


            If colname = "View" Then
                With frmviewsupplie

                    .txtcompany.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
                    .txtcont.Text = dataprod.Rows(e.RowIndex).Cells(5).Value.ToString()
                    .txtemail.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
                    .txtmob.Text = dataprod.Rows(e.RowIndex).Cells(6).Value.ToString()
                    .txttel.Text = dataprod.Rows(e.RowIndex).Cells(7).Value.ToString()


                    Dim imageData As Byte() = Nothing
                    If Not IsDBNull(dataprod.Rows(e.RowIndex).Cells(4).Value) Then
                        Dim bitmap As Bitmap = TryCast(dataprod.Rows(e.RowIndex).Cells(4).Value, Bitmap)
                        If bitmap IsNot Nothing Then
                            Using ms As New MemoryStream()
                                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                                imageData = ms.ToArray()
                            End Using
                        End If
                    End If


                    ' Pass the image data to the frmmodifsupplier form
                    .SetImageData(imageData)
                    .ShowDialog()

                End With

            End If


            'If colname = "view" Then
            '    ' View logic here
            '    With staffviewsupplier

            '        '.txtcomp.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
            '        '.txtcontact.Text = dataprod.Rows(e.RowIndex).Cells(5).Value.ToString()
            '        '.Label3.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
            '        '.txtmobile.Text = dataprod.Rows(e.RowIndex).Cells(6).Value.ToString()
            '        '.txttel.Text = dataprod.Rows(e.RowIndex).Cells(7).Value.ToString()


            '        'Dim imageData As Byte() = Nothing
            '        'If Not IsDBNull(dataprod.Rows(e.RowIndex).Cells(4).Value) Then
            '        '    Dim bitmap As Bitmap = TryCast(dataprod.Rows(e.RowIndex).Cells(4).Value, Bitmap)
            '        '    If bitmap IsNot Nothing Then
            '        '        Using ms As New MemoryStream()
            '        '            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            '        '            imageData = ms.ToArray()
            '        '        End Using
            '        '    End If
            '        'End If


            '        '' Pass the image data to the frmmodifsupplier form
            '        '.SetImageData(imageData)
            '        .ShowDialog()


            '    End With
            'End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try



    End Sub









    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub frmstaffsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUserList()

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class