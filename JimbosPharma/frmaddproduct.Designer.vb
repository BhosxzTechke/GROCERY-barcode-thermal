<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmaddproduct
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmaddproduct))
        Me.Guna2BorderlessForm1 = New Guna.UI2.WinForms.Guna2BorderlessForm(Me.components)
        Me.categorycbx = New System.Windows.Forms.ComboBox()
        Me.txtcost = New Guna.UI2.WinForms.Guna2TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ckbarcode = New System.Windows.Forms.CheckBox()
        Me.txtsales = New Guna.UI2.WinForms.Guna2TextBox()
        Me.unitcbx = New System.Windows.Forms.ComboBox()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.unitid = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.itemdes = New Guna.UI2.WinForms.Guna2TextBox()
        Me.labelprice = New System.Windows.Forms.Label()
        Me.Guna2Button1 = New Guna.UI2.WinForms.Guna2Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.labelcateg = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblid = New System.Windows.Forms.Label()
        Me.lblbarcode = New System.Windows.Forms.Label()
        Me.lblclass = New System.Windows.Forms.Label()
        Me.btnsave = New Guna.UI2.WinForms.Guna2Button()
        Me.btnupdate = New Guna.UI2.WinForms.Guna2Button()
        Me.picbarcode = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Guna2Button2 = New Guna.UI2.WinForms.Guna2Button()
        CType(Me.picbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Guna2BorderlessForm1
        '
        Me.Guna2BorderlessForm1.ContainerControl = Me
        Me.Guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6R
        Me.Guna2BorderlessForm1.TransparentWhileDrag = True
        '
        'categorycbx
        '
        Me.categorycbx.FormattingEnabled = True
        Me.categorycbx.Location = New System.Drawing.Point(157, 282)
        Me.categorycbx.Name = "categorycbx"
        Me.categorycbx.Size = New System.Drawing.Size(346, 24)
        Me.categorycbx.TabIndex = 121
        '
        'txtcost
        '
        Me.txtcost.BackColor = System.Drawing.Color.Transparent
        Me.txtcost.BorderColor = System.Drawing.Color.Gray
        Me.txtcost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtcost.DefaultText = ""
        Me.txtcost.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtcost.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtcost.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcost.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtcost.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcost.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtcost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtcost.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtcost.Location = New System.Drawing.Point(157, 388)
        Me.txtcost.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtcost.Name = "txtcost"
        Me.txtcost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtcost.PlaceholderText = ""
        Me.txtcost.SelectedText = ""
        Me.txtcost.Size = New System.Drawing.Size(346, 33)
        Me.txtcost.TabIndex = 120
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(15, 403)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 18)
        Me.Label5.TabIndex = 119
        Me.Label5.Text = "Cost Price:"
        '
        'Button1
        '
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(314, 116)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(189, 34)
        Me.Button1.TabIndex = 118
        Me.Button1.Text = "Generate barcode"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ckbarcode
        '
        Me.ckbarcode.AutoSize = True
        Me.ckbarcode.ForeColor = System.Drawing.Color.White
        Me.ckbarcode.Location = New System.Drawing.Point(561, 82)
        Me.ckbarcode.Name = "ckbarcode"
        Me.ckbarcode.Size = New System.Drawing.Size(172, 21)
        Me.ckbarcode.TabIndex = 117
        Me.ckbarcode.Text = "Saving Barcode Image"
        Me.ckbarcode.UseVisualStyleBackColor = True
        '
        'txtsales
        '
        Me.txtsales.BackColor = System.Drawing.Color.Transparent
        Me.txtsales.BorderColor = System.Drawing.Color.Gray
        Me.txtsales.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtsales.DefaultText = ""
        Me.txtsales.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.txtsales.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.txtsales.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtsales.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.txtsales.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtsales.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtsales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtsales.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtsales.Location = New System.Drawing.Point(157, 458)
        Me.txtsales.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtsales.Name = "txtsales"
        Me.txtsales.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtsales.PlaceholderText = ""
        Me.txtsales.SelectedText = ""
        Me.txtsales.Size = New System.Drawing.Size(346, 33)
        Me.txtsales.TabIndex = 116
        '
        'unitcbx
        '
        Me.unitcbx.FormattingEnabled = True
        Me.unitcbx.Location = New System.Drawing.Point(157, 331)
        Me.unitcbx.Name = "unitcbx"
        Me.unitcbx.Size = New System.Drawing.Size(346, 24)
        Me.unitcbx.TabIndex = 115
        '
        'txtbarcode
        '
        Me.txtbarcode.Location = New System.Drawing.Point(157, 156)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(346, 22)
        Me.txtbarcode.TabIndex = 114
        '
        'unitid
        '
        Me.unitid.AutoSize = True
        Me.unitid.BackColor = System.Drawing.Color.Black
        Me.unitid.ForeColor = System.Drawing.Color.White
        Me.unitid.Location = New System.Drawing.Point(137, 309)
        Me.unitid.Name = "unitid"
        Me.unitid.Size = New System.Drawing.Size(0, 17)
        Me.unitid.TabIndex = 112
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(15, 339)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 18)
        Me.Label4.TabIndex = 111
        Me.Label4.Text = "Unit:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(13, 205)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 18)
        Me.Label3.TabIndex = 110
        Me.Label3.Text = "Item Description:"
        '
        'itemdes
        '
        Me.itemdes.BackColor = System.Drawing.Color.Transparent
        Me.itemdes.BorderColor = System.Drawing.Color.DimGray
        Me.itemdes.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.itemdes.DefaultText = ""
        Me.itemdes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.itemdes.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.itemdes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.itemdes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(138, Byte), Integer))
        Me.itemdes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.itemdes.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.itemdes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.itemdes.HoverState.BorderColor = System.Drawing.Color.FromArgb(CType(CType(94, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.itemdes.Location = New System.Drawing.Point(157, 187)
        Me.itemdes.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.itemdes.Name = "itemdes"
        Me.itemdes.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.itemdes.PlaceholderText = "e.g., ""Paracetamol (Biogesic) 500mg "
        Me.itemdes.SelectedText = ""
        Me.itemdes.Size = New System.Drawing.Size(346, 50)
        Me.itemdes.TabIndex = 109
        '
        'labelprice
        '
        Me.labelprice.AutoSize = True
        Me.labelprice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelprice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.labelprice.Location = New System.Drawing.Point(15, 466)
        Me.labelprice.Name = "labelprice"
        Me.labelprice.Size = New System.Drawing.Size(87, 18)
        Me.labelprice.TabIndex = 108
        Me.labelprice.Text = "Sales Price:"
        '
        'Guna2Button1
        '
        Me.Guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.Guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.Guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.Guna2Button1.FillColor = System.Drawing.Color.Brown
        Me.Guna2Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Guna2Button1.ForeColor = System.Drawing.Color.White
        Me.Guna2Button1.Location = New System.Drawing.Point(561, 494)
        Me.Guna2Button1.Name = "Guna2Button1"
        Me.Guna2Button1.Size = New System.Drawing.Size(308, 27)
        Me.Guna2Button1.TabIndex = 107
        Me.Guna2Button1.Text = "SELECT PHOTO"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(-3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(186, 32)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "Product Entry"
        '
        'labelcateg
        '
        Me.labelcateg.AutoSize = True
        Me.labelcateg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelcateg.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.labelcateg.Location = New System.Drawing.Point(15, 282)
        Me.labelcateg.Name = "labelcateg"
        Me.labelcateg.Size = New System.Drawing.Size(116, 18)
        Me.labelcateg.TabIndex = 105
        Me.labelcateg.Text = "Category Name:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(15, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 18)
        Me.Label2.TabIndex = 102
        Me.Label2.Text = "Barcode:"
        '
        'lblid
        '
        Me.lblid.AutoSize = True
        Me.lblid.BackColor = System.Drawing.Color.Transparent
        Me.lblid.Location = New System.Drawing.Point(15, 116)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(0, 17)
        Me.lblid.TabIndex = 104
        Me.lblid.Visible = False
        '
        'lblbarcode
        '
        Me.lblbarcode.AutoSize = True
        Me.lblbarcode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblbarcode.Location = New System.Drawing.Point(104, 161)
        Me.lblbarcode.Name = "lblbarcode"
        Me.lblbarcode.Size = New System.Drawing.Size(0, 17)
        Me.lblbarcode.TabIndex = 103
        '
        'lblclass
        '
        Me.lblclass.AutoSize = True
        Me.lblclass.ForeColor = System.Drawing.Color.White
        Me.lblclass.Location = New System.Drawing.Point(137, 281)
        Me.lblclass.Name = "lblclass"
        Me.lblclass.Size = New System.Drawing.Size(0, 17)
        Me.lblclass.TabIndex = 101
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
        Me.btnsave.Location = New System.Drawing.Point(767, 556)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(115, 27)
        Me.btnsave.TabIndex = 99
        Me.btnsave.Text = "SUBMIT"
        '
        'btnupdate
        '
        Me.btnupdate.DisabledState.BorderColor = System.Drawing.Color.DarkGray
        Me.btnupdate.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray
        Me.btnupdate.DisabledState.FillColor = System.Drawing.Color.FromArgb(CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(169, Byte), Integer))
        Me.btnupdate.DisabledState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer), CType(CType(141, Byte), Integer))
        Me.btnupdate.FillColor = System.Drawing.Color.Teal
        Me.btnupdate.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnupdate.ForeColor = System.Drawing.Color.White
        Me.btnupdate.Location = New System.Drawing.Point(767, 589)
        Me.btnupdate.Name = "btnupdate"
        Me.btnupdate.Size = New System.Drawing.Size(115, 27)
        Me.btnupdate.TabIndex = 100
        Me.btnupdate.Text = "UPDATE"
        '
        'picbarcode
        '
        Me.picbarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picbarcode.Location = New System.Drawing.Point(561, 110)
        Me.picbarcode.Name = "picbarcode"
        Me.picbarcode.Size = New System.Drawing.Size(308, 160)
        Me.picbarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picbarcode.TabIndex = 113
        Me.picbarcode.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(561, 276)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(308, 212)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 106
        Me.PictureBox1.TabStop = False
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
        Me.Guna2Button2.Location = New System.Drawing.Point(875, 0)
        Me.Guna2Button2.Name = "Guna2Button2"
        Me.Guna2Button2.Size = New System.Drawing.Size(34, 26)
        Me.Guna2Button2.TabIndex = 122
        '
        'frmaddproduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 639)
        Me.Controls.Add(Me.Guna2Button2)
        Me.Controls.Add(Me.categorycbx)
        Me.Controls.Add(Me.txtcost)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ckbarcode)
        Me.Controls.Add(Me.txtsales)
        Me.Controls.Add(Me.unitcbx)
        Me.Controls.Add(Me.txtbarcode)
        Me.Controls.Add(Me.picbarcode)
        Me.Controls.Add(Me.unitid)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.itemdes)
        Me.Controls.Add(Me.labelprice)
        Me.Controls.Add(Me.Guna2Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.labelcateg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblid)
        Me.Controls.Add(Me.lblbarcode)
        Me.Controls.Add(Me.lblclass)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.btnupdate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmaddproduct"
        Me.Text = "frmaddproduct"
        CType(Me.picbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Guna2BorderlessForm1 As Guna.UI2.WinForms.Guna2BorderlessForm
    Friend WithEvents categorycbx As System.Windows.Forms.ComboBox
    Friend WithEvents txtcost As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ckbarcode As System.Windows.Forms.CheckBox
    Friend WithEvents txtsales As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents unitcbx As System.Windows.Forms.ComboBox
    Friend WithEvents txtbarcode As System.Windows.Forms.TextBox
    Friend WithEvents picbarcode As System.Windows.Forms.PictureBox
    Friend WithEvents unitid As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents itemdes As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents labelprice As System.Windows.Forms.Label
    Friend WithEvents Guna2Button1 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents labelcateg As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents lblbarcode As System.Windows.Forms.Label
    Friend WithEvents lblclass As System.Windows.Forms.Label
    Friend WithEvents btnsave As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnupdate As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Button2 As Guna.UI2.WinForms.Guna2Button
End Class
