Imports Centinela.ClassLib

Public Class frmCambiarClave
    Dim datos As New AccesoDatos()
    Dim usr As Usuario

    Public Sub New(ByVal codUsr As String)
        InitializeComponent()
        datos.Conectar()
        Me.usr = datos.SelecUsuario(codUsr)
        datos.Desconectar()
    End Sub
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If (Me.txtClaveAnterior.Text <> usr.Clave.Trim) Then
            MsgBox("Clave anterior incorrecta", MsgBoxStyle.OkOnly, "Error")
            Return
        End If
        Me.usr.Clave = Me.txtClaveNueva.Text
        datos.Conectar()
        datos.ModificarUsuario(Me.usr)
        datos.Desconectar()
        Me.Close()
    End Sub

    Private Sub txtClaveAnterior_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClaveAnterior.TextChanged
        Me.VerificarBtnAceptar()
    End Sub

    Private Sub VerificarBtnAceptar()
        If (Me.txtClaveAnterior.Text = "" Or Me.txtClaveNueva.Text = "") Then
            Me.btnAceptar.Enabled = False
        Else
            Me.btnAceptar.Enabled = True
        End If
    End Sub

    Private Sub txtClaveNueva_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClaveNueva.TextChanged
        Me.VerificarBtnAceptar()
    End Sub
End Class