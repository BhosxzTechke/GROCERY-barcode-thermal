Imports System.Data.SqlClient

Public Class frmunitlist

    Private Sub btnew_Click(sender As Object, e As EventArgs) Handles btnew.Click
        With frmunit
            .btnsave.Visible = True
            .btnupdate.Visible = False
            .ShowDialog()
        End With
    End Sub


    Sub unitview()    ' a subroutine or subprocedure to create a specific task in this case is we will open a connection and read a table in tblcategory and ang purpose ng while loop is to read each row from the database result set and add it to the DataGridView.
        Dim i As Integer = 0
        dataunit.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("Select * from tblunit order by unit", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            dataunit.Rows.Add(dr.Item("unitID").ToString, i, dr.Item("unit").ToString)
        End While
        dr.Close()
        con.Close()
        rc1.Text = " Record Found.(" & dataunit.RowCount & ") "

        ' bro im cooked



    End Sub



    Private Sub DataView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub frmunitlist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        unitview()

        dataunit.AlternatingRowsDefaultCellStyle = dataunit.RowsDefaultCellStyle

    End Sub

    Private Sub DataView_Leave(sender As Object, e As EventArgs)
        With frmunit
            .txtunit.Clear()
            .btnupdate.Visible = False
            .btnsave.Visible = True

        End With
    End Sub
    Sub SearchUNIT()
        Dim i As Integer = 0
        dataunit.Rows.Clear()

        ' Open database connection
        con.Open()

        ' Use parameterized query to avoid SQL injection
        Dim query As String = "SELECT * FROM tblunit WHERE unit LIKE @searchText"
        cmd = New SqlClient.SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@searchText", searchboxing.Text & "%")

        ' Execute the reader
        dr = cmd.ExecuteReader()
        While dr.Read()
            i += 1
            dataunit.Rows.Add(i, dr.Item("unitID").ToString(), dr.Item("unit").ToString())
        End While

        ' Close the reader and connection
        dr.Close()
        con.Close()
    End Sub


    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub dataprod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataunit.CellContentClick
        ' ilalabas nito yung form na dialog "frmcategory" but in this case is mapipindot mo sia sa column 4 or an update button na nilagay natin 
        ' then magshoshow si update and mahahide si save                                                                                                                                                             
        Dim colname As String = dataunit.Columns(e.ColumnIndex).Name

        If colname = "Edit" Then
            With frmunit
                .lblid.Text = dataunit.Rows(e.RowIndex).Cells(0).Value.ToString()
                .txtunit.Text = dataunit.Rows(e.RowIndex).Cells(2).Value.ToString()
                .btnsave.Visible = False
                .btnupdate.Visible = True
                .ShowDialog()
            End With


        ElseIf colname = "Delete" Then
            ' Confirm before deleting
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this Unit?",
                                                 "Confirm Deletion",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning)

            If result = DialogResult.No Then
                Return
            End If



            If result = DialogResult.Yes Then
                Try
                    ' Delete Record
                    Using cmd As New SqlClient.SqlCommand("DELETE FROM tblunit WHERE unitID = @unitID", con)
                        cmd.Parameters.AddWithValue("@unitID", dataunit.Rows(e.RowIndex).Cells(0).Value.ToString())
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using



                    MessageBox.Show("Unit has been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)


                    ' Refresh unit List
                    unitview()

                Catch ex As Exception
                    ' Error Message
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Error"
                    Guna2MessageDialog1.Text = "An error occurred: " & ex.Message
                    Guna2MessageDialog1.Show()
                Finally
                    con.Close()
                End Try
            End If

        End If
    End Sub

    Private Sub dataunit_Leave(sender As Object, e As EventArgs) Handles dataunit.Leave
        With frmunit
            .txtunit.Clear()
            .btnupdate.Visible = False
            .btnsave.Visible = True

        End With
    End Sub


    Private Sub Guna2TextBox2_TextChanged(sender As Object, e As EventArgs)
        SearchUNIT()

    End Sub

    Private Sub btnew_Leave(sender As Object, e As EventArgs) Handles btnew.Leave
        With frmunit
            .txtunit.Clear()

        End With
    End Sub

    Private Sub searchboxing_TextChanged_1(sender As Object, e As EventArgs) Handles searchboxing.TextChanged
        SearchUNIT()
    End Sub
End Class