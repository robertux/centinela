''' <summary>
''' Representa un servicio de revision del puerto paralelo y actualizacion de sensores
''' </summary>
''' <remarks></remarks>
Public Class Servicio
    Inherits Timer

    ''' <summary>
    ''' Lee datos del puerto paralelo
    ''' </summary>
    ''' <param name="PortAddress">Direccion del puerto</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Declare Function Inp Lib "inpout32.dll" Alias "Inp32" (ByVal PortAddress As Short) As Short
    ''' <summary>
    ''' Escribe datos en el puerto paralelo
    ''' </summary>
    ''' <param name="PortAddress">Direccion del puerto</param>
    ''' <param name="Value">Valor decimal a escribir</param>
    ''' <remarks></remarks>
    Public Declare Sub Outp Lib "inpout32.dll" Alias "Out32" (ByVal PortAddress As Short, ByVal Value As Short)

#Region "Campos"

    ''' <summary>
    ''' Arreglo que contiene los bits leidos del puerto de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private _datos As List(Of Short)
    ''' <summary>
    ''' Arreglo que contiene los bits leidos del puerto de control
    ''' </summary>
    ''' <remarks></remarks>
    Private _control As List(Of Short)
    ''' <summary>
    ''' Arreglo que contiene los bits leidos del puerto de status
    ''' </summary>
    ''' <remarks></remarks>
    Private _status As List(Of Short)

    ''' <summary>
    ''' Representa la direccion del puerto de datos
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PUERTODATOS As Integer = 888 'Activado/Desactivado
    ''' <summary>
    ''' Representa la direccion del puerto de status
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PUERTOSTATUS As Integer = 889
    ''' <summary>
    ''' Representa la direccion del puerto de control
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PUERTOCONTROL As Integer = 890

    ''' <summary>
    ''' Constante utilizada para representar la lectura de los 1ros 8 pines
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRIMEROSOCHOPINES As Boolean = True
    ''' <summary>
    ''' Constante utilizada para representar la lectura de los 2dos 8 pines
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEGUNDOSOCHOPINES As Boolean = False

    ''' <summary>
    ''' Evento disparado cuando cambian los valores en el puerto de datos
    ''' </summary>
    ''' <param name="nuevoVal"></param>
    ''' <remarks></remarks>
    Public Event CambioPuertoDatos(ByVal nuevoVal As List(Of Short))
    ''' <summary>
    ''' Evento disparado cuando cambian los valores en el puerto de control
    ''' </summary>
    ''' <param name="nuevoVal"></param>
    ''' <remarks></remarks>
    Public Event CambioPuertoControl(ByVal nuevoVal As List(Of Short))
    ''' <summary>
    ''' Evento disparado cuando cambian los valores en el puerto de status
    ''' </summary>
    ''' <param name="nuevoVal"></param>
    ''' <remarks></remarks>
    Public Event CambioPuertoStatus(ByVal nuevoVal As List(Of Short))

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Devuelve o establece la lista de bits leidos del puerto de datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Datos() As List(Of Short)
        Get
            Return Me._datos
        End Get
        Set(ByVal value As List(Of Short))
            Me._datos = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece la lista de bits leidos del puerto de control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Control() As List(Of Short)
        Get
            Return Me._control
        End Get
        Set(ByVal value As List(Of Short))
            Me._control = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve o establece la lista de bits leidos del puerto de status
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status() As List(Of Short)
        Get
            Return Me._status
        End Get
        Set(ByVal value As List(Of Short))
            Me._status = value
        End Set
    End Property

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Crea una instancia de la clase servicio
    ''' </summary>
    Public Sub New()
        MyBase.New()
        Me.Init()
    End Sub

    ''' <summary>
    '''Crea una instancia de la clase servicio
    ''' </summary>
    Public Sub New(ByVal contanierrr As System.ComponentModel.IContainer)
        MyBase.New(contanierrr)
        Me.Init()
    End Sub

    ''' <summary>
    '''Inicializa los miembros de la clase
    ''' </summary>
    Public Sub Init()
        Me._datos = New List(Of Short)(8)
        Me._control = New List(Of Short)(8)
        Me._status = New List(Of Short)(8)
    End Sub

    ''' <summary>
    '''Verifica el estado de los puertos
    ''' </summary>
    ''' <param name="leerPrimerosOcho">Define si se leen los primeros ocho o los segundos</param>
    Public Sub VerificarPuertos(ByVal leerPrimerosOcho As Boolean)
        If (leerPrimerosOcho) Then
            Outp(Servicio.PUERTOCONTROL, 240) 'Leer los 1ros 8 bits
        Else
            Outp(Servicio.PUERTOCONTROL, 242) 'Leer los 2dos 8 bits
        End If
        Me.Init()
        Dim entradaDatos As Short = Inp(Servicio.PUERTODATOS)
        Dim pDatos As Short() = DecToBin(entradaDatos)
        Dim entradaControl As Short = Inp(Servicio.PUERTOCONTROL)
        Dim pControl As Short() = DecToBin(entradaControl)
        Dim entradaStatus As Short = Inp(Servicio.PUERTOSTATUS)
        Dim pStatus As Short() = DecToBin(entradaControl)

        Me._datos.Clear()
        Me._control.Clear()
        Me._status.Clear()
        For i As Integer = 0 To 7
            Me._datos.Add(pDatos(i))
            Me._control.Add(pControl(i))
            Me._status.Add(pStatus(i))
        Next
    End Sub

    ''' <summary>
    '''Actualiza el estado del puerto de encendido
    ''' </summary>
    ''' <param name="bites">los bites a escribir</param>
    Public Sub ActualizarPuertoEncendido(ByVal bites As Short())
        Outp(Servicio.PUERTOSTATUS, Me.BinToDec(bites))
    End Sub

    ''' <summary>
    '''Actualiza el estado del puerto de activacion
    ''' </summary>
    ''' <param name="bites">los bites a escribir</param>
    Public Sub ActualizarPuertoActivacion(ByVal bites As Short())
        Outp(Servicio.PUERTOCONTROL, Me.BinToDec(bites))
    End Sub

    ''' <summary>
    '''Convierte un arreglo de valores que representan un numero binario a su equivalente decimal
    ''' </summary>
    ''' <param name="bites">los bites a convertir</param>
    Public Function BinToDec(ByVal bites As Short()) As Short
        Dim valor As Short = 0
        For i As Integer = 0 To bites.Length - 1
            valor += CType(((2 ^ i) * bites(bites.Length - 1 - i)), Short)
        Next
    End Function

    ''' <summary>
    '''Convierte un numero decimal su equivalente en un arreglo de bits
    ''' </summary>
    ''' <param name="NDecimal">el numero decimal a convertir</param>
    Public Function DecToBin(ByVal NDecimal As Short) As Short()

        Dim Binario(7) As Short
        Binario(0) = 0
        Binario(1) = 0
        Binario(2) = 0
        Binario(3) = 0
        Binario(4) = 0
        Binario(5) = 0
        Binario(6) = 0
        Binario(7) = 0
        If NDecimal = 0 Then
            Return Binario
        End If


        Dim bin As String = ""
        Do While Not NDecimal = 1
            bin = bin + (NDecimal Mod 2).ToString.Trim
            NDecimal = CShort(NDecimal \ 2)
        Loop
        bin = bin + "1"
        Try
            Binario(0) = CShort(bin.Substring(0, 1))
        Catch ex As Exception
            Binario(0) = 0
        End Try
        Try
            Binario(1) = CShort(bin.Substring(1, 1))
        Catch ex As Exception
            Binario(1) = 0
        End Try
        Try
            Binario(2) = CShort(bin.Substring(2, 1))
        Catch ex As Exception
            Binario(2) = 0
        End Try
        Try
            Binario(3) = CShort(bin.Substring(3, 1))
        Catch ex As Exception
            Binario(3) = 0
        End Try
        Try
            Binario(4) = CShort(bin.Substring(4, 1))
        Catch ex As Exception
            Binario(4) = 0
        End Try
        Try
            Binario(5) = CShort(bin.Substring(5, 1))
        Catch ex As Exception
            Binario(5) = 0
        End Try
        Try
            Binario(6) = CShort(bin.Substring(6, 1))
        Catch ex As Exception
            Binario(6) = 0
        End Try
        Try
            Binario(7) = CShort(bin.Substring(7, 1))
        Catch ex As Exception
            Binario(7) = 0
        End Try


        Return Binario
    End Function

    ''' <summary>
    '''Actualiza el estado de los sensores
    ''' </summary>
    ''' <param name="sensores">La lista de sensores a actualizar</param>
    Public Sub ActualizarSensores(ByRef sensores As List(Of SensorVisual))
        'Verificando los sensores asociados con los "1ros 8 pines" del puerto de datos...
        Me.VerificarPuertos(Servicio.PRIMEROSOCHOPINES)
        For Each senvis As SensorVisual In sensores
            If (senvis.Sen.Pin <= 8) Then
                Dim estadoSen As Integer = CInt(Me.Datos(senvis.Sen.Pin - 1))
                If (estadoSen = 1) Then
                    senvis.Sen.Desactivar()
                ElseIf (estadoSen = 0) Then
                    senvis.Sen.Activar()
                    frmMain.DispararNotificador("Sensor Activado")
                End If
            End If
        Next

        'Verificando los sensores asociados con los "2dos 8 pines" del puerto de datos...
        Me.VerificarPuertos(Servicio.SEGUNDOSOCHOPINES)
        For Each senvis As SensorVisual In sensores
            If (senvis.Sen.Pin > 8) Then
                Dim estadoSen As Integer = CInt(Me.Datos(senvis.Sen.Pin - 9))
                If (estadoSen = 1) Then
                    senvis.Sen.Desactivar()
                ElseIf (estadoSen = 0) Then
                    senvis.Sen.Activar()
                    frmMain.DispararNotificador("Sensor Activado")
                End If
            End If
        Next
    End Sub

#End Region

End Class
