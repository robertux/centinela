Public Class frmSelectMapa
    Private mapas As List(Of ClassLib.Mapa)
    Private mapasExcluidos As List(Of ClassLib.Mapa)
    Private datos As ClassLib.AccesoDatos
    Private cargando As Boolean
    Private _acepted As Boolean
    Private idsen As Integer

    Public Property Acepted() As Boolean
        Get
            Return Me._acepted
        End Get
        Set(ByVal value As Boolean)
            Me._acepted = value
        End Set
    End Property

    Public ReadOnly Property MapaSeleccionado() As ClassLib.Mapa
        Get
            Return Me.mapas(Me.grdMapas.CurrentCell.RowIndex)
        End Get
    End Property

    Public Sub New(ByVal idsensor As Integer)
        MyBase.New()
        Me.InitializeComponent()
        Me.idsen = idsensor
    End Sub

    Private Sub frmMapas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New ClassLib.AccesoDatos()
        Me.CargarGrdMapas()
    End Sub

    Public Sub CargarGrdMapas()
        Me.grdMapas.Rows.Clear()
        datos.Conectar()
        Me.mapas = datos.SelectMapas()
        datos.Desconectar()
        datos.Conectar()
        Me.mapasExcluidos = datos.SelectMapas(Me.idsen)
        datos.Desconectar()
        Do While (Me.mapasExcluidos.Count > 0)
            For i As Integer = 0 To Me.mapas.Count - 1
                If (Me.mapasExcluidos(0).Id = Me.mapas(i).Id) Then
                    Me.mapas.RemoveAt(i)
                    Me.mapasExcluidos.RemoveAt(0)
                    Exit For
                End If
            Next
        Loop
        Me.cargando = True
        For Each mpa As ClassLib.Mapa In Me.mapas
            Me.grdMapas.Rows.Add()
            Me.grdMapas.Rows(Me.grdMapas.Rows.Count - 1).Cells("colNombre").Value = mpa.Nombre
        Next
        Me.cargando = False
        If (Me.mapas.Count > 0) Then
            Me.grdMapas_SelectionChanged(Me, New EventArgs())
            Me.btnAceptar.Enabled = True
        End If
    End Sub

    Private Sub grdMapas_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdMapas.SelectionChanged
        If Not (Me.cargando) Then
            datos.Conectar()
            Dim bytes As Byte() = datos.GetImgMapa(Me.mapas(Me.grdMapas.CurrentCell.RowIndex))
            If Not (bytes Is Nothing) Then
                Dim mstream As New System.IO.MemoryStream(bytes.Length)
                mstream.Write(bytes, 0, bytes.Length)
                Dim img As New Bitmap(mstream)
                Me.pbxImagenMapa.Image = img
            End If
            datos.Desconectar()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Acepted = False
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Acepted = True
        Me.Close()
    End Sub
End Class