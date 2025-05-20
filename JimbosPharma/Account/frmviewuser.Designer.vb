<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmviewuser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmviewuser))
        Me.Guna2Elipse1 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2Elipse2 = New Guna.UI2.WinForms.Guna2Elipse(Me.components)
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        Me.fullname = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.usertype = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lbluser = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblpass = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.lblstatus = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.picture = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtstatus = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtemail = New System.Windows.Forms.Label()
        Me.txtmob = New System.Windows.Forms.Label()
        Me.txtaddre = New System.Windows.Forms.Label()
        Me.txtfullname = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2Elipse1
        '
        Me.Guna2Elipse1.BorderRadius = 45
        Me.Guna2Elipse1.TargetControl = Me
        '
        'Guna2Elipse2
        '
        Me.Guna2Elipse2.BorderRadius = 65
        Me.Guna2Elipse2.TargetControl = Me.Guna2Button2
        '
        'Guna2Button2
        '
        Me.Guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Guna2Button2.FillColor = System.Drawing.Color.Firebrick
        Me.Guna2Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button2.ForeColor = System.Drawing.Color.White
        Me.Guna2Button2.Location = New System.Drawing.Point(484, 3)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.Size = New System.Drawing.Size(44, 28)
        Me.Guna2Button2.TabIndex = 75
        '
        'fullname
        '
        Me.fullname.BackColor = System.Drawing.Color.Transparent
        Me.fullname.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fullname.ForeColor = System.Drawing.Color.DimGray
        Me.fullname.Location = New System.Drawing.Point(79, 218)
        Me.fullname.Name = "fullname"
        Me.fullname.Size = New System.Drawing.Size(81, 24)
        Me.fullname.TabIndex = 0
        Me.fullname.Text = "Full name"
        '
        'usertype
        '
        Me.usertype.BackColor = System.Drawing.Color.Transparent
        Me.usertype.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.usertype.ForeColor = System.Drawing.Color.DimGray
        Me.usertype.Location = New System.Drawing.Point(353, 218)
        Me.usertype.Name = "usertype"
        Me.usertype.Size = New System.Drawing.Size(87, 24)
        Me.usertype.TabIndex = 2
        Me.usertype.Text = "User Type"
        '
        'lbluser
        '
        Me.lbluser.BackColor = System.Drawing.Color.Transparent
        Me.lbluser.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbluser.ForeColor = System.Drawing.Color.DimGray
        Me.lbluser.Location = New System.Drawing.Point(75, 348)
        Me.lbluser.Name = "lbluser"
        Me.lbluser.Size = New System.Drawing.Size(85, 24)
        Me.lbluser.TabIndex = 3
        Me.lbluser.Text = "Username"
        '
        'lblpass
        '
        Me.lblpass.BackColor = System.Drawing.Color.Transparent
        Me.lblpass.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpass.ForeColor = System.Drawing.Color.DimGray
        Me.lblpass.Location = New System.Drawing.Point(353, 348)
        Me.lblpass.Name = "lblpass"
        Me.lblpass.Size = New System.Drawing.Size(82, 24)
        Me.lblpass.TabIndex = 4
        Me.lblpass.Text = "Password"
        '
        'lblstatus
        '
        Me.lblstatus.BackColor = System.Drawing.Color.Transparent
        Me.lblstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblstatus.ForeColor = System.Drawing.Color.DimGray
        Me.lblstatus.Location = New System.Drawing.Point(172, 124)
        Me.lblstatus.Name = "lblstatus"
        Me.lblstatus.Size = New System.Drawing.Size(54, 24)
        Me.lblstatus.TabIndex = 76
        Me.lblstatus.Text = "Status"
        '
        'picture
        '
        Me.picture.BackColor = System.Drawing.Color.Transparent
        Me.picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picture.Image = CType(resources.GetObject("picture.Image"), System.Drawing.Image)
        Me.picture.ImageRotate = 0!
        Me.picture.Location = New System.Drawing.Point(24, 30)
        Me.picture.Name = "picture"
        Me.picture.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        Me.picture.Size = New System.Drawing.Size(130, 118)
        Me.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picture.TabIndex = 1
        Me.picture.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.Controls.Add(Me.txtstatus)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtemail)
        Me.Panel1.Controls.Add(Me.txtmob)
        Me.Panel1.Controls.Add(Me.txtaddre)
        Me.Panel1.Controls.Add(Me.txtfullname)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Guna2Button2)
        Me.Panel1.Controls.Add(Me.picture)
        Me.Panel1.Location = New System.Drawing.Point(-3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(540, 472)
        Me.Panel1.TabIndex = 0
        '
        'txtstatus
        '
        Me.txtstatus.AutoSize = True
        Me.txtstatus.BackColor = System.Drawing.Color.Transparent
        Me.txtstatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtstatus.Location = New System.Drawing.Point(161, 408)
        Me.txtstatus.Name = "txtstatus"
        Me.txtstatus.Size = New System.Drawing.Size(81, 16)
        Me.txtstatus.TabIndex = 90
        Me.txtstatus.Text = "Full Name:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(21, 408)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(61, 18)
        Me.Label5.TabIndex = 89
        Me.Label5.Text = "Status:"
        '
        'txtemail
        '
        Me.txtemail.AutoSize = True
        Me.txtemail.BackColor = System.Drawing.Color.Transparent
        Me.txtemail.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtemail.Location = New System.Drawing.Point(161, 354)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(81, 16)
        Me.txtemail.TabIndex = 88
        Me.txtemail.Text = "Full Name:"
        '
        'txtmob
        '
        Me.txtmob.AutoSize = True
        Me.txtmob.BackColor = System.Drawing.Color.Transparent
        Me.txtmob.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmob.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtmob.Location = New System.Drawing.Point(161, 294)
        Me.txtmob.Name = "txtmob"
        Me.txtmob.Size = New System.Drawing.Size(81, 16)
        Me.txtmob.TabIndex = 87
        Me.txtmob.Text = "Full Name:"
        '
        'txtaddre
        '
        Me.txtaddre.AutoSize = True
        Me.txtaddre.BackColor = System.Drawing.Color.Transparent
        Me.txtaddre.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtaddre.Location = New System.Drawing.Point(161, 241)
        Me.txtaddre.Name = "txtaddre"
        Me.txtaddre.Size = New System.Drawing.Size(81, 16)
        Me.txtaddre.TabIndex = 86
        Me.txtaddre.Text = "Full Name:"
        '
        'txtfullname
        '
        Me.txtfullname.AutoSize = True
        Me.txtfullname.BackColor = System.Drawing.Color.Transparent
        Me.txtfullname.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfullname.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtfullname.Location = New System.Drawing.Point(161, 189)
        Me.txtfullname.Name = "txtfullname"
        Me.txtfullname.Size = New System.Drawing.Size(81, 16)
        Me.txtfullname.TabIndex = 85
        Me.txtfullname.Text = "Full Name:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 356)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(121, 18)
        Me.Label4.TabIndex = 84
        Me.Label4.Text = "Email Address:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 296)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 18)
        Me.Label3.TabIndex = 83
        Me.Label3.Text = "Mobile no:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 242)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 18)
        Me.Label2.TabIndex = 82
        Me.Label2.Text = "Address:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 190)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 18)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Full Name:"
        '
        'frmviewuser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSlateGray
        Me.ClientSize = New System.Drawing.Size(537, 460)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmviewuser"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmviewuser"
        CType(Me.picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2Elipse1 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents Guna2Elipse2 As Guna.UI2.WinForms.Guna2Elipse
    Friend WithEvents fullname As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents usertype As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lbluser As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblpass As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents lblstatus As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents picture As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtstatus As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtemail As System.Windows.Forms.Label
    Friend WithEvents txtmob As System.Windows.Forms.Label
    Friend WithEvents txtaddre As System.Windows.Forms.Label
    Friend WithEvents txtfullname As System.Windows.Forms.Label
End Class
