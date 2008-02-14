'
' Created by SharpDevelop.
' User: _
' Date: 01/01/2001
' Time: 0:35
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class frmUsuarios
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
        Me.grdUsuarios = New System.Windows.Forms.DataGridView
        Me.colNombreCompleto = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnAgregar = New System.Windows.Forms.Button
        Me.btnEliminar = New System.Windows.Forms.Button
        Me.btnHorario = New System.Windows.Forms.Button
        CType(Me.grdUsuarios, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdUsuarios
        '
        Me.grdUsuarios.AllowUserToAddRows = False
        Me.grdUsuarios.AllowUserToDeleteRows = False
        Me.grdUsuarios.AllowUserToResizeColumns = False
        Me.grdUsuarios.AllowUserToResizeRows = False
        Me.grdUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.grdUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdUsuarios.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNombreCompleto})
        Me.grdUsuarios.Location = New System.Drawing.Point(12, 12)
        Me.grdUsuarios.MultiSelect = False
        Me.grdUsuarios.Name = "grdUsuarios"
        Me.grdUsuarios.ReadOnly = True
        Me.grdUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdUsuarios.Size = New System.Drawing.Size(427, 224)
        Me.grdUsuarios.TabIndex = 0
        '
        'colNombreCompleto
        '
        Me.colNombreCompleto.HeaderText = "Nombre Completo"
        Me.colNombreCompleto.Name = "colNombreCompleto"
        Me.colNombreCompleto.ReadOnly = True
        Me.colNombreCompleto.Width = 106
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(463, 213)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(116, 23)
        Me.btnCerrar.TabIndex = 1
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(463, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(116, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Cambiar Clave..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnAgregar
        '
        Me.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregar.Location = New System.Drawing.Point(463, 12)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(116, 23)
        Me.btnAgregar.TabIndex = 3
        Me.btnAgregar.Text = "Agregar..."
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnEliminar
        '
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(463, 41)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(116, 23)
        Me.btnEliminar.TabIndex = 4
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = True
        '
        'btnHorario
        '
        Me.btnHorario.Location = New System.Drawing.Point(463, 131)
        Me.btnHorario.Name = "btnHorario"
        Me.btnHorario.Size = New System.Drawing.Size(116, 23)
        Me.btnHorario.TabIndex = 5
        Me.btnHorario.Text = "Horario de Acceso..."
        Me.btnHorario.UseVisualStyleBackColor = True
        '
        'frmUsuarios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 248)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnHorario)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.grdUsuarios)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmUsuarios"
        Me.Text = "Usuarios"
        CType(Me.grdUsuarios, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents grdUsuarios As System.Windows.Forms.DataGridView
    Private WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents colNombreCompleto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents btnHorario As System.Windows.Forms.Button
End Class
