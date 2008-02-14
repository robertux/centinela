Imports Centinela.ClassLib

Public Class frmSensores
    Private datos As AccesoDatos
    Private sensores As List(Of Sensor)
    Private mapas As List(Of Mapa)
    Private cargando As Boolean
    Private nombreAnterior As String = ""

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Dim faltaSensorEnMapa As Boolean = False
        If Not (sensores Is Nothing) Then
            For Each sen As Sensor In sensores
                Dim listaMapas As New List(Of Mapa)
                Me.datos.Conectar()
                listaMapas = Me.datos.SelectMapas(sen.Id)
                Me.datos.Desconectar()
                If (listaMapas Is Nothing) Then
                    faltaSensorEnMapa = True
                Else
                    If (listaMapas.Count = 0) Then
                        faltaSensorEnMapa = True
                    End If
                End If
            Next
        End If
        If (faltaSensorEnMapa) Then
            MsgBox("Hay sensores que aun no han sido asignados a algun mapa")
        Else
            Me.Close()
        End If

    End Sub

    Private Sub frmSensores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New AccesoDatos()
        Me.CargarCmbTiposSensores()
        Me.CargarSensores()
    End Sub

    Public Sub CargarSensores()
        datos.Conectar()
        Me.lstSensores.Items.Clear()

        Me.sensores = New List(Of Sensor)

        Me.sensores = datos.SelectSensores()
        If (Not Me.sensores Is Nothing) Then
            Me.lstSensores.Items.Clear()
            Me.cargando = True
            For Each sen As Sensor In Me.sensores
                Me.lstSensores.Items.Add(sen.Id.ToString() + " - " + sen.Nombre)
            Next
            Me.cargando = False
            If (Me.sensores.Count > 0) Then
                Me.lstSensores.SelectedIndex = 0
            End If
        End If
        datos.Desconectar()

    End Sub

    Private Sub lstSensores_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSensores.SelectedIndexChanged
        If Not (Me.cargando) Then
            datos.Desconectar()
            datos.Conectar()
            Dim tiposen As String = datos.GetNombreTipoSensor(Me.sensores(Me.lstSensores.SelectedIndex).Tipo)
            datos.Desconectar()
            For i As Integer = 0 To Me.cmbTipo.Items.Count - 1
                If (Me.cmbTipo.Items(i).ToString() = tiposen) Then
                    Me.cmbTipo.SelectedIndex = i
                End If
            Next
            Me.txtNombre.Text = Me.sensores(Me.lstSensores.SelectedIndex).Nombre
            Me.nombreAnterior = Me.txtNombre.Text
            Me.CargarMapas(Me.sensores(Me.lstSensores.SelectedIndex).Id)
        End If
    End Sub

    Public Sub CargarMapas(ByVal idSen As Integer)
        Me.lstMapas.Items.Clear()
        Me.datos.Conectar()
        Me.mapas = Me.datos.SelectMapas(idSen)
        Me.datos.Desconectar()
        Me.cargando = True
        For Each mpa As Mapa In mapas
            Me.datos.Conectar()
            Dim posSen As System.Drawing.Point = datos.GetPosSensor(idSen, mpa.Id)
            Me.datos.Desconectar()
            Dim strPos As String = ", en la posicion (X:" + posSen.X.ToString() + " , Y:" + posSen.Y.ToString() + ")"
            Me.lstMapas.Items.Add(mpa.Nombre.Trim() + strPos)
        Next
        Me.cargando = False        
        If (Me.lstMapas.Items.Count > 0) Then
            Me.lstMapas.SelectedIndex = 0
        End If
    End Sub

    Public Sub CargarCmbTiposSensores()
        Me.datos.Conectar()
        Dim lst As List(Of String) = Me.datos.GetTiposSensor()
        Me.cmbTipo.Items.Clear()
        For Each tiposen As String In lst
            Me.cmbTipo.Items.Add(tiposen)
        Next
        Me.datos.Desconectar()
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        datos.Conectar()
        Me.sensores(Me.lstSensores.SelectedIndex).Tipo = datos.GetIdTipoSensor(Me.cmbTipo.Text)
        datos.Desconectar()
        datos.Conectar()
        datos.ModificarSensor(Me.sensores(Me.lstSensores.SelectedIndex))
        datos.Desconectar()        
    End Sub

    Private Sub txtNombre_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.Leave
        If (Me.txtNombre.Text <> Me.nombreAnterior) Then
            Me.sensores(Me.lstSensores.SelectedIndex).Nombre = Me.txtNombre.Text
            datos.Conectar()
            datos.ModificarSensor(Me.sensores(Me.lstSensores.SelectedIndex))
            datos.Desconectar()
            Me.nombreAnterior = Me.txtNombre.Text
            Me.CargarSensores()
        End If
    End Sub

    Private Sub btnAgregarMpa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarMpa.Click
        Dim frmSelMpa As New frmSelectMapa(Me.sensores(Me.lstSensores.SelectedIndex).Id)
        frmSelMpa.ShowDialog(Me)
        If (frmSelMpa.Acepted) Then
            Dim mpa As Mapa = frmSelMpa.MapaSeleccionado
            datos.Conectar()
            Dim nuevoId As Integer = datos.GetMaxId("sensor_en_mapa") + 1
            datos.Desconectar()
            datos.Conectar()
            datos.AgregarSensorMapa(nuevoId, Me.sensores(Me.lstSensores.SelectedIndex).Id, mpa.Id)
            datos.Desconectar()
            Me.CargarMapas(Me.sensores(Me.lstSensores.SelectedIndex).Id)
        End If
    End Sub

    Private Sub btnEliminarMpa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarMpa.Click
        If (Me.lstMapas.Items.Count > 0) Then
            If (MsgBox("Esta usted seguro?", MsgBoxStyle.YesNo, "Confirmacion") = MsgBoxResult.Yes) Then
                datos.Conectar()
                datos.EliminarSensorMapa(Me.sensores(Me.lstSensores.SelectedIndex).Id, Me.mapas(Me.lstMapas.SelectedIndex).Id)
                datos.Desconectar()
                Me.CargarMapas(Me.sensores(Me.lstSensores.SelectedIndex).Id)
            End If
        End If        
    End Sub

    Private Sub btnAgregarSen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregarSen.Click
        Dim frmaddsen As New frmAgregarSensor()
        frmaddsen.ShowDialog(Me)
        Me.CargarSensores()
    End Sub

    Private Sub btnEliminarSen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarSen.Click
        If (Me.lstSensores.Items.Count > 0) Then
            If (MsgBox("Esta usted seguro?", MsgBoxStyle.YesNo, "Confirmacion") = MsgBoxResult.Yes) Then
                datos.Conectar()
                datos.EliminarSensor(Me.sensores(Me.lstSensores.SelectedIndex))
                datos.Desconectar()
                Me.CargarSensores()
            End If
        End If
    End Sub
End Class