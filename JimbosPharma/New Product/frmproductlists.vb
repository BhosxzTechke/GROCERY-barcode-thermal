Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports MissionsGrocery.loginform




Public Class frmproductlists

    Private Sub frmproductlists_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        prodrecord()
        dataprod.AlternatingRowsDefaultCellStyle = dataprod.RowsDefaultCellStyle


    End Sub

    'dropdownunit(frmproduct) ' ✅ Load the dropdown first

    Private Sub dataprod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataprod.CellContentClick
        If e.RowIndex < 0 Then Return

        Dim colname As String = dataprod.Columns(e.ColumnIndex).Name

        If colname = "Edit" Then
            ' ✅ Close existing instance if open
            For Each frm As Form In Application.OpenForms
                If TypeOf frm Is frmproduct Then
                    frm.Close()
                    Exit For
                End If
            Next

            ' ✅ Open new instance of frmproduct
            Dim productForm As New frmproduct()

            ' ✅ Set Editing Mode
            productForm.isEditing = True

            ' ✅ Ensure ComboBoxes have DataSource before setting SelectedValue
            productForm.LoadCategory() ' Call a method to load data
            productForm.LoadUnits() ' Call a method to load data

            ' ✅ Assign category safely
            Dim catIdValue As Object = dataprod.Rows(e.RowIndex).Cells("catID").Value
            If catIdValue IsNot DBNull.Value AndAlso catIdValue IsNot Nothing Then
                productForm.categorycbx.SelectedValue = Convert.ToInt32(catIdValue) ' Ensure correct type
            Else
                productForm.categorycbx.SelectedIndex = -1 ' Reset
            End If

            ' ✅ Assign unit safely
            Dim unitIDValue As Object = dataprod.Rows(e.RowIndex).Cells("UnitID").Value
            If unitIDValue IsNot DBNull.Value AndAlso unitIDValue IsNot Nothing Then
                productForm.unitcbx.SelectedValue = Convert.ToInt32(unitIDValue)
            Else
                productForm.unitcbx.SelectedIndex = -1 ' Reset
            End If

            ' ✅ Assign other fields
            productForm.txtbarcode.Text = dataprod.Rows(e.RowIndex).Cells("Barcode").Value.ToString()
            productForm.itemdes.Text = dataprod.Rows(e.RowIndex).Cells("Description").Value.ToString()
            productForm.txtcost.Text = dataprod.Rows(e.RowIndex).Cells("CostPrice").Value.ToString()
            productForm.txtsales.Text = dataprod.Rows(e.RowIndex).Cells("SalesPrice").Value.ToString()
            productForm.txtwhole.Text = dataprod.Rows(e.RowIndex).Cells("wholeprice").Value.ToString()

            productForm.lblid.Text = dataprod.Rows(e.RowIndex).Cells("id").Value.ToString()

            ' ✅ Load Image safely
            Try
                If Not IsDBNull(dataprod.Rows(e.RowIndex).Cells("ProductImage").Value) Then
                    Dim img As Image = CType(dataprod.Rows(e.RowIndex).Cells("ProductImage").Value, Image)
                    productForm.PictureBox1.Image = img
                Else
                    productForm.PictureBox1.Image = Nothing
                End If
            Catch ex As Exception
                productForm.PictureBox1.Image = Nothing
            End Try



            ' ✅ Show update button, hide save button
            productForm.btnupdate.Visible = True
            productForm.btnsave.Visible = False

            ' ✅ Open as Modal Dialog
            productForm.ShowDialog()



        ElseIf colname = "Delete" Then

            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If confirm = DialogResult.Yes Then
                Dim productID As String = dataprod.Rows(e.RowIndex).Cells("id").Value.ToString()
                Dim oldvalue As String = ""

                Try
                    If con.State <> ConnectionState.Open Then
                        con.Open()
                    End If

                    Dim fetchquery As String = "select id, item_des, Category from tblproduct as p inner join tblcategory as ct on p.cid = ct.catID where id = @id"
                    Dim fetchcmd As New SqlCommand(fetchquery, con)
                    fetchcmd.Parameters.AddWithValue("id", productID)
                    Dim reader As SqlDataReader = fetchcmd.ExecuteReader()

                    If reader.Read() Then
                        oldvalue = "ProductID: " & productID &
                                   ", Item Description: " & reader("item_des").ToString() &
                                   ", Category: " & reader("Category").ToString()

                    End If
                    reader.Close()
                Catch ex As Exception
                    MessageBox.Show("Error fetching old item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If con.State = ConnectionState.Open Then
                        con.Close()
                    End If
                End Try


                Dim userid As Integer = GlobalUser.UserId

                logoaudit(userid, "Delete", "Table Product", productID.ToString(), oldvalue, "Record Delete")


                con.Open()
                    cmd = New SqlCommand("DELETE FROM tblproduct WHERE barcode = @barcode", con)
                    cmd.Parameters.AddWithValue("@barcode", dataprod.Rows(e.RowIndex).Cells("Barcode").Value.ToString())
                    cmd.ExecuteNonQuery()
                    con.Close()

                    MsgBox("Record has been successfully deleted.", vbInformation)
                    prodrecord() ' ✅ Refresh DataGridView
                End If
            End If


    End Sub


    Sub prodrecord()
        Dim i As Integer = 0
        dataprod.Rows.Clear()

        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            Dim query As String = "SELECT p.id, p.barcode, p.item_des, p.price, p.costprice, p.WholePrice, p.imagepath, " &
                                  "c.catID, c.Category AS Category, " &
                                  "u.unitID, u.unit " &
                                  "FROM tblproduct AS p " &
                                  "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
                                  "INNER JOIN tblunit AS u ON p.uid = u.unitID"

            cmd = New SqlClient.SqlCommand(query, con)
            dr = cmd.ExecuteReader()

            While dr.Read()
                i += 1

                ' ✅ Load Image Safely
                Dim img As Image = Nothing
                If Not IsDBNull(dr("imagepath")) AndAlso dr("imagepath") IsNot Nothing Then
                    Try
                        Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                        If imgData.Length > 0 Then img = ByteArrayToImage(imgData)
                    Catch ex As Exception
                        img = Nothing ' Default image
                    End Try
                End If

                ' ✅ Handle DBNull values correctly
                Dim barcode As String = If(IsDBNull(dr("barcode")), String.Empty, dr("barcode").ToString())
                Dim itemDes As String = If(IsDBNull(dr("item_des")), String.Empty, dr("item_des").ToString())

                ' ✅ Add Category ID and Name
                Dim categoryID As String = If(IsDBNull(dr("catID")), String.Empty, dr("catID").ToString()) ' Category ID
                Dim categoryName As String = If(IsDBNull(dr("Category")), String.Empty, dr("Category").ToString()) ' Category Name

                ' ✅ Add Unit ID and Name
                Dim unitID As String = If(IsDBNull(dr("unitID")), String.Empty, dr("unitID").ToString()) ' Unit ID
                Dim unitName As String = If(IsDBNull(dr("unit")), String.Empty, dr("unit").ToString()) ' Unit Name

                Dim costprice As String = If(IsDBNull(dr("costprice")), "0.00", Convert.ToDecimal(dr("costprice")).ToString("#,##0.00"))
                Dim price As String = If(IsDBNull(dr("price")), "0.00", Convert.ToDecimal(dr("price")).ToString("#,##0.00"))
                Dim Wholeprice As String = If(IsDBNull(dr("wholeprice")), "0.00", Convert.ToDecimal(dr("wholeprice")).ToString("#,##0.00"))


                ' ✅ Ensure the column count matches DataGridView
                dataprod.Rows.Add(i, dr("id").ToString(), img, barcode, itemDes, categoryID, categoryName, unitID, unitName, costprice, price, Wholeprice)
            End While

            ' ✅ Set Row Height
            For Each r As DataGridViewRow In dataprod.Rows
                r.Height = 40
            Next

            ' ✅ Display Record Count
            rc1.Text = "Record Found: (" & dataprod.RowCount & ")"

        Catch ex As Exception
            MsgBox("Error: " & ex.Message & vbCrLf & "Stack Trace: " & ex.StackTrace, vbCritical)
        Finally
            ' ✅ Ensure resources are properly closed
            If dr IsNot Nothing AndAlso Not dr.IsClosed Then dr.Close()
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub




    '' 📌 Clear Controls Before Fetching Data
    'Private Sub ClearsControls()
    '    With frmproduct
    '        .txtbarcode.Clear()
    '        .itemdes.Clear()
    '        .txtcost.Clear()
    '        .txtsales.Clear()
    '        .lblclass.Text = ""
    '        .unitid.Text = ""
    '        .categorycbx.SelectedIndex = -1
    '        .unitcbx.SelectedIndex = -1
    '        .PictureBox1.Image = Nothing
    '    End With
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


    Private Sub frmproductlists_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        With frmproduct
            .txtbarcode.Clear()
            .txtsales.Clear()
            .itemdes.Clear()

            .PictureBox1.Image = Nothing

            .btnsave.Visible = True
            .btnupdate.Visible = False
            .categorycbx.SelectedIndex = -1
            .unitcbx.SelectedIndex = -1


            'categorycbx.SelectedIndex = -1
            'unitcbx.SelectedIndex = -1
        End With
    End Sub

    Private Sub btnew_Click(sender As Object, e As EventArgs) Handles btnew.Click
        Dim frm As New frmproduct()
        frm.ActiveControl = frm.txtbarcode ' Set focus to txtbarcode
        frm.btnsave.Visible = True
        frm.ShowDialog()
    End Sub


    Sub SearchBarcode()
        Try
            Dim i As Integer = 0
            dataprod.Rows.Clear() ' Clear existing rows in DataGridView

            ' Open database connection
            If con.State = ConnectionState.Closed Then con.Open()

            ' Define query
            Dim query As String = "SELECT p.id, p.imagepath, p.barcode, p.item_des, " &
                                  "c.Category, u.unit, p.costprice, p.price, p.WholePrice " &
                                  "FROM tblproduct AS p " &
                                  "INNER JOIN tblcategory AS c ON p.cid = c.catID " &
                                  "INNER JOIN tblunit AS u ON p.uid = u.unitID " &
                                  "WHERE p.barcode LIKE @barcode or p.item_des LIKE @item_des"


            ' Create SQL command and parameters
            cmd = New SqlClient.SqlCommand(query, con)
            cmd.Parameters.Add("@barcode", SqlDbType.VarChar).Value = "%" & Productsearch.Text.Trim() & "%"
            cmd.Parameters.Add("@item_des", SqlDbType.VarChar).Value = "%" & Productsearch.Text.Trim() & "%"

            ' Execute reader
            dr = cmd.ExecuteReader()

            ' Process data
            While dr.Read()
                i += 1


                Dim img As Image = Nothing
                If Not IsDBNull(dr("imagepath")) AndAlso dr("imagepath") IsNot Nothing Then
                    Try
                        Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                        If imgData.Length > 0 Then img = ByteArrayToImage(imgData)
                    Catch ex As Exception
                        img = Nothing ' Default image
                    End Try
                End If

                ' Handle costPrice and price
                Dim costPrice As Decimal = If(IsDBNull(dr("costprice")), 0, Convert.ToDecimal(dr("costprice")))
                Dim price As Decimal = If(IsDBNull(dr("price")), 0, Convert.ToDecimal(dr("price")))
                Dim wholeprice As Decimal = If(IsDBNull(dr("wholeprice")), 0, Convert.ToDecimal(dr("WholePrice")))

                ' Add row to DataGridView
                dataprod.Rows.Add(i,
                                      dr("id").ToString(),
                                      img,
                                      dr("barcode").ToString(),
                                      dr("item_des").ToString(),
                                      dr("Category").ToString(),
                                      dr("unit").ToString(),
                                      costPrice.ToString("C2"),
                                      price.ToString("C2"),
                                      wholeprice.ToString("C2"))



            End While


            ' Adjust row height and auto-size columns
            For Each r As DataGridViewRow In dataprod.Rows
                r.Height = 40
            Next
            dataprod.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

            dr.Close()

        Catch ex As Exception
            ' Display error message
            MsgBox("Error: {ex.Message}", vbCritical, "Search Error")
        Finally
            ' Ensure connection is closed
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    'Private Sub Productsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Productsearch.KeyDown
    '    SearchBarcode()

    'End Sub

    'Private Sub ClearControls()
    '    ' Clear TextBox controls
    '    frmproduct.txtbarcode.Clear()
    '    frmproduct.itemdes.Clear()
    '    frmproduct.txtcost.Clear()
    '    frmproduct.txtsales.Clear()

    '    ' Clear ComboBox controls
    '    frmproduct.categorycbx.SelectedIndex = -1
    '    frmproduct.unitcbx.SelectedIndex = -1

    '    ' Clear PictureBox controls
    '    frmproduct.PictureBox1.Image = Nothing
    '    frmproduct.picbarcode.Text = String.Empty

    '    ' Clear any other controls if needed
    '    ' frmproduct.txtgeneric.Clear()
    '    ' frmproduct.brandcbx.SelectedIndex = -1
    'End Sub



    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Productsearch_TextChanged(sender As Object, e As EventArgs) Handles Productsearch.TextChanged
        SearchBarcode()

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    'Private Sub dataprod_Leave(sender As Object, e As EventArgs) Handles dataprod.Leave
    '    frmproduct.categorycbx.SelectedIndex = -1

    'End Sub


End Class