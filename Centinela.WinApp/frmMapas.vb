Public Class frmMapas
    Private mapas As List(Of ClassLib.Mapa)
    Private datos As ClassLib.AccesoDatos
    Private cargando As Boolean

    Private Sub frmMapas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.datos = New ClassLib.AccesoDatos()
        Me.CargarGrdMapas()
    End Sub

    Public Sub CargarGrdMapas()
        Me.grdMapas.Rows.Clear()
        datos.Conectar()
        Me.mapas = datos.SelectMapas()
        datos.Desconectar()
        Me.cargando = True
        For Each mpa As ClassLib.Mapa In Me.mapas
            Me.grdMapas.Rows.Add()
            Me.grdMapas.Rows(Me.grdMapas.Rows.Count - 1).Cells("colNombre").Value = mpa.Nombre
        Next
        Me.cargando = False
        Me.grdMapas_SelectionChanged(Me, New EventArgs())
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim dlg As New OpenFileDialog()
        dlg.Filter = "Imagenes(*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png"
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Dim stream As System.IO.Stream = dlg.OpenFile()
            Dim bytes(CInt(stream.Length)) As Byte
            stream.Read(bytes, 0, CInt(stream.Length))
            datos.Conectar()
            datos.SetImgMapa(Me.mapas(Me.grdMapas.CurrentCell.RowIndex), bytes)            
            datos.Desconectar()
            Me.grdMapas_SelectionChanged(Me, New EventArgs())
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (Me.grdMapas.Rows.Count > 0) Then
            If (MsgBox("Esta usted seguro?", MsgBoxStyle.YesNo, "Confirmacion") = MsgBoxResult.Yes) Then

                Dim lstSensores As New List(Of ClassLib.Sensor)
                datos.Conectar()
                lstSensores = datos.SelectSensores(Me.mapas(Me.grdMapas.CurrentCell.RowIndex).Id)
                datos.Desconectar()
                If (lstSensores.Count > 0) Then
                    MsgBox("El mapa no se puede eliminar porque contiene sensores asociados")
                Else
                    datos.Conectar()
                    datos.EliminarMapa(Me.mapas(Me.grdMapas.CurrentCell.RowIndex))
                    datos.Desconectar()
                End If


                Me.grdMapas_SelectionChanged(Me, New EventArgs())
            End If
        End If
        Me.CargarGrdMapas()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim frmAddMpa As New frmAgregarMapa()
        frmAddMpa.ShowDialog(Me)
        Me.CargarGrdMapas()
    End Sub
End Class