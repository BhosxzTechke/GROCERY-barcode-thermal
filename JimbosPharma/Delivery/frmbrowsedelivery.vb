Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Guna.UI2.WinForms

Public Class frmbrowsedelivery


    Private DeliveryID As Integer




    ' Constructor to initialize the form with the delivery ID
    'Public Sub New(deliveryId As Integer)
    '    InitializeComponent()
    '    Me.DeliveryID = deliveryId
    'End Sub

    ' Method to load products into the DataGridView




    Sub FilterSearch()
        FlowLayoutPanel1.Controls.Clear() ' Clear existing items

        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            ' Define the search query
            Dim query As String
            If String.IsNullOrWhiteSpace(txtsearch.Text) Then
                query = "SELECT p.id, p.imagepath, p.barcode, p.item_des, " &
                              "c.Category, u.unit, p.costprice, p.price, p.WholePrice " &
                              "FROM tblproduct AS p " &
                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
                              "INNER JOIN tblunit AS u ON p.uid = u.unitID where Category Like @Category OR item_des Like @item ORDER BY Category "

            Else
                query = "SELECT p.id, p.imagepath, p.barcode, p.item_des, " &
                              "c.Category, u.unit, p.costprice, p.price, p.WholePrice " &
                              "FROM tblproduct AS p " &
                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
                              "INNER JOIN tblunit AS u ON p.uid = u.unitID where Category Like @Category OR item_des Like @item ORDER BY Category "
            End If

            ' Execute the query
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Category", "%" & txtsearch.Text.Trim() & "%")
            cmd.Parameters.AddWithValue("@item", "%" & txtsearch.Text.Trim() & "%")

            dr = cmd.ExecuteReader()


            Dim found As Boolean = False

            While dr.Read()
                found = True ' Set flag to indicate that results exist


                ' Create panel for product
                Dim productPanel As New Panel With {
                .Size = New Size(200, 280),
                .BackColor = Color.LightGray,
                .Margin = New Padding(10)
            }

                ' Load image
                Dim pic As New PictureBox With {
                .Size = New Size(180, 120),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(10, 10)
            }
                If Not IsDBNull(dr("imagepath")) Then
                    Dim imageData As Object = dr("imagepath")
                    If TypeOf imageData Is Byte() Then
                        pic.Image = ByteArrayToImages(DirectCast(imageData, Byte()))
                    ElseIf TypeOf imageData Is String AndAlso Not String.IsNullOrEmpty(imageData.ToString()) Then
                        pic.Image = Nothing ' Load image from file if needed
                    End If
                End If

                ' Product Name Label
                Dim lblName As New Label With {
                .Text = dr("item_des").ToString(),
                .Size = New Size(180, 30),
                .Location = New Point(10, 140),
                .Font = New Font("Arial", 10, FontStyle.Bold)
            }

                ' Category Label
                Dim lblCategory As New Label With {
                .Text = "Category: " & dr("Category").ToString(),
                .Size = New Size(180, 20),
                .Location = New Point(10, 170)
            }

                ' Price Label
                Dim lblPrice As New Label With {
                .Text = "Retail Price: " & "₱" & dr("price").ToString(),
                .Size = New Size(180, 20),
                .Location = New Point(10, 190),
                .ForeColor = Color.Green
            }

                ' Price Label
                Dim lblwhole As New Label With {
                .Text = "Whole Price: " & "₱" & dr("WholePrice").ToString(),
                .Size = New Size(180, 20),
                .Location = New Point(10, 210),
                .ForeColor = Color.Green
            }

                ' Add to List Button (Replaces DataGridView Click)
                Dim btnAdd As New Button With {
                .Text = "Received Delivery",
                .Size = New Size(180, 30),
                .Location = New Point(10, 230)
            }

                ' Store product details in button Tag
                btnAdd.Tag = New String() {
                dr("id").ToString(),        ' ID
                dr("barcode").ToString(),   ' Barcode
                dr("item_des").ToString(),  ' Item Description
                dr("costprice").ToString()  ' Cost Price
            }

                ' Attach Click Event
                AddHandler btnAdd.Click, AddressOf AddToDeliveryList

                ' Add controls to panel
                productPanel.Controls.Add(pic)
                productPanel.Controls.Add(lblName)
                productPanel.Controls.Add(lblCategory)
                productPanel.Controls.Add(lblPrice)
                productPanel.Controls.Add(lblwhole)
                productPanel.Controls.Add(btnAdd)

                ' Add panel to FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(productPanel)
            End While

            dr.Close()
        Catch ex As Exception
            MsgBox("Error loading products: " & ex.Message, vbCritical)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    Private Sub browseprod()
        FlowLayoutPanel1.Controls.Clear() ' Clear product items only

        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            ' SQL query
            Dim query As String = "SELECT p.id, p.imagepath, p.barcode, p.item_des, " &
                              "c.Category, u.unit, p.costprice, p.price " &
                              "FROM tblproduct AS p " &
                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
                              "INNER JOIN tblunit AS u ON p.uid = u.unitID"

            cmd = New SqlClient.SqlCommand(query, con)
            dr = cmd.ExecuteReader()

            Dim productCheckboxes As New List(Of CheckBox)

            While dr.Read()
                Dim productPanel As New Panel With {
                .Size = New Size(200, 300),
                .BackColor = Color.LightGray,
                .Margin = New Padding(10)
            }

                ' Image
                Dim pic As New PictureBox With {
                .Size = New Size(220, 120),
                .SizeMode = PictureBoxSizeMode.Zoom,
                .Location = New Point(10, 10),
                .BackColor = Color.White
            }

                If Not IsDBNull(dr("imagepath")) Then
                    Dim imageData As Object = dr("imagepath")
                    If TypeOf imageData Is Byte() Then
                        pic.Image = ByteArrayToImages(DirectCast(imageData, Byte()))
                    End If
                End If

                ' Product Name
                Dim lblName As New Label With {
                .Text = dr("item_des").ToString(),
                .AutoSize = False,
                .Size = New Size(220, 30),
                .Location = New Point(10, 140),
                .Font = New Font("Segoe UI Semibold", 10, FontStyle.Bold),
                .ForeColor = Color.Black
            }

                ' Category
                Dim lblCategory As New Label With {
                .Text = "Category: " & dr("Category").ToString(),
                .AutoSize = False,
                .Size = New Size(220, 20),
                .Location = New Point(10, 170),
                .Font = New Font("Segoe UI", 9),
                .ForeColor = Color.DimGray
            }

                ' Wholesale Price
                Dim lblWholesale As New Label With {
                .Text = "Wholesale: ₱" & Convert.ToDecimal(dr("costprice")).ToString("N2"),
                .AutoSize = False,
                .Size = New Size(220, 20),
                .Location = New Point(10, 195),
                .Font = New Font("Segoe UI", 9, FontStyle.Italic),
                .ForeColor = Color.DarkSlateGray
            }

                ' Retail Price
                Dim lblRetail As New Label With {
                .Text = "Retail: ₱" & Convert.ToDecimal(dr("price")).ToString("N2"),
                .AutoSize = False,
                .Size = New Size(220, 20),
                .Location = New Point(10, 215),
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .ForeColor = Color.SeaGreen
            }

                ' Checkbox
                Dim prodCheckBox As New CheckBox With {
                .Location = New Point(10, 250),
                .Text = "Select",
                .Font = New Font("Segoe UI", 9),
                .Tag = New String() {
                    dr("id").ToString(),
                    dr("barcode").ToString(),
                    dr("item_des").ToString(),
                    dr("costprice").ToString()
                }
            }

                productCheckboxes.Add(prodCheckBox)

                ' Add controls to panel
                productPanel.Controls.Add(pic)
                productPanel.Controls.Add(lblName)
                productPanel.Controls.Add(lblCategory)
                productPanel.Controls.Add(lblWholesale)
                productPanel.Controls.Add(lblRetail)
                productPanel.Controls.Add(prodCheckBox)

                FlowLayoutPanel1.Controls.Add(productPanel)
            End While

            dr.Close()

            ' Select All Checkbox behavior
            RemoveHandler chkSelectAll.CheckedChanged, Nothing ' Reset if handler exists
            AddHandler chkSelectAll.CheckedChanged, Sub(sender2 As Object, e2 As EventArgs)
                                                        For Each cb In productCheckboxes
                                                            cb.Checked = chkSelectAll.Checked
                                                        Next
                                                    End Sub

        Catch ex As Exception
            MsgBox("Error loading products: " & ex.Message, vbCritical)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub



    Private Sub ReceiveSelectedProducts()
        For Each panel As Control In FlowLayoutPanel1.Controls
            If TypeOf panel Is Panel Then
                For Each ctrl As Control In panel.Controls
                    If TypeOf ctrl Is CheckBox AndAlso DirectCast(ctrl, CheckBox).Checked Then
                        Dim productData As String() = DirectCast(ctrl.Tag, String())
                        Dim itemId As String = productData(0)
                        Dim barcode As String = productData(1)
                        Dim itemDesc As String = productData(2)
                        Dim costPrice As String = productData(3)

                        AddToDeliveryListLogic(itemId, barcode, itemDesc, costPrice)
                    End If
                Next
            End If
        Next
    End Sub

    Public Function HasItem(barcode As String) As Boolean
        ' Assuming you have a DataGridView named dgvDeliveryList
        For Each row As DataGridViewRow In frmdeliverylist.Guna2DataGridView1.Rows
            If Not row.IsNewRow AndAlso row.Cells("barcode").Value IsNot Nothing Then
                If row.Cells("barcode").Value.ToString() = barcode Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function



    Public Sub AddToDeliveryListLogic(itemId As String, barcode As String, itemDesc As String, costPrice As String)
        ' Check if frmdeliverylist is already open
        Dim targetForm As frmdeliverylist = Nothing
        For Each frm As Form In Application.OpenForms
            If TypeOf frm Is frmdeliverylist Then
                targetForm = CType(frm, frmdeliverylist)
                Exit For
            End If
        Next

        If targetForm Is Nothing Then
            targetForm = New frmdeliverylist()
            targetForm.Show()
        End If

        ' Check for duplicate
        For Each row As DataGridViewRow In targetForm.Guna2DataGridView1.Rows
            If row.Cells(3).Value IsNot Nothing AndAlso row.Cells(3).Value.ToString() = itemId Then
                Return ' Skip duplicates silently
            End If
        Next

        ' Add new row
        Dim newRowIndex As Integer = targetForm.Guna2DataGridView1.Rows.Add()
        With targetForm.Guna2DataGridView1.Rows(newRowIndex)
            .Cells(3).Value = itemId
            .Cells(4).Value = barcode
            .Cells(5).Value = itemDesc
            .Cells(7).Value = costPrice
        End With


    End Sub



    Private Sub AddToDeliveryList(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim productData As String() = DirectCast(btn.Tag, String())

        Dim itemId As String = productData(0) ' Product ID
        Dim barcode As String = productData(1) ' Barcode
        Dim itemDesc As String = productData(2) ' Description
        Dim costPrice As String = productData(3) ' Cost Price

        ' Confirmation prompt using Guna2MessageDialog
        Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
        Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
        Guna2MessageDialog1.Caption = "Confirm Selection"
        Guna2MessageDialog1.Text = "Item Description: " & itemDesc & vbCrLf & "Please confirm."

        Dim result As DialogResult = Guna2MessageDialog1.Show()
        If result = DialogResult.Yes Then

            ' Check if frmdeliverylist is already open
            Dim targetForm As frmdeliverylist = Nothing
            For Each frm As Form In Application.OpenForms
                If TypeOf frm Is frmdeliverylist Then
                    targetForm = CType(frm, frmdeliverylist)
                    Exit For
                End If
            Next

            ' If the form is not open, create a new instance
            If targetForm Is Nothing Then
                targetForm = New frmdeliverylist()
                targetForm.Show()
            End If

            ' Check for duplicate ID in the target DataGridView
            Dim isDuplicate As Boolean = False
            For Each row As DataGridViewRow In targetForm.Guna2DataGridView1.Rows
                If row.Cells(3).Value IsNot Nothing AndAlso row.Cells(3).Value.ToString() = itemId Then
                    isDuplicate = True
                    Exit For
                End If
            Next

            If isDuplicate Then
                ' Show warning using Guna2MessageDialog
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Duplicate Entry"
                Guna2MessageDialog1.Text = "This item is already in the list."
                Guna2MessageDialog1.Show()
            Else
                ' Add row to target form's DataGridView
                Dim newRowIndex As Integer = targetForm.Guna2DataGridView1.Rows.Add()
                With targetForm.Guna2DataGridView1.Rows(newRowIndex)
                    .Cells(3).Value = itemId
                    .Cells(4).Value = barcode
                    .Cells(5).Value = itemDesc
                    .Cells(7).Value = costPrice
                End With




                ' Show success message using Guna2MessageDialog
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Success"
                Guna2MessageDialog1.Text = "Item successfully added to the delivery list."
                Guna2MessageDialog1.Show()
            End If
        End If
    End Sub











    'Sub SearchBarcode()
    '    Try
    '        Dim i As Integer = 0
    '        DataGridView1.Rows.Clear() ' Clear existing rows in DataGridView

    '        ' Open database connection
    '        If con.State = ConnectionState.Closed Then con.Open()

    '        ' Define query
    '        Dim query As String = "SELECT p.id, p.imagepath, p.barcode, p.item_des, " &
    '                              "c.Category, u.unit, p.costprice, p.price " &
    '                              "FROM tblproduct AS p " &
    '                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
    '                              "INNER JOIN tblunit AS u ON p.uid = u.unitID " &
    '                              "WHERE p.barcode LIKE @barcode OR p.item_des LIKE @item_des"

    '        ' Create SQL command and parameters
    '        cmd = New SqlClient.SqlCommand(query, con)
    '        cmd.Parameters.Add("@barcode", SqlDbType.VarChar).Value = "%" & txtsearch.Text.Trim() & "%"
    '        cmd.Parameters.Add("@item_des", SqlDbType.VarChar).Value = "%" & txtsearch.Text.Trim() & "%"

    '        ' Execute reader
    '        dr = cmd.ExecuteReader()

    '        ' Process data
    '        While dr.Read()
    '            i += 1

    '            ' Handle image
    '            Dim img As Image = Nothing
    '            If Not IsDBNull(dr("imagepath")) Then
    '                Dim imageData As Object = dr("imagepath")
    '                If TypeOf imageData Is Byte() Then
    '                    img = ByteArrayToImage(DirectCast(imageData, Byte()))
    '                ElseIf TypeOf imageData Is String AndAlso Not String.IsNullOrEmpty(imageData.ToString()) Then
    '                    Dim imgPath As String = imageData.ToString()
    '                    Try
    '                        img = Nothing
    '                    Catch ex As Exception
    '                        img = Nothing
    '                    End Try
    '                End If
    '            Else
    '                img = Nothing ' Default image if no imagepath
    '            End If

    '            ' Handle costPrice and price
    '            Dim costPrice As Decimal = If(IsDBNull(dr("costprice")), 0, Convert.ToDecimal(dr("costprice")))
    '            Dim price As Decimal = If(IsDBNull(dr("price")), 0, Convert.ToDecimal(dr("price")))

    '            ' Add row to DataGridView
    '            DataGridView1.Rows.Add(i,
    '                                  dr("id").ToString(),
    '                                  img,
    '                                  dr("barcode").ToString(),
    '                                  dr("item_des").ToString(),
    '                                  dr("Category").ToString(),
    '                                  dr("unit").ToString(),
    '                                  costPrice.ToString("C2"),
    '                                  price.ToString("C2"))
    '        End While

    '        ' Adjust row height and auto-size columns
    '        For Each r As DataGridViewRow In DataGridView1.Rows
    '            r.Height = 40
    '        Next
    '        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

    '        dr.Close()

    '    Catch ex As Exception
    '        ' Display error message
    '        MsgBox("Error: " & ex.Message, vbCritical, "Search Error")
    '    Finally
    '        ' Ensure connection is closed
    '        If con.State = ConnectionState.Open Then con.Close()
    '    End Try
    'End Sub'




    'Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
    '    ' Check if the Enter key is pressed
    '    If e.KeyCode = Keys.Enter Then
    '        ' Call the search function with the full barcode
    '        SearchBarcode()

    '        ' Prevent the beep sound when pressing Enter
    '        e.SuppressKeyPress = True
    '    End If
    'End Sub


    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        ' Check if the barcode textbox is empty before setting focus back
        If String.IsNullOrEmpty(txtsearch.Text) Then
            ' Set focus back to the barcode textbox if it's empty
            txtsearch.Focus()
        End If
    End Sub

    'Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged

    '    SearchBarcode()
    'End Sub

    ' Form load event to load products
    Private Sub frmbrowsedelivery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        browseprod()
        'deliverCBX()
        Me.KeyPreview = True

        'cbofilter.SelectedIndex = -1
    End Sub




    'Sub deliverCBX()        '       AUTOMATIC TO FILL IN COMBOBOX IN SUPPLIER
    '    con.Open()

    '    cmd = New SqlClient.SqlCommand("Select * from tblcategory order by Category", con)
    '    Dim ds As New DataSet
    '    Dim da As New SqlClient.SqlDataAdapter(cmd)
    '    da.Fill(ds, "Category")
    '    Dim col As New List(Of String)
    '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '        col.Add(ds.Tables(0).Rows(i)("Category").ToString)
    '    Next
    '    cbofilter.DataSource = col
    '    cbofilter.AutoCompleteMode = AutoCompleteMode.None
    '    cbofilter.DropDownStyle = ComboBoxStyle.DropDownList
    '    cmd = Nothing
    '    con.Close()

    'End Sub

    Private Function ByteArrayToImages(ByVal byteArray As Byte()) As Image
        ' Check if byteArray is valid and has data
        If byteArray Is Nothing OrElse byteArray.Length = 0 Then
            Return Nothing
        End If

        Try
            Using ms As New MemoryStream(byteArray)
                Dim originalImage As Image = Image.FromStream(ms)

                ' Resize the image to specific dimensions, e.g., 100x130 pixels
                Dim resizedImage As New Bitmap(100, 130)
                Using g As Graphics = Graphics.FromImage(resizedImage)
                    g.DrawImage(originalImage, 0, 0, resizedImage.Width, resizedImage.Height)
                End Using

                Return resizedImage
            End Using
        Catch ex As ArgumentException
            ' Return placeholder image in case of invalid format
            Return Nothing
        End Try
    End Function


    'Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Try
    '        Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name

    '        If colname = "colselect" Then
    '            ' Extract the data from the selected row
    '            Dim itemId As String = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString() ' Assume ID is in Cell(1)
    '            Dim data As String = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
    '            Dim arr() As String = data.Split("|")

    '            ' Confirmation prompt using Guna2MessageDialog
    '            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
    '            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
    '            Guna2MessageDialog1.Caption = "Confirm Selection"
    '            Guna2MessageDialog1.Text = "Item Description: " & arr(0).ToString & vbCrLf & "Please confirm."

    '            Dim result As DialogResult = Guna2MessageDialog1.Show()
    '            If result = DialogResult.Yes Then

    '                ' Check if the target form is already open
    '                Dim targetForm As frmdeliverylist = Nothing

    '                For Each frm As Form In Application.OpenForms
    '                    If TypeOf frm Is frmdeliverylist Then
    '                        targetForm = CType(frm, frmdeliverylist)
    '                        Exit For
    '                    End If
    '                Next

    '                ' If the form is not open, create a new instance
    '                If targetForm Is Nothing Then
    '                    targetForm = New frmdeliverylist()
    '                    targetForm.Show()
    '                End If

    '                ' Check for duplicate ID in the target DataGridView
    '                Dim isDuplicate As Boolean = False
    '                For Each row As DataGridViewRow In targetForm.Guna2DataGridView1.Rows
    '                    If row.Cells(3).Value IsNot Nothing AndAlso row.Cells(3).Value.ToString() = itemId Then
    '                        isDuplicate = True
    '                        Exit For
    '                    End If
    '                Next

    '                If isDuplicate Then
    '                    ' Show warning using Guna2MessageDialog
    '                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning
    '                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
    '                    Guna2MessageDialog1.Caption = "Duplicate Entry"
    '                    Guna2MessageDialog1.Text = "This item is already in the list."
    '                    Guna2MessageDialog1.Show()
    '                Else
    '                    ' Add row data to specific columns in the target form's DataGridView
    '                    Dim newRowIndex As Integer = targetForm.Guna2DataGridView1.Rows.Add()
    '                    With targetForm.Guna2DataGridView1.Rows(newRowIndex)
    '                        .Cells(3).Value = itemId ' Add to column 4 (zero-based index) 
    '                        .Cells(4).Value = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString() ' Barcode
    '                        .Cells(5).Value = DataGridView1.Rows(e.RowIndex).Cells(4).Value.ToString() ' Item Description
    '                        .Cells(7).Value = DataGridView1.Rows(e.RowIndex).Cells(7).Value.ToString() ' Cost Price
    '                    End With

    '                    ' Show success message using Guna2MessageDialog
    '                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
    '                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
    '                    Guna2MessageDialog1.Caption = "Success"
    '                    Guna2MessageDialog1.Text = "Item successfully added to the delivery list."
    '                    Guna2MessageDialog1.Show()
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ' Show error message using Guna2MessageDialog
    '        Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
    '        Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
    '        Guna2MessageDialog1.Caption = "Error"
    '        Guna2MessageDialog1.Text = "An error occurred: " & ex.Message
    '        Guna2MessageDialog1.Show()
    '    End Try
    'End Sub





    'Sub prodrecord()
    '    Dim i As Integer = 0
    '    DataGridView1.Rows.Clear()

    '    Try
    '        ' Ensure the connection is closed before opening
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If

    '        ' Open connection
    '        con.Open()

    '        ' Define the query to fetch product records with INNER JOIN on related tables, excluding the Type column
    '        Dim query As String = "SELECT p.id, p.barcode, p.item_des, b.brand, g.generic, c.Category, f.Formulations, d.Dosage, p.reorder, p.price, u.unit, p.imagepath " &
    '                              "FROM tblproduct AS p " &
    '                              "INNER JOIN tblbrand AS b ON p.bid = b.brandID " &
    '                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
    '                              "INNER JOIN tblformulations AS f ON p.fid = f.formID " &
    '                              "INNER JOIN tbldosage AS d ON p.did = d.dosageID " &
    '                              "INNER JOIN tblgeneric AS g ON p.gid = g.genericID " &
    '                              "INNER JOIN tblunit AS u ON p.uid = u.unitID"

    '        ' Create and execute the command
    '        cmd = New SqlClient.SqlCommand(query, con)
    '        dr = cmd.ExecuteReader()

    '        ' Loop through data and populate DataGridView
    '        While dr.Read()
    '            i += 1

    '            ' Check if the image is stored as binary data or a file path
    '            Dim img As Image = Nothing
    '            If Not IsDBNull(dr("imagepath")) Then
    '                Dim imageData As Object = dr("imagepath")

    '                If TypeOf imageData Is Byte() Then
    '                    ' Convert binary data to image
    '                    img = ByteArrayToImage(DirectCast(imageData, Byte()))
    '                ElseIf TypeOf imageData Is String AndAlso Not String.IsNullOrEmpty(imageData.ToString()) Then
    '                    ' Load image from file path if it exists
    '                    Dim imgPath As String = imageData.ToString()
    '                    If IO.File.Exists(imgPath) Then
    '                        img = Image.FromFile(imgPath)
    '                    Else
    '                        img = My.Resources.eye_close_up ' Placeholder image if file is missing
    '                    End If
    '                End If
    '            Else
    '                ' Placeholder image if imagepath is NULL
    '                img = My.Resources.eye_close_up
    '            End If

    '            ' Add data to DataGridView
    '            DataGridView1.Rows.Add(i, dr("id").ToString(), dr("barcode").ToString(), dr("item_des").ToString(), dr("brand").ToString(), dr("generic").ToString(), dr("Category").ToString(), dr("Formulations").ToString(), dr("Dosage").ToString(), dr("reorder").ToString(), dr("unit").ToString(), dr("price").ToString(), img)
    '        End While

    '    Catch ex As Exception
    '        MsgBox("Error: " & ex.Message, vbCritical)

    '    Finally
    '        ' Close reader and connection
    '        If dr IsNot Nothing Then dr.Close()
    '        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
    '    End Try
    'End Sub

    Private Function ByteArrayToImage(ByVal byteArray As Byte()) As Image
        ' Check if byteArray is valid and has data
        If byteArray Is Nothing OrElse byteArray.Length = 0 Then
            Return Nothing
        End If

        Try
            Using ms As New MemoryStream(byteArray)
                Dim originalImage As Image = Image.FromStream(ms)

                ' Resize the image to specific dimensions, e.g., 100x130 pixels
                Dim resizedImage As New Bitmap(100, 130)
                Using g As Graphics = Graphics.FromImage(resizedImage)
                    g.DrawImage(originalImage, 0, 0, resizedImage.Width, resizedImage.Height)
                End Using

                Return resizedImage
            End Using
        Catch ex As ArgumentException
            ' Return placeholder image in case of invalid format
            Return Nothing
        End Try
    End Function

    'Sub searchproduct(ByVal search As String)
    '    Try
    '        If search = String.Empty Then Return
    '        con.Open()

    '        cmd = New SqlClient.SqlCommand("SELECT * FROM tblproduct WHERE id LIKE '" & search & "'", con)
    '        dr = cmd.ExecuteReader
    '        dr.Read()
    '        If dr.HasRows Then

    '            ' so kapag pinindut ung nasa datagridview pupunta sa form na qty

    '            With frmproddelivery

    '                ' Load image if available
    '                If Not IsDBNull(dr("imagepath")) Then
    '                    Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
    '                    Dim img As Image = ByteArrayToImage(imgData)
    '                    .Guna2CirclePictureBox1.Image = img
    '                Else
    '                    .Guna2CirclePictureBox1.Image = My.Resources.eye_close_up ' Default or placeholder image
    '                End If
    '                .lblPid.Text = dr.Item("id").ToString        '  INSERTING A VALUE ID FOR LABEL PROD 

    '                dr.Close()
    '                con.Close()
    '                .ShowDialog() '           SHOWING A DIALOG FORM FOR QTY
    '            End With
    '            ' Show your form or perform other actions here
    '        End If
    '        dr.Close()
    '        con.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbCritical)
    '    Finally
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    ''    End Try
    'End Sub

    'Sub browseproduct()
    '    ' Validate filter and search input
    '    If String.IsNullOrWhiteSpace(cbofilter.Text) OrElse String.IsNullOrWhiteSpace(txtsearch.Text) Then
    '        Return
    '    End If

    '    Try
    '        Dim i As Integer = 0
    '        DataGridView1.Rows.Clear()

    '        con.Open()

    '        ' Use parameterized query to prevent SQL injection
    '        Dim query As String = "SELECT * FROM tblproduct AS p " &
    '                              "INNER JOIN tblbrand AS b ON p.bid = b.brandID " &
    '                              "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
    '                              "INNER JOIN tblformulations AS f ON p.fid = f.formID " &
    '                              "INNER JOIN tblgeneric AS g ON p.gid = g.genericID " &
    '                              "INNER JOIN tbldosage AS ds ON p.did = ds.dosageID " &
    '                              "INNER JOIN tblunit AS u ON p.uid = u.unitID " &
    '                              "WHERE " & cbofilter.Text & " LIKE @search"

    '        cmd = New SqlClient.SqlCommand(query, con)
    '        cmd.Parameters.AddWithValue("@search", txtsearch.Text & "%")

    '        dr = cmd.ExecuteReader()

    '        While dr.Read()
    '            i += 1
    '            Dim img As Image = Nothing

    '            ' Handle image loading
    '            If Not IsDBNull(dr("imagepath")) Then
    '                Dim imageData As Object = dr("imagepath")

    '                If TypeOf imageData Is Byte() Then
    '                    ' Convert binary data to image
    '                    img = ByteArrayToImage(DirectCast(imageData, Byte()))
    '                ElseIf TypeOf imageData Is String AndAlso Not String.IsNullOrWhiteSpace(imageData.ToString()) Then
    '                    Dim imgPath As String = imageData.ToString()
    '                    If IO.File.Exists(imgPath) Then
    '                        img = Image.FromFile(imgPath)
    '                    Else
    '                        img = My.Resources.eye_close_up ' Placeholder image
    '                    End If
    '                End If
    '            Else
    '                img = My.Resources.eye_close_up ' Placeholder image
    '            End If

    '            ' Concatenate strings for the DataGridView row
    '            Dim productDetails As String = dr("brand").ToString() & " | " & dr("generic").ToString() & " | " &
    '                                           dr("Category").ToString() & " | " & dr("dosage").ToString() & " | " &
    '                                           dr("Formulations").ToString() & " | " & dr("unit").ToString()

    '            ' Add data to DataGridView
    '            DataGridView1.Rows.Add(i, dr("id").ToString(), dr("barcode").ToString(), productDetails, img)
    '        End While

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        ' Ensure resources are released
    '        If dr IsNot Nothing AndAlso Not dr.IsClosed Then
    '            dr.Close()
    '        End If
    '        If con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    '    End Try
    'End Sub


    Private Sub cbofilter_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Prevent typing in the filter ComboBox
        e.Handled = True
    End Sub











    'Dim colname As String = DataGridView1.Columns(e.ColumnIndex).Name
    'Dim data As String = DataGridView1.Rows(e.RowIndex).Cells(3).Value.ToString
    'Dim arr() As String = data.Split("|")

    'If colname = "Colselect" Then
    '    '#1
    '    If MsgBox("brand:  " & arr(0).ToString & vbCr &
    '              "Generic: " & arr(1).ToString & vbCr &
    '              "Category: " & arr(2).ToString & vbCr &
    '              "unit: " & arr(3).ToString & vbCr &
    '              "Formulations: " & arr(4).ToString & vbCr &
    '             "Dosage: " & arr(5).ToString & vbCr & "Please confirm. ", vbYesNo + vbQuestion) = vbYes Then

    '        With frmdeliverylist
    '            DataGridView1.Rows.Add(DataGridView1.Rows.Count + 1, .DataView.Rows(e.RowIndex).Cells(1).Value.ToString, .DataView.Rows(e.RowIndex).Cells(2).Value.ToString, arr(0).ToString, arr(1).ToString, arr(2).ToString, arr(3).ToString, arr(4).ToString, arr(5).ToString)

    '        End With
    '    End If

    'End If


    Private Sub cbofilter_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
    'Private Sub LoadProducts()
    '    Dim query As String = "SELECT ProductID, Barcode, Brand + ' - ' + GenericName AS ProductName FROM tblProduct"
    '    Dim dt As New DataTable()

    '    con.Open()
    '    Using cmd As New SqlCommand(query, con)
    '        Using da As New SqlDataAdapter(cmd)
    '            da.Fill(dt)
    '        End Using
    '    End Using

    '    DataGridView1.DataSource = dt
    'End Sub
    'Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

    '    Dim connectionString As String = "YourConnectionStringHere"
    '    Dim lineItemQuery As String = "INSERT INTO tblDeliveryLineItem (DeliveryID, ProductID, Quantity, CostPrice) VALUES (@DeliveryID, @ProductID, @Quantity, @CostPrice)"
    '    Dim updateInventoryQuery As String = "UPDATE tblInventory SET Quantity = Quantity + @Quantity WHERE ProductID = @ProductID"

    '    For Each row As DataGridViewRow In DataGridView1.SelectedRows
    '        con.Open()

    '        ' Insert line item
    '        Using cmd As New SqlCommand(lineItemQuery, con)
    '            cmd.Parameters.AddWithValue("@DeliveryID", DeliveryID)
    '            cmd.Parameters.AddWithValue("@ProductID", row.Cells("ProductID").Value)
    '            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(row.Cells("Quantity").Value))
    '            cmd.Parameters.AddWithValue("@CostPrice", Convert.ToDecimal(row.Cells("CostPrice").Value))
    '            cmd.ExecuteNonQuery()
    '        End Using

    '        ' Update inventory
    '        Using cmd As New SqlCommand(updateInventoryQuery, con)
    '            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(row.Cells("Quantity").Value))
    '            cmd.Parameters.AddWithValue("@ProductID", row.Cells("ProductID").Value)
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    Next

    '    MessageBox.Show("Products added to delivery successfully!")

    Private Sub frmbrowsedelivery_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                With frmdeliverylist

                End With
                Me.Close()
        End Select
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    'Sub searchproduct(ByVal search As String)
    '        Try
    '            ' Exit if the search input is empty
    '            If String.IsNullOrWhiteSpace(search) Then
    '                MsgBox("Please enter a valid barcode.", vbExclamation)
    '                Return
    '            End If

    '            ' Use the connection in a Using block
    '            Using con As New SqlConnection(Dbsql.connString)
    '                con.Open()

    '                ' SQL query with parameterized input
    '                Dim query As String = "SELECT * FROM tblproduct WHERE barcode = @search"

    '                Using cmd As New SqlClient.SqlCommand(query, con)
    '                    cmd.Parameters.AddWithValue("@search", search)

    '                    Using dr As SqlClient.SqlDataReader = cmd.ExecuteReader()
    '                        ' Check if any rows are returned
    '                        If dr.Read() Then
    '                            ' Pass the retrieved values to frmqty
    '                            With frmqty
    '                                .lblprice.Text = If(dr("price") IsNot DBNull.Value, dr("price").ToString(), "N/A")
    '                                .lblPid.Text = If(dr("InventoryID") IsNot DBNull.Value, dr("InventoryID").ToString(), "N/A")
    '                                .ShowDialog()
    '                            End With
    '                        Else
    '                            MsgBox("No product found for the given barcode.", vbExclamation)
    '                        End If
    '                    End Using
    '                End Using
    '            End Using

    '        Catch ex As SqlException
    '            MsgBox("Database error: " & ex.Message, vbCritical)
    '        Catch ex As Exception
    '            MsgBox("Unexpected error: " & ex.Message, vbCritical)
    '        End Try
    '    End Sub


    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        FilterSearch()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub rcbdelivery_Click(sender As Object, e As EventArgs) Handles rcbdelivery.Click
        ReceiveSelectedProducts()
    End Sub

End Class