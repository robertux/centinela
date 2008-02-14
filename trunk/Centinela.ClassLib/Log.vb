Namespace ClassLib
	''' <summary>
    ''' Representa un log de un suceso relacionado con un sensor
    ''' </summary>
    <Serializable()> Public Class Log

#Region "Campos"

        ''' <summary>
        ''' El identificador unico del log
        ''' </summary>
        Private _id As Integer
        ''' <summary>
        ''' El identificador del sensor asociado con el suceso
        ''' </summary>
        Private _idSensor As Integer
        ''' <summary>
        ''' El identificador del estado actual del sensor
        ''' </summary>
        Private _idEstadoSensor As Integer
        ''' <summary>
        ''' Descripcion del suceso ocurrido
        ''' </summary>
        Private _suceso As String
        ''' <summary>
        ''' Fecha y hora de ocurrencia del suceso
        ''' </summary>
        Private _fechaHora As DateTime
        ''' <summary>
        ''' Id del usuario logueado al momento de ocurrir el suceso
        ''' </summary>
        Private _IdUsuario As Integer
        ''' <summary>
        ''' Nombre del sensor asociado
        ''' </summary>
        Private _nomSensor As String

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Devuelve o establece el identificador del log
        ''' </summary>
        Public Property Id() As Integer
            Get
                Return Me._id
            End Get
            Set(ByVal value As Integer)
                Me._id = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el id del sensor asociado
        ''' </summary>
        Public Property IdSensor() As Integer
            Get
                Return Me._idSensor
            End Get
            Set(ByVal value As Integer)
                Me._idSensor = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el id del estado del sensor
        ''' </summary>
        Public Property IdEstadoSensor() As Integer
            Get
                Return Me._idEstadoSensor
            End Get
            Set(ByVal value As Integer)
                Me._idEstadoSensor = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la descripcion del suceso ocurrido
        ''' </summary>
        Public Property Suceso() As String
            Get
                Return Me._suceso
            End Get
            Set(ByVal value As String)
                Me._suceso = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la fecha y la hora a la que ocurrio el suceso
        ''' </summary>
        Public Property FechaHora() As DateTime
            Get
                Return Me._fechaHora
            End Get
            Set(ByVal value As DateTime)
                Me._fechaHora = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el id del usuario logueado durante el suceso
        ''' </summary>
        Public Property IdUsuario() As Integer
            Get
                Return Me._IdUsuario
            End Get
            Set(ByVal value As Integer)
                Me._IdUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el nombre del sensor asociado
        ''' </summary>
        Public Property NomSensor() As String
            Get
                Return Me._nomSensor
            End Get
            Set(ByVal value As String)
                Me._nomSensor = value
            End Set
        End Property

#End Region

#Region "Metodos"

        ''' <summary>
        ''' Crea una nueva instancia de la clase Log
        ''' </summary>
        ''' <param name="id">El id del log</param>
        ''' <param name="idSen">El id del sensor asociado</param>
        ''' <param name="idEstSen">El id del estado del sensor</param>
        ''' <param name="succ">La descripcion del suceso ocurrido</param>
        ''' <param name="fechaHr">La fecha y hora cuando ocurrio el suceso</param>
        ''' <param name="idUsr">el Id del usuario logueado</param>
        ''' <param name="NomSen">el nombre del sensor</param>
        Public Sub New(ByVal id As Integer, ByVal idSen As Integer, ByVal idEstSen As Integer, ByVal succ As String, ByVal fechaHr As DateTime, ByVal idUsr As Integer, ByVal NomSen As String)

            Me.Id = id
            Me.IdSensor = idSen
            Me.IdEstadoSensor = idEstSen
            Me.Suceso = succ
            Me.FechaHora = fechaHr
            Me.IdUsuario = idUsr
            Me.NomSensor = NomSen
        End Sub

#End Region

    End Class
End Namespace