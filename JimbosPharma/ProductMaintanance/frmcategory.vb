
Public Class frmcategory

    Private Sub btncancel_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Sub clear()
        txtcategory.Clear()


    End Sub
    Private Function ValidateDuplicateEntry(query As String, value As String) As Boolean
        Using cmd As New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Category", value)
            con.Open()
            Dim count As Integer = CInt(cmd.ExecuteScalar())
            con.Close()
            Return count > 0
        End Using
    End Function

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click


        If String.IsNullOrWhiteSpace(txtcategory.Text) Then
            MessageBox.Show("Category cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Optional regex-based validation (only letters, numbers, spaces)
        Dim pattern As String = "^[a-zA-Z0-9\s]+$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(txtcategory.Text, pattern) Then
            MessageBox.Show("Invalid characters in category name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                Using cmd As New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblcategory WHERE Category = @Category", con)
                    cmd.Parameters.AddWithValue("@Category", txtcategory.Text)
                    con.Open()
                    Dim count As Integer = CInt(cmd.ExecuteScalar())
                    found = (count > 0)
                End Using

                If Not found Then
                    ' Insert new category
                    Using cmd As New SqlClient.SqlCommand("INSERT INTO tblcategory (Category) VALUES (@Category)", con)
                        cmd.Parameters.AddWithValue("@Category", txtcategory.Text)
                        cmd.ExecuteNonQuery()
                    End Using

                    ' Success Message
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Save Successful"
                    Guna2MessageDialog1.Text = "Category has been successfully saved."
                    Guna2MessageDialog1.Show()

                    con.Close()
                    Me.Close()

                    ' Refresh category list
                    frmcategorylist.catview()
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


    Private Sub frmcategory_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the 'ding' sound
            btnsave.PerformClick() ' Simulate a click on the save button
        End If
    End Sub

    Private Sub frmcategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtcategory_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the 'ding' sound
            btnsave.PerformClick() ' Simulate a click on the save button
        End If
    End Sub

    Private Sub txtcategory_TextChanged(sender As Object, e As EventArgs)
        txtcategory.Focus()
    End Sub

    Private Sub txtcategory_MouseClick(sender As Object, e As MouseEventArgs)
        If txtcategory.Text <> "" Then
            txtcategory.Text = ""
        End If
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            ' Confirm Update Action using Guna2MessageDialog
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
            Guna2MessageDialog1.Caption = "Confirm Update"
            Guna2MessageDialog1.Text = "Are you sure you want to update this record?"

            Dim result As DialogResult = Guna2MessageDialog1.Show()

            If result = DialogResult.Yes Then
                ' Check for duplicate entry
                If ValidateDuplicateEntry("SELECT COUNT(*) FROM tblcategory WHERE Category = @Category", txtcategory.Text) Then
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Duplicate Entry"
                    Guna2MessageDialog1.Text = "Error: This category already exists."
                    Guna2MessageDialog1.Show()
                    Return
                End If

                ' Update the category
                Using cmd As New SqlClient.SqlCommand("UPDATE tblcategory SET Category = @Category WHERE catID = @catID", con)
                    cmd.Parameters.AddWithValue("@Category", txtcategory.Text)
                    cmd.Parameters.AddWithValue("@catID", lblID.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                End Using

                ' Success Message
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Update Successful"
                Guna2MessageDialog1.Text = "Category has been successfully updated."
                Guna2MessageDialog1.Show()

                ' Refresh the category list
                frmcategorylist.catview()
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


    Private Sub lblID_Click(sender As Object, e As EventArgs) Handles lblID.Click

    End Sub

    Private Sub btnsave_KeyDown(sender As Object, e As KeyEventArgs) Handles btnsave.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the 'ding' sound
            btnsave.PerformClick() ' Simulate a click on the save button

        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
        txtcategory.Clear()


    End Sub

    Private Sub txtcategory_KeyDown_1(sender As Object, e As KeyEventArgs) Handles txtcategory.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Prevent the 'ding' sound
            btnsave.PerformClick() ' Simulate a click on the save button
        End If

        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Close()

    End Sub

    Private Sub Guna2CustomGradientPanel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2CustomGradientPanel2.Paint

    End Sub
End Class