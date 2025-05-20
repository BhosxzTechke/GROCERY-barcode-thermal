Public Class frmqty




    Private Sub frmqty_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown   ' FOR USING ESC IN KEYBOARD
        Select Case e.KeyCode
            Case Keys.Escape
                With cashier
                    .txtsearch.Focus()
                    .txtsearch.SelectionStart = 0
                    .txtsearch.SelectionLength = .txtsearch.Text.Length
                End With
                Me.Close()
            Case Keys.Enter
                AddToCart()
        End Select
    End Sub

    Private Sub frmqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub frmqty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        txtqty.Focus()
        txtqty.SelectionStart = 0
        txtqty.SelectionLength = txtqty.Text.Length


    End Sub


    Function GetStockavailable(ByVal sql As String) As Integer         ' Function to find out if have available stocks
        con.Open()
        cmd = New SqlClient.SqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            GetStockavailable = CInt(dr.Item("Quantity").ToString)
        Else
            GetStockavailable = 0
        End If
        dr.Close()
        con.Close()
        Return GetStockavailable
    End Function



    Sub AddToCart()
        Try
            ' Check if the quantity textbox is empty or zero
            If txtqty.Text = String.Empty Or txtqty.Text = "0" Then Return

            ' Get the current date in yyyy-MM-dd format
            Dim sdate As String = Now.ToString("yyyy-MM-dd")

            ' Open the database connection
            con.Open()

            ' Check if the product is in stock
            cmd = New SqlClient.SqlCommand("Select * from tblInventory where InventoryID like '" & lblPid.Text & "' and Quantity >= " & CInt(txtqty.Text) & "", con)
            dr = cmd.ExecuteReader
            dr.Read()

            With cashier

                ' Check if the product exists and has enough quantity in stock
                If dr.HasRows Then
                    ' Close the reader and connection
                    dr.Close()
                    con.Close()
                Else
                    ' Close the reader and connection
                    dr.Close()
                    con.Close()

                    ' Display a message indicating insufficient stock
                    MsgBox("Unable to proceed. Remaining stock " & GetStockavailable("Select * from tblInventory where InventoryID like '" & lblPid.Text & "'") & ",", vbCritical)

                    ' Set focus to the quantity textbox and select its content
                    txtqty.Focus()
                    txtqty.SelectionStart = 0
                    txtqty.SelectionLength = txtqty.Text.Length

                    ' Return from the method
                    Return
                End If

                ' Open the connection again
                con.Open()


                ' Check if the product is already in the cart
                cmd = New SqlClient.SqlCommand("SELECT qty FROM tblcart WHERE invoice = @invoice AND pid = @pid", con)
                cmd.Parameters.AddWithValue("@invoice", .lblinvoice.Text)
                cmd.Parameters.AddWithValue("@pid", lblPid.Text)
                dr = cmd.ExecuteReader()

                Dim inCart As Boolean = dr.Read()
                Dim existingQty As Integer = 0
                If inCart Then
                    existingQty = CInt(dr("qty"))
                End If
                dr.Close()

                If inCart Then
                    ' Compute new quantity and total
                    Dim newQty As Integer = existingQty + CInt(txtqty.Text)
                    Dim priceToUse As Double = If(newQty >= 10, CDbl(lblwhole.Text), CDbl(lblprice.Text))

                    cmd = New SqlClient.SqlCommand("UPDATE tblcart SET qty = @qty, total = @total WHERE invoice = @invoice AND pid = @pid", con)
                    cmd.Parameters.AddWithValue("@qty", newQty)
                    cmd.Parameters.AddWithValue("@total", priceToUse * newQty)
                    cmd.Parameters.AddWithValue("@invoice", .lblinvoice.Text)
                    cmd.Parameters.AddWithValue("@pid", lblPid.Text)
                    cmd.ExecuteNonQuery()
                    cashier.txtsearch.Clear()

                Else
                    ' Compute total based on qty
                    Dim qty As Integer = CInt(txtqty.Text)
                    Dim unitPrice As Double = If(qty >= 10, CDbl(lblwhole.Text), CDbl(lblprice.Text))

                    cmd = New SqlClient.SqlCommand("INSERT INTO tblcart (invoice, pid, price, wholeprice, qty, total, sdate, users) VALUES(@invoice, @pid, @price, @wholeprice, @qty, @total, @sdate, @users)", con)
                    cmd.Parameters.AddWithValue("@invoice", .lblinvoice.Text)
                    cmd.Parameters.AddWithValue("@pid", lblPid.Text)
                    cmd.Parameters.AddWithValue("@price", CDbl(lblprice.Text))
                    cmd.Parameters.AddWithValue("@wholeprice", CDbl(lblwhole.Text))
                    cmd.Parameters.AddWithValue("@qty", qty)
                    cmd.Parameters.AddWithValue("@total", unitPrice * qty)
                    cmd.Parameters.AddWithValue("@sdate", sdate)
                    cmd.Parameters.AddWithValue("@users", lblname.Text)
                    cmd.ExecuteNonQuery()
                    cashier.txtsearch.Clear()

                End If

                ' Close the connection
                con.Close()

                ' Set focus to the search textbox and select its content
                cashier.txtsearch.Focus()
                cashier.txtsearch.SelectionStart = 0
                cashier.txtsearch.SelectionLength = cashier.txtsearch.Text.Length

                ' Load the updated cart items
                cashier.loadcart()

                ' Dispose of the current form
                Me.Close()
            End With


        Catch ex As Exception
            ' Close the connection and display an error message
            con.Close()
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub






    Private Sub txtqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtqty.KeyPress  '  KEY PRESS MEANS EVENTS THAT CHANGING THE TEXT CASE
        Select Case Asc(e.KeyChar)
            Case 13         '         ASCII, the decimal value 13 corresponds to the "Carriage Return" (CR) character.
                AddToCart()
            Case 48 To 57 '          0 - 9
            Case 8        '            BACKSPACE OR DEL 
            Case Else
                e.Handled = True   '  Indicate to no processing needed

        End Select
    End Sub

    Private Sub txtqty_TextChanged(sender As Object, e As EventArgs) Handles txtqty.TextChanged
        UpdateTotal()

    End Sub

    Private Sub UpdateTotal()
        Try
            Dim price As Decimal = Decimal.Parse(lblprice.Text)
            Dim wholeprice As Decimal = Decimal.Parse(lblwhole.Text)
            Dim qty As Integer = Integer.Parse(txtqty.Text)
            Dim total As Decimal

            If qty >= 10 Then
                total = wholeprice * qty
            Else
                total = price * qty
            End If

            txttotal.Text = total.ToString("F2") ' Format as fixed decimal
        Catch ex As Exception
            txttotal.Text = "0.00" ' Fallback in case of invalid input
        End Try
    End Sub




    Private Sub lblprice_TextChanged(sender As Object, e As EventArgs) Handles lblprice.TextChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click, Label11.Click

    End Sub

    Private Sub frmqty_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave

        txtqty.Text = "0"

    End Sub

    Private Sub frmqty_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        txtqty.Text = "0"
    End Sub
End Class