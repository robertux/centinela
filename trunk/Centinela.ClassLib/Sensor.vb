Imports System.Drawing
Imports System.Collections.Generic

Namespace ClassLib
	''' <summary>
    ''' Representa un Sensor
    ''' </summary>
    Public Class Sensor

#Region "Campos"

		''' <summary>
	    ''' Identificador unico del sensor
	    ''' </summary>
        Private _id As Integer
		''' <summary>
	    ''' Nombre representativo del sensor
	    ''' </summary>
        Private _nombre As String
		''' <summary>
	    ''' Id del estado actual del sensor
	    ''' </summary>
        Private _estadoActual As Integer
		''' <summary>
	    ''' Id del tipo de sensor
	    ''' </summary>
        Private _tipo As Integer
		''' <summary>
	    ''' Numero de pin asociado con el sensor
	    ''' </summary>
        Private _pin As Integer

#End Region

#Region "Eventos"

		''' <summary>
	    ''' Evento disparado cuando se enciende un sensor
	    ''' </summary>
        Public Event onEncender(ByVal fhOcur As DateTime)
		''' <summary>
	    ''' Evento disparado cuando se apaga un sensor
	    ''' </summary>
        Public Event onApagar(ByVal fhOcur As DateTime)
		''' <summary>
	    ''' Evento disparado cuando se activa un sensor
	    ''' </summary>
        Public Event onActivar(ByVal fhOcur As DateTime)
		''' <summary>
	    ''' Evento disparado cuando se desactiva un sensor
	    ''' </summary>
        Public Event onDesactivar(ByVal fhOcur As DateTime)

#End Region

#Region "Propiedades"

		''' <summary>
	    ''' Devuelve o establece el id del sensor
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
	    ''' Devuelve o establece el nombre del sensor
	    ''' </summary>
        Public Property Nombre() As String
            Get
                Return Me._nombre
            End Get
            Set(ByVal value As String)
                Me._nombre = value
            End Set
        End Property

		''' <summary>
	    ''' Devuelve o establece el id del estado actual del sensor
	    ''' </summary>
        Public Property EstadoActual() As Integer
            Get
                Return Me._estadoActual
            End Get
            Set(ByVal value As Integer)
                If (Me._estadoActual <> value) Then
                    Me._estadoActual = value
                    If (Me._estadoActual = 1) Then RaiseEvent onApagar(DateTime.Now)
                    If (Me._estadoActual = 2) Then RaiseEvent onDesactivar(DateTime.Now)
                    If (Me._estadoActual = 3) Then RaiseEvent onActivar(DateTime.Now)
                End If
            End Set
        End Property

		''' <summary>
	    ''' Devuelve o establece el id del tipo de sensor
	    ''' </summary>
        Public Property Tipo() As Integer
            Get
                Return Me._tipo
            End Get
            Set(ByVal value As Integer)
                Me._tipo = value
            End Set
        End Property

		''' <summary>
        ''' Devuelve o establece el numero del pin asociado con el sensor
	    ''' </summary>
        Public Property Pin() As Integer
            Get
                Return Me._pin
            End Get
            Set(ByVal value As Integer)
                Me._pin = value
            End Set
        End Property

#End Region

#Region "Metodos"

		''' <summary>
	    ''' Crea una nueva instancia de la clase sensor
	    ''' </summary>
        ''' <param name="id">El id del sensor</param>
        ''' <param name="nom">El nombre del sensor</param>
        ''' <param name="tipo">El id del tipo de sensor</param>
        ''' <param name="estado">El id del estado de sensor</param>
        ''' <param name="pn">El numero del pin asociado con el sensor</param>
        Public Sub New(ByVal id As Integer, ByVal nom As String, ByVal tipo As Integer, ByVal estado As Integer, ByVal pn As Integer)
            Me.Id = id
            Me.Nombre = nom
            Me._estadoActual = estado
            Me.Tipo = tipo
            Me.Pin = pn
        End Sub

		''' <summary>
	    ''' Cambia el estado del sensor a Encendido
	    ''' </summary>
        ''' <param name="autoActivar">Si es verdadero, activa automaticamente el sensor</param>
        Public Sub Encender(Optional ByVal autoActivar As Boolean = False)
            If (Me.EstadoActual = 1) Then
                If (autoActivar) Then
                    Me._estadoActual = 2
                    RaiseEvent onActivar(DateTime.Now)
                Else
                    Me._estadoActual = 3
                    RaiseEvent onDesactivar(DateTime.Now)
                End If
                RaiseEvent onEncender(DateTime.Now)
            End If
        End Sub

		''' <summary>
	    ''' Cambia el estado del sensor a Apagado
	    ''' </summary>
        Public Sub Apagar()
            If (Me.EstadoActual = 2) Then
                Me._estadoActual = 1
                RaiseEvent onApagar(DateTime.Now)
            End If
        End Sub

		''' <summary>
	    ''' Cambia el estado del sensor a Activo
	    ''' </summary>
        Public Sub Activar()
            If (Me.EstadoActual <> 3) Then
                Me._estadoActual = 3
                RaiseEvent onActivar(DateTime.Now)
            End If
        End Sub

		''' <summary>
	    ''' Cambia el estado del sensor a Inactivo
	    ''' </summary>
        Public Sub Desactivar()
            If (Me.EstadoActual <> 2) Then
                Me._estadoActual = 2
                RaiseEvent onDesactivar(DateTime.Now)
            End If
        End Sub

#End Region
    End Class

End Namespace
