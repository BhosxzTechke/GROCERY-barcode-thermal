Public Class frmcategorylist

    'Dim id, _category As String


    Private Sub btnew_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Dispose()


    End Sub

    Private Sub frmcategorylist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataprod.AlternatingRowsDefaultCellStyle = dataprod.RowsDefaultCellStyle
        catview()




    End Sub



     Sub catview()    ' a subroutine or subprocedure to create a specific task in this case is we will open a connection and read a table in tblcategory and ang purpose ng while loop is to read each row from the database result set and add it to the DataGridView.
        Dim i As Integer = 0
        dataprod.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("Select * from tblcategory order by Category", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            dataprod.Rows.Add(dr.Item("catID").ToString, i, dr.Item("Category").ToString)
        End While
        dr.Close()
        con.Close()
        rc1.Text = " Record Found.(" & dataprod.RowCount & ") "

    End Sub


    Private Sub rc1_TextChanged(sender As Object, e As EventArgs)
        rc1.Enabled = False


    End Sub




    Private Sub Button10_Click_1(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With frmcategory


            .btnupdate.Visible = False
            .btnsave.Visible = True

            .ShowDialog()
        End With
    End Sub


    Sub Searchcat()
        Dim i As Integer = 0
        dataprod.Rows.Clear()

        ' Open database connection
        con.Open()

        ' Use parameterized query to avoid SQL injection
        Dim query As String = "SELECT * FROM tblcategory WHERE Category LIKE @searchText"
        cmd = New SqlClient.SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@searchText", searchboxing.Text & "%")

        ' Execute the reader
        dr = cmd.ExecuteReader()
        While dr.Read()
            i += 1
            dataprod.Rows.Add(i, dr.Item("catID").ToString(), dr.Item("Category").ToString())
        End While

        ' Close the reader and connection
        dr.Close()
        con.Close()
    End Sub

    Private Sub Button1_Leave(sender As Object, e As EventArgs) Handles Button1.Leave 'exit button
        With frmcategory
            .txtcategory.Clear()

        End With
    End Sub


    Private Sub dataprod_Leave_1(sender As Object, e As EventArgs)
        With frmcategory
            .txtcategory.Clear()
            .btnupdate.Visible = False
            .btnsave.Visible = True

        End With
    End Sub



    Private Sub dataprod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataprod.CellContentClick


        ' ilalabas nito yung form na dialog "frmcategory" but in this case is mapipindot mo sia sa column 4 or an update button na nilagay natin 
        ' then magshoshow si update and mahahide si save                                                                                                                                                             
        Dim colname As String = dataprod.Columns(e.ColumnIndex).Name

        If colname = "Edit" Then ' FOR UPDATE IT MEANS YOU NEED TO AN ID TO FIND OUT  
            With frmcategory
                .lblID.Text = dataprod.Rows(e.RowIndex).Cells(0).Value.ToString() 'THIS IS FOR ID
                .txtcategory.Text = dataprod.Rows(e.RowIndex).Cells(2).Value.ToString
                .btnsave.Visible = False
                .btnupdate.Visible = True

                .ShowDialog()
            End With


        ElseIf colname = "Delete" Then
            ' Confirm before deleting
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this Category?",
                                                 "Confirm Deletion",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                Return
            End If



            If result = DialogResult.Yes Then
                Try
                    ' Delete Record
                    Using cmd As New SqlClient.SqlCommand("DELETE FROM tblcategory WHERE catID = @catID", con)
                        cmd.Parameters.AddWithValue("@catID", dataprod.Rows(e.RowIndex).Cells(0).Value.ToString())
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using



                    MessageBox.Show("Category has been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    catview()

                Catch ex As Exception
                    ' Error Message
                    Guna2MessageDialog2.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog2.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog2.Caption = "Error"
                    Guna2MessageDialog2.Text = "An error occurred: " & ex.Message

                    ' Show error dialog
                    Guna2MessageDialog2.Show()
                Finally
                    con.Close()
                End Try
            End If



        End If
    End Sub

    Private Sub searchboxing_TextChanged_1(sender As Object, e As EventArgs) Handles searchboxing.TextChanged
        Searchcat()
    End Sub
End Class