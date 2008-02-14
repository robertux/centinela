<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.menuStrip1 = New System.Windows.Forms.MenuStrip
        Me.aplicacionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.salirToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.CerrarSesionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.configuracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SensoresToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.administracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.usuariosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MapasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SensoresToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReporteDiarioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReporteMensualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReporteAnualToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReporteTotalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportePorSensorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lblSensores = New System.Windows.Forms.Label
        Me.lblMapa = New System.Windows.Forms.Label
        Me.lstSensores = New System.Windows.Forms.ListBox
        Me.lblTipo = New System.Windows.Forms.Label
        Me.lblPosicion = New System.Windows.Forms.Label
        Me.lblEstado = New System.Windows.Forms.Label
        Me.cmbMapas = New System.Windows.Forms.ComboBox
        Me.grpSensores = New System.Windows.Forms.GroupBox
        Me.tmrVerificarUsuario = New System.Windows.Forms.Timer(Me.components)
        Me.lblPin = New System.Windows.Forms.Label
        Me.srvPuertos = New Centinela.WinApp.Servicio(Me.components)
        Me.menuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuStrip1
        '
        Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.aplicacionToolStripMenuItem, Me.configuracionToolStripMenuItem, Me.administracionToolStripMenuItem, Me.ReportesToolStripMenuItem})
        Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.menuStrip1.Name = "menuStrip1"
        Me.menuStrip1.Size = New System.Drawing.Size(686, 24)
        Me.menuStrip1.TabIndex = 5
        Me.menuStrip1.Text = "menuStrip1"
        '
        'aplicacionToolStripMenuItem
        '
        Me.aplicacionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.salirToolStripMenuItem, Me.ToolStripSeparator1, Me.CerrarSesionToolStripMenuItem})
        Me.aplicacionToolStripMenuItem.Name = "aplicacionToolStripMenuItem"
        Me.aplicacionToolStripMenuItem.Size = New System.Drawing.Size(66, 20)
        Me.aplicacionToolStripMenuItem.Text = "Aplicacion"
        '
        'salirToolStripMenuItem
        '
        Me.salirToolStripMenuItem.Name = "salirToolStripMenuItem"
        Me.salirToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.salirToolStripMenuItem.Text = "Salir"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(159, 6)
        '
        'CerrarSesionToolStripMenuItem
        '
        Me.CerrarSesionToolStripMenuItem.Name = "CerrarSesionToolStripMenuItem"
        Me.CerrarSesionToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.CerrarSesionToolStripMenuItem.Text = "Cerrar Sesion..."
        '
        'configuracionToolStripMenuItem
        '
        Me.configuracionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SensoresToolStripMenuItem1})
        Me.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem"
        Me.configuracionToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.configuracionToolStripMenuItem.Text = "Configuracion"
        '
        'SensoresToolStripMenuItem1
        '
        Me.SensoresToolStripMenuItem1.Name = "SensoresToolStripMenuItem1"
        Me.SensoresToolStripMenuItem1.Size = New System.Drawing.Size(141, 22)
        Me.SensoresToolStripMenuItem1.Text = "Sensores..."
        '
        'administracionToolStripMenuItem
        '
        Me.administracionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.usuariosToolStripMenuItem, Me.MapasToolStripMenuItem, Me.SensoresToolStripMenuItem})
        Me.administracionToolStripMenuItem.Name = "administracionToolStripMenuItem"
        Me.administracionToolStripMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.administracionToolStripMenuItem.Text = "Administracion"
        '
        'usuariosToolStripMenuItem
        '
        Me.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem"
        Me.usuariosToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.usuariosToolStripMenuItem.Text = "Usuarios..."
        '
        'MapasToolStripMenuItem
        '
        Me.MapasToolStripMenuItem.Name = "MapasToolStripMenuItem"
        Me.MapasToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.MapasToolStripMenuItem.Text = "Mapas..."
        '
        'SensoresToolStripMenuItem
        '
        Me.SensoresToolStripMenuItem.Name = "SensoresToolStripMenuItem"
        Me.SensoresToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.SensoresToolStripMenuItem.Text = "Sensores..."
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReporteDiarioToolStripMenuItem, Me.ReporteMensualToolStripMenuItem, Me.ReporteAnualToolStripMenuItem, Me.ReporteTotalToolStripMenuItem, Me.ReportePorSensorToolStripMenuItem})
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        Me.ReportesToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.ReportesToolStripMenuItem.Text = "Reportes"
        '
        'ReporteDiarioToolStripMenuItem
        '
        Me.ReporteDiarioToolStripMenuItem.Name = "ReporteDiarioToolStripMenuItem"
        Me.ReporteDiarioToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReporteDiarioToolStripMenuItem.Text = "Reporte de hoy..."
        '
        'ReporteMensualToolStripMenuItem
        '
        Me.ReporteMensualToolStripMenuItem.Name = "ReporteMensualToolStripMenuItem"
        Me.ReporteMensualToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReporteMensualToolStripMenuItem.Text = "Reporte de este mes..."
        '
        'ReporteAnualToolStripMenuItem
        '
        Me.ReporteAnualToolStripMenuItem.Name = "ReporteAnualToolStripMenuItem"
        Me.ReporteAnualToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReporteAnualToolStripMenuItem.Text = "Reporte de este año..."
        '
        'ReporteTotalToolStripMenuItem
        '
        Me.ReporteTotalToolStripMenuItem.Name = "ReporteTotalToolStripMenuItem"
        Me.ReporteTotalToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReporteTotalToolStripMenuItem.Text = "Reporte total..."
        '
        'ReportePorSensorToolStripMenuItem
        '
        Me.ReportePorSensorToolStripMenuItem.Name = "ReportePorSensorToolStripMenuItem"
        Me.ReportePorSensorToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ReportePorSensorToolStripMenuItem.Text = "Reporte por sensor..."
        '
        'lblSensores
        '
        Me.lblSensores.AutoSize = True
        Me.lblSensores.BackColor = System.Drawing.SystemColors.Control
        Me.lblSensores.Location = New System.Drawing.Point(12, 27)
        Me.lblSensores.Name = "lblSensores"
        Me.lblSensores.Size = New System.Drawing.Size(54, 13)
        Me.lblSensores.TabIndex = 10
        Me.lblSensores.Text = "Sensores:"
        '
        'lblMapa
        '
        Me.lblMapa.AutoSize = True
        Me.lblMapa.BackColor = System.Drawing.SystemColors.Control
        Me.lblMapa.Location = New System.Drawing.Point(221, 27)
        Me.lblMapa.Name = "lblMapa"
        Me.lblMapa.Size = New System.Drawing.Size(37, 13)
        Me.lblMapa.TabIndex = 11
        Me.lblMapa.Text = "Mapa:"
        '
        'lstSensores
        '
        Me.lstSensores.FormattingEnabled = True
        Me.lstSensores.HorizontalScrollbar = True
        Me.lstSensores.Location = New System.Drawing.Point(0, 43)
        Me.lstSensores.Name = "lstSensores"
        Me.lstSensores.Size = New System.Drawing.Size(205, 225)
        Me.lstSensores.TabIndex = 12
        '
        'lblTipo
        '
        Me.lblTipo.AutoSize = True
        Me.lblTipo.Location = New System.Drawing.Point(10, 280)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(31, 13)
        Me.lblTipo.TabIndex = 13
        Me.lblTipo.Text = "Tipo:"
        '
        'lblPosicion
        '
        Me.lblPosicion.AutoSize = True
        Me.lblPosicion.Location = New System.Drawing.Point(10, 303)
        Me.lblPosicion.Name = "lblPosicion"
        Me.lblPosicion.Size = New System.Drawing.Size(50, 13)
        Me.lblPosicion.TabIndex = 14
        Me.lblPosicion.Text = "Posicion:"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Location = New System.Drawing.Point(10, 332)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(43, 13)
        Me.lblEstado.TabIndex = 15
        Me.lblEstado.Text = "Estado:"
        '
        'cmbMapas
        '
        Me.cmbMapas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMapas.FormattingEnabled = True
        Me.cmbMapas.Location = New System.Drawing.Point(211, 43)
        Me.cmbMapas.Name = "cmbMapas"
        Me.cmbMapas.Size = New System.Drawing.Size(367, 21)
        Me.cmbMapas.TabIndex = 18
        '
        'grpSensores
        '
        Me.grpSensores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.grpSensores.Location = New System.Drawing.Point(211, 70)
        Me.grpSensores.Name = "grpSensores"
        Me.grpSensores.Size = New System.Drawing.Size(200, 100)
        Me.grpSensores.TabIndex = 19
        Me.grpSensores.TabStop = False
        '
        'tmrVerificarUsuario
        '
        Me.tmrVerificarUsuario.Enabled = True
        Me.tmrVerificarUsuario.Interval = 30000
        '
        'lblPin
        '
        Me.lblPin.AutoSize = True
        Me.lblPin.Location = New System.Drawing.Point(10, 360)
        Me.lblPin.Name = "lblPin"
        Me.lblPin.Size = New System.Drawing.Size(25, 13)
        Me.lblPin.TabIndex = 21
        Me.lblPin.Text = "Pin:"
        '
        'srvPuertos
        '
        Me.srvPuertos.Control = Nothing
        Me.srvPuertos.Datos = CType(resources.GetObject("srvPuertos.Datos"), System.Collections.Generic.List(Of Short))
        Me.srvPuertos.Interval = 2000
        Me.srvPuertos.Status = Nothing
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 421)
        Me.Controls.Add(Me.lblPin)
        Me.Controls.Add(Me.grpSensores)
        Me.Controls.Add(Me.cmbMapas)
        Me.Controls.Add(Me.lblEstado)
        Me.Controls.Add(Me.lblPosicion)
        Me.Controls.Add(Me.lblTipo)
        Me.Controls.Add(Me.lstSensores)
        Me.Controls.Add(Me.lblMapa)
        Me.Controls.Add(Me.lblSensores)
        Me.Controls.Add(Me.menuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.menuStrip1
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Centinela"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.menuStrip1.ResumeLayout(False)
        Me.menuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private lblMapa As System.Windows.Forms.Label
    Private lblSensores As System.Windows.Forms.Label
    Private WithEvents usuariosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents administracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents configuracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents salirToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents aplicacionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents menuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents lstSensores As System.Windows.Forms.ListBox
    Friend WithEvents lblTipo As System.Windows.Forms.Label
    Friend WithEvents lblPosicion As System.Windows.Forms.Label
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents srvPuertos As Centinela.WinApp.Servicio
    Friend WithEvents cmbMapas As System.Windows.Forms.ComboBox
    Friend WithEvents MapasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SensoresToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpSensores As System.Windows.Forms.GroupBox
    Friend WithEvents SensoresToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrVerificarUsuario As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CerrarSesionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPin As System.Windows.Forms.Label
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReporteDiarioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReporteMensualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReporteAnualToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReporteTotalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportePorSensorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
