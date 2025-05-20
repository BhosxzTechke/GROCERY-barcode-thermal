Imports System.Data.SqlClient
Imports Tulpep.NotificationWindow
Imports System.IO
Imports MissionsGrocery.loginform
Imports Guna.UI2.WinForms

Public Class dashboard


    ' Flag to toggle colors
    Private isColorCyan As Boolean = True


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


    Private Sub btncategory_Click(sender As Object, e As EventArgs) Handles btncategory.Click
        'Dim x As Integer = btncategory.Width ' Set the X position to the button's width (right edge)
        'Dim y As Integer = 0 ' Set the Y position to align at the top of the button
        'Guna2ContextMenuStrip2.Show(btncategory, x, y)


        With frmvat
            .txtvat.Text = getvat() * 100
            .ShowDialog()

        End With


    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnaccount.Click
        With frmuser
            .LoadUserList()
            .ShowDialog()

        End With
    End Sub



    Private Sub Button11_Click(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        NotifyCriticalStock()
        'Timer1.Start()

        With frmdashboard
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmdashboard)
            .BringToFront()
            .Show()
        End With

        Me.WindowState = FormWindowState.Maximized
        Me.FormBorderStyle = FormBorderStyle.Sizable

        Timer5.Start()

    End Sub

    Private Sub btnstock_Click(sender As Object, e As EventArgs) Handles btnstock.Click
        ' FORM NI supplier
        With frmsupplierlist
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmsupplierlist)
            .BringToFront()
            .Show()
            '.supplierrecord()
        End With
    End Sub




    Private Sub dashboard_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If

    End Sub





    'Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    tiktok.Text = Date.Now.ToString("hh:mm:ss")
    '    ampm.Text = Date.Now.ToString("tt")

    '    Guna2CircleProgressBar1.Value = Date.Now.ToString("ss")

    '    day.Text = Date.Now.ToString("dddd")
    '    calendar.Text = Date.Now.Date

    'End Sub


    Private Sub Guna2ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Guna2ContextMenuStrip1.Opening
        ' Check the current color mode and update the menu item text
        Command1ToolStripMenuItem.Text = "System Lock"
        Command2ToolStripMenuItem.Text = "Change Password"
        AToolStripMenuItem.Text = "Logout"
    End Sub



    'Private Sub btnew_Click(sender As Object, e As EventArgs) Handles btnew.Click
    '    Guna2ContextMenuStrip1.Show(btnew, 0, btnew.Height)

    'End Sub


    Private Sub AToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AToolStripMenuItem.Click

        logaccount(GlobalUser.UserId, GlobalUser.Username, "LOGOUT", "", "User logged out")

        loginform.Show()

        Me.Dispose()
    End Sub


    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click, Button4.Click

    '    With frmdashboard
    '        .ProductCount()
    '        .Opacity = 0
    '        .TopLevel = False
    '        Panel3.Controls.Add(frmdashboard)
    '        .BringToFront()
    '        .Show()
    '    End With
    'End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub





    Private Sub Command1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Command1ToolStripMenuItem.Click
        With frmlock
            .ShowDialog()        ' LOCK THE SYSTEM
        End With
    End Sub


    Private Sub Command2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Command2ToolStripMenuItem.Click
        ' Assuming you have a Guna2MessageDialog component named 'gunaMessageDialog' on your form

        gunaMessageDialog.Caption = "Confirmation"
        gunaMessageDialog.Text = "Before you change your password, you need to answer a security question. Continue?"
        gunaMessageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question
        gunaMessageDialog.Buttons = MessageDialogButtons.YesNo

        Dim result As DialogResult = gunaMessageDialog.Show()

        If result = DialogResult.Yes Then
            Using passSecurityDialog As New passsecurity()
                passSecurityDialog.StartPosition = FormStartPosition.CenterParent
                passSecurityDialog.ShowDialog(Me)
            End Using
        Else
            gunaMessageDialog.Caption = "Cancelled"
            gunaMessageDialog.Text = "Password change cancelled."
            gunaMessageDialog.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information
            gunaMessageDialog.Buttons = MessageDialogButtons.OK
            gunaMessageDialog.Show()
        End If

    End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs)
    '    With loadchart
    '        .ShowDialog()
    '    End With
    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    '    With frmreporter
    '        .Opacity = 0
    '        .TopLevel = False
    '        Panel3.Controls.Add(frmreporter)
    '        .BringToFront()
    '        .Show()
    '    End With
    'End Sub



    Private Sub Guna2CircleProgressBar2_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
    '    Label1.Text = Now.ToLongDateString
    '    Label2.Text = Now.ToString("hh:mm:ss tt")
    'End Sub

    Private Sub OtherMaintananceToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub




    ' ---------- DELIVERY AND INVENTORY AND RECORDS BUTTON  AND PRODUCT BUTTON HIDING ----------

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        ' FORM NI Delivery
        With frmdeliverylist
            .ShowDialog()
        End With

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        With frminventorylist
            .ShowDialog()

        End With
    End Sub

    Private Sub btnrecords_Click(sender As Object, e As EventArgs)

        With frmrecords
            '.stockinventory()
            .ShowDialog()

        End With

    End Sub


    Private Sub btnproduct_Click(sender As Object, e As EventArgs)


        ' FORM NI PRODUCT
        With frmproductlists
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmproductlists)
            .BringToFront()
            .Show()

        End With


    End Sub


    ' ---------- END OF THIS ' ----------

    Private Sub Timer2_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
        Dim popup = New PopupNotifier
        'popup.Image = My.Resources
        popup.TitleText = "pogi ko"
        popup.ContentText = "ediwaw"
        popup.Popup()

    End Sub



    Sub NotifyCriticalStock()
        Dim underStockCount As Integer = 0
        Dim outOfStockCount As Integer = 0

        Try
            ' Ensure the connection is closed before opening
            If con.State = ConnectionState.Open Then
                con.Close()
            End If

            con.Open()

            ' Query to get details of items at or below the Critical Level
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity, sts.StockLevel, sts.CriticalLevel 
                                       FROM tblInventory AS iv 
                                       INNER JOIN tblproduct AS p ON iv.id = p.id 
                                       INNER JOIN tblStockSettings AS sts ON iv.InventoryID = sts.ID
                                       WHERE iv.Quantity <= sts.CriticalLevel AND iv.Quantity > 0", con)

            Dim underStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for items at or below Critical Level
            While underStockDr.Read()
                Dim productName As String = underStockDr("item_des").ToString()
                Dim quantity As Integer = CInt(underStockDr("Quantity"))
                Dim criticalLevel As Integer = CInt(underStockDr("CriticalLevel"))
                Dim stockLevel As Integer = CInt(underStockDr("StockLevel"))

                ' Alert for items at or below the critical level
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Critical Stock Alert"
                popupNotifier.ContentText = String.Format("Critical Stock Alert: {0} - only {1} left in stock (Critical Level: {2}, Stock Level: {3}).", productName, quantity, criticalLevel, stockLevel)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            underStockDr.Close()

            ' Query to get items where stock is below the Stock Level but above Critical Level
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity, sts.StockLevel 
                                       FROM tblInventory AS iv 
                                       INNER JOIN tblproduct AS p ON iv.id = p.id 
                                       INNER JOIN tblStockSettings AS sts ON iv.InventoryID = sts.ID
                                       WHERE iv.Quantity < sts.StockLevel AND iv.Quantity > sts.CriticalLevel", con)

            Dim lowStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for items below Stock Level but above Critical Level
            While lowStockDr.Read()
                Dim productName As String = lowStockDr("item_des").ToString()
                Dim quantity As Integer = CInt(lowStockDr("Quantity"))
                Dim stockLevel As Integer = CInt(lowStockDr("StockLevel"))

                ' Alert for items below stock level but above critical level
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Low Stock Alert"
                popupNotifier.ContentText = String.Format("Low Stock Alert: {0} - {1} left in stock (Stock Level: {2}).", productName, quantity, stockLevel)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            lowStockDr.Close()

            ' Query to get details of out-of-stock items (quantity = 0)
            cmd = New SqlClient.SqlCommand("SELECT p.item_des, iv.Quantity FROM tblInventory AS iv INNER JOIN tblproduct AS p ON iv.id = p.id WHERE iv.Quantity = 0", con)
            Dim outOfStockDr As SqlClient.SqlDataReader = cmd.ExecuteReader()

            ' Notify for out of stock items
            While outOfStockDr.Read()
                Dim productName As String = outOfStockDr("item_des").ToString()

                ' Alert for items that are out of stock
                Dim popupNotifier As New Tulpep.NotificationWindow.PopupNotifier
                popupNotifier.TitleText = "Out of Stock Alert"
                popupNotifier.ContentText = String.Format("Out of Stock Alert: {0} is currently unavailable.", productName)
                popupNotifier.Delay = 5000 ' Show for 5 seconds
                popupNotifier.ShowCloseButton = True
                popupNotifier.Popup()
            End While
            outOfStockDr.Close()

            con.Close()

        Catch ex As Exception
            MsgBox("Error Notify Critical: " & ex.Message, vbCritical)
        Finally
            ' Close the connection in case it's still open
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub


    Private Sub userpic_Click(sender As Object, e As EventArgs) Handles userpic.Click

    End Sub

    Private Sub Guna2ContextMenuStrip2_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub Guna2Panel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Function GetUserImageData(userId As String) As Byte()
        Throw New NotImplementedException
    End Function

    Private Sub VatToolStripMenuItem_Click_1(sender As Object, e As EventArgs)
        With frmvat
            .txtvat.Text = getvat()
            .ShowDialog()

        End With
    End Sub


    Private Sub DiscountToolStripMenuItem_Click_1(sender As Object, e As EventArgs)
        With frmdiscount
            .loaddiscount()
            .ShowDialog()

        End With
    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click


        With frmcategorylist
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmcategorylist)
            .BringToFront()
            .Show()
        End With

    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        With frmdiscount
            .loaddiscount()
            .ShowDialog()

        End With
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        With frmunitlist
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmunitlist)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub lbluser_Click(sender As Object, e As EventArgs) Handles lbluser.Click

    End Sub

    Private Sub btnsettings_Click(sender As Object, e As EventArgs) Handles btnsettings.Click
        ' Calculate the position to show the context menu on the right side
        Dim x As Integer = btnsettings.Width
        Dim y As Integer = btnsettings.Height / 2
        Guna2ContextMenuStrip1.Show(btnsettings, New Point(x, y))
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs)
        With frmdeliverylist
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmdeliverylist)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs)
        ' FORM NI PRODUCT
        With frmproductlists
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmproductlists)
            .BringToFront()
            .Show()

        End With
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        ' FORM NI PRODUCT
        With frmproductlists
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmproductlists)
            .BringToFront()
            .Show()

        End With
    End Sub





    ' FORM NI INVENTORY AND DELIVERY AND PRODUCT AND AUDIT

    Private Sub btninvent_Click(sender As Object, e As EventArgs) Handles btninvent.Click
        With frminventorylist
            .ShowDialog()

        End With
    End Sub




    Private Sub btndelivery_Click(sender As Object, e As EventArgs) Handles btndelivery.Click
        With frmdeliverylist
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmdeliverylist)
            .BringToFront()
            .Show()
        End With

    End Sub





    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        ' FORM NI PRODUCT
        With frmproductlists
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmproductlists)
            .BringToFront()
            .Show()

        End With
    End Sub

    Private Sub btnaudit_Click(sender As Object, e As EventArgs) Handles btnaudit.Click
        ' FORM NI AUDIT
        With frmaudit
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmaudit)
            .BringToFront()
            .Show()

        End With
    End Sub

    Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click
        With frmrecords
            '.stockinventory()
            .ShowDialog()

        End With
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim settings = GetStockSettings() ' Tuple with StockLevel and CriticalLevel

        With frmlevel
            .txtstock.Text = settings.StockLevel.ToString()
            .txtcritical.Text = settings.CriticalLevel.ToString()
            .ShowDialog()
        End With
    End Sub

    Private Sub btnsetting_Click(sender As Object, e As EventArgs) Handles btnsetting.Click

        logaccount(GlobalUser.UserId, GlobalUser.Username, "LOGOUT", "", "User logged out")

        loginform.Show()

        Me.Dispose()

    End Sub



    Private Sub btnpos_Click(sender As Object, e As EventArgs) Handles btnpos.Click
        With cashier
            .txtsearch.Enabled = False
            .ShowDialog()
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click  ' dashboard
        With frmdashboard
            .ProductCount()
            .Opacity = 0
            .TopLevel = False
            Panel3.Controls.Add(frmdashboard)
            .BringToFront()
            .Show()
        End With
    End Sub

    Private Sub dashboard_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub





    'Private Sub Button5_Click_2(sender As Object, e As EventArgs)
    '    With frmrecords
    '        .ShowDialog()
    '    End With
    'End Sub
End Class
