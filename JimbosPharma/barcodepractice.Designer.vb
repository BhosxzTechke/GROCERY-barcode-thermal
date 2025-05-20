<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class barcodepractice
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
        Me.txt = New System.Windows.Forms.TextBox()
        Me.btngenerate = New System.Windows.Forms.Button()
        Me.btnsave = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.picbarcode = New System.Windows.Forms.PictureBox()
        CType(Me.picbarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txt
        '
        Me.txt.Location = New System.Drawing.Point(79, 97)
        Me.txt.Name = "txt"
        Me.txt.Size = New System.Drawing.Size(459, 22)
        Me.txt.TabIndex = 0
        '
        'btngenerate
        '
        Me.btngenerate.Location = New System.Drawing.Point(79, 125)
        Me.btngenerate.Name = "btngenerate"
        Me.btngenerate.Size = New System.Drawing.Size(122, 53)
        Me.btngenerate.TabIndex = 1
        Me.btngenerate.Text = "Generate"
        Me.btngenerate.UseVisualStyleBackColor = True
        '
        'btnsave
        '
        Me.btnsave.Location = New System.Drawing.Point(253, 126)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(122, 53)
        Me.btnsave.TabIndex = 2
        Me.btnsave.Text = "Save"
        Me.btnsave.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(416, 125)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(122, 53)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Clear"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'picbarcode
        '
        Me.picbarcode.Location = New System.Drawing.Point(79, 209)
        Me.picbarcode.Name = "picbarcode"
        Me.picbarcode.Size = New System.Drawing.Size(459, 224)
        Me.picbarcode.TabIndex = 4
        Me.picbarcode.TabStop = False
        '
        'barcodepractice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 445)
        Me.Controls.Add(Me.picbarcode)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.btngenerate)
        Me.Controls.Add(Me.txt)
        Me.Name = "barcodepractice"
        Me.Text = "barcodepractice"
        CType(Me.picbarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt As System.Windows.Forms.TextBox
    Friend WithEvents btngenerate As System.Windows.Forms.Button
    Friend WithEvents btnsave As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents picbarcode As System.Windows.Forms.PictureBox
End Class
