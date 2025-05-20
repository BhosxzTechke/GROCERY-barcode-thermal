Imports System.IO
Imports System.Data.SqlClient
Imports System.Drawing
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions
Imports System.Web.UI.Design

Public Class frmuser
    Dim imagePath As String ' To store selected photo path
    Private isEditMode As Boolean = False
    Private editUserId As Integer ' Store the user ID for editing


    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = True

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        ' Reset background colors before validation
        ResetBackgroundColors()

        Try


            ' Validate required fields
            If AreRequiredFieldsEmpty() Then
                Guna2MessageDialog1.Text = "Please fill in all required fields."
                Guna2MessageDialog1.Show()
                Return
            End If

            ' Validate name fields
            If Not IsValidName(txtfn.Text) OrElse Not IsValidName(txtln.Text) OrElse Not IsValidName(txtmn.Text) Then
                Guna2MessageDialog1.Text = "Names should only contain alphabetic characters."
                Guna2MessageDialog1.Show()
                Return
            End If

            ' Validate username uniqueness

            ' if hindi sia naiiba then already exist lets say sa function may nakita na name na katulat ni txtuser

            If Not IsUniqueUsername(txtuser.Text) Then
                Guna2MessageDialog1.Text = "Username already exists. Please choose another."
                Guna2MessageDialog1.Show()
                txtuser.BackColor = Color.LightCoral
                Return
            End If

            ' Validate User Type selection
            If combowombo.Text = "Select a User Type" Then
                Guna2MessageDialog1.Text = "Please choose a valid User Type."
                Guna2MessageDialog1.Show()
                combowombo.Focus()
                Return
            End If

            Dim password As String = txtpass.Text.Trim()

            If Not IsStrongPassword(password) Then
                Guna2MessageDialog1.Text = "Password must be at least 8 characters long and include uppercase, lowercase, a number, and a special character (@$!%*?&), or be at least 8 digits if numeric only."
                Guna2MessageDialog1.Show()
                txtpass.BackColor = Color.LightCoral
                Return
            Else
                txtpass.BackColor = Color.White
            End If



            ' Validate email format
            If Not IsValidEmail(txtemail.Text) Then
                Guna2MessageDialog1.Text = "Please enter a valid email address."
                Guna2MessageDialog1.Show()
                txtemail.BackColor = Color.LightCoral
                Return
            End If

            ' Validate contact number format
            If Not IsValidContactNumber(txtcont.Text) Then
                Guna2MessageDialog1.Text = "Contact number must be exactly 11 digits and start with '09'."
                Guna2MessageDialog1.Show()
                txtcont.BackColor = Color.LightCoral
                Return
            End If

            ' Confirmation dialog
            Guna2MessageDialog1.Buttons = MessageDialogButtons.YesNo
            Guna2MessageDialog1.Text = "Are you sure you want to save this new account?"

            If Guna2MessageDialog1.Show() = DialogResult.Yes Then
                ' Convert image to byte array
                Dim imageBytes As Byte() = Nothing
                If Not String.IsNullOrEmpty(imagePath) Then
                    imageBytes = File.ReadAllBytes(imagePath)
                End If

                ' Open database connection
                con.Open()

                ' Check if we're in edit mode
                If isEditMode Then
                    ' Update user
                    cmd = New SqlClient.SqlCommand("UPDATE tbluser SET FirstName=@FirstName, LastName=@LastName, MiddleName=@MiddleName, Address=@Address, Contact=@Contact, EmailAddress=@EmailAddress, Username=@Username, Password=@Password, User_Type=@User_Type, imagepath=@imagepath WHERE ID=@ID", con)
                    cmd.Parameters.AddWithValue("@ID", editUserId)
                Else
                    ' Insert new user
                    cmd = New SqlClient.SqlCommand("INSERT INTO tbluser (FirstName, LastName, MiddleName, Address, Contact, EmailAddress, Username, Password, User_Type, Status, imagepath) VALUES (@FirstName, @LastName, @MiddleName, @Address, @Contact, @EmailAddress, @Username, @Password, @User_Type, 'active', @imagepath)", con)
                End If

                ' Common parameters
                cmd.Parameters.AddWithValue("@FirstName", txtfn.Text)
                cmd.Parameters.AddWithValue("@LastName", txtln.Text)
                cmd.Parameters.AddWithValue("@MiddleName", txtmn.Text)
                cmd.Parameters.AddWithValue("@Address", txtadr.Text)
                cmd.Parameters.AddWithValue("@Contact", txtcont.Text)
                cmd.Parameters.AddWithValue("@EmailAddress", txtemail.Text)
                cmd.Parameters.AddWithValue("@Username", txtuser.Text)
                cmd.Parameters.AddWithValue("@Password", txtpass.Text)
                cmd.Parameters.AddWithValue("@User_Type", combowombo.Text)

                ' Handle image
                If imageBytes IsNot Nothing Then
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = imageBytes
                Else
                    cmd.Parameters.Add("@imagepath", SqlDbType.VarBinary).Value = DBNull.Value
                End If

                cmd.ExecuteNonQuery()
                Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
                Guna2MessageDialog1.Text = "New account has been successfully saved."
                Guna2MessageDialog1.Show()
                ClearControls()
                LoadUserList()
                combowombo.Text = "Cashier"

            End If
        Catch ex As Exception
            Guna2MessageDialog1.Text = "Error: " & ex.Message
            Guna2MessageDialog1.Show()
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        ' Ensure we are in edit mode


        ' Validate required fields
        If AreRequiredFieldsEmpty() Then
            Guna2MessageDialog1.Text = "Please fill in all required fields."
            Guna2MessageDialog1.Show()
            Return
        End If

        ' Validate username uniqueness
        If Not IsUniqueUsernameupdate(txtuser.Text, editUserId) Then
            Guna2MessageDialog1.Text = "Username already exists. Please choose another."
            Guna2MessageDialog1.Show()
            txtuser.BackColor = Color.LightCoral
            Return
        End If


        Dim password As String = txtpass.Text.Trim()

        If Not IsStrongPassword(password) Then
            Guna2MessageDialog1.Text = "Password must be at least 8 characters long and include uppercase, lowercase, a number, and a special character (@$!%*?&), or be at least 8 digits if numeric only."
            Guna2MessageDialog1.Show()
            txtpass.BackColor = Color.LightCoral
            Return
        Else
            txtpass.BackColor = Color.White
        End If



        ' Validate email format
        If Not IsValidEmail(txtemail.Text) Then
            Guna2MessageDialog1.Text = "Please enter a valid email address."
            Guna2MessageDialog1.Show()
            txtemail.BackColor = Color.LightCoral
            Return
        End If

        ' Validate contact number format
        If Not IsValidContactNumber(txtcont.Text) Then
            Guna2MessageDialog1.Text = "Contact number must be exactly 11 digits and start with '09'."
            Guna2MessageDialog1.Show()
            txtcont.BackColor = Color.LightCoral
            Return
        End If

        If Not isEditMode Then Return

        ' Confirmation message before updating
        Guna2MessageDialog1.Buttons = MessageDialogButtons.YesNo
        Guna2MessageDialog1.Text = "Are you sure you want to update this account?"


        If Guna2MessageDialog1.Show() = DialogResult.No Then
            Return
        End If

        Try
            ' Update existing user
            con.Open()
            Dim query As String = "UPDATE tbluser SET FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Address = @Address, " &
                                  "Contact = @Contact, EmailAddress = @EmailAddress, Username = @Username, Password = @Password, " &
                                  "User_Type = @User_Type"

            ' Convert image to byte array only if a new image has been selected
            Dim imageBytes As Byte() = Nothing
            If Not String.IsNullOrEmpty(imagePath) Then
                imageBytes = File.ReadAllBytes(imagePath)
                query &= ", imagepath = @imagepath"
            End If
            query &= " WHERE ID = @ID"

            cmd = New SqlClient.SqlCommand(query, con)

            ' Set parameters
            cmd.Parameters.AddWithValue("@ID", editUserId)
            cmd.Parameters.AddWithValue("@FirstName", txtfn.Text)
            cmd.Parameters.AddWithValue("@LastName", txtln.Text)
            cmd.Parameters.AddWithValue("@MiddleName", txtmn.Text)
            cmd.Parameters.AddWithValue("@Address", txtadr.Text)
            cmd.Parameters.AddWithValue("@Contact", txtcont.Text)
            cmd.Parameters.AddWithValue("@EmailAddress", txtemail.Text)
            cmd.Parameters.AddWithValue("@Username", txtuser.Text)
            cmd.Parameters.AddWithValue("@Password", txtpass.Text)
            cmd.Parameters.AddWithValue("@User_Type", combowombo.Text)

            ' Only add the image parameter if a new image was selected
            If imageBytes IsNot Nothing Then
                cmd.Parameters.AddWithValue("@imagepath", CType(imageBytes, Object))
            End If

            cmd.ExecuteNonQuery()

            ' Success message
            Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
            Guna2MessageDialog1.Text = "Account has been successfully updated."
            Guna2MessageDialog1.Show()

            ' Reset form and refresh user list
            ClearControls()
            LoadUserList()

            combowombo.Text = "Cashier"
            btnupdate.Visible = False
            btnsave.Visible = True
            isEditMode = False


        Catch ex As Exception
            Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
            Guna2MessageDialog1.Text = "Error: " & ex.Message
            Guna2MessageDialog1.Show()
        Finally
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub



    Public Sub ClearControls()
        txtfn.Clear()
        txtln.Clear()
        txtmn.Clear()
        txtuser.Clear()
        txtadr.Clear()
        txtcont.Clear()
        txtemail.Clear()
        txtpass.Clear()
        combowombo.SelectedIndex = -1
        PictureBox1.Image = Nothing ' Or set a default image
        txtpass.Enabled = True
        combowombo.Text = "Cashier"
    End Sub




    Private Sub Guna2DataGridView1_CellContentClick_2(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick
        If e.RowIndex < 0 Then Return

        ' Access the column name (Edit button column, etc.)
        Dim colName As String = Guna2DataGridView1.Columns(e.ColumnIndex).Name

        ' Handle actions based on the column name
        Select Case colName
            Case "Edit"
                Dim userType As String = Guna2DataGridView1.Rows(e.RowIndex).Cells("UserType").Value.ToString()

                TabControl1.SelectedIndex = 1
                btnupdate.Visible = True
                btnsave.Visible = False
                editUserId = Convert.ToInt32(Guna2DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                isEditMode = True
                FetchAndDisplayUserData(editUserId)
                txtpass.PasswordChar = "*"c
                txtpass.Enabled = False
                If userType = "admin" Then
                    combowombo.Text = "admin"
                End If


            Case "ChangeStatus"
                ' Call method to update user status if the status column is clicked
                UpdateUserStatus(e.RowIndex)

            Case "View"
                editUserId = Convert.ToInt32(Guna2DataGridView1.Rows(e.RowIndex).Cells(0).Value)
                fetchviewuser(editUserId)

            Case "Delete"
                Dim userType As String = Guna2DataGridView1.Rows(e.RowIndex).Cells("UserType").Value.ToString()
                If userType.ToLower() = "admin" Then
                    MessageBox.Show("Admin users cannot be deleted.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                DeleteUser(e.RowIndex)


            Case Else
                ' You can add a default case if needed for other columns
                Exit Sub
        End Select
    End Sub



    Private Sub UpdateUserStatus(rowIndex As Integer)

        Dim userType As String = Guna2DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()
        Dim currentStatus As String = Guna2DataGridView1.Rows(rowIndex).Cells(5).Value.ToString()
        Dim username As String = Guna2DataGridView1.Rows(rowIndex).Cells(2).Value.ToString()

        ' Prevent changes to System Administrator's status
        If userType = "admin" Or userType = "Admin" Then
            MessageBox.Show("You cannot change the status of the system administrator.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Toggle status between "active" and "Inactive"
        Dim newStatus As String = If(currentStatus = "active", "Inactive", "active")
        Dim confirmationMessage As String = String.Format("Are you sure you want to set this account to {0}?", newStatus)

        ' Confirm status change
        If MessageBox.Show(confirmationMessage, "Confirm Status Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                con.Open()
                cmd = New SqlClient.SqlCommand("UPDATE tbluser SET Status = @Status WHERE Username = @Username", con)
                cmd.Parameters.AddWithValue("@Status", newStatus)
                cmd.Parameters.AddWithValue("@Username", username)
                cmd.ExecuteNonQuery()

                MessageBox.Show(String.Format("Account status has been successfully set to {0}.", newStatus), "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadUserList() ' Refresh the table to show updated status

            Catch ex As Exception
                MessageBox.Show("Error updating status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                con.Close()
            End Try
        End If




    End Sub


    Private Sub DeleteUser(rowIndex As Integer)


        Dim userType As String = Guna2DataGridView1.Rows(rowIndex).Cells(3).Value.ToString()

        ' Confirm before deleting
        Guna2MessageDialog1.Buttons = MessageDialogButtons.YesNo
        Guna2MessageDialog1.Icon = MessageDialogIcon.Warning
        Guna2MessageDialog1.Text = "Are you sure you want to delete this user?"

        If Guna2MessageDialog1.Show() = DialogResult.No Then
            Return
        End If



        If userType = "Admin" Or userType = "admin" Then
            MessageBox.Show("You can't delete system administrator.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            con.Open()
            cmd = New SqlClient.SqlCommand("DELETE FROM tbluser WHERE ID = @ID", con)
            cmd.Parameters.AddWithValue("@ID", Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString())
            cmd.ExecuteNonQuery()



            ' Success message
            Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
            Guna2MessageDialog1.Icon = MessageDialogIcon.Information
            Guna2MessageDialog1.Text = "User has been deleted successfully."
            Guna2MessageDialog1.Show()

            LoadUserList() ' Refresh the table
        Catch ex As Exception
            ' Error message
            Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
            Guna2MessageDialog1.Icon = MessageDialogIcon.Error
            Guna2MessageDialog1.Text = "Error deleting user: " & ex.Message
            Guna2MessageDialog1.Show()
        Finally
            con.Close()
        End Try
    End Sub



    ' Method to fetch all user data from the database based on the ID
    Private Sub fetchviewuser(userId As Integer)
        ' Clear existing data in the controls
        ClearControls()

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            ' Query to fetch all user details
            cmd = New SqlCommand("SELECT * FROM tbluser WHERE ID = @ID", con)
            cmd.Parameters.AddWithValue("@ID", userId)

            ' Execute the query and read the data
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                ' Combine first name, middle name, and last name
                Dim firstName As String = dr("FirstName").ToString()
                Dim middleName As String = dr("MiddleName").ToString()
                Dim lastName As String = dr("LastName").ToString()
                Dim fullName As String = (firstName & " " & middleName & " " & lastName).Trim()

                ' Fill the controls with data from the database
                With frmviewuser
                    .txtfullname.Text = fullName ' Display the combined full name
                    .txtaddre.Text = dr("Address").ToString()
                    .txtmob.Text = dr("Contact").ToString()
                    .txtemail.Text = dr("EmailAddress").ToString()
                    .txtstatus.Text = dr("Status").ToString()


                    ' Load image if available
                    If Not IsDBNull(dr("imagepath")) Then
                        Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                        Dim img As Image = ByteArrayToImage(imgData)
                        .picture.Image = img
                    Else
                        .picture.Image = Nothing ' Default or placeholder image
                    End If
                    .ShowDialog()
                End With
            End If

            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub


    ' Method to fetch all user data from the database based on the ID
    Private Sub FetchAndDisplayUserData(userId As Integer)
        ' Clear existing data in the controls
        ClearControls()

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            ' Query to fetch all user details
            cmd = New SqlCommand("SELECT * FROM tbluser WHERE ID = @ID", con)
            cmd.Parameters.AddWithValue("@ID", userId)

            ' Execute the query and read the data
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                ' Fill the controls with data from the database
                txtfn.Text = dr("FirstName").ToString()
                txtln.Text = dr("LastName").ToString()
                txtmn.Text = dr("MiddleName").ToString()
                txtuser.Text = dr("Username").ToString()
                combowombo.Text = dr("User_Type").ToString()
                txtadr.Text = dr("Address").ToString()
                txtcont.Text = dr("Contact").ToString()
                txtemail.Text = dr("EmailAddress").ToString()

                ' Password fields (you might want to handle this securely, for now assuming it's directly shown)
                txtpass.Text = dr("Password").ToString()

                ' Load image if available
                If Not IsDBNull(dr("imagepath")) Then
                    Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                    Dim img As Image = ByteArrayToImage(imgData)
                    PictureBox1.Image = img
                Else
                    PictureBox1.Image = Nothing ' Default or placeholder image
                End If
            End If

            dr.Close()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub





    ' Check if required fields are empty
    Private Function AreRequiredFieldsEmpty() As Boolean
        Return String.IsNullOrWhiteSpace(txtfn.Text) OrElse
               String.IsNullOrWhiteSpace(txtln.Text) OrElse
               String.IsNullOrWhiteSpace(txtcont.Text) OrElse
               String.IsNullOrWhiteSpace(txtemail.Text)
    End Function

    ' Validate email format
    Private Function IsValidEmail(email As String) As Boolean
        Dim pattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Return Regex.IsMatch(email, pattern)
    End Function

    ' Reset background colors of all input fields
    Private Sub ResetBackgroundColors()
        txtemail.BackColor = SystemColors.Window
        txtcont.BackColor = SystemColors.Window
        ' Add other text boxes if needed
    End Sub

    ' Ensure contact number TextBox only accepts numeric input
    Private Sub txtcont_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcont.KeyPress
        ' Allow only digits and control characters (e.g., Backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub



    ' Check if the username is unique
    Private Function IsUniqueUsername(username As String) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM tbluser WHERE Username = @Username"
        Using cmd As New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Username", username)
            con.Open()
            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            Return result = 0 ' True if no duplicates found
        End Using
    End Function

    ' if 0 to if hindi sia 0 then may exist

    Private Function IsUniqueUsernameupdate(username As String, userId As Integer) As Boolean
        Dim query As String = "SELECT COUNT(*) FROM tbluser WHERE Username = @Username AND ID <> @UserID"
        Using cmd As New SqlClient.SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Username", username)
            cmd.Parameters.AddWithValue("@UserID", userId) ' Exclude the current user's own record
            con.Open()
            Dim result As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            Return result = 0 ' True if no duplicates found
        End Using
    End Function



    ' Password strength validation

    Private Function IsStrongPassword(password As String) As Boolean
        ' Updated regex to allow either complex passwords or numbers only
        Dim regex As New Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$|^\d{8,}$")
        Return regex.IsMatch(password)
    End Function


    ' Validate that names contain only alphabetic characters
    Private Function IsValidName(name As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(name, "^[A-Za-z\s]+$")
    End Function

    ' Validate contact number format
    Private Function IsValidContactNumber(contact As String) As Boolean
        Return IsNumeric(contact) AndAlso contact.Length = 11 AndAlso contact.StartsWith("09")
    End Function




    Public Sub IS_EMPTY()
        txtfn.Clear()
        txtln.Clear()
        txtmn.Clear()

    End Sub

    Private Sub frmuser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtcont.MaxLength = 11 ' Limit the contact number to 11 characters

        'txtpass.PasswordChar = Chr(149)
        'txtconfirm.PasswordChar = Chr(149)
        LoadUserList()


        ' Set the placeholder text for the ComboBox
        combowombo.Text = "Select a User Type"
        combowombo.ForeColor = Color.Black
        Guna2DataGridView1.AlternatingRowsDefaultCellStyle = Guna2DataGridView1.RowsDefaultCellStyle


        'MsgBox(IsStrongPassword("Password1!")) ' Should show True

    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Me.Dispose()
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs)
        Me.Hide()

    End Sub




    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Me.Close()
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





    'Private Sub PictureBox2_Click(sender As Object, e As EventArgs)
    '    If txtpass.PasswordChar = ControlChars.NullChar Then
    '        ' Hide the password and change icon to "closed eye"
    '        txtpass.PasswordChar = "*"c
    '        PictureBox2.Image = My.Resources.eye ' Replace with your actual closed eye image
    '    Else

    '        txtpass.PasswordChar = ControlChars.NullChar
    '        PictureBox2.Image = Nothing
    '    End If
    'End Sub


    'Private Sub txtpass_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpass.KeyPress
    '    txtpass.PasswordChar = "*"c
    'End Sub

    'Private Sub txtconfirm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtconfirm.KeyPress
    '    txtpass.PasswordChar = "*"c
    'End Sub


    'End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        ' Check if the user navigated to the "Create Account" tab (Tab index 0)
        If TabControl1.SelectedIndex = 0 Then
            ' Clear all controls when the user goes back to the Create Account tab
            ClearControls()
            combowombo.Text = "Cashier"

            ' Set buttons visibility for "Create Account" tab (assuming 'btnsave' is for create account)
            btnupdate.Visible = False
            btnsave.Visible = True
        End If


    End Sub


    Sub LoadUserList()
        Guna2DataGridView1.Rows.Clear()
        Dim i As Integer = 0

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = New SqlCommand("SELECT * FROM tbluser ORDER BY LastName", con)
            dr = cmd.ExecuteReader

            While dr.Read
                i += 1

                ' Combine FirstName, MiddleName, and LastName to form FullName
                Dim fullName As String = String.Format("{0} {1} {2}", dr("FirstName").ToString(), dr("MiddleName").ToString(), dr("LastName").ToString()).Trim()

                ' Retrieve and convert the ImageSpl column
                Dim img As Image = Nothing
                If Not IsDBNull(dr("imagepath")) Then
                    Dim imgData As Byte() = DirectCast(dr("imagepath"), Byte())
                    img = ByteArrayToImage(imgData)
                Else
                    ' Optional: Set a placeholder image if ImageSpl is NULL
                    img = Nothing ' Ensure this resource exists, or replace it
                End If

                ' Add row with FullName and other fields, including img
                Guna2DataGridView1.Rows.Add(dr("ID").ToString(), i, dr("Username").ToString(), dr("User_Type").ToString(), fullName, dr("Status").ToString(), img)
            End While

            dr.Close()

            ' Set row height
            For i = 0 To Guna2DataGridView1.Rows.Count - 1
                Dim r As DataGridViewRow = Guna2DataGridView1.Rows(i)
                r.Height = 75
            Next


            ' Set image layout for the image column
            Dim imageColumn = DirectCast(Guna2DataGridView1.Columns("image"), DataGridViewImageColumn)
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch


            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                If Not row.IsNewRow Then ' Avoid the empty new row at the bottom
                    If row.Cells("UserType").Value.ToString().ToLower() = "admin" Then
                        row.Cells("Delete").ReadOnly = True
                        row.Cells("Delete").Style.ForeColor = Color.Gray
                        row.Cells("Delete").Style.SelectionForeColor = Color.Gray
                    End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading user list: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
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





    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles combowombo.SelectedIndexChanged

    End Sub

    Private Sub combowombo_Leave(sender As Object, e As EventArgs) Handles combowombo.Leave
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub txtcont_TextChanged(sender As Object, e As EventArgs) Handles txtcont.TextChanged

    End Sub

    Private Sub Guna2Button2_Click_1(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Me.Close()

    End Sub

    Private Sub txtfn_TextChanged(sender As Object, e As EventArgs) Handles txtfn.TextChanged

    End Sub

    Private Sub txtconfirm_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtfn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtfn.KeyPress
        ' Allow only letters and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If


    End Sub

    Private Sub txtmn_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmn.KeyPress
        ' Allow only letters and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

    End Sub

    Private Sub txtln_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtln.KeyPress
        ' Allow only letters and backspace
        If Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> Convert.ToChar(Keys.Back) Then
            e.Handled = True
        End If

    End Sub






    ' Disable Ctrl+V paste




    Private Sub txtuser_KeyDown(sender As Object, e As KeyEventArgs) Handles txtuser.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtpass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpass.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtcont_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcont.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtmn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtmn.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtfn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtfn.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtln_KeyDown(sender As Object, e As KeyEventArgs) Handles txtln.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtadr_KeyDown(sender As Object, e As KeyEventArgs) Handles txtadr.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtemail_KeyDown(sender As Object, e As KeyEventArgs) Handles txtemail.KeyDown
        ' Disable Ctrl+V paste
        If e.Control AndAlso e.KeyCode = Keys.V Then
            e.SuppressKeyPress = True
        End If
    End Sub
End Class














'Private Sub ShowUserDetails(rowIndex As Integer)
'    Dim userId As String = Guna2DataGridView1.Rows(rowIndex).Cells(0).Value.ToString()
'    Dim query As String = "SELECT User_Type, Username, Status, Password, imagepath FROM tbluser WHERE ID = @UserID"

'    Try
'        con.Open()
'        cmd = New SqlClient.SqlCommand(query, con)
'        cmd.Parameters.AddWithValue("@UserID", userId)
'        Dim reader As SqlClient.SqlDataReader = cmd.ExecuteReader()

'        If reader.Read() Then
'            With frmviewuser
'                .usertype.Text = reader("User_Type").ToString()
'                .lbluser.Text = reader("Username").ToString()
'                .lblstatus.Text = reader("Status").ToString()
'                .lblpass.Text = reader("Password").ToString()

'                ' Load the image if the file exists
'                Dim imagePath As String = reader("imagepath").ToString()
'                If Not String.IsNullOrEmpty(imagePath) AndAlso System.IO.File.Exists(imagePath) Then
'                    Using img As System.Drawing.Image = System.Drawing.Image.FromFile(imagePath)
'                        .picture.Image = New Bitmap(img) ' Using Bitmap to avoid locking the file
'                    End Using
'                Else
'                    .picture.Image = Nothing ' Clear picture box if image not found
'                End If


'                .ShowDialog()
'            End With
'        End If
'    Catch ex As Exception
'        MessageBox.Show("Error loading user details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'    Finally
'        con.Close()
'    End Try
'End Sub