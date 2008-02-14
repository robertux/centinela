Public Class frmAgregarMapa
    Private datos As ClassLib.AccesoDatos
    Private dlg As OpenFileDialog

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click        
        Me.datos.Conectar()
        Dim nuevoId As Integer = datos.GetMaxId("mapa") + 1
        Me.datos.Desconectar()
        Dim mpa As New ClassLib.Mapa(nuevoId, Me.txtNombre.Text)

        Me.datos.Conectar()
        Me.datos.AgregarMapa(mpa)
        Me.datos.Desconectar()
        Dim stream As System.IO.Stream = dlg.OpenFile()
        Dim bytes(CInt(stream.Length)) As Byte
        stream.Read(bytes, 0, CInt(stream.Length))

        datos.Conectar()
        datos.SetImgMapa(mpa, bytes)
        datos.Desconectar()
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click        
        dlg.Filter = "Imagenes(*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png"
        If (dlg.ShowDialog() = Windows.Forms.DialogResult.OK) Then
            Dim stream As System.IO.Stream = dlg.OpenFile()
            Dim bytes(CInt(stream.Length)) As Byte
            stream.Read(bytes, 0, CInt(stream.Length))
            Me.pbxImagen.Image = New System.Drawing.Bitmap(stream)
            Me.txtImagen.Text = dlg.FileName
        End If
        Me.VerificarBtnAceptar()
    End Sub

    Public Sub VerificarBtnAceptar()
        If (Me.txtNombre.Text <> "" And Me.txtImagen.Text <> "") Then
            Me.btnAceptar.Enabled = True
        Else
            Me.btnAceptar.Enabled = False
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged
        Me.VerificarBtnAceptar()
    End Sub

    Private Sub frmAgregarMapa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New ClassLib.AccesoDatos()
        Me.dlg = New OpenFileDialog()
    End Sub
End Class