<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Monitoreo.aspx.vb" Inherits="MonitoreoWeb.Monitoreo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Monitoreo de Sensores</title>
    <link href="estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmMonitoreo" runat="server"><br />
<div id="container" style="width: 752px; height: 848px">
<div id="core_header" style="height: 152px">
     <div id="header_text">
         MONITOREO</div></div>
	<div id="core_container">
	<div id="core_container2">
		<div id="core_left">
		     <div id="navcontainer">
                    <ul style="left: 0px; top: 0px; height: 1px">
                    <li><a href="../Default.aspx" title="Hogar">Hogar</a></li>
                    <li><a href="../AcercaDe.aspx" title="Acercade">Acerca De...</a></li>
                    </ul>
		     </div>
		</div>
        <div id="core_right">
          <div class="content-box" style="text-align: center; height: 224px;">
                  <h3 style="font-weight: bold; text-align: left">
                      Estado de los Sensores...</h3>
              <center>
                  <asp:SqlDataSource ID="sqldata2" runat="server" ConnectionString="<%$ ConnectionStrings:dbproyectoConnectionString %>"
                      SelectCommand="SELECT * FROM [VistaEstadoSensores]"></asp:SqlDataSource>
                  <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                      DataKeyNames="IDSensor" DataSourceID="sqldata2">
                      <Columns>
                          <asp:BoundField DataField="IDSensor" HeaderText="IDSensor" ReadOnly="True" SortExpression="IDSensor" />
                          <asp:BoundField DataField="TipoDeSensor" HeaderText="TipoDeSensor" SortExpression="TipoDeSensor" />
                          <asp:BoundField DataField="EstadoDelSensor" HeaderText="EstadoDelSensor" SortExpression="EstadoDelSensor" />
                      </Columns>
                  </asp:GridView>
                  &nbsp;</center>
              &nbsp; &nbsp;&nbsp;
          </div>
            <div class="content-box" style="text-align: center; height: 168px;">
                <h3 style="font-weight: bold; text-align: left">
                    Log del Sistema...</h3>
                <center>
                    <asp:SqlDataSource ID="sqldata" runat="server" ConnectionString="<%$ ConnectionStrings:dbproyectoConnectionString %>"
                        SelectCommand="SELECT * FROM [VistaLog]"></asp:SqlDataSource>
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        DataSourceID="sqldata" Height="264px" Width="448px">
                        <Columns>
                            <asp:BoundField DataField="pk_id" HeaderText="pk_id" SortExpression="pk_id" />
                            <asp:BoundField DataField="TipoDeSensor" HeaderText="TipoDeSensor" SortExpression="TipoDeSensor" />
                            <asp:BoundField DataField="EstadoDeSensor" HeaderText="EstadoDeSensor" SortExpression="EstadoDeSensor" />
                            <asp:BoundField DataField="suceso" HeaderText="suceso" SortExpression="suceso" />
                            <asp:BoundField DataField="fecha_hora" HeaderText="fecha_hora" SortExpression="fecha_hora" />
                            <asp:BoundField DataField="NombreDelUsuario" HeaderText="NombreDelUsuario" SortExpression="NombreDelUsuario" />
                        </Columns>
                    </asp:GridView>
                    &nbsp;</center>
                &nbsp; &nbsp;&nbsp;
              </div>   
        <!-- El contenido va aqui... -->
          <div class="content-box">
              Historial de sensores activados desde inicio de la instalacion del sistema de seguridad.</div>
        <!-- Final de el contenido de la pagina -->
        </div>
        </div>
        <div id="footer2"><div id="footer_line"></div>    
             <div id='footer_text'>Copyleft &copy; ...</div>
            </div>
        </div>
</div>
<div id="footer"></div>
</form>
</body>
</html>