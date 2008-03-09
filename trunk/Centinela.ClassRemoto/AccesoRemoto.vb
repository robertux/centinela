Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports Centinela.ClassLib
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Namespace ClassLib

    ''' <summary>
    ''' Clase que proporciona el acceso remoto
    ''' La idea principal del Acceso Remoto, es presentar la sustitucion de las funciones
    ''' de acceso a dato por cadenas de mensajes o flujos de bits serializados, para
    ''' enviar los datos de una ubicacion a otra remota.
    ''' </summary>

    Public Class AccesoRemoto

#Region "Campos"
        'Aqui va lo de la IP y todo eso para el cliente?
        'quizas un objeto socket o algo asi
        Friend WithEvents sClient As SocketClient
        Private peticion As New Peticion()

#End Region

#Region "Propiedades"

        'Mas propiedades magicas van aqui.

#End Region


#Region "Metodos"

        ''' <summary>
        ''' Crea una nueva instancia de la clase Acceso a Datos
        ''' </summary>
        Public Sub New()
            Me.sClient = New SocketClient()
        End Sub

        ''' <summary>
        ''' Abre una conexion remota
        ''' </summary>		
        Public Sub Conectar()
            Me.sClient.Connect()
        End Sub

        ''' <summary>
        ''' Cierra una conexion remota
        ''' </summary>
        Public Sub Desconectar()
            Me.sClient.Close()
        End Sub

        <STAThread()> Public Shared Function ObjetoABinario(ByVal peticion As Peticion) As Byte()
            Try
                Dim mem As MemoryStream = New MemoryStream()
                Dim formato As BinaryFormatter = New BinaryFormatter()
                formato.Serialize(mem, peticion)
                Dim bytes() As Byte = mem.GetBuffer()
                mem.Close()
                Return bytes
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        <STAThread()> Public Shared Function BinarioAObjeto(ByVal bytes() As Byte) As Peticion
            Try
                Dim mem As MemoryStream = New MemoryStream(bytes)
                Dim p As Object = New Peticion()
                Dim formato As BinaryFormatter = New BinaryFormatter()
                p = CType(formato.Deserialize(mem), Peticion)
                Return p
                'MemoryStream memStream2 = new MemoryStream(bytes);
                'Car car2 = (Car)formatter.Deserialize(memStream2);
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

#Region "Usuarios"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla usuarios, basado en los datos de un objeto Usuario
        ''' </summary>
        ''' <param name="usr">Usuario a agregar a la tabla</param>
        Public Sub AgregarUsuario(ByVal usr As Usuario)
            Me.Conectar()
            Me.peticion = New Peticion()
            Dim vis As Integer
            If (usr.Visible) Then
                vis = 1
            Else
                vis = 0
            End If
            Me.peticion.Mensaje = "addusr;" + usr.Id + ";" + usr.NombreCompleto + ";" + usr.NombreUsuario + ";" + usr.Clave + ";" + vis.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
        End Sub

        ''' <summary>
        ''' Modifica los valores de un registro existente en la tabla usuarios, basado en los datos de un objeto Usuario
        ''' </summary>
        ''' <param name="usr">Usuario a modificar, ya incluidos sus valores modificados</param>
        Public Sub ModificarUsuario(ByVal usr As Usuario)
            Me.Conectar()
            Me.peticion = New Peticion()
            Dim vis As Integer
            If (usr.Visible) Then
                vis = 1
            Else
                vis = 0
            End If
            Me.peticion.Mensaje = "editusr;" + usr.Id + ";" + usr.NombreCompleto + ";" + usr.NombreUsuario + ";" + usr.Clave + ";" + vis.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Elimina un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="usr">Usuario a eliminar</param>
        Public Sub EliminarUsuario(ByVal usr As Usuario)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "delusr;" + usr.Id
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Devuelve un objeto Usuario con los datos de un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="id">Id del registro a seleccionar</param>
        Public Function SelecUsuario(ByVal id As String) As Usuario
            Me.Conectar()
            'Que vamos a hacer con el objeto enviado
            peticion = New Peticion()
            peticion.Mensaje = "loginbyid;" + id
            Dim b() As Byte
            'recibir datos!
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
            peticion = AccesoRemoto.BinarioAObjeto(b) 'viva el casting implicito            
            Return CType(peticion.objeto, Usuario)
        End Function

        ''' <summary>
        ''' Devuelve un objeto Usuario con los datos de un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="nom">nombre del usuario dentro del registro</param>
        ''' <param name="clave">clave del usuario dentro del registro</param>
        Public Function SelecUsuario(ByVal nom As String, ByVal clave As String) As Usuario
            Me.Conectar()
            'Que vamos a hacer con el objeto enviado
            peticion = New Peticion()
            peticion.Mensaje = "login;" + nom + ";" + clave
            Dim b() As Byte
            'recibir datos!
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
            peticion = AccesoRemoto.BinarioAObjeto(b) 'viva el casting implicito            
            Return CType(peticion.objeto, Usuario) 'y ya?            
        End Function

        ''' <summary>
        ''' Devuelve una lista de objetos Usuario con los datos de registros de la tabla usuarios. Si no hay condicion, devuelve todos los usuarios
        ''' </summary>
        ''' <param name="condicion">Condicion para filtrar los registros a seleccionar</param>
        Public Function SelectUsuarios(Optional ByVal condicion As String = "True") As List(Of Usuario)
            Me.Conectar()
            peticion = New Peticion()
            peticion.Mensaje = "selectusuarios"
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            peticion = AccesoRemoto.BinarioAObjeto(b)
            Return CType(peticion.objeto, List(Of Usuario))
            Me.Desconectar()
        End Function

