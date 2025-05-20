Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Public Class frmproductinquiry

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub

    'Sub ProductInventoryCart()  '   READING ATTRIBUTES FROM TABLE PRODUCT WITH USING INNER JOIN
    '    Dim i As Integer = 0
    '    Guna2DataGridView1.Rows.Clear()
    '    con.Open()
    '    cmd = New SqlClient.SqlCommand("select * from tblproduct as p inner join tblbrand as b on p.bid = b.brandID inner join tblcategory as c on p.cid =	c.catID inner join tblformulations as f on p.fid = f.formID inner join tbldosage as d on p.did = d.dosageID inner join tblgeneric as g on p.gid = g.genericID  inner join tblunit as un on p.uid = un.unitID where qty <> 0 and (brand like '" & txtsearch.Text & "%' or generic like '" & txtsearch.Text & "%' or Category like '" & txtsearch.Text & "%')", con)
    '    dr = cmd.ExecuteReader
    '    While dr.Read
    '        i += 1
    '        Guna2DataGridView1.Rows.Add(i, dr.Item("id").ToString, dr.Item("barcode").ToString, dr.Item("brand").ToString, dr.Item("generic").ToString, dr.Item("Category").ToString, dr.Item("Type").ToString, dr.Item("Formulations").ToString, dr.Item("Dosage").ToString, dr.Item("CompanyName").ToString, dr.Item("reorder").ToString, dr.Item("price").ToString, dr.Item("qty").ToString)
    '    End While
    '    dr.Close()
    '    con.Close()


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





















    'Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.Escape
    '            Me.Dispose()

    '    End Select
    'End Sub

    Private Sub txtsearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsearch.KeyPress
        '    Select Case Asc(e.KeyChar)
        '        Case 13
        '            ProductInventoryCart()
        '    End Select
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        FilterSearch()
    End Sub


    ' ESC KEYo
    Private Sub frmproductinquiry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()

        End Select
    End Sub

    Private Sub frmproductinquiry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True

    End Sub


    Sub loadinventory()
        FlowLayoutPanel1.Controls.Clear() ' Clear existing items

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = New SqlCommand("SELECT * FROM tblInventory AS iv INNER JOIN tblDelivery AS del ON iv.DeliveryID = del.DeliveryID INNER JOIN tblproduct AS p ON iv.id = p.id LEFT JOIN tblcategory AS c ON p.cid = c.catID LEFT JOIN tblunit AS un ON p.uid = un.unitID", con)
            dr = cmd.ExecuteReader

            While dr.Read()
                ' Create a panel for each item
                Dim panel As New Panel With {
                .Size = New Size(200, 300), ' Set panel size
                .BackColor = Color.LightGray,
                .Margin = New Padding(10)
            }

                ' Convert image data to PictureBox
                Dim pic As New PictureBox With {
                .Size = New Size(180, 130),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(10, 10)
            }
                If Not IsDBNull(dr("imagepath")) Then
                    Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                    pic.Image = ByteArrayToImage(imgData)
                End If

                ' Add product name label
                Dim lblName As New Label With {
                .Text = dr("item_des").ToString(),
                .Size = New Size(180, 30),
                .Location = New Point(10, 150),
                .Font = New Font("Arial", 10, FontStyle.Bold)
            }

                ' Add price label
                Dim lblPrice As New Label With {
                .Text = "Retail Price: " & "₱" & dr("price").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 180),
                .ForeColor = Color.Green
            }

                ' Add price label
                Dim lblwhole As New Label With {
                .Text = "Whole Price: " & "₱" & dr("WholePrice").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 205),
                .ForeColor = Color.Green
            }

                ' Add price label
                Dim lblqnty As New Label With {
                .Text = "Product Stocks: " & dr("Quantity").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 228),
                .ForeColor = Color.DarkBlue
            }

                ' Add a button to select product
                Dim btnAdd As New Button With {
                .Text = "Add",
                .Size = New Size(180, 30),
                .Location = New Point(10, 255),
                .Tag = dr("InventoryID").ToString()
            }
                AddHandler btnAdd.Click, AddressOf AddToCart

                ' Add controls to the panel
                panel.Controls.Add(pic)
                panel.Controls.Add(lblName)
                panel.Controls.Add(lblPrice)
                panel.Controls.Add(lblwhole)
                panel.Controls.Add(lblqnty)
                panel.Controls.Add(btnAdd)

                ' Add panel to FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(panel)
            End While

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub



    Sub filtersearch()
        FlowLayoutPanel1.Controls.Clear() ' Clear existing items



        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()



            ' Define the search query
            Dim query As String
            If String.IsNullOrWhiteSpace(txtsearch.Text) Then
                query = "SELECT * FROM tblInventory AS iv INNER JOIN tblDelivery AS del ON iv.DeliveryID = del.DeliveryID INNER JOIN tblproduct AS p ON iv.id = p.id LEFT JOIN tblcategory AS c ON p.cid = c.catID LEFT JOIN tblunit AS un ON p.uid = un.unitID where Category Like @Category OR item_des Like @item ORDER BY Category"


            Else
                query = "SELECT * FROM tblInventory AS iv INNER JOIN tblDelivery AS del ON iv.DeliveryID = del.DeliveryID INNER JOIN tblproduct AS p ON iv.id = p.id LEFT JOIN tblcategory AS c ON p.cid = c.catID LEFT JOIN tblunit AS un ON p.uid = un.unitID where Category Like @Category OR item_des Like @item ORDER BY Category"

            End If

            ' Execute the query
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Category", "%" & txtsearch.Text.Trim() & "%")
            cmd.Parameters.AddWithValue("@item", "%" & txtsearch.Text.Trim() & "%")

            dr = cmd.ExecuteReader()


            Dim found As Boolean = False

            While dr.Read()
                found = True
                ' Create a panel for each item
                Dim panel As New Panel With {
                .Size = New Size(200, 300), ' Set panel size
                .BackColor = Color.LightGray,
                .Margin = New Padding(10)
            }

                ' Convert image data to PictureBox
                Dim pic As New PictureBox With {
                .Size = New Size(180, 130),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(10, 10)
            }
                If Not IsDBNull(dr("imagepath")) Then
                    Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                    pic.Image = ByteArrayToImage(imgData)
                End If

                ' Add product name label
                Dim lblName As New Label With {
                .Text = dr("item_des").ToString(),
                .Size = New Size(180, 30),
                .Location = New Point(10, 150),
                .Font = New Font("Arial", 10, FontStyle.Bold)
            }

                ' Add price label
                Dim lblPrice As New Label With {
                .Text = "Retail Price: " & "₱" & dr("price").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 180),
                .ForeColor = Color.Green
            }

                ' Add price label
                Dim lblwhole As New Label With {
                .Text = "Whole Price: " & "₱" & dr("WholePrice").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 205),
                .ForeColor = Color.Green
            }

                ' Add price label
                Dim lblqnty As New Label With {
                .Text = "Product Stocks: " & dr("Quantity").ToString(),
                .Size = New Size(180, 25),
                .Location = New Point(10, 228),
                .ForeColor = Color.DarkBlue
            }

                ' Add a button to select product
                Dim btnAdd As New Button With {
                .Text = "Add",
                .Size = New Size(180, 30),
                .Location = New Point(10, 255),
                .Tag = dr("InventoryID").ToString()
            }
                AddHandler btnAdd.Click, AddressOf AddToCart

                ' Add controls to the panel
                panel.Controls.Add(pic)
                panel.Controls.Add(lblName)
                panel.Controls.Add(lblPrice)
                panel.Controls.Add(lblwhole)
                panel.Controls.Add(lblqnty)
                panel.Controls.Add(btnAdd)

                ' Add panel to FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(panel)
            End While

            dr.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub
    Private Sub AddToCart(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim inventoryID As String = btn.Tag.ToString()

        ' Call searchproduct to show details and add to cart
        searchproduct(inventoryID)

        ' Reload cart in cashier form
        cashier.loadcart()


    End Sub



    Sub searchproduct(ByVal search As String)
        Try
            If search = String.Empty Then Return

            con.Open()
            ' Fix: Corrected SQL query syntax
            cmd = New SqlClient.SqlCommand("SELECT * FROM tblInventory AS iv INNER JOIN tblproduct AS p ON iv.id = p.id WHERE InventoryID = @search", con)
            cmd.Parameters.AddWithValue("@search", search)
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                Dim stockQty As Integer = CInt(dr("Quantity"))

                ' Check if quantity is less than 10
                If stockQty <= 10 Then
                    dr.Close()
                    con.Close()
                    MsgBox("This product has only " & stockQty & " left. You can only sell if 10 or more are in stock.", vbExclamation)
                    Return
                End If

                ' Proceed if stock is 10 or more

                frmqty.txtqty.Focus()

                With frmqty

                    .txtdescription.Text = dr.Item("item_des").ToString
                    .lblprice.Text = dr.Item("price").ToString
                    .lblwhole.Text = dr.Item("WholePrice").ToString
                    .lblPid.Text = dr.Item("InventoryID").ToString

                    dr.Close()
                    con.Close()
                    .ShowDialog()
                End With
            Else
                dr.Close()
                con.Close()
                MsgBox("Product not found or out of stock.", vbCritical)
                Return
            End If

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub


    'Private Sub Guna2DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)
    '    Dim colname As String = Guna2DataGridView1.Columns(e.ColumnIndex).Name
    '    If colname = "Colladd" Then
    '        searchproduct(Guna2DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)

    '        With cashier  '  real time na mag cacart
    '            .loadcart()
    '        End With

    '    End If
    'End Sub



End Class