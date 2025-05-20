Imports System.Data.SqlClient
Imports Tulpep.NotificationWindow
Imports System.Drawing.Printing
Imports MissionsGrocery.loginform
Imports MissionsGrocery.cashier

Public Class frmsettle


    Dim PrintPreview As New PrintPreviewDialog
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer

    Public discountba As Double

    ' Class-level variable to hold cart data
    Private cartData As New List(Of CartItem)

    Public Class CartItem
        Public Property ProductName As String
        Public Property Price As Decimal
        Public Property Quantity As Integer
        Public Property LineTotal As Decimal
    End Class
    Public Class SalesData
        Public Shared Subtotal As Double
        Public Shared VAT As Double

    End Class

    Sub settlepayment()
        Try
            Dim sdate As String = Now.ToString("yyyy-MM-dd")
            If CDbl(lbltotalitem.Text) > CDbl(txtcash.Text) Then
                MsgBox("Insufficient cash! Please input correct amount.", vbExclamation)
                Return
            End If

            If MsgBox("Are you sure you want to save this payment?", vbYesNo + vbQuestion) = vbYes Then
                ' Save payment record in database
                con.Open()
                cmd = New SqlClient.SqlCommand("INSERT INTO tblpayment (invoice, subtotal, vat, discount, amountdue, sdate, users) VALUES(@invoice, @subtotal, @vat, @discount, @amountdue, @sdate, @users)", con)
                With cashier
                    cmd.Parameters.AddWithValue("@invoice", .lblinvoice.Text)
                    cmd.Parameters.AddWithValue("@subtotal", CDbl(.lblsubtotal.Text))
                    cmd.Parameters.AddWithValue("@vat", CDbl(.lblvat.Text))
                    cmd.Parameters.AddWithValue("@discount", CDbl(.lbldics.Text))
                    cmd.Parameters.AddWithValue("@amountdue", CDbl(.lbldue.Text))
                    cmd.Parameters.AddWithValue("@sdate", sdate)
                    cmd.Parameters.AddWithValue("@users", lblsettle.Text)
                    cmd.ExecuteNonQuery()
                    con.Close()



                    ' Clear and populate cart data
                    cartData.Clear()
                    For Each row As DataGridViewRow In .Guna2DataGridView1.Rows
                        If Not row.IsNewRow Then
                            Dim item As New CartItem With {
                                .ProductName = row.Cells(5).Value.ToString(),
                                .Price = Convert.ToDecimal(row.Cells(7).Value),
                                .Quantity = Convert.ToInt32(row.Cells(6).Value),
                                .LineTotal = Convert.ToDecimal(row.Cells(7).Value) * Convert.ToInt32(row.Cells(6).Value)
                            }
                            cartData.Add(item)
                        End If
                    Next

                    ' Subtract stock quantities
                    MinusStockQty()
                    cashier.NotifyCriticalStock()

                    ' Inform payment success
                    MsgBox("Payment has been successfully saved.", vbInformation)
                    frminventorylist.LoadInventoryToFlowPanel()
                    ' Ask to print receipt
                    If MsgBox("Do you want to print the receipt?", vbYesNo + vbQuestion) = vbYes Then

                        'AddHandler PD.PrintPage, AddressOf PrintReceipt
                        Dim PrintPreview As New PrintPreviewDialog()
                        'changelongpaper()
                        PPD.Document = PD
                        PPD.ShowDialog()
                    End If

                    ' Reset cartl
                    .lblinvoice.Text = .getinvoiceNo
                    .loadcart()
                    .txtsearch.Focus()
                    .txtsearch.Clear()
                    .ClearAfterPaid()
                    Me.Dispose()
                    frmdashboard.loadchart()

                    .lbldics.Text = "0.00" ' Reset displayed discount to zero
                    .CurrentDiscountRate = 0 ' Reset discount rate variable
                    .computesalesdetail(CDbl(.lbltotal.Text)) ' Recompute totals without discount

                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub

    'Sub changelongpaper()
    '    Dim rowcount As Integer
    '    longpaper = 0
    '    rowcount = cashier.Guna2DataGridView1.Rows.Count
    '    longpaper = rowcount * 15
    '    longpaper = longpaper + 240
    'End Sub
    Private Function GetDiscountFromDB(invoice As String) As Decimal
        Dim discount As Decimal = 0
        Try
            con.Open()
            Dim cmd As New SqlCommand("SELECT discount FROM tblpayment WHERE invoice = @invoice", con)
            cmd.Parameters.AddWithValue("@invoice", invoice)
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            If reader.Read() Then
                Decimal.TryParse(reader("discount").ToString(), discount)
            End If
            reader.Close()
        Catch ex As Exception
            MsgBox("Error retrieving discount: " & ex.Message, vbCritical)
        Finally
            con.Close()
        End Try
        Return discount
    End Function

    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 800) ' use dynamic paper height
        PD.DefaultPageSettings = pagesetup
    End Sub

    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14b As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftMargin As Integer = 3 ' Adjusted for better alignment
        Dim centerMargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2

        Dim center As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim line As String = "****************************************************************"

        ' Store Header
        e.Graphics.DrawString("Mission Grocery Store", f14b, Brushes.Black, centerMargin - 25, 40, center) ' Shifted slightly left
        e.Graphics.DrawString("Block 7 Phase 2", f10, Brushes.Black, centerMargin - 25, 60, center)
        e.Graphics.DrawString("Pinagsama, Taguig City", f10, Brushes.Black, centerMargin - 25, 75, center)
        e.Graphics.DrawString(DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), f10, Brushes.Black, centerMargin - 15, 92, center)

        ' Invoice Details
        e.Graphics.DrawString("Transaction #: " & cashier.lblinvoice.Text, f8, Brushes.Black, leftMargin, 118)
        e.Graphics.DrawString("Cashier: " & GlobalUser.Username, f8, Brushes.Black, leftMargin, 130)

        '' Detail Header
        'e.Graphics.DrawString("Item", f8, Brushes.Black, leftMargin, 145)
        ''e.Graphics.DrawString("Qty", f8, Brushes.Black, leftMargin + 150, 145)
        'e.Graphics.DrawString("Price", f8, Brushes.Black, leftMargin + 150, 145)
        'e.Graphics.DrawString("Amount", f8, Brushes.Black, leftMargin + 170, 145)

        e.Graphics.DrawString(line, f8, Brushes.Black, leftMargin, 160)

        ' Itemized Billing
        Dim y As Integer = 180
        Dim y1 As Integer = 165
        Dim maxWidth As Integer = 80 ' Adjusted max width for item name and quantity
        Dim totalSubtotal As Decimal = 0

        For Each item As CartItem In cartData
            ' Check if there's enough space for the next line, if not, start a new page

            If y + 12 > e.MarginBounds.Bottom Then
                e.HasMorePages = True
                Return ' Exit the method and start a new page
            End If

            ' Concatenate product name and quantity
            Dim productNameQty As String = item.ProductName & " (" & item.Quantity.ToString() & ")"
            Dim price As String = item.Price.ToString("C2")
            Dim lineTotal As String = item.LineTotal.ToString("C2")
            Dim pricename As String = "Price"
            Dim totalprice As String = "Total"


            ' Measure the combined product name and quantity for wrapping
            Dim nameQtySize As SizeF = e.Graphics.MeasureString(productNameQty, f8)

            ' Wrap the product name and quantity if it exceeds max width
            If nameQtySize.Width > maxWidth Then
                Dim words As String() = productNameQty.Split(" "c)
                Dim currentLine As String = ""
                For Each word As String In words
                    If e.Graphics.MeasureString(currentLine & " " & word, f8).Width < maxWidth Then
                        currentLine &= " " & word
                    Else
                        e.Graphics.DrawString(currentLine.Trim(), f8, Brushes.Black, leftMargin, y)
                        y += 12
                        currentLine = word
                    End If
                Next
                If Not String.IsNullOrEmpty(currentLine) Then
                    e.Graphics.DrawString(currentLine.Trim(), f8, Brushes.Black, leftMargin, y)
                    y += 12
                End If
            Else
                e.Graphics.DrawString(productNameQty, f8, Brushes.Black, leftMargin, y)
                y += 12
            End If

            ' Wrap price and line total if they exceed the space
            Dim priceSize As SizeF = e.Graphics.MeasureString(price, f8)
            Dim lineTotalSize As SizeF = e.Graphics.MeasureString(lineTotal, f8)


            e.Graphics.DrawString(pricename, f8, Brushes.Black, leftMargin + 97, y1)
            e.Graphics.DrawString(totalprice, f8, Brushes.Black, leftMargin + 140, y1)


            ' Print price
            If priceSize.Width > 90 Then ' Price exceeds the space, wrap it
                e.Graphics.DrawString(price, f8, Brushes.Black, leftMargin + 97, y)
                y += 12
            Else
                e.Graphics.DrawString(price, f8, Brushes.Black, leftMargin + 97, y)
            End If

            ' Print line total
            If lineTotalSize.Width > 90 Then ' Line total exceeds the space, wrap it
                e.Graphics.DrawString(lineTotal, f8, Brushes.Black, leftMargin + 140, y)
                y += 12
            Else
                e.Graphics.DrawString(lineTotal, f8, Brushes.Black, leftMargin + 140, y)
            End If

            y += 12
            totalSubtotal += item.LineTotal
        Next


        ' VAT Calculation
        Dim vatRate As Decimal = 0.12D
        Dim vatableAmount As Decimal = totalSubtotal / (1 + vatRate)
        Dim vatAmount As Decimal = totalSubtotal - vatableAmount



        ' Assign VAT value to SalesData.VAT
        SalesData.VAT = vatAmount


        ' Totals Section
        y += 10
        e.Graphics.DrawString(line, f8, Brushes.Black, leftMargin, y)
        y += 15

        Dim line2 As String = "****************************************************************"

        e.Graphics.DrawString("Total:", f10b, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString((totalSubtotal - discountba).ToString("C2"), f14b, Brushes.Black, leftMargin + 100, y)
        y += 20




        ' Payment and Change
        Dim paymentReceived As Decimal = Decimal.Parse(txtcash.Text)

        Dim amountDue As Decimal = totalSubtotal - discountba
        Dim change As Decimal = paymentReceived - amountDue


        e.Graphics.DrawString("Payment Received:", f8, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString(paymentReceived.ToString("C2"), f8, Brushes.Black, leftMargin + 100, y)
        y += 20

        ' Add a line below "Payment Received"
        e.Graphics.DrawString(line2, f8, Brushes.Black, leftMargin, y)
        y += 15

        e.Graphics.DrawString("Change:", f10b, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString(change.ToString("C2"), f8, Brushes.Black, leftMargin + 100, y)
        y += 30

        ' VAT Breakdown
        e.Graphics.DrawString("Vatable Sales:", f8, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString(vatableAmount.ToString("C2"), f8, Brushes.Black, leftMargin + 100, y)
        y += 20

        e.Graphics.DrawString("VAT (12%):", f8, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString(SalesData.VAT.ToString("C2"), f8, Brushes.Black, leftMargin + 100, y)
        y += 20

        e.Graphics.DrawString("Discount:", f8, Brushes.Black, leftMargin, y)
        e.Graphics.DrawString(discountba.ToString("C2"), f8, Brushes.Black, leftMargin + 100, y)
        y += 30

        ' Other Details Section
        e.Graphics.DrawString("Other Details:", f10b, Brushes.Black, leftMargin, y)
        y += 15

        e.Graphics.DrawString("Customer Name: _____________________", f8, Brushes.Black, leftMargin, y)
        y += 15

        e.Graphics.DrawString("Customer Address: ___________________", f8, Brushes.Black, leftMargin, y)
        y += 15

        e.Graphics.DrawString("TIN No.: ____________________________", f8, Brushes.Black, leftMargin, y)
        y += 15

        e.Graphics.DrawString("SC ID No.: __________________________", f8, Brushes.Black, leftMargin, y)
        y += 15


        e.Graphics.DrawString("Signature: __________________________", f8, Brushes.Black, leftMargin, y)


        ' Thank You Message
        e.Graphics.DrawString("~ Thanks for shopping ~", f10, Brushes.Black, centerMargin - 15, y + 90, center)
        e.Graphics.DrawString("~ Mission Grocery Store ~", f10, Brushes.Black, centerMargin - 15, y + 105, center)

        'e.Graphics.DrawString("~ Salinas Fam ~", f10, Brushes.Black, centerMargin - 15, y + 125, center)

    End Sub



    Private Function WrapText(ByVal text As String, ByVal font As Font, ByVal maxWidth As Integer) As String
        Dim words As String() = text.Split(" "c)
        Dim currentLine As String = ""
        Dim wrappedText As String = ""

        For Each word As String In words
            If Graphics.FromImage(New Bitmap(1, 1)).MeasureString(currentLine & " " & word, font).Width < maxWidth Then
                currentLine &= " " & word
            Else
                wrappedText &= currentLine.Trim() & vbCrLf
                currentLine = word
            End If
        Next

        If Not String.IsNullOrEmpty(currentLine) Then
            wrappedText &= currentLine.Trim()
        End If

        Return wrappedText
    End Function



    'Dim t_price As Long
    'Dim t_qty As Long
    '    Sub sumprice()
    '        Dim countprice As Long = 0
    '        For rowitem As Long = 0 To cashier.Guna2DataGridView1.RowCount - 1
    '            countprice = countprice + Val(cashier.Guna2DataGridView1.Rows(rowitem).Cells(7).Value * cashier.Guna2DataGridView1.Rows(rowitem).Cells(6).Value)
    '        Next

    '        t_price = countprice
    '        Dim countqty As Long = 0
    '        For rowitem As Long = 0 To cashier.Guna2DataGridView1.RowCount - 1
    '            countqty = countqty + cashier.Guna2DataGridView1.Rows(rowitem).Cells(9).Value
    '        Next
    '        t_qty = countqty
    '    End Sub


    'Private Sub PrintReceipt(sender As Object, e As PrintPageEventArgs)
    '    Try
    '        ' Logo
    '        Dim receiptWidth As Integer = 500 ' Adjust for thermal printer width
    '        Dim margin As Integer = 5 ' Left margin
    '        Dim y As Integer = 50 ' Initial Y position

    '        ' Fonts
    '        Dim largeFont As New Font("Arial", 10, FontStyle.Bold)
    '        Dim font As New Font("Arial", 8, FontStyle.Regular)
    '        Dim boldFont As New Font("Arial", 8, FontStyle.Bold)

    '        ' Logo
    '        Dim logoImage As Image = My.Resources.icons8_double_down_30
    '        e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 35)

    '        ' Header
    '        Dim receiptTitle As String = "Doc Care's Pharmacy"
    '        Dim address As String = "76 Tanyag St., Manila, Metro Manila"
    '        Dim phone As String = "Tel: +63 912 345 6789"
    '        e.Graphics.DrawString(receiptTitle, largeFont, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString(address, font, Brushes.Black, 0, y)
    '        y += 12
    '        e.Graphics.DrawString(phone, font, Brushes.Black, 0, y)
    '        y += 20

    '        ' Transaction Details
    '        Dim invoice As String = "Invoice #: " & cashier.lblinvoice.Text
    '        Dim dateText As String = "Transaction Date: " & DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
    '        Dim cashierText As String = "Cashier: " & frmqty.lblname.Text

    '        e.Graphics.DrawString(invoice, font, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString(dateText, font, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString(cashierText, font, Brushes.Black, 0, y)
    '        y += 25

    '        ' Itemized Billing Header
    '        e.Graphics.DrawString("Item", boldFont, Brushes.Black, 0, y)
    '        e.Graphics.DrawString("Price", boldFont, Brushes.Black, 110, y)
    '        e.Graphics.DrawString("Qty", boldFont, Brushes.Black, 140, y)
    '        e.Graphics.DrawString("Total", boldFont, Brushes.Black, 200, y)
    '        y += 15
    '        e.Graphics.DrawLine(Pens.Black, 0, y, receiptWidth, y)
    '        y += 5

    '        ' Itemized Billing
    '        For Each item As CartItem In cartData
    '            ' Check if there's enough space for the next line, if not, start a new page
    '            If y + 12 > e.MarginBounds.Bottom Then
    '                e.HasMorePages = True
    '                Return ' Exit the method and start a new page
    '            End If

    '            ' Print the item details
    '            ' Wrap the product name if it's too long
    '            Dim productName As String = item.ProductName
    '            Dim productWidth As Integer = receiptWidth - 220 ' Space available for product name
    '            Dim stringFormat As New StringFormat() With {
    '                .LineAlignment = StringAlignment.Near,
    '                .Alignment = StringAlignment.Near
    '            }
    '            e.Graphics.DrawString(productName, font, Brushes.Black, New RectangleF(0, y, productWidth, 100), stringFormat)

    '            e.Graphics.DrawString(item.Price.ToString("C2"), font, Brushes.Black, 120, y)
    '            e.Graphics.DrawString(item.Quantity.ToString(), font, Brushes.Black, 170, y)
    '            e.Graphics.DrawString(item.LineTotal.ToString("C2"), font, Brushes.Black, 220, y)

    '            ' Move to the next line (adjust as needed)
    '            y += 12
    '        Next

    '        ' Adding space before the totals
    '        y += 10
    '        e.Graphics.DrawLine(Pens.Black, 0, y, receiptWidth, y)
    '        y += 10

    '        ' Totals and VAT
    '        Dim subtotal As Decimal = Convert.ToDecimal(cashier.lblsubtotal.Text)
    '        Dim vat As Decimal = Convert.ToDecimal(cashier.lblvat.Text)
    '        Dim discount As Decimal = Convert.ToDecimal(cashier.lbldics.Text)
    '        Dim total As Decimal = subtotal + vat - discount
    '        Dim cash As Decimal = Convert.ToDecimal(txtcash)
    '        Dim change As Decimal = cash - total

    '        e.Graphics.DrawString("Subtotal: " & subtotal.ToString("C2"), boldFont, Brushes.Black, 120, y)
    '        y += 15
    '        e.Graphics.DrawString("VAT (12%): " & vat.ToString("C2"), boldFont, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString("Discount: " & discount.ToString("C2"), boldFont, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawLine(Pens.Black, 0, y, 500, y)
    '        y += 10
    '        e.Graphics.DrawString("Total: " & total.ToString("C2"), boldFont, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString("Cash: " & cash.ToString("C2"), boldFont, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString("Change: " & change.ToString("C2"), boldFont, Brushes.Black, 0, y)
    '        y += 25

    '        ' Footer - Check if there is enough space for the footer
    '        If y + 30 > e.PageBounds.Height Then
    '            e.HasMorePages = True
    '            Return
    '        End If

    '        Dim footerMessage As String = "Thank you for shopping with us!"
    '        Dim footerNote As String = "Please retain this receipt for any inquiries."
    '        e.Graphics.DrawString(footerMessage, font, Brushes.Black, 0, y)
    '        y += 15
    '        e.Graphics.DrawString(footerNote, font, Brushes.Black, 0, y)

    '    Catch ex As Exception
    '        MessageBox.Show("An error occurred while printing the receipt: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub




    Private Sub frmsettle_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                With cashier
                    .txtsearch.Focus()
                    .txtsearch.SelectionStart = 0
                    .txtsearch.SelectionLength = .txtsearch.Text.Length
                End With
                Me.Dispose()
        End Select


    End Sub

    'Private Sub frmsettle_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
    '    Select Case Keys.KeyCode
    '        Case Keys.Escape          ' Escape key to esc
    '            Me.Dispose()
    '    End Select
    'End Sub


    Private Sub frmsettle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True    'When KeyPreview is set to True, the form's KeyPress, KeyDown, and KeyUp events will be raised before the same events occur for the active control on the form.
        txtcash.MaxLength = 6
    End Sub

    Private Sub txtcash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcash.KeyPress
        Select Case Asc(e.KeyChar)
            Case 13
                settlepayment() ' Enter key is pressed
            Case 48 To 57   ' 0 to 9
            Case 46  ' allows decimal (.)
            Case 8    ' backspace
            Case Else
                e.Handled = True

        End Select

    End Sub


    Sub MinusStockQty()

        Try
            With cashier
                For i = 0 To .Guna2DataGridView1.RowCount - 1
                    con.Open()
                    cmd = New SqlClient.SqlCommand("Update tblinventory set Quantity = Quantity - " & CInt(.Guna2DataGridView1.Rows(i).Cells(6).Value.ToString) & "where InventoryID like '" & .Guna2DataGridView1.Rows(i).Cells(2).Value.ToString & "'", con)
                    cmd.ExecuteNonQuery()
                    con.Close()
                Next

                con.Open()
                cmd = New SqlClient.SqlCommand("update tblcart set status  = 'Sold' where invoice like '" & Trim(.lblinvoice.Text) & "'", con)
                cmd.ExecuteNonQuery()
                con.Close()
                .lblinvoice.Text = .getinvoiceNo  '  TO CREATE NEW INVOICE WHEN SUCCESFULLY PAID
                .loadcart()
            End With



        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub


    Private Sub txtcash_TextChanged(sender As Object, e As EventArgs) Handles txtcash.TextChanged  ' CALCULATOR
        Try
            Dim change As Double = CDbl(txtcash.Text) - CDbl(lbltotalitem.Text)
            If change < 0 Then
                lblchange.Text = "0.00"     ' IF MAS MABABA ANG SUKLI SA 0
            Else
                lblchange.Text = Format(change, "#,##0.00")
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lblchange_Click(sender As Object, e As EventArgs) Handles lblchange.Click

    End Sub





    '' Button click event to trigger printing
    'Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnprint.Click
    '    Try
    '        ' Attach the PrintPage event
    '        AddHandler PD.PrintPage, AddressOf PrintReceipt

    '        ' Set up the print preview dialog
    '        Dim PrintPreview As New PrintPreviewDialog()
    '        PrintPreview.Document = PD

    '        ' Show the Print Preview dialog
    '        PrintPreview.ShowDialog()

    '    Catch ex As Exception
    '        MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        'AddHandler PD.PrintPage, AddressOf PrintReceipt
        Dim PrintPreview As New PrintPreviewDialog()
        PrintPreview.Document = PD
        PrintPreview.ShowDialog()
    End Sub

    Private Sub lblsettle_Click(sender As Object, e As EventArgs) Handles lblsettle.Click

    End Sub
End Class

