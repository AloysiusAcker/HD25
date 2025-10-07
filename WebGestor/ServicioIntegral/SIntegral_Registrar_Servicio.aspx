<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="SIntegral_Registrar_Servicio.aspx.vb" Inherits="ServicioIntegral_SIntegral_Registrar_Servicio" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<script runat = "Server" >
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New clsSIntegral
        Dim psCodServ As Double = 0
        Dim pdSector As Double = 0
        Dim pdTipo As Double = 0
        Dim pdTipo2 As Double = 0
        Dim strSaveFileAs As String
        Dim strStatusMessage As String = ""
        Dim posicion As Integer = 0
        Dim NCant As String = 0
        Dim Variable As String = ""
        Dim NombreArchivo As String = ""
        Dim Mensaje As String = ""
        Dim i As Integer = 0
        Dim CodProveedor As Double = 0
        Dim psPais As String = ""
        Dim psDpto As String = ""
        Dim psProv As String = ""
        Dim psDist As String = ""
        Dim psFecInicia As String = ""
        Dim psFecTermina As String = ""
        Dim pdPrecio As Double = 0
        Dim psObs As String = ""
        Try
            If txtProveedor.Text.Trim = "" And txtCodProveedor.Text = "" Then lblError.Text = "<br> - Ingresar Proveedor"
            If cboSector.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Sector Económico"
            If cboTipo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Tipo Servicio"
            If txtDescripcion.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar la Descripción del Servicio."
            If (fuArchivo.HasFile) Then
                Dim FileName As String = Server.HtmlEncode(fuArchivo.FileName)
                Dim Extensión As String = ""
                FileName = System.IO.Path.GetExtension(FileName)
                Extensión = FileName
                For i = 1 To Len(fuArchivo.PostedFile.FileName)
                    If Mid(fuArchivo.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                Next
                Variable = UCase(fuArchivo.PostedFile.FileName)
                For i = 1 To NCant
                    posicion = InStr(Variable, "\")
                    Variable = Mid(Variable, posicion + 1)
                    If i = NCant Then NombreArchivo = Variable
                Next
                NombreArchivo = Variable
            Else
                lblError.Text = lblError.Text & "<br> - No hay Archivo que guardar"
            End If
            If txtDireccion.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar Dirección"
            If txtPrecio.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar Precio del Servicio."
            If Not IsNumeric(txtPrecio.Text) Then lblError.Text = lblError.Text & "<br> - El precio debe ser numerico."
            If lblError.Text <> "" Then
                lblError.Text = " Existen las sgtes. observaciones: <br>" & lblError.Text
                Exit Sub
            End If
            psFecInicia = Right(txtFecInicia.Text.Trim, 4) & Mid(txtFecInicia.Text.Trim, 4, 2) & Left(txtFecInicia.Text.Trim, 2)
            psFecTermina = Right(txtFecTermina.Text.Trim, 4) & Mid(txtFecTermina.Text.Trim, 4, 2) & Left(txtFecTermina.Text.Trim, 2)
            If cboSector.SelectedValue <> "< Seleccionar >" Then pdSector = cboSector.SelectedValue.Trim
            If cboTipo.SelectedValue <> "< Seleccionar >" Then pdTipo = cboTipo.SelectedValue.Trim
            If cboTipo2.SelectedValue <> "< Seleccionar >" Then pdTipo2 = cboTipo2.SelectedValue.Trim
            If txtCodProveedor.Text.Trim <> "" Then CodProveedor = txtCodProveedor.Text.Trim
            If cboPais.SelectedValue <> "< Seleccionar >" And cboPais.Text <> "" And cboPais.SelectedValue <> "< Seleccionar >" Then psPais = cboPais.SelectedValue.Trim
            If cboDpto.SelectedValue <> "< Seleccionar >" And cboDpto.Text <> "" And cboDpto.SelectedValue <> "< Seleccionar >" Then psDpto = cboDpto.SelectedValue.Trim
            If cboProv.SelectedValue <> "< Seleccionar >" And cboProv.Text <> "" Then psProv = cboProv.SelectedValue
            If cboDist.SelectedValue <> "< Seleccionar >" And cboDist.Text <> "" Then psDist = cboDist.SelectedValue
            psObs = txtObservacion.Text.Trim
            pdPrecio = txtPrecio.Text.Trim
            'guardar archivo y datos
            If Mensaje <> "" Then Mensaje = Mensaje & Chr(13)
            Mensaje = Mensaje & "        " & NombreArchivo
            strSaveFileAs = Server.MapPath("Imagenes/" & fuArchivo.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
            fuArchivo.SaveAs(strSaveFileAs)
            obj.Ins_ServDetalle(Session("Ruta_Emp"), Session("CodEmpresa"), pdSector, pdTipo, pdTipo2, CodProveedor, txtDescripcion.Text.Trim, pdPrecio, txtDireccion.Text.Trim, psPais, psDpto, psProv, psDist, psObs, NombreArchivo, psFecInicia, psFecTermina, HttpContext.Current.User.Identity.Name)
            btnCancelar_Click(sender, e)
        Catch ex As Data.SqlClient.SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="6" style="height: 50px; text-align: center" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 15pt; vertical-align: middle; width: 536px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        height: 18px; text-align: center">
                        Registrar Servicios</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="8" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 190px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px;" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 30px;" valign="top">
                </td>
                <td align="left" style="width: 25px;" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="6" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 550px" id="lblServicio" runat="server" visible="true">
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Proveedor"></asp:Label></td>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <contenttemplate>
                                    <asp:TextBox ID="txtProveedor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="420px"></asp:TextBox>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                                    <asp:Button ID="btnBuscar" runat="server" CssClass="EstiloBoton_Ac" Text="..." Width="25px" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Sector Economico"
                                        Width="88px"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:DropDownList ID="cboSector" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="456px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo"
                                        Width="1px"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <contenttemplate>
                                    <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="456px">
                                    </asp:DropDownList>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboSector" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo 2"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <contenttemplate>
                                    <asp:DropDownList ID="cboTipo2" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="456px">
                                    </asp:DropDownList>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboTipo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: text-top; width: 90px; height: 50px" valign="top">
                                    <asp:Label ID="lblEtq5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: text-top; height: 50px" valign="top">
                                    <asp:TextBox ID="txtDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Height="40px" MaxLength="1000" TextMode="MultiLine" Width="450px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: text-top; width: 90px; height: 50px" valign="top">
                                    <asp:Label ID="lblEtq17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Observación"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: text-top; height: 50px" valign="top">
                                    <asp:TextBox ID="txtObservacion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Height="40px" MaxLength="1000" TextMode="MultiLine" Width="450px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: text-top; width: 90px; height: 45px" valign="top">
                                    <asp:Label ID="lblEtq7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Dirección"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: text-top; height: 45px" valign="top">
                                    <asp:TextBox ID="txtDireccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        MaxLength="200" TextMode="MultiLine" Width="450px" Height="32px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="País"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                    <asp:DropDownList ID="cboPais" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="186px">
                                    </asp:DropDownList>&nbsp;
                                </td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Departamento"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <contenttemplate>
                                    <asp:DropDownList ID="cboDpto" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="186px">
                                    </asp:DropDownList>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboPais" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                    </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Provincia"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <contenttemplate>
                                    <asp:DropDownList ID="cboProv" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="186px">
                                    </asp:DropDownList>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboDpto" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Distrito"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <contenttemplate>
                                    <asp:DropDownList ID="cboDist" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="186px">
                                    </asp:DropDownList>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboProv" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cargar Imagen"></asp:Label></td>
                                <td align="left" colspan="4" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:FileUpload ID="fuArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="455px" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Comienza"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                    <asp:TextBox ID="txtFecInicia" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="180px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Fin"></asp:Label></td>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:TextBox ID="txtFecTermina" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="180px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                                    <asp:Label ID="lblEtq14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Precio"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                                    <asp:TextBox ID="txtPrecio" runat="server" Font-Names="Arial" Font-Size="8pt" Width="180px"></asp:TextBox></td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <contenttemplate>
<asp:TextBox id="txtCodProveedor" runat="server" Width="40px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox>
</contenttemplate>
                                        <triggers>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                                    </asp:UpdatePanel>
                                </td>
                                <td align="left" style="vertical-align: middle; width: 160px; height: 22px" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 30px; height: 22px" valign="top">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 90px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 190px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: right;" valign="top">
                    <asp:Button ID="btnCancelar" runat="server" CssClass="EstiloBoton_Ac" Font-Names="Arial"
                        Font-Size="8pt" Text="Cancelar" Width="86px" /></td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="2">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="EstiloBoton_Ac" OnClick="btnGuardar_Click" Text="Guardar"
                        Width="86px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
    <div style="text-align: left">
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<TABLE style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; LEFT: 300px; BORDER-LEFT: gray 1px outset; WIDTH: 500px; BORDER-BOTTOM: gray 1px outset; POSITION: absolute; TOP: 400px; BACKGROUND-COLOR: darkgray" id="lblProveedor" cellSpacing=0 cellPadding=0 border=0 runat="server" visible="false"><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 25px; TEXT-ALIGN: center" vAlign=top align=left><asp:Label id="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Lista de Personas" ForeColor="Maroon"></asp:Label></TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Tipo Persona"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left><asp:DropDownList id="cboTipoPer" runat="server" Width="298px" Font-Size="8pt" Font-Names="Arial">
                    </asp:DropDownList></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnListar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Listar"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Razón Social"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 300px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtBusApePat" runat="server" Width="290px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 80px; HEIGHT: 22px" vAlign=top align=left><asp:Button id="btnCerrar" runat="server" CssClass="EstiloBoton_Ac" Width="72px" Text="Cerrar"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 22px" vAlign=top align=left colSpan=3><DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 440px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 240px"><asp:GridView id="Flex" runat="server" Width="530px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="&lt;&lt;">
                                    <ControlStyle CssClass="EstiloBoton_Ac" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="tipoper" HeaderText="Tipo Persona">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Raz&#243;n Social">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="400px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CODIGO">
                                    <ItemStyle ForeColor="DarkGray" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView> </DIV></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=top align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 70px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 300px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 80px; HEIGHT: 25px" vAlign=top align=left></TD><TD style="WIDTH: 25px; HEIGHT: 25px" vAlign=top align=left></TD></TR></TBODY></TABLE>
</contenttemplate>
            <triggers>
<asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click"></asp:AsyncPostBackTrigger>
</triggers>
        </asp:UpdatePanel><cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
            PopupButtonID="txtFecInicia" TargetControlID="txtFecInicia">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFecTermina"
            TargetControlID="txtFecTermina">
        </cc1:CalendarExtender>
    </div>
</asp:Content>

