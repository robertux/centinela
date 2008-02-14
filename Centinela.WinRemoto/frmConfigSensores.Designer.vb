<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigSensores
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.nudFrecuencia = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSenDesconectado = New System.Windows.Forms.TextBox
        Me.btnSenDesconectado = New System.Windows.Forms.Button
        Me.btnSenDesactivado = New System.Windows.Forms.Button
        Me.txtSenDesactivado = New System.Windows.Forms.TextBox
        Me.btnSenActivado = New System.Windows.Forms.Button
        Me.txtSenActivado = New System.Windows.Forms.TextBox
        Me.pbxImagenSensor = New System.Windows.Forms.PictureBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtAlarmaSen = New System.Windows.Forms.TextBox
        Me.btnAlarmaSen = New System.Windows.Forms.Button
        CType(Me.nudFrecuencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbxImagenSensor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(379, 312)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(112, 23)
        Me.btnCerrar.TabIndex = 0
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(203, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Frecuencia de lectura del puerto paralelo:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(154, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Imagen Sensor Desconectado:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 146)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Imagen Sensor Desactivado:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Imagen Sensor Activado:"
        '
        'nudFrecuencia
        '
        Me.nudFrecuencia.Location = New System.Drawing.Point(241, 27)
        Me.nudFrecuencia.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.nudFrecuencia.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudFrecuencia.Name = "nudFrecuencia"
        Me.nudFrecuencia.Size = New System.Drawing.Size(120, 20)
        Me.nudFrecuencia.TabIndex = 5
        Me.nudFrecuencia.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(367, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "milisegundos"
        '
        'txtSenDesconectado
        '
        Me.txtSenDesconectado.Location = New System.Drawing.Point(193, 119)
        Me.txtSenDesconectado.Name = "txtSenDesconectado"
        Me.txtSenDesconectado.ReadOnly = True
        Me.txtSenDesconectado.Size = New System.Drawing.Size(259, 20)
        Me.txtSenDesconectado.TabIndex = 7
        '
        'btnSenDesconectado
        '
        Me.btnSenDesconectado.Location = New System.Drawing.Point(458, 117)
        Me.btnSenDesconectado.Name = "btnSenDesconectado"
        Me.btnSenDesconectado.Size = New System.Drawing.Size(25, 23)
        Me.btnSenDesconectado.TabIndex = 8
        Me.btnSenDesconectado.Text = "..."
        Me.btnSenDesconectado.UseVisualStyleBackColor = True
        '
        'btnSenDesactivado
        '
        Me.btnSenDesactivado.Location = New System.Drawing.Point(458, 144)
        Me.btnSenDesactivado.Name = "btnSenDesactivado"
        Me.btnSenDesactivado.Size = New System.Drawing.Size(25, 23)
        Me.btnSenDesactivado.TabIndex = 10
        Me.btnSenDesactivado.Text = "..."
        Me.btnSenDesactivado.UseVisualStyleBackColor = True
        '
        'txtSenDesactivado
        '
        Me.txtSenDesactivado.Location = New System.Drawing.Point(193, 146)
        Me.txtSenDesactivado.Name = "txtSenDesactivado"
        Me.txtSenDesactivado.ReadOnly = True
        Me.txtSenDesactivado.Size = New System.Drawing.Size(259, 20)
        Me.txtSenDesactivado.TabIndex = 9
        '
        'btnSenActivado
        '
        Me.btnSenActivado.Location = New System.Drawing.Point(458, 170)
        Me.btnSenActivado.Name = "btnSenActivado"
        Me.btnSenActivado.Size = New System.Drawing.Size(25, 23)
        Me.btnSenActivado.TabIndex = 12
        Me.btnSenActivado.Text = "..."
        Me.btnSenActivado.UseVisualStyleBackColor = True
        '
        'txtSenActivado
        '
        Me.txtSenActivado.Location = New System.Drawing.Point(193, 172)
        Me.txtSenActivado.Name = "txtSenActivado"
        Me.txtSenActivado.ReadOnly = True
        Me.txtSenActivado.Size = New System.Drawing.Size(259, 20)
        Me.txtSenActivado.TabIndex = 11
        '
        'pbxImagenSensor
        '
        Me.pbxImagenSensor.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.pbxImagenSensor.Location = New System.Drawing.Point(176, 210)
        Me.pbxImagenSensor.Name = "pbxImagenSensor"
        Me.pbxImagenSensor.Size = New System.Drawing.Size(154, 125)
        Me.pbxImagenSensor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbxImagenSensor.TabIndex = 13
        Me.pbxImagenSensor.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(223, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "Sodino de Alarma de Activaicon de Sensores:"
        '
        'txtAlarmaSen
        '
        Me.txtAlarmaSen.Location = New System.Drawing.Point(241, 60)
        Me.txtAlarmaSen.Name = "txtAlarmaSen"
        Me.txtAlarmaSen.ReadOnly = True
        Me.txtAlarmaSen.Size = New System.Drawing.Size(211, 20)
        Me.txtAlarmaSen.TabIndex = 15
        '
        'btnAlarmaSen
        '
        Me.btnAlarmaSen.Location = New System.Drawing.Point(458, 57)
        Me.btnAlarmaSen.Name = "btnAlarmaSen"
        Me.btnAlarmaSen.Size = New System.Drawing.Size(25, 23)
        Me.btnAlarmaSen.TabIndex = 16
        Me.btnAlarmaSen.Text = "..."
        Me.btnAlarmaSen.UseVisualStyleBackColor = True
        '
        'frmConfigSensores
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 357)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnAlarmaSen)
        Me.Controls.Add(Me.txtAlarmaSen)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.pbxImagenSensor)
        Me.Controls.Add(Me.btnSenActivado)
        Me.Controls.Add(Me.txtSenActivado)
        Me.Controls.Add(Me.btnSenDesactivado)
        Me.Controls.Add(Me.txtSenDesactivado)
        Me.Controls.Add(Me.btnSenDesconectado)
        Me.Controls.Add(Me.txtSenDesconectado)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.nudFrecuencia)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmConfigSensores"
        Me.Text = "Configurar Sensores"
        CType(Me.nudFrecuencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbxImagenSensor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nudFrecuencia As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSenDesconectado As System.Windows.Forms.TextBox
    Friend WithEvents btnSenDesconectado As System.Windows.Forms.Button
    Friend WithEvents btnSenDesactivado As System.Windows.Forms.Button
    Friend WithEvents txtSenDesactivado As System.Windows.Forms.TextBox
    Friend WithEvents btnSenActivado As System.Windows.Forms.Button
    Friend WithEvents txtSenActivado As System.Windows.Forms.TextBox
    Friend WithEvents pbxImagenSensor As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAlarmaSen As System.Windows.Forms.TextBox
    Friend WithEvents btnAlarmaSen As System.Windows.Forms.Button
End Class
