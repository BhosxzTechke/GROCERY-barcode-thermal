Public Class frmstockin

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Dispose()

    End Sub

    Sub stockrecord()  '   READING ATTRIBUTES FROM TABLE PRODUCT WITH USING INNER JOIN

        If cbofilter.Text = String.Empty Then Return
        If txtsearch.Text = String.Empty Then Return


        Dim i As Integer = 0
        Guna2DataGridView2.Rows.Clear()
        con.Open() '                                                          '(PRIORITY TO LEARN THIS QUERY)
        cmd = New SqlClient.SqlCommand("select * from tblproduct as p inner join tblbrand as b on p.bid = b.brandID inner join tblcategory as c on p.cid =	c.catID inner join tblformulations as f on p.fid = f.formID inner join tblgeneric as g on p.gid = g.genericID  inner join tbldosage as ds on p.did = ds.dosageID inner join tblunit as u on p.uid = u.unitID  where " & cbofilter.Text & " like '" & txtsearch.Text & "%'", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView2.Rows.Add(i, dr.Item("id").ToString, dr.Item("brand").ToString & Space(2) & "|" & Space(2) & dr.Item("generic").ToString & Space(2) & "|" & Space(2) & dr.Item("Category").ToString & Space(2) & "|" & Space(2) & dr.Item("dosage").ToString & Space(2) & "|" & Space(2) & dr.Item("Formulations").ToString & Space(2) & "|" & Space(2) & dr.Item("unit").ToString)
        End While
        dr.Close()
        con.Close()
        Label2.Text = " Record Found.(" & Guna2DataGridView2.RowCount & ") "

    End Sub



    Private Sub Guna2DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView2.CellContentClick
        Try
            If Walanglaman(txtbatch) = True Then Return
            If Walanglaman(txtcost) = True Then Return

            Dim colname As String = Guna2DataGridView2.Columns(e.ColumnIndex).Name
            Dim data As String = Guna2DataGridView2.Rows(e.RowIndex).Cells(3).Value.ToString
            Dim arr() As String = data.Split("|")

            If colname = "colselect" Then
                '#1
                If MsgBox("brand:  " & arr(0).ToString & vbCr &
                          "Generic: " & arr(1).ToString & vbCr &
                          "Category: " & arr(2).ToString & vbCr &
                          "unit: " & arr(3).ToString & vbCr &
                          "Formulations: " & arr(4).ToString & vbCr &
                         "Dosage: " & arr(5).ToString & vbCr & "Please confirm. ", vbYesNo + vbQuestion) = vbYes Then
                    DataView.Rows.Add(DataView.Rows.Count + 1, Guna2DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString, Guna2DataGridView2.Rows(e.RowIndex).Cells(2).Value.ToString, arr(0).ToString, arr(1).ToString, arr(2).ToString, arr(3).ToString, arr(4).ToString, arr(5).ToString)
                End If
            End If



        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    'Private Sub savebtn_Click(sender As Object, e As EventArgs) Handles savebtn.Click


    '    Try
    '        For i As Integer = 0 To Guna2DataGridView1.Rows.Count - 1
    '            If Guna2DataGridView1.Rows(i).Cells(7).Value = String.Empty Then     '#2 Warning if qty are empty   
    '                MsgBox("Please input quantity. ", vbExclamation)
    '                Return
    '            End If
    '        Next
    '        Dim sdate As String = ddate.Value.ToString("yyyy-MM-dd")
    '        Dim added_rec As Boolean = False       '      define a boolean if every na magiging true then mag proprocess


    '        For i As Integer = 0 To Guna2DataGridView1.Rows.Count - 1


    '            ' This code is iiwasan ang  duplicate entry 
    '            Dim found As Boolean = False
    '            con.Open()
    '            cmd = New SqlClient.SqlCommand("Select * from tblstockin where refno like '" & txtref.Text & "'and pid like '" & Guna2DataGridView1.Rows(i).Cells(1).Value.ToString & "' and sdate like '" & sdate & "'", con)
    '            dr = cmd.ExecuteReader
    '            dr.Read()
    '            If dr.HasRows Then found = True Else found = False
    '            dr.Close()
    '            con.Close()
    '            'hanggang dito



    '            If found = False Then
    '                con.Open()
    '                cmd = Nothing
    '                cmd = New SqlClient.SqlCommand("insert into tblstockin (refno, receivedby, pid, sid, qty, sdate) VALUES('" & txtref.Text & "' , '" & txtreceived.Text & "','" & CInt(Guna2DataGridView1.Rows(i).Cells(1).Value.ToString) & "', '" & CInt(Guna2DataGridView1.Rows(i).Cells(2).Value.ToString) & "','" & CInt(Guna2DataGridView1.Rows(i).Cells(9).Value.ToString) & "','" & sdate & "')", con)     '#4
    '                cmd.ExecuteNonQuery()
    '                con.Close()



    '                con.Open()
    '                cmd = Nothing
    '                cmd = New SqlClient.SqlCommand("update tblproduct set qty = qty + " & CInt(Guna2DataGridView1.Rows(i).Cells(9).Value.ToString) & " where id like '" & Guna2DataGridView1.Rows(i).Cells(1).Value.ToString & "'", con)
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '                added_rec = True
    '            End If
    '        Next


    '        If added_rec = True Then MsgBox("Stock has been succesfully added. ", vbInformation)
    '        With frmproductlist
    '            .prodrecord()
    '        End With

    '    Catch ex As Exception
    '        MsgBox(ex.Message, vbCritical)
    '        con.Close()

    '    End Try
    'End Sub

    ' THIS CODE IS FOR ADDING STOCK IN EVERY QTY COLUMN YOU ADD AND  DISPLAY TO TEXTBOX
    'Private Sub Guna2DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
    '    On Error Resume Next
    '    Dim stock As Double = 0
    '    For i As Integer = 0 To Guna2DataGridView1.Rows.Count - 1
    '        If Guna2DataGridView1.Rows(i).Cells(7).Value.ToString <> String.Empty Then stock += CDbl(Guna2DataGridView1.Rows(i).Cells(7).Value.ToString)
    '    Next
    '    txtcount.Text = stock

    'End Sub

    Sub stockinhistory()
        Dim sdate1 As String = Guna2DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim sdate2 As String = Guna2DateTimePicker2.Value.ToString("yyyy-MM-dd")
        Dim i As Integer = 0
        Guna2DataGridView3.Rows.Clear()
        con.Open()
        cmd = New SqlClient.SqlCommand("select * from tblstockin as s inner join tblproduct as p on  s.pid = p.id join tblbrand as b on p.bid = b.brandID inner join tblcategory as c on p.cid =	c.catID inner join tblformulations as f on p.fid = f.formID inner join tblgeneric as g on p.gid = g.genericID inner join tblType as t on p.tid = t.TypeID inner join tbldosage as ds on p.did = ds.dosageID inner join tblSupplier as sp on p.sid = sp.SupplierID  where sdate between '" & sdate1 & "' and '" & sdate2 & "' order by s.id", con)
        dr = cmd.ExecuteReader
        While dr.Read
            i += 1
            Guna2DataGridView3.Rows.Add(i, dr.Item("id").ToString, dr.Item("refno").ToString, dr.Item("receivedby").ToString, dr.Item("brand").ToString, dr.Item("generic").ToString, dr.Item("Category").ToString, dr.Item("Type").ToString, dr.Item("Formulations").ToString, dr.Item("Dosage").ToString, dr.Item("CompanyName").ToString, dr.Item("qty").ToString, Format(CDate(dr.Item("sdate").ToString), "MM/dd/yyyy"))
        End While
        dr.Close()
        con.Close()

    End Sub






    'Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
    '    stockinhistory()
    'End Sub



    Private Sub cbofilter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbofilter.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtsearch_LostFocus(sender As Object, e As EventArgs) Handles txtsearch.LostFocus
        If txtsearch.Text = "" Then
            txtsearch.Text = "Search Box"
        End If
    End Sub

    Private Sub txtsearch_MouseClick(sender As Object, e As MouseEventArgs) Handles txtsearch.MouseClick
        If txtsearch.Text <> "" Then
            txtsearch.Text = ""
        End If
    End Sub
    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        stockrecord()
    End Sub





    Private Sub cbofilter_TextChanged(sender As Object, e As EventArgs) Handles cbofilter.TextChanged

    End Sub
    Private Sub frmstockin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
  
    End Sub



    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)


    End Sub


    ''                               FOR REFERENCE NO
    'Private Sub txtref_Leave(sender As Object, e As EventArgs)
    '    If String.IsNullOrWhiteSpace(txtref.Text) Then

    '    End If

    'End Sub
    'Private Sub txtref_TextChanged(sender As Object, e As EventArgs)

    '    txtref.ForeColor = Color.Black
    '    txtref.Focus()
    'End Sub


    ''                                FOR RECEIVED BY
    'Private Sub txtreceived_TextChanged(sender As Object, e As EventArgs)

    '    txtreceived.ForeColor = Color.Black
    '    txtreceived.Focus()
    'End Sub
    'Private Sub txtreceived_Leave(sender As Object, e As EventArgs)
    '    If String.IsNullOrWhiteSpace(txtreceived.Text) Then

    '    End If

    'End Sub




    'Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs)
    '    txtreceived.Clear()
    '    txtref.Clear()
    '    ddate.Value = Now
    '    Guna2DataGridView1.Rows.Clear()
    'End Sub





    Private Sub ddate_ValueChanged(sender As Object, e As EventArgs) Handles ddate.ValueChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Guna2DataGridView3_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView3.CellContentClick

    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub cbofilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbofilter.SelectedIndexChanged

    End Sub
End Class
