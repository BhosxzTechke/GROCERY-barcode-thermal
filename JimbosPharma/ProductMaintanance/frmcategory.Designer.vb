<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcategory
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcategory))
        Me.btnsave = New System.Windows.Forms.Button()
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.btnupdate = New System.Windows.Forms.Button()
        Me.lblID = New System.Windows.Forms.Label()
        Me.txtcategory = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Guna2CustomGradientPanel2 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Guna2MessageDialog1 = New Guna.UI2.WinForms.Guna2MessageDialog()
        Me.Guna2CustomGradientPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.Transparent
        Me.btnsave.BackgroundImage = CType(resources.GetObject("btnsave.BackgroundImage"), System.Drawing.Image)
        Me.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnsave.FlatAppearance.BorderSize = 0
        Me.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsave.ForeColor = System.Drawing.Color.White
        Me.btnsave.Location = New System.Drawing.Point(260, 182)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(130, 40)
        Me.btnsave.TabIndex = 4
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = False
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.ResizeForm = False
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'btnupdate
        '
        Me.btnupdate.BackColor = System.Drawing.Color.Transparent
        Me.btnupdate.BackgroundImage = CType(resources.GetObject("btnupdate.BackgroundImage"), System.Drawing.Image)
        Me.btnupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnupdate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnupdate.FlatAppearance.BorderSize = 0
        Me.btnupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.Location = New System.Drawing.Point(260, 183)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(130, 40)
        Me.btnupdate.TabIndex = 6
        Me.btnupdate.Text = "Update"
        Me.btnupdate.UseVisualStyleBackColor = False
        '
        'lblID
        '
        Me.lblID.AutoSize = True
        Me.lblID.Location = New System.Drawing.Point(10, 192)
        Me.lblID.Name = "lblID"
        Me.lblID.Size = New System.Drawing.Size(59, 20)
        Me.lblID.TabIndex = 8
        Me.lblID.Text = "Label2"
        Me.lblID.Visible = False
        '
        'txtcategory
        '
        Me.txtcategory.BorderColor = System.Drawing.Color.Gray
        Me.txtcategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcategory.DefaultText = ""
        Me.txtcategory.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtcategory.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtcategory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcategory.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcategory.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtcategory.ForeColor = System.Drawing.Color.Black
        Me.txtcategory.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcategory.Location = New System.Drawing.Point(10, 101)
        Me.txtcategory.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtcategory.Name = "txtcategory"
        Me.txtcategory.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcategory.PlaceholderForeColor = System.Drawing.Color.Black
        Me.txtcategory.PlaceholderText = ""
        Me.txtcategory.SelectedText = ""
        Me.txtcategory.Size = New System.Drawing.Size(384, 47)
        Me.txtcategory.TabIndex = 79
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(10, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(147, 29)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "CATEGORY"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(10, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 20)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Label2"
        Me.Label1.Visible = False
        '
        'Guna2CustomGradientPanel2
        '
        Me.Guna2CustomGradientPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CustomGradientPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2CustomGradientPanel2.BackgroundImage = CType(resources.GetObject("Guna2CustomGradientPanel2.BackgroundImage"), System.Drawing.Image)
        Me.Guna2CustomGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Guna2Button1)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.txtcategory)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Button10)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Label2)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.btnupdate)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.btnsave)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.lblID)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Label1)
        Me.Guna2CustomGradientPanel2.FillColor = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor2 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor3 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor4 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.Location = New System.Drawing.Point(2, -1)
        Me.Guna2CustomGradientPanel2.Name = "Guna2CustomGradientPanel2"
        Me.Guna2CustomGradientPanel2.Size = New System.Drawing.Size(403, 241)
        Me.Guna2CustomGradientPanel2.TabIndex = 80
        '
        'Guna2Button1
        '
        Me.Guna2Button1.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Button1.BackgroundImage = CType(resources.GetObject("Guna2Button1.BackgroundImage"), System.Drawing.Image)
        Me.Guna2Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Guna2Button1.FillColor = System.Drawing.Color.Transparent
        Me.Guna2Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button1.ForeColor = System.Drawing.Color.Transparent
        Me.Guna2Button1.Location = New System.Drawing.Point(366, 3)
        Me.Guna2Button1.Name = "Guna2Button1"
        Me.Guna2Button1.Size = New System.Drawing.Size(34, 26)
        Me.Guna2Button1.TabIndex = 100
        '
        'Button10
        '
        Me.Button10.BackColor = System.Drawing.Color.Transparent
        Me.Button10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button10.FlatAppearance.BorderSize = 0
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Font = New System.Drawing.Font("Century Gothic", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button10.Image = CType(resources.GetObject("Button10.Image"), System.Drawing.Image)
        Me.Button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button10.Location = New System.Drawing.Point(416, 0)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(34, 26)
        Me.Button10.TabIndex = 99
        Me.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Guna2MessageDialog1
        '
        Me.Guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo
        Me.Guna2MessageDialog1.Caption = Nothing
        Me.Guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.None
        Me.Guna2MessageDialog1.Parent = Me
        Me.Guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Light
        Me.Guna2MessageDialog1.Text = Nothing
        '
        'frmcategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(404, 242)
        Me.Controls.Add(Me.Guna2CustomGradientPanel2)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmcategory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmcategory"
        Me.Guna2CustomGradientPanel2.ResumeLayout(False)
        Me.Guna2CustomGradientPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents btnupdate As System.Windows.Forms.Button
    Friend WithEvents lblID As System.Windows.Forms.Label
    Friend WithEvents txtcategory As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Guna2CustomGradientPanel2 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Guna2MessageDialog1 As Guna.UI2.WinForms.Guna2MessageDialog
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
End Class
