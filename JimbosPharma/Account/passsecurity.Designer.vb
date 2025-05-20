<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class passsecurity
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(passsecurity))
        Me.cmbquestion = New Guna.UI2.WinForms.Guna2ComboBox()
        Me.txtanswer = New Guna.UI2.WinForms.Guna2TextBox()
        Me.btnVerifys = New Guna.UI2.WinForms.Guna2Button()
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        Me.SuspendLayout()
        '
        'cmbquestion
        '
        Me.cmbquestion.BackColor = System.Drawing.Color.Transparent
        Me.cmbquestion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbquestion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbquestion.FocusedColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbquestion.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cmbquestion.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cmbquestion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(88, Byte), Integer), CType(CType(112, Byte), Integer))
        Me.cmbquestion.ItemHeight = 30
        Me.cmbquestion.Location = New System.Drawing.Point(57, 68)
        Me.cmbquestion.Name = "cmbquestion"
        Me.cmbquestion.Size = New System.Drawing.Size(418, 36)
        Me.cmbquestion.TabIndex = 0
        '
        'txtanswer
        '
        Me.txtanswer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtanswer.DefaultText = ""
        Me.txtanswer.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtanswer.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtanswer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtanswer.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtanswer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtanswer.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtanswer.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtanswer.Location = New System.Drawing.Point(57, 130)
        Me.txtanswer.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtanswer.Name = "txtanswer"
        Me.txtanswer.PlaceholderText = ""
        Me.txtanswer.SelectedText = ""
        Me.txtanswer.Size = New System.Drawing.Size(418, 48)
        Me.txtanswer.TabIndex = 1
        '
        'btnVerifys
        '
        Me.btnVerifys.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnVerifys.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnVerifys.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnVerifys.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnVerifys.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnVerifys.ForeColor = System.Drawing.Color.White
        Me.btnVerifys.Location = New System.Drawing.Point(57, 220)
        Me.btnVerifys.Name = "btnVerifys"
        Me.btnVerifys.Size = New System.Drawing.Size(418, 45)
        Me.btnVerifys.TabIndex = 2
        Me.btnVerifys.Text = "Submit"
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'Guna2Button2
        '
        Me.Guna2Button2.BackColor = System.Drawing.Color.Transparent
        Me.Guna2Button2.BackgroundImage = CType(resources.GetObject("Guna2Button2.BackgroundImage"), System.Drawing.Image)
        Me.Guna2Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Guna2Button2.FillColor = System.Drawing.Color.Transparent
        Me.Guna2Button2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button2.ForeColor = System.Drawing.Color.Transparent
        Me.Guna2Button2.Location = New System.Drawing.Point(481, 0)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.Size = New System.Drawing.Size(40, 36)
        Me.Guna2Button2.TabIndex = 78
        '
        'passsecurity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 284)
        Me.Controls.Add(Me.Guna2Button2)
        Me.Controls.Add(Me.btnVerifys)
        Me.Controls.Add(Me.txtanswer)
        Me.Controls.Add(Me.cmbquestion)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "passsecurity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "passsecurity"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cmbquestion As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents txtanswer As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnVerifys As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
End Class
