Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient

Namespace ClassLib

    ''' <summary>
    ''' Clase que proporciona el acceso a la base de datos para consultar, agregar, editar, borrar registros de las tablas
    ''' </summary>
    Public Class AccesoDatos

#Region "Campos"
		''' <summary>
	    ''' Objeto para realizar conexiones a la base de datos
	    ''' </summary>
        Private _con As SqlConnection
		''' <summary>
	    ''' Objeto para realizar consultas SQL sobre la base de datos usando la conexion
        ''' </summary>
        Private _cmd As SqlCommand


#End Region

#Region "Propiedades"

		''' <summary>
	    ''' Devuelve el estado actual de la conexion a la base de datos
	    ''' </summary>
        Public ReadOnly Property Estado() As ConnectionState
            Get
                Return Me._con.State
            End Get
        End Property

		''' <summary>
	    ''' Devuelve la cadena de conexion necesaria para establecer una conexion a la base de datos
	    ''' </summary>
        Public ReadOnly Property ConnectionString() As String
            Get
                Return "Data Source=.\SQLEXPRESS;AttachDbFilename='" + Application.StartupPath + "\dbproyecto.mdf';Integrated Security=True;Connect Timeout=30;User Instance=True"
            End Get
        End Property
#End Region


#Region "Metodos"

		''' <summary>
	    ''' Crea una nueva instancia de la clase Acceso a Datos
	    ''' </summary>
        Public Sub New()

            Me._con = New SqlConnection(Me.ConnectionString)
            Me._cmd = New SqlCommand("", Me._con)
        End Sub

		''' <summary>
	    ''' Abre una conexion a la base de datos
	    ''' </summary>		
        Public Sub Conectar()
            If (Me._con.State <> ConnectionState.Open) Then
                Me._con.Open()
            End If
        End Sub

		''' <summary>
	    ''' Cierra una conexion a la base de datos
	    ''' </summary>
        Public Sub Desconectar()
            If (Me._con.State <> ConnectionState.Closed) Then
                Me._con.Close()
            End If
        End Sub

