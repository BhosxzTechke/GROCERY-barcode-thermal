Imports Guna.UI2.WinForms
Imports System.Data.SqlClient
Imports Tulpep.NotificationWindow
Imports System.IO


Public Class loginform
    Public Class GlobalUser
        Public Shared Username As String
        Public Shared Password As String
        Public Shared LastName As String
        Public Shared UserType As String
        Public Shared Status As String
        Public Shared EmailAddress As String
        Public Shared Contact As String
        Public Shared UserId As String
    End Class

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        ' Check if username or password fields are empty
        If Walanglamanuser(txtuser) OrElse Walanglamanuser(txtpass) Then Return

        ' SQL command to select user details
        cmd = New SqlClient.SqlCommand("SELECT * FROM tbluser WHERE Username = @username AND Password = @password", con)
        cmd.Parameters.AddWithValue("@username", txtuser.Text.Trim())
        cmd.Parameters.AddWithValue("@password", txtpass.Text.Trim())

        Try
            ' Open connection and execute command
            con.Open()
            dr = cmd.ExecuteReader()

            If dr.Read() Then
                ' Assign user information to shared properties
                GlobalUser.Username = dr("Username").ToString()
                GlobalUser.Password = dr("Password").ToString()
                GlobalUser.LastName = dr("LastName").ToString()
                GlobalUser.UserType = dr("User_Type").ToString()
                GlobalUser.Status = dr("Status").ToString()
                GlobalUser.EmailAddress = dr("EmailAddress").ToString()
                GlobalUser.Contact = dr("Contact").ToString()
                GlobalUser.UserId = dr("ID").ToString()

                dr.Close()

                ' Check account status
                If GlobalUser.Status.Equals("active", StringComparison.OrdinalIgnoreCase) Then
                    ' Clear login fields
                    txtuser.Clear()
                    txtpass.Clear()

                    ' Handle user based on type
                    HandleUserType()
                Else
                    Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning
                    Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                    Guna2MessageDialog1.Caption = "Access Denied"
                    Guna2MessageDialog1.Text = "Inactive account."
                    Guna2MessageDialog1.Show()

                End If
            Else
                dr.Close()
                Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
                Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
                Guna2MessageDialog1.Caption = "Access Denied"
                Guna2MessageDialog1.Text = "Invalid username or password."
                Guna2MessageDialog1.Show()

            End If

        Catch ex As SqlClient.SqlException
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
            Guna2MessageDialog1.Caption = "Database Error"
            Guna2MessageDialog1.Text = ex.Message
            Guna2MessageDialog1.Show()

        Catch ex As Exception
            Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error
            Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
            Guna2MessageDialog1.Caption = "Unexpected Error"
            Guna2MessageDialog1.Text = ex.Message
            Guna2MessageDialog1.Show()

        Finally
            ' Ensure connection is properly closed
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then con.Close()
        End Try
    End Sub


    Public Sub HandleUserType()
        ' Display a common message for all user types
        Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
        Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK
        Guna2MessageDialog1.Caption = "Access Granted"
        Guna2MessageDialog1.Text = "Welcome " & GlobalUser.LastName
        Guna2MessageDialog1.Show()

        Dim userForm As Form = Nothing


        Select Case GlobalUser.UserType
            Case "Cashier"
                frmsettle.lblsettle.Text = GlobalUser.Username
                'cashier.btnreturn.Visible = False
                frmchangepass.oldpassword.Text = GlobalUser.Password
                'frmchangepass.newpass.Text = GlobalUser.Password
                frmlock.lockpass.Text = GlobalUser.Password ' ✅ Restored line
                frmchangepass.struserrs.Text = GlobalUser.Username
                LoadImage(cashier.userpic, GlobalUser.UserId)
                'cashier.btnreturns.Visible = False
                cashier.btnlogouts.Visible = True
                cashier.txtsearch.Enabled = False
                cashier.lblcashier.Text = GlobalUser.Username


                'hide others button
                dashboard.btndelivery.Visible = False

                dashboard.btnaccount.Visible = False
                dashboard.btninvent.Visible = False
                dashboard.Button2.Visible = False
                dashboard.btnstock.Visible = False
                dashboard.Button7.Visible = False
                dashboard.btncategory.Visible = False
                dashboard.Button6.Visible = False
                dashboard.btnaudit.Visible = False
                dashboard.btnaccount.Visible = False
                dashboard.btnsettings.Visible = False
                dashboard.Button5.Visible = False
                dashboard.Button8.Visible = False
                dashboard.Button3.Visible = False


                dashboard.lbluser.Text = GlobalUser.Username
                LoadImage(dashboard.userpic, GlobalUser.UserId)


                logaccount(GlobalUser.UserId, GlobalUser.Username, "LOGIN", "", "User logged in")
                userForm = dashboard

            Case "Staff"
                frmstaffboard.lbluser.Text = "Welcome, " & GlobalUser.LastName
                frmstockadjustment.txtadjusted.Text = GlobalUser.Username
                frmsettle.lblsettle.Text = GlobalUser.Username
                LoadImage(frmstaffboard.userpic, GlobalUser.UserId)
                'cashier.btnreturn.Visible = False
                frmqty.lblname.Text = GlobalUser.Username
                frmchangepass.oldpassword.Text = GlobalUser.Password
                frmlock.lockpass.Text = GlobalUser.Password ' ✅ Restored line
                frmstockadjustment.txtadjusted.Text = GlobalUser.Username
                LoadImage(frmstaffboard.userpic, GlobalUser.UserId)
                'cashier.btnreturns.Visible = True
                cashier.btnlogouts.Visible = False
                userForm = frmstaffboard

            Case Else ' Admin or other users
                frmqty.lblname.Text = GlobalUser.Username
                frmsettle.lblsettle.Text = GlobalUser.Username
                dashboard.lbluser.Text = GlobalUser.Username
                LoadImage(dashboard.userpic, GlobalUser.UserId)

                'hide button
                dashboard.btnsetting.Visible = False
                dashboard.btnpos.Visible = False



                logaccount(GlobalUser.UserId, GlobalUser.Username, "LOGIN", "", "User logged in")
                userForm = dashboard
        End Select

        ' Hide login form before showing the user form
        If userForm IsNot Nothing Then
            Me.Hide() ' Hide the login form
            userForm.ShowDialog() ' Show the user form as a modal
        End If
    End Sub


    Private Sub LoadImage(pictureBox As PictureBox, userId As String)


        Dim imageData As Byte() = GetUserImageData(userId)
        Try
            If imageData IsNot Nothing Then
                Using ms As New MemoryStream(imageData)
                    pictureBox.Image = Image.FromStream(ms)
                End Using
            Else
                pictureBox.Image = Nothing ' Clear the image if no data is available
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading image: " & ex.Message)
        End Try



    End Sub

    Private Function GetUserImageData(userId As String) As Byte()
        Dim imageData As Byte() = Nothing
        Dim query As String = "SELECT imagepath FROM tbluser WHERE ID = @userId" ' imagepath stores the binary data

        Using cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@userId", userId)

            Try
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If

                Dim result = cmd.ExecuteScalar()
                If result IsNot Nothing AndAlso TypeOf result Is Byte() Then
                    imageData = CType(result, Byte())
                End If
            Catch ex As Exception
                MessageBox.Show("Error retrieving image data: " & ex.Message)
            Finally
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
            End Try
        End Using

        Return imageData
    End Function


    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs)
        Application.Exit()

    End Sub


    Private Sub loginform_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter Then
            Guna2GradientButton1.PerformClick() ' Simulate a click  if ENTER on the login button

        End If
    End Sub


    Private Sub Button10_Click_1(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub Guna2GradientButton2_Click_1(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        Application.Exit()
    End Sub

    Private Sub txtpass_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpass.KeyDown
        txtpass.PasswordChar = "*"c

    End Sub




    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub


    'Private Sub loginform_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Dim licenseFilePath As String = "C:\Project\JimbosPharma\license.txt"

    '    If Not File.Exists(licenseFilePath) Then
    '        Guna2MessageDialog1.Caption = "Error"
    '        Guna2MessageDialog1.Text = "License file missing! Contact Antiks."
    '        Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
    '        Guna2MessageDialog1.Icon = MessageDialogIcon.Error
    '        Guna2MessageDialog1.Show()

    '        Application.Exit()
    '        Return
    '    End If

    '    ' Read license file
    '    Dim licenseLines() As String = File.ReadAllLines(licenseFilePath)
    '    Dim expirationDate As Date

    '    For Each line As String In licenseLines
    '        If line.StartsWith("Expiration=") Then
    '            expirationDate = Date.Parse(line.Split("="c)(1))
    '        End If
    '    Next

    '    ' Check expiration
    '    If Date.Now > expirationDate Then
    '        Dim gunaDialog As New Guna2MessageDialog()
    '        Guna2MessageDialog1.Caption = "License Expired"
    '        Guna2MessageDialog1.Text = "Your license has expired. Please contact Antiks."
    '        Guna2MessageDialog1.Buttons = MessageDialogButtons.OK
    '        Guna2MessageDialog1.Icon = MessageDialogIcon.Warning
    '        Guna2MessageDialog1.Show()

    '        Application.Exit()
    '    End If
    'End Sub




    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub



    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If txtpass.PasswordChar = ControlChars.NullChar Then
            ' Hide the password and change icon to "closed eye"
            txtpass.PasswordChar = "*"c
            PictureBox1.Image = My.Resources.invisible ' Replace with your actual closed eye image
        Else
            ' Show the password and change icon to "open eye"
            txtpass.PasswordChar = ControlChars.NullChar
            PictureBox1.Image = My.Resources.visible ' Replace with your actual open eye image
        End If
    End Sub

    Private Sub txtuser_TextChanged(sender As Object, e As EventArgs) Handles txtuser.TextChanged

    End Sub
End Class