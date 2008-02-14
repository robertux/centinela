Imports Centinela.ClassLib
Imports System.Collections.Generic

Public Partial Class frmUsuarios
    Dim usrs As List(Of Usuario)
    Dim datos As New AccesoDatos()
    Public Sub New()
        Me.InitializeComponent()
    End Sub
	
    Sub BtnCerrarClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub frmUsuarios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CargarUsuarios()
    End Sub

    Private Sub CargarUsuarios()
        datos.Conectar()
        Me.usrs = datos.SelectUsuarios()
        datos.Desconectar()
        Me.grdUsuarios.Rows.Clear()
        For Each usr As Usuario In usrs
            If (usr.Visible) Then
                Me.grdUsuarios.Rows.Add()
                Me.grdUsuarios.Rows(Me.grdUsuarios.Rows.Count - 1).Cells("colNombreCompleto").Value = usr.NombreCompleto
            End If
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cclave As New frmCambiarClave(Me.usrs(Me.grdUsuarios.CurrentCell.RowIndex).Id)
        cclave.ShowDialog(Me)
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        If (Me.grdUsuarios.Rows.Count > 0) Then
            If (MsgBox("Esta usted seguro?", MsgBoxStyle.YesNo, "Confirmacion") = MsgBoxResult.Yes) Then
                datos.Conectar()
                datos.EliminarUsuario(Me.usrs(Me.grdUsuarios.CurrentCell.RowIndex))
                datos.Desconectar()
                Me.CargarUsuarios()
            End If
        End If
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim frmaddusr As New frmAddUsuario()
        frmaddusr.ShowDialog(Me)
        Me.CargarUsuarios()
    End Sub

    Private Sub btnHorario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHorario.Click
        Dim frmHUsr As New frmHorarioUsuario(CInt(Me.usrs(Me.grdUsuarios.CurrentCell.RowIndex).Id))
        frmHUsr.ShowDialog(Me)
    End Sub
End Class
