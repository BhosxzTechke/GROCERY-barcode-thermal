
Imports System.Data.SqlClient
Imports System.IO
Imports MissionsGrocery.loginform

Public Class frmdeliverylist

    ' Connection string
    Private con As New SqlConnection(Dbsql.connString)


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Function GetBatchNo() As String
        ' A FUNCTION TO RETURN A STRING BATCH NUMBER
        Try
            ' Open the connection to the database
            Using con As New SqlConnection(Dbsql.connString)
                con.Open()

                ' Query to find the latest batch number, sorted by ID (assuming batch numbers are stored in tblDelivery)
                Using cmd As New SqlCommand("SELECT TOP 1 BatchNumber FROM tblDelivery ORDER BY DeliveryID DESC", con)
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        ' Read the first row
                        If dr.HasRows Then
                            dr.Read()
                            Dim lastBatch As String = dr.Item("BatchNumber").ToString()
                            ' Extract the numeric part and increment it
                            Dim numericPart As Integer = CInt(lastBatch.Substring(5))
                            Return "Batch" & (numericPart + 1).ToString("D3")
                        Else
                            ' If no batch number exists, start from Batch001
                            Return "Batch001"
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' In case of an error, display the error message
            MsgBox(ex.Message, vbCritical)
            Return String.Empty
        End Try
    End Function





    Sub SupplierCBXdelivery()        '       AUTOMATIC TO FILL IN COMBOBOX IN SUPPLIER
        con.Open()

        cmd = New SqlClient.SqlCommand("Select * from tblSupplier order by CompanyName", con)
        Dim ds As New DataSet
        Dim da As New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds, "CompanyName")
        Dim col As New List(Of String)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            col.Add(ds.Tables(0).Rows(i)("CompanyName").ToString)
        Next
        suppliercombo.DataSource = col
        suppliercombo.AutoCompleteMode = AutoCompleteMode.None
        suppliercombo.DropDownStyle = ComboBoxStyle.DropDownList
        cmd = Nothing
        con.Close()

    End Sub

    Private Sub frmdeliverylist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtbatch.Text = GetBatchNo()
        Catch ex As Exception
            MsgBox("Error loading batch number: " & ex.Message, vbCritical)
        End Try



        If String.IsNullOrWhiteSpace(txtcost.Text) Then
            txtcost.Text = "₱"
            txtcost.SelectionStart = txtcost.Text.Length ' Position cursor at the end
        End If

        SupplierCBXdelivery()
        SupplierCBXdelivery()

        Guna2DataGridView1.AlternatingRowsDefaultCellStyle = Guna2DataGridView1.RowsDefaultCellStyle

        suppliercombo.SelectedIndex = -1

        Me.KeyPreview = True




        ' Allow past dates but restrict future dates
        ddate.MaxDate = DateTime.Today

        ddate.MinDate = DateTimePicker.MinimumDateTime ' Allows past dates


        RemovePastDates()





    End Sub




    ' EXPIRATION CALENDAR NO PAST DATE
    Private Sub Guna2DataGridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles Guna2DataGridView1.RowsAdded
        ' Automatically enter edit mode for the "Expiration Date" column in newly added rows
        If e.RowCount > 0 Then
            Dim calendarColumnIndex As Integer = Guna2DataGridView1.Columns("calendar").Index
            For i As Integer = 0 To e.RowCount - 1
                Dim rowIndex As Integer = e.RowIndex + i
                Dim cell = Guna2DataGridView1.Rows(rowIndex).Cells(calendarColumnIndex)

                ' Ensure the default expiration date is in the future
                cell.Value = DateTime.Today.AddDays(1)

                ' Focus the cell and enter edit mode to show the calendar
                Guna2DataGridView1.CurrentCell = cell
                Guna2DataGridView1.BeginEdit(True)
            Next
        End If
    End Sub

    Private Sub Guna2DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellEndEdit
        Try
            Dim colName As String = Guna2DataGridView1.Columns(e.ColumnIndex).Name

            ' Check if the edited column is the expiration date column
            If colName = "calendar" Then
                Dim selectedDate As DateTime
                If DateTime.TryParse(Guna2DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString(), selectedDate) Then
                    ' Ensure expiration date is in the future
                    If selectedDate < DateTime.Today Then
                        MessageBox.Show("Expiration date must be in the future.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        ' Remove past date by clearing the cell or setting it to tomorrow
                        Guna2DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = DateTime.Today.AddDays(1)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error validating expiration date: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub RemovePastDates()
        ' Loop through each row and remove past expiration dates
        For Each row As DataGridViewRow In Guna2DataGridView1.Rows
            If Not row.IsNewRow Then
                Dim colIndex As Integer = Guna2DataGridView1.Columns("calendar").Index
                If row.Cells(colIndex).Value IsNot Nothing Then
                    Dim expDate As DateTime
                    If DateTime.TryParse(row.Cells(colIndex).Value.ToString(), expDate) Then
                        ' If the date is in the past, reset it to tomorrow
                        If expDate < DateTime.Today Then
                            row.Cells(colIndex).Value = DateTime.Today.AddDays(1)
                        End If
                    End If
                End If
            End If
        Next
    End Sub



    Private Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            ' Validate input fields
            If String.IsNullOrWhiteSpace(txtbatch.Text) Then
                MessageBox.Show("Batch Number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If suppliercombo.SelectedIndex = -1 Then
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Perform database operation within a transaction
            Using con As New SqlConnection(Dbsql.connString)
                con.Open()
                Using transaction = con.BeginTransaction()
                    Try
                        ' Insert new delivery and retrieve the DeliveryID
                        Dim deliveryQuery As String = "INSERT INTO tblDelivery (BatchNumber, Supplier, DeliveryDate, CostPrice) OUTPUT INSERTED.DeliveryID VALUES (@BatchNumber, @Supplier, @DeliveryDate, @CostPrice)"
                        Dim deliveryID As Integer

                        Using cmd As New SqlCommand(deliveryQuery, con, transaction)
                            ' Add parameters explicitly
                            cmd.Parameters.Add("@BatchNumber", SqlDbType.VarChar).Value = txtbatch.Text.Trim()
                            cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = suppliercombo.Text.Trim()
                            cmd.Parameters.Add("@DeliveryDate", SqlDbType.Date).Value = ddate.Value
                            cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = txtcost.Text.Trim()
                            deliveryID = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' Commit the transaction
                        transaction.Commit()

                        ' Display success message
                        lbldelid.Text = deliveryID.ToString()
                        MessageBox.Show("Delivery saved successfully! Delivery ID: " & deliveryID.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' Disable inputs to prevent redundant saves
                        ToggleInputs(False)

                    Catch ex As Exception
                        ' Roll back transaction in case of error
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs)
        Try
            ' Confirm reset action
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to reset? This will delete the current unsaved delivery record.", "Reset Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If result = DialogResult.Yes Then
                ' Perform deletion if a DeliveryID exists
                If Not String.IsNullOrEmpty(lbldelid.Text) Then
                    DeleteRecentDelivery(lbldelid.Text)
                End If

                ' Clear all input fields and reset controls
                ResetForm()

                MessageBox.Show("Form has been reset and the current unsaved delivery record has been deleted.", "Reset Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred during reset: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetForm()
        ' Clear input fields
        txtbatch.Text = String.Empty
        suppliercombo.SelectedIndex = -1
        ddate.Value = DateTime.Now
        lbldelid.Text = String.Empty

        ' Generate a new batch number
        txtbatch.Text = GetBatchNo()

        ' Re-enable inputs
        ToggleInputs(True)

        Guna2DataGridView1.Rows.Clear() ' This will remove all rows from the DataGridView

    End Sub

    Private Sub ToggleInputs(enabled As Boolean)
        txtbatch.Enabled = enabled
        suppliercombo.Enabled = enabled
        ddate.Enabled = enabled
        'btnsave.Enabled = enabled
    End Sub

    ' Delete the most recent delivery record (associated with the current form's delivery ID)
    Private Sub DeleteRecentDelivery(currentDeliveryID As String)
        Try
            Using con As New SqlConnection(connString)
                con.Open()

                ' Check if the current delivery is the last one added
                Dim checkQuery As String = "SELECT TOP 1 DeliveryID FROM tblDelivery ORDER BY DeliveryID DESC"
                Dim recentID As Integer = 0

                Using cmd As New SqlCommand(checkQuery, con)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        recentID = Convert.ToInt32(result)
                    End If
                End Using

                ' Only delete if the current ID matches the most recent ID
                If recentID.ToString() = currentDeliveryID Then
                    Dim deleteQuery As String = "DELETE FROM tblDelivery WHERE DeliveryID = @DeliveryID"
                    Using cmd As New SqlCommand(deleteQuery, con)
                        cmd.Parameters.AddWithValue("@DeliveryID", currentDeliveryID)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred while deleting the delivery record: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    ' Open browse delivery form
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Using frm As New frmbrowsedelivery
            frm.txtsearch.Focus()
            frm.ShowDialog()

        End Using
    End Sub




    ' Toggle input controls


    Private Sub Guna2DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Guna2DataGridView1.EditingControlShowing
        Dim txtBox As TextBox = TryCast(e.Control, TextBox)

        If txtBox IsNot Nothing Then
            RemoveHandler txtBox.KeyPress, AddressOf NumericOnly_KeyPress ' Prevent multiple handlers
            If Guna2DataGridView1.CurrentCell.ColumnIndex = 6 Or Guna2DataGridView1.CurrentCell.ColumnIndex = 7 Then ' Adjust indices for Quantity and Cost Price
                AddHandler txtBox.KeyPress, AddressOf NumericOnly_KeyPress
            End If
        End If






        ' di makikita ung past at ung lima lng

        If Guna2DataGridView1.CurrentCell.OwningColumn.Name = "calendar" Then
            Dim dtp As DateTimePicker = TryCast(e.Control, DateTimePicker)
            If dtp IsNot Nothing Then
                ' Allow 5 days before today to show but not selectable
                dtp.MinDate = DateTime.Today.AddDays(-5)
                dtp.MaxDate = DateTimePicker.MaximumDateTime

                ' Hook the ValueChanged to block selection of past dates
                RemoveHandler dtp.ValueChanged, AddressOf CalendarDateChanged
                AddHandler dtp.ValueChanged, AddressOf CalendarDateChanged
            End If
        End If




    End Sub

    Private Sub CalendarDateChanged(sender As Object, e As EventArgs)
        Dim dtp As DateTimePicker = CType(sender, DateTimePicker)
        If dtp.Value < DateTime.Today Then
            MessageBox.Show("You can't select past dates.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            dtp.Value = DateTime.Today
        End If
    End Sub





    Private Sub NumericOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow numbers, backspace, and period (decimal point)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        Dim txtBox As TextBox = CType(sender, TextBox)
        If e.KeyChar = "." AndAlso txtBox.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub



    Private Sub ValidateAndSave()
        Dim isValid As Boolean = True

        ' Loop through each row in the DataGridView
        For Each row As DataGridViewRow In Guna2DataGridView1.Rows
            If row.IsNewRow Then Continue For

            ' Validate Quantity (Column 2)
            Dim quantityCell = row.Cells(6)
            If quantityCell.Value Is Nothing OrElse String.IsNullOrWhiteSpace(quantityCell.Value.ToString()) OrElse Not IsNumeric(quantityCell.Value.ToString()) Then
                quantityCell.Style.BackColor = Color.Red
                isValid = False
            Else
                quantityCell.Style.BackColor = Color.White
            End If

            ' Validate Cost Price (Column 3)
            Dim costPriceCell = row.Cells(7)
            If costPriceCell.Value Is Nothing OrElse String.IsNullOrWhiteSpace(costPriceCell.Value.ToString()) OrElse Not IsNumeric(costPriceCell.Value.ToString()) Then
                costPriceCell.Style.BackColor = Color.Red
                isValid = False
            Else
                costPriceCell.Style.BackColor = Color.White
            End If

        Next

        ' Prompt the user based on the validation result
        If Not isValid Then
            MsgBox("Please correct the highlighted cells before saving.", vbExclamation)
        Else
            MsgBox("All rows are valid. Proceeding to save.", vbInformation)

            ' Call the save logic here
            SaveData()
        End If
    End Sub

    ' Save logic placeholder
    Private Sub SaveData()
        ' Add your code to save the data to a database or file here
        MsgBox("Data saved successfully!", vbInformation)
    End Sub






    Private Sub UpdateInventory(productID As Integer, quantity As Integer, expirationDate As Date, deliveryID As Integer)
        Try
            Dim query As String
            ' Check if batch exists based on ProductID, ExpirationDate, and DeliveryID
            Dim batchExists As Boolean
            query = "SELECT COUNT(*) FROM tblInventory WHERE id = @ProductID AND ExpirationDate = @ExpirationDate"

            Using conn As New SqlConnection(connString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ProductID", productID)
                    cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate)
                    'cmd.Parameters.AddWithValue("@DeliveryID", deliveryID)
                    conn.Open()
                    batchExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using

            'And DeliveryID = @DeliveryID


            If batchExists Then
                ' Update quantity if batch exists

                ' AUDIT: Save new values
                Dim newvalues As String = "ID: " & productID & ", " &
                                  "Quantity Add: " & quantity
                Dim userid As Integer = GlobalUser.UserId
                logoaudit(userid, "Update", "Delivery", productID, "", newvalues)



                query = "UPDATE tblInventory SET Quantity = Quantity + @Quantity WHERE id = @ProductID AND ExpirationDate = @ExpirationDate"
                Using conn As New SqlConnection(connString)
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Quantity", quantity)
                        cmd.Parameters.AddWithValue("@ProductID", productID)
                        cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate)
                        'cmd.Parameters.AddWithValue("@DeliveryID", deliveryID)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            Else
                ' Insert new batch if it does not exist
                query = "INSERT INTO tblInventory (id, Quantity, ExpirationDate,  DeliveryID) VALUES (@ProductID, @Quantity, @ExpirationDate, @DeliveryID)"
                Using conn As New SqlConnection(connString)
                    Using cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("@ProductID", productID)
                        cmd.Parameters.AddWithValue("@Quantity", quantity)
                        cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate)
                        cmd.Parameters.AddWithValue("@DeliveryID", deliveryID)
                        conn.Open()
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("Error updating inventory: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' Function to save delivery line item details
    Private Sub SaveDeliveryLineItem(deliveryID As Integer, productID As Integer, productName As String, quantity As Integer, costPrice As Decimal, supplierID As Integer, expirationDate As Date)
        Try
            ' Insert delivery line item
            Dim query As String = "INSERT INTO tblDeliveryLineItem (DeliveryID, id, ProductName, Quantity, CostPrice, SupplierID, ExpirationDate) VALUES (@DeliveryID, @ProductID, @ProductName, @Quantity, @CostPrice, @SupplierID, @ExpirationDate)"
            Using conn As New SqlConnection(connString)
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@DeliveryID", deliveryID)
                    cmd.Parameters.AddWithValue("@ProductID", productID)
                    cmd.Parameters.AddWithValue("@ProductName", productName)
                    cmd.Parameters.AddWithValue("@Quantity", quantity)
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice)
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID)
                    cmd.Parameters.AddWithValue("@ExpirationDate", expirationDate)
                    conn.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            'MessageBox.Show("Delivery line item saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)



        Catch ex As Exception
            MessageBox.Show("Error saving delivery line item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Function to save delivery and update all relevant details
    Private Function SaveDelivery(deliveryCostPrice As Decimal, supplierID As Integer, deliveryDate As DateTime) As Integer
        Dim deliveryID As Integer = 0
        Try
            ' Validate input fields
            If String.IsNullOrWhiteSpace(txtbatch.Text) Then
                MessageBox.Show("Batch Number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return 0
            End If

            If suppliercombo.SelectedIndex = -1 Then
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return 0
            End If

            ' Perform database operation within a transaction

            Using con As New SqlConnection(connString)
                con.Open()
                Using transaction = con.BeginTransaction()
                    Try


                        ' Insert new delivery and retrieve the DeliveryID
                        Dim deliveryQuery As String = "INSERT INTO tblDelivery (BatchNumber, Supplier, DeliveryDate, CostPrice) OUTPUT INSERTED.DeliveryID VALUES (@BatchNumber, @Supplier, @DeliveryDate, @CostPrice)"
                        Using cmd As New SqlCommand(deliveryQuery, con, transaction)
                            cmd.Parameters.Add("@BatchNumber", SqlDbType.VarChar).Value = txtbatch.Text.Trim()
                            cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = suppliercombo.Text.Trim()
                            cmd.Parameters.Add("@DeliveryDate", SqlDbType.Date).Value = deliveryDate
                            cmd.Parameters.Add("@CostPrice", SqlDbType.Decimal).Value = deliveryCostPrice
                            deliveryID = Convert.ToInt32(cmd.ExecuteScalar()) ' Get the generated DeliveryID
                        End Using


                        Dim userid As Integer = GlobalUser.UserId

                        Dim newvalues As String = "Delivery Id: " & deliveryID & ", " &
                                      "Delivery Cost Price: " & deliveryCostPrice & ", " &
                                      "Supplier: " & suppliercombo.Text.Trim() & ", " &
                                      "Delivery Date: " & deliveryDate



                        logoaudit(userid, "INSERT", "Delivery ", deliveryID.ToString(), "", newvalues)



                        ' Commit the transaction
                        transaction.Commit()
                    Catch ex As Exception
                        ' Roll back transaction in case of error
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return deliveryID
    End Function




    Private Sub btnsavinginvent_Click(sender As Object, e As EventArgs) Handles btnsavinginvent.Click
        Try
            ' Declare the variable for total line items cost
            Dim totalLineItemsCost As Decimal = 0
            Dim quantity As Integer
            Dim costPrice As Decimal

            ' Validate if there's at least one item in the DataGridView
            If Guna2DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("No line items to save. Please add products to the delivery.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Calculate the total cost from the Delivery Line Items
            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                If Not row.IsNewRow Then
                    ' Check if Quantity or Cost Price is missing or invalid
                    If row.Cells("Quantity").Value Is Nothing OrElse row.Cells("CostPriceitem").Value Is Nothing Then
                        MessageBox.Show("Quantity and Cost Price are required for all line items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' Get Quantity value and ensure it is a valid positive integer
                    If Not Integer.TryParse(row.Cells("Quantity").Value.ToString(), quantity) OrElse quantity <= 0 Then
                        MessageBox.Show("Invalid or zero quantity in the line item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' Parse the cost price and ensure it's a valid positive decimal value
                    Dim rowCostPrice As String = row.Cells("CostPriceitem").Value.ToString().Replace("₱", "").Trim()
                    If Not Decimal.TryParse(rowCostPrice, costPrice) OrElse costPrice <= 0 Then
                        MessageBox.Show("Invalid cost price format or zero cost in the line item.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' Accumulate the total cost of line items
                    totalLineItemsCost += quantity * costPrice

                    '' Validate Reorder level (ensure it's not empty, null, or negative)
                    'Dim reorderLevel As Integer
                    'If row.Cells("Reorder").Value Is Nothing OrElse Not Integer.TryParse(row.Cells("Reorder").Value.ToString(), reorderLevel) OrElse reorderLevel < 0 Then
                    '    MessageBox.Show("Reorder level is required and must be a valid non-negative integer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    Exit Sub
                    'End If
                End If
            Next

            ' Now handle the delivery cost price and remove the Peso sign
            Dim deliveryCostPrice As Decimal
            Dim currentText As String = txtcost.Text.Replace("₱", "").Trim() ' Remove the Peso sign from text

            ' Try to parse the cleaned string into a decimal
            If Not Decimal.TryParse(currentText, deliveryCostPrice) OrElse deliveryCostPrice <= 0 Then
                MessageBox.Show("Invalid cost price format or zero delivery cost price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate if the total line item cost matches the delivery cost
            If totalLineItemsCost <> deliveryCostPrice Then
                MessageBox.Show("The total cost of items (" & totalLineItemsCost.ToString("₱0.00") & ") does not match the Delivery Cost Price (" & deliveryCostPrice.ToString("₱0.00") & ").", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate required fields before saving
            If String.IsNullOrEmpty(supid.Text) Then
                MessageBox.Show("Supplier is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                supid.Focus()
                Exit Sub
            End If



            ' Save the Delivery first to generate the DeliveryID
            Dim deliveryID As Integer = SaveDelivery(deliveryCostPrice, Integer.Parse(supid.Text), DateTime.Now)

            ' Ensure Delivery ID is successfully generated
            If deliveryID <= 0 Then
                MessageBox.Show("Failed to save the delivery. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Loop through all rows and save each line item
            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                If Not row.IsNewRow Then
                    ' Check if ProductID or other required fields are missing
                    If row.Cells("ProductID").Value Is Nothing OrElse row.Cells("ProductNamecolumn").Value Is Nothing OrElse row.Cells("Quantity").Value Is Nothing Then
                        MessageBox.Show("Missing required information in the line item (ProductID, ProductName, or Quantity).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    ' Parse required fields
                    Dim productID As Integer = Integer.Parse(row.Cells("ProductID").Value.ToString())
                    Dim productName As String = row.Cells("ProductNamecolumn").Value.ToString()
                    quantity = Integer.Parse(row.Cells("Quantity").Value.ToString())
                    costPrice = Decimal.Parse(row.Cells("CostPriceitem").Value.ToString())


                    ' Ensure expiration date is valid
                    Dim expirationDate As Date = Date.Parse(row.Cells("calendar").Value.ToString())

                    ' Add supplierID based on the context (e.g., from supid.Text or other control)
                    Dim supplierID As Integer = Integer.Parse(supid.Text)

                    ' Save the Delivery Line Item with the generated DeliveryID and supplierID
                    SaveDeliveryLineItem(deliveryID, productID, productName, quantity, costPrice, supplierID, expirationDate)

                    ' Update the Inventory for the product
                    UpdateInventory(productID, quantity, expirationDate, deliveryID)
                End If
            Next



            MessageBox.Show("Delivery and Inventory saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            clearall()

        Catch ex As Exception
            MessageBox.Show("An unexpected error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Function ValidateGridCell(row As DataGridViewRow, columnName As String) As Boolean
        If row.Cells(columnName).Value Is Nothing OrElse String.IsNullOrEmpty(row.Cells(columnName).Value.ToString()) Then
            MessageBox.Show("Please fill in the " & columnName & " for all line items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Sub clearall()

        Guna2DataGridView1.Rows.Clear() ' Clear existing rows in DataGridView
        txtcost.Clear()
        suppliercombo.SelectedIndex = -1
        'ddate.Value = DateTime.Now ' Reset to current date and time
        txtbatch.Text = GetBatchNo()
        tsEnableBarcode.Checked = False

    End Sub



    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        ' Check if the clicked cell is the "Expiration Date" cell (calendar column)
        If Guna2DataGridView1.Columns(e.ColumnIndex).Name = "calendar" Then
            Guna2DataGridView1.BeginEdit(True) ' Force edit mode to keep the calendar visible
        End If
    End Sub
    'Sub inventorylist()
    '    Guna2DataGridView3.Rows.Clear()
    '    Dim i As Integer = 0

    '    Try
    '        ' Ensure the connection is closed before opening
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    '        con.Open()
    '        cmd = New SqlCommand("select * from tblInventory as iv inner join tblproduct as p on iv.id = p.id ", con)
    '        dr = cmd.ExecuteReader

    '        While dr.Read
    '            i += 1

    '            '' Combine FirstName, MiddleName, and LastName to form FullName
    '            'Dim fullName As String = String.Format("{0} {1} {2}", dr("FirstName").ToString(), dr("MiddleName").ToString(), dr("LastName").ToString()).Trim()

    '            ' Retrieve and convert the ImageSpl column
    '            Dim img As Image = Nothing
    '            If Not IsDBNull(dr("imagepath")) Then
    '                Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
    '                img = ByteArrayToImage(imgData)
    '            Else
    '                ' Optional: Set a placeholder image if ImageSpl is NULL
    '                img = My.Resources.eye_close_up ' Ensure this resource exists, or replace it
    '            End If

    '            ' Add row with FullName and other fields, including img
    '            Guna2DataGridView3.Rows.Add(dr("InventoryID").ToString(), i, dr("id").ToString(), img, dr("barcode").ToString(), dr("item_des").ToString(), dr("Quantity").ToString(), dr("ExpirationDate").ToString(), dr("price").ToString())
    '        End While

    '        dr.Close()

    '        ' Set row height
    '        For i = 0 To Guna2DataGridView3.Rows.Count - 1
    '            Dim r As DataGridViewRow = Guna2DataGridView3.Rows(i)
    '            r.Height = 75
    '        Next

    '        ' Set image layout for the image column
    '        Dim imageColumn = DirectCast(Guna2DataGridView3.Columns("image"), DataGridViewImageColumn)
    '        imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch

    '    Catch ex As Exception
    '        MessageBox.Show("Error loading user list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        con.Close()
    '    End Try
    'End Sub

    Private Function ByteArrayToImage(ByVal byteArray As Byte()) As Image
        ' Check if byteArray is valid and has data
        If byteArray Is Nothing OrElse byteArray.Length = 0 Then
            Return Nothing
        End If

        Try
            Using ms As New MemoryStream(byteArray)
                Dim originalImage As System.Drawing.Image = System.Drawing.Image.FromStream(ms)

                ' Resize the image to specific dimensions, e.g., 100x130 pixels
                Dim resizedImage As New Bitmap(100, 130) ' Adjust the width and height as needed
                Using g As Graphics = Graphics.FromImage(resizedImage)
                    g.DrawImage(originalImage, 0, 0, 100, 130) ' Draw the image at specified size
                End Using

                Return resizedImage
            End Using
        Catch ex As ArgumentException
            ' Handle invalid image format
            Return Nothing
        End Try
    End Function
    Private Sub Guna2DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    'Private Sub Guna2DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellClick
    '    If e.RowIndex >= 0 Then
    '        ' Assuming "Quantity" is in the third column (index 2)
    '        ' Create the custom calendar column
    '        Dim expirationDateColumn As New DataGridViewCalendarColumn()
    '        expirationDateColumn.HeaderText = "Expiration Date"
    '        expirationDateColumn.Name = "ExpirationDate"

    '        ' Add the custom column to the DataGridView
    '        Guna2DataGridView1.Columns.Add(expirationDateColumn)

    '        Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(e.RowIndex).Cells("Quantity")
    '        Guna2DataGridView1.BeginEdit(True) ' Start editing to focus
    '    End If
    'End Sub



    '                                                               CUSTOMIZING DATAGRID AND TEXTBOX
    Private Sub Guna2DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Guna2DataGridView1.CellFormatting
        ' Check if the column is "Cost Price" and format accordingly
        If Guna2DataGridView1.Columns(e.ColumnIndex).Name = "CostPrice" AndAlso e.Value IsNot Nothing Then
            ' Format with ₱ symbol if not already formatted
            Dim costValue As Decimal
            If Decimal.TryParse(e.Value.ToString(), costValue) Then
                e.Value = "₱" & costValue.ToString("N2") ' Format to currency with 2 decimal places
                e.FormattingApplied = True
            End If
        End If
    End Sub

    'Private Sub Guna2DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles Guna2DataGridView1.KeyDown
    '    ' Check if Enter key was pressed
    '    If e.KeyCode = Keys.Enter Then
    '        ' Prevent the Enter key from moving to the next row
    '        e.SuppressKeyPress = True

    '        ' Get the current cell
    '        Dim currentCell As DataGridViewCell = Guna2DataGridView1.CurrentCell
    '        If currentCell IsNot Nothing Then
    '            Dim nextColumnIndex As Integer = currentCell.ColumnIndex + 1
    '            Dim currentRowIndex As Integer = currentCell.RowIndex

    '            ' Move to the next cell in the same row
    '            If nextColumnIndex < Guna2DataGridView1.ColumnCount Then
    '                Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(currentRowIndex).Cells(nextColumnIndex)
    '            Else
    '                ' If it's the last column, move to the first column of the next row
    '                If currentRowIndex + 1 < Guna2DataGridView1.RowCount Then
    '                    Guna2DataGridView1.CurrentCell = Guna2DataGridView1.Rows(currentRowIndex + 1).Cells(0)
    '                End If
    '            End If

    '            ' Begin editing the new cell
    '            Guna2DataGridView1.BeginEdit(True)
    '        End If
    '    End If
    'End Sub

    'Private Sub Guna2DataGridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellEnter
    '    ' Ensure the row index is greater than 0 and that we're in the "Expiration Date" column
    '    If e.RowIndex > 0 AndAlso Guna2DataGridView1.Columns(e.ColumnIndex).Name = "ExpirationDate" Then
    '        ' Start editing the cell to show the calendar
    '        Guna2DataGridView1.BeginEdit(True)
    '    End If
    'End Sub


    'Private Sub Guna2DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Guna2DataGridView1.CellBeginEdit
    ' ' Ensure the DateTimePicker appears when editing the Expiration Date cell
    '    If Guna2DataGridView1.Columns(e.ColumnIndex).Name = "calendar" Then
    '        ' Optionally, you can set the DateTimePicker's initial value or format here
    '        Dim dateCell As DataGridViewDateTimePickerCell = CType(Guna2DataGridView1.Rows(e.RowIndex).Cells("calendar"), DataGridViewDateTimePickerCell)
    '        ' You can adjust the default behavior or formatting of the calendar if needed
    '    End If
    'End Sub




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim totalCost As Decimal = 0

        ' Loop through the rows in reverse to avoid index shifting
        For i As Integer = Guna2DataGridView1.Rows.Count - 1 To 0 Step -1
            Dim isChecked As Boolean = Convert.ToBoolean(Guna2DataGridView1.Rows(i).Cells("check").Value)

            ' If the checkbox is checked, remove the row
            If isChecked Then
                ' Subtract the cost price from total before deleting
                Dim rowCost As Decimal
                If Decimal.TryParse(Guna2DataGridView1.Rows(i).Cells("CostPriceItem").Value.ToString(), rowCost) Then
                    totalCost += rowCost
                End If

                Guna2DataGridView1.Rows.RemoveAt(i)
            End If
        Next

        ' Update txtcost by subtracting the total removed cost
        Dim currentCost As Decimal
        If Decimal.TryParse(txtcost.Text, currentCost) Then
            txtcost.Text = (currentCost - totalCost).ToString("N2") ' Format as a number with 2 decimal places
        Else
            txtcost.Text = "0.00" ' If parsing fails, reset to zero
        End If
    End Sub







    Private Sub frmdeliverylist_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Dim value As Decimal

        If Decimal.TryParse(txtcost.Text.Replace("₱", "").Replace(",", "").Trim(), value) Then
            ' Format the text with the peso sign and two decimal places
            txtcost.Text = "₱" & value.ToString("#,##0.00")
        Else
            ' Clear the text if the input is invalid
            txtcost.Clear()
        End If
    End Sub

    Private Sub txtcost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcost.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txtcost.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub suppliercombo_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles suppliercombo.SelectedIndexChanged
        Dim supplierid As Integer

        If suppliercombo.SelectedIndex <> -1 Then
            Try
                con.Open()
                cmd = New SqlClient.SqlCommand("Select * from tblSupplier where CompanyName = @supplier", con)
                cmd.Parameters.AddWithValue("@supplier", suppliercombo.SelectedItem.ToString())
                supplierid = Convert.ToInt32(cmd.ExecuteScalar())

                dr = cmd.ExecuteReader
                If dr.Read() Then
                    supid.Text = dr("SupplierID").ToString()
                    supid.Text = supplierid.ToString()

                Else
                    supid.Text = String.Empty
                End If
            Catch ex As Exception
                ' Handle the exception, if needed
            Finally
                If dr IsNot Nothing Then dr.Close()
                If cmd IsNot Nothing Then cmd.Dispose() ' Dispose the command to release resources
                If con.State = ConnectionState.Open Then con.Close() ' Close the connection
            End Try
        End If
    End Sub


    Private Sub UpdateTotalCost()
        Dim totalCost As Decimal = 0

        ' Loop through all rows in the DataGridView
        For Each row As DataGridViewRow In Guna2DataGridView1.Rows
            ' Skip new rows (empty rows)
            If Not row.IsNewRow Then
                Dim quantity As Integer = 0
                Dim costPrice As Decimal = 0

                ' Safely check and parse Quantity cell value
                If row.Cells("Quantity").Value IsNot Nothing AndAlso Integer.TryParse(row.Cells("Quantity").Value.ToString(), quantity) Then
                    ' Safely check and parse Cost Price Item cell value
                    If row.Cells("CostPriceitem").Value IsNot Nothing AndAlso Decimal.TryParse(row.Cells("CostPriceitem").Value.ToString(), costPrice) Then
                        ' Accumulate total cost
                        totalCost += quantity * costPrice
                    End If
                End If
            End If
        Next

        ' Update the Delivery Cost Price TextBox with Peso Sign
        txtcost.Text = "₱ " & totalCost.ToString("N2")
    End Sub


    Private Sub Guna2DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellValueChanged
        ' Check if the edited cell is in the Quantity or Cost Price Item column
        If e.RowIndex >= 0 AndAlso (e.ColumnIndex = Guna2DataGridView1.Columns("Quantity").Index Or e.ColumnIndex = Guna2DataGridView1.Columns("CostPriceitem").Index) Then
            UpdateTotalCost()
        End If
    End Sub

    Private Sub Guna2DataGridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles Guna2DataGridView1.CurrentCellDirtyStateChanged
        If Guna2DataGridView1.IsCurrentCellDirty Then
            Guna2DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub frmdeliverylist_KeyDown(sender As Object, e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub

    Private Sub ddate_ValueChanged(sender As Object, e As EventArgs) Handles ddate.ValueChanged
        ' Ensure the user cannot select a future date
        If ddate.Value > DateTime.Today Then
            MessageBox.Show("Future dates are not allowed!", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ddate.Value = DateTime.Today ' Reset to today's date
        End If
    End Sub




    Private Sub tsEnableBarcode_CheckedChanged(sender As Object, e As EventArgs) Handles tsEnableBarcode.CheckedChanged


        ' Optionally refocus the textbox
        If tsEnableBarcode.Checked Then
            txtBarcodeInput.Focus()
        End If


    End Sub




    Private Sub AddScannedProductToDelivery(scannedBarcode As String)
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT id, barcode, item_des, costprice FROM tblproduct WHERE barcode = @barcode"
            cmd = New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@barcode", scannedBarcode)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim itemId As String = dr("id").ToString()
                Dim barcode As String = dr("barcode").ToString()
                Dim itemDesc As String = dr("item_des").ToString()
                Dim costPrice As String = dr("costprice").ToString()

                ' Check if already in delivery list (assuming frmbrowsedelivery.HasItem(barcode) returns Boolean)
                If frmbrowsedelivery.HasItem(barcode) Then
                    MessageBox.Show("You already have this item: " & itemDesc)
                Else
                    frmbrowsedelivery.AddToDeliveryListLogic(itemId, barcode, itemDesc, costPrice)
                End If
            Else
                MessageBox.Show("Product not found for barcode: " & scannedBarcode)
            End If

            dr.Close()
        Catch ex As Exception
            MsgBox("Barcode scan error: " & ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then con.Close()

            ' If toggle is ON, keep focus in txtBarcodeInput
            If tsEnableBarcode.Checked Then
                txtBarcodeInput.Focus()
            End If
        End Try
    End Sub




    Private Sub txtBarcodeInput_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBarcodeInput.KeyPress
        If e.KeyChar = Chr(13) Then ' Enter key detected
            Dim barcode As String = txtBarcodeInput.Text.Trim()
            If barcode <> "" Then
                AddScannedProductToDelivery(barcode)
                txtBarcodeInput.Clear()
            End If
            e.Handled = True ' Prevent beep sound on Enter key
        End If
    End Sub

End Class
