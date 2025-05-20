Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports MissionsGrocery.loginform



Public Class frmmodifsupplier
    Public isEditing As Boolean = False ' Default is False (Not Editing)

    Dim imagePath As String ' To store selected photo path

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Sub cleartext()
        If isEditing Then Return ' ✅ Prevent clearing when editing

        btnsave.Visible = True
        btnupdate.Visible = False
        txtcompany.Clear()
        txtaddress.Clear()
        txtcontact.Clear()
        txtmob.Clear()
        txttel.Clear()

        PictureBox1.Image = Nothing
    End Sub




    Private Sub frmmodifsupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtmob.MaxLength = 11 ' ✅ Limit the contact number to 11 characters

        If Not isEditing Then
            cleartext() ' ✅ Only clear fields if NOT editing
        End If

        txttel.MaxLength = 11 ' or 10 depending on your country


    End Sub


    Sub clear()
        txtcompany.Clear()
        txtaddress.Clear()
        txtcontact.Clear()
        txtmob.Clear()
        txttel.Clear()
        PictureBox1.Image = Nothing ' Clear the image
        imagePath = String.Empty ' Clear the image path
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Open file dialog to select a photo
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
        openFileDialog.Title = "Select a Photo"

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            imagePath = openFileDialog.FileName
            ' Display the image in PictureBox1 after loading it as a Bitmap
            Using img As System.Drawing.Image = System.Drawing.Image.FromFile(imagePath)
                PictureBox1.Image = New Bitmap(img) ' Load image without locking the file
            End Using
        End If
    End Sub

    ' Validate contact number format
    Private Function IsValidContactNumber(contact As String) As Boolean
        Return IsNumeric(contact) AndAlso contact.Length = 11 AndAlso contact.StartsWith("09")
    End Function


    Public Sub SetImageData(ByVal imageData As Byte())
        If imageData IsNot Nothing AndAlso imageData.Length > 0 Then
            Try
                ' Convert byte array to Image and set it to PictureBox
                Using ms As New MemoryStream(imageData)
                    PictureBox1.Image = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                ' Handle any errors (e.g., invalid image format)
                MessageBox.Show("Error loading image: " & ex.Message)
                PictureBox1.Image = Nothing ' Optional: Set default image if error occurs
            End Try
        Else
            ' If no image data is available, clear the PictureBox
            PictureBox1.Image = Nothing
        End If
    End Sub



    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            ' Validate required fields
            If AreRequiredFieldsEmpty() Then
                MsgBox("Please fill in all required fields.", vbExclamation)
                Return
            End If

            ' Validate unique company name
            If Not IsUniqueCompanyName(txtcompany.Text) Then
                MsgBox("Company name already exists. Please choose another.", vbExclamation)
                txtcompany.BackColor = Color.LightCoral
                Return
            End If

            If txttel.Text.Length > 15 Then
                MessageBox.Show("Please enter a valid phone number (up to 15 digits).")
            End If

            ' Validate contact number format
            If Not IsValidContactNumber(txtmob.Text) Then
                txtmob.BackColor = Color.LightCoral
                MsgBox("Contact number must be exactly 11 digits and start with '09'.", vbExclamation)
                Return
            End If


            If String.IsNullOrWhiteSpace(txtcompany.Text) Then
                MessageBox.Show("Company cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            ' Optional regex-based validation (only letters, numbers, spaces)
            Dim pattern As String = "^[a-zA-Z0-9\s]+$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtcompany.Text, pattern) Then
                MessageBox.Show("Invalid characters in Company name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            ' Confirm save action
            If MsgBox("Are you sure you want to save this new account?", vbYesNo + vbQuestion) = MsgBoxResult.Yes Then
                ' Convert image to byte array, or set to DBNull if imagePath is empty
                Dim imageBytes As Byte() = Nothing
                If Not String.IsNullOrEmpty(imagePath) Then
                    imageBytes = File.ReadAllBytes(imagePath)
                End If

                ' Database operation
                con.Open()
                cmd = New SqlCommand("INSERT INTO tblSupplier (CompanyName, Address, ImageSpl, Contact_Person, Mobile_no, Tel_no) " &
                                     "OUTPUT INSERTED.SupplierID " &
                                     "VALUES (@CompanyName, @Address, @ImageSpl, @Contact_Person, @Mobile_no, @Tel_no)", con)

                ' Add parameters
                cmd.Parameters.AddWithValue("@CompanyName", txtcompany.Text)
                cmd.Parameters.AddWithValue("@Address", txtaddress.Text)

                ' Add image as a BLOB or set it to DBNull if no image is provided
                If imageBytes IsNot Nothing Then
                    cmd.Parameters.Add("@ImageSpl", SqlDbType.VarBinary).Value = imageBytes
                Else
                    cmd.Parameters.Add("@ImageSpl", SqlDbType.VarBinary).Value = DBNull.Value
                End If

                cmd.Parameters.AddWithValue("@Contact_Person", txtcontact.Text)
                cmd.Parameters.AddWithValue("@Mobile_no", txtmob.Text)
                cmd.Parameters.AddWithValue("@Tel_no", txttel.Text)



                Dim newproductID As Integer = CInt(cmd.ExecuteScalar())


                Dim userid As Integer = GlobalUser.UserId

                Dim newvalues As String = "Company: " & txtcompany.Text & ", " &
                                      "Address: " & txtaddress.Text & ", " &
                                      "Contact Person: " & txtcontact.ToString()

                logoaudit(userid, "INSERT", "Table tblsupplier", newproductID.ToString(), "", newvalues)
                MsgBox("New Supplier has been successfully saved.", vbInformation)


                ' Clear the form for next entry
                With frmsupplierlist
                    .LoadUserListToFlowPanel()
                End With
                clear()
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical)
        Finally
            ' Ensure the connection is closed even if an error occurs
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub


    '' Helper function to check if required fields are empty
    'Private Function AreRequiredFieldsEmpty() As Boolean
    '    Return String.IsNullOrEmpty(txtcompany.Text) OrElse
    '           String.IsNullOrEmpty(txtaddress.Text) OrElse
    '           String.IsNullOrEmpty(txtcontact.Text) OrElse
    '           String.IsNullOrEmpty(txtmob.Text) OrElse
    '           String.IsNullOrEmpty(txttel.Text)
    'End Function

    ' Example function to check unique company name
    Private Function IsUniqueCompanyName(companyName As String) As Boolean
        Dim unique As Boolean = True
        Try
            con.Open()
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM tblSupplier WHERE CompanyName = @CompanyName", con)
            checkCmd.Parameters.AddWithValue("@CompanyName", companyName)
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            unique = (count = 0)
        Catch ex As Exception
            MsgBox("Error checking unique company name: " & ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
        Return unique
    End Function

    Private Function IsUniqueCompanyNameUPDATE(companyName As String, supid As Integer) As Boolean
        Dim unique As Boolean = True
        Try
            con.Open()
            Dim checkCmd As New SqlCommand("SELECT COUNT(*) FROM tblSupplier WHERE CompanyName = @CompanyName and  SupplierID <> @supplierid", con)
            checkCmd.Parameters.AddWithValue("@CompanyName", companyName)
            checkCmd.Parameters.AddWithValue("@supplierid", supid)
            Dim count As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
            unique = (count = 0)
        Catch ex As Exception
            MsgBox("Error checking unique company name: " & ex.Message, vbCritical)
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
        Return unique
    End Function

    ' Check if required fields are empty
    Private Function AreRequiredFieldsEmpty() As Boolean
        Return String.IsNullOrWhiteSpace(txtcompany.Text) OrElse
               String.IsNullOrWhiteSpace(txtaddress.Text) OrElse
               String.IsNullOrWhiteSpace(txtcontact.Text) OrElse
               String.IsNullOrWhiteSpace(txtmob.Text) OrElse
               String.IsNullOrWhiteSpace(txttel.Text)
    End Function



    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try


            ' Validate required fields
            If AreRequiredFieldsEmpty() Then
                MsgBox("Please fill in all required fields.", vbExclamation)
                Return
            End If

            ' Validate unique company name
            If Not IsUniqueCompanyNameUPDATE(txtcompany.Text, lblid.Text) Then
                MsgBox("Company name already exists. Please choose another.", vbExclamation)
                txtcompany.BackColor = Color.LightCoral
                Return
            End If

            If txttel.Text.Length > 15 Then
                MessageBox.Show("Please enter a valid phone number (up to 15 digits).")
            End If

            ' Validate contact number format
            If Not IsValidContactNumber(txtmob.Text) Then
                txtmob.BackColor = Color.LightCoral
                MsgBox("Contact number must be exactly 11 digits and start with '09'.", vbExclamation)
                Return
            End If


            If String.IsNullOrWhiteSpace(txtcompany.Text) Then
                MessageBox.Show("Company cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If



            ' Optional regex-based validation (only letters, numbers, spaces)
            Dim pattern As String = "^[a-zA-Z0-9\s]+$"
            If Not System.Text.RegularExpressions.Regex.IsMatch(txtcompany.Text, pattern) Then
                MessageBox.Show("Invalid characters in Company name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            ' Validate SupplierID
            If String.IsNullOrEmpty(lblid.Text) OrElse Not IsNumeric(lblid.Text) Then
                MsgBox("Invalid Supplier ID. Please select a valid supplier to update.", vbExclamation)
                Return
            End If


            ' Confirm update action
            If MsgBox("Are you sure you want to update this account?", vbYesNo + vbQuestion) = MsgBoxResult.Yes Then
                ' Convert image to byte array if a new image path is provided, or keep the existing image
                Dim imageBytes As Byte() = Nothing
                If Not String.IsNullOrEmpty(imagePath) Then
                    imageBytes = File.ReadAllBytes(imagePath)
                End If





                ' AUDIT: Save new values
                Dim productID As String = lblid.Text
                Dim newvalues As String = "Company Name: " & txtcompany.Text & ", " &
                                  "Address: " & txtaddress.Text & ", " &
                                  "Contact Person: " & txtcontact.Text & ", " &
                                  "Mobile Person: " & txtmob.Text & ", " &
                                  "Telephone Number: " & txttel.Text
                Dim userid As Integer = GlobalUser.UserId
                logoaudit(userid, "Update", "tblSupplier", productID, "", newvalues)



                ' Database operation
                con.Open()
                cmd = New SqlCommand("UPDATE tblSupplier SET CompanyName = @CompanyName, Address = @Address, " &
                                     "ImageSpl = CASE WHEN @ImageSpl IS NULL THEN ImageSpl ELSE @ImageSpl END, " &
                                     "Contact_Person = @Contact_Person, Mobile_no = @Mobile_no, Tel_no = @Tel_no " &
                                     "WHERE SupplierID = @SupplierID", con)

                ' Add parameters
                cmd.Parameters.AddWithValue("@CompanyName", txtcompany.Text)
                cmd.Parameters.AddWithValue("@Address", txtaddress.Text)
                cmd.Parameters.AddWithValue("@Contact_Person", txtcontact.Text)
                cmd.Parameters.AddWithValue("@Mobile_no", txtmob.Text)
                cmd.Parameters.AddWithValue("@Tel_no", txttel.Text)
                cmd.Parameters.AddWithValue("@SupplierID", CInt(lblid.Text)) ' Convert SupplierID to an integer

                ' Check if image data is provided; if not, pass DBNull
                If imageBytes IsNot Nothing Then
                    cmd.Parameters.Add("@ImageSpl", SqlDbType.VarBinary).Value = imageBytes
                Else
                    cmd.Parameters.Add("@ImageSpl", SqlDbType.VarBinary).Value = DBNull.Value
                End If

                ' Execute the command
                cmd.ExecuteNonQuery()
                MsgBox("Account has been successfully updated.", vbInformation)

                ' Reload the supplier list
                frmsupplierlist.LoadUserListToFlowPanel()
                clear()
                Me.Close()

            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, vbCritical)
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub Guna2Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel1.Paint

    End Sub

    Private Sub txtmob_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmob.KeyPress
        ' Allow only digits, one decimal point, and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If


    End Sub

    Private Sub txttel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttel.KeyPress
        ' Allow only digits,  and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcontact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcontact.KeyPress
        ' Allow only digits,  and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtcompany_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcompany.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtaddress_KeyDown(sender As Object, e As KeyEventArgs) Handles txtaddress.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtcontact_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcontact.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtmob_KeyDown(sender As Object, e As KeyEventArgs) Handles txtmob.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txttel_KeyDown(sender As Object, e As KeyEventArgs) Handles txttel.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub


    'if hindi daw digit ung char atsaka not equal sa pag convert sa char 
End Class