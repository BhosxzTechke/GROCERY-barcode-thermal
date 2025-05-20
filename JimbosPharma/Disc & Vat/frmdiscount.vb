Public Class frmdiscount

    'Dim _id As String

    Private Sub txtdiscount_KeyPress(sender As Object, e As KeyPressEventArgs)
        Select Case Asc(e.KeyChar)
            Case 48 To 57 ' 0 - 9
            Case 46
            Case 8
            Case Else
                e.Handled = True


        End Select
    End Sub

    Private Sub txtdiscount_TextChanged(sender As Object, e As EventArgs)

    End Sub

    'Private Sub btnsave_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Walanglaman(txtdesc) = True Then Return
    '        If Walanglaman(txtdiscount) = True Then Return

    '        If ValidateDuplicateEntry("select * from tbldiscount where Description_disc like '" & txtdesc.Text & "'") = True Then Return

    '        con.Open()
    '        cmd = New SqlClient.SqlCommand("INSERT INTO  tbldiscount (Description_disc, Discount ) VALUES (@Description_disc, @Discount)", con)
    '        With cmd
    '            .Parameters.AddWithValue("@Description_disc", txtdesc.Text)
    '            .Parameters.AddWithValue("@Discount", txtdiscount.Text)
    '            .ExecuteNonQuery()


    '        End With
    '        con.Close()
    '        MsgBox("Discount has been succesfully saved.", vbInformation)
    '        loaddiscount()
    '        cleardiscount()



    '    Catch ex As Exception
    '        con.Close()
    '        MsgBox(ex.Message, vbCritical)

    '    End Try
    'End Sub

 


    Sub loaddiscount()
        Dim i As Integer = 0
        Guna2DataGridView2.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("Select * from tbldiscount", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView2.Rows.Add(i, dr.Item(0).ToString, dr.Item(1).ToString, dr.Item(2).ToString)
        End While
        dr.Close()
        con.Close()


    End Sub

    Private Sub savebtn_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Sub Guna2DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellContentClick
        Dim colname As String = Guna2DataGridView2.Columns(e.ColumnIndex).Name
        If colname = "coledit" Then
            With frmdiscountmodif
                .lblid.Text = Guna2DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString
                .txtdesc.Text = Guna2DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString
                Dim discountValue As Double = CDbl(Guna2DataGridView2.Rows(e.RowIndex).Cells(3).Value)
                .txtdiscount.Text = (discountValue * 100).ToString("0") ' Show as whole number
                .btnsave.Visible = False
                .btnupdate.Visible = True
                .ShowDialog()

            End With


        ElseIf colname = "coldelete" Then
            If MsgBox("Are you sure you want to delete this discount?", vbYesNo + vbQuestion) = vbYes Then
                con.Open()
                cmd = New SqlClient.SqlCommand("delete from tbldiscount where ID =" & Guna2DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString(), con)
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Discount has been succesfully deleted", vbInformation)
                loaddiscount()
            End If



        End If
    End Sub

    'Private Sub btnupdate_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Walanglaman(txtdesc) = True Then Return
    '        If Walanglaman(txtdiscount) = True Then Return

    '        con.Open()
    '        cmd = New SqlClient.SqlCommand("UPDATE tbldiscount set Description_disc =@Description_disc, Discount=@Discount where ID like @ID", con)
    '        With cmd
    '            .Parameters.AddWithValue("@Description_disc", txtdesc.Text)
    '            .Parameters.AddWithValue("@Discount", CDbl(txtdiscount.Text))
    '            .Parameters.AddWithValue("@ID", _id)   '  NEED THIS IF IN THE SAME FORM
    '            .ExecuteNonQuery()


    '        End With
    '        con.Close()
    '        MsgBox("Discount has been succesfully saved.", vbInformation)
    '        loaddiscount()
    '        cleardiscount()


    '    Catch ex As Exception
    '        con.Close()
    '        MsgBox(ex.Message, vbCritical)

    '    End Try
    'End Sub

    'Private Sub txtdesc_TextChanged(sender As Object, e As EventArgs)
    '    btnsave.Enabled = True

    'End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub






    Private Sub savebtn_Click_1(sender As Object, e As EventArgs) Handles savebtn.Click
        With frmdiscountmodif
            .btnsave.Visible = True
            .btnupdate.Visible = False
            .ShowDialog()
        End With
    End Sub

    Private Sub Guna2DataGridView2_Leave(sender As Object, e As EventArgs) Handles Guna2DataGridView2.Leave

        With frmdiscountmodif
            .txtdesc.Clear()
            .txtdiscount.Clear()
            .btnupdate.Visible = False
            .btnsave.Visible = True

        End With

    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Close()

    End Sub
End Class