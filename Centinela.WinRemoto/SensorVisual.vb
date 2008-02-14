Imports Centinela.ClassLib
Imports XmlClassLib

''' <summary>
''' Representa un sensor visualizable en un formulario sobre un mapa
''' </summary>
Public Class SensorVisual
    Inherits PictureBox

#Region "Campos"
    ''' <summary>
    ''' Acceso a los datos de la base de datos
    ''' </summary>
    Private datos As AccesoDatos
    ''' <summary>
    ''' Ruta donde buscar las imagenes
    ''' </summary>
    Private _rutaImgs As String
    ''' <summary>
    ''' Clase que permite leer el archivo de configuracion
    ''' </summary>
    Private cfg As ConfigManager
    ''' <summary>
    ''' El nombre lo dice todo
    ''' </summary>
    Private WithEvents ttp As ToolTip
    ''' <summary>
    ''' Para reproducir el sonido de alarma
    ''' </summary>
    Private snd As Microsoft.VisualBasic.Devices.Audio
    ''' <summary>
    ''' Ruta del archivo de sonido de alarma
    ''' </summary>
    Private sndFile As String
    ''' <summary>
    ''' El objeto sensor
    ''' </summary>
    Private WithEvents _sen As ClassLib.Sensor

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Devuelve o establece el sensor contenido
    ''' </summary>
    Public Property Sen() As ClassLib.Sensor
        Get
            Return Me._sen
        End Get
        Set(ByVal value As ClassLib.Sensor)
            Me._sen = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Crea una instancia de la clase SensorVisual
    ''' </summary>
    ''' <param name="sen">El sensor asociado</param>
    ''' <param name="mpaActual">el mapa en el actualmente se muestra el sensor</param>
    Public Sub New(ByVal sen As ClassLib.Sensor, ByVal mpaActual As Mapa)
        MyBase.New()
        Me._rutaImgs = Application.StartupPath + "\Media\"
        Me.cfg = New ConfigManager("app.config")
        Me.datos = New AccesoDatos()
        Me._sen = sen
        Me.Height = 20
        Me.Width = 20
        Me.CambiarMapa(mpaActual)
        Me.BackColor = Color.LightGray
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.ttp = New ToolTip()
        Me.ttp.IsBalloon = True
        Me.ttp.ShowAlways = True
        Me.ttp.InitialDelay = 0
        Me.ttp.ToolTipTitle = Me.Sen.Nombre
        Me.ttp.ToolTipIcon = ToolTipIcon.Info
        Me.ttp.SetToolTip(Me, "Posicion: (X:" + Me.Location.X.ToString() + ", Y:" + Me.Location.Y.ToString() + ")")
        Me.snd = New Microsoft.VisualBasic.Devices.Audio()
        If (Me._sen.EstadoActual = 3) Then
            Me.sndFile = Me._rutaImgs + Me.cfg("sndalarmasensor")
            snd.Play(sndFile, AudioPlayMode.BackgroundLoop)
        End If
    End Sub

    ''' <summary>
    ''' Cambia de mapa y toma la nueva posicion relativa
    ''' </summary>
    ''' <param name="mpaActual">El nuevo mapa en el cual se muestra</param>
    Public Sub CambiarMapa(ByVal mpaActual As Mapa)
        datos.Conectar()
        Me.Location = datos.GetPosSensor(Sen.Id, mpaActual.Id)
        datos.Desconectar()
    End Sub

    ''' <summary>
    ''' Cambia la imagen del sensor de acuerdo a su estado actual
    ''' </summary>
    Public Sub CargarImagen()
        If (Me._sen.EstadoActual = 1) Then
            Me.ImageLocation = Me._rutaImgs + Me.cfg("imgdesconectado")            
        ElseIf (Me._sen.EstadoActual = 2) Then
            Me.ImageLocation = Me._rutaImgs + Me.cfg("imgdesactivado")
        ElseIf (Me._sen.EstadoActual = 3) Then
            Me.ImageLocation = Me._rutaImgs + Me.cfg("imgactivado")
        End If

    End Sub

    ''' <summary>
    ''' Actualiza el sensor visual cuando el sensor se apaga
    ''' </summary>
    ''' <param name="focur">La fecha cuando ocurre el suceso</param>
    Public Sub Apagado(ByVal focur As Date) Handles _sen.onApagar        
        Me.ttp.Show("Nuevo estado: Desconectado", Me, 10000)
        Me.ActualizarSensor()
    End Sub

    ''' <summary>
    ''' Actualiza el sensor visual cuando el sensor se enciende
    ''' </summary>
    ''' <param name="focur">La fecha cuando ocurre el suceso</param>
    Public Sub Encendido(ByVal focur As Date) Handles _sen.onEncender
        Me.ttp.Show("Nuevo estado: Encendido", Me, 10000)
        Me.ActualizarSensor()
    End Sub

    ''' <summary>
    ''' Actualiza el sensor visual cuando el sensor se activa
    ''' </summary>
    ''' <param name="focur">La fecha cuando ocurre el suceso</param>
    Public Sub Activado(ByVal focur As Date) Handles _sen.onActivar
        Me.ttp.Show("Nuevo estado: Activado", Me, 10000)
        Me.sndFile = Me._rutaImgs + Me.cfg("sndalarmasensor")
        snd.Play(sndFile, AudioPlayMode.BackgroundLoop)
        Me.ActualizarSensor()
    End Sub

    ''' <summary>
    ''' Actualiza el sensor visual cuando el sensor se desactiva
    ''' </summary>
    ''' <param name="focur">La fecha cuando ocurre el suceso</param>
    Public Sub Desactivado(ByVal focur As Date) Handles _sen.onDesactivar        
        Me.ttp.Show("Nuevo estado: Desactivado", Me, 10000)
        snd.Stop()
        Me.ActualizarSensor()
    End Sub

    ''' <summary>
    ''' Actualiza el sensor visual cuando ocurre un suceso en  su sensor interno
    ''' </summary>
    Public Sub ActualizarSensor()
        Me.CargarImagen()
        datos.Conectar()
        datos.ModificarSensor(Me.Sen)
        datos.Desconectar()
        If Not (TypeOf (frmMain.usr) Is Administrador) Then
            datos.Conectar()
            Dim nuevoid As Integer = datos.GetMaxId("tb_log") + 1
            datos.Desconectar()
            Dim lg As New Log(nuevoid, Me.Sen.Id, Me.Sen.EstadoActual, "Cambio de estado", DateTime.Now, CInt(frmMain.usr.Id), Me.Sen.Nombre)
            datos.Conectar()
            datos.AgregarLog(lg)
            datos.Desconectar()
        End If
    End Sub

#End Region
End Class
