Imports System.Data.SqlClient
Imports MissionsGrocery.loginform

Public Class frmchangepass


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            ' Validate fields
            If Walanglaman(oldpassword) OrElse Walanglaman(newpass) OrElse Walanglaman(txtconfpass) Then Return

            ' Verify old password matches
            If txtold.Text <> GlobalUser.Password Then
                MsgBox("Old password is not matched!", vbExclamation)
                Return
            End If

            ' Verify new password matches confirmation
            If newpass.Text <> txtconfpass.Text Then
                MsgBox("Confirm new password did not match!", vbExclamation)
                Return
            End If

            ' Confirm password change
            If MsgBox("Are you sure you want to save changes?", vbYesNo + vbQuestion) = vbYes Then
                ' Save the new password
                SavePassword(newpass.Text)
                MsgBox("Password successfully changed!", vbInformation)

                ' Update global variable if necessary
                ' GlobalUser.Password = newpass.Text

                ' Close the form
                clear()

                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Unexpected error: " & ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub SavePassword(newPassword As String)
        Try
            ' Update query to save the new password
            Dim query As String = "UPDATE tbluser SET Password = @newPassword WHERE Username = @username"
            Using cmd As New SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@newPassword", newPassword)
                cmd.Parameters.AddWithValue("@username", GlobalUser.Username) ' Ensure GlobalUser.Username is the logged-in username

                con.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As SqlException
            MsgBox("Database error: " & ex.Message, vbCritical)
        Catch ex As Exception
            MsgBox("Unexpected error: " & ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    ' Clear form fields
    Private Sub clear()
        txtold.Clear()
        newpass.Clear()
        txtconfpass.Clear()
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs)
        clear()

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub newpass_TextChanged(sender As Object, e As EventArgs) Handles newpass.TextChanged

    End Sub

    Private Sub txtold_TextChanged(sender As Object, e As EventArgs) Handles txtold.TextChanged

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        txtold.Clear()
        txtconfpass.Clear()
        newpass.Clear()


        Me.Close()

    End Sub
End Class