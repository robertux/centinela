<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaster
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    	Me.components = New System.ComponentModel.Container
    	Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaster))
    	Me.srvPuertos = New Centinela.Master.Servicio(Me.components)
    	Me.btnComenzar = New System.Windows.Forms.Button
    	Me.btnDetener = New System.Windows.Forms.Button
    	Me.txtLog = New System.Windows.Forms.TextBox
    	Me.SuspendLayout
    	'
    	'srvPuertos
    	'
    	Me.srvPuertos.Control = CType(resources.GetObject("srvPuertos.Control"),System.Collections.Generic.List(Of Short))
    	Me.srvPuertos.Datos = CType(resources.GetObject("srvPuertos.Datos"),System.Collections.Generic.List(Of Short))
    	Me.srvPuertos.Interval = 2000
    	Me.srvPuertos.Status = CType(resources.GetObject("srvPuertos.Status"),System.Collections.Generic.List(Of Short))
    	'
    	'btnComenzar
    	'
    	Me.btnComenzar.Location = New System.Drawing.Point(12, 12)
    	Me.btnComenzar.Name = "btnComenzar"
    	Me.btnComenzar.Size = New System.Drawing.Size(75, 23)
    	Me.btnComenzar.TabIndex = 0
    	Me.btnComenzar.Text = "Comenzar"
    	Me.btnComenzar.UseVisualStyleBackColor = true
    	'
    	'btnDetener
    	'
    	Me.btnDetener.Location = New System.Drawing.Point(93, 12)
    	Me.btnDetener.Name = "btnDetener"
    	Me.btnDetener.Size = New System.Drawing.Size(75, 23)
    	Me.btnDetener.TabIndex = 0
    	Me.btnDetener.Text = "Detener"
    	Me.btnDetener.UseVisualStyleBackColor = true
    	'
    	'txtLog
    	'
    	Me.txtLog.Location = New System.Drawing.Point(12, 41)
    	Me.txtLog.Multiline = true
    	Me.txtLog.Name = "txtLog"
    	Me.txtLog.Size = New System.Drawing.Size(239, 74)
    	Me.txtLog.TabIndex = 1
    	'
    	'frmMaster
    	'
    	Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
    	Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    	Me.ClientSize = New System.Drawing.Size(349, 127)
    	Me.Controls.Add(Me.txtLog)
    	Me.Controls.Add(Me.btnDetener)
    	Me.Controls.Add(Me.btnComenzar)
    	Me.Name = "frmMaster"
    	Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    	Me.Text = "Centinela, Servicio Maestro."
    	Me.ResumeLayout(false)
    	Me.PerformLayout
    End Sub
    Friend WithEvents srvPuertos As Centinela.Master.Servicio
    Friend WithEvents btnComenzar As System.Windows.Forms.Button
    Friend WithEvents btnDetener As System.Windows.Forms.Button
    Friend WithEvents txtLog As System.Windows.Forms.TextBox

End Class
