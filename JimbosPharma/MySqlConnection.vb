Imports System.Data
Imports System.Data.SqlClient

Module Dbsql

    ' Global user-related variables
    Public struser As String
    Public strPass As String
    Public strtype As String
    Public stremail As String
    Public strstat As String
    Public strcontact As String
    Public strvat As String
    Public strlastname As String
    Public strId As String
    ' Define the connection string
    Public connString As String = "Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False;"

    ' Declare the connection object
    Public con As New SqlConnection(connString)

    ' Declare the command object
    Public cmd As New SqlCommand With {
        .Connection = con
    }

    ' Reader object
    Public dr As SqlDataReader



    ' Ensure the connection is open
    Public Sub EnsureConnectionOpen()
        If con.State = ConnectionState.Closed Then
            Try
                con.Open()
            Catch ex As Exception
                MessageBox.Show("Error connecting to the database: " & ex.Message)
            End Try
        End If
    End Sub

    ' Ensure the connection is closed
    Public Sub EnsureConnectionClosed()
        If con.State = ConnectionState.Open Then
            Try
                con.Close()
            Catch ex As Exception
                MessageBox.Show("Error closing the database connection: " & ex.Message)
            End Try
        End If
    End Sub


    ' #1 for vat 
    Function getvat() As Double
        con.Open()
        cmd = New SqlClient.SqlCommand("select * from tblVat", con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then getvat = CDbl(dr.Item("vat").ToString) Else getvat = 0.0
        dr.Close()
        con.Close()
        Return getvat

    End Function

    Function GetStockSettings() As (StockLevel As Integer, CriticalLevel As Integer)
        Dim stockLevel As Integer = 0
        Dim criticalLevel As Integer = 0

        Try
            con.Open()
            cmd = New SqlClient.SqlCommand("SELECT * FROM tblStockSettings", con)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                stockLevel = CInt(dr("StockLevel").ToString())
                criticalLevel = CInt(dr("CriticalLevel").ToString())
            End If
            dr.Close()
            con.Close()
        Catch ex As Exception
            con.Close()
            MsgBox("Error loading stock settings: " & ex.Message, vbCritical)
        End Try

        Return (stockLevel, criticalLevel)
    End Function





    Public Function Walanglaman(ByVal sText As Object) As Boolean

        On Error Resume Next
        If sText.Text = "" Then
            Walanglaman = True
            MsgBox("Warning: Required missing field", vbExclamation)
            sText.FillColor = Color.LemonChiffon
            sText.Focus()
        Else
            Walanglaman = False
            sText.FillColor = Color.White
        End If

    End Function
    Public Function Walanglamanuser(ByVal sText As Object) As Boolean

        On Error Resume Next
        If sText.Text = "" Then
            Walanglamanuser = True
            MsgBox("Warning: Required missing field", vbExclamation)
            sText.Bordercolor = Color.White
            sText.Focus()
        Else
            Walanglamanuser = False
            sText.FillColor = Color.White
        End If

    End Function



    Public Function ValidateDuplicateEntry(ByVal sql As String) As Boolean ' FUNCTION NA PWEDENG TAWAGIN IN VALIDATION
        con.Open()
        cmd = New SqlClient.SqlCommand(sql, con)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            ValidateDuplicateEntry = True
            MsgBox("Warning: Duplicate entry.", vbExclamation)
        Else
            ValidateDuplicateEntry = False
        End If

        dr.Close()
        con.Close()
    End Function






    'Sub connection()
    '    With con
    '        .ConnectionString = "Data Source=TECHQUINA\SQLEXPRESS;Initial Catalog=JimbospharmaDB;Integrated Security=True"
    '    End With
    'End Sub


    'Public da As New SqlDataAdapter
    'Public ds As New DataSet
    ' Public dt As New DataTable
    ' Public qr As String
    ' Public i As Integer




    'FOR SEARCHING
    ''  Public Function searchdata(ByVal qr As String) As DataSet
    ''      da = New sqldataadapter(qr, con)
    '    ds = New dataset
    '    da.fill(ds)
    '    Return ds



    'End Function

    ''for Insert Update and Delete Record
    'Public Function InserData(ByVal qr As String) As Integer
    '    cmd = New SqlCommand(qr, con)
    '    con.Open()
    '    i = cmd.ExecuteNonQuery()
    '    con.Close()
    '    Return i
    'End Function



End Module