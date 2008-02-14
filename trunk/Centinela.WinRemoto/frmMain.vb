Imports Centinela.ClassLib
Imports Centinela.WinRemo
Imports System.IO

Public Class frmMain
    Public usr As Usuario
    Private datos As ClassLib.AccesoRemoto
    Private mab As MinimizarABandeja
    Private sensores As List(Of SensorVisual)
    Private cargando As Boolean
    Dim uPunto As Point
    Dim mapas As List(Of Mapa)
    Dim lastCmbMapasIndex As Integer = 0

    Public Notificador As NotificarBandeja.ShellNot = New NotificarBandeja.ShellNot()

#Region "EventHandlers"

#Region "MainForm"

    Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.datos = New AccesoRemoto()
        Me.PosicionarHijos()
        Dim sesion As New frmSesion()
        sesion.ShowDialog(Me)
        Me.usr = sesion.usr
        If (Me.usr.GetType().Name = "Administrador") Then
            Me.configuracionToolStripMenuItem.Enabled = True
            Me.administracionToolStripMenuItem.Enabled = True
        Else
            Me.configuracionToolStripMenuItem.Enabled = False
            Me.administracionToolStripMenuItem.Enabled = False
        End If
        Me.Text = "Centinela - Usuario: " + usr.NombreCompleto
        Me.CargarMapas()
        Me.CargarSensores()
        Me.mab = New MinimizarABandeja(Me, "Centinela")
        Dim cfg As New XmlClassLib.ConfigManager("app.config")
        Me.srvPuertos.Interval = CInt(cfg("intervalo"))
        Me.srvPuertos.Start()
    End Sub

    Sub MainFormResizeEnd(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        Me.PosicionarHijos()
    End Sub

    Private Sub frmMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.GuardarPosSensores()
    End Sub

#End Region

#Region "Sensores"

    Private Sub SensorVisualClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim idSen As Integer = CType(sender, SensorVisual).Sen.Id
        For i As Integer = 0 To Me.sensores.Count - 1
            If (idSen = Me.sensores(i).Sen.Id) Then
                Me.lstSensores.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub SensorVisualMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (TypeOf (Me.usr) Is Administrador) Then
            If (e.Button <> Windows.Forms.MouseButtons.Left) Then
                Exit Sub
            Else
                SensorVisualMouseMove(sender, e)
            End If
        End If
    End Sub

    Private Sub SensorVisualMouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (TypeOf (Me.usr) Is Administrador) Then
            If (e.Button <> Windows.Forms.MouseButtons.Left) Then
                Exit Sub
            Else
                Dim Izqd As Double = CType(sender, PictureBox).Left
                Dim Tope As Double = CType(sender, PictureBox).Top
                Dim nIzqd As Double = Izqd + e.X - (CType(sender, PictureBox).Height / 2)
                Dim nTope As Double = Tope + e.Y - (CType(sender, PictureBox).Width / 2)

                CType(sender, PictureBox).Left = CType(nIzqd, Integer)
                CType(sender, PictureBox).Top = CType(nTope, Integer)
                uPunto = New Point(e.X, e.Y)
                Me.lblPosicion.Text = "Posicion: (X:" + Me.sensores(Me.lstSensores.SelectedIndex).Left.ToString() + ", Y:" + Me.sensores(Me.lstSensores.SelectedIndex).Top.ToString() + ")"
            End If
        End If
    End Sub

    Private Sub SensoresToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SensoresToolStripMenuItem.Click
        Dim frmSen As New frmSensores()
        frmSen.ShowDialog(Me)
        Me.CargarSensores()
    End Sub

    Private Sub SensoresToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SensoresToolStripMenuItem1.Click
        Dim frmCfgSen As New frmConfigSensores()
        frmCfgSen.ShowDialog(Me)
        Dim cfg As New XmlClassLib.ConfigManager("app.config")
        Me.srvPuertos.Interval = CInt(cfg("intervalo"))
    End Sub

#End Region

    Sub SalirToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles salirToolStripMenuItem.Click
        End
    End Sub

    Sub UsuariosToolStripMenuItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles usuariosToolStripMenuItem.Click
        Dim frmUsr As New frmUsuarios()
        frmUsr.ShowDialog(Me)
    End Sub

    Private Sub lstSensores_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSensores.SelectedIndexChanged
        If Not (Me.cargando) Then
            For Each senvis As SensorVisual In Me.sensores
                senvis.BackColor = Color.LightGray
            Next
            Me.sensores(Me.lstSensores.SelectedIndex).BackColor = Color.Blue
            datos.Desconectar()
            datos.Conectar()
            Me.lblTipo.Text = "Tipo: " + datos.GetNombreTipoSensor(Me.sensores(Me.lstSensores.SelectedIndex).Sen.Tipo)
            Me.lblPosicion.Text = "Posicion: (X:" + Me.sensores(Me.lstSensores.SelectedIndex).Left.ToString() + ", Y:" + Me.sensores(Me.lstSensores.SelectedIndex).Top.ToString() + ")"
            datos.Desconectar()
            datos.Conectar()
            Dim nomEst As String = datos.GetNombreEstadoSensor(Me.sensores(Me.lstSensores.SelectedIndex).Sen.EstadoActual)
            datos.Desconectar()
            Me.lblEstado.Text = "Estado: " + nomEst
            Me.lblPin.Text = "Pin: " + Me.sensores(Me.lstSensores.SelectedIndex).Sen.Pin.ToString()
        End If
    End Sub

    Private Sub MapasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapasToolStripMenuItem.Click
        Dim frmmpas As New frmMapas()
        frmmpas.ShowDialog(Me)
        Me.CargarMapas()
    End Sub

    Private Sub cmbMapas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMapas.SelectedIndexChanged
        Me.GuardarPosSensores()
        datos.Conectar()
        Dim bytes As Byte() = datos.GetImgMapa(Me.mapas(Me.cmbMapas.SelectedIndex))
        If Not (bytes Is Nothing) Then
            Dim mstream As New System.IO.MemoryStream(bytes.Length)
            mstream.Write(bytes, 0, bytes.Length)
            Dim img As New Bitmap(mstream)
            Me.grpSensores.BackgroundImage = img
        End If
        datos.Desconectar()
        Me.CargarSensores()
        Me.lastCmbMapasIndex = Me.cmbMapas.SelectedIndex
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (Me.sensores(0).Sen.EstadoActual = 3) Then
            Me.sensores(0).Sen.Desactivar()
        ElseIf (Me.sensores(0).Sen.EstadoActual = 1) Then
            Me.sensores(0).Sen.Activar()
        ElseIf (Me.sensores(0).Sen.EstadoActual = 2) Then
            Me.sensores(0).Sen.Activar()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (Me.sensores(1).Sen.EstadoActual = 3) Then
            Me.sensores(1).Sen.Desactivar()
        ElseIf (Me.sensores(1).Sen.EstadoActual = 1) Then
            Me.sensores(1).Sen.Activar()
        ElseIf (Me.sensores(1).Sen.EstadoActual = 2) Then
            Me.sensores(1).Sen.Activar()
        End If
    End Sub

    Private Sub tmrVerificarUsuario_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrVerificarUsuario.Tick
        If Not (Me.usr Is Nothing) Then
            If Not (TypeOf (Me.usr) Is Administrador) Then
                datos.Conectar()
                Dim HUsr As HorarioUsuario = datos.SelectHorarioUsuario(CInt(Me.usr.Id))
                datos.Desconectar()
                Dim finicio As DateTime = New DateTime(HUsr.FechaInicio.Year, HUsr.FechaInicio.Month, HUsr.FechaInicio.Day, HUsr.HoraInicio.Hour, HUsr.HoraInicio.Minute, 0)
                Dim ffin As DateTime = New DateTime(HUsr.FechaFin.Year, HUsr.FechaFin.Month, HUsr.FechaFin.Day, HUsr.HoraFin.Hour, HUsr.HoraFin.Minute, 59)
                If (DateTime.Now < finicio Or DateTime.Now > ffin) Then
                    datos.Conectar()
                    Dim nuevoid As Integer = datos.GetMaxId("log_horario") + 1
                    datos.Desconectar()
                    datos.Conectar()
                    Dim lg As New LogHorario(nuevoid, CInt(Me.usr.Id), DateTime.Now, "Usuario logueado fuera de su horario")
                    datos.AgregarLogHorario(lg)
                    datos.Desconectar()
                End If
            End If
        End If
    End Sub

    Private Sub CerrarSesionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CerrarSesionToolStripMenuItem.Click
        Me.usr = Nothing
        Me.lstSensores.Items.Clear()
        Me.grpSensores.Controls.Clear()
        Me.grpSensores.BackgroundImage = Nothing
        Me.Text = "Centinela"
        Me.cmbMapas.Items.Clear()
        Me.lblEstado.Text = "Estado: "
        Me.lblTipo.Text = "Tipo: "
        Me.lblPosicion.Text = "Posicion: "
        Me.lblPin.Text = "Pin: "
        Me.MainFormLoad(Me, New EventArgs())
    End Sub

#End Region

    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Public Sub Restaurar(ByVal mb As System.Windows.Forms.MouseButtons)
        If mb = Windows.Forms.MouseButtons.Left Then
            Me.WindowState = FormWindowState.Maximized
            Try
                Notificador.DelNotifyBox()
            Catch ex As Exception
                'pass
            End Try
        End If
    End Sub

    Public Sub DispararNotificador(ByVal Mensaje As String)
        If Me.WindowState = FormWindowState.Minimized Then
            Notificador.AddNotifyBox(Me.Icon.Handle, Me.Text, "Aviso!", Mensaje)
            Notificador._delegateOfCallBack = New NotificarBandeja.ShellNot.delegateOfCallBack(AddressOf Restaurar)
        End If
    End Sub

    Public Sub PosicionarHijos()
        Me.lstSensores.Left = 0
        Me.lstSensores.Top = 50
        Me.lstSensores.Height = CInt(Me.Height * 0.7)
        Me.lstSensores.Width = CInt(Me.Width * 0.25)
        Me.cmbMapas.Left = Me.lstSensores.Width + 10
        Me.cmbMapas.Top = 50
        Me.lblSensores.Left = 15
        Me.lblMapa.Left = Me.lstSensores.Width + 15
        Me.lblTipo.Top = Me.lstSensores.Top + Me.lstSensores.Height + 20
        Me.lblPosicion.Top = Me.lblTipo.Top + 20
        Me.lblEstado.Top = Me.lblPosicion.Top + 20
        Me.lblPin.Top = Me.lblEstado.Top + 20
        Me.grpSensores.Left = Me.lstSensores.Width + 10
        Me.grpSensores.Top = Me.cmbMapas.Top + Me.cmbMapas.Height + 10
        Me.grpSensores.Height = Me.Height - 150
        Me.grpSensores.Width = CInt(Me.Width * 0.75) - 30
    End Sub

    Public Sub CargarMapas()
        Me.cmbMapas.Items.Clear()
        Me.datos.Conectar()
        Me.mapas = Me.datos.SelectMapas()
        Me.cargando = True
        For Each mpa As Mapa In Me.mapas
            Me.cmbMapas.Items.Add(mpa.Nombre)
        Next
        Me.cargando = False
        Me.datos.Desconectar()
        If (Me.cmbMapas.Items.Count > 0) Then
            Me.cmbMapas.SelectedIndex = 0
        End If        
    End Sub

    Public Sub CargarSensores()
        datos.Conectar()
        Me.lstSensores.Items.Clear()

        Me.sensores = New List(Of SensorVisual)

        Dim sens As List(Of Sensor) = datos.SelectSensores(Me.mapas(Me.cmbMapas.SelectedIndex).Id)
        If (Not sens Is Nothing) Then
            Me.lstSensores.Items.Clear()
            Me.cargando = True
            Me.grpSensores.Controls.Clear()
            For Each sen As Sensor In sens
                Me.sensores.Add(New SensorVisual(sen, Me.mapas(Me.cmbMapas.SelectedIndex)))
                Me.lstSensores.Items.Add(sen.Id.ToString() + " - " + sen.Nombre)
            Next
            Me.cargando = False
            If (Me.sensores.Count > 0) Then
                Me.lstSensores.SelectedIndex = 0
                Dim myleft As Integer = 10
                For Each senvis As SensorVisual In Me.sensores
                    If (senvis.Left = 0) Then
                        senvis.Left = myleft
                        myleft += 50
                        senvis.Top = 10
                    End If
                    senvis.CargarImagen()
                    senvis.BringToFront()
                    AddHandler senvis.Click, AddressOf Me.SensorVisualClick
                    AddHandler senvis.MouseDown, AddressOf Me.SensorVisualMouseDown
                    AddHandler senvis.MouseMove, AddressOf Me.SensorVisualMouseMove
                    Me.grpSensores.Controls.Add(senvis)
                Next
            End If
        End If
        datos.Desconectar()
    End Sub

    Private Sub ActualizarPuertosASensores(ByVal sender As Object, ByVal e As EventArgs) Handles srvPuertos.Tick        
        If Not (Me.usr Is Nothing) Then
            Me.srvPuertos.ActualizarSensores(Me.sensores)
            'For i As Integer = 0 To Me.srvPuertos.Datos.Count - 1
            '    If (Me.srvPuertos.Datos(i) = 0) Then
            '        If (Me.sensores(i).Sen.EstadoActual <> 3) Then
            '            Me.sensores(i).Sen.Desactivar()
            '        End If
            '    Else
            '        If (Me.sensores(i).Sen.EstadoActual <> 2) Then
            '            Me.sensores(i).Sen.Encender()
            '        End If
            '    End If
            '    datos.Conectar()
            '    Me.lblEstado.Text = datos.GetNombreEstadoSensor(Me.sensores(i).Sen.EstadoActual)
            '    datos.Desconectar()
            'Next
            'For i As Integer = 0 To Me.srvPuertos.Activacion.Count - 1
            '    If (Me.srvPuertos.Activacion(i) = 0) And (Me.srvPuertos.Encendido(i) = 1) Then
            '        If (Me.sensores(i).Sen.EstadoActual <> 2) Then
            '            Me.sensores(i).Sen.Desactivar()
            '        End If
            '    ElseIf (Me.srvPuertos.Activacion(i) = 1) And (Me.srvPuertos.Encendido(i) = 1) Then
            '        If (Me.sensores(i).Sen.EstadoActual <> 3) Then
            '            Me.sensores(i).Sen.Activar()
            '        End If
            '    End If
            '    If i = Me.lstSensores.SelectedIndex Then
            '        datos.Conectar()
            '        Me.lblEstado.Text = datos.GetNombreEstadoSensor(Me.sensores(i).Sen.EstadoActual)
            '        datos.Desconectar()
            '    End If
            'Next
            datos.Conectar()
            If Me.lstSensores.Items.Count > 0 Then
                Me.lblEstado.Text = datos.GetNombreEstadoSensor(Me.sensores(Me.lstSensores.SelectedIndex).Sen.EstadoActual)
                datos.Desconectar()
            End If
        End If
    End Sub

    Private Sub GuardarPosSensores()
        If (Not Me.sensores Is Nothing) Then
            For Each senvis As SensorVisual In Me.sensores
                datos.Desconectar()
                datos.Conectar()
                datos.SetPosSensor(senvis.Sen.Id, Me.mapas(Me.lastCmbMapasIndex).Id, senvis.Location)
                datos.Desconectar()
            Next
        End If
    End Sub

    Private Sub ReporteDiarioToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteDiarioToolStripMenuItem.Click
        XmlSensorLog.GenerarXml("diario")
        Dim frmRptDiario As New frmReporteDiario()
        frmRptDiario.Text = "Reporte de Actividad de los Sensores - Dia Actual"
        frmRptDiario.ShowDialog(Me)
    End Sub

    Private Sub ReporteMensualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteMensualToolStripMenuItem.Click
        XmlSensorLog.GenerarXml("mensual")
        Dim frmRptMensual As New frmReporteDiario()
        frmRptMensual.Text = "Reporte de Actividad de los Sensores - Mes Actual"
        frmRptMensual.ShowDialog(Me)
    End Sub

    Private Sub ReporteAnualToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteAnualToolStripMenuItem.Click
        XmlSensorLog.GenerarXml("anual")
        Dim frmRptMensual As New frmReporteDiario()
        frmRptMensual.Text = "Reporte de Actividad de los Sensores - Año Actual"
        frmRptMensual.ShowDialog(Me)
    End Sub

    Private Sub ReporteTotalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReporteTotalToolStripMenuItem.Click
        XmlSensorLog.GenerarXml()
        Dim frmRptMensual As New frmReporteDiario()
        frmRptMensual.Text = "Reporte de Actividad de los Sensores"
        frmRptMensual.ShowDialog(Me)
    End Sub

    Private Sub ReportePorSensorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportePorSensorToolStripMenuItem.Click
        XmlSensorLog.GenerarXml()
        Dim frmReportePorSensor As New frmRptPorSensor()
        frmReportePorSensor.Text = "Reporte de Actividad de los Sensores"
        frmReportePorSensor.ShowDialog(Me)
    End Sub
End Class