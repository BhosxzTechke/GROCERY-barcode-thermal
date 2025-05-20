<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdiscountmodif
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmdiscountmodif))
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.btnupdate = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.btnsave = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.txtdesc = New Guna.UI2.WinForms.Guna2TextBox()
        Me.txtdiscount = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblid = New System.Windows.Forms.Label()
        Me.Guna2Panel1 = New Guna.UI2.WinForms.Guna2Panel()
        Me.Guna2CustomGradientPanel2 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Guna2Panel1.SuspendLayout()
        Me.Guna2CustomGradientPanel2.SuspendLayout()
        Me.SuspendLayout()
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
        Me.btnupdate.BorderRadius = 2
        Me.btnupdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnupdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnupdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnupdate.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnupdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnupdate.FillColor = System.Drawing.Color.Empty
        Me.btnupdate.FillColor2 = System.Drawing.Color.Empty
        Me.btnupdate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnupdate.Location = New System.Drawing.Point(357, 238)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(178, 38)
        Me.btnupdate.TabIndex = 80
        Me.btnupdate.Text = "UPDATE"
        '
        'btnsave
        '
        Me.btnsave.BackColor = System.Drawing.Color.Transparent
        Me.btnsave.BackgroundImage = CType(resources.GetObject("btnsave.BackgroundImage"), System.Drawing.Image)
        Me.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnsave.BorderRadius = 2
        Me.btnsave.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnsave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnsave.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnsave.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnsave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnsave.FillColor = System.Drawing.Color.Empty
        Me.btnsave.FillColor2 = System.Drawing.Color.Empty
        Me.btnsave.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnsave.ForeColor = System.Drawing.Color.White
        Me.btnsave.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.btnsave.Location = New System.Drawing.Point(360, 238)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(178, 38)
        Me.btnsave.TabIndex = 79
        Me.btnsave.Text = "SAVE"
        '
        'txtdesc
        '
        Me.txtdesc.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtdesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtdesc.DefaultText = ""
        Me.txtdesc.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtdesc.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtdesc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtdesc.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtdesc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtdesc.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtdesc.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtdesc.Location = New System.Drawing.Point(20, 53)
        Me.txtdesc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.PlaceholderText = ""
        Me.txtdesc.SelectedText = ""
        Me.txtdesc.Size = New System.Drawing.Size(495, 42)
        Me.txtdesc.TabIndex = 81
        '
        'txtdiscount
        '
        Me.txtdiscount.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtdiscount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtdiscount.DefaultText = ""
        Me.txtdiscount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtdiscount.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtdiscount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtdiscount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtdiscount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtdiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtdiscount.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtdiscount.Location = New System.Drawing.Point(20, 154)
        Me.txtdiscount.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtdiscount.Name = "txtdiscount"
        Me.txtdiscount.PlaceholderText = "Enter discount (1–99%)"
        Me.txtdiscount.SelectedText = ""
        Me.txtdiscount.Size = New System.Drawing.Size(495, 52)
        Me.txtdiscount.TabIndex = 82
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(207, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 32)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Discount"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(207, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(148, 32)
        Me.Label5.TabIndex = 83
        Me.Label5.Text = "Desciption"
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.BackColor = System.Drawing.Color.Transparent
        Me.lblid.ForeColor = System.Drawing.Color.Black
        Me.lblid.Location = New System.Drawing.Point(374, 30)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(32, 16)
        Me.lblid.TabIndex = 85
        Me.lblid.Text = "lblid"
        Me.lblid.Visible = False
        '
        'Guna2Panel1
        '
        Me.Guna2Panel1.BackColor = System.Drawing.Color.White
        Me.Guna2Panel1.Controls.Add(Me.txtdiscount)
        Me.Guna2Panel1.Controls.Add(Me.txtdesc)
        Me.Guna2Panel1.Controls.Add(Me.Guna2CustomGradientPanel2)
        Me.Guna2Panel1.Location = New System.Drawing.Point(-1, 0)
        Me.Guna2Panel1.Name = "Guna2Panel1"
        Me.Guna2Panel1.Size = New System.Drawing.Size(538, 279)
        Me.Guna2Panel1.TabIndex = 84
        '
        'Guna2CustomGradientPanel2
        '
        Me.Guna2CustomGradientPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CustomGradientPanel2.BackColor = System.Drawing.SystemColors.Window
        Me.Guna2CustomGradientPanel2.BackgroundImage = CType(resources.GetObject("Guna2CustomGradientPanel2.BackgroundImage"), System.Drawing.Image)
        Me.Guna2CustomGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.btnsave)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.lblid)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Button10)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Label5)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Label2)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.btnupdate)
        Me.Guna2CustomGradientPanel2.FillColor = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor2 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor3 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor4 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel2.Name = "Guna2CustomGradientPanel2"
        Me.Guna2CustomGradientPanel2.Size = New System.Drawing.Size(538, 279)
        Me.Guna2CustomGradientPanel2.TabIndex = 86
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
        Me.Button10.Location = New System.Drawing.Point(489, 5)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(46, 26)
        Me.Button10.TabIndex = 99
        Me.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button10.UseVisualStyleBackColor = False
        '
        'frmdiscountmodif
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSlateGray
        Me.ClientSize = New System.Drawing.Size(537, 281)
        Me.Controls.Add(Me.Guna2Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmdiscountmodif"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmdiscountmodif"
        Me.Guna2Panel1.ResumeLayout(False)
        Me.Guna2CustomGradientPanel2.ResumeLayout(False)
        Me.Guna2CustomGradientPanel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents Guna2Panel1 As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtdiscount As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents txtdesc As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnsave As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents btnupdate As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents Guna2CustomGradientPanel2 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Button10 As System.Windows.Forms.Button
End Class
