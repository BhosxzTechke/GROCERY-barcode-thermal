Imports System.Data.SqlClient
Imports Tulpep.NotificationWindow
Imports System.Drawing.Printing
Imports MissionsGrocery.loginform

Public Class cashier



    Public CurrentDiscountRate As Decimal = 0D


    Public Class GlobalDiscounts
        Public Shared DiscountedAmount As Decimal
    End Class
    Private Sub Button8_Click(sender As Object, e As EventArgs)
        loginform.Show()
        Me.Close()

    End Sub

    Private Sub cashier_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                btnnewing_Click(sender, e)
            Case Keys.F2
                btbprodinqu_Click(sender, e)
            Case Keys.F3
                btndiscountan_Click(sender, e)
            Case Keys.F4
                btnset_Click(sender, e)
            Case Keys.F5
                btndaily_Click(sender, e)
            Case Keys.F6
                Button1_Click(sender, e)
            Case Keys.F10
                btnlogouts_Click(sender, e)
        End Select
    End Sub

    Private Sub cashier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        'Me.WindowState = FormWindowState.Maximized
        Panel1.Dock = DockStyle.Fill
        Timer2.Start()
        NotifyCriticalStock()


    End Sub


    Sub loadcart()
        Guna2DataGridView1.Rows.Clear()
        Dim _total As Double = 0, i As Integer = 0
        Try

            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = New SqlCommand("SELECT ct.*, iv.*, p.*, c.*, un.*, CAST(ct.total / NULLIF(ct.qty, 0) AS decimal(18,2)) AS unitprice FROM tblcart AS ct INNER JOIN tblInventory AS iv ON ct.pid = iv.InventoryID INNER JOIN tblproduct AS p ON iv.id = p.id INNER JOIN tblcategory AS c ON p.cid = c.catID INNER JOIN tblunit AS un ON p.uid = un.unitID WHERE ct.invoice = @invoice", con)
            cmd.Parameters.AddWithValue("@invoice", lblinvoice.Text)
            dr = cmd.ExecuteReader()

            While dr.Read
                i += 1

                ' Add row with FullName and other fields, including img
                Guna2DataGridView1.Rows.Add(dr("id").ToString(), i, dr("InventoryID").ToString(), dr("id").ToString(), dr("invoice").ToString(), dr("item_des").ToString(), dr("qty").ToString(), dr("unitprice").ToString(), dr("total").ToString())
                _total += CDbl(dr.Item("total").ToString)


            End While

            con.Close()
            dr.Close()


            computesalesdetail(CDbl(_total))
            If Guna2DataGridView1.RowCount > 0 Then btnset.Enabled = True Else btnset.Enabled = False
            If Guna2DataGridView1.RowCount > 0 Then btndiscountan.Enabled = True Else btndiscountan.Enabled = False

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message & vbCrLf & "StackTrace: " & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            con.Close()
        End Try
    End Sub



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
    Public Sub computesalesdetail(ByVal _total As Double)
        lbltotal.Text = Format(_total, "#,##0.00")

        Dim discount As Decimal

        If CurrentDiscountRate > 0 Then
            discount = _total * CurrentDiscountRate
        Else
            discount = 0
        End If

        ' Display
        lbldics.Text = Format(discount, "#,##0.00")

        Dim tempDiscount As Decimal
        If Decimal.TryParse(lbldics.Text, tempDiscount) Then
            discount = tempDiscount
        Else
            MessageBox.Show("Failed to assign discount from label.")
        End If





        Dim totalAfterDiscount As Double = _total - discount

        ' --- VATABLE COMPUTATION ---
        Dim vatRate As Double = getvat()
        Dim subtotal As Double = totalAfterDiscount / (1 + vatRate)
        lbldue.Text = Format(subtotal, "#,##0.00")
        frmsettle.SalesData.Subtotal = subtotal

        ' --- VAT AMOUNT ---
        Dim vatAmount As Double = totalAfterDiscount - subtotal
        lblvat.Text = Format(vatAmount, "#,##0.00")
        frmsettle.SalesData.VAT = vatAmount

        ' --- SUBTOTAL & DISPLAY ---
        lblsubtotal.Text = Format(totalAfterDiscount, "#,##0.00")
        lbldisplaytot.Text = lblsubtotal.Text
    End Sub






    Function getinvoiceNo() As String          ' A FUNCTION TO RETURN A STRING
        Dim invoiceNo As String = String.Empty ' Use a different name for the local variable

        Try
            Dim sdate As String = Now.ToString("yyyyMMdd")     ' Gets the current date in "yyyyMMdd" format
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            ' Open the connection and execute the query
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT TOP 1 invoice FROM tblcart WHERE invoice LIKE '%" & sdate & "%' ORDER BY id DESC", con) ' SQL to find the latest invoice
            dr = cmd.ExecuteReader
            dr.Read()

            ' Check if a row exists
            If dr.HasRows Then
                invoiceNo = dr.Item("invoice").ToString()
            Else
                invoiceNo = String.Empty
            End If
            dr.Close()
            con.Close()

            ' Generate a new invoice number if no rows exist
            If invoiceNo = String.Empty Then
                invoiceNo = sdate & "0001"
            Else
                invoiceNo = Trim((CLng(invoiceNo) + 1).ToString())
            End If

        Catch ex As Exception
            MsgBox("Error on getting invoice: " & ex.Message, vbCritical)
        Finally
            ' Close the connection in case it's still open
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try

        ' Return the invoice number
        Return invoiceNo
    End Function



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick  '       LIVE DATE AND TIME
        lbldate.Text = Now.ToLongDateString
        lbltime.Text = Now.ToString("hh:mm:ss tt")
    End Sub

    Sub searchproduct(ByVal search As String)
        Try
            ' Exit if the search input is empty
            If String.IsNullOrWhiteSpace(search) Then
                'MsgBox("Search input is empty.")
                Return
            End If

            ' Debugging - Confirm search input
            MsgBox("Searching for: " & search)

            ' Use the connection in a Using block
            Using con As New SqlConnection(Dbsql.connString)
                con.Open()

                ' SQL query with parameterized input
                Dim query As String = "select * from tblInventory as iv " &
                                      "inner join tblproduct as p on iv.id = p.id " &
                                      "WHERE barcode = @search"

                Using cmd As New SqlClient.SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@search", search)

                    Using dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
                        ' Check if any rows are returned
                        If dr.Read() Then
                            MsgBox("Product found!")

                            ' Pass the retrieved values to frmqty
                            With frmqty
                                .txtqty.Focus()
                                .txtdescription.Text = dr("item_des").ToString()
                                .lblprice.Text = dr("price").ToString()
                                .lblPid.Text = dr("InventoryID").ToString()
                                .ShowDialog()
                            End With
                        Else
                            MsgBox("No product found for the given barcode.", vbExclamation)
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical)
        End Try
    End Sub


    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        Dim colname As String = Guna2DataGridView1.Columns(e.ColumnIndex).Name

        If colname = "Delete" Then
            If MsgBox("Remove this Item? Please confirm", vbYesNo + vbQuestion) = vbYes Then


                con.Open()
                cmd = New SqlClient.SqlCommand("Delete from tblcart where id=" & Guna2DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString(), con)
                cmd.ExecuteNonQuery()
                con.Close()
                ClearAfterPaid()
                loadcart()
            End If
        End If
    End Sub


    Private Sub btnproduct_Click(sender As Object, e As EventArgs)
        'With frmproductinquiry
        '    .ProductInventoryCart()
        '    .ShowDialog()
        'End With
    End Sub

    Private Sub btndiscount_Click(sender As Object, e As EventArgs)  ' CLICK TO SHOW DISCOUNT FORM
        'With frmdiscountcashier
        '    .GetDiscount()
        '    .ShowDialog()

        'End With
    End Sub




    Private Sub btndailysales_Click(sender As Object, e As EventArgs)
        With frmdailysales
            .loadsales()
            .ShowDialog()

        End With
    End Sub


    Sub ClearAfterPaid()
        lbldisplaytot.Text = "0.00"
        lbldics.Text = "0.00"
        lblvat.Text = "0.00"
        lblsubtotal.Text = "0.00"
        lbldue.Text = "0.00"

    End Sub



    'Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    '    tiktok.Text = Date.Now.ToString("hh:mm:ss")
    '    ampm.Text = Date.Now.ToString("tt")

    '    Guna2CircleProgressBar1.Value = Date.Now.ToString("ss")

    '    day.Text = Date.Now.ToString("dddd")
    '    calendar.Text = Date.Now.Date
    'End Sub


    'shortcut key SA BABA


    ' NEW TRANSACTION
    Private Sub btnnewing_Click(sender As Object, e As EventArgs) Handles btnnewing.Click

        If Guna2DataGridView1.RowCount > 0 Then Return ' IF may laman na sa datagrid di na mageexecute yung whole code if wala pang laman pedeng mag new uli
        lblinvoice.Text = getinvoiceNo() ' THIS LINE IS RETURNING A STRING TO UPDATE THE lblinvoice.text 
        btbprodinqu.Enabled = True
        txtsearch.Enabled = True
        txtsearch.Focus()
    End Sub

    ' PRODUCT INQUIRY
    Private Sub btbprodinqu_Click(sender As Object, e As EventArgs) Handles btbprodinqu.Click
        With frmproductinquiry
            frmqty.txtqty.Focus()
            .loadinventory()
            .ShowDialog()
        End With
    End Sub

    ' ADD DISCOUNT
    Private Sub btndiscountan_Click(sender As Object, e As EventArgs) Handles btndiscountan.Click
        With frmdiscountcashier
            .GetDiscount()
            .ShowDialog()

        End With
    End Sub

    ' SETTLE
    Private Sub btnset_Click(sender As Object, e As EventArgs) Handles btnset.Click
        With frmsettle
            .lbltotalitem.Text = lblsubtotal.Text     ' Para ilagay niya sa form yung total due 
            .discountba = lbldics.Text
            .ShowDialog()
        End With

    End Sub

    ' DAILY SALES
    Private Sub btndaily_Click(sender As Object, e As EventArgs) Handles btndaily.Click
        With frmdailysales
            .loadsales()
            .ShowDialog()

        End With


    End Sub

    Private Sub btnpass_Click(sender As Object, e As EventArgs)
        With frmchangepass
            .ShowDialog()
        End With
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub




    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        ' Check if the Enter key is pressed

        'If e.KeyCode = Keys.Enter Then
        '    ' Call the search function with the full barcode
        '    searchproduct(txtsearch.Text)

        '    ' Prevent the beep sound when pressing Enter
        '    e.SuppressKeyPress = True
        'End If
    End Sub




    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        'searchproduct(txtsearch.Text)
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btnreturn_Click(sender As Object, e As EventArgs)
        If Not frmstaffboard.Visible Then
            frmstaffboard.Show() ' Show the frmstaffboard form
        End If
        Me.Hide() ' Hide the current form
    End Sub

    Private Sub btnchangepass_Click(sender As Object, e As EventArgs)
        With frmchangepass
            .ShowDialog()
        End With
    End Sub


    Private Sub btnreturningg_Click(sender As Object, e As EventArgs)
        frmstaffboard.ShowDialog()
        Me.Hide()
    End Sub

    Private Sub lblvat_Click(sender As Object, e As EventArgs) Handles lblvat.Click

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub btnlogouts_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub btnlogouts_Click_1(sender As Object, e As EventArgs) Handles btnlogouts.Click

        Me.Dispose()
        txtsearch.Enabled = False


    End Sub

    Private Sub lblinvoice_Click(sender As Object, e As EventArgs) Handles lblinvoice.Click, lblcashier.Click, Label3.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub btnreturns_Click(sender As Object, e As EventArgs)
        If Not frmstaffboard.Visible Then
            frmstaffboard.Show() ' Show the frmstaffboard form
        End If
        Me.Hide() ' Hide the current form
    End Sub

    Sub NotifyCriticalStock()
        Dim underStockCount As Integer = 0
        Dim outOfStockCount As Integer = 0

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Query to get details of items at or below the Critical Level
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity, sts.StockLevel, sts.CriticalLevel 
                                       FROM tblInventory AS iv 
                                       INNER JOIN tblproduct AS p ON iv.id = p.id 
                                       INNER JOIN tblStockSettings AS sts ON iv.InventoryID = sts.ID
                                       WHERE iv.Quantity <= sts.CriticalLevel AND iv.Quantity > 0", con)

            Dim underStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for items at or below Critical Level
            While underStockDr.Read()
                Dim productName As String = underStockDr("item_des").ToString()
                Dim quantity As Integer = CInt(underStockDr("Quantity"))
                Dim criticalLevel As Integer = CInt(underStockDr("CriticalLevel"))
                Dim stockLevel As Integer = CInt(underStockDr("StockLevel"))

                ' Alert for items at or below the critical level
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Critical Stock Alert"
                popupNotifier.ContentText = String.Format("Critical Stock Alert: {0} - only {1} left in stock (Critical Level: {2}, Stock Level: {3}).", productName, quantity, criticalLevel, stockLevel)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            underStockDr.Close()

            ' Query to get items where stock is below the Stock Level but above Critical Level
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity, sts.StockLevel 
                                       FROM tblInventory AS iv 
                                       INNER JOIN tblproduct AS p ON iv.id = p.id 
                                       INNER JOIN tblStockSettings AS sts ON iv.InventoryID = sts.ID
                                       WHERE iv.Quantity < sts.StockLevel AND iv.Quantity > sts.CriticalLevel", con)

            Dim lowStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for items below Stock Level but above Critical Level
            While lowStockDr.Read()
                Dim productName As String = lowStockDr("item_des").ToString()
                Dim quantity As Integer = CInt(lowStockDr("Quantity"))
                Dim stockLevel As Integer = CInt(lowStockDr("StockLevel"))

                ' Alert for items below stock level but above critical level
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Low Stock Alert"
                popupNotifier.ContentText = String.Format("Low Stock Alert: {0} - {1} left in stock (Stock Level: {2}).", productName, quantity, stockLevel)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            lowStockDr.Close()

            ' Query to get details of out-of-stock items (quantity = 0)
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity FROM tblInventory AS iv INNER JOIN tblproduct AS p ON iv.id = p.id WHERE iv.Quantity = 0", con)
            Dim outOfStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for out of stock items
            While outOfStockDr.Read()
                Dim productName As String = outOfStockDr("item_des").ToString()

                ' Alert for items that are out of stock
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Out of Stock Alert"
                popupNotifier.ContentText = String.Format("Out of Stock Alert: {0} is currently unavailable.", productName)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            outOfStockDr.Close()

            con.Close()

        Catch ex As Exception
            MsgBox("Error Notify Critical: " & ex.Message, vbCritical)
        Finally
            ' Close the connection in case it's still open
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub



    Private Sub txtsearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsearch.KeyPress
        If e.KeyChar = Chr(13) Then
            searchproduct(txtsearch.Text)
        End If
    End Sub

    Private Sub lbldics_Click(sender As Object, e As EventArgs) Handles lbldics.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmchangepass.ShowDialog()

    End Sub
End Class