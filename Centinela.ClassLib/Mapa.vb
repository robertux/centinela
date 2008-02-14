
Namespace ClassLib
	''' <summary>
    ''' Representa un mapa en el cual visualizar sensores
    ''' </summary>
    Public Class Mapa

#Region "Campos"

		''' <summary>
	    ''' Identificador unico del mapa
	    ''' </summary>
        Private _id As Integer
		''' <summary>
	    ''' Nombre representativo del mapa
	    ''' </summary>
        Private _nombre As String

#End Region

#Region "Propiedades"

		''' <summary>
	    ''' Devuelve o establece el identificador del mapa
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
	    ''' Devuelve o establece el nombre del mapa
	    ''' </summary>
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

		''' <summary>
	    ''' Crea una nueva instancia de la clase Mapa
	    ''' </summary>
        ''' <param name="newId">El id del mapa</param>
        ''' <param name="newNombre">El nombre del mapa</param>
        Public Sub New(ByVal newId As Integer, ByVal newNombre As String)
            Me._id = newId
            Me._nombre = newNombre
        End Sub

#End Region

    End Class
End Namespace
