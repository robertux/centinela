Namespace ClassLib
		''' <summary>
	    ''' Representa el log del horario de un usuario
	    ''' </summary>
    <Serializable()> Public Class LogHorario

#Region "Campos"

        ''' <summary>
        ''' El identificador unico
        ''' </summary>
        Private _id As Integer
        ''' <summary>
        ''' El id del usuario logueado
        ''' </summary>
        Private _fkidusuario As Integer
        ''' <summary>
        ''' La fecha y la hora de ocurrencia del suceso
        ''' </summary>
        Private _fechaHora As DateTime
        ''' <summary>
        ''' La descripcion del suceso
        ''' </summary>
        Private _suceso As String

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
        ''' Devuelve o establece el id del usuario
        ''' </summary>
        Public Property IdUsuario() As Integer
            Get
                Return Me._fkidusuario
            End Get
            Set(ByVal value As Integer)
                Me._fkidusuario = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la fecha y la hora de ocurrencia del suceso
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
        ''' Devuelve o establece la descripcion del suceso
        ''' </summary>
        Public Property Suceso() As String
            Get
                Return Me._suceso
            End Get
            Set(ByVal value As String)
                Me._suceso = value
            End Set
        End Property

#End Region

#Region "Metodos"

        ''' <summary>
        ''' Crea una nueva instancia de la clase LogHorario
        ''' </summary>
        ''' <param name="id">El id del log</param>
        ''' <param name="idUsuario">El id del usuario</param>
        ''' <param name="fechaHr">La fecha de ocurrencia del suceso</param>
        ''' <param name="succ">La descripcion del suceso</param>
        Public Sub New(ByVal id As Integer, ByVal idUsuario As Integer, ByVal fechaHr As DateTime, ByVal succ As String)
            Me.Id = id
            Me._fkidusuario = idUsuario
            Me.Suceso = succ
            Me.FechaHora = fechaHr
        End Sub

#End Region

    End Class
End Namespace