#Region "Usuarios"

		''' <summary>
        ''' Agrega un nuevo registro a la tabla usuarios, basado en los datos de un objeto Usuario
        ''' </summary>
        ''' <param name="usr">Usuario a agregar a la tabla</param>
        Public Sub AgregarUsuario(ByVal usr As Usuario)
            Me._cmd.CommandText = "Insert into usuario values(@id, @nomCompl, @clave, @usuario, 1)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", usr.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@nomCompl", usr.NombreCompleto))
            Me._cmd.Parameters.Add(New SqlParameter("@clave", usr.Clave))
            Me._cmd.Parameters.Add(New SqlParameter("@usuario", usr.NombreUsuario))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Modifica los valores de un registro existente en la tabla usuarios, basado en los datos de un objeto Usuario
        ''' </summary>
        ''' <param name="usr">Usuario a modificar, ya incluidos sus valores modificados</param>
        Public Sub ModificarUsuario(ByVal usr As Usuario)
            Me._cmd.CommandText = "Update usuario set nombre_completo = @nomCompl, clave = @clave, usuario = @usuario where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", usr.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@nomCompl", usr.NombreCompleto))
            Me._cmd.Parameters.Add(New SqlParameter("@clave", usr.Clave))
            Me._cmd.Parameters.Add(New SqlParameter("@usuario", usr.NombreUsuario))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Elimina un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="usr">Usuario a eliminar</param>
        Public Sub EliminarUsuario(ByVal usr As Usuario)
            Me._cmd.CommandText = "Update usuario set visible = 0 where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", usr.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Devuelve un objeto Usuario con los datos de un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="id">Id del registro a seleccionar</param>
        Public Function SelecUsuario(ByVal id As String) As Usuario
            Me._cmd.CommandText = "Select * from usuario where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", id))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim usr As New Usuario(lector("pk_id").ToString(), lector("nombre_completo").ToString(), lector("clave").ToString(), lector("usuario").ToString(), CBool(lector("visible")))
                lector.Close()
                Return usr
            End If
            Return Nothing
        End Function

		
		''' <summary>
        ''' Devuelve un objeto Usuario con los datos de un registro de la tabla usuarios
        ''' </summary>
        ''' <param name="nom">nombre del usuario dentro del registro</param>
        ''' <param name="clave">clave del usuario dentro del registro</param>
        Public Function SelecUsuario(ByVal nom As String, ByVal clave As String) As Usuario
            Me._cmd.CommandText = "Select * from usuario where usuario = @usr and clave = @clv"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@usr", nom))
            Me._cmd.Parameters.Add(New SqlParameter("@clv", clave))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim usr As New Usuario(lector("pk_id").ToString(), lector("nombre_completo").ToString(), lector("clave").ToString(), lector("usuario").ToString(), CBool(lector("visible")))
                lector.Close()
                Return usr
            End If
            Return Nothing
        End Function

		''' <summary>
        ''' Devuelve una lista de objetos Usuario con los datos de registros de la tabla usuarios. Si no hay condicion, devuelve todos los usuarios
        ''' </summary>
        ''' <param name="condicion">Condicion para filtrar los registros a seleccionar</param>
        Public Function SelectUsuarios(Optional ByVal condicion As String = "True") As List(Of Usuario)
            Me._cmd.CommandText = "Select * from usuario where visible = 1 "
            If (condicion <> "True") Then
                Me._cmd.CommandText += condicion
            End If
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                Dim lst As New List(Of Usuario)
                Do While (lector.Read())
                    lst.Add(New Usuario(lector("pk_id").ToString(), lector("nombre_completo").ToString(), lector("clave").ToString(), lector("usuario").ToString(), CBool(lector("visible"))))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

#End Region

#Region "Administradores"

		''' <summary>
        ''' Devuelve un objeto Administrador con los datos de un registro de la tabla administradores
        ''' </summary>
        ''' <param name="id">Id del registro a seleccionar</param>
        Public Function SelecAdministrador(ByVal id As String) As Administrador
            Me._cmd.CommandText = "Select * from administrador where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", id))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim adm As New Administrador(lector("pk_id").ToString(), lector("nombre_completo").ToString(), lector("clave").ToString(), lector("administrador").ToString(), CBool(lector("visible")))
                lector.Close()
                Return adm
            End If
            Return Nothing
        End Function

		''' <summary>
        ''' Devuelve un objeto Administrador con los datos de un registro de la tabla administradores
        ''' </summary>
        ''' <param name="nom">nombre del administrador dentro del registro</param>
        ''' <param name="clave">clave del administrador dentro del registro</param>
        Public Function SelecAdministrador(ByVal nom As String, ByVal clave As String) As Administrador
            Me._cmd.CommandText = "Select * from administrador where administrador = @usr and clave = @clv"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@usr", nom))
            Me._cmd.Parameters.Add(New SqlParameter("@clv", clave))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim adm As New Administrador(lector("pk_id").ToString(), lector("nombre_completo").ToString(), lector("clave").ToString(), lector("administrador").ToString(), CBool(lector("visible")))
                lector.Close()
                Return adm
            End If
            Return Nothing
        End Function

#End Region

