Imports System.IO

Public Class frmrecordstaff

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub




    Private Sub cboselect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboselect.SelectedIndexChanged
        CriticalStock()

    End Sub
    Private Sub cbofilter_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True

    End Sub


    Private Sub cboselect_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboselect.KeyPress
        e.Handled = True
    End Sub
    Sub CriticalStock()       ' FOR CRITICAL STOCK UNDER STOCK / OUT OF STOCK
        Guna2DataGridView2.Rows.Clear()
        Dim i As Integer = 0
        Dim total As Double = 0

        con.Open()

        If Walanglaman(cboselect) = True Then
            con.Close()
            Return
        End If

        If cboselect.Text = "Under Stock" Then
            cmd = New SqlClient.SqlCommand("select * from tblInventory as iv  inner join tblproduct as p on iv.id = p.id inner join tblbrand as b on p.bid = b.brandID inner join tblcategory as c on p.cid =	c.catID inner join tblformulations as f on p.fid = f.formID inner join tbldosage as d on p.did = d.dosageID inner join tblgeneric as g on p.gid = g.genericID  inner join tblunit as un on p.uid = un.unitID  where (Quantity <= ReorderLevel and Quantity > 0)", con)

        ElseIf cboselect.Text = "Out of Stock" Then
            cmd = New SqlClient.SqlCommand("select * from tblInventory as iv  inner join tblproduct as p on iv.id = p.id inner join tblbrand as b on p.bid = b.brandID inner join tblcategory as c on p.cid =	c.catID inner join tblformulations as f on p.fid = f.formID inner join tbldosage as d on p.did = d.dosageID inner join tblgeneric as g on p.gid = g.genericID  inner join tblunit as un on p.uid = un.unitID  where Quantity = 0", con)
        End If

        dr = cmd.ExecuteReader

        While dr.Read
            i += 1
            total += CInt(dr.Item("Quantity").ToString)
            Guna2DataGridView2.Rows.Add(i, dr.Item("InventoryID").ToString, dr.Item("id").ToString, dr.Item("barcode").ToString, dr.Item("brand").ToString, dr.Item("generic").ToString, dr.Item("Category").ToString, dr.Item("unit").ToString, dr.Item("Formulations").ToString, dr.Item("Dosage").ToString, dr.Item("ReorderLevel").ToString, dr.Item("Quantity").ToString)
        End While

        dr.Close()
        cmd = Nothing
        con.Close()
        cs1.Text = "Record count : " & Format(CLng(Guna2DataGridView2.Rows.Count), "#,##0")


    End Sub


    '       THIS QUERY IS RETRIEVE ALL HAVE THE SAME PRODUCT ID AND ALSO CALCULATE ONLY THE 'SOLD' STATUS AND COMBINE IN QTY 
    Sub SoldItem()

        Dim i As Integer = 0
        Dim date1 As String = DateTimePicker4.Value.ToString("yyyy-MM-dd")
        Dim date2 As String = DateTimePicker3.Value.ToString("yyyy-MM-dd")

        Guna2DataGridView4.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("SELECT  ca.pid, b.brand, g.generic, c.Category, un.unit, f.Formulations, d.Dosage, SUM(ca.qty) as qty FROM tblcart as ca INNER JOIN tblInventory as inv on ca.pid = inv.InventoryID INNER JOIN tblproduct as p on inv.id  = p.id INNER JOIN tblbrand as b on p.bid = b.brandID INNER JOIN tblgeneric as g on p.gid = g.genericID INNER JOIN tblcategory as c on p.cid = c.catID INNER JOIN tblunit as un on p.uid = un.unitID INNER JOIN tblformulations as f on p.fid = f.formID INNER JOIN tbldosage as d on p.did = d.dosageID WHERE ca.sdate BETWEEN '" & date1 & "' AND '" & date2 & "' AND ca.status = 'Sold' GROUP BY ca.pid, b.brand, g.generic, c.Category, un.unit, f.Formulations, d.Dosage", con)
        dr = cmd.ExecuteReader()
        While dr.Read
            i += 1
            Guna2DataGridView4.Rows.Add(i, dr.Item(0).ToString, dr.Item(1).ToString, dr.Item(2).ToString, dr.Item(3).ToString, dr.Item(4).ToString, dr.Item(5).ToString, dr.Item(6).ToString, dr.Item(7).ToString)
        End While
        dr.Close()
        con.Close()

    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        SoldItem()
    End Sub





    Sub ExpiredItem()
        Dim i As Integer = 0
        Dim date1 As String = DateTimePicker6.Value.ToString("yyyy-MM-dd")
        Dim date2 As String = DateTimePicker5.Value.ToString("yyyy-MM-dd")

        Guna2DataGridView5.Rows.Clear()

        Try
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            Dim query As String = "SELECT * FROM tblDeliveryLineItem as div INNER JOIN tbldelivery as del on div.DeliveryID = del.DeliveryID JOIN tblSupplier as sp on div.SupplierID = sp.SupplierID INNER JOIN tblproduct as p on div.id = p.id WHERE div.ExpirationDate BETWEEN @StartDate AND @EndDate ORDER BY div.ExpirationDate DESC;"

            cmd = New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@StartDate", date1)
            cmd.Parameters.AddWithValue("@EndDate", date2)
            dr = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read
                    ' Handle image conversion
                    Dim img As Image = Nothing
                    If Not IsDBNull(dr("imagepath")) Then
                        Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                        img = ByteArrayToImage(imgData)
                    Else
                        img = Nothing ' Placeholder image
                    End If

                    ' Add row to DataGridView
                    i += 1
                    Guna2DataGridView5.Rows.Add(i, dr("LineItemID").ToString(), dr("DeliveryID").ToString(),
                                                dr("barcode").ToString(), dr("BatchNumber").ToString(),
                                                dr("CompanyName").ToString(), img,
                                                dr("ProductName").ToString(), dr("DeliveryDate").ToString(),
                                                dr("ExpirationDate").ToString())
                End While
            Else
                MessageBox.Show("No records found for the given date range.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

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
End Class