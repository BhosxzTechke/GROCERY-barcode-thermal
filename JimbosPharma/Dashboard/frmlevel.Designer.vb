<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlevel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmlevel))
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnsave = New Guna.UI2.WinForms.Guna2Button()
        Me.txtcritical = New System.Windows.Forms.TextBox()
        Me.txtstock = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        Me.searchboxing = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rc1 = New Guna.UI2.WinForms.Guna2HtmlLabel()
        Me.btnew = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Black
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.Controls.Add(Me.btnsave)
        Me.Panel3.Controls.Add(Me.txtcritical)
        Me.Panel3.Controls.Add(Me.txtstock)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Guna2Button2)
        Me.Panel3.Controls.Add(Me.searchboxing)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.btnew)
        Me.Panel3.Location = New System.Drawing.Point(-303, -187)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(926, 502)
        Me.Panel3.TabIndex = 13
        '
        'btnsave
        '
        Me.btnsave.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnsave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnsave.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnsave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnsave.FillColor = System.Drawing.Color.Teal
        Me.btnsave.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnsave.ForeColor = System.Drawing.Color.White
        Me.btnsave.Location = New System.Drawing.Point(672, 461)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(219, 27)
        Me.btnsave.TabIndex = 89
        Me.btnsave.Text = "Save"
        '
        'txtcritical
        '
        Me.txtcritical.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcritical.Location = New System.Drawing.Point(501, 356)
        Me.txtcritical.Name = "txtcritical"
        Me.txtcritical.Size = New System.Drawing.Size(314, 27)
        Me.txtcritical.TabIndex = 87
        '
        'txtstock
        '
        Me.txtstock.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstock.Location = New System.Drawing.Point(501, 263)
        Me.txtstock.Name = "txtstock"
        Me.txtstock.Size = New System.Drawing.Size(314, 27)
        Me.txtstock.TabIndex = 87
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(370, 364)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(95, 18)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Critical Level:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(370, 272)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 18)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "Stock Level:"
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
        Me.Guna2Button2.Location = New System.Drawing.Point(885, 186)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.Size = New System.Drawing.Size(34, 26)
        Me.Guna2Button2.TabIndex = 84
        '
        'searchboxing
        '
        Me.searchboxing.BackColor = System.Drawing.Color.Transparent
        Me.searchboxing.BorderColor = System.Drawing.Color.Transparent
        Me.searchboxing.BorderRadius = 1
        Me.searchboxing.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.searchboxing.DefaultText = ""
        Me.searchboxing.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.searchboxing.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.searchboxing.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.searchboxing.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.searchboxing.FillColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.searchboxing.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.searchboxing.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.searchboxing.ForeColor = System.Drawing.Color.Black
        Me.searchboxing.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.searchboxing.IconLeft = CType(resources.GetObject("searchboxing.IconLeft"), System.Drawing.Image)
        Me.searchboxing.Location = New System.Drawing.Point(12, 109)
        Me.searchboxing.Margin = New System.Windows.Forms.Padding(4)
        Me.searchboxing.Name = "searchboxing"
        Me.searchboxing.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(125, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(149, Byte), Integer))
        Me.searchboxing.PlaceholderText = "Search Unit"
        Me.searchboxing.SelectedText = ""
        Me.searchboxing.Size = New System.Drawing.Size(369, 24)
        Me.searchboxing.TabIndex = 49
        Me.searchboxing.WordWrap = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(632, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(175, 32)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Manage Unit"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.rc1)
        Me.Panel5.Location = New System.Drawing.Point(1159, 561)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(138, 23)
        Me.Panel5.TabIndex = 23
        '
        'rc1
        '
        Me.rc1.BackColor = System.Drawing.Color.Transparent
        Me.rc1.ForeColor = System.Drawing.Color.Black
        Me.rc1.Location = New System.Drawing.Point(22, 4)
        Me.rc1.Name = "rc1"
        Me.rc1.Size = New System.Drawing.Size(76, 18)
        Me.rc1.TabIndex = 17
        Me.rc1.Text = "No Records"
        '
        'btnew
        '
        Me.btnew.BackColor = System.Drawing.Color.Honeydew
        Me.btnew.BackgroundImage = CType(resources.GetObject("btnew.BackgroundImage"), System.Drawing.Image)
        Me.btnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnew.FlatAppearance.BorderSize = 0
        Me.btnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnew.ForeColor = System.Drawing.Color.White
        Me.btnew.Location = New System.Drawing.Point(1124, 109)
        Me.btnew.Margin = New System.Windows.Forms.Padding(4)
        Me.btnew.Name = "btnew"
        Me.btnew.Size = New System.Drawing.Size(173, 30)
        Me.btnew.TabIndex = 8
        Me.btnew.Text = "New Unit"
        Me.btnew.UseVisualStyleBackColor = False
        '
        'frmlevel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 313)
        Me.Controls.Add(Me.Panel3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmlevel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmlevel"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents Panel3 As Panel
    Friend WithEvents searchboxing As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents rc1 As Guna.UI2.WinForms.Guna2HtmlLabel
    Friend WithEvents btnew As Button
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents txtcritical As TextBox
    Friend WithEvents txtstock As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnsave As Guna.UI2.WinForms.Guna2Button
End Class
