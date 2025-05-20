Public Class frmdiscountcashier

    Private Sub frmdiscountcashier_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub frmdiscountcashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

    End Sub

    Private Sub cboDescrip_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboDescrip.KeyPress
        e.Handled = True

    End Sub

    Sub GetDiscount()              ' ILALAGAY NIYA UNG VALUE NA DESCRIPTION SA COMBOBOX
        cboDescrip.Items.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("Select * from tbldiscount", con)
        dr = cmd.ExecuteReader
        While dr.Read
            cboDescrip.Items.Add(dr.Item(1).ToString)
        End While
        dr.Close()
        con.Close()
    End Sub

    Private Sub cboDescrip_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDescrip.SelectedIndexChanged
        con.Open()
        cmd = New SqlClient.SqlCommand("SELECT * FROM tbldiscount WHERE Description_disc = @desc", con)
        cmd.Parameters.AddWithValue("@desc", cboDescrip.Text)
        dr = cmd.ExecuteReader()

        If dr.Read() Then
            ' Convert 0.10 to 10 and add % sign for display
            Dim discountDecimal As Double = CDbl(dr.Item("discount"))
            Dim displayDiscount As String = (discountDecimal * 100).ToString("0") & "%"
            txtdiscount.Text = displayDiscount
        Else
            txtdiscount.Text = "0%"
        End If

        dr.Close()
        con.Close()
    End Sub


    Private Sub btnselect_Click(sender As Object, e As EventArgs) Handles btnselect.Click
        If Walanglaman(cboDescrip) = True Then Return

        With cashier
            ' ✅ Strip % and convert to decimal (e.g., 10% → 0.10)
            Dim discountText As String = txtdiscount.Text.Replace("%", "")
            .CurrentDiscountRate = CDbl(discountText) / 100

            ' Calculate discount
            Dim discount As Double = CDbl(.lbltotal.Text) * .CurrentDiscountRate
            .lbldics.Text = Format(discount, "#,##0.00")

            ' Refresh cart with new discount
            .loadcart()
            Me.Dispose()
        End With



    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Me.Close()

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        txtdiscount.Text = ""
        cboDescrip.SelectedIndex = -1 ' if you're using a combo box for description

        With cashier
            .lbldics.Text = "0.00" ' Reset displayed discount to zero
            .CurrentDiscountRate = 0 ' Reset discount rate variable
            .computesalesdetail(CDbl(.lbltotal.Text)) ' Recompute totals without discount
        End With

        Me.Close() ' Optional: Close the discount form

    End Sub



    ' THEN DITO IS KAPAG CINLICK NANATIN SI SELECT WHICH IS ITATIMES SA TOTAL NA GALING SA CASHIER SA DISCOUNT NA NAPILI SA TEXTBOX
    'THEN LALAGYAN NIYA NA NG FORMAT SI LABEL DISCOUNT

End Class

