Imports Centinela.ClassLib

Public Class frmAgregarSensor
    Private datos As AccesoDatos

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub txtNombre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        Me.VerificarBtnAceptar()
    End Sub

    Public Sub VerificarBtnAceptar()
        If (Me.txtNombre.Text <> "" And Me.lstPines.Items.Count > 0) Then
            Me.btnAgregar.Enabled = True
        Else
            Me.btnAgregar.Enabled = False
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        datos.Conectar()
        Dim newId As Integer = datos.GetMaxId("Sensor") + 1
        datos.Desconectar()        
        datos.Conectar()
        Dim idTipoSen As Integer = datos.GetIdTipoSensor(Me.cmbTipoSensor.Text)
        datos.Desconectar()
        Dim sen As New Sensor(newId, Me.txtNombre.Text, idTipoSen, 1, CInt(Me.lstPines.Items(Me.lstPines.SelectedIndex)))
        datos.Conectar()
        datos.AgregarSensor(sen)
        datos.Desconectar()
        Me.Close()
    End Sub

    Private Sub frmAgregarSensor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New AccesoDatos()
        Me.RellenarTipos()
        Me.CargarPinesDisponibles()
    End Sub

    Public Sub RellenarTipos()
        Me.cmbTipoSensor.Items.Clear()
        datos.Conectar()
        Dim tipos As List(Of String) = datos.GetTiposSensor()
        datos.Desconectar()
        For Each tiposen As String In tipos
            Me.cmbTipoSensor.Items.Add(tiposen)
        Next
        Me.cmbTipoSensor.SelectedIndex = 0
    End Sub

    Public Sub CargarPinesDisponibles()
        Dim pinesOcupados As New List(Of Integer)
        datos.Conectar()
        Dim sensores As List(Of Sensor) = datos.SelectSensores()
        datos.Desconectar()
        if not(sensores is nothing)
            For Each sen As Sensor In sensores
                pinesOcupados.Add(sen.Pin)
            Next
        End If
        For i As Integer = 1 To 16
            If Not (pinesOcupados.Contains(i)) Then
                Me.lstPines.Items.Add(i)
            End If
        Next
        If (Me.lstPines.Items.Count > 0) Then
            Me.lstPines.SelectedIndex = 0
        End If
    End Sub
End Class