Public Class frmstockadjustment

    Private Sub frmstockadjustment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Dispose()

    End Sub

    Sub stockrecord()  '   READING ATTRIBUTES FROM TABLE PRODUCT WITH USING INNER JOIN

        If cbofilter.Text = String.Empty Then Return
        If txtsearch.Text = String.Empty Then Return

        Dim i As Integer = 0
        Guna2DataGridView2.Rows.Clear()
        con.Open() '                                                          '(PRIORITY TO LEARN THIS QUERY)
        cmd = New SqlClient.SqlCommand("select * from tblInventory as iv inner join tblproduct as p on iv.id = p.id LEFT JOIN  tblbrand as b on p.bid = b.brandID LEFT JOIN tblcategory as c on p.cid =	c.catID LEFT JOIN  tblformulations as f on p.fid = f.formID LEFT JOIN  tbldosage as d on p.did = d.dosageID LEFT JOIN  tblgeneric as g on p.gid = g.genericID  LEFT JOIN  tblunit as un on p.uid = un.unitID where " & cbofilter.Text & " like '" & txtsearch.Text & "%'", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView2.Rows.Add(i, dr.Item("InventoryID").ToString, dr.Item("id").ToString, dr.Item("brand").ToString & Space(2) & "|" & Space(2) & dr.Item("generic").ToString & Space(2) & "|" & Space(2) & dr.Item("Category").ToString & Space(2) & "|" & Space(2) & dr.Item("Formulations").ToString & Space(2) & "|" & Space(2) & dr.Item("unit").ToString & Space(2) & "|" & Space(2) & dr.Item("Dosage").ToString & Space(2) & "|" & Space(2) & dr.Item("Quantity").ToString)
        End While
        dr.Close()
        con.Close()

    End Sub

    Private Sub Guna2DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellContentClick
        Try


            Dim colname As String = Guna2DataGridView2.Columns(e.ColumnIndex).Name

            If colname = "colselect" Then
                con.Open()
                cmd = New SqlClient.SqlCommand("select * from tblInventory as iv inner join tblproduct as p on iv.id = p.id LEFT JOIN  tblbrand as b on p.bid = b.brandID LEFT JOIN tblcategory as c on p.cid =	c.catID LEFT JOIN  tblformulations as f on p.fid = f.formID LEFT JOIN  tbldosage as d on p.did = d.dosageID LEFT JOIN  tblgeneric as g on p.gid = g.genericID  LEFT JOIN  tblunit as un on p.uid = un.unitID where InventoryID like '" & Guna2DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString & "'", con)
                dr = cmd.ExecuteReader
                dr.Read()
                If dr.HasRows Then
                    txtinvent.Text = dr.Item("InventoryID").ToString
                    txtProdID.Text = dr.Item("id").ToString
                    txtbrand.Text = dr.Item("brand").ToString
                    txtgeneric.Text = dr.Item("generic").ToString
                    txtcategory.Text = dr.Item("category").ToString
                    txtformulation.Text = dr.Item("Formulations").ToString
                    txttype.Text = dr.Item("unit").ToString
                    txtdosage.Text = dr.Item("Dosage").ToString
                    txtlaman.Text = dr.Item("Quantity").ToString

                    ' IF CINLICK IPUPUNTA NIYA SA MGA TEXTBOX 
                End If
                dr.Close()
                con.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try


    End Sub
    Private Sub txtsearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsearch.KeyPress
        Select Case Asc(e.KeyChar)
            Case 13
                If Walanglaman(cbofilter) = True Then Return
                If Walanglaman(txtsearch) = True Then Return
                stockrecord()

        End Select
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged

    End Sub



    Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click
        Try
            If Walanglaman(txtbagongstock) OrElse Not IsNumeric(txtbagongstock.Text) Then
                MsgBox("Please enter a valid number for stock adjustment.", vbExclamation)
                Return
            End If

            If Not (rbadd.Checked Or rbremove.Checked) Then
                MsgBox("Please select adjustment type!", vbExclamation)
                Return
            End If

            If Walanglaman(txtreasons) OrElse String.IsNullOrEmpty(txtProdID.Text) Then
                MsgBox("Required fields are missing.", vbExclamation)
                Return
            End If

            Dim currentQty As Integer = CInt(txtlaman.Text)
            Dim newQty As Integer = CInt(txtbagongstock.Text)

            If rbremove.Checked AndAlso currentQty < newQty Then
                MsgBox("Unable to process. Available stock is " & currentQty & ".", vbExclamation)
                Return
            End If

            Dim adjustmentType As String = If(rbadd.Checked, "Add", "Remove")
            Dim adjustmentDate As Date = Now

            If MsgBox("Are you sure you want to Adjust this product Quantity?", vbYesNo + vbQuestion) = vbYes Then
                con.Open()
                Dim query As String = If(rbadd.Checked,
                    "UPDATE tblInventory SET Quantity = (Quantity + @Quantity) WHERE InventoryID = @InventoryID",
                    "UPDATE tblInventory SET Quantity = (Quantity - @Quantity) WHERE InventoryID = @InventoryID")

                cmd = New SqlClient.SqlCommand(query, con)
                cmd.Parameters.AddWithValue("@Quantity", newQty)
                cmd.Parameters.AddWithValue("@InventoryID", txtinvent.Text)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected = 0 Then
                    MsgBox("No rows were updated. Please check the Inventory ID.", vbExclamation)
                    Return
                End If

                cmd = New SqlClient.SqlCommand("INSERT INTO tbladjustment (InvID, pid, qty, stype, reason, adjusted_by, sdate) VALUES (@InvID, @pid, @qty, @stype, @reason, @adjusted_by, @sdate)", con)
                With cmd
                    .Parameters.AddWithValue("@InvID", txtinvent.Text)
                    .Parameters.AddWithValue("@pid", txtProdID.Text)
                    .Parameters.AddWithValue("@qty", newQty)
                    .Parameters.AddWithValue("@stype", adjustmentType)
                    .Parameters.AddWithValue("@reason", txtreasons.Text)
                    .Parameters.AddWithValue("@adjusted_by", txtadjusted.Text)
                    .Parameters.AddWithValue("@sdate", adjustmentDate)
                    .ExecuteNonQuery()
                End With

                MsgBox("Product has been successfully adjusted.", vbInformation)
                clear()
                With frmproductlists
                    .prodrecord()
                End With
            End If

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub



    Sub clear()
        txtinvent.Clear()
        txtProdID.Clear()
        txtbrand.Clear()
        txtgeneric.Clear()
        txtcategory.Clear()
        txtformulation.Clear()
        txttype.Clear()
        txtdosage.Clear()
        txtreasons.Clear()
        txtlaman.Clear()
        txtadjusted.Clear()
    End Sub


    Sub loadAdjustmentHistory()

        Dim date1 As String = Guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim date2 As String = Guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")

        Dim i As Integer = 0
        Guna2DataGridView3.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("select  a.id, b.brand, g.generic, c.Category, u.unit, f.Formulations,d.Dosage, a.qty, a.stype, a.adjusted_by, a.reason, a.sdate  from tbladjustment as a inner join tblproduct as p on a.pid = p.id inner join tblbrand as b on b.brandID = p.bid inner join tblgeneric as g on p.gid = g.genericID inner join tblcategory as c on c.catID = p.cid inner join tblunit as u on u.unitID = p.uid inner join tblformulations as f on f.formID = p.fid INNER JOIN tbldosage as d on p.did = d.dosageID where sdate between  '" & date1 & "' and '" & date2 & "'", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView3.Rows.Add(i, dr.Item("id").ToString, dr.Item("brand").ToString, dr.Item("generic").ToString, dr.Item("Category").ToString, dr.Item("unit").ToString, dr.Item("Formulations").ToString, dr.Item("Dosage").ToString, dr.Item("qty").ToString, dr.Item("stype").ToString, dr.Item("reason").ToString, dr.Item("adjusted_by").ToString, dr.Item("sdate").ToString)

        End While
        dr.Close()
        con.Close()

    End Sub


    Private Sub txtqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtbagongstock.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57 ' 0- 9
            Case 8
            Case Else
                e.Handled = True

        End Select
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        loadAdjustmentHistory()
    End Sub

    Private Sub txtadjusted_TextChanged(sender As Object, e As EventArgs) Handles txtadjusted.TextChanged

    End Sub

    Private Sub lblname_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtlaman_TextChanged(sender As Object, e As EventArgs) Handles txtlaman.TextChanged

    End Sub

    Private Sub cbofilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbofilter.SelectedIndexChanged

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Guna2DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView3.CellContentClick

    End Sub
End Class