#End Region

#Region "Administradores"

        ''' <summary>
        ''' Devuelve un objeto Administrador con los datos de un registro de la tabla administradores
        ''' </summary>
        ''' <param name="id">Id del registro a seleccionar</param>
        Public Function SelecAdministrador(ByVal id As String) As Administrador
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectadminbyid;" + id
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
            peticion = AccesoRemoto.BinarioAObjeto(b)
            Return CType(peticion.objeto, Administrador)            
        End Function

        ''' <summary>
        ''' Devuelve un objeto Administrador con los datos de un registro de la tabla administradores
        ''' </summary>
        ''' <param name="nom">nombre del administrador dentro del registro</param>
        ''' <param name="clave">clave del administrador dentro del registro</param>
        Public Function SelecAdministrador(ByVal nom As String, ByVal clave As String) As Administrador
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectadmin;" + nom + ";" + clave
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
            peticion = AccesoRemoto.BinarioAObjeto(b)
            Return CType(peticion.objeto, Administrador)
        End Function

#End Region

#Region "Sensores"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla sensores
        ''' </summary>
        ''' <param name="sen">Objeto sensor del cual tomar los datos</param>
        Public Sub AgregarSensor(ByVal sen As Sensor)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "addsen;" + sen.Id.ToString() + ";" + sen.Nombre + ";" + sen.Pin.ToString + ";" + sen.Tipo.ToString() + ";" + sen.EstadoActual.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Modifica los datos de un registro existente dentro de la tabla sensores
        ''' </summary>
        ''' <param name="sen">sensor a modificar, ya incluidos sus valores modificados</param>
        Public Sub ModificarSensor(ByVal sen As Sensor)
            'Esta funcion NUNCA se ejecuta remotamente, esto lo hace "MASTER"
            'pero como uno nunca sabe... ni modo mejor se deja
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "editsen;" + sen.Id.ToString() + ";" + sen.Nombre + ";" + sen.Pin.ToString + ";" + sen.Tipo.ToString() + ";" + sen.EstadoActual.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Elimina un registro de la tabla sensores
        ''' </summary>
        ''' <param name="sen">Sensor a eliminar</param>
        Public Sub EliminarSensor(ByVal sen As Sensor)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "delsen;" + sen.Id.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

#Region "Estado/Tipo"

        ''' <summary>
        ''' Devuelve el Id de un registro en la tabla de estados de sensor, basado en su nombre
        ''' </summary>
        ''' <param name="nomEstadoSensor">El nombre del estado del sensor</param>
        Public Function GetIdEstadoSensor(ByVal nomEstadoSensor As String) As Integer
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getidestadosen;" + nomEstadoSensor
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, Integer)
        End Function

        ''' <summary>
        ''' Devuelve el nombre de un registro en la tabla estados de sensor, basado en su Id
        ''' </summary>
        ''' <param name="idEstadoSensor">El id del estado de sensor</param>
        Public Function GetNombreEstadoSensor(ByVal idEstadoSensor As Integer) As String
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getnomestadosen;" + idEstadoSensor.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, String)
        End Function

        ''' <summary>
        ''' Devuelve una lista que contiene todos los nombres de los estados de sensor existentes en la tabla de estados de sensor
        ''' </summary>
        Public Function GetEstadosSensor() As List(Of String)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getestadossensor"
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of String))
        End Function

        ''' <summary>
        ''' Devuelve una lista que contiene todos los nombres de los tipos de sensor existentes en la tabla tipos de sensor
        ''' </summary>
        Public Function GetTiposSensor() As List(Of String)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "gettipossensor"
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of String))
        End Function

        ''' <summary>
        ''' Devuelve el Id de un registro en la tabla de tipos de sensor, basado en su nombre
        ''' </summary>
        ''' <param name="nomTipoSensor">El nombre del tipo del sensor</param>
        Public Function GetIdTipoSensor(ByVal nomTipoSensor As String) As Integer
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getidtiposensor;" + nomTipoSensor
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, Integer)
        End Function

        ''' <summary>
        ''' Devuelve el nombre de un registro en la tabla tipos de sensor, basado en su Id
        ''' </summary>
        ''' <param name="idTipoSensor">El id del tipo de sensor</param>
        Public Function GetNombreTipoSensor(ByVal idTipoSensor As Integer) As String
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getnomtiposensor;" + idTipoSensor.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, String)
        End Function

#End Region

        ''' <summary>
        ''' Devuelve un objeto Sensor con los datos de un registro de la tabla sensores
        ''' </summary>
        ''' <param name="id">El id del sensor del cual tomar los datos</param>
        Public Function SelectSensor(ByVal id As String) As Sensor
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectsen;" + id                        
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, Sensor)
        End Function

        ''' <summary>
        ''' Devuelve una lista de objetos Sensor basados en todos los registros de la tabla sensores
        ''' </summary>
        Public Function SelectSensores() As List(Of Sensor)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectsensores"
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of Sensor))
        End Function

        ''' <summary>
        ''' Devuelve una lista de objetos Sensor basados en los registros de la tabla sensores asociados con un mapa especifico
        ''' </summary>
        ''' <param name="idMapa">el id del Mapa asociado con los sensores a seleccionar</param>
        Public Function SelectSensores(ByVal idMapa As Integer) As List(Of Sensor)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectsensoresbymapa;" + idMapa.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of Sensor))
        End Function

        ''' <summary>
        ''' Devuelve la posicion de un sensor, en un mapa especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor</param>
        ''' <param name="idMapa">El id del mapa</param>
        Public Function GetPosSensor(ByVal idSen As Integer, ByVal idMapa As Integer) As System.Drawing.Point
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getpossen;" + idSen.ToString() + ";" + idMapa.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, System.Drawing.Point)
        End Function

        ''' <summary>
        ''' Establece la posicion de un sensor, en un mapa especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor</param>
        ''' <param name="idMapa">El id del mapa</param>
        ''' <param name="nuevaPos">Un objeto Point que contiene la nueva posicion del sensor en el mapa</param>
        Public Sub SetPosSensor(ByVal idSen As Integer, ByVal idMapa As Integer, ByVal nuevaPos As System.Drawing.Point)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "setpossen;" + idSen.ToString() + ";" + idMapa.ToString() + ";" + nuevaPos.X.ToString() + ";" + nuevaPos.Y.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Agrega una relacion entre un sensor existente y un mapa existente
        ''' </summary>
        ''' <param name="nuevoId">El id de la relacion</param>
        ''' <param name="idSen">El id del sensor a relacionar</param>
        ''' <param name="idMapa">El id del mapa a relacionar</param>
        Public Sub AgregarSensorMapa(ByVal nuevoId As Integer, ByVal idSen As Integer, ByVal idMapa As Integer)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "addsentomapa;" + nuevoId.ToString() + ";" + idSen.ToString() + ";" + idMapa.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Elimina una relacion entre un sensor existente y un mapa existente
        ''' </summary>
        ''' <param name="idSen">El id del sensor relacionado</param>
        ''' <param name="idMapa">El id del mapa relacionado</param>
        Public Sub EliminarSensorMapa(ByVal idSen As Integer, ByVal idMapa As Integer)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "delsenfrommapa;" + idSen.ToString() + ";" + idMapa.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

