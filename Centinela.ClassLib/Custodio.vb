Namespace ClassLib

    Public Class Vigilante

#Region "Campos"

        Private _id As String
        Private _nombre As String

#End Region

#Region "Propiedades"

        Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return Me._nombre
            End Get
            Set(ByVal value As String)
                Me._nombre = value
            End Set
        End Property

#End Region

#Region "Metodos"

        Public Sub New(ByVal id As String, ByVal nombre As String)
            Me.Id = id
            Me.Nombre = nombre
        End Sub

#End Region

    End Class

End Namespace
