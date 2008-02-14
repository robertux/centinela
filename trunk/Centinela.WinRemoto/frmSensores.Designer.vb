<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSensores
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
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.lstSensores = New System.Windows.Forms.ListBox
        Me.btnAgregarSen = New System.Windows.Forms.Button
        Me.btnEliminarSen = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lstMapas = New System.Windows.Forms.ListBox
        Me.btnEliminarMpa = New System.Windows.Forms.Button
        Me.btnAgregarMpa = New System.Windows.Forms.Button
        Me.lblNombre = New System.Windows.Forms.Label
        Me.lblTipo = New System.Windows.Forms.Label
        Me.txtNombre = New System.Windows.Forms.TextBox
        Me.cmbTipo = New System.Windows.Forms.ComboBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(15, 381)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(119, 23)
        Me.btnCerrar.TabIndex = 0
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'lstSensores
        '
        Me.lstSensores.FormattingEnabled = True
        Me.lstSensores.HorizontalScrollbar = True
        Me.lstSensores.Location = New System.Drawing.Point(12, 31)
        Me.lstSensores.Name = "lstSensores"
        Me.lstSensores.Size = New System.Drawing.Size(189, 225)
        Me.lstSensores.TabIndex = 13
        '
        'btnAgregarSen
        '
        Me.btnAgregarSen.Location = New System.Drawing.Point(12, 262)
        Me.btnAgregarSen.Name = "btnAgregarSen"
        Me.btnAgregarSen.Size = New System.Drawing.Size(40, 20)
        Me.btnAgregarSen.TabIndex = 14
        Me.btnAgregarSen.Text = "+"
        Me.btnAgregarSen.UseVisualStyleBackColor = True
        '
        'btnEliminarSen
        '
        Me.btnEliminarSen.Location = New System.Drawing.Point(58, 262)
        Me.btnEliminarSen.Name = "btnEliminarSen"
        Me.btnEliminarSen.Size = New System.Drawing.Size(40, 20)
        Me.btnEliminarSen.TabIndex = 15
        Me.btnEliminarSen.Text = "-"
        Me.btnEliminarSen.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Sensores:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(238, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "En mapas:"
        '
        'lstMapas
        '
        Me.lstMapas.FormattingEnabled = True
        Me.lstMapas.HorizontalScrollbar = True
        Me.lstMapas.Location = New System.Drawing.Point(217, 31)
        Me.lstMapas.Name = "lstMapas"
        Me.lstMapas.Size = New System.Drawing.Size(279, 225)
        Me.lstMapas.TabIndex = 18
        '
        'btnEliminarMpa
        '
        Me.btnEliminarMpa.Location = New System.Drawing.Point(263, 262)
        Me.btnEliminarMpa.Name = "btnEliminarMpa"
        Me.btnEliminarMpa.Size = New System.Drawing.Size(40, 20)
        Me.btnEliminarMpa.TabIndex = 20
        Me.btnEliminarMpa.Text = "-"
        Me.btnEliminarMpa.UseVisualStyleBackColor = True
        '
        'btnAgregarMpa
        '
        Me.btnAgregarMpa.Location = New System.Drawing.Point(217, 262)
        Me.btnAgregarMpa.Name = "btnAgregarMpa"
        Me.btnAgregarMpa.Size = New System.Drawing.Size(40, 20)
        Me.btnAgregarMpa.TabIndex = 19
        Me.btnAgregarMpa.Text = "+"
        Me.btnAgregarMpa.UseVisualStyleBackColor = True
        '
        'lblNombre
        '
        Me.lblNombre.AutoSize = True
        Me.lblNombre.Location = New System.Drawing.Point(9, 310)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(47, 13)
        Me.lblNombre.TabIndex = 22
        Me.lblNombre.Text = "Nombre:"
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.Location = New System.Drawing.Point(9, 340)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(31, 13)
        Me.lblTipo.TabIndex = 21
        Me.lblTipo.Text = "Tipo:"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(62, 307)
        Me.txtNombre.MaxLength = 20
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(195, 20)
        Me.txtNombre.TabIndex = 23
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Location = New System.Drawing.Point(62, 337)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(195, 21)
        Me.cmbTipo.TabIndex = 24
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(326, 267)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(168, 145)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 25
        Me.PictureBox1.TabStop = False
        '
        'frmSensores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(508, 416)
        Me.ControlBox = False
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmbTipo)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.lblNombre)
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.btnEliminarMpa)
        Me.Controls.Add(Me.btnAgregarMpa)
        Me.Controls.Add(Me.lstMapas)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEliminarSen)
        Me.Controls.Add(Me.btnAgregarSen)
        Me.Controls.Add(Me.lstSensores)
        Me.Controls.Add(Me.btnCerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmSensores"
        Me.Text = "Sensores"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents lstSensores As System.Windows.Forms.ListBox
    Friend WithEvents btnAgregarSen As System.Windows.Forms.Button
    Friend WithEvents btnEliminarSen As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lstMapas As System.Windows.Forms.ListBox
    Friend WithEvents btnEliminarMpa As System.Windows.Forms.Button
    Friend WithEvents btnAgregarMpa As System.Windows.Forms.Button
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
