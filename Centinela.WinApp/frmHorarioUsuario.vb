Public Class frmHorarioUsuario

    Private datos As ClassLib.AccesoDatos
    Private HUsr As ClassLib.HorarioUsuario
    Private idusr As Integer

    Public Sub New(ByVal iduser As Integer)
        Me.InitializeComponent()
        datos = New ClassLib.AccesoDatos()
        Me.idusr = iduser
    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub frmHorarioActividad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RellenarEstadosSensor()
        Me.CargarHorario()
    End Sub

    Public Sub CargarHorario()
        Me.datos.Conectar()
        Me.HUsr = datos.SelectHorarioUsuario(idusr)
        Me.datos.Desconectar()
        If Not (Me.HUsr Is Nothing) Then
            'Me.datos.Conectar()
            'Dim estadoSen As String = Me.datos.GetNombreEstadoSensor(hAct.EstadoSen)
            'Me.datos.Desconectar()
            Me.datos.Conectar()
            Me.dtpFechaInicio.Value = Me.HUsr.FechaInicio
            Me.dtpFechaFin.Value = Me.HUsr.FechaFin
            Me.nudHoraE.Value = Me.HUsr.HoraInicio.Hour
            Me.nudMinutoE.Value = Me.HUsr.HoraInicio.Minute
            Me.nudHoraS.Value = Me.HUsr.HoraFin.Hour
            Me.nudMinutoS.Value = Me.HUsr.HoraFin.Minute
            'For i As Integer = 0 To Me.cmbEstados.Items.Count - 1
            '    If (Me.cmbEstados.Items(i).ToString() = estadoSen) Then
            '        '        Me.cmbEstados.SelectedIndex = i
            '        Exit For
            '    End If
            'Next
            'Me.nudDias.Value = Me.hAct.Periodo
            'Me.txtMotivo.Text = Me.hAct.Motivo
        End If
        datos.Desconectar()
    End Sub

    Public Sub RellenarEstadosSensor()
        'Me.datos.Conectar()
        'Dim estados As List(Of String) = Me.datos.GetEstadosSensor()
        'Me.cmbEstados.Items.Clear()
        'For Each est As String In estados
        '    Me.cmbEstados.Items.Add(est)
        'Next
        'Me.cmbEstados.SelectedIndex = 0
        'Me.datos.Desconectar()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click        

        Dim horaentrada As New DateTime(1900, 12, 1, CInt(Me.nudHoraE.Value), CInt(Me.nudMinutoE.Value), 0)
        Dim horasalida As New DateTime(1900, 12, 1, CInt(Me.nudHoraS.Value), CInt(Me.nudMinutoS.Value), 0)
        If (horaentrada >= horasalida) Then
            MsgBox("La hora de entrada debe ser mayor que la hora de salida", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        If (Me.dtpFechaInicio.Value >= Me.dtpFechaFin.Value) Then
            MsgBox("La fecha de inicio debe ser mayor que la fecha de finalizacion", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If
        Me.datos.Desconectar()
        Me.datos.Conectar()
        Dim newId As Integer = datos.GetMaxId("horario_usuario") + 1
        datos.Desconectar()
        Me.datos.Conectar()
        'Dim newHAct As New ClassLib.HorarioUsuario(newId, Me.idsen, Me.dtpFechaInicio.Value, New TimeSpan(CInt(Me.nudHoraS.Value), CInt(Me.nudMinutoS.Value), CInt(Me.nudSegundoS.Value)), idEstSen, CInt(Me.nudDias.Value), Me.txtMotivo.Text)
        Dim newHUsr As New ClassLib.HorarioUsuario(newId, horaentrada, horasalida, Me.dtpFechaInicio.Value, Me.dtpFechaFin.Value, Me.idusr)
        If (Me.HUsr Is Nothing) Then
            datos.AgregarHorarioUsuario(newHUsr)
        Else
            newHUsr.Id = Me.HUsr.Id
            datos.ModificarHorarioUsuario(newHUsr)
        End If

        Me.Close()
    End Sub
End Class