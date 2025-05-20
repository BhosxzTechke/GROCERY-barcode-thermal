
Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Guna.UI2.WinForms
Imports MissionsGrocery.loginform

Public Class frmsupplierlist

    ' Helper function to convert byte array to Image and resize it





    Sub LoadUserListToFlowPanel()
        ' Clear existing controls in the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Clear()

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            ' Open the connection
            con.Open()

            Using cmd As New SqlCommand("SELECT * FROM tblSupplier ORDER BY CompanyName", con)
                Using dr As SqlDataReader = cmd.ExecuteReader()
                    While dr.Read()
                        ' 🔹 Retrieve Data
                        Dim supplierID As String = dr("SupplierID").ToString()
                        Dim companyName As String = dr("CompanyName").ToString()
                        Dim address As String = dr("Address").ToString()
                        Dim contactPerson As String = dr("Contact_Person").ToString()
                        Dim mobileNo As String = dr("Mobile_no").ToString()
                        Dim telNo As String = dr("Tel_no").ToString()

                        ' 🔹 Retrieve and Convert Image
                        Dim img As Image = Nothing
                        If Not IsDBNull(dr("ImageSpl")) Then
                            Dim imgData As Byte() = DirectCast(dr("ImageSpl"), Byte())
                            img = ByteArrayToImage(imgData)
                        Else
                            img = Nothing ' Set a default placeholder image
                        End If

                        ' 🔹 Create a Panel for Each Supplier
                        Dim itemPanel As New Guna.UI2.WinForms.Guna2Panel With {
                        .Size = New Size(250, 180),
                        .BackColor = Color.LightGray,
                        .BorderRadius = 8,
                        .Margin = New Padding(5)
                    }

                        ' 🔹 PictureBox for Supplier Image
                        Dim picBox As New PictureBox With {
                        .Size = New Size(80, 80),
                        .SizeMode = PictureBoxSizeMode.StretchImage,
                        .Location = New Point(10, 10),
                        .Image = img
                    }

                        ' 🔹 Label for Company Name
                        Dim lblCompanyName As New Label With {
                        .Text = companyName,
                        .Font = New Font("Arial", 10, FontStyle.Bold),
                        .AutoSize = True,
                        .Location = New Point(100, 10)
                    }

                        ' 🔹 Label for Address
                        Dim lblAddress As New Label With {
                        .Text = "📍 " & address,
                        .Font = New Font("Arial", 9, FontStyle.Regular),
                        .AutoSize = True,
                        .Location = New Point(100, 30)
                    }

                        ' 🔹 Label for Contact Person
                        Dim lblContact As New Label With {
                        .Text = "👤 " & contactPerson,
                        .Font = New Font("Arial", 9, FontStyle.Regular),
                        .AutoSize = True,
                        .Location = New Point(100, 50)
                    }

                        ' 🔹 Label for Mobile and Telephone Numbers
                        Dim lblMobile As New Label With {
                        .Text = "📞 " & mobileNo & " / " & telNo,
                        .Font = New Font("Arial", 8, FontStyle.Italic),
                        .AutoSize = True,
                        .ForeColor = Color.DarkBlue,
                        .Location = New Point(100, 70)
                    }



                        ' 🔹 Button to View Details
                        Dim btnDetails As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "View Details",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 100)
                        }

                        ' 🔹 Attach Click Event Handler
                        AddHandler btnDetails.Click, Sub(sender, e) ShowSupplierDetails(supplierID, itemPanel)

                        ' 🔹 Add Controls to Panel
                        itemPanel.Controls.Add(btnDetails)



                        ' 🔹 Button to Edit Supplier
                        Dim btnEdit As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "Edit",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 125),
                            .FillColor = Color.LimeGreen,
                            .ForeColor = Color.White
                        }

                        ' 🔹 Attach Click Event Handler
                        AddHandler btnEdit.Click, Sub(sender, e)
                                                      With frmmodifsupplier
                                                          .isEditing = True ' ✅ Prevent clearing fields in Load event
                                                          .lblid.Text = supplierID
                                                          .txtcompany.Text = companyName
                                                          .txtaddress.Text = address
                                                          .txtcontact.Text = contactPerson
                                                          .txtmob.Text = mobileNo
                                                          .txttel.Text = telNo
                                                          .btnsave.Visible = False
                                                          .btnupdate.Visible = True
                                                          .PictureBox1.Image = img
                                                          .ShowDialog()
                                                      End With

                                                  End Sub

                        ' 🔹 Add Edit Button to Panel
                        itemPanel.Controls.Add(btnEdit)


                        ' 🔹 Button to Delete Supplier
                        Dim btnDelete As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "Delete",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 150),
                            .FillColor = Color.Red,
                            .ForeColor = Color.White
                        }
                        AddHandler btnDelete.Click, Sub(sender, e) DeleteSupplier(supplierID, itemPanel)

                        itemPanel.Controls.Add(btnDelete)



                        ' 🔹 Add Controls to Panel
                        itemPanel.Controls.Add(picBox)
                        itemPanel.Controls.Add(lblCompanyName)
                        itemPanel.Controls.Add(lblAddress)
                        itemPanel.Controls.Add(lblContact)
                        itemPanel.Controls.Add(lblMobile)
                        itemPanel.Controls.Add(btnDetails)
                        itemPanel.Controls.Add(btnEdit)

                        ' 🔹 Add Panel to FlowLayoutPanel
                        FlowLayoutPanel1.Controls.Add(itemPanel)
                    End While
                End Using
            End Using

        Catch ex As Exception
            MsgBox("Error loading supplier list: " & ex.Message, vbCritical)
        Finally
            ' Close the connection properly
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub frmsupplierlist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUserListToFlowPanel()

        'dataprod.AlternatingRowsDefaultCellStyle = dataprod.RowsDefaultCellStyle

    End Sub

    ' Create a new supplier
    Private Sub btnew_Click(sender As Object, e As EventArgs) Handles btnew.Click
        With frmmodifsupplier
            .isEditing = False ' ✅ Ensure new entry mode
            .cleartext() ' ✅ Call cleartext() to reset fields
            .ShowDialog()
        End With

    End Sub




    Private Sub datagridview_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


    Private Sub DataGridView1_Leave(sender As Object, e As EventArgs)


        With frmmodifsupplier
            .txtcompany.Clear()
            .txtaddress.Clear()
            .txtmob.Clear()
            .txttel.Clear()
            .PictureBox1.Image = Nothing ' Or set a default image
            .txtcontact.Clear()
            .btnsave.Visible = True
            .btnupdate.Visible = False
        End With


    End Sub

    Private Function ByteArrayToImage(ByVal byteArray As Byte()) As Image
        If byteArray Is Nothing OrElse byteArray.Length = 0 Then Return Nothing

        Try
            Using ms As New MemoryStream(byteArray)
                Dim originalImage As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                Dim resizedImage As New Bitmap(100, 130) ' Adjust dimensions as needed
                Using g As Graphics = Graphics.FromImage(resizedImage)
                    g.DrawImage(originalImage, 0, 0, 100, 130)
                End Using
                Return resizedImage
            End Using
        Catch ex As ArgumentException
            Return Nothing
        End Try
    End Function









    Sub Searchcompany()
        Try
            ' Clear previous search results from the FlowLayoutPanel
            FlowLayoutPanel1.Controls.Clear()

            ' Open the connection if it's closed
            If con.State = ConnectionState.Closed Then con.Open()

            ' Define the search query
            Dim query As String
            If String.IsNullOrWhiteSpace(searchboxing.Text) Then
                query = "SELECT * FROM tblSupplier ORDER BY CompanyName"
            Else
                query = "SELECT * FROM tblSupplier WHERE CompanyName LIKE @CompanyName OR Address LIKE @Address ORDER BY CompanyName"
            End If

            ' Execute the query
            cmd = New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@CompanyName", "%" & searchboxing.Text.Trim() & "%")
            cmd.Parameters.AddWithValue("@Address", "%" & searchboxing.Text.Trim() & "%")
            dr = cmd.ExecuteReader()

            ' Check if there are any results
            Dim found As Boolean = False

            While dr.Read()
                found = True ' Set flag to indicate that results exist

                ' 🔹 Retrieve Supplier Data
                Dim supplierID As String = dr("SupplierID").ToString()
                Dim companyName As String = dr("CompanyName").ToString()
                Dim address As String = dr("Address").ToString()
                Dim contactPerson As String = If(Not IsDBNull(dr("Contact_Person")), dr("Contact_Person").ToString(), "N/A")
                Dim mobileNo As String = If(Not IsDBNull(dr("Mobile_no")), dr("Mobile_no").ToString(), "N/A")
                Dim telNo As String = If(Not IsDBNull(dr("Tel_no")), dr("Tel_no").ToString(), "N/A")




                ' 🔹 Retrieve Image from Database
                Dim img As Image = Nothing
                If Not IsDBNull(dr("ImageSpl")) Then
                    Dim imgData As Byte() = DirectCast(dr("ImageSpl"), Byte())
                    If imgData.Length > 0 Then img = ByteArrayToImage(imgData)
                Else
                    img = Nothing ' Placeholder image
                End If




                ' 🔹 Create a Panel for Each Supplier
                Dim itemPanel As New Guna.UI2.WinForms.Guna2Panel With {
                .Size = New Size(250, 200),
                .BackColor = Color.LightGray,
                .BorderRadius = 8,
                .Margin = New Padding(5)
            }

                ' 🔹 PictureBox for Supplier Image
                Dim picBox As New PictureBox With {
                .Size = New Size(80, 80),
                .SizeMode = PictureBoxSizeMode.StretchImage,
                .Location = New Point(10, 10),
                .Image = img
            }

                ' 🔹 Labels for Supplier Info
                Dim lblCompanyName As New Label With {
                .Text = companyName,
                .Font = New Font("Arial", 10, FontStyle.Bold),
                .AutoSize = True,
                .Location = New Point(100, 10)
            }

                Dim lblAddress As New Label With {
                .Text = "📍 " & address,
                .Font = New Font("Arial", 9, FontStyle.Regular),
                .AutoSize = True,
                .Location = New Point(100, 30)
            }

                Dim lblContact As New Label With {
                .Text = "👤 " & contactPerson,
                .Font = New Font("Arial", 9, FontStyle.Regular),
                .AutoSize = True,
                .Location = New Point(100, 50)
            }

                Dim lblMobile As New Label With {
                .Text = "📞 " & mobileNo & " / " & telNo,
                .Font = New Font("Arial", 8, FontStyle.Italic),
                .AutoSize = True,
                .ForeColor = Color.DarkBlue,
                .Location = New Point(100, 70)
            }






                ' 🔹 Button to View Details
                Dim btnDetails As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "View Details",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 100)
                        }

                ' 🔹 Attach Click Event Handler
                AddHandler btnDetails.Click, Sub(sender, e) ShowSupplierDetails(supplierID, itemPanel)

                ' 🔹 Add Controls to Panel
                itemPanel.Controls.Add(btnDetails)



                ' 🔹 Button to Edit Supplier
                Dim btnEdit As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "Edit",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 125),
                            .FillColor = Color.LimeGreen,
                            .ForeColor = Color.White
                        }

                ' 🔹 Attach Click Event Handler
                AddHandler btnEdit.Click, Sub(sender, e)
                                              With frmmodifsupplier
                                                  .lblid.Text = supplierID
                                                  .txtcompany.Text = companyName
                                                  .txtaddress.Text = address
                                                  .txtcontact.Text = contactPerson
                                                  .txtmob.Text = mobileNo
                                                  .txttel.Text = telNo

                                                  .ShowDialog()
                                              End With
                                          End Sub

                ' 🔹 Add Edit Button to Panel
                itemPanel.Controls.Add(btnEdit)


                ' 🔹 Button to Delete Supplier
                Dim btnDelete As New Guna.UI2.WinForms.Guna2Button With {
                            .Text = "Delete",
                            .Size = New Size(120, 22),
                            .Location = New Point(100, 150),
                            .FillColor = Color.Red,
                            .ForeColor = Color.White
                        }
                AddHandler btnDelete.Click, Sub(sender, e) DeleteSupplier(supplierID, itemPanel)

                itemPanel.Controls.Add(btnDelete)





                ' 🔹 Add Controls to Panel
                itemPanel.Controls.Add(picBox)
                itemPanel.Controls.Add(lblCompanyName)
                itemPanel.Controls.Add(lblAddress)
                itemPanel.Controls.Add(lblContact)
                itemPanel.Controls.Add(lblMobile)
                itemPanel.Controls.Add(btnDetails)
                itemPanel.Controls.Add(btnDelete)

                ' 🔹 Add Panel to FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(itemPanel)
            End While

            ' If no results were found, show a message
            If Not found Then
                MsgBox("No records found.", vbInformation, "Search Results")
            End If

        Catch ex As Exception
            MsgBox("An error occurred while searching: " & ex.Message, vbCritical, "Search Error")
        Finally
            ' Close data reader and connection
            If dr IsNot Nothing AndAlso Not dr.IsClosed Then dr.Close()
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub








    Sub ShowSupplierDetails(supplierID As String, itemPanel As Guna.UI2.WinForms.Guna2Panel)
        Try
            ' Open the connection if it's closed
            If con.State = ConnectionState.Closed Then con.Open()

            ' Fetch Supplier Details
            Dim query As String = "SELECT * FROM tblSupplier WHERE SupplierID = @SupplierID"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@SupplierID", supplierID)
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.Read() Then
                Dim companyName As String = dr("CompanyName").ToString()
                Dim address As String = dr("Address").ToString()
                Dim contactPerson As String = If(Not IsDBNull(dr("Contact_Person")), dr("Contact_Person").ToString(), "N/A")
                Dim mobileNo As String = If(Not IsDBNull(dr("Mobile_no")), dr("Mobile_no").ToString(), "N/A")
                Dim telNo As String = If(Not IsDBNull(dr("Tel_no")), dr("Tel_no").ToString(), "N/A")

                ' Display details (Modify as needed)
                MsgBox("Supplier Details:" & vbCrLf &
                   "Company: " & companyName & vbCrLf &
                   "Address: " & address & vbCrLf &
                   "Contact: " & contactPerson & vbCrLf &
                   "Mobile: " & mobileNo & " / " & telNo, vbInformation, "Supplier Info")

            Else
                MsgBox("Supplier details not found.", vbExclamation, "Error")
            End If

            dr.Close()
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message, vbCritical, "Error")
        Finally
            If con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub



    Sub DeleteSupplier(supplierID As String, itemPanel As Guna.UI2.WinForms.Guna2Panel)


        Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If confirm = DialogResult.Yes Then
            Dim productID As String = supplierID
            Dim oldvalue As String = ""

            Try
                If con.State <> ConnectionState.Open Then
                    con.Open()
                End If

                Dim fetchquery As String = "select * from tblsupplier where SupplierID = @SupplierID"
                Dim fetchcmd As New SqlCommand(fetchquery, con)
                fetchcmd.Parameters.AddWithValue("SupplierID", supplierID)
                Dim reader As SqlDataReader = fetchcmd.ExecuteReader()

                If reader.Read() Then
                    oldvalue = "Supplier: " & supplierID &
                               ", Company Name: " & reader("CompanyName").ToString() &
                               ", Address: " & reader("Address").ToString()

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

            logoaudit(userid, "Delete", "Table Supplier", supplierID.ToString(), oldvalue, "Record Delete")
            Try
                ' Open the database connection
                If con.State = ConnectionState.Closed Then con.Open()

                ' Execute delete query
                Dim cmd As New SqlCommand("DELETE FROM tblSupplier WHERE SupplierID = @SupplierID", con)
                cmd.Parameters.AddWithValue("@SupplierID", supplierID)
                cmd.ExecuteNonQuery()

                ' Success message
                MessageBox.Show("Supplier has been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Remove the panel from the FlowLayoutPanel
                FlowLayoutPanel1.Controls.Remove(itemPanel)
            Catch ex As Exception
                ' Error message
                MessageBox.Show("Error deleting supplier: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Close the database connection
                If con.State = ConnectionState.Open Then con.Close()
            End Try

        End If






    End Sub


































    'Private Sub dataprod_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    '    ' Prevent processing if the header row is clicked
    '    If e.RowIndex < 0 Then Return

    '    Dim colname As String = dataprod.Columns(e.ColumnIndex).Name

    '    If colname = "View" Then
    '        With frmviewsupplie

    '            .txtcompany.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
    '            .txtcont.Text = dataprod.Rows(e.RowIndex).Cells(5).Value.ToString()
    '            .txtemail.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
    '            .txtmob.Text = dataprod.Rows(e.RowIndex).Cells(6).Value.ToString()
    '            .txttel.Text = dataprod.Rows(e.RowIndex).Cells(7).Value.ToString()


    '            Dim imageData As Byte() = Nothing
    '            If Not IsDBNull(dataprod.Rows(e.RowIndex).Cells(4).Value) Then
    '                Dim bitmap As Bitmap = TryCast(dataprod.Rows(e.RowIndex).Cells(4).Value, Bitmap)
    '                If bitmap IsNot Nothing Then
    '                    Using ms As New MemoryStream()
    '                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
    '                        imageData = ms.ToArray()
    '                    End Using
    '                End If
    '            End If


    '            ' Pass the image data to the frmmodifsupplier form
    '            .SetImageData(imageData)
    '            .ShowDialog()

    '        End With

    '    ElseIf colname = "Edit" Then
    '        ' Populate the frmmodifsupplier form with retrieved data from the DataGridView

    '        Dim supplierform As New frmmodifsupplier()

    '        ' ✅ Set Editing Mode to prevent clearing fields
    '        supplierform.isEditing = True  ' 🔥 Add this line

    '        supplierform.lblid.Text = dataprod.Rows(e.RowIndex).Cells(0).Value.ToString()
    '        supplierform.txtcompany.Text = dataprod.Rows(e.RowIndex).Cells(2).Value.ToString()
    '        supplierform.txtaddress.Text = dataprod.Rows(e.RowIndex).Cells(3).Value.ToString()
    '        supplierform.txtcontact.Text = dataprod.Rows(e.RowIndex).Cells(5).Value.ToString()
    '        supplierform.txtmob.Text = dataprod.Rows(e.RowIndex).Cells(6).Value.ToString()
    '        supplierform.txttel.Text = dataprod.Rows(e.RowIndex).Cells(7).Value.ToString()
    '        supplierform.btnupdate.Visible = True
    '        supplierform.btnsave.Visible = False



    '        Dim imageData As Byte() = Nothing
    '        If Not IsDBNull(dataprod.Rows(e.RowIndex).Cells(4).Value) Then
    '            Dim bitmap As Bitmap = TryCast(dataprod.Rows(e.RowIndex).Cells(4).Value, Bitmap)
    '            If bitmap IsNot Nothing Then
    '                Using ms As New MemoryStream()
    '                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png) ' You can also use other formats like .Jpeg
    '                    imageData = ms.ToArray()
    '                End Using
    '            End If
    '        End If

    '        ' Pass the image data to the frmmodifsupplier form
    '        supplierform.SetImageData(imageData)

    '        ' Show the modification form as a dialog
    '        supplierform.ShowDialog()

    '    ElseIf colname = "Delete" Then
    '        ' Confirm before deleting
    '        Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this Supplier?",
    '                                                     "Confirm Deletion",
    '                                                     MessageBoxButtons.YesNo,
    '                                                     MessageBoxIcon.Warning)

    '        If result = DialogResult.No Then
    '            Return
    '        End If

    '        Try
    '            con.Open()
    '            cmd = New SqlClient.SqlCommand("DELETE FROM tblSupplier WHERE SupplierID = " &
    '                                           dataprod.Rows(e.RowIndex).Cells(0).Value.ToString(), con)
    '            cmd.ExecuteNonQuery()

    '            ' Success message
    '            MessageBox.Show("Supplier has been deleted successfully.",
    '                            "Success",
    '                            MessageBoxButtons.OK,
    '                            MessageBoxIcon.Information)

    '            LoadUserList()

    '        Catch ex As Exception
    '            ' Error message
    '            MessageBox.Show("Error deleting user: " & ex.Message,
    '                            "Error",
    '                            MessageBoxButtons.OK,
    '                            MessageBoxIcon.Error)
    '        Finally
    '            con.Close()
    '        End Try
    '    End If

    'End Sub




    Private Sub FlowLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel1.Paint

    End Sub

    Private Sub searchboxing_TextChanged_1(sender As Object, e As EventArgs) Handles searchboxing.TextChanged
        Searchcompany()
    End Sub
End Class







'Private Sub Guna2DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
'    ' Check if the clicked row is valid (ignores header row clicks)
'    If e.RowIndex < 0 Then Return

'    ' Get the column name where the click occurred
'    Dim colname As String = Guna2DataGridView1.Columns(e.ColumnIndex).Name

'    ' Check if the clicked column is "Edit" or "Delete"
'    If colname = "Action" Then
'        ' Store the row index in the ContextMenuStrip's Tag for later use
'        Guna2ContextMenuStrip1.Tag = e.RowIndex

'        ' Show the context menu at the mouse position
'        Dim mousePosition As Point = Guna2DataGridView1.PointToClient(Cursor.Position)
'        Guna2ContextMenuStrip1.Show(Guna2DataGridView1, mousePosition)
'    End If
'End Sub

'Private Sub DELETEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DELETEToolStripMenuItem.Click
'    ' Get the row index from the ContextMenuStrip's Tag property
'    If Guna2ContextMenuStrip1.Tag IsNot Nothing Then
'        Dim rowIndex As Integer = CType(Guna2ContextMenuStrip1.Tag, Integer)

'        ' Confirm delete operation
'        If MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
'            con.Open()
'            Dim cmd As New SqlCommand("DELETE FROM tblSupplier WHERE SupplierID = @SupplierID", con)
'            cmd.Parameters.AddWithValue("@SupplierID", Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString())
'            cmd.ExecuteNonQuery()
'            con.Close()

'            ' Notify user
'            MessageBox.Show("Record has been successfully deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
'            supplierrecord() ' Refresh grid or view
'        End If
'    End If
'End Sub
