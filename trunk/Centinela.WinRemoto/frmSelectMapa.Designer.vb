<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectMapa
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
        Me.pbxImagenMapa = New System.Windows.Forms.PictureBox
        Me.grdMapas = New System.Windows.Forms.DataGridView
        Me.colNombre = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnAceptar = New System.Windows.Forms.Button
        CType(Me.pbxImagenMapa, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMapas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbxImagenMapa
        '
        Me.pbxImagenMapa.Location = New System.Drawing.Point(395, 12)
        Me.pbxImagenMapa.Name = "pbxImagenMapa"
        Me.pbxImagenMapa.Size = New System.Drawing.Size(247, 237)
        Me.pbxImagenMapa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxImagenMapa.TabIndex = 0
        Me.pbxImagenMapa.TabStop = False
        '
        'grdMapas
        '
        Me.grdMapas.AllowUserToAddRows = False
        Me.grdMapas.AllowUserToDeleteRows = False
        Me.grdMapas.AllowUserToResizeColumns = False
        Me.grdMapas.AllowUserToResizeRows = False
        Me.grdMapas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.grdMapas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdMapas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNombre})
        Me.grdMapas.Location = New System.Drawing.Point(12, 12)
        Me.grdMapas.Name = "grdMapas"
        Me.grdMapas.ReadOnly = True
        Me.grdMapas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdMapas.Size = New System.Drawing.Size(364, 237)
        Me.grdMapas.TabIndex = 1
        '
        'colNombre
        '
        Me.colNombre.HeaderText = "Nombre"
        Me.colNombre.Name = "colNombre"
        Me.colNombre.ReadOnly = True
        Me.colNombre.Width = 69
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(520, 271)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(122, 23)
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Enabled = False
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(381, 271)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(122, 23)
        Me.btnAceptar.TabIndex = 9
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'frmSelectMapa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 308)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.grdMapas)
        Me.Controls.Add(Me.pbxImagenMapa)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmSelectMapa"
        Me.Text = "Mapas"
        CType(Me.pbxImagenMapa, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMapas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbxImagenMapa As System.Windows.Forms.PictureBox
    Friend WithEvents grdMapas As System.Windows.Forms.DataGridView
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents colNombre As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
End Class
