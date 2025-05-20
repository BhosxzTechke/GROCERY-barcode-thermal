Imports System.IO

Public Class frmaddproduct
    Dim imagePath As String ' To store selected photo path
    Public isEditing As Boolean = False ' Default is False (Not Editing)


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    'Sub clear()
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
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If Not IsNumeric(txtsales.Text) Or Not IsNumeric(txtcost.Text) Then
                MsgBox("please enter valid numeric values for cost price and price.", vbExclamation)
                Return
            End If

            If Walanglaman(txtbarcode) Then Return
            If Walanglaman(itemdes) Then Return
            'If Walanglaman(brandcbx) Then Return
            'If Walanglaman(gencbx) Then Return
            'If Walanglaman(categorycbx) Then Return
            'If Walanglaman(formucbx) Then Return
            'If Walanglaman(dosagecbx) Then Return
            If Walanglaman(txtsales) Then Return
            'If Walanglaman(unitcbx) Then Return

            '' Ensure barcode is provided
            'If String.IsNullOrWhiteSpace(txtbarcode.Text) Then
            '    txtbarcode.Text = GenerateUniqueBarcode() ' Generate a unique barcode if none is provided
            'End If

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
                Dim savePath As String = "C:\Users\danmi\OneDrive\Documents\Desktop\barcode product" ' Replace with your desired directory
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
            Dim query As String = "INSERT INTO tblproduct (barcode, item_des, cid, price, costprice, barcode_image, imagepath, uid) " &
                                  "VALUES (@barcode, @item_des, @cid, @price, @costprice, @barcode_image, @imagepath, @uid)"
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

                .Parameters.Add("@barcode_image", SqlDbType.VarBinary).Value = If(barcodeImageBytes IsNot Nothing, barcodeImageBytes, DBNull.Value)
                .Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = If(imageBytes IsNot Nothing, imageBytes, DBNull.Value)
                .Parameters.AddWithValue("@uid", If(String.IsNullOrWhiteSpace(unitid.Text), DBNull.Value, CInt(unitid.Text)))
            End With

            cmd.ExecuteNonQuery()
            MsgBox("Record has been successfully saved.", vbInformation)
            txtbarcode.Refresh()
            con.Close()

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


    Sub clear()
        If isEditing Then Return ' ✅ Prevent clearing when editing
        btnsave.Visible = True
        btnupdate.Visible = False
        txtbarcode.Focus()

        itemdes.Clear()
        txtbarcode.Clear()
        unitcbx.SelectedIndex = -1
        categorycbx.SelectedIndex = -1
        txtsales.Clear()
        txtcost.Clear()

        PictureBox1.Image = Nothing
    End Sub
    Private Sub frmaddproduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If isEditing Then Return ' ✅ Skip clearing fields when editing
        txtbarcode.Text = ""
        itemdes.Text = ""
        txtcost.Text = ""
        txtsales.Text = ""
        categorycbx.SelectedIndex = -1
        unitcbx.SelectedIndex = -1
        PictureBox1.Image = Nothing
    End Sub


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

            Dim cmd As New SqlClient.SqlCommand("SELECT catID, Category FROM tblcategory ORDER BY Category", con)
            Dim dt As New DataTable()
            Dim da As New SqlClient.SqlDataAdapter(cmd)
            da.Fill(dt)

            With categorycbx
                .DataSource = Nothing ' ✅ Reset first
                .Items.Clear()        ' ✅ Clear previous data
                .DataSource = dt
                .DisplayMember = "Category"
                .ValueMember = "catID"
            End With

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


    Private Sub unitcbx_SelectedIndexChanged(sender As Object, e As EventArgs) Handles unitcbx.SelectedIndexChanged
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
End Class