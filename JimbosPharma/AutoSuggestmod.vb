Imports System.Data.SqlClient

Module AutoSuggestmod



    'Public Sub DeliveryCBX()        '       AUTOMATIC TO FILL IN COMBOBOX IN SUPPLIER
    '    con.Open()

    '    cmd = New SqlClient.SqlCommand("Select * from tblcategory order by Category", con)
    '    Dim ds As New DataSet
    '    Dim da As New SqlClient.SqlDataAdapter(cmd)
    '    da.Fill(ds, "Category")
    '    Dim col As New List(Of String)
    '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '        col.Add(ds.Tables(0).Rows(i)("Category").ToString)
    '    Next
    '    With frmbrowsedelivery
    '        .Guna2ComboBox1.DataSource = col
    '        .Guna2ComboBox1.AutoCompleteMode = AutoCompleteMode.None
    '        .Guna2ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
    '    End With
    '    cmd = Nothing
    '    con.Close()

    'End Sub





    'Sub dropdowncategory(targetForm As frmproduct)        '       AUTOMATIC TO FILL IN COMBOBOX IN CATEGORY
    '    con.Open()

    '    cmd = New SqlClient.SqlCommand("Select * from tblcategory order by Category", con)
    '    Dim ds As New DataSet
    '    Dim da As New SqlClient.SqlDataAdapter(cmd)
    '    da.Fill(ds, "Category")
    '    Dim col As New List(Of String)
    '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '        col.Add(ds.Tables(0).Rows(i)("Category").ToString)
    '    Next
    '    With targetForm
    '        .boxcat.DataSource = col
    '        .boxcat.AutoCompleteMode = AutoCompleteMode.None
    '        .boxcat.DropDownStyle = ComboBoxStyle.DropDownList
    '    End With
    '    cmd = Nothing
    '    con.Close()
    'End Sub


    'Sub dropdownunit(targetForm As frmproduct)
    '    Try
    '        con.Open()

    '        Dim cmd As New SqlClient.SqlCommand("SELECT unitID, unit FROM tblunit ORDER BY unit", con)
    '        Dim dt As New DataTable()
    '        Dim da As New SqlClient.SqlDataAdapter(cmd)
    '        da.Fill(dt)

    '        With targetForm.unitcbx
    '            .DataSource = Nothing ' ✅ Reset first
    '            .Items.Clear()        ' ✅ Clear previous data
    '            .DataSource = dt
    '            .DisplayMember = "unit"
    '            .ValueMember = "unitID"
    '        End With

    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        con.Close()
    '    End Try
    'End Sub





End Module



'                                                      AUTO SUGGEST

'Sub Dosagesuggest()          '        AUTOMATIC TO FILL IN COMBOBOX IN DOSAGE

'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tbldosage order by Dosage", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "Dosage")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("Dosage").ToString)
'    Next
'    With frmproduct
'        .txtdosage.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtdosage.AutoCompleteCustomSource = col
'        .txtdosage.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub




''         AUTO SUGGEST

'Sub Formulationname()

'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tblformulations order by Formulations", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "Formulations")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("Formulations").ToString)
'    Next
'    With frmproduct
'        .txtformu.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtformu.AutoCompleteCustomSource = col
'        .txtformu.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub

'Sub loadbrand()
'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tblbrand order by brand", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "brand")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("brand").ToString)
'    Next
'    With frmproduct
'        .txtbrand.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtbrand.AutoCompleteCustomSource = col
'        .txtbrand.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub


'Sub loadgenericname()

'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tblgeneric order by generic", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "generic")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("generic").ToString)
'    Next
'    With frmproduct
'        .txtgeneric.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtgeneric.AutoCompleteCustomSource = col
'        .txtgeneric.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub



'Sub loadunit()

'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tblunit order by unit", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "unit")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("unit").ToString)
'    Next
'    With frmproduct
'        .txtunit.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtunit.AutoCompleteCustomSource = col
'        .txtunit.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub

'Sub loadcategory()

'    con.Open()
'    cmd = New SqlClient.SqlCommand("Select * from tblcategory order by Category", con)
'    Dim ds As New DataSet
'    Dim da As New SqlClient.SqlDataAdapter(cmd)
'    da.Fill(ds, "Category")
'    Dim col As New AutoCompleteStringCollection
'    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
'        col.Add(ds.Tables(0).Rows(i)("Category").ToString)
'    Next
'    With frmproduct
'        .txtcategory.AutoCompleteSource = AutoCompleteSource.CustomSource
'        .txtcategory.AutoCompleteCustomSource = col
'        .txtcategory.AutoCompleteMode = AutoCompleteMode.Suggest
'        cmd = Nothing
'        con.Close()
'    End With

'End Sub