#End Region

#Region "HorarioUsuario"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El objeto HorarioUsuario del cual tomar los datos</param>
        Public Sub AgregarHorarioUsuario(ByVal hUsr As HorarioUsuario)
        End Sub

        ''' <summary>
        ''' Modifica un registro existente en la tabla horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El objeto HorarioUsuario a modificar, con sus valores ya modificados</param>
        Public Sub ModificarHorarioUsuario(ByVal hUsr As HorarioUsuario)
        End Sub

        ''' <summary>
        ''' Elimina un registro de la tabla de horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El horario a eliminar</param>
        Public Sub EliminarHorarioUsuario(ByVal hUsr As HorarioUsuario)
        End Sub

        ''' <summary>
        ''' Devuelve un objeto HorarioUsuario con los valores de un registro de la tabla horarios de usuario
        ''' </summary>
        ''' <param name="idusr">el id del usuario asociado con el horario</param>
        Public Function SelectHorarioUsuario(ByVal idusr As Integer) As HorarioUsuario
        End Function

#End Region


#Region "LogLog"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla log
        ''' </summary>
        ''' <param name="lg">El objeto log del cual tomar los datos</param>
        Public Sub AgregarLog(ByVal lg As Log)
        End Sub

        ''' <summary>
        ''' Devuelve una lista de objetos Log en base a los registros de la tabla log
        ''' </summary>
        ''' <param name="periodo">El periodo sobre el cual filtrar los resultados a devolver</param>Public Function SelectLogs(Optional ByVal periodo As String = "true") As List(Of Log)
        Public Function SelectLogs(Optional ByVal periodo As String = "true") As List(Of Log)
        End Function


