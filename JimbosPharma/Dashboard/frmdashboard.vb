Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Data.SqlClient

Public Class frmdashboard

    Dim cn As New SqlConnection

    'Sub loadchart()
    '    Try
    '        ' Database connection
    '        cn.ConnectionString = "Data Source=Data Source=TECHQUINA\SQLNEWINSTANCE;Initial Catalog=JimbospharmaDB;Integrated Security=True;MultipleActiveResultSets=True;"
    '        If cn.State = ConnectionState.Closed Then cn.Open()

    '        ' Define query to group by subtotal, vat, and discount
    '        Dim query As String = "SELECT subtotal, vat, discount, SUM(amountdue) AS TotalAmount " &
    '                              "FROM tblpayment " &
    '                              "WHERE sdate = CAST(GETDATE() AS DATE) " &
    '                              "GROUP BY subtotal, vat, discount"

    '        ' Execute query
    '        Dim da As New SqlDataAdapter(query, cn)
    '        Dim ds As New DataSet
    '        da.Fill(ds, "tblpayment")

    '        ' Check if data exists
    '        If ds.Tables("tblpayment").Rows.Count = 0 Then
    '            'MessageBox.Show("No data found for the selected criteria.", "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Chart1.Series.Clear()
    '            Return
    '        End If

    '        ' Configure the chart
    '        With Chart1
    '            .Series.Clear()
    '            .Series.Add("Series1")
    '            .Series("Series1").ChartType = SeriesChartType.Pie
    '            .Series("Series1").IsValueShownAsLabel = True ' Show labels on the pie chart
    '            .Series("Series1").LabelFormat = "P2" ' Show percentage labels

    '            ' Loop through the rows of the dataset to set the chart labels and values
    '            For Each row As DataRow In ds.Tables("tblpayment").Rows
    '                ' Create a label combining subtotal, vat, and discount
    '                Dim label As String = "Subtotal: " & row("subtotal").ToString() & ", VAT: " & row("vat").ToString() & ", Discount: " & row("discount").ToString()
    '                Dim totalAmount As Double = Convert.ToDouble(row("TotalAmount"))

    '                ' Add the data to the chart
    '                .Series("Series1").Points.AddXY(label, totalAmount)
    '            Next
    '        End With

    '        ' Bind data to chart
    '        Chart1.DataSource = ds.Tables("tblpayment")
    '        Chart1.DataBind()

    '    Catch ex As Exception
    '        'MessageBox.Show("Error wala pang laman ang chart: " & ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If cn.State = ConnectionState.Open Then cn.Close()
    '    End Try
    'End Sub


    Sub loadchart()
        Try
            ' Set up database connection string
            cn.ConnectionString = "Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False;"

            ' Open connection if closed
            If cn.State = ConnectionState.Closed Then cn.Open()

            ' Query: Get the top 5 products sold in the current month
            Dim query As String = "SELECT TOP 5 p.item_des, SUM(ca.qty) AS TotalSold " & _
                                  "FROM tblcart AS ca " & _
                                  "INNER JOIN tblInventory AS inv ON ca.pid = inv.InventoryID " & _
                                  "INNER JOIN tblproduct AS p ON inv.id = p.id " & _
                                  "WHERE MONTH(ca.sdate) = MONTH(GETDATE()) AND YEAR(ca.sdate) = YEAR(GETDATE()) " & _
                                  "GROUP BY p.item_des " & _
                                  "ORDER BY TotalSold DESC"

            ' Execute query and fill dataset
            Dim da As New SqlDataAdapter(query, cn)
            Dim ds As New DataSet
            da.Fill(ds, "tblsales")

            ' Check if dataset contains data
            If ds.Tables("tblsales").Rows.Count = 0 Then
                'MessageBox.Show("No data found for this month's chart.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Chart1.Series.Clear()
                Return
            End If

            ' Configure the pie chart
            With Chart1
                .Series.Clear() ' Clear previous data
                .Titles.Clear() ' Clear previous titles
                .Legends.Clear() ' Clear previous legends

                ' Add a new chart title with white font
                Dim title As New Title("Top 5 Products Sold This Month")
                title.ForeColor = Color.Black ' Set title color to white
                .Titles.Add(title)

                ' Add legend with white font
                Dim legend As New Legend("Legend1")
                legend.ForeColor = Color.Black ' Set legend text color to white
                .Legends.Add(legend)

                ' Add a pie chart series
                .Series.Add("Sales")
                .Series("Sales").ChartType = SeriesChartType.Pyramid
                .Series("Sales").IsValueShownAsLabel = True ' Show labels on slices
                .Series("Sales").LabelFormat = "N2" ' Show values in plain numeric format with 2 decimal places
                .Series("Sales").Legend = "Legend1" ' Attach legend

                ' Populate the chart with data
                For Each row As DataRow In ds.Tables("tblsales").Rows
                    ' Generate readable labels for pie slices using String.Format
                    Dim label As String = String.Format("Product: {0}", row("item_des"))
                    Dim totalSold As Double = Convert.ToDouble(row("TotalSold"))

                    ' Add data points
                    .Series("Sales").Points.AddXY(label, totalSold)
                Next
            End With

        Catch ex As Exception
            ' Handle errors
            MessageBox.Show("Error generating the chart: " & ex.Message, "Chart Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure the connection is closed
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub





    'Sub sellingprod()

    '    Dim query As String = "SELECT p.prodname, SUM(pay.amountdue) AS TotalSales " &
    '                  "FROM tblproduct p " &
    '                  "JOIN tblpayment pay ON p.productid = pay.productid " &
    '                  "GROUP BY p.prodname " &
    '                  "ORDER BY TotalSales DESC"

    '    Dim da As New SqlDataAdapter(query, cn)
    '    Dim ds As New DataSet
    '    da.Fill(ds, "TopProducts")

    '    With Chart1
    '        .Series.Clear()
    '        .Series.Add("Top Products")
    '        .Series("Top Products").ChartType = SeriesChartType.Bar
    '        .Series("Top Products").IsValueShownAsLabel = True

    '        For Each row As DataRow In ds.Tables("TopProducts").Rows
    '            .Series("Top Products").Points.AddXY(row("prodname").ToString(), Convert.ToDouble(row("TotalSales")))
    '        Next
    '    End With

    'End Sub




    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub
    Private Sub frmdashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        supplierCount()
        usercounts()
        'GetExpiredItemCount()
        ProductCount()
        'purchaseamount()
        'deliverycost()
        loadchart()
        LoadLowStock()
        Timer1.Start()

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub




    Sub supplierCount()
        Try
            Dim i As Integer = 0
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblSupplier", con)
            i = CInt(cmd.ExecuteScalar())
            con.Close()

            lblsupplier.Text = i.ToString()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Sub ProductCount()
        Try
            Dim i As Integer = 0
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblInventory as iv inner join tblproduct as p on iv.id = p.id", con)
            i = CInt(cmd.ExecuteScalar())
            con.Close()

            itemcount.Text = i.ToString()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Sub usercounts()
        Try
            Dim i As Integer = 0
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tbluser WHERE User_Type <> 'System Administrator' ", con)
            i = CInt(cmd.ExecuteScalar())
            con.Close()

            userlbl.Text = i.ToString()

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try
    End Sub


    Private Sub lbltotalsales_Click(sender As Object, e As EventArgs) Handles lbltotalsales.Click

        With frmdailysales
            .loadsales()
            .lbltotal.Text = lbltotalsales.Text

        End With


    End Sub

    Private Sub lbltotalsales_TextChanged(sender As Object, e As EventArgs) Handles lbltotalsales.TextChanged

        With frmdailysales
            Dim sdate As String = .DateTime.Value.ToString("yyyy-MM-dd")

            lbltotalsales.Text = Format(.GetData("select sum(amountdue) from tblpayment where sdate between '" & sdate & "' and '" & sdate & "'"), "#,#00.00")
        End With

    End Sub




    'Function GetExpiredItemCount() As Integer  ' PURPOSE IS TO  COUNT ALL EXPIRED THAT = TO OUR CURRENT DATE
    '    Dim count As Integer = 0
    '    Dim currentDate As Date = Date.Today

    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If


    '    cmd = New SqlClient.SqlCommand("SELECT COUNT(*) FROM tblproduct WHERE expdate = @CurrentDate", con)
    '    cmd.Parameters.AddWithValue("@CurrentDate", currentDate)
    '    count = Convert.ToInt32(cmd.ExecuteScalar())
    '    con.Close()

    '    lblexp.Text = count.ToString()


    '    If con.State = ConnectionState.Open Then
    '        con.Close()
    '    End If
    '    Return count
    'End Function



    'Sub purchaseamount()
    '    Try
    '        con.Open()

    '        cmd = New SqlClient.SqlCommand("select top 1 deliveryid, orderr, supid, daterel, receivedby, priceperunit, priceperunit * qty as totalprice, shippingcost, taxcost, qty, othercost, shippingcost + taxcost + othercost as totaldeliverycost, [status] from tbldelivery order by deliveryid desc", con)
    '        dr = cmd.ExecuteReader()

    '        con.Close()

    '        ' Process the data if needed

    '        lblpurchamount.Text = Format(gettotaldata("select isnull(sum(priceperunit * qty),0) from tbldelivery"), "#,##0.00")

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '            MessageBox.Show("Connection closed successfully.")
    '        End If
    '    End Try
    'End Sub


    'Sub deliverycost()
    '    Try
    '        con.Open()

    '        cmd = New SqlClient.SqlCommand("select top 1 deliveryid, orderr, supid, daterel, receivedby, priceperunit, priceperunit * qty as totalprice, shippingcost, taxcost, qty, othercost, shippingcost + taxcost + othercost as totaldeliverycost, [status] from tbldelivery order by deliveryid desc", con)
    '        dr = cmd.ExecuteReader()

    '        con.Close()

    '        ' Process the data if needed

    '        Dim totalDeliveryCost As Decimal = gettotaldata("select isnull(sum(shippingcost + taxcost + othercost),0) from tbldelivery")
    '        divcost.Text = Format(totalDeliveryCost, "#,##0.00")

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '            MessageBox.Show("Connection closed successfully.")
    '        End If
    '    End Try
    'End Sub




    Function gettotaldata(ByVal sql As String) As Double
        Try
            con.Open()
            cmd = New SqlClient.SqlCommand(sql, con)
            Dim result As Object = cmd.ExecuteScalar()
            Return If(result IsNot Nothing AndAlso Not IsDBNull(result), Convert.ToDouble(result), 0)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Function







    'Private Sub BindChart(query As String)
    '    ' Create a connection and retrieve the data
    '    Using conn As New SqlConnection("your_connection_string_here")
    '        conn.Open()

    '        Dim cmd As New SqlCommand(query, conn)
    '        Dim reader As SqlDataReader = cmd.ExecuteReader()

    '        ' Configure the chart control
    '        Dim chartControl As New System.Windows.Forms.DataVisualization.Charting.Chart
    '        Dim series As New System.Windows.Forms.DataVisualization.Charting.Series

    '        ' Set chart properties
    '        series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
    '        series.Name = "Today's Sales"
    '        chartControl.Series.Clear()
    '        chartControl.Series.Add(series)

    '        ' Add data to the chart
    '        While reader.Read()
    '            series.Points.AddXY(reader("users").ToString(), Convert.ToDecimal(reader("TotalSales")))
    '        End While

    '        ' Close reader and connection
    '        reader.Close()
    '        conn.Close()

    '        ' Set chart appearance
    '        chartControl.Titles.Add("Today's Sales Report")
    '        chartControl.Dock = DockStyle.Fill

    '        ' Add the chart to your form
    '        frmdailysales.Controls.Clear() ' Clear previous controls
    '        frmdailysales.Controls.Add(chartControl)
    '    End Using
    'End Sub

    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Guna2Panel9_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel9.Paint

    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub

    Sub LoadLowStock()
        Try
            cn.ConnectionString = "Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False;"

            If cn.State = ConnectionState.Closed Then cn.Open()

            Dim query As String = "SELECT item_des, Quantity FROM tblinventory as iv inner join tblproduct as p on iv.id = p.id WHERE Quantity < 10 ORDER BY Quantity ASC"

            Dim da As New SqlDataAdapter(query, cn)
            Dim dt As New DataTable
            da.Fill(dt)

            ' Bind data
            Guna2DataGridView1.DataSource = dt
            Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Guna2DataGridView1.DefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Bold)
            Guna2DataGridView1.RowTemplate.Height = 30

            ' Rename headers
            Guna2DataGridView1.Columns(0).HeaderText = "Product Name"
            Guna2DataGridView1.Columns(1).HeaderText = "Stock Quantity"

            ' Highlight low stock items
            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                Dim quantity As Integer = Convert.ToInt32(row.Cells(1).Value)

                ' 🔴 Critical Stock (0-4) -> Red
                If quantity < 5 Then
                    'row.DefaultCellStyle.BackColor = Color.Red
                    row.DefaultCellStyle.ForeColor = Color.White
                    row.DefaultCellStyle.SelectionBackColor = Color.DarkRed
                    row.DefaultCellStyle.SelectionForeColor = Color.White

                    ' 🟡 Low Stock Warning (5-9) -> Yellow
                ElseIf quantity >= 5 And quantity <= 9 Then
                    row.DefaultCellStyle.BackColor = Color.Yellow
                    row.DefaultCellStyle.ForeColor = Color.Black

                    ' ✅ Normal Stock (10+) -> Default (No Highlight)
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                    row.DefaultCellStyle.ForeColor = Color.Black
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading low stock items: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then cn.Close()
        End Try
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LoadLowStock()
    End Sub
End Class