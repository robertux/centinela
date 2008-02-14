Namespace ClassLib

    <Serializable()> Public Class ActividadProgramada

#Region "Campos"

        Private _fechaHoraInicio As DateTime
        Private _duracion As TimeSpan
        Private _periodo As Integer 'TODO: Usar Integer para # de dias? (sujeto a cambio)
        Private _idsen As Integer

#End Region

#Region "Propiedades"

        Public Property FechaHoraInicio() As DateTime
            Get
                Return Me._fechaHoraInicio
            End Get
            Set(ByVal value As DateTime)
                Me._fechaHoraInicio = value
            End Set
        End Property

        Public Property Duracion() As TimeSpan
            Get
                Return Me._duracion
            End Get
            Set(ByVal value As TimeSpan)
                Me._duracion = value
            End Set
        End Property

        Public Property IdSen() As Integer
            Get
                Return Me._idsen
            End Get
            Set(ByVal value As Integer)
                Me._idsen = value
            End Set
        End Property

        Public Property Periodo() As Integer
            Get
                Return Me._periodo
            End Get
            Set(ByVal value As Integer)
                Me._periodo = value
            End Set
        End Property

        Public ReadOnly Property Estado() As EstadoTarea
            Get
                If (Me.FechaHoraInicio > DateTime.Now) Then
                    Return EstadoTarea.Pendiente
                ElseIf ((Me.FechaHoraInicio <= DateTime.Now) And (Me.FechaHoraInicio.Add(Me.Duracion) >= DateTime.Now)) Then
                    Return EstadoTarea.Realizando
                ElseIf (Me.FechaHoraInicio.Add(Me.Duracion) < DateTime.Now) Then
                    Return EstadoTarea.Realizada
                End If
                Return EstadoTarea.Pendiente
            End Get
        End Property

#End Region

#Region "Metodos"

        Public Sub New(ByVal sen As Integer, ByVal fhini As DateTime, ByVal dur As TimeSpan, ByVal per As Integer)
            Me._idsen = sen
            Me._fechaHoraInicio = fhini
            Me._duracion = dur
            Me._periodo = per
        End Sub

#End Region

    End Class

End Namespace