#End Region

#Region "LogActividad"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla log de los horarios
        ''' </summary>
        ''' <param name="lg">El objeto LogHorario del cual tomar los datos</param>
        Public Sub AgregarLogHorario(ByVal lg As LogHorario)
        End Sub

        ''' <summary>
        ''' Devuelve una lista de objetos LogHorario basados en los registros de la tabla log de horarios
        ''' </summary>
        ''' <param name="condicion">La condicion para filtrar los registros a seleccionar</param>
        Public Function SelectLogsHorario(Optional ByVal condicion As String = "true") As List(Of LogHorario)
        End Function

#End Region

#Region "Custodios"

        Public Sub AgregarCustodio(ByVal v As Vigilante)
        End Sub

        Public Sub EliminarCustodio(ByVal v As Vigilante)
        End Sub

        Public Function SelecCustodio(ByVal id As String) As Vigilante
        End Function

        Public Function SelectCustodios() As List(Of Vigilante)
        End Function

#End Region

#Region "Mapas"

        ''' <summary>
        ''' Devuelve una lista de objetos Mapa basados en los registros de la tabla mapas
        ''' </summary>
        Public Function SelectMapas() As List(Of Mapa)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectmapas"
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of Mapa))
        End Function

        ''' <summary>
        ''' Devuelve una lista de objetos Mapa relacionados con un sensor especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor relacionado</param>
        Public Function SelectMapas(ByVal idSen As Integer) As List(Of Mapa)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "selectmapasbyidsen;" + idSen.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.peticion = AccesoRemoto.BinarioAObjeto(b)
            Me.Desconectar()
            Return CType(Me.peticion.objeto, List(Of Mapa))
        End Function

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla mapas
        ''' </summary>
        ''' <param name="mpa">El objeto Mapa del cual tomar los datos</param>
        Public Sub AgregarMapa(ByVal mpa As Mapa)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "addmapa;" + mpa.Id.ToString() + ";" + mpa.Nombre
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Elimina un registro existente en la tabla mapas
        ''' </summary>
        ''' <param name="mpa">El ojbeto Mapa a eliminar</param>
        Public Sub EliminarMapa(ByVal mpa As Mapa)
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "delmapa;" + mpa.Id.ToString()
            Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
        End Sub

        ''' <summary>
        ''' Devuelve un arreglo de bytes que representan la imagen asociada con un mapa
        ''' </summary>
        ''' <param name="mpa">El mapa asociado con la imagen</param>
        Public Function GetImgMapa(ByVal mpa As Mapa) As Byte()
            Me.Conectar()
            Me.peticion = New Peticion()
            Me.peticion.Mensaje = "getimgmapa;" + mpa.Id.ToString()
            Dim b() As Byte
            b = Me.sClient.SendAndRecDataSync(AccesoRemoto.ObjetoABinario(peticion))
            Me.Desconectar()
            Return b
        End Function

        ''' <summary>
        ''' Establece un arreglo de bytes que representan la imagen asociada con un mapa
        ''' </summary>
        ''' <param name="mpa">El mapa a asociar con la imagen</param>
        ''' <param name="img">El arreglo de bytes a asociar con el mapa</param>
        Public Sub SetImgMapa(ByVal mpa As Mapa, ByVal img As Byte())
        End Sub

#End Region

        ''' <summary>
        ''' Devuelve el maximo valor de Id de los registros de una tabla especifica
        ''' </summary>
        ''' <param name="nomTabla">El nombre de la tabla en la cual buscar</param>
        Public Function GetMaxId(ByVal nomTabla As String) As Integer
        End Function

#Region "SocketClient"

        Public Sub OnRecibirDatos(ByVal datos As Byte()) Handles sClient.ReceiveData

        End Sub

#End Region

#End Region

    End Class

End Namespace
