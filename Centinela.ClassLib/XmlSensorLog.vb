Imports System.Xml
Imports System.IO

Namespace ClassLib
	''' <summary>
    ''' Representa una clase capaz de almacenar una lista de logs en un archivo XML
    ''' </summary>
    Public Class XmlSensorLog

		''' <summary>
	    ''' Genera un archivo XML con la lista de logs encontrados en la tabla log de la base de datos
	    ''' </summary>
        ''' <param name="periodo">Define un periodo para filtrar la lista de logs a generar</param>
        ''' <param name="nombreArchivoXml">Define el nombre del archivo XML en el cual guardar la lista de logs. Por defecto es bdlog.xml</param>
        Public Shared Sub GenerarXml(Optional ByVal periodo As String = "", Optional ByVal nombreArchivoXml As String = "")
            Dim datos As New AccesoDatos()
            If (nombreArchivoXml = "") Then nombreArchivoXml = "bdlog.xml"
            If (File.Exists(nombreArchivoXml)) Then
                File.Delete(nombreArchivoXml)
            End If
            Dim fstream As FileStream = File.Create(nombreArchivoXml)

            Dim escritor As New System.Xml.XmlTextWriter(fstream, New System.Text.UTF8Encoding())
            escritor.WriteStartDocument()
            escritor.Formatting = Formatting.Indented
            escritor.WriteStartElement("bdlog")

            datos.Conectar()
            Dim logs As List(Of Log)
            If (periodo = "") Then
                logs = datos.SelectLogs()
            Else
                logs = datos.SelectLogs(periodo)
            End If
            datos.Desconectar()
            If Not (logs Is Nothing) Then
                For Each lg As Log In logs
                    datos.Conectar()
                    Dim sen As Sensor = datos.SelectSensor(lg.IdSensor.ToString())
                    datos.Desconectar()
                    datos.Conectar()
                    Dim tipoSen As String = datos.GetNombreTipoSensor(sen.Tipo)
                    datos.Desconectar()
                    datos.Conectar()
                    Dim estadoSen As String = datos.GetNombreEstadoSensor(lg.IdEstadoSensor)
                    datos.Desconectar()
                    datos.Conectar()
                    Dim usr As Usuario = datos.SelecUsuario(lg.IdUsuario.ToString())
                    datos.Desconectar()

                    escritor.WriteStartElement("log")
                    escritor.WriteAttributeString("fechahora", lg.FechaHora.ToShortDateString() + " " + lg.FechaHora.ToShortTimeString)
                    escritor.WriteAttributeString("sensor", sen.Nombre.Trim())
                    escritor.WriteAttributeString("tiposensor", tipoSen.Trim())
                    escritor.WriteAttributeString("estadosensor", estadoSen.Trim())
                    escritor.WriteAttributeString("suceso", lg.Suceso.Trim())
                    escritor.WriteAttributeString("usuario", usr.NombreCompleto.Trim())
                    escritor.WriteEndElement()
                Next
            Else
                escritor.WriteStartElement("log")
                escritor.WriteAttributeString("fechahora", "")
                escritor.WriteAttributeString("sensor", "")
                escritor.WriteAttributeString("tiposensor", "")
                escritor.WriteAttributeString("estadosensor", "")
                escritor.WriteAttributeString("suceso", "")
                escritor.WriteAttributeString("usuario", "")
                escritor.WriteEndElement()
            End If
            escritor.WriteEndElement()
            escritor.Flush()
            escritor.Close()
        End Sub

    End Class

End Namespace
