Imports Centinela.ClassLib

Public Partial Class frmSesion
    Public usr As Usuario
    Private husr As HorarioUsuario
    Private datos As New AccesoRemoto()
	
	Public Sub New()
		Me.InitializeComponent()
	End Sub
	
    Sub BtnCancelarClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        End
    End Sub
	
    Sub BtnAceptarClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAceptar.Click
        Me.datos.Conectar()
        usr = Me.datos.SelecUsuario(Me.txtUsuario.Text, Me.txtClave.Text)
        Me.datos.Desconectar()
        If Not (usr Is Nothing) Then
            Me.datos.Conectar()
            Me.husr = Me.datos.SelectHorarioUsuario(CInt(Me.usr.Id))
            Me.datos.Desconectar()
            If Not (Me.husr Is Nothing) Then
                Dim finicio As DateTime = New DateTime(husr.FechaInicio.Year, husr.FechaInicio.Month, husr.FechaInicio.Day, husr.HoraInicio.Hour, husr.HoraInicio.Minute, 0)
                Dim ffin As DateTime = New DateTime(husr.FechaFin.Year, husr.FechaFin.Month, husr.FechaFin.Day, husr.HoraFin.Hour, husr.HoraFin.Minute, 59)
                If (DateTime.Now < finicio Or DateTime.Now > ffin) Then
                    MsgBox("Su horario de acceso no coincide con la fecha y hora actual", MsgBoxStyle.OkOnly, "Sesion")
                Else
                    Me.Dispose()
                End If
            Else
                MsgBox("Su horario de acceso no coincide con la fecha y hora actual", MsgBoxStyle.OkOnly, "Sesion")
            End If
        Else
            Me.datos.Conectar()
            usr = Me.datos.SelecAdministrador(Me.txtUsuario.Text, Me.txtClave.Text)
            Me.datos.Desconectar()
            If Not (usr Is Nothing) Then
                Me.Dispose()
            Else
                MsgBox("Usuario y / o Clave invalidos", MsgBoxStyle.OkOnly, "Sesion")
            End If
        End If
    End Sub
	
    Sub TextBox1TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtUsuario.TextChanged
        Me.RevisarBtnAceptar()
    End Sub
	
    Sub TxtClaveTextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtClave.TextChanged
        Me.RevisarBtnAceptar()
    End Sub
	
	Sub RevisarBtnAceptar()
		If(Me.txtUsuario.Text = "" Or Me.txtClave.Text = "") Then
			Me.btnAceptar.Enabled = False
		Else
			me.btnAceptar.Enabled = true
		End If
	End Sub
End Class