#Region "Sensores"

		''' <summary>
        ''' Agrega un nuevo registro a la tabla sensores
        ''' </summary>
        ''' <param name="sen">Objeto sensor del cual tomar los datos</param>
        Public Sub AgregarSensor(ByVal sen As Sensor)
            Me._cmd.CommandText = "Insert into Sensor values(@id, @nombre, @tipo, @estado, @pin)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", sen.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@nombre", sen.Nombre))
            Me._cmd.Parameters.Add(New SqlParameter("@tipo", sen.Tipo))
            Me._cmd.Parameters.Add(New SqlParameter("@estado", sen.EstadoActual))
            Me._cmd.Parameters.Add(New SqlParameter("@pin", sen.Pin))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Modifica los datos de un registro existente dentro de la tabla sensores
        ''' </summary>
        ''' <param name="sen">sensor a modificar, ya incluidos sus valores modificados</param>
        Public Sub ModificarSensor(ByVal sen As Sensor)
            If Not (Me.Estado = ConnectionState.Open) Then
                Me.Conectar()
            End If
            Try
                Me._cmd.CommandText = "update Sensor set nombre=@nombre, fk_tipo = @tipo, fk_estado = @estado, pin=@pin where pk_id = @id"
                Me._cmd.Parameters.Clear()
                Me._cmd.Parameters.Add(New SqlParameter("@nombre", sen.Nombre))
                Me._cmd.Parameters.Add(New SqlParameter("@tipo", sen.Tipo))
                Me._cmd.Parameters.Add(New SqlParameter("@estado", sen.EstadoActual))
                Me._cmd.Parameters.Add(New SqlParameter("@id", sen.Id))
                Me._cmd.Parameters.Add(New SqlParameter("@pin", sen.Pin))
                Me._cmd.ExecuteNonQuery()
            Catch ex As Exception
                'pass
            End Try
        End Sub

		''' <summary>
        ''' Elimina un registro de la tabla sensores
        ''' </summary>
        ''' <param name="sen">Sensor a eliminar</param>
        Public Sub EliminarSensor(ByVal sen As Sensor)
            Me._cmd.CommandText = "delete from Sensor where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", sen.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

#Region "Estado/Tipo"

		''' <summary>
        ''' Devuelve el Id de un registro en la tabla de estados de sensor, basado en su nombre
        ''' </summary>
        ''' <param name="nomEstadoSensor">El nombre del estado del sensor</param>
        Public Function GetIdEstadoSensor(ByVal nomEstadoSensor As String) As Integer
            Me._cmd.CommandText = "select pk_id from estado_sensor where nombre = @nom"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@nom", nomEstadoSensor))
            Return CInt(Me._cmd.ExecuteScalar())
        End Function

		''' <summary>
        ''' Devuelve el nombre de un registro en la tabla estados de sensor, basado en su Id
        ''' </summary>
        ''' <param name="idEstadoSensor">El id del estado de sensor</param>
        Public Function GetNombreEstadoSensor(ByVal idEstadoSensor As Integer) As String
            Me._cmd.CommandText = "select nombre from estado_sensor where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", idEstadoSensor))
            Return Me._cmd.ExecuteScalar().ToString()
        End Function

		''' <summary>
        ''' Devuelve una lista que contiene todos los nombres de los estados de sensor existentes en la tabla de estados de sensor
        ''' </summary>
        Public Function GetEstadosSensor() As List(Of String)
            Me._cmd.CommandText = "Select * from estado_sensor"
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            Dim lst As New List(Of String)
            If (lector.HasRows) Then
                Do While (lector.Read())
                    lst.Add(lector("nombre").ToString())
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

		''' <summary>
        ''' Devuelve una lista que contiene todos los nombres de los tipos de sensor existentes en la tabla tipos de sensor
        ''' </summary>
        Public Function GetTiposSensor() As List(Of String)
            Me._cmd.CommandText = "Select * from tipo_sensor"
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            Dim lst As New List(Of String)
            If (lector.HasRows) Then
                Do While (lector.Read())
                    lst.Add(lector("nombre").ToString())
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing

        End Function

		''' <summary>
        ''' Devuelve el Id de un registro en la tabla de tipos de sensor, basado en su nombre
        ''' </summary>
        ''' <param name="nomTipoSensor">El nombre del tipo del sensor</param>
        Public Function GetIdTipoSensor(ByVal nomTipoSensor As String) As Integer
            Me._cmd.CommandText = "select pk_id from tipo_sensor where nombre = @nom"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@nom", nomTipoSensor))
            Return CInt(Me._cmd.ExecuteScalar())
        End Function

		''' <summary>
        ''' Devuelve el nombre de un registro en la tabla tipos de sensor, basado en su Id
        ''' </summary>
        ''' <param name="idTipoSensor">El id del tipo de sensor</param>
        Public Function GetNombreTipoSensor(ByVal idTipoSensor As Integer) As String
            Dim mycmd As New SqlCommand("select nombre from tipo_sensor where pk_id = @id", Me._con)
            mycmd.Parameters.Clear()
            mycmd.Parameters.Add(New SqlParameter("@id", idTipoSensor))
            Return mycmd.ExecuteScalar().ToString()
        End Function

