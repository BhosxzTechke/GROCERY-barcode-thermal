Imports System.Data.SqlClient

Public Class frmviewuser

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        frmuser.ClearControls()
        Me.Close()

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Const WM_NCLBUTTONDOWN As Integer = &HA1
        Const HTCAPTION As Integer = 2

        If m.Msg = WM_NCLBUTTONDOWN AndAlso m.WParam.ToInt32() <> HTCAPTION Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub




    Private Sub frmviewuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txtstatus_Click(sender As Object, e As EventArgs) Handles txtstatus.Click

    End Sub
End Class