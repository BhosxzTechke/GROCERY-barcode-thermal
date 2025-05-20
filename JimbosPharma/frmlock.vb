Imports MissionsGrocery.loginform
Imports Guna.UI2.WinForms

Public Class frmlock

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        If GlobalUser.Password <> txtpass.Text Then
            Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
            Guna2MessageDialog1.Icon = MessageDialogIcon.Error
            Guna2MessageDialog1.Text = "Invalid password. unable to unlock"
            Guna2MessageDialog1.Show()

            Return
        Else
            txtpass.Clear()

            Me.Close()
        End If
    End Sub

    Private Sub frmlock_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnsave.PerformClick()         ' Simulate a click on the save button

        End If
    End Sub


    Private Sub frmlock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged

    End Sub
End Class