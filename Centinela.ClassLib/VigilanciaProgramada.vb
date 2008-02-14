Namespace ClassLib

    Public Class VigilanciaProgramada
        Inherits ActividadProgramada

#Region "Campos"

        Private _vgl As Vigilante

#End Region

#Region "Propiedades"

        Public Property Vigi() As Vigilante
            Get
                Return Me._vgl
            End Get
            Set(ByVal value As Vigilante)
                Me._vgl = value
            End Set
        End Property

#End Region

#Region "Metodos"

        Public Sub New(ByVal vgl As Vigilante, ByVal sn As Integer, ByVal fhvigil As DateTime, ByVal dur As TimeSpan, ByVal per As Integer)
            MyBase.New(sn, fhvigil, dur, per)
            Me._vgl = vgl
        End Sub

#End Region

    End Class

End Namespace
