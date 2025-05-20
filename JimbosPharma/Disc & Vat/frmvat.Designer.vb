<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmvat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmvat))
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.Guna2CustomGradientPanel2 = New Guna.UI2.WinForms.Guna2CustomGradientPanel()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.savebtn = New Guna.UI2.WinForms.Guna2GradientButton()
        Me.txtvat = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Guna2CustomGradientPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.ResizeForm = False
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'Guna2CustomGradientPanel2
        '
        Me.Guna2CustomGradientPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Guna2CustomGradientPanel2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2CustomGradientPanel2.BackgroundImage = CType(resources.GetObject("Guna2CustomGradientPanel2.BackgroundImage"), System.Drawing.Image)
        Me.Guna2CustomGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Button10)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.Label5)
        Me.Guna2CustomGradientPanel2.Controls.Add(Me.savebtn)
        Me.Guna2CustomGradientPanel2.FillColor = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor2 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor3 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.FillColor4 = System.Drawing.Color.Empty
        Me.Guna2CustomGradientPanel2.Location = New System.Drawing.Point(0, 0)
        Me.Guna2CustomGradientPanel2.Name = "Guna2CustomGradientPanel2"
        Me.Guna2CustomGradientPanel2.Size = New System.Drawing.Size(450, 232)
        Me.Guna2CustomGradientPanel2.TabIndex = 76
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
        Me.Button10.Location = New System.Drawing.Point(405, 3)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(42, 26)
        Me.Button10.TabIndex = 99
        Me.Button10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button10.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(13, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 46)
        Me.Label5.TabIndex = 68
        Me.Label5.Text = "VAT"
        '
        'savebtn
        '
        Me.savebtn.BackColor = System.Drawing.Color.Transparent
        Me.savebtn.BackgroundImage = CType(resources.GetObject("savebtn.BackgroundImage"), System.Drawing.Image)
        Me.savebtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.savebtn.BorderRadius = 50
        Me.savebtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot
        Me.savebtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.savebtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.savebtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.savebtn.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.savebtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.savebtn.FillColor = System.Drawing.Color.Empty
        Me.savebtn.FillColor2 = System.Drawing.Color.Empty
        Me.savebtn.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.savebtn.ForeColor = System.Drawing.Color.White
        Me.savebtn.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.savebtn.Location = New System.Drawing.Point(259, 181)
        Me.savebtn.Name = "savebtn"
        Me.savebtn.Size = New System.Drawing.Size(180, 41)
        Me.savebtn.TabIndex = 66
        Me.savebtn.Text = "SAVE"
        '
        'txtvat
        '
        Me.txtvat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtvat.DefaultText = ""
        Me.txtvat.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtvat.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtvat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtvat.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtvat.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtvat.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtvat.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtvat.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtvat.Location = New System.Drawing.Point(139, 63)
        Me.txtvat.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtvat.Name = "txtvat"
        Me.txtvat.PlaceholderText = ""
        Me.txtvat.SelectedText = ""
        Me.txtvat.Size = New System.Drawing.Size(278, 61)
        Me.txtvat.TabIndex = 67
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Controls.Add(Me.txtvat)
        Me.Panel1.Controls.Add(Me.Guna2CustomGradientPanel2)
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(450, 235)
        Me.Panel1.TabIndex = 70
        '
        'frmvat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkSlateGray
        Me.ClientSize = New System.Drawing.Size(450, 233)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmvat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmvat"
        Me.Guna2CustomGradientPanel2.ResumeLayout(False)
        Me.Guna2CustomGradientPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtvat As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Guna2CustomGradientPanel2 As Guna.UI2.WinForms.Guna2CustomGradientPanel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents savebtn As Guna.UI2.WinForms.Guna2GradientButton
    Friend WithEvents Button10 As System.Windows.Forms.Button
End Class
