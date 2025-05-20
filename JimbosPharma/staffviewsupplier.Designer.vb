<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class staffviewsupplier
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
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.picture = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.lblcomp = New System.Windows.Forms.Label()
        Me.lblcontact = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcomp = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txttel = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtcontact = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtmobile = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Maroon
        Me.Panel1.Location = New System.Drawing.Point(697, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(39, 29)
        Me.Panel1.TabIndex = 0
        '
        'picture
        '
        Me.picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picture.ImageRotate = 0.0!
        Me.picture.Location = New System.Drawing.Point(37, 47)
        Me.picture.Name = "picture"
        Me.picture.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.picture.Size = New System.Drawing.Size(216, 186)
        Me.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picture.TabIndex = 1
        Me.picture.TabStop = False
        '
        'lblcomp
        '
        Me.lblcomp.AutoSize = True
        Me.lblcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcomp.Location = New System.Drawing.Point(102, 278)
        Me.lblcomp.Name = "lblcomp"
        Me.lblcomp.Size = New System.Drawing.Size(151, 25)
        Me.lblcomp.TabIndex = 2
        Me.lblcomp.Text = "Company name"
        '
        'lblcontact
        '
        Me.lblcontact.AutoSize = True
        Me.lblcontact.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblcontact.Location = New System.Drawing.Point(462, 278)
        Me.lblcontact.Name = "lblcontact"
        Me.lblcontact.Size = New System.Drawing.Size(147, 25)
        Me.lblcontact.TabIndex = 3
        Me.lblcontact.Text = "Contact Person"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(102, 388)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(141, 25)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Telephone No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(462, 388)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Mobile no:"
        '
        'txtcomp
        '
        Me.txtcomp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcomp.DefaultText = ""
        Me.txtcomp.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtcomp.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtcomp.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcomp.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcomp.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcomp.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtcomp.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcomp.Location = New System.Drawing.Point(107, 325)
        Me.txtcomp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtcomp.Name = "txtcomp"
        Me.txtcomp.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcomp.PlaceholderText = ""
        Me.txtcomp.ReadOnly = True
        Me.txtcomp.SelectedText = ""
        Me.txtcomp.Size = New System.Drawing.Size(229, 37)
        Me.txtcomp.TabIndex = 6
        '
        'txttel
        '
        Me.txttel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txttel.DefaultText = ""
        Me.txttel.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txttel.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txttel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txttel.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txttel.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txttel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txttel.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txttel.Location = New System.Drawing.Point(107, 446)
        Me.txttel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txttel.Name = "txttel"
        Me.txttel.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txttel.PlaceholderText = ""
        Me.txttel.ReadOnly = True
        Me.txttel.SelectedText = ""
        Me.txttel.Size = New System.Drawing.Size(229, 37)
        Me.txttel.TabIndex = 7
        '
        'txtcontact
        '
        Me.txtcontact.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcontact.DefaultText = ""
        Me.txtcontact.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtcontact.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtcontact.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcontact.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcontact.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcontact.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtcontact.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcontact.Location = New System.Drawing.Point(467, 325)
        Me.txtcontact.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtcontact.Name = "txtcontact"
        Me.txtcontact.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcontact.PlaceholderText = ""
        Me.txtcontact.ReadOnly = True
        Me.txtcontact.SelectedText = ""
        Me.txtcontact.Size = New System.Drawing.Size(229, 37)
        Me.txtcontact.TabIndex = 8
        '
        'txtmobile
        '
        Me.txtmobile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtmobile.DefaultText = ""
        Me.txtmobile.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtmobile.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtmobile.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtmobile.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtmobile.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtmobile.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtmobile.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtmobile.Location = New System.Drawing.Point(467, 446)
        Me.txtmobile.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtmobile.Name = "txtmobile"
        Me.txtmobile.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtmobile.PlaceholderText = ""
        Me.txtmobile.ReadOnly = True
        Me.txtmobile.SelectedText = ""
        Me.txtmobile.Size = New System.Drawing.Size(229, 37)
        Me.txtmobile.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(299, 208)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 25)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Contact Person"
        '
        'staffviewsupplier
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(748, 549)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtmobile)
        Me.Controls.Add(Me.txtcontact)
        Me.Controls.Add(Me.txttel)
        Me.Controls.Add(Me.txtcomp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblcontact)
        Me.Controls.Add(Me.lblcomp)
        Me.Controls.Add(Me.picture)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "staffviewsupplier"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "staffviewsupplier"
        CType(Me.picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblcontact As System.Windows.Forms.Label
    Friend WithEvents lblcomp As System.Windows.Forms.Label
    Friend WithEvents picture As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents txtcomp As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtmobile As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtcontact As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txttel As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
