Public Class frmAddUsuario

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim datos As New ClassLib.AccesoDatos()
        datos.Conectar()
        Dim esUsr As ClassLib.Usuario = datos.SelecUsuario(Me.txtUsuario.Text, Me.txtClave.Text)
        datos.Desconectar()
        datos.Conectar()
        If Not (esUsr Is Nothing) Then
            MsgBox("Usuario existente", MsgBoxStyle.OkOnly, "Error")
        Else
            Dim usr As New ClassLib.Usuario((datos.GetMaxId("usuario") + 1).ToString(), Me.txtNombreCompleto.Text, Me.txtClave.Text, Me.txtUsuario.Text, True)
            datos.Desconectar()
            datos.Conectar()
            datos.AgregarUsuario(usr)
            Me.Close()
        End If

    End Sub

    Private Sub txtNombreCompleto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombreCompleto.TextChanged
        Me.VerificarBtnAceptar()
    End Sub

    Private Sub VerificarBtnAceptar()
        If (Me.txtNombreCompleto.Text = "" Or Me.txtUsuario.Text = "" Or Me.txtClave.Text = "") Then
            Me.btnAceptar.Enabled = False
        Else
            Me.btnAceptar.Enabled = True
        End If
    End Sub

    Private Sub txtUsuario_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsuario.TextChanged
        Me.VerificarBtnAceptar()
    End Sub

    Private Sub txtClave_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClave.TextChanged
        Me.VerificarBtnAceptar()
    End Sub
End Class