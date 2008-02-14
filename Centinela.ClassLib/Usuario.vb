Namespace ClassLib
	''' <summary>
    ''' Representa un Usuario de la aplicacion
    ''' </summary>
    <Serializable()> Public Class Usuario

#Region "Campos"
        ''' <summary>
        ''' Identificador unico del usuario
        ''' </summary>
        Private _id As String
        ''' <summary>
        ''' Nombre completo del usuario
        ''' </summary>
        Private _nombreCompleto As String
        ''' <summary>
        ''' Clave de acceso del usuario
        ''' </summary>
        Private _clave As String
        ''' <summary>
        ''' Nickname del usuario
        ''' </summary>
        Private _nombreUsuario As String
        ''' <summary>
        ''' Si el usuario es visible o no
        ''' </summary>
        Private _visible As Boolean

#End Region

#Region "Propiedades"

        ''' <summary>
        ''' Devuelve o establece el identificador del usuario
        ''' </summary>
        Public Property Id() As String
            Get
                Return Me._id
            End Get
            Set(ByVal value As String)
                Me._id = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el nombre completo del usuario
        ''' </summary>
        Public Property NombreCompleto() As String
            Get
                Return Me._nombreCompleto
            End Get
            Set(ByVal value As String)
                Me._nombreCompleto = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece la clave de acceso del usuario
        ''' </summary>
        Public Property Clave() As String
            Get
                Return Me._clave
            End Get
            Set(ByVal value As String)
                Me._clave = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece el Nickname del usuario
        ''' </summary>
        Public Property NombreUsuario() As String
            Get
                Return Me._nombreUsuario
            End Get
            Set(ByVal value As String)
                Me._nombreUsuario = value
            End Set
        End Property

        ''' <summary>
        ''' Devuelve o establece si el usuario es visible o no
        ''' </summary>
        Public Property Visible() As Boolean
            Get
                Return Me._visible
            End Get
            Set(ByVal value As Boolean)
                Me._visible = value
            End Set
        End Property

#End Region

#Region "Metodos"

        ''' <summary>
        ''' Crea una nueva instancia de la clase Usuario
        ''' </summary>
        ''' <param name="id">El id del usuario</param>
        ''' <param name="nomComp">El nombre completo del usuario</param>
        ''' <param name="clv">La clave de acceso del usuario</param>
        ''' <param name="nomUsr">El Nickname del usuario</param>
        ''' <param name="vis">Si el usuario es visible</param>
        Public Sub New(ByVal id As String, ByVal nomComp As String, ByVal clv As String, ByVal nomUsr As String, ByVal vis As Boolean)
            Me.Id = id
            Me.NombreCompleto = nomComp
            Me.Clave = clv
            Me.NombreUsuario = nomUsr
            Me.Visible = vis
        End Sub

#End Region

    End Class

End Namespace
