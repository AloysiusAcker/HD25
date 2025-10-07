<%@ Page Language="VB" MasterPageFile="~/OperadorLogistico/PagPrincipal_OpLogistico.master" AutoEventWireup="false" CodeFile="Inventario_GuiaRemision_CargaArch_Admin.aspx.vb" Inherits="Inventario_GuiaRemision_CargaArch_Admin" title="Untitled Page" %>
<script runat="server" >
    
    Protected Sub btnGuardarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim strSaveFileAs As String
        Dim strStatusMessage As String = ""
        Dim posicion As Integer = 0
        Dim i As Integer = 0
        Dim NCant As String = 0
        Dim Variable As String = ""
        Dim NombreArchivo As String = ""
        Dim Mensaje As String = ""
        Dim objIns As New clsInv_InsUpdDel
        Dim CodPedido As Double : CodPedido = 0
        Dim CodArch As Double : CodArch = 0
        lblError.Text = ""
        if cbotipoarchivo.SelectedValue = "< Seleccionar >" then lblerror.Text ="Seleccionar tipo de archivo.":exit Sub 
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
        Else
            lblError.Text = "No hay Archivo que guardar" : Exit Sub
        End If
        CodPedido = CDbl(txtCodPedido.Text)
        Try
            If lblEt12.Text = "Ingresar Archivo" Then
                If Mensaje <> "" Then Mensaje = Mensaje & Chr(13)
                Mensaje = Mensaje & "        " & NombreArchivo
                strSaveFileAs = Server.MapPath("Temas/" & fuArchivo.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                fuArchivo.SaveAs(strSaveFileAs)
                objIns.Ins_PedArchivo(Session("Ruta_Emp"), Session("CodEmpresa"), CodPedido, cboTipoArchivo.SelectedValue.Trim, NombreArchivo, txtArchDescrip.Text.Trim)
            End If
            btnCancelarTA_Click(sender, e)
            Call btnListar_Click(sender, e)
        Catch Ex As Data.SqlClient.SqlException
            lblError.Text = "Unable to save the uploaded file.The error was: " & Ex.Message
            lblError.Visible = True
         Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <div style="text-align: left">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
                <tr>
                    <td align="left" style="width: 25px; height: 51px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 51px; text-align: center" valign="top">
                        <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                            font-size: 14pt; vertical-align: middle; width: 550px; color: gray; font-style: italic;
                            font-family: 'Bell MT', Broadway, Arial, Serif; position: static; height: 1px;
                            text-align: center">
                            Guía de Remisión - Carga de Archivo</div>
                    </td>
                    <td align="left" style="width: 58px; height: 51px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top">
                        </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 75px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 80px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 30px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 115px; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Entrega"
                            Width="70px"></asp:Label></td>
                    <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtFecha" runat="server" Font-Names="Arial" Font-Size="8pt" Width="105px"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" Font-Size="8pt" Width="95px"></asp:TextBox></td>
                    <td align="left" style="width: 150px; height: 22px; vertical-align: middle; text-align: left;" valign="top">
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Formato dd/mm/yyyy"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 22px; text-align: right"
                        valign="top">
                        <asp:Button ID="btnListar" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                            Text="Listar" Width="80px" /></td>
                    <td align="left" style="width: 58px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 22px" valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 75px; height: 22px; text-align: left"
                        valign="top">
                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° Serie"></asp:Label></td>
                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                        valign="top">
                        <asp:TextBox ID="txtBSerie" runat="server" Font-Names="Arial" Font-Size="8pt" Width="205px"></asp:TextBox></td>
                    <td align="left" style="vertical-align: middle; width: 150px; height: 22px; text-align: left"
                        valign="top">
                    </td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 22px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 22px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="5" style="height: 19px" valign="top">
                        <asp:Label ID="lblRegistro" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Maroon"></asp:Label></td>
                    <td align="left" style="vertical-align: middle; width: 115px; height: 19px; text-align: right"
                        valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px" valign="top">
                                <div id="DIV1" runat="server" style="border-right: dimgray 1px inset; border-top: dimgray 1px inset;
                                    overflow: auto; border-left: dimgray 1px inset; width: 550px; border-bottom: dimgray 1px inset;
                                    position: static; height: 300px">
                                    <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                                        Font-Size="8pt" Width="1120px" OnSelectedIndexChanged="Flex_SelectedIndexChanged" UseAccessibleHeader="False">
                                        <Columns>
                                            <asp:ButtonField ButtonType="Button" CommandName="Carga" Text="Ing. Archivo">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Button" CommandName="Archivo" Text="Archivo">
                                                <ControlStyle BackColor="LightGray" BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Width="70px" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" />
                                            </asp:ButtonField>
                                            <asp:BoundField DataField="ESTADO_ENTREGA" HeaderText="Est. Entrega">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" Wrap="True" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GUIREM_SERIE" HeaderText="Serie">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GUIREM_NUMERO" HeaderText="Nro. Gu&#237;a">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FECHA_ENTREGA" HeaderText="Fecha Entrega">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESTINO_CODIGO" HeaderText="Cod. Destinatario">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DESTINO_NOMBRE" HeaderText="Destinatario">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CURRIERRUC" HeaderText="RUC Courier">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CURRIERRAZON_SOCIAL" HeaderText="Raz&#243;n Social Courier">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GUIREM_ESTADO_ENTREGA">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GUIREM_CODIGO">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="0px" ForeColor="White" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PEDIDO_CODIGO">
                                                <ItemStyle ForeColor="White" Width="0px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PEDIDO_NRO" HeaderText="Nro. Pedido">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                    </td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 20px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="vertical-align: middle; height: 20px" valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 20px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px;" valign="top">
                    </td>
                    <td align="left" colspan="6" style="vertical-align: middle;" valign="top">
                        <div id="DIV2" runat="server" style="border-right: dimgray 1px outset; border-top: dimgray 1px outset;
                            overflow: auto; border-left: dimgray 1px outset; width: 550px; border-bottom: dimgray 1px outset;" visible="false">
                            <asp:GridView ID="FlexDet" runat="server" AutoGenerateColumns="False" Width="550px">
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" />
                                <Columns>
                                    <asp:BoundField DataField="ARCHIVO_CODIGO" HeaderText="Codigo">
                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                            Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TIPO" HeaderText="Tipo Archivo">
                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                            Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Nombre del Archivo">
                                        <ItemTemplate>
                                            <div id="Doc" runat="server" style="width: 200px; height: 22px">
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                            Width="200px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ARCHIVO_DESCRIPCION" HeaderText="Descripci&#243;n">
                                        <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle"
                                            Width="200px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                    <td align="left" style="width: 58px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px" valign="top">
                    </td>
                    <td align="left" colspan="6" style="height: 19px; vertical-align: middle;" valign="top">
                        <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                    <td align="left" style="width: 58px; height: 19px" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 173px;" valign="top">
                    </td>
                    <td align="left" colspan="6" valign="top" style="height: 173px">
                        <div style="text-align: left">
                            <table id="lblIngresarFecha" runat="server" border="0" cellpadding="0" cellspacing="0"
                                style="width: 550px" visible="true" enableviewstate="true">
                                <tr>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                        <asp:Label ID="lblEt12" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Enabled="False" Font-Bold="True">Ingresar Archivo</asp:Label></td>
                                    <td align="left" style="height: 22px; vertical-align: middle; text-align: right;" valign="top" colspan="3">
                        <asp:Button ID="btnGuardarTA" runat="server" BackColor="LightGray" BorderColor="Gray"
                            BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" Enabled="False"
                            EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnGuardarTA_Click"
                            onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                            Text="Guardar" Width="75px" /><asp:Button ID="btnCancelarTA" runat="server" BackColor="LightGray"
                                BorderColor="Gray" BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton"
                                Enabled="False" EnableTheming="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Gray"
                                OnClick="btnCancelarTA_Click" onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                                Text="Cancelar" Width="75px" />&nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cod. Guía" Enabled="False"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 155px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtCodGuia" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                            Width="145px"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Serie Guía" Enabled="False"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 75px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtSerieGuia" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ReadOnly="True" Width="67px"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 60px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nro. Guía"
                                            Width="48px" Enabled="False"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 100px; height: 21px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtNroGuia" runat="server" Font-Names="Arial" Font-Size="8pt" ReadOnly="True"
                                            Width="92px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Destinatario" Enabled="False"></asp:Label></td>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtDestinatario" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ReadOnly="True" Width="302px"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 60px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Código" Enabled="False"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 100px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtCodDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ReadOnly="True" Width="92px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado Entrega" Enabled="False"></asp:Label></td>
                                    <td align="left" style="vertical-align: middle; width: 155px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtEstado" runat="server" Font-Names="Arial" Font-Size="8pt" Width="145px" ReadOnly="True"></asp:TextBox></td>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lbl10" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Nro. Pedido"></asp:Label></td>
                                    <td align="left" colspan="3" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtNroPedido" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ReadOnly="True" Width="129px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt13" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Tipo de Archivo"></asp:Label></td>
                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:DropDownList ID="cboTipoArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Width="466px">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                                <asp:Label ID="lblEt9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cargar Imagen" Width="72px" Enabled="False"></asp:Label></td>
                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                                <asp:FileUpload ID="fuArchivo" runat="server" Font-Names="Arial" Font-Size="8pt" Width="464px" Enabled="False" /></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:Label ID="lblEt14" runat="server" Enabled="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Descripción"></asp:Label></td>
                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtArchDescrip" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="500" TextMode="MultiLine" Width="459px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left" style="vertical-align: middle; width: 80px; height: 22px; text-align: left"
                                        valign="top">
                                    </td>
                                    <td align="left" colspan="5" style="vertical-align: middle; height: 22px; text-align: left"
                                        valign="top">
                                        <asp:TextBox ID="txtCodPedido" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Visible="False" Width="73px"></asp:TextBox></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td align="left" style="width: 58px; height: 173px;" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 25px; height: 19px;" valign="top">
                    </td>
                    <td align="left" valign="top" colspan="2" style="vertical-align: middle; text-align: left; height: 19px;">
                        </td>
                    <td align="left" style="width: 30px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 100px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 150px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 115px; height: 19px;" valign="top">
                    </td>
                    <td align="left" style="width: 58px; height: 19px;" valign="top">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    &nbsp;
</asp:Content>

