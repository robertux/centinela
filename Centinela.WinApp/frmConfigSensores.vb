Imports XmlClassLib
Imports System.IO

Public Class frmConfigSensores
    Dim cfg As ConfigManager
    Private dlg, dlg2 As OpenFileDialog

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.cfg.EditValor("intervalo", Me.nudFrecuencia.Value.ToString())
        Me.Close()
    End Sub

    Private Sub frmConfigSensores_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cfg = New ConfigManager("app.config")
        Me.dlg = New OpenFileDialog()
        Me.dlg2 = New OpenFileDialog()
        dlg.Filter = "Imagenes(*.jpg;*.bmp;*.gif;*.png)|*.jpg;*.bmp;*.gif;*.png"
        dlg2.Filter = "Archivos wav(*.wav)|*.wav"

        Me.nudFrecuencia.Value = CDec(Me.cfg("intervalo"))
        Me.txtAlarmaSen.Text = Me.cfg("sndalarmasensor")
        Me.txtSenDesconectado.Text = Me.cfg("imgdesconectado")
        Me.txtSenDesactivado.Text = Me.cfg("imgdesactivado")
        Me.txtSenActivado.Text = Me.cfg("imgactivado")
    End Sub

    Private Sub txtSenDesconectado_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSenDesconectado.Enter
        Me.pbxImagenSensor.ImageLocation = Application.StartupPath + "\media\" + Me.txtSenDesconectado.Text
    End Sub

    Private Sub txtSenDesactivado_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSenDesactivado.Enter
        Me.pbxImagenSensor.ImageLocation = Application.StartupPath + "\media\" + Me.txtSenDesactivado.Text
    End Sub

    Private Sub txtSenActivado_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSenActivado.Enter
        Me.pbxImagenSensor.ImageLocation = Application.StartupPath + "\media\" + Me.txtSenActivado.Text
    End Sub

    Private Sub btnSenDesconectado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenDesconectado.Click        
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Me.CambiarImagen("SensorDesconectado", "imgdesconectado")
            Me.txtSenDesconectado.Text = Me.cfg("imgdesconectado")
            Me.txtSenDesconectado.Focus()
        End If
    End Sub

    Public Sub CambiarImagen(ByVal imgNombre As String, ByVal cfgClaveNombre As String)
        Dim mediaDir As String = Application.StartupPath + "\Media"
        File.Delete(Directory.GetFiles(mediaDir, imgNombre + "*")(0))
        Dim origenDir As String = dlg.FileName
        Dim destDir As String = mediaDir + "\" + imgNombre + dlg.FileName.Substring(dlg.FileName.LastIndexOf("."))
        File.Copy(origenDir, destDir)
        Dim fileName As String = destDir.Substring(destDir.LastIndexOf("\") + 1)
        Me.cfg.DelClave(cfgClaveNombre)
        Me.cfg.AddClave(cfgClaveNombre, fileName)
    End Sub

    Public Sub CambiarSonido(ByVal sndNombre As String, ByVal cfgClaveNombre As String)
        Dim mediaDir As String = Application.StartupPath + "\Media"
        File.Delete(Directory.GetFiles(mediaDir, sndNombre + "*")(0))
        Dim origenDir As String = dlg2.FileName
        Dim destDir As String = mediaDir + "\" + sndNombre + dlg2.FileName.Substring(dlg2.FileName.LastIndexOf("."))
        File.Copy(origenDir, destDir)
        Dim fileName As String = destDir.Substring(destDir.LastIndexOf("\") + 1)
        Me.cfg.DelClave(cfgClaveNombre)
        Me.cfg.AddClave(cfgClaveNombre, fileName)
    End Sub

    Private Sub btnSenDesactivado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenDesactivado.Click
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Me.CambiarImagen("SensorDesactivado", "imgdesactivado")
            Me.txtSenDesactivado.Text = Me.cfg("imgdesactivado")
            Me.txtSenDesactivado.Focus()
        End If
    End Sub

    Private Sub btnSenActivado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSenActivado.Click
        If (dlg.ShowDialog() = DialogResult.OK) Then
            Me.CambiarImagen("SensorActivado", "imgactivado")
            Me.txtSenActivado.Text = Me.cfg("imgactivado")
            Me.txtSenActivado.Focus()
        End If
    End Sub

    Private Sub btnAlarmaSen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlarmaSen.Click
        If (dlg2.ShowDialog() = DialogResult.OK) Then
            Me.CambiarSonido("Alarma", "imgdesconectado")
            Me.txtSenDesconectado.Text = Me.cfg("imgdesconectado")
            Me.txtSenDesconectado.Focus()
        End If
    End Sub
End Class