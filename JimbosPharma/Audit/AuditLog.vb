Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel

Module AuditLog

    Public Sub logoaudit(UserID As String, action As String, tablename As String, recordID As String, oldValue As String, newValue As String)

        Dim query As String = "INSERT INTO auditlog(UserID, Action, TableName, RecordID, OldValue, NewValue) VALUES (@UserID, @Action, @TableName, @RecordID, @OldValue, @NewValue)"

        Using conn As New SqlConnection("Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False;")
            Using cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@UserID", UserID)
                cmd.Parameters.AddWithValue("@Action", action)
                cmd.Parameters.AddWithValue("@TableName", tablename)
                cmd.Parameters.AddWithValue("@RecordID", recordID)
                cmd.Parameters.AddWithValue("@OldValue", oldValue)
                cmd.Parameters.AddWithValue("@NewValue", newValue)

                conn.Open()
                cmd.ExecuteNonQuery()
            End Using

        End Using

    End Sub

    Public Sub logaccount(UserID As String, username As String, Action As String, oldValue As String, newValue As String)
        Try
            con.Open()
            Dim query As String = "INSERT INTO tblAuditLogin(UserID, Username, Action, OldValue, NewValue) VALUES (@UserID, @Username, @Action,  @OldValue, @NewValue)"

            Using conn As New SqlConnection("Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False;")
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@UserID", UserID)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Action", Action)
                    cmd.Parameters.AddWithValue("@OldValue", If(Action = "LOGIN", "Logged Out", "Logged In"))
                    cmd.Parameters.AddWithValue("@NewValue", If(Action = "LOGIN", "Logged In", "Logged Out"))

                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using

            End Using
        Catch ex As Exception
            MessageBox.Show("Audit Login Failed: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub




End Module
