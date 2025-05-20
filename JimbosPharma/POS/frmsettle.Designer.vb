<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmsettle
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
        Me.lbltotalitem = New System.Windows.Forms.Label()
        Me.lblchange = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtcash = New Guna.UI2.WinForms.Guna2TextBox()
        Me.lblsettle = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'lbltotalitem
        '
        Me.lbltotalitem.Font = New System.Drawing.Font("Segoe UI Symbol", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalitem.ForeColor = System.Drawing.Color.Lime
        Me.lbltotalitem.Location = New System.Drawing.Point(1, 0)
        Me.lbltotalitem.Name = "lbltotalitem"
        Me.lbltotalitem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbltotalitem.Size = New System.Drawing.Size(737, 73)
        Me.lbltotalitem.TabIndex = 0
        Me.lbltotalitem.Text = "0.00"
        Me.lbltotalitem.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblchange
        '
        Me.lblchange.Font = New System.Drawing.Font("Segoe UI Symbol", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblchange.ForeColor = System.Drawing.Color.White
        Me.lblchange.Location = New System.Drawing.Point(1, 217)
        Me.lblchange.Name = "lblchange"
        Me.lblchange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblchange.Size = New System.Drawing.Size(737, 73)
        Me.lblchange.TabIndex = 2
        Me.lblchange.Text = "0.00"
        Me.lblchange.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(12, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 46)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Total"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(12, 134)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(113, 46)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Cash"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(12, 244)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 46)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Change"
        '
        'txtcash
        '
        Me.txtcash.BorderThickness = 0
        Me.txtcash.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcash.DefaultText = ""
        Me.txtcash.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtcash.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtcash.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcash.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcash.FillColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.txtcash.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcash.Font = New System.Drawing.Font("Segoe UI Symbol", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcash.ForeColor = System.Drawing.Color.White
        Me.txtcash.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcash.Location = New System.Drawing.Point(145, 109)
        Me.txtcash.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtcash.Name = "txtcash"
        Me.txtcash.PlaceholderForeColor = System.Drawing.Color.Black
        Me.txtcash.PlaceholderText = ""
        Me.txtcash.SelectedText = ""
        Me.txtcash.Size = New System.Drawing.Size(582, 71)
        Me.txtcash.TabIndex = 7
        Me.txtcash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblsettle
        '
        Me.lblsettle.AutoSize = True
        Me.lblsettle.Location = New System.Drawing.Point(168, 0)
        Me.lblsettle.Name = "lblsettle"
        Me.lblsettle.Size = New System.Drawing.Size(48, 16)
        Me.lblsettle.TabIndex = 8
        Me.lblsettle.Text = "Label1"
        Me.lblsettle.Visible = False
        '
        'frmsettle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(43, Byte), Integer), CType(CType(67, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(739, 299)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblsettle)
        Me.Controls.Add(Me.txtcash)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblchange)
        Me.Controls.Add(Me.lbltotalitem)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmsettle"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmsettle"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents txtcash As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblchange As System.Windows.Forms.Label
    Friend WithEvents lbltotalitem As System.Windows.Forms.Label
    Friend WithEvents lblsettle As System.Windows.Forms.Label
End Class
