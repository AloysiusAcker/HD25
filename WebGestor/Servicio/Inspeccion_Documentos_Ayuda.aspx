<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inspeccion_Documentos_Ayuda.aspx.vb" Inherits="Inspeccion_Documentos_Ayuda" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat = "Server" >
    Protected Sub btnGuardarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim psConexion As String = Session("Ruta_Emp")
        Dim psCodEmpresa As String = Session("CodEmpresa")
        Dim psGrpEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        Dim strSaveFileAs As String
        Dim strStatusMessage As String = ""
        Dim posicion As Integer = 0
        Dim i As Integer = 0
        Dim NCant As String = 0
        Dim Variable As String = ""
        Dim NombreArchivo As String = ""
        Dim Mensaje As String = ""
        Dim objIns As New ModuloGeneral
        Dim CodPedido As Double : CodPedido = 0
        Dim CodArch As Double : CodArch = 0
        Dim CodDocum As Double = 0
        Dim oficina As Double = 0
        Dim fechaIng As String = ""
        Dim inspec As Double = 0
        If txtNroInspeccion.Text <> "" Then
            inspec = (txtNroInspeccion.Text.Trim)
        End If
        lblError.Text = ""
        If cboTipoIngreso.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo de Ingreso." : Exit Sub
        If cboTipoArchivo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo de Archivo." : Exit Sub
        If cboCategoria.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Nivel de Acceso." : Exit Sub
        If txtFechaIngreso.Text.Trim <> "" Then fechaIng = txtFechaIngreso.Text.Trim Else lblError.Text = "Ingresar Fecha de Ingreso" : Exit Sub
        'If txtOficinaRuc.Text = "" Then lblError.Text = "Ingresar Oficina" : Exit Sub
        fechaIng = Right(txtFechaIngreso.Text.Trim, 4) & Mid(txtFechaIngreso.Text.Trim, 4, 2) & Left(txtFechaIngreso.Text.Trim, 2)
        If txtCodOficina.Text <> "" Then oficina = CDbl(txtCodOficina.Text.Trim)
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
            lblError.Text = "No hay Archivo que guardar" : Exit Sub
        End If
        'CodDocum = CDbl(CodDocum.Text)
        CodDocum = 0
        Try
            If lblEt12.Text = "Ingresar Archivo" Then
                If Mensaje <> "" Then Mensaje = Mensaje & Chr(13)
                Mensaje = Mensaje & "        " & NombreArchivo
                strSaveFileAs = Server.MapPath("Temas/" & fuArchivo.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                fuArchivo.SaveAs(strSaveFileAs)
                objIns.Ins_Documentos(psConexion, CodDocum, cboTipoIngreso.SelectedValue.Trim,
                cboTipoArchivo.SelectedValue.Trim, cboCategoria.SelectedValue.Trim, fechaIng,
                oficina, NombreArchivo, txtDocDescrip.Text.Trim, inspec, psCodEmpresa)
            End If
            Call LimpiarIngreso()
            btnCancelarTA_Click(sender, e)
            Call btnListar_Click(sender, e)
        Catch Ex As Data.SqlClient.SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & Ex.Message
            lblError.Visible = True
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    </script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">   
            function comprueba() {       
                                  return confirm("Confirme el postback");   
                                 }
    </script>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
        <tr>
            <td align="left" colspan="10" style="height: 50px; text-align: center" valign="top">
                <div id="Div2" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                    left: 253px; vertical-align: middle; width: 582px; color: gray; font-style: italic;
                    font-family: 'Bell MT', Broadway, Arial, Serif; top: 275px; height: 1px; text-align: center">
                    Documentos de Ayuda</div>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="10" style="background-image: url(../Fotos/linea.JPG); height: 11px"
                valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 80px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 100px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 31px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 75px; height: 15px" valign="middle">
                </td>
            <td align="left" style="width: 25px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 119px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 15px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 15px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 80px; height: 22px" valign="middle">
                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro Inspeccion" Width="72px"></asp:Label></td>
            <td align="left" style="width: 100px; height: 22px" valign="middle">
                <asp:TextBox ID="txtXInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="95px"></asp:TextBox></td>
            <td align="left" style="width: 31px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="5" style="height: 22px" valign="middle">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                    Text="Listar" Width="100px" />
                <asp:Button ID="btnNuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                    BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                    Text="Nuevo" Width="100px" /></td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 80px; height: 22px" valign="middle">
                <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"
                    Width="32px"></asp:Label></td>
            <td align="left" style="width: 100px; height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                <asp:TextBox ID="txtXRucOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="95px"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 31px; height: 22px" valign="middle">
                <asp:Button ID="btnBuscarXOficina" runat="server" BackColor="LightGray" UseSubmitBehavior = "false"
                        BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial"
                        Font-Size="8pt" Text="..." Width="25px" ForeColor="Gray" /></td>
            <td align="left" colspan="5" style="height: 22px" valign="middle">
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                <asp:TextBox ID="txtXDesOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Width="330px" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 80px; height: 22px" valign="middle">
                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Ingreso"
                    Width="70px"></asp:Label></td>
            <td align="left" style="width: 100px; height: 22px" valign="middle">
                <asp:TextBox ID="txtFechaIng" runat="server" Font-Names="Arial" Font-Size="8pt" Width="95px"></asp:TextBox></td>
            <td align="left" style="width: 31px; height: 22px" valign="middle">
                </td>
            <td align="left" style="width: 75px; height: 22px" valign="middle">
                <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="95px"></asp:TextBox></td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
                </td>
            <td align="left" style="width: 119px; height: 22px" valign="middle">
                <asp:TextBox ID="txtXCodOficina" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Visible="False" Width="8px"></asp:TextBox></td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="6" style="height: 22px" valign="middle">
                <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Maroon"></asp:Label></td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="8" style="height: 22px" valign="middle">
                <div id="DIV1" runat="server" style="border-right: darkgray 1px outset; border-top: darkgray 1px outset;
                    overflow: auto; border-left: darkgray 1px outset; width: 540px; border-bottom: darkgray 1px outset;
                    position: static; height: 300px">
                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                        Font-Size="8pt" Width="1270px">
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="F. Ingreso">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TEMA_AYUDA_DESCRIPCION" HeaderText="Descripcion">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Nombre del Documento">
                                <ItemTemplate>
                                    <div id="Doc" runat="server" style="width: 250px; height: 22px">
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="250px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="COD_OFICINA" HeaderText="Codigo Interno">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCRIPCION" HeaderText="Oficina">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="230px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIPOINGRESO" HeaderText="Tipo Ingreso">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIPODOCUMENTO" HeaderText="Tipo Documento">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INSPECCION" HeaderText="Nro Inspeccion" />
                            <asp:BoundField DataField="Categoria" HeaderText="Categoria">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TEMA_AYUDA_CODIGO">
                                <ItemStyle ForeColor="White" Width="0px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                </div>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="6" style="height: 22px" valign="middle">
                <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 60px; height: 22px" valign="middle">
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
            <td align="left" colspan="8" style="height: 22px" valign="middle">
                <table id="lblIngresarFecha" runat="server" border="0" cellpadding="0" cellspacing="0"
                    enableviewstate="true" style="width: 550px" visible="true">
                    <tr>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                            <asp:Label ID="lblEt12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Maroon">Ingresar Archivo</asp:Label></td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: right; width: 225px;"
                            valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="lblEt1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cod. Doc"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 155px; height: 21px; text-align: left"
                            valign="top">
                            <asp:TextBox ID="txtCodDoc" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                Width="145px"></asp:TextBox></td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Inspeccion"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 225px; height: 21px; text-align: left"
                            valign="top" colspan="3">
                            <asp:TextBox ID="txtNroInspeccion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="213px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo Ingreso"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 155px; height: 21px; text-align: left"
                            valign="top">
                            <asp:DropDownList ID="cboTipoIngreso" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="150px">
                            </asp:DropDownList></td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="lblEt13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Archivo"></asp:Label></td>
                        <td align="left" colspan="3" style="vertical-align: middle; height: 21px; text-align: left; width: 225px;"
                            valign="top">
                            <asp:DropDownList ID="cboTipoArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="220px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fec. Ingreso" Width="64px"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 155px; height: 21px; text-align: left"
                            valign="top">
                            <div style="text-align: left">
                                <asp:TextBox ID="txtFechaIngreso" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="144px"></asp:TextBox>&nbsp;</div>
                        </td>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Categoria"></asp:Label></td>
                        <td align="left" colspan="3" style="vertical-align: middle; width: 225px; height: 21px;
                            text-align: left" valign="top">
                            <asp:DropDownList ID="cboCategoria" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="220px">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Oficina"></asp:Label></td>
                        <td align="left" style="vertical-align: middle; width: 155px; height: 21px; text-align: left"
                            valign="top">
                            <div style="text-align: left">
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 155px">
                                    <tr>
                                        <td align="left" style="width: 125px" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
<asp:TextBox id="txtOficinaRuc" runat="server" Width="115px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox> 
</ContentTemplate>
                                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td align="left" style="width: 30px" valign="top">
                            <asp:Button ID="btnBuscar" runat="server" BackColor="LightGray" BorderColor="Gray" 
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Gray" Height="20px" Text="..." Width="25px" /></td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td align="left" colspan="4" style="vertical-align: middle; height: 21px; text-align: left"
                            valign="top">
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
<asp:TextBox id="txtOficinaDescripcion" runat="server" Width="295px" Font-Size="8pt" Font-Names="Arial" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                                <Triggers>
<asp:AsyncPostBackTrigger ControlID="FlexUbicacion" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                            valign="top">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Documento"></asp:Label></td>
                        <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                            valign="top">
                            <asp:FileUpload ID="fuArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="460px" /></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                            valign="top">
                            <asp:Label ID="lblEt14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label></td>
                        <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                            valign="top">
                            <asp:TextBox ID="txtDocDescrip" runat="server" Font-Names="Arial" Font-Size="8pt"
                                MaxLength="500" TextMode="MultiLine" Width="455px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="left" style="vertical-align: middle; width: 80px; height: 25px; text-align: left"
                            valign="top">
                            <asp:TextBox ID="txtCodOficina" runat="server" Font-Names="Arial" Font-Size="8pt" Width="8px" Visible="False"></asp:TextBox></td>
                        <td align="left" colspan="5" style="vertical-align: middle; height: 25px; text-align: right"
                            valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 470px">
                                <tr>
                                    <td align="left" style="width: 310px; height: 22px" valign="top">
                                    </td>
                                    <td align="left" style="width: 80px; height: 22px" valign="top">
                            <asp:Button ID="btnGuardarTA" runat="server" BackColor="LightGray" BorderColor="Gray"
                                BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                                Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnGuardarTA_Click" onmouseout="this.style.fontWeight='normal'"
                                onmouseover="this.style.fontWeight='bolder'" Text="Guardar" Width="75px" /></td>
                                    <td align="left" style="width: 80px; height: 22px" valign="top">
                                        <asp:Button
                                    ID="btnCancelarTA" runat="server" BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset"
                                    BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Gray" onmouseout="this.style.fontWeight='normal'"
                                    onmouseover="this.style.fontWeight='bolder'" Text="Cancelar" Width="72px" /></td>
                                </tr>
                            </table>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" style="width: 25px; height: 22px" valign="middle">
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
<DIV style="LEFT: 300px; WIDTH: 100px; POSITION: absolute; TOP: 400px; HEIGHT: 100px" id="lblBusCentroCosto" runat="server" visible="false"><TABLE style="BORDER-RIGHT: gray 2px outset; BORDER-TOP: gray 2px outset; BORDER-LEFT: gray 2px outset; WIDTH: 500px; BORDER-BOTTOM: gray 2px outset; BACKGROUND-COLOR: darkgray" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; HEIGHT: 30px; TEXT-ALIGN: center" vAlign=middle align=left colSpan=3><asp:Label id="Label1" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Busqueda de Centro de Costos"></asp:Label></TD><TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Text="Código"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusCod" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiCerrar" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Cerrar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Silver" BackColor="LightGray"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 70px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:Label id="Label12" runat="server" Width="60px" Font-Size="8pt" Font-Names="Arial" Text="Descripción"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 280px; HEIGHT: 22px; TEXT-ALIGN: left" vAlign=middle align=left><asp:TextBox id="txtBusDescripcion" runat="server" Width="270px" Font-Size="8pt" Font-Names="Arial"></asp:TextBox></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 100px; HEIGHT: 22px; TEXT-ALIGN: right" vAlign=middle align=left><asp:Button id="btnUbiListar" runat="server" Width="80px" ForeColor="Gray" Font-Size="8pt" Font-Names="Arial" Text="Listar" BorderWidth="1px" BorderStyle="Outset" BorderColor="Gray" BackColor="LightGray"></asp:Button></TD><TD style="WIDTH: 25px; HEIGHT: 22px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px" vAlign=middle align=left></TD><TD vAlign=middle align=left colSpan=3><asp:UpdatePanel id="UpdatePanel8" runat="server"><ContentTemplate>
<DIV style="BORDER-RIGHT: gray 1px outset; BORDER-TOP: gray 1px outset; OVERFLOW: auto; BORDER-LEFT: gray 1px outset; WIDTH: 444px; BORDER-BOTTOM: gray 1px outset; HEIGHT: 250px" id="lblBusCentroCosto2" runat="server"><asp:GridView id="FlexUbicacion" runat="server" Width="450px" Font-Size="8pt" Font-Names="Arial" AutoGenerateColumns="False" Font-Overline="False">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Aceptar" Text="&lt;&lt;">
                                    <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                        Font-Names="Arial" Font-Size="8pt" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="CODINTERNO" HeaderText="C&#243;digo">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CODIGO">
                                    <ItemStyle ForeColor="DarkGray" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView> </DIV>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID="btnUbiListar" EventName="Command"></asp:AsyncPostBackTrigger>
</Triggers>
</asp:UpdatePanel> </TD><TD style="WIDTH: 25px" vAlign=middle align=left></TD></TR><TR><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 70px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 280px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 100px; HEIGHT: 19px" vAlign=middle align=left></TD><TD style="WIDTH: 25px; HEIGHT: 19px" vAlign=middle align=left></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnBuscarXOficina" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaIng"
        TargetControlID="txtFechaIng">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaFin"
        TargetControlID="txtFechaFin">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFechaIngreso"
        TargetControlID="txtFechaIngreso">
    </cc1:CalendarExtender>
</asp:Content>

