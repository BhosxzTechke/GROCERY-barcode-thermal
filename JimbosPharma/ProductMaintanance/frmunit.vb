Imports System.Data.SqlClient
Public Class frmunit

    Private Sub frmunit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
        txtunit.Clear()

    End Sub

    Private Function ValidateDuplicateEntry(query As String, value As String) As Boolean
        Using cmd As New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@unit", value)
            con.Open()
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            con.Close()
            Return count > 0
        End Using
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        If String.IsNullOrWhiteSpace(txtunit.Text) Then
            MessageBox.Show("Unit cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If



        ' Optional regex-based validation (only letters, numbers, spaces)
        Dim pattern As String = "^[a-zA-Z0-9\s]+$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(txtunit.Text, pattern) Then
            MessageBox.Show("Invalid characters in Unit name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If


        ' Confirm Save Action using Guna2MessageDialog
        Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
        Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
        Guna2MessageDialog1.Caption = "Confirm Save"
        Guna2MessageDialog1.Text = "Are you sure you want to save this record?"

        Dim result As DialogResult = Guna2MessageDialog1.Show()

        If result = DialogResult.Yes Then
            Try
                Dim found As Boolean = False



                ' Check for duplicate entry
                Using cmd As New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblunit WHERE unit = @unit", con)
                    cmd.Parameters.AddWithValue("@unit", txtunit.Text)
                    con.Open()
                    Dim count As Integer = CInt(cmd.ExecuteScalar())
                    found = (count > 0)
                End Using

                If Not found Then
                    ' Insert new category
                    Using cmd As New SqlClient.SqlCommand("INSERT INTO tblunit (unit) VALUES (@unit)", con)
                        cmd.Parameters.AddWithValue("@unit", txtunit.Text)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Success Message
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Save Successful"
                    Guna2MessageDialog1.Text = "Unit has been successfully saved."
                    Guna2MessageDialog1.Show()

                    con.Close()
                    Me.Close()

                    ' Refresh category list
                    frmunitlist.unitview()
                    clear()
                Else
                    ' Duplicate Entry Message
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Duplicate Entry"
                    Guna2MessageDialog1.Text = "Error: This category already exists."
                    Guna2MessageDialog1.Show()
                End If
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
    End Sub

    Sub clear()
        txtunit.Clear()
        txtunit.Focus()
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try

            If String.IsNullOrWhiteSpace(txtunit.Text) Then
                MessageBox.Show("Unit cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            ' Optional regex-based validation (only letters, numbers, spaces)
            Dim pattern As String = "^[a-zA-Z0-9\s]+$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtunit.Text, pattern) Then
                MessageBox.Show("Invalid characters in Unit name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            ' Confirm Update Action using Guna2MessageDialog
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
            Guna2MessageDialog1.Caption = "Confirm Update"
            Guna2MessageDialog1.Text = "Are you sure you want to update this record?"

            Dim result As DialogResult = Guna2MessageDialog1.Show()

            If result = DialogResult.Yes Then
                ' Check for duplicate entry
                If ValidateDuplicateEntry("SELECT COUNT(*) FROM tblunit WHERE unit = @unit", txtunit.Text) Then
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Duplicate Entry"
                    Guna2MessageDialog1.Text = "Error: This category already exists."
                    Guna2MessageDialog1.Show()
                    Return
                End If

                ' Update the category
                Using cmd As New SqlClient.SqlCommand("UPDATE tblunit SET unit = @unit WHERE unitID = @unitID", con)
                    cmd.Parameters.AddWithValue("@unit", txtunit.Text)
                    cmd.Parameters.AddWithValue("@unitID", lblid.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using

                ' Success Message
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Update Successful"
                Guna2MessageDialog1.Text = "Unit has been successfully updated."
                Guna2MessageDialog1.Show()

                ' Refresh the category list
                frmunitlist.unitview()
                clear()
                Me.Close()
            End If

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
    End Sub



    Private Sub txtunit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtunit.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the 'ding' sound
            btnsave.PerformClick() ' Simulate a click on the save button
        End If

        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Close()
    End Sub

    Private Sub Guna2CustomGradientPanel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel2.Paint

    End Sub
End Class