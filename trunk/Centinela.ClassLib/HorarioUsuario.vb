Namespace ClassLib
	''' <summary>
    ''' Representa el horario de acceso al sistema por un usuario
    ''' </summary>
    <Serializable()> Public Class HorarioUsuario

#Region "Campos"

        ''' <summary>
        ''' Identificador unico para cada registro en la base de datos
        ''' </summary>
        Private _id As Integer
        ''' <summary>
        ''' Hora  inicial de acceso
        ''' </summary>
        Private _horaInicio As DateTime
        ''' <summary>
        ''' Hora final de acceso
        ''' </summary>
        Private _horaFin As DateTime
        ''' <summary>
        ''' Fecha inicial de acceso
        ''' </summary>
        Private _fechaInicio As DateTime
        ''' <summary>
        ''' Fecha final de acceso
        ''' </summary>
        Private _fechaFin As DateTime
        ''' <summary>
        ''' Id del usuario relacionado con el horario
        ''' </summary>
        Private _fkIdUsuario As Integer

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Devuelve o establece el id del horario
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
        ''' Devuelve o establece la hora inicial de acceso
        ''' </summary>
        Public Property HoraInicio() As DateTime
            Get
                Return Me._horaInicio
            End Get
            Set(ByVal value As DateTime)
                Me._horaInicio = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la hora final de acceso
        ''' </summary>
        Public Property HoraFin() As DateTime
            Get
                Return Me._horaFin
            End Get
            Set(ByVal value As DateTime)
                Me._horaFin = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la fecha inicial de acceso
        ''' </summary>
        Public Property FechaInicio() As DateTime
            Get
                Return Me._fechaInicio
            End Get
            Set(ByVal value As DateTime)
                Me._fechaInicio = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la fecha final de acceso
        ''' </summary>
        Public Property FechaFin() As DateTime
            Get
                Return Me._fechaFin
            End Get
            Set(ByVal value As DateTime)
                Me._fechaFin = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el Id del usuario asociado con el horario
        ''' </summary>
        Public Property Usuario() As Integer
            Get
                Return Me._fkIdUsuario
            End Get
            Set(ByVal value As Integer)
                Me._fkIdUsuario = value
            End Set
        End Property

#End Region

#Region "Metodos"
        ''' <summary>
        ''' Crea una nueva instancia de la clase HorarioUsuario
        ''' </summary>
        ''' <param name="id">El id del horario</param>
        ''' <param name="horaInicio">La hora de inicio del horario</param>
        ''' <param name="horafin">La hora de finalizacion del horario</param>
        ''' <param name="fechainicio">La fecha de inicio del horario</param>
        ''' <param name="fechafin">La fecha de finalizacion del horario</param>
        ''' <param name="fkidusuario">El id del usuario asociado con el horario</param>
        Public Sub New(ByVal id As Integer, ByVal horaInicio As DateTime, ByVal horafin As DateTime, _
        ByVal fechainicio As DateTime, ByVal fechafin As DateTime, ByVal fkidusuario As Integer)
            Me._id = id
            Me._horaInicio = horaInicio
            Me._horaFin = horafin
            Me._fechaInicio = fechainicio
            Me._fechaFin = fechafin
            Me._fkIdUsuario = fkidusuario
        End Sub

#End Region

    End Class

End Namespace
