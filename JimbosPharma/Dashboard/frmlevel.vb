Public Class frmlevel
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblStockSettings", con)
            Dim icount As Integer = CInt(cmd.ExecuteScalar)
            con.Close()

            Dim stockLevel As Integer = CInt(txtstock.Text)
            Dim criticalLevel As Integer = CInt(txtcritical.Text)

            If icount > 0 Then
                con.Open()
                cmd = New SqlClient.SqlCommand("UPDATE tblStockSettings SET StockLevel = @stock, CriticalLevel = @critical", con)
                cmd.Parameters.AddWithValue("@stock", stockLevel)
                cmd.Parameters.AddWithValue("@critical", criticalLevel)
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                con.Open()
                cmd = New SqlClient.SqlCommand("INSERT INTO tblStockSettings (StockLevel, CriticalLevel) VALUES (@stock, @critical)", con)
                cmd.Parameters.AddWithValue("@stock", stockLevel)
                cmd.Parameters.AddWithValue("@critical", criticalLevel)
                cmd.ExecuteNonQuery()
                con.Close()
            End If

            MsgBox("Stock settings have been successfully saved.", vbInformation)

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub txtstock_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtstock.KeyPress
        ' Allow only digits
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcritical_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcritical.KeyPress
        ' Allow only digits
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmlevel_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtstock.MaxLength = 3
        txtcritical.MaxLength = 2
    End Sub
End Class