Imports Centinela.ClassLib
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Public Class frmMaster
    Public usr As Usuario
    Private datos As ClassLib.AccesoDatos
    Private mab As MinimizarABandeja

    Public peticion As New Peticion()

    Private sensores As List(Of Sensor)
    Private cargando As Boolean
    friend withevents sServ as SocketServer

    Public Notificador As NotificarBandeja.ShellNot = New NotificarBandeja.ShellNot()

    Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.datos = New AccesoDatos()
        me.sServ = new SocketServer()
        Dim sesion As New frmSesion()
        sesion.ShowDialog(Me)
        Me.usr = sesion.usr
        If Not (Me.usr.GetType().Name = "Administrador") Then
            MessageBox.Show("Solo el Administrador puede iniciar el Servicio Maestro de Centinela.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End If

        Me.txtLog.Text = "Bienvenido Usuario: " + usr.NombreCompleto

        Me.mab = New MinimizarABandeja(Me, "Centinela, Servicio Maestro")
        Dim cfg As New XmlClassLib.ConfigManager("app.config")
        Me.srvPuertos.Interval = CInt(cfg("intervalo"))
    End Sub

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
            Notificador.AddNotifyBox(Me.Icon.Handle, Me.Text, "AVISO!", Mensaje)
            Notificador._delegateOfCallBack = New NotificarBandeja.ShellNot.delegateOfCallBack(AddressOf Restaurar)
        End If
    End Sub

    Private Sub ActualizarPuertosASensores(ByVal sender As Object, ByVal e As EventArgs) Handles srvPuertos.Tick
        'Agregar try o algun tipo de seguridad aqui.
        For Each s As Sensor In Me.sensores
            Me.datos.ModificarSensor(s)
        Next
    End Sub

    Public Sub CargarSensores()
        datos.Conectar()
        Me.sensores = New List(Of Sensor)
        Me.sensores.Clear() 'Just in case...
        Me.sensores = datos.SelectSensores() 'Todos los sensores
        If (Not sensores Is Nothing) Then
            If (Me.sensores.Count > 0) Then
                'Do something!
            End If
        End If
        datos.Desconectar()
    End Sub

    Private Sub btnComenzar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComenzar.Click
        CargarSensores()
        Me.srvPuertos.Start()
        btnComenzar.Enabled = False
        btnDetener.Enabled = True
    End Sub

    Private Sub btnDetener_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetener.Click
        btnComenzar.Enabled = True
        Me.srvPuertos.Stop()
    End Sub     
    
    Private Sub DatosRecibidos(ByVal nCliente As Integer, ByVal datos As Byte()) Handles sServ.DataReceived
        Try
            peticion = AccesoRemoto.BinarioAObjeto(datos)
            Dim s() As String = peticion.Mensaje.Split(";")
            Select Case s(1)
                Case "login"
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelecUsuario(s(1), s(2))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "delusr"
                    Dim u As New Usuario(s(2), "", "", "", True)
                    Me.datos.Conectar()
                    Me.datos.EliminarUsuario(u)
                    Me.datos.Desconectar()
                    'Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "selectusuarios"
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelectUsuarios()
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "editusr"
                    Dim b As Boolean
                    If s(6) = "1" Then
                        b = True
                    ElseIf s(6) = "0" Then
                        b = False
                    End If
                    Dim u As New Usuario(s(2), s(3), s(5), s(4), b) '"editusr;" + usr.Id + ";" + usr.NombreCompleto + ";" + usr.NombreUsuario + ";" + usr.Clave + ";" + usr.Visible
                    Me.datos.Conectar()
                    Me.datos.ModificarUsuario(u)
                    Me.datos.Desconectar()
                    'Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "addusr"
                    Dim b As Boolean
                    If s(6) = "1" Then
                        b = True
                    ElseIf s(6) = "0" Then
                        b = False
                    End If
                    Dim u As New Usuario(s(2), s(3), s(5), s(4), b) '"addusr;" + usr.Id + ";" + usr.NombreCompleto + ";" + usr.NombreUsuario + ";" + usr.Clave + ";" + usr.Visible
                    Me.datos.Conectar()
                    Me.datos.AgregarUsuario(u)
                    Me.datos.Desconectar()
                Case "loginbyid"
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelecUsuario(s(2))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "selectadminbyid"
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelecAdministrador(s(2))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "selectadmin"
                    Me.datos.Conectar() '"selectadmin;" + nom + ";" + clave
                    peticion.objeto = Me.datos.SelecAdministrador(s(2), s(3))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "addsen"
                    Dim sen As New Sensor(s(2), s(3), s(4), s(6), s(5)) '"addsen;" + sen.Id.ToString() + ";" + sen.Nombre + ";" + sen.Pin.ToString + ";" + sen.Tipo.ToString() + ";" + sen.EstadoActual.ToString()
                    Me.datos.Conectar()
                    Me.datos.AgregarSensor(sen)
                    Me.datos.Desconectar()
                Case "editsen"
                    '"editsen;" + sen.Id.ToString() + ";" + sen.Nombre + ";" + sen.Pin.ToString + ";" + sen.Tipo.ToString() + ";" + sen.EstadoActual.ToString()
                    Dim sen As New Sensor(s(2), s(3), s(4), s(6), s(5))
                    Me.datos.Conectar()
                    Me.datos.ModificarSensor(sen)
                    Me.datos.Desconectar()
                Case "delsen" '"delsen;" + sen.Id.ToString()
                    Dim sen As New Sensor(s(2), "", "", "", "")
                    Me.datos.Conectar()
                    Me.datos.EliminarSensor(sen)
                    Me.datos.Desconectar()
                Case "selectsen" '"selectsen;" + id
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelectSensor(s(2))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "selectsensores"
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelectSensores()
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "selectsensoresbymapa" '"selectsensoresbymapa;" + idMapa.ToString()
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.SelectMapas(s(2))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "getpossen" '"getpossen;" + idSen.ToString() + ";" + idMapa.ToString()
                    Me.datos.Conectar()
                    peticion.objeto = Me.datos.GetPosSensor(s(2), s(3))
                    Me.datos.Desconectar()
                    Me.sServ.SendDataToClient(AccesoRemoto.ObjetoABinario(peticion), nCliente)
                Case "setpossen"
                    '"setpossen;" + idSen.ToString() + ";" + idMapa.ToString() + ";" + nuevaPos.X.ToString() + ";" + nuevaPos.Y.ToString()
                    Me.datos.Conectar()
                    Dim pos As New System.Drawing.Point(s(4), s(5))
                    Me.datos.SetPosSensor(s(2), s(3), pos)
                    Me.datos.Desconectar()
                Case "addsentomapa" '"addsentomapa;" + nuevoId.ToString() + ";" + idSen.ToString() + ";" + idMapa.ToString()
                    Me.datos.Conectar()
                    Me.datos.AgregarSensorMapa(s(2), s(3), s(4))
                    Me.datos.Desconectar()
                Case ""
                Case Else
                    Debug.WriteLine("ups1")
            End Select
        Catch ex As Exception
            'ups2
        End Try
    End Sub
    
End Class
