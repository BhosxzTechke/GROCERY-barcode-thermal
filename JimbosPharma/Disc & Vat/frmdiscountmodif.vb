Imports System.Data.SqlClient

Public Class frmdiscountmodif


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click

        Try
            If Walanglaman(txtdesc) = True Then Return
            If Walanglaman(txtdiscount) = True Then Return

            Dim discountPercent As Double


            ' Validate unique company name
            If Not IsUniqueDiscount(txtdesc.Text) Then
                MsgBox("Discount name already exists. Please choose another.", vbExclamation)
                txtdesc.BackColor = Color.LightCoral
                Return
            End If


            If IsNumeric(txtdiscount.Text) Then
                discountPercent = CDbl(txtdiscount.Text)

                If discountPercent < 0 Or discountPercent >= 100 Then
                    MsgBox("Discount must be between 0 and 99.99.", vbExclamation)
                    txtdiscount.Focus()
                    Exit Sub
                End If

                ' Convert 20 to 0.20
                discountPercent = discountPercent / 100
                txtdiscount.Text = Format(discountPercent, "0.00")

            Else
                MsgBox("Please enter a valid numeric discount.", vbExclamation)
                txtdiscount.Focus()
                Exit Sub
            End If


            con.Open()
            cmd = New SqlClient.SqlCommand("INSERT INTO  tbldiscount (Description_disc, Discount ) VALUES (@Description_disc, @Discount)", con)
            With cmd
                .Parameters.AddWithValue("@Description_disc", txtdesc.Text)
                .Parameters.AddWithValue("@Discount", txtdiscount.Text)
                .ExecuteNonQuery()


            End With
            con.Close()
            MsgBox("Discount has been succesfully saved.", vbInformation)
            With frmdiscount
                .loaddiscount()
                cleardiscount()
            End With
            Me.Dispose()



        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)

        End Try
    End Sub


    Private Function IsUniqueDiscount(description As String) As Boolean
        Dim unique As Boolean = True
        Try
            con.Open()
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM tbldiscount WHERE Description_disc = @Description_disc", con)
            checkCmd.Parameters.AddWithValue("@Description_disc", description)
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            unique = (count = 0)
        Catch ex As Exception
            MsgBox("Error checking unique Discount: " & ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
        Return unique
    End Function



    Sub cleardiscount()
        txtdesc.Clear()
        txtdiscount.Clear()

    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            If Walanglaman(txtdesc) = True Then Return
            If Walanglaman(txtdiscount) = True Then Return

            Dim discountPercent As Double

            If IsNumeric(txtdiscount.Text) Then
                discountPercent = CDbl(txtdiscount.Text)

                If discountPercent < 0 Or discountPercent >= 100 Then
                    MsgBox("Discount must be between 0 and 99.99.", vbExclamation)
                    txtdiscount.Focus()
                    Exit Sub
                End If

                ' Convert 20 to 0.20
                discountPercent = discountPercent / 100
                txtdiscount.Text = Format(discountPercent, "0.00")

            Else
                MsgBox("Please enter a valid numeric discount.", vbExclamation)
                txtdiscount.Focus()
                Exit Sub
            End If



            con.Open()
            cmd = New SqlClient.SqlCommand("UPDATE tbldiscount set Description_disc = @Description_disc, Discount=@Discount where ID like @ID", con)
            With cmd
                .Parameters.AddWithValue("@Description_disc", txtdesc.Text)
                .Parameters.AddWithValue("@Discount", CDbl(txtdiscount.Text))
                .Parameters.AddWithValue("@ID", lblid.Text)   '  NEED THIS IF IN THE SAME FORM
                .ExecuteNonQuery()


            End With
            con.Close()
            MsgBox("Discount has been succesfully saved.", vbInformation)
            With frmdiscount
                .loaddiscount()
                cleardiscount()
                Me.Close()

            End With

        Catch ex As Exception
            con.Close()
            MsgBox(ex.Message, vbCritical)

        End Try
    End Sub

    Private Sub txtdiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdiscount.KeyPress

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' block input
        End If
    End Sub

    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()

    End Sub

    Private Sub txtdiscount_TextChanged(sender As Object, e As EventArgs) Handles txtdiscount.TextChanged

    End Sub

    Private Sub txtdesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdesc.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

    End Sub

    Private Sub frmdiscountmodif_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtdiscount.MaxLength = 4
    End Sub

    Private Sub txtdesc_KeyDown(sender As Object, e As KeyEventArgs) Handles txtdesc.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtdiscount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtdiscount.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtdiscount_Leave(sender As Object, e As EventArgs) Handles txtdiscount.Leave

        If txtdiscount.Text <> "" Then
            Dim val As Integer = CInt(txtdiscount.Text)
            If val < 1 Or val > 99 Then
                MessageBox.Show("Please enter a discount between 1 and 99.", "Invalid Discount", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtdiscount.Text = ""
                txtdiscount.Focus()
            End If
        End If
    End Sub
End Class