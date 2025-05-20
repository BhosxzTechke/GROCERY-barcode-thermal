Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports MissionsGrocery.loginform


Public Class frminventorylist
    ' SQL Connection
    Private con As New SqlConnection("Data Source=DESKTOP-RND3FTT\MSSQLSERVER01;Initial Catalog=JimbospharmaDB;Integrated Security=True;Encrypt=False")

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Public Sub LoadInventoryToFlowPanel()
        ' Clear existing controls
        FlowLayoutPanel1.Controls.Clear()

        Try
            If con.State <> ConnectionState.Open Then
                con.Open()
            End If

            ' SQL Query to Fetch Inventory Data
            Dim query As String = "SELECT iv.InventoryID, iv.Quantity, iv.ExpirationDate, p.item_des, p.price, p.imagepath " &
                                  "FROM tblInventory AS iv " &
                                  "INNER JOIN tblProduct AS p ON iv.id = p.id"

            Dim cmd As New SqlCommand(query, con)
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                While dr.Read()
                    ' Store values in variables BEFORE closing reader
                    Dim inventoryID As Integer = dr("InventoryID")
                    Dim itemName As String = dr("item_des").ToString()
                    Dim price As Decimal = Convert.ToDecimal(dr("price"))
                    Dim stock As Integer = Convert.ToInt32(dr("Quantity"))
                    Dim expirationDate As String = CDate(dr("ExpirationDate")).ToString("yyyy-MM-dd")

                    ' Read Image Data **before closing the reader**
                    Dim img As Image = Nothing
                    If Not IsDBNull(dr("imagepath")) Then
                        Dim imgData As Byte() = TryCast(dr("imagepath"), Byte())
                        If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
                            img = ByteArrayToImage(imgData)
                        End If
                    End If

                    ' 🔹 Create Panel for Each Inventory Item
                    Dim itemPanel As New Guna.UI2.WinForms.Guna2Panel With {
                        .Size = New Size(250, 140),
                        .BackColor = Color.LightGray,
                        .BorderRadius = 8,
                        .Margin = New Padding(5)
                    }

                    ' 🔹 PictureBox for Product Image
                    Dim picBox As New PictureBox With {
                        .Size = New Size(80, 80),
                        .SizeMode = PictureBoxSizeMode.StretchImage,
                        .Location = New Point(10, 10),
                        .Image = img
                    }

                    ' 🔹 Label for Item Name
                    Dim lblName As New Label With {
                        .Text = itemName,
                        .Font = New Font("Arial", 10, FontStyle.Bold),
                        .AutoSize = True,
                        .Location = New Point(100, 10)
                    }

                    ' 🔹 Label for Price
                    Dim lblPrice As New Label With {
                        .Text = "₱" & price.ToString("N2"),
                        .Font = New Font("Arial", 9, FontStyle.Regular),
                        .AutoSize = True,
                        .Location = New Point(100, 30)
                    }

                    ' 🔹 Label for Stock
                    Dim lblStock As New Label With {
                        .Text = "Stock: " & stock.ToString(),
                        .Font = New Font("Arial", 9, FontStyle.Regular),
                        .AutoSize = True,
                        .Location = New Point(100, 50)
                    }

                    ' 🔹 Label for Expiration Date
                    Dim lblExp As New Label With {
                        .Text = "Expires: " & expirationDate,
                        .Font = New Font("Arial", 8, FontStyle.Italic),
                        .AutoSize = True,
                        .ForeColor = Color.Red,
                        .Location = New Point(100, 70)
                    }

                    ' 🔹 Delete Button
                    Dim btnDelete As New Guna.UI2.WinForms.Guna2Button With {
                        .Text = "Remove In Inventory",
                        .Size = New Size(140, 30),
                        .Location = New Point(100, 100)
                    }
                    AddHandler btnDelete.Click, Sub(sender, e) DeleteItem(inventoryID.ToString())




                    ' 🔹 Add Controls to Panel
                    itemPanel.Controls.Add(picBox)
                    itemPanel.Controls.Add(lblName)
                    itemPanel.Controls.Add(lblPrice)
                    itemPanel.Controls.Add(lblStock)
                    itemPanel.Controls.Add(lblExp)
                    itemPanel.Controls.Add(btnDelete)

                    ' 🔹 Add Panel to FlowLayoutPanel
                    FlowLayoutPanel1.Controls.Add(itemPanel)
                End While
            Else
                MessageBox.Show("No inventory records found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            ' Close the reader AFTER reading all data
            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Error loading inventory: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure connection is closed properly
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub DeleteItem(itemID As String)
        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm = DialogResult.Yes Then
            Dim productID As String = itemID
            Dim oldvalue As String = ""
            Try
                If con.State <> ConnectionState.Open Then
                    con.Open()
                End If

                Dim fetchquery As String = "SELECT Quantity, ReorderLevel, ExpirationDate, DeliveryID FROM tblinventory where InventoryID = @id"
                Dim fetchcmd As New SqlCommand(fetchquery, con)
                fetchcmd.Parameters.AddWithValue("id", productID)
                Dim reader As SqlDataReader = fetchcmd.ExecuteReader()

                If reader.Read() Then
                    oldvalue = "InventoryID: " & productID &
                               ", Quantity: " & reader("Quantity").ToString() &
                               ", ReorderLevel: " & reader("ReorderLevel").ToString() &
                               ", ExpirationDate: " & reader("ExpirationDate").ToString() &
                               ", DeliveryID: " & reader("DeliveryID").ToString()

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

            logoaudit(userid, "Delete", "Table Inventory", productID.ToString(), oldvalue, "Record Delete")



            Try
                con.Open()

                Using cmd As New SqlCommand("DELETE FROM tblInventory WHERE InventoryID = @id", con)
                    cmd.Parameters.AddWithValue("@id", itemID)
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Refresh Inventory List
                        LoadInventoryToFlowPanel()


                    Else
                        MessageBox.Show("Item not found or already deleted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show("Error deleting item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        End If
    End Sub

    Private Function ByteArrayToImage(ByVal byteArray As Object) As Image
        ' Check if the value is NULL or empty
        If byteArray Is DBNull.Value OrElse byteArray Is Nothing Then
            Return My.Resources.icondowns ' Return default image if NULL
        End If

        Dim imgData As Byte() = TryCast(byteArray, Byte())
        If imgData IsNot Nothing AndAlso imgData.Length > 0 Then
            Using ms As New MemoryStream(imgData)
                Return Image.FromStream(ms)
            End Using
        End If

        Return My.Resources.icondowns ' Return default image if no valid data
    End Function

    Private Sub Guna2DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub Guna2DataGridView2_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub frminventorylist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryToFlowPanel()
        Me.KeyPreview = True

    End Sub

    Private Sub frminventorylist_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
        End Select
    End Sub


    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()
    End Sub
End Class