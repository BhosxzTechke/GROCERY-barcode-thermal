Public Class frmvat

    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Try
            If Not IsNumeric(txtvat.Text) Then
                MsgBox("Please enter a valid VAT value.", vbExclamation)
                Exit Sub
            End If

            Dim vatPercent As Double = CDbl(txtvat.Text)

            If vatPercent < 0 Or vatPercent >= 100 Then
                MsgBox("VAT must be between 0 and 99.99.", vbExclamation)
                Exit Sub
            End If

            Dim vatDecimal As Double = vatPercent / 100 ' Convert 20 to 0.20

            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblVat", con)
            Dim icount As Integer = CInt(cmd.ExecuteScalar)
            con.Close()

            If icount > 0 Then
                con.Open()
                cmd = New SqlClient.SqlCommand("UPDATE tblVat SET vat = @vat", con)
                cmd.Parameters.AddWithValue("@vat", vatDecimal)
                cmd.ExecuteNonQuery()
                con.Close()
            Else
                con.Open()
                cmd = New SqlClient.SqlCommand("INSERT INTO tblVat (vat) VALUES (@vat)", con)
                cmd.Parameters.AddWithValue("@vat", vatDecimal)
                cmd.ExecuteNonQuery()
                con.Close()
            End If

            MsgBox("VAT has been successfully saved.", vbInformation)

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub







    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub

    Private Sub txtvat_TextChanged(sender As Object, e As EventArgs) Handles txtvat.TextChanged

    End Sub

    Private Sub frmvat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtvat.MaxLength = 2


    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()

    End Sub

    Private Sub txtvat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtvat.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' block input
        End If
    End Sub

    Private Sub txtvat_Leave(sender As Object, e As EventArgs) Handles txtvat.Leave
        If txtvat.Text <> "" Then
            Dim val As Integer = CInt(txtvat.Text)
            If val < 1 Or val > 99 Then
                MessageBox.Show("Please enter a Vat between 1 and 99.", "Invalid Vat", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtvat.Text = ""
                txtvat.Focus()
            End If
        End If
    End Sub

    Private Sub txtvat_KeyDown(sender As Object, e As KeyEventArgs) Handles txtvat.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub
End Class