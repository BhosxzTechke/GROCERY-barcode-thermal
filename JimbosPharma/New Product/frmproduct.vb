Imports System.Data.SqlClient
Imports System.IO
Imports MessagingToolkit.Barcode
Imports MissionsGrocery.loginform


Public Class frmproduct
    Dim imagePath As String ' To store selected photo path
    Dim productForm As New frmaddproduct()
    ' ✅ Set flag to indicate editing
    Public isEditing As Boolean = False ' Default is False (Not Editing)



    Private Sub txtbarcode_TextChanged_1(sender As Object, e As EventArgs) Handles txtbarcode.TextChanged
        Try

            If String.IsNullOrWhiteSpace(txtbarcode.Text) Then
                picbarcode.Image = Nothing
                Return
            End If



            Dim encoder As New BarcodeEncoder()
            encoder.IncludeLabel = True
            encoder.CustomLabel = txtbarcode.Text


            Dim barcodeImage As Bitmap = encoder.Encode(BarcodeFormat.Code128, txtbarcode.Text)

            picbarcode.Image = barcodeImage
        Catch ex As Exception

            picbarcode.Image = Nothing
            MsgBox("Error generating barcode: " & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub txtsales_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsales.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnsave.PerformClick()
        End If
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Sub clear()
        btnsave.Visible = True
        btnupdate.Visible = False
        txtbarcode.Focus()

        itemdes.Clear()
        txtbarcode.Clear()
        unitcbx.SelectedIndex = -1
        categorycbx.SelectedIndex = -1
        txtsales.Clear()
        txtcost.Clear()
        txtwhole.Clear()

        PictureBox1.Image = Nothing

    End Sub




    Private Function ValidateDuplicateEntry(query As String) As Boolean
        Try
            cmd = New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@barcode", txtbarcode.Text.Trim()) ' 
            con.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            Return reader.HasRows ' Return true if the barcode exists
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical)
            Return False
        Finally
            con.Close()
        End Try
    End Function
    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try


            If Walanglaman(txtbarcode) Then Return
            If Walanglaman(itemdes) Then Return
            If Walanglaman(txtsales) Then Return
            If Walanglaman(unitcbx) Then Return
            If Walanglaman(txtcost) Then Return
            If Walanglaman(txtwhole) Then Return

            ' Optional regex-based validation (only letters, numbers, spaces)
            Dim pattern As String = "^[a-zA-Z0-9\s]+$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(itemdes.Text, pattern) Then
                MessageBox.Show("Invalid characters in category name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            Dim costPricing, retailPricing, wholePricing As Decimal

            ' Remove peso symbol and trim
            Dim costText As String = txtcost.Text.Replace("₱", "").Trim()
            Dim retailText As String = txtsales.Text.Replace("₱", "").Trim()
            Dim wholeText As String = txtwhole.Text.Replace("₱", "").Trim()

            ' Validate numeric input
            If Not Decimal.TryParse(costText, costPricing) Then
                txtcost.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Cost Price.")
                txtcost.Focus()
                Exit Sub
            Else
                txtcost.BackColor = Color.White
            End If

            If Not Decimal.TryParse(retailText, retailPricing) Then
                txtsales.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Retail Price.")
                txtsales.Focus()
                Exit Sub
            Else
                txtsales.BackColor = Color.White
            End If

            If Not Decimal.TryParse(wholeText, wholePricing) Then
                txtwhole.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Whole Price.")
                txtwhole.Focus()
                Exit Sub
            Else
                txtwhole.BackColor = Color.White
            End If

            ' Check pricing rules
            Dim isValid As Boolean = True

            If retailPricing <= costPricing Then
                txtsales.BackColor = Color.MistyRose
                isValid = False
            End If

            If wholePricing <= costPricing Then
                txtwhole.BackColor = Color.MistyRose
                isValid = False
            End If

            ' New Rule: Whole price must not exceed Retail Price
            If wholePricing > retailPricing Then
                txtwhole.BackColor = Color.MistyRose
                MessageBox.Show("Whole Price must not be greater than Retail Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtwhole.Focus()
                Exit Sub
            End If

            If Not isValid Then
                MessageBox.Show("Retail and Whole Price must be greater than the Cost Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            ' Confirm Update Action using Guna2MessageDialog
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
            Guna2MessageDialog1.Caption = "Confirm Update"
            Guna2MessageDialog1.Text = "Are you sure you want to update this product?"

            Dim result As DialogResult = Guna2MessageDialog1.Show()
            If result = DialogResult.No Then Return

            ' Check if sales and cost values are numeric
            If Not IsNumeric(txtsales.Text) Or Not IsNumeric(txtcost.Text) Then
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Invalid Input"
                Guna2MessageDialog1.Text = "Please enter valid numeric values for cost price and sales price."
                Guna2MessageDialog1.Show()
                Return
            End If

            ' Check if required fields are not empty
            If Walanglaman(txtbarcode) OrElse Walanglaman(itemdes) OrElse Walanglaman(txtsales) Then Return

            ' Validate barcode length (must be 13 digits)
            If txtbarcode.Text.Length <> 13 OrElse Not IsNumeric(txtbarcode.Text) Then
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Invalid Barcode"
                Guna2MessageDialog1.Text = "Barcode must be a 13-digit numeric value."
                Guna2MessageDialog1.Show()
                Return
            End If

            ' Process barcode image if checkbox is checked
            Dim barcodeImageBytes As Byte() = Nothing
            If ckbarcode.Checked Then
                Dim Generator As New MessagingToolkit.Barcode.BarcodeEncoder
                Dim barcodeImage As Bitmap = New Bitmap(Generator.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, txtbarcode.Text))

                ' Save barcode image
                Dim savePath As String = "C:\Barcode_Image"
                If Not Directory.Exists(savePath) Then Directory.CreateDirectory(savePath)
                Dim barcodeImagePath As String = Path.Combine(savePath, txtbarcode.Text & ".png")
                barcodeImage.Save(barcodeImagePath, System.Drawing.Imaging.ImageFormat.Png)

                ' Convert barcode image to byte array
                Using ms As New MemoryStream()
                    barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    barcodeImageBytes = ms.ToArray()
                End Using
            End If

            ' Convert product image to byte array (if provided)
            Dim imageBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(imagePath) Then
                imageBytes = File.ReadAllBytes(imagePath)
            End If


            ' AUDIT: Save new values
            Dim productID As String = lblid.Text
            Dim newvalues As String = "Barcode: " & txtbarcode.Text & ", " &
                                  "Item: " & itemdes.Text & ", " &
                                  "Price: " & txtsales.Text & ", " &
                                  "Cost: " & txtcost.Text & ", " &
                                  "Whole: " & txtwhole.Text
            Dim userid As Integer = GlobalUser.UserId
            logoaudit(userid, "Update", "tblproduct", productID, "", newvalues)


            ' Update product record in the database
            Using cmd As New SqlClient.SqlCommand("UPDATE tblproduct SET barcode=@barcode, item_des=@item_des, price=@price, costprice=@costprice, WholePrice=@WholePrice, barcode_image=@barcode_image, imagepath=@imagepath, cid=@cid, uid=@uid WHERE id=@id", con)
                cmd.Parameters.AddWithValue("@barcode", txtbarcode.Text.Trim())
                cmd.Parameters.AddWithValue("@item_des", itemdes.Text.Trim())
                cmd.Parameters.AddWithValue("@uid", If(String.IsNullOrWhiteSpace(unitid.Text) OrElse Not IsNumeric(unitid.Text), DBNull.Value, CInt(unitid.Text)))
                cmd.Parameters.AddWithValue("@cid", If(String.IsNullOrWhiteSpace(lblclass.Text) OrElse Not IsNumeric(lblclass.Text), DBNull.Value, CInt(lblclass.Text)))

                ' Convert sale price and cost price to Decimal
                Dim salePrice As Decimal
                If Decimal.TryParse(txtsales.Text.Replace("₱", "").Replace(",", "").Trim(), salePrice) Then
                    cmd.Parameters.AddWithValue("@price", salePrice)
                Else
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Input Error"
                    Guna2MessageDialog1.Text = "Invalid sale price value. Please enter a valid number."
                    Guna2MessageDialog1.Show()
                    con.Close()
                    Exit Sub
                End If

                Dim costPrice As Decimal
                If Decimal.TryParse(txtcost.Text.Replace("₱", "").Replace(",", "").Trim(), costPrice) Then
                    cmd.Parameters.AddWithValue("@costprice", costPrice)
                Else
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Input Error"
                    Guna2MessageDialog1.Text = "Invalid cost price value. Please enter a valid number."
                    Guna2MessageDialog1.Show()
                    con.Close()
                    Exit Sub
                End If

                Dim WholePrice As Decimal
                If Decimal.TryParse(txtwhole.Text.Replace("₱", "").Replace(",", "").Trim(), WholePrice) Then
                    cmd.Parameters.AddWithValue("@WholePrice", WholePrice)
                Else
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Input Error"
                    Guna2MessageDialog1.Text = "Invalid Whole price value. Please enter a valid number."
                    Guna2MessageDialog1.Show()
                    con.Close()
                    Exit Sub
                End If




                cmd.Parameters.Add("@barcode_image", SqlDbType.VarBinary).Value = If(barcodeImageBytes IsNot Nothing, barcodeImageBytes, DBNull.Value)
                cmd.Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = If(imageBytes IsNot Nothing, imageBytes, DBNull.Value)
                cmd.Parameters.AddWithValue("@id", If(IsNumeric(lblid.Text), CInt(lblid.Text), DBNull.Value))
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using

            ' Success Message
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
            Guna2MessageDialog1.Caption = "Update Successful"
            Guna2MessageDialog1.Text = "Product has been successfully updated."

            Guna2MessageDialog1.Show()
            Me.Close()
            clear()
            frmproductlists.prodrecord()
        Catch ex As Exception
            ShowError("An error occurred: " & ex.Message)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub ShowError(message As String)
        Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
        Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
        Guna2MessageDialog1.Caption = "Error"
        Guna2MessageDialog1.Text = message
        Guna2MessageDialog1.Show()
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        openFileDialog.Title = "Select a Photo"

        If openFileDialog.ShowDialog = DialogResult.OK Then
            imagePath = openFileDialog.FileName

            PictureBox1.Image = Image.FromFile(imagePath)
        End If
    End Sub




    '                                                   FOR RETRIEVING IMAGE IN UPDATE
    Public Sub SetImageData(ByVal imageData As Byte())
        If imageData IsNot Nothing AndAlso imageData.Length > 0 Then
            Try
                Using ms As New MemoryStream(imageData)
                    PictureBox1.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message)
                PictureBox1.Image = Nothing
            End Try
        Else

            PictureBox1.Image = Nothing
        End If
    End Sub




    'Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
    '    Try
    '        If MsgBox("Are you sure you want to update this Record?", vbYesNo + vbQuestion) = vbYes Then
    '            Dim imageBytes As Byte() = Nothing
    '            If Not String.IsNullOrEmpty(imagePath) Then
    '                imageBytes = File.ReadAllBytes(imagePath)
    '            End If



    '            Dim brandId, genericId, classId, formulId, dosId, unitIds As Integer

    '            If Not Integer.TryParse(lblbrand.Text, brandId) Then
    '                MsgBox("Brand ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            If Not Integer.TryParse(lblgeneric.Text, genericId) Then
    '                MsgBox("Generic ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            If Not Integer.TryParse(lblclass.Text, classId) Then
    '                MsgBox("Category ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            If Not Integer.TryParse(lblformul.Text, formulId) Then
    '                MsgBox("Formulation ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            If Not Integer.TryParse(lbldos.Text, dosId) Then
    '                MsgBox("Dosage ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            If Not Integer.TryParse(unitid.Text, unitIds) Then
    '                MsgBox("Unit ID is not a valid number.", vbExclamation)
    '                Exit Sub
    '            End If

    '            con.Open()
    '            cmd = New SqlClient.SqlCommand("UPDATE tblproduct SET barcode=@barcode, item_des=@item_des, bid=@bid, gid=@gid, cid=@cid, fid=@fid, did=@did, price=@price, imagepath=@imagepath, uid=@uid WHERE id=@id", con)

    '            With cmd
    '                .Parameters.AddWithValue("@barcode", txtbarcode.Text)
    '                .Parameters.AddWithValue("@item_des", itemdes.Text)
    '                .Parameters.AddWithValue("@bid", brandId)
    '                .Parameters.AddWithValue("@gid", genericId)
    '                .Parameters.AddWithValue("@cid", classId)
    '                .Parameters.AddWithValue("@fid", formulId)
    '                .Parameters.AddWithValue("@did", dosId)
    '                .Parameters.AddWithValue("@uid", unitIds)
    '                .Parameters.AddWithValue("@price", CDbl(txtprice.Text))
    '                .Parameters.AddWithValue("@id", CInt(lblid.Text))

    '                If imageBytes IsNot Nothing Then
    '                    .Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = imageBytes
    '                Else
    '                    .Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = DBNull.Value
    '                End If

    '                .ExecuteNonQuery()
    '            End With

    '            MsgBox("Product has been successfully updated.", vbInformation)
    '            con.Close()
    '            clear()

    '            With frmproductlist
    '                .prodrecord() ' for REFRESH
    '            End With
    '        End If

    '    Catch ex As Exception
    '        If con.State = ConnectionState.Open Then
    '            con.Close() ' Only close the connection if it is open
    '        End If
    '        MsgBox(ex.Message, vbCritical)
    '    End Try
    'End Sub



    'Private Sub txtqty_KeyDown(sender As Object, e As KeyEventArgs)
    '    If e.KeyCode = Keys.Enter Then
    '        e.SuppressKeyPress = True
    '        btnsave.PerformClick()
    '    End If
    'End Sub




    Private Sub SearchProductByBarcode(barcode As String)
        Dim query As String = "SELECT * FROM tblproduct WHERE barcode = @barcode"

        Using cmd As New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@barcode", barcode)
            con.Open()

            Using reader As SqlClient.SqlDataReader = cmd.ExecuteReader()
                If reader.HasRows Then
                    While reader.Read()
                        ' KAPAG MERON NANG BARCODE SCANNER
                        itemdes.Text = reader("item_des").ToString()
                        txtsales.Text = reader("price").ToString()
                    End While
                Else
                    MsgBox("Product not found.", vbExclamation)
                End If
            End Using

            con.Close()
        End Using
    End Sub


    Private Function GenerateUniqueBarcode() As String
        Dim timestamp As String = DateTime.Now.ToString("yyyyMMddHHmmssfff")
        Dim random As New Random()

        ' Generate a 12-digit random number as a string
        Dim randomPart As String = ""
        For i As Integer = 1 To 12
            randomPart &= random.Next(0, 10).ToString()  ' Append a random digit (0-9)
        Next

        ' Calculate the check digit for EAN-13
        Dim checkDigit As Integer = CalculateEAN13CheckDigit(randomPart)

        ' Combine the random 12 digits with the check digit
        Dim uniqueBarcode As String = randomPart & checkDigit.ToString()

        Return uniqueBarcode
    End Function



    ' Function to calculate the check digit for EAN-13 barcode
    Private Function CalculateEAN13CheckDigit(barcodeWithoutCheckDigit As String) As Integer
        Dim sum As Integer = 0
        For i As Integer = 0 To barcodeWithoutCheckDigit.Length - 1
            Dim digit As Integer = Convert.ToInt32(barcodeWithoutCheckDigit(i).ToString())
            ' Odd positions (1, 3, 5, 7, 9, 11) are multiplied by 1, even positions by 3
            If i Mod 2 = 0 Then
                sum += digit
            Else
                sum += digit * 3
            End If
        Next

        ' Calculate the modulo 10 of the sum
        Dim mod10 As Integer = sum Mod 10

        ' If the modulo is 0, the check digit is 0, otherwise, the check digit is 10 - mod10
        If mod10 = 0 Then
            Return 0
        Else
            Return 10 - mod10
        End If
    End Function












    '                                           retrieving the id when we selecte in index


    'Private Sub categorycbx_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    If categorycbx.SelectedIndex <> -1 Then
    '        Try
    '            con.Open()
    '            cmd = New SqlClient.SqlCommand("Select * from tblcategory where Category = @Category", con)
    '            cmd.Parameters.AddWithValue("@Category", categorycbx.SelectedItem.ToString())
    '            dr = cmd.ExecuteReader
    '            If dr.Read() Then
    '                lblclass.Text = dr("catID").ToString()
    '            Else
    '                lblclass.Text = String.Empty
    '            End If
    '        Catch ex As Exception
    '            ' Handle the exception, if needed
    '        Finally
    '            If dr IsNot Nothing Then dr.Close()
    '            If cmd IsNot Nothing Then cmd.Dispose() ' Dispose the command to release resources
    '            If con.State = ConnectionState.Open Then con.Close() ' Close the connection
    '        End Try
    '    End If
    'End Sub








    '    -------------- ----- ----- ----- -----        COMBOBOX   POPULATE USING DATASOURCE BECAUSE ITS AN ID




    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' ✅ Load Categories and Units when Form Opens
        LoadCategory()
        LoadUnits()
    End Sub

    Public Sub LoadCategory()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Using cmd As New SqlClient.SqlCommand("SELECT catID, Category FROM tblcategory ORDER BY Category", con)
                Using da As New SqlClient.SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        With categorycbx
                            .DataSource = Nothing
                            .DataSource = dt
                            .DisplayMember = "Category"
                            .ValueMember = "catID"
                        End With
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub


    Public Sub LoadUnits()
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim cmd As New SqlClient.SqlCommand("SELECT unitID, unit FROM tblunit ORDER BY unit", con)
            Dim dt As New DataTable()
            Dim da As New SqlClient.SqlDataAdapter(cmd)
            da.Fill(dt)

            With unitcbx
                .DataSource = Nothing ' ✅ Reset first
                .Items.Clear()        ' ✅ Clear previous data
                .DataSource = dt
                .DisplayMember = "unit"
                .ValueMember = "unitID"
            End With

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub



    Private Sub categorycbx_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles categorycbx.SelectedIndexChanged
        Try
            If categorycbx.SelectedIndex >= 0 Then
                lblclass.Text = categorycbx.SelectedValue.ToString()
            Else
                lblclass.Text = String.Empty
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try



    End Sub


    Public Sub unitcbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles unitcbx.SelectedIndexChanged
        Try
            If unitcbx.SelectedIndex >= 0 Then
                unitid.Text = unitcbx.SelectedValue.ToString()
            Else
                unitid.Text = String.Empty
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    '---------------------------------------   END COMBOBOX



    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs)
        Me.Close()
    End Sub




    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub






    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If String.IsNullOrWhiteSpace(txtbarcode.Text) Then
            txtbarcode.Text = GenerateUniqueBarcode()
        Else
            MsgBox("Barcode already exists in the TextBox.", vbInformation)
        End If
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs)

        Me.Close()

    End Sub





    Private Sub ValidatePrices()
        ' Remove peso symbols and trim spaces
        Dim costText = txtcost.Text.Replace("₱", "").Trim()
        Dim retailText = txtsales.Text.Replace("₱", "").Trim()
        Dim wholeText = txtwhole.Text.Replace("₱", "").Trim()

        Dim cost, retail, whole As Decimal
        Dim costValid = Decimal.TryParse(costText, cost)
        Dim retailValid = Decimal.TryParse(retailText, retail)
        Dim wholeValid = Decimal.TryParse(wholeText, whole)

        ' Cost Price
        If costValid Then
            txtcost.ForeColor = Color.Green
        Else
            txtcost.ForeColor = Color.Red
            txtcost.Clear()
        End If

        ' Retail Price
        If retailValid AndAlso retail > cost Then
            txtsales.ForeColor = Color.Green
        Else
            txtsales.ForeColor = Color.Red
            If retailText <> "" Then txtsales.Clear()
        End If

        ' Whole Price
        If wholeValid AndAlso whole > cost AndAlso whole <= retail Then
            txtwhole.ForeColor = Color.Green
        Else
            txtwhole.ForeColor = Color.Red
            If wholeText <> "" Then txtwhole.Clear()
        End If
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click


        Try



            If Walanglaman(txtbarcode) Then Return
            If Walanglaman(itemdes) Then Return
            If Walanglaman(txtsales) Then Return
            If Walanglaman(unitcbx) Then Return
            If Walanglaman(txtcost) Then Return
            If Walanglaman(txtwhole) Then Return



            If Not IsNumeric(txtsales.Text) Or Not IsNumeric(txtcost.Text) Then
                MsgBox("please enter valid numeric values for cost price and price.", vbExclamation)
                Return
            End If


            Dim costPricing, retailPricing, wholePricing As Decimal

            ' Remove peso symbol and trim
            Dim costText As String = txtcost.Text.Replace("₱", "").Trim()
            Dim retailText As String = txtsales.Text.Replace("₱", "").Trim()
            Dim wholeText As String = txtwhole.Text.Replace("₱", "").Trim()

            ' Validate numeric input
            If Not Decimal.TryParse(costText, costPricing) Then
                txtcost.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Cost Price.")
                txtcost.Focus()
                Exit Sub
            Else
                txtcost.BackColor = Color.White
            End If

            If Not Decimal.TryParse(retailText, retailPricing) Then
                txtsales.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Retail Price.")
                txtsales.Focus()
                Exit Sub
            Else
                txtsales.BackColor = Color.White
            End If

            If Not Decimal.TryParse(wholeText, wholePricing) Then
                txtwhole.BackColor = Color.MistyRose
                MessageBox.Show("Please enter a valid Whole Price.")
                txtwhole.Focus()
                Exit Sub
            Else
                txtwhole.BackColor = Color.White
            End If

            ' Check pricing rules
            Dim isValid As Boolean = True

            If retailPricing <= costPricing Then
                txtsales.BackColor = Color.MistyRose
                isValid = False
            End If

            If wholePricing <= costPricing Then
                txtwhole.BackColor = Color.MistyRose
                isValid = False
            End If

            ' New Rule: Whole price must not exceed Retail Price
            If wholePricing > retailPricing Then
                txtwhole.BackColor = Color.MistyRose
                MessageBox.Show("Whole Price must not be greater than Retail Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtwhole.Focus()
                Exit Sub
            End If

            If Not isValid Then
                MessageBox.Show("Retail and Whole Price must be greater than the Cost Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            '' Ensure barcode is provided
            'If String.IsNullOrWhiteSpace(txtbarcode.Text) Then
            '    txtbarcode.Text = GenerateUniqueBarcode() ' Generate a unique barcode if none is provided
            'End If


            ' Optional regex-based validation (only letters, numbers, spaces)
            Dim pattern As String = "^[a-zA-Z0-9\s]+$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(itemdes.Text, pattern) Then
                MessageBox.Show("Invalid characters in category name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            ' Validate barcode format
            If txtbarcode.Text.Length <> 13 OrElse Not IsNumeric(txtbarcode.Text) Then
                MsgBox("Barcode must be a 13-digit numeric value.", vbExclamation)
                Return
            End If

            ' Validate for duplicate barcode entries
            If ValidateDuplicateEntry("SELECT * FROM tblproduct WHERE barcode = @barcode") Then
                MsgBox("A product with this barcode already exists.", vbExclamation)
                Return
            End If

            ' Confirm save action
            If MsgBox("Are you sure you want to save this Product?", vbYesNo + vbQuestion) = MsgBoxResult.No Then Return




            ' Process barcode image only if the checkbox is checked
            Dim barcodeImageBytes As Byte() = Nothing
            If ckbarcode.Checked Then
                ' Generate and save the barcode image
                Dim Generator As New MessagingToolkit.Barcode.BarcodeEncoder
                Dim barcodeImage As Bitmap = New Bitmap(Generator.Encode(MessagingToolkit.Barcode.BarcodeFormat.Code128, txtbarcode.Text))




                ' Save barcode image to a specific path
                Dim savePath As String = "C:\Barcode_Image" ' Replace with your desired directory
                If Not Directory.Exists(savePath) Then
                    Directory.CreateDirectory(savePath) ' Create the directory if it doesn't exist
                End If
                Dim barcodeImagePath As String = Path.Combine(savePath, txtbarcode.Text & ".png")
                barcodeImage.Save(barcodeImagePath, System.Drawing.Imaging.ImageFormat.Png)



                ' Convert the barcode image to byte array for database storage
                Using ms As New MemoryStream()
                    barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
                    barcodeImageBytes = ms.ToArray()
                End Using
            End If



            ' Convert product image to byte array, or set to DBNull if imagePath is empty
            Dim imageBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(imagePath) Then
                imageBytes = File.ReadAllBytes(imagePath)
            End If

            ' Insert data into the database
            con.Open()
            Dim query As String = "INSERT INTO tblproduct (barcode, item_des, cid, price, costprice, WholePrice, barcode_image, imagepath, uid) " &
                                  "OUTPUT INSERTED.id " &
                                  "VALUES (@barcode, @item_des, @cid, @price, @costprice, @WholePrice, @barcode_image,  @imagepath, @uid)"
            cmd = New SqlClient.SqlCommand(query, con)

            ' Set parameter values
            With cmd
                .Parameters.AddWithValue("@barcode", txtbarcode.Text.Trim())
                .Parameters.AddWithValue("@item_des", itemdes.Text.Trim())
                .Parameters.AddWithValue("@cid", If(String.IsNullOrWhiteSpace(lblclass.Text), DBNull.Value, CInt(lblclass.Text)))




                ' Convert sale price and cost price to Decimal, removing any formatting
                Dim salePrice As Decimal
                If Decimal.TryParse(txtsales.Text.Replace("₱", "").Replace(",", "").Trim(), salePrice) Then
                    .Parameters.AddWithValue("@price", salePrice)
                Else
                    MessageBox.Show("Invalid sale price value. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                    Exit Sub
                End If

                Dim costPrice As Decimal
                If Decimal.TryParse(txtcost.Text.Replace("₱", "").Replace(",", "").Trim(), costPrice) Then
                    .Parameters.AddWithValue("@costprice", costPrice)
                Else
                    MessageBox.Show("Invalid cost price value. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                    Exit Sub
                End If


                Dim wholeprice As Decimal
                If Decimal.TryParse(txtwhole.Text.Replace("₱", "").Replace(",", "").Trim(), wholeprice) Then
                    .Parameters.AddWithValue("@WholePrice", wholeprice)
                Else
                    MessageBox.Show("Invalid cost price value. Please enter a valid number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    con.Close()
                    Exit Sub
                End If


                .Parameters.Add("@barcode_image", SqlDbType.VarBinary).Value = If(barcodeImageBytes IsNot Nothing, barcodeImageBytes, DBNull.Value)
                .Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = If(imageBytes IsNot Nothing, imageBytes, DBNull.Value)
                .Parameters.AddWithValue("@uid", If(String.IsNullOrWhiteSpace(unitid.Text), DBNull.Value, CInt(unitid.Text)))
            End With



            Dim newproductID As Integer = CInt(cmd.ExecuteScalar())

            con.Close()

            Dim userid As Integer = GlobalUser.UserId

            Dim newvalues As String = "Barcode: " & txtbarcode.Text & ", " &
                                      "Item: " & itemdes.Text & ", " &
                                      "Price: " & txtsales.ToString() & ", " &
                                      "Cost: " & txtcost.ToString()
            logoaudit(userid, "INSERT", "Table Product", newproductID.ToString(), "", newvalues)


            'cmd.ExecuteNonQuery()
            MsgBox("Record has been successfully saved.", vbInformation)
            txtbarcode.Refresh()
            Me.Close()


            clear()
            With frmproductlists
                .prodrecord()
            End With
            'Me.Close()




        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical)
            con.Close()
        End Try
    End Sub





    'Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox1.SelectedIndexChanged
    '    Try
    '        ' Ensure ComboBox has a valid selection
    '        If Guna2ComboBox1.SelectedIndex >= 0 Then
    '            ' Using SelectedValue instead of SelectedItem for better reliability
    '            lblclass.Text = Guna2ComboBox1.SelectedValue.ToString()
    '        Else
    '            lblclass.Text = String.Empty
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    'Private Sub boxcat_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        ' Ensure ComboBox has a valid selection
    '        If boxcat.SelectedIndex >= 0 Then
    '            ' Try casting the selected item as a DataRowView
    '            Dim selectedRow As DataRowView = TryCast(boxcat.SelectedItem, DataRowView)
    '            If selectedRow IsNot Nothing Then
    '                lblclass.Text = selectedRow("catID").ToString()
    '            Else
    '                lblclass.Text = String.Empty
    '            End If
    '        Else
    '            lblclass.Text = String.Empty
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub



    'Private Sub frmproduct_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    txtbarcode.Text = ""
    '    itemdes.Text = ""
    '    txtcost.Text = ""
    '    txtsales.Text = ""
    '    categorycbx.SelectedIndex = -1
    '    unitcbx.SelectedIndex = -1
    '    PictureBox1.Image = Nothing
    'End Sub


    'Sub cleartext()
    '    If isEditing Then Return ' ✅ Prevent clearing when editing
    '    btnsave.Visible = True
    '    btnupdate.Visible = False
    '    txtbarcode.Focus()

    '    itemdes.Clear()
    '    txtbarcode.Clear()
    '    unitcbx.SelectedIndex = -1
    '    categorycbx.SelectedIndex = -1
    '    txtsales.Clear()
    '    txtcost.Clear()

    '    PictureBox1.Image = Nothing
    'End Sub


    Private Sub frmproduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isEditing Then Return ' ✅ Skip clearing fields when editing
        txtbarcode.Text = ""
        itemdes.Text = ""
        txtcost.Text = ""
        txtsales.Text = ""
        txtwhole.Text = ""

        categorycbx.SelectedIndex = -1
        unitcbx.SelectedIndex = -1
        PictureBox1.Image = Nothing


        'txtbarcode.Text = GenerateUniqueBarcode() ' Generate a unique barcode if none is provided


        If String.IsNullOrWhiteSpace(txtcost.Text) Then
            txtcost.Text = "₱"
            txtcost.SelectionStart = txtcost.Text.Length ' Position cursor at the end
        End If

        If String.IsNullOrWhiteSpace(txtsales.Text) Then
            txtsales.Text = "₱"
            txtsales.SelectionStart = txtsales.Text.Length ' Position cursor at the end
        End If



        If String.IsNullOrWhiteSpace(txtwhole.Text) Then
            txtwhole.Text = "₱"
            txtwhole.SelectionStart = txtwhole.Text.Length ' Position cursor at the end
        End If


        ' max length of price

        txtsales.MaxLength = 7
        txtwhole.MaxLength = 7
        txtcost.MaxLength = 7





    End Sub

    Private Sub Guna2Button2_Click_2(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub itemdes_TextChanged(sender As Object, e As EventArgs) Handles itemdes.TextChanged

    End Sub




    '                       FOR Allow only Digits
    Private Sub txtwhole_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtwhole.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txtsales.Text.Contains(".") Then
            e.Handled = True
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


    Private Sub txtsales_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsales.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "." AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

        ' Allow only one decimal point
        If e.KeyChar = "." AndAlso txtsales.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtwhole_TextChanged(sender As Object, e As EventArgs) Handles txtwhole.TextChanged

        If Not txtwhole.Text.StartsWith("₱") Then
            ' Remove all ₱ and prepend one
            txtwhole.Text = "₱" & txtwhole.Text.Replace("₱", "")
            txtwhole.SelectionStart = txtwhole.Text.Length ' Move cursor to end
        End If

    End Sub

    Private Sub txtcost_TextChanged(sender As Object, e As EventArgs) Handles txtcost.TextChanged

        If Not txtcost.Text.StartsWith("₱") Then
            ' Remove all ₱ and prepend one
            txtcost.Text = "₱" & txtcost.Text.Replace("₱", "")
            txtcost.SelectionStart = txtcost.Text.Length ' Move cursor to end
        End If



    End Sub

    Private Sub txtsales_TextChanged(sender As Object, e As EventArgs) Handles txtsales.TextChanged


        If Not txtsales.Text.StartsWith("₱") Then
            ' Remove all ₱ and prepend one
            txtsales.Text = "₱" & txtsales.Text.Replace("₱", "")
            txtsales.SelectionStart = txtsales.Text.Length ' Move cursor to end
        End If

    End Sub

    Private Sub txtbarcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbarcode.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

    End Sub

    Private Sub itemdes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles itemdes.KeyPress

    End Sub

    Private Sub itemdes_KeyDown(sender As Object, e As KeyEventArgs) Handles itemdes.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If


    End Sub

    Private Sub txtcost_Leave(sender As Object, e As EventArgs) Handles txtcost.Leave
        ValidatePrices()
    End Sub

    Private Sub txtsales_Leave(sender As Object, e As EventArgs) Handles txtsales.Leave
        ValidatePrices()
    End Sub

    Private Sub txtwhole_Leave(sender As Object, e As EventArgs) Handles txtwhole.Leave
        ValidatePrices()
    End Sub
End Class






'Private Sub txtbarcode_Leave(sender As Object, e As EventArgs) Handles txtbarcode.Leave

'    If String.IsNullOrWhiteSpace(txtbarcode.Text) Then

'        txtbarcode.BackColor = Color.LightCoral
'        MsgBox("Barcode field cannot be empty!", MsgBoxStyle.Exclamation)
'    Else

'        txtbarcode.BackColor = Color.White
'    End If
'End Sub










''                                                                                TEXT CHANGE IF WE TRACK IN QUERY

''Private Sub txtbrand_TextChanged(sender As Object, e As EventArgs) Handles txtbrand.TextChanged
''    If Not String.IsNullOrEmpty(txtbrand.Text) Then
''        Try
''            con.Open()
''            cmd = New SqlClient.SqlCommand("SELECT brandID FROM tblbrand WHERE brand = @brand", con)
''            cmd.Parameters.AddWithValue("@brand", txtbrand.Text)

''            dr = cmd.ExecuteReader()
''            If dr.Read() Then
''                lblbrand.Text = dr("brandID").ToString()
''            Else
''                lblbrand.Text = String.Empty ' Clear label if no match is found
''            End If
''        Catch ex As Exception
''            MessageBox.Show("Error retrieving brand ID: " & ex.Message)
''        Finally
''            If dr IsNot Nothing Then dr.Close()
''            If cmd IsNot Nothing Then cmd.Dispose()
''            If con.State = ConnectionState.Open Then con.Close()
''        End Try
''    Else
''        lblbrand.Text = String.Empty ' Clear label if text is empty
''    End If
''End Sub


'Private Sub txtgeneric_TextChanged(sender As Object, e As EventArgs) Handles txtgeneric.TextChanged

'    If Not String.IsNullOrEmpty(txtgeneric.Text) Then
'        Try
'            con.Open()
'            cmd = New SqlClient.SqlCommand("SELECT genericID FROM tblgeneric WHERE generic = @generic", con)
'            cmd.Parameters.AddWithValue("@generic", txtgeneric.Text)

'            dr = cmd.ExecuteReader()
'            If dr.Read() Then
'                lblgeneric.Text = dr("genericID").ToString()
'            Else
'                lblgeneric.Text = String.Empty ' Clear label if no match is found
'            End If
'        Catch ex As Exception
'            MessageBox.Show("Error retrieving generic ID: " & ex.Message)
'        Finally
'            If dr IsNot Nothing Then dr.Close()
'            If cmd IsNot Nothing Then cmd.Dispose()
'            If con.State = ConnectionState.Open Then con.Close()
'        End Try
'    Else
'        lblgeneric.Text = String.Empty ' Clear label if text is empty
'    End If
'End Sub




'Private Sub txtdosage_TextChanged(sender As Object, e As EventArgs) Handles txtdosage.TextChanged

'    If Not String.IsNullOrEmpty(txtdosage.Text) Then
'        Try
'            con.Open()
'            cmd = New SqlClient.SqlCommand("SELECT dosageID FROM tbldosage WHERE Dosage = @Dosage", con)
'            cmd.Parameters.AddWithValue("@Dosage", txtdosage.Text)

'            dr = cmd.ExecuteReader()
'            If dr.Read() Then
'                lbldos.Text = dr("dosageID").ToString()
'            Else
'                lbldos.Text = String.Empty ' Clear label if no match is found
'            End If
'        Catch ex As Exception
'            MessageBox.Show("Error retrieving Dosage ID: " & ex.Message)
'        Finally
'            If dr IsNot Nothing Then dr.Close()
'            If cmd IsNot Nothing Then cmd.Dispose()
'            If con.State = ConnectionState.Open Then con.Close()
'        End Try
'    Else
'        lbldos.Text = String.Empty ' Clear label if text is empty
'    End If
'End Sub


'Private Sub txtcategory_TextChanged(sender As Object, e As EventArgs) Handles txtcategory.TextChanged

'    If Not String.IsNullOrEmpty(txtcategory.Text) Then
'        Try
'            con.Open()
'            cmd = New SqlClient.SqlCommand("SELECT catID FROM tblcategory WHERE Category = @Category", con)
'            cmd.Parameters.AddWithValue("@Category", txtcategory.Text)

'            dr = cmd.ExecuteReader()
'            If dr.Read() Then
'                lblclass.Text = dr("catID").ToString()
'            Else
'                lblclass.Text = String.Empty ' Clear label if no match is found
'            End If
'        Catch ex As Exception
'            MessageBox.Show("Error retrieving Category ID: " & ex.Message)
'        Finally
'            If dr IsNot Nothing Then dr.Close()
'            If cmd IsNot Nothing Then cmd.Dispose()
'            If con.State = ConnectionState.Open Then con.Close()
'        End Try
'    Else
'        lblclass.Text = String.Empty ' Clear label if text is empty
'    End If


'End Sub



'Private Sub txtformu_TextChanged(sender As Object, e As EventArgs) Handles txtformu.TextChanged

'    If Not String.IsNullOrEmpty(txtformu.Text) Then
'        Try
'            con.Open()
'            cmd = New SqlClient.SqlCommand("SELECT formID FROM tblformulations WHERE Formulations = @Formulations", con)
'            cmd.Parameters.AddWithValue("@Formulations", txtformu.Text)

'            dr = cmd.ExecuteReader()
'            If dr.Read() Then
'                lblformul.Text = dr("formID").ToString()
'            Else
'                lblformul.Text = String.Empty ' Clear label if no match is found
'            End If
'        Catch ex As Exception
'            MessageBox.Show("Error retrieving form ID: " & ex.Message)
'        Finally
'            If dr IsNot Nothing Then dr.Close()
'            If cmd IsNot Nothing Then cmd.Dispose()
'            If con.State = ConnectionState.Open Then con.Close()
'        End Try
'    Else
'        lblformul.Text = String.Empty ' Clear label if text is empty
'    End If
'End Sub


'Private Sub txtunit_TextChanged(sender As Object, e As EventArgs) Handles txtunit.TextChanged

'    If Not String.IsNullOrEmpty(txtunit.Text) Then
'        Try
'            con.Open()
'            cmd = New SqlClient.SqlCommand("SELECT unitID FROM tblunit WHERE unit = @unit", con)
'            cmd.Parameters.AddWithValue("@unit", txtunit.Text)

'            dr = cmd.ExecuteReader()
'            If dr.Read() Then
'                unitid.Text = dr("unitID").ToString()
'            Else
'                unitid.Text = String.Empty ' Clear label if no match is found
'            End If
'        Catch ex As Exception
'            MessageBox.Show("Error retrieving unit ID: " & ex.Message)
'        Finally
'            If dr IsNot Nothing Then dr.Close()
'            If cmd IsNot Nothing Then cmd.Dispose()
'            If con.State = ConnectionState.Open Then con.Close()
'        End Try
'    Else
'        unitid.Text = String.Empty ' Clear label if text is empty
'    End If
'End Sub
