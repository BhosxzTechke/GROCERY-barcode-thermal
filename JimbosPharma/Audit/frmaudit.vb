Imports System.Security.Cryptography
Imports Guna.UI2.WinForms

Public Class frmaudit
    Private Sub frmaudit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadaudit()
        loadauditaccount()
    End Sub
    Sub loadaudit()
        Dim selectedDate As Date = dtpicker.Value
        Dim i As Integer = 0

        dataprod.Rows.Clear()

        Try
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT * FROM auditlog WHERE CAST(ActionDate AS DATE) = @sdate", con)
            cmd.Parameters.AddWithValue("@sdate", selectedDate.Date)

            dr = cmd.ExecuteReader()
            While dr.Read()
                i += 1
                dataprod.Rows.Add(dr("AuditID").ToString, i, dr("UserID").ToString,
                              dr("Action").ToString, dr("TableName").ToString,
                              dr("RecordID").ToString, dr("OldValue").ToString,
                              dr("NewValue").ToString)
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading audit log: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Sub loadauditaccount()
        Dim selectedDate As Date = Guna2DateTimePicker2.Value
        Dim i As Integer = 0

        Guna2DataGridView1.Rows.Clear()

        Try
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT * FROM tblAuditLogin WHERE CAST(ActionDate AS DATE) = @sdate", con)
            cmd.Parameters.AddWithValue("@sdate", selectedDate.Date)

            dr = cmd.ExecuteReader()
            While dr.Read()
                i += 1
                Guna2DataGridView1.Rows.Add(dr("AuditLoginID").ToString, i, dr("UserID").ToString,
                              dr("Username").ToString, dr("Action").ToString,
                              dr("OldValue").ToString, dr("NewValue").ToString,
                              dr("ActionDate").ToString)
            End While
            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error loading audit log: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    Private Sub dtpicker_ValueChanged_1(sender As Object, e As EventArgs) Handles dtpicker.ValueChanged
        loadaudit()
    End Sub

    Private Sub Guna2DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles Guna2DateTimePicker2.ValueChanged
        loadauditaccount()
    End Sub
End Class