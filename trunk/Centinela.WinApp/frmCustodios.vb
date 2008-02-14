Public Class frmCustodios
    Dim datos As ClassLib.AccesoDatos
    Dim custodios As List(Of ClassLib.Vigilante)
    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub frmCustodios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New ClassLib.AccesoDatos()
        Me.CargarCustodios()
    End Sub

    Public Sub CargarCustodios()
        datos.Conectar()
        Me.custodios = datos.SelectCustodios()
        Me.grdCustodios.Rows.Clear()
        For Each cst As ClassLib.Vigilante In Me.custodios
            Me.grdCustodios.Rows.Add()
            Me.grdCustodios.Rows(Me.grdCustodios.Rows.Count - 1).Cells("colNombre").Value = cst.Nombre
        Next
        datos.Desconectar()

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If (MsgBox("Esta usted seguro?", MsgBoxStyle.YesNo, "Confirmacion") = MsgBoxResult.Yes) Then
            datos.Conectar()
            datos.EliminarCustodio(Me.custodios(Me.grdCustodios.CurrentCell.RowIndex))
            datos.Desconectar()
            Me.CargarCustodios()
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim nombre As String = InputBox("Escriba el nombre del custodio", "Agregar Custodio")
        If (nombre <> "") Then
            datos.Conectar()
            Dim newId As Integer = datos.GetMaxId("vigilante") + 1
            datos.Desconectar()
            Dim cst As New ClassLib.Vigilante(newId.ToString(), nombre)
            datos.Conectar()
            datos.AgregarCustodio(cst)
            datos.Desconectar()
            Me.CargarCustodios()
        End If
    End Sub
End Class