#End Region

		''' <summary>
        ''' Devuelve un objeto Sensor con los datos de un registro de la tabla sensores
        ''' </summary>
        ''' <param name="id">El id del sensor del cual tomar los datos</param>
        Public Function SelectSensor(ByVal id As String) As Sensor
            Me._cmd.CommandText = "Select * from Sensor where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", id))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim sen As New Sensor(CInt(lector("pk_id")), lector("nombre").ToString(), CInt(lector("fk_tipo")), CInt(lector("fk_estado")), CInt(lector("pin")))
                lector.Close()
                Return sen
            End If
            Return Nothing
        End Function

		''' <summary>
        ''' Devuelve una lista de objetos Sensor basados en todos los registros de la tabla sensores
        ''' </summary>
        Public Function SelectSensores() As List(Of Sensor)
            Me._cmd = New SqlCommand("SELECT * FROM Sensor", Me._con)            
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            Dim lst As New List(Of Sensor)
            If (lector.HasRows) Then
                Do While (lector.Read())
                    lst.Add(New Sensor(CInt(lector("pk_id")), lector("nombre").ToString(), CInt(lector("fk_tipo")), CInt(lector("fk_estado")), CInt(lector("pin"))))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

		''' <summary>
        ''' Devuelve una lista de objetos Sensor basados en los registros de la tabla sensores asociados con un mapa especifico
        ''' </summary>
        ''' <param name="idMapa">el id del Mapa asociado con los sensores a seleccionar</param>
        Public Function SelectSensores(ByVal idMapa As Integer) As List(Of Sensor)
            Dim otroCmd As New SqlCommand("select fk_id_sensor from sensor_en_mapa where fk_id_mapa = @idmapa order by fk_id_sensor", Me._con)
            Dim idSensoresMapa As New List(Of Integer)
            otroCmd.Parameters.Clear()
            otroCmd.Parameters.Add(New SqlParameter("@idmapa", idMapa))
            Dim otroLector As SqlDataReader = otroCmd.ExecuteReader()
            If (otroLector.HasRows) Then
                Do While (otroLector.Read())
                    idSensoresMapa.Add(CInt(otroLector("fk_id_sensor")))
                Loop
                otroLector.Close()
            End If

            Dim lst As New List(Of Sensor)
            For Each idSen As Integer In idSensoresMapa
                Me._cmd = New SqlCommand("SELECT * FROM Sensor where pk_id = @idsen", Me._con)
                Me._cmd.Parameters.Clear()
                Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
                Dim lector As SqlDataReader = Me._cmd.ExecuteReader()                
                If (lector.HasRows) Then
                    lector.Read()
                    lst.Add(New Sensor(CInt(lector("pk_id")), lector("nombre").ToString(), CInt(lector("fk_tipo")), CInt(lector("fk_estado")), CInt(lector("pin"))))
                    lector.Close()                    
                End If
            Next
            Return lst            
        End Function

		''' <summary>
        ''' Devuelve la posicion de un sensor, en un mapa especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor</param>
        ''' <param name="idMapa">El id del mapa</param>
        Public Function GetPosSensor(ByVal idSen As Integer, ByVal idMapa As Integer) As System.Drawing.Point
            Me._cmd = New SqlCommand("select posx, posy from sensor_en_mapa where fk_id_sensor = @idsen and fk_id_mapa = @idmapa", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
            Me._cmd.Parameters.Add(New SqlParameter("@idmapa", idMapa))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            Dim pt As New System.Drawing.Point(0, 0)
            If (lector.HasRows) Then
                lector.Read()
                pt.X = CInt(lector("posx"))
                pt.Y = CInt(lector("posy"))
                Return pt
            End If
            lector.Close()
            Return Nothing
        End Function

		''' <summary>
        ''' Establece la posicion de un sensor, en un mapa especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor</param>
        ''' <param name="idMapa">El id del mapa</param>
        ''' <param name="nuevaPos">Un objeto Point que contiene la nueva posicion del sensor en el mapa</param>
        Public Sub SetPosSensor(ByVal idSen As Integer, ByVal idMapa As Integer, ByVal nuevaPos As System.Drawing.Point)
            Me._cmd = New SqlCommand("update sensor_en_mapa set posx = @posx, posy = @posy where fk_id_sensor = @idsen and fk_id_mapa = @idmapa", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@posx", nuevaPos.X))
            Me._cmd.Parameters.Add(New SqlParameter("@posy", nuevaPos.Y))
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
            Me._cmd.Parameters.Add(New SqlParameter("@idmapa", idMapa))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Agrega una relacion entre un sensor existente y un mapa existente
        ''' </summary>
        ''' <param name="nuevoId">El id de la relacion</param>
        ''' <param name="idSen">El id del sensor a relacionar</param>
        ''' <param name="idMapa">El id del mapa a relacionar</param>
        Public Sub AgregarSensorMapa(ByVal nuevoId As Integer, ByVal idSen As Integer, ByVal idMapa As Integer)
            Me._cmd = New SqlCommand("insert into sensor_en_mapa values(@newId, @idsen, @idmapa, 0, 0)", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@newId", nuevoId))
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
            Me._cmd.Parameters.Add(New SqlParameter("@idmapa", idMapa))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Elimina una relacion entre un sensor existente y un mapa existente
        ''' </summary>
        ''' <param name="idSen">El id del sensor relacionado</param>
        ''' <param name="idMapa">El id del mapa relacionado</param>
        Public Sub EliminarSensorMapa(ByVal idSen As Integer, ByVal idMapa As Integer)
            Me._cmd = New SqlCommand("delete from sensor_en_mapa where fk_id_sensor = @idsen and fk_id_mapa = @idmapa", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
            Me._cmd.Parameters.Add(New SqlParameter("@idmapa", idMapa))
            Me._cmd.ExecuteNonQuery()
        End Sub

#End Region

#Region "HorarioUsuario"

		''' <summary>
        ''' Agrega un nuevo registro a la tabla horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El objeto HorarioUsuario del cual tomar los datos</param>
        Public Sub AgregarHorarioUsuario(ByVal hUsr As HorarioUsuario)
            Me._cmd.CommandText = "insert into horario_usuario values(@id, @horaini, @horafin, @fechaini, @fechafin, @fkidusuario)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", hUsr.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@horaini", hUsr.HoraInicio))
            Me._cmd.Parameters.Add(New SqlParameter("@horafin", hUsr.HoraFin))
            Me._cmd.Parameters.Add(New SqlParameter("@fechaini", hUsr.FechaInicio))
            Me._cmd.Parameters.Add(New SqlParameter("@fechafin", hUsr.FechaFin))
            Me._cmd.Parameters.Add(New SqlParameter("@fkidusuario", hUsr.Usuario))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Modifica un registro existente en la tabla horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El objeto HorarioUsuario a modificar, con sus valores ya modificados</param>
        Public Sub ModificarHorarioUsuario(ByVal hUsr As HorarioUsuario)
            Me._cmd.CommandText = "update horario_usuario set " + _
"hora_inicio = @horaini, hora_fin = @horafin, fecha_inicio = @fechaini, fecha_fin = @fechafin, " + _
"fk_id_usuario = @fkidusuario where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", hUsr.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@horaini", hUsr.HoraInicio))
            Me._cmd.Parameters.Add(New SqlParameter("@horafin", hUsr.HoraFin))
            Me._cmd.Parameters.Add(New SqlParameter("@fechaini", hUsr.FechaInicio))
            Me._cmd.Parameters.Add(New SqlParameter("@fechafin", hUsr.FechaFin))
            Me._cmd.Parameters.Add(New SqlParameter("@fkidusuario", hUsr.Usuario))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Elimina un registro de la tabla de horarios de usuario
        ''' </summary>
        ''' <param name="hUsr">El horario a eliminar</param>
        Public Sub EliminarHorarioUsuario(ByVal hUsr As HorarioUsuario)
            Me._cmd.CommandText = "delete from horario_usuario where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", hUsr.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Devuelve un objeto HorarioUsuario con los valores de un registro de la tabla horarios de usuario
        ''' </summary>
        ''' <param name="idusr">el id del usuario asociado con el horario</param>
        Public Function SelectHorarioUsuario(ByVal idusr As Integer) As HorarioUsuario
            Me._cmd = New SqlCommand("select * from horario_usuario where fk_id_usuario = @idusr", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@idusr", idusr))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                'Dim motv As String = ""
                'If Not (TypeOf (lector("motivo")) Is System.DBNull) Then
                '    motv = lector("motivo").ToString()
                'End If
                Dim hUsr As New HorarioUsuario(CInt(lector("pk_id")), CDate(lector("hora_inicio")), CDate(lector("hora_fin")), CDate(lector("fecha_inicio")), CDate(lector("fecha_fin")), idusr)
                Return hUsr
            End If
            Return Nothing
        End Function

#End Region


#Region "LogLog"

		''' <summary>
        ''' Agrega un nuevo registro a la tabla log
        ''' </summary>
        ''' <param name="lg">El objeto log del cual tomar los datos</param>
        Public Sub AgregarLog(ByVal lg As Log)
            Me._cmd.CommandText = "Insert into tb_log values(@id, @idsen, @idestsen, @suceso, @fhHr, @idusr, @nomsensor)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", lg.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", lg.IdSensor))
            Me._cmd.Parameters.Add(New SqlParameter("@idestsen", lg.IdEstadoSensor))
            Me._cmd.Parameters.Add(New SqlParameter("@suceso", lg.Suceso))
            Me._cmd.Parameters.Add(New SqlParameter("@fhHr", lg.FechaHora))
            Me._cmd.Parameters.Add(New SqlParameter("@idusr", lg.IdUsuario))
            Me._cmd.Parameters.Add(New SqlParameter("@nomsensor", lg.NomSensor))
            Me._cmd.ExecuteNonQuery()
        End Sub

		''' <summary>
        ''' Devuelve una lista de objetos Log en base a los registros de la tabla log
        ''' </summary>
        ''' <param name="periodo">El periodo sobre el cual filtrar los resultados a devolver</param>Public Function SelectLogs(Optional ByVal periodo As String = "true") As List(Of Log)
        Public Function SelectLogs(Optional ByVal periodo As String = "true") As List(Of Log)
            Me._cmd.CommandText = "select * from tb_log"
            Me._cmd.Parameters.Clear()
            If (periodo = "diario") Then
                Dim iniAhora As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)
                Dim finAhora As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59)
                Me._cmd.CommandText += " where fecha_hora between @fini and @ffin"
                Me._cmd.Parameters.Add(New SqlParameter("@fini", iniAhora))
                Me._cmd.Parameters.Add(New SqlParameter("@ffin", finAhora))
            ElseIf (periodo = "mensual") Then
                Dim iniMes As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0)
                Dim finMes As New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month), 23, 59, 59)
                Me._cmd.CommandText += " where fecha_hora between @fini and @ffin"
                Me._cmd.Parameters.Add(New SqlParameter("@fini", iniMes))
                Me._cmd.Parameters.Add(New SqlParameter("@ffin", finMes))
            ElseIf (periodo = "anual") Then
                Dim iniAnio As New DateTime(DateTime.Now.Year, 1, 1, 0, 0, 0)
                Dim finAnio As New DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59)
                Me._cmd.CommandText += " where fecha_hora between @fini and @ffin"
                Me._cmd.Parameters.Add(New SqlParameter("@fini", iniAnio))
                Me._cmd.Parameters.Add(New SqlParameter("@ffin", finAnio))
            End If
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                Dim lst As New List(Of Log)
                Do While (lector.Read())
                    lst.Add(New Log(CInt(lector("pk_id")), CInt(lector("fk_id_sensor")), CInt(lector("fk_id_estado_sensor")), lector("suceso").ToString(), CDate(lector("fecha_hora")), CInt(lector("fk_id_usuario")), lector("nombresensor").ToString()))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function


#End Region

#Region "LogActividad"

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla log de los horarios
        ''' </summary>
        ''' <param name="lg">El objeto LogHorario del cual tomar los datos</param>
        Public Sub AgregarLogHorario(ByVal lg As LogHorario)
            Me._cmd.CommandText = "Insert into log_horario values(@id, @fkidusr, @fechahora, @suceso)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", lg.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@fkidusr", lg.IdUsuario))
            Me._cmd.Parameters.Add(New SqlParameter("@fechahora", lg.FechaHora))
            Me._cmd.Parameters.Add(New SqlParameter("@suceso", lg.Suceso))
            Me._cmd.ExecuteNonQuery()
        End Sub

        ''' <summary>
        ''' Devuelve una lista de objetos LogHorario basados en los registros de la tabla log de horarios
        ''' </summary>
        ''' <param name="condicion">La condicion para filtrar los registros a seleccionar</param>
        Public Function SelectLogsHorario(Optional ByVal condicion As String = "true") As List(Of LogHorario)
            Me._cmd.CommandText = "select * from log_horario where " + condicion
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                Dim lst As New List(Of LogHorario)
                Do While (lector.Read())
                    lst.Add(New LogHorario(CInt(lector("pk_id")), CInt(lector("fk_id_usuario")), CDate(lector("fecha_hora")), lector("suceso").ToString()))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

#End Region

#Region "Custodios"

        Public Sub AgregarCustodio(ByVal v As Vigilante)
            Me._cmd.CommandText = "Insert into vigilante values(@id, @nombre)"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", v.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@nombre", v.Nombre))
            Me._cmd.ExecuteNonQuery()
        End Sub

        Public Sub EliminarCustodio(ByVal v As Vigilante)
            Me._cmd.CommandText = "delete from vigilante where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", v.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

        Public Function SelecCustodio(ByVal id As String) As Vigilante
            Me._cmd.CommandText = "Select * from vigilante where pk_id = @id"
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", id))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                lector.Read()
                Dim v As New Vigilante(lector("pk_id").ToString(), lector("nombre").ToString())
                lector.Close()
                Return v
            End If
            Return Nothing
        End Function

        Public Function SelectCustodios() As List(Of Vigilante)
            Me._cmd.CommandText = "Select * from vigilante"
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                Dim lst As New List(Of Vigilante)
                Do While (lector.Read())
                    lst.Add(New Vigilante(lector("pk_id").ToString(), lector("nombre").ToString()))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

#End Region

#Region "Mapas"

        ''' <summary>
        ''' Devuelve una lista de objetos Mapa basados en los registros de la tabla mapas
        ''' </summary>
        Public Function SelectMapas() As List(Of Mapa)
            Me._cmd = New SqlCommand("select * from mapa", Me._con)
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows) Then
                Dim lst As New List(Of Mapa)
                Do While (lector.Read())
                    lst.Add(New Mapa(CInt(lector("pk_id")), lector("nombre").ToString()))
                Loop
                lector.Close()
                Return lst
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Devuelve una lista de objetos Mapa relacionados con un sensor especifico
        ''' </summary>
        ''' <param name="idSen">El id del sensor relacionado</param>
        Public Function SelectMapas(ByVal idSen As Integer) As List(Of Mapa)
            Dim idmapas As New List(Of Integer)
            Dim lst As New List(Of Mapa)

            Me._cmd = New SqlCommand("select fk_id_mapa from sensor_en_mapa where fk_id_sensor = @idsen", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@idsen", idSen))
            Dim otroLector As SqlDataReader = Me._cmd.ExecuteReader()
            If (otroLector.HasRows) Then
                Do While (otroLector.Read())
                    idmapas.Add(CInt(otroLector("fk_id_mapa")))
                Loop
            End If
            otroLector.Close()

            For Each idmapa As Integer In idmapas
                Me._cmd = New SqlCommand("select * from mapa where pk_id = @idmapa", Me._con)
                Me._cmd.Parameters.Clear()
                Me._cmd.Parameters.Add(New SqlParameter("@idmapa", idmapa))
                Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
                If (lector.HasRows) Then
                    Do While (lector.Read())
                        lst.Add(New Mapa(CInt(lector("pk_id")), lector("nombre").ToString()))
                    Loop
                    lector.Close()
                End If
            Next
            Return lst
        End Function

        ''' <summary>
        ''' Agrega un nuevo registro a la tabla mapas
        ''' </summary>
        ''' <param name="mpa">El objeto Mapa del cual tomar los datos</param>
        Public Sub AgregarMapa(ByVal mpa As Mapa)
            Me._cmd = New SqlCommand("insert into mapa(pk_id, nombre) values(@id, @nom)", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", mpa.Id))
            Me._cmd.Parameters.Add(New SqlParameter("@nom", mpa.Nombre))
            Me._cmd.ExecuteNonQuery()
        End Sub

        ''' <summary>
        ''' Elimina un registro existente en la tabla mapas
        ''' </summary>
        ''' <param name="mpa">El ojbeto Mapa a eliminar</param>
        Public Sub EliminarMapa(ByVal mpa As Mapa)
            Me._cmd = New SqlCommand("delete from mapa where pk_id = @id", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", mpa.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

        ''' <summary>
        ''' Devuelve un arreglo de bytes que representan la imagen asociada con un mapa
        ''' </summary>
        ''' <param name="mpa">El mapa asociado con la imagen</param>
        Public Function GetImgMapa(ByVal mpa As Mapa) As Byte()
            Me._cmd = New SqlCommand("select imagen from mapa where pk_id = @id", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@id", mpa.Id))
            Dim lector As SqlDataReader = Me._cmd.ExecuteReader()
            If (lector.HasRows()) Then
                lector.Read()
                Return CType(lector("imagen"), Byte())
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Establece un arreglo de bytes que representan la imagen asociada con un mapa
        ''' </summary>
        ''' <param name="mpa">El mapa a asociar con la imagen</param>
        ''' <param name="img">El arreglo de bytes a asociar con el mapa</param>
        Public Sub SetImgMapa(ByVal mpa As Mapa, ByVal img As Byte())
            Me._cmd = New SqlCommand("update mapa set imagen = @img where pk_id = @id", Me._con)
            Me._cmd.Parameters.Clear()
            Me._cmd.Parameters.Add(New SqlParameter("@img", img))
            Me._cmd.Parameters.Add(New SqlParameter("@id", mpa.Id))
            Me._cmd.ExecuteNonQuery()
        End Sub

#End Region

        ''' <summary>
        ''' Devuelve el maximo valor de Id de los registros de una tabla especifica
        ''' </summary>
        ''' <param name="nomTabla">El nombre de la tabla en la cual buscar</param>
        Public Function GetMaxId(ByVal nomTabla As String) As Integer
            Me._cmd.CommandText = "select max(pk_id) from " + nomTabla
            If (TypeOf (Me._cmd.ExecuteScalar()) Is System.DBNull) Then
                Return 0
            End If
            Return CInt(Me._cmd.ExecuteScalar())
        End Function

#End Region

    End Class

End Namespace
