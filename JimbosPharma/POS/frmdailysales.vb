Imports System.Data.SqlClient
Imports System.Drawing.Printing

Public Class frmdailysales
    Dim WithEvents PD As New PrintDocument
    Dim PPD As New PrintPreviewDialog
    Dim longpaper As Integer

    Private Sub Button10_Click(sender As Object, e As EventArgs)

    End Sub



    Sub changelongpaper()
        Dim rowcount As Integer
        longpaper = 0
        rowcount = Guna2DataGridView1.Rows.Count
        longpaper = rowcount * 15
        longpaper = longpaper + 240
    End Sub
    Private Sub PD_BeginPrint(sender As Object, e As PrintEventArgs) Handles PD.BeginPrint
        Dim pagesetup As New PageSettings
        pagesetup.PaperSize = New PaperSize("Custom", 250, 500) 'fixed size
        'pagesetup.PaperSize = New PaperSize("Custom", 250, longpaper)
        PD.DefaultPageSettings = pagesetup
    End Sub



    Private Sub PD_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PD.PrintPage
        Dim f8 As New Font("Calibri", 8, FontStyle.Regular)
        Dim f10 As New Font("Calibri", 10, FontStyle.Regular)
        Dim f10b As New Font("Calibri", 10, FontStyle.Bold)
        Dim f14 As New Font("Calibri", 14, FontStyle.Bold)

        Dim leftmargin As Integer = PD.DefaultPageSettings.Margins.Left
        Dim centermargin As Integer = PD.DefaultPageSettings.PaperSize.Width / 2
        Dim rightmargin As Integer = PD.DefaultPageSettings.PaperSize.Width

        'font alignment
        Dim right As New StringFormat
        Dim center As New StringFormat

        right.Alignment = StringAlignment.Far
        center.Alignment = StringAlignment.Center

        Dim line As String
        line = "****************************************************************"

        'range from top
        'logo
        Dim logoImage As Image = My.Resources.ResourceManager.GetObject("logo")
        e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - 150) / 2), 5, 150, 35)

        'e.Graphics.DrawImage(logoImage, 0, 250, 150, 50)
        'e.Graphics.DrawImage(logoImage, CInt((e.PageBounds.Width - logoImage.Width) / 2), CInt((e.PageBounds.Height - logoImage.Height) / 2), logoImage.Width, logoImage.Height)

        'e.Graphics.DrawString("Store :", f14, Brushes.Black, centermargin, 5, center)
        e.Graphics.DrawString("New York Street 15 Avenue", f10, Brushes.Black, centermargin, 40, center)
        e.Graphics.DrawString("Tel +1763545473", f10, Brushes.Black, centermargin, 55, center)

        e.Graphics.DrawString("Invoice ID", f8, Brushes.Black, 0, 75)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 75)
        e.Graphics.DrawString("DRW8555RE", f8, Brushes.Black, 70, 75)

        e.Graphics.DrawString("Cashier", f8, Brushes.Black, 0, 85)
        e.Graphics.DrawString(":", f8, Brushes.Black, 50, 85)
        e.Graphics.DrawString("Steve Jobs", f8, Brushes.Black, 70, 85)

        e.Graphics.DrawString("08/17/2021 | 15.34", f8, Brushes.Black, 0, 95)
        'DetailHeader
        e.Graphics.DrawString("Qty", f8, Brushes.Black, 0, 110)
        e.Graphics.DrawString("Item", f8, Brushes.Black, 25, 110)
        e.Graphics.DrawString("Price", f8, Brushes.Black, 180, 110, right)
        e.Graphics.DrawString("Total", f8, Brushes.Black, rightmargin, 110, right)
        '
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, 120)

        Dim height As Integer 'DGV Position
        Dim i As Long
        cashier.Guna2DataGridView1.AllowUserToAddRows = False
        'If DataGridView1.CurrentCell.Value Is Nothing Then
        '    Exit Sub
        'Else
        For row As Integer = 0 To cashier.Guna2DataGridView1.RowCount - 1
            height += 15
            e.Graphics.DrawString(cashier.Guna2DataGridView1.Rows(row).Cells(1).Value.ToString, f8, Brushes.Black, 0, 115 + height)
            e.Graphics.DrawString(cashier.Guna2DataGridView1.Rows(row).Cells(0).Value.ToString, f8, Brushes.Black, 25, 115 + height)
            i = cashier.Guna2DataGridView1.Rows(row).Cells(2).Value
            cashier.Guna2DataGridView1.Rows(row).Cells(2).Value = Format(i, "##,##0")
            e.Graphics.DrawString(cashier.Guna2DataGridView1.Rows(row).Cells(2).Value.ToString, f8, Brushes.Black, 180, 115 + height, right)

            'totalprice
            Dim totalprice As Long
            totalprice = Val(cashier.Guna2DataGridView1.Rows(row).Cells(1).Value * cashier.Guna2DataGridView1.Rows(row).Cells(2).Value)
            e.Graphics.DrawString(totalprice.ToString("##,##0"), f8, Brushes.Black, rightmargin, 115 + height, right)
            '

        Next
        'End If

        Dim height2 As Integer
        height2 = 145 + height
        sumprice() 'call sub
        e.Graphics.DrawString(line, f8, Brushes.Black, 0, height2)
        e.Graphics.DrawString("Total: " & Format(t_price, "##,##0"), f10b, Brushes.Black, rightmargin, 10 + height2, right)
        e.Graphics.DrawString(t_qty, f10b, Brushes.Black, 0, 10 + height2)
        'Barcode
        Dim gbarcode As New MessagingToolkit.Barcode.BarcodeEncoder
        Try
            Dim barcodeimage As Image
            barcodeimage = New Bitmap(gbarcode.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, "DRW8555RE"))
            e.Graphics.DrawImage(barcodeimage, CInt((e.PageBounds.Width - 150) / 2), 35 + height2, 150, 35)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        e.Graphics.DrawString("~ Thanks for shopping ~", f10, Brushes.Black, centermargin, 70 + height2, center)
        e.Graphics.DrawString("~ Nosware Store ~", f10, Brushes.Black, centermargin, 85 + height2, center)
    End Sub
    Dim t_price As Long
    Dim t_qty As Long
    Sub sumprice()
        Dim countprice As Long = 0
        For rowitem As Long = 0 To cashier.Guna2DataGridView1.RowCount - 1
            countprice = countprice + Val(cashier.Guna2DataGridView1.Rows(rowitem).Cells(2).Value * cashier.Guna2DataGridView1.Rows(rowitem).Cells(1).Value)
        Next
        t_price = countprice
        Dim countqty As Long = 0
        For rowitem As Long = 0 To cashier.Guna2DataGridView1.RowCount - 1
            countqty = countqty + cashier.Guna2DataGridView1.Rows(rowitem).Cells(1).Value
        Next
        t_qty = countqty
    End Sub



    Private Sub btnsave_Click(sender As Object, e As EventArgs)
        changelongpaper()
        PPD.Document = PD
        PPD.ShowDialog()
        'PD.Print()  'Direct Print
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        cashier.Guna2DataGridView1.AllowUserToAddRows = True

    End Sub




    Sub loadsales()
        ' Get the selected date from DateTimePicker1
        Dim selectedDate As Date = DateTime.Value

        ' Ensure a valid date is selected
        If selectedDate = Nothing Then
            MessageBox.Show("Please select a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' Format the selected date as "yyyy-MM-dd"
        Dim sdate As String = selectedDate.ToString("yyyy-MM-dd")

        Dim i As Integer = 0
        Guna2DataGridView1.Rows.Clear()

        ' Open the database connection
        con.Open()

        ' SQL query using the selected date and the new join structure
        cmd = New SqlClient.SqlCommand(
            "SELECT ca.id, ca.invoice, c.Category, un.unit, " &
            "ca.price, ca.qty, ca.total " &
            "FROM tblcart AS ca " &
            "INNER JOIN tblInventory AS inv ON ca.pid = inv.InventoryID " &
            "INNER JOIN tblproduct AS p ON inv.id = p.id " &
            "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
            "INNER JOIN tblunit AS un ON p.uid = un.unitID " &
            "WHERE ca.sdate = '" & sdate & "'", con)

        ' Execute the query and populate the DataGridView
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView1.Rows.Add(i, dr("id").ToString, dr("invoice").ToString, dr("Category").ToString,
                                        dr("unit").ToString, dr("price").ToString, dr("qty").ToString, dr("total").ToString)
        End While
        dr.Close()
        con.Close()

        ' Update summary labels
        lblTot.Text = Format(GetData("SELECT SUM(total) FROM tblcart WHERE sdate = '" & sdate & "' AND status LIKE 'Sold'"), "#,##0.00")
        lbldiscount.Text = Format(GetData("SELECT SUM(Discount) FROM tblpayment WHERE sdate = '" & sdate & "'"), "#,##0.00")
        lblsubtotal.Text = Format(GetData("SELECT SUM(subtotal) FROM tblpayment WHERE sdate = '" & sdate & "'"), "#,##0.00")
        lblvat.Text = Format(GetData("SELECT SUM(vat) FROM tblpayment WHERE sdate = '" & sdate & "'"), "#,##0.00")
        lbltotsales.Text = Format(GetData("SELECT SUM(amountdue) FROM tblpayment WHERE sdate = '" & sdate & "'"), "#,##0.00")

        ' Display total sales in the label
        lbltotal.Text = "Total Daily Sales: " & lbltotsales.Text

        ' Add total row to DataGridView
        Guna2DataGridView1.Rows.Add("", "", "", "", "", "", "", "", "", "", "TOTAL", lblTot.Text)
    End Sub




    Function GetData(ByVal sql As String) As Double
        Try
            con.Open()
            cmd = New SqlClient.SqlCommand(sql, con)
            Dim result As Object = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CDbl(result)
            Else
                Return 0
            End If
        Catch ex As Exception
            ' Handle exception (e.g., log error)
            Return 0
        Finally
            con.Close()
        End Try
    End Function




    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub

    Private Sub DateTime_ValueChanged(sender As Object, e As EventArgs) Handles DateTime.ValueChanged
        loadsales()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub frmdailysales_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub
End Class