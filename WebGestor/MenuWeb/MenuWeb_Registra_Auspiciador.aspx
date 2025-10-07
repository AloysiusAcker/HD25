<%@ Page Language="VB" MasterPageFile="~/MenuWeb/PagPrincipal_MenuWeb.master" AutoEventWireup="false" CodeFile="MenuWeb_Registra_Auspiciador.aspx.vb" Inherits="MenuWeb_MenuWeb_Registra_Auspiciador" title="Untitled Page" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<script runat = "Server" >
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Session("Ingreso") = "Si" Then
            Dim obj As New clsMenuWeb_Consultas
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
            Dim pdCodGrupo As Double = 0
            Dim pdCodAuspiciador As Double = 0
            Dim fso = CreateObject("scripting.filesystemobject")
            Dim psFileName As String = Server.HtmlEncode(fuImagen.FileName)
            Dim psExtension As String = ""
            lblError.Text = ""
            Try
                If txtNombre.Text.Trim = "" Then lblError.Text = "<br> - Ingresar nombre del auspiciador"
                If txtDescripcion.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar la descripción del auspiciador."
                If txtPagina.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar la pagina del auspiciador."
                If (fuImagen.HasFile) Then
                Else
                    lblError.Text = lblError.Text & "<br> - No hay Archivo que guardar"
                End If
                psFileName = System.IO.Path.GetExtension(psFileName)
                psExtension = psFileName
                If psExtension = ".jpg" Or psExtension = ".JPG" Or psExtension = ".gif" Or psExtension = ".GIF" Then
                Else
                    lblError.Text = lblError.Text & "<br> - Debe ser una imagen."
                End If
                If lblError.Text <> "" Then
                    lblError.Text = " Existen las sgtes. observaciones: <br>" & lblError.Text
                    Exit Sub
                End If
                For i = 1 To Len(fuImagen.PostedFile.FileName)
                    If Mid(fuImagen.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                Next
                Variable = fuImagen.PostedFile.FileName
                For i = 1 To NCant
                    posicion = InStr(Variable, "\")
                    Variable = Mid(Variable, posicion + 1)
                    If i = NCant Then NombreArchivo = Variable
                Next
                NombreArchivo = Variable
                'verificar si existe la carpeta donde se alojaran las imagenes de cada auspiiador
                If Not (fso.folderexists(Server.MapPath("ImagesAusp_" & Session("SiglaGrupoEmpresa")))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("ImagesAusp_" & Session("SiglaGrupoEmpresa")))
                End If
                'guardar imagen
                strSaveFileAs = Server.MapPath("ImagesAusp_" & Session("SiglaGrupoEmpresa") & "/" & fuImagen.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                fuImagen.SaveAs(strSaveFileAs)
                'guardar datos del auspiciador en la base de datos
                pdCodGrupo = cboGrupo.SelectedValue.Trim
                obj.Ins_Auspiciador(pdCodGrupo, cboEmpresa.SelectedValue.Trim, txtNombre.Text.Trim, txtDescripcion.Text.Trim, txtPagina.Text.Trim, NombreArchivo, psExtension)
                btnListar_Click(sender, e)
                btnCancelar_Click(sender, e)
                Session("Ingreso") = "No"
            Catch ex As Data.SqlClient.SqlException
                lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
            Catch ex As Exception
                lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
            End Try
        End If
    End Sub
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="3" style="height: 50px; text-align: center;" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Auspiciadores</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5" style="height: 11px; background-image: url(../Fotos/Linea_Gris.bmp);" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 410px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" Width="544px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Grupo"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboGrupo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="408px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnListar" runat="server" CssClass="EstiloBoton_Ac" Text="Listar" Width="76px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                    <asp:DropDownList ID="cboEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="408px">
                    </asp:DropDownList>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboGrupo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="EstiloBoton_Ac" Text="Nuevo" Width="76px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="2">
                    <asp:Label ID="lblRegistro" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Width="464px"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <contenttemplate>
                    <div style="border-right: gray 1px outset; border-top: gray 1px outset; overflow: auto;
                        border-left: gray 1px outset; width: 544px; border-bottom: gray 1px outset; height: 176px">
                        <asp:GridView ID="Flex" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="8pt" Height="48px" 
                            Width="550px">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Eliminar" Text="Eliminar">
                                    <ControlStyle CssClass="EstiloBoton_Ac" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="AUSPI_NOMBRE" HeaderText="Nombre">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AUSPI_DESCRIP" HeaderText="Descripci&#243;n">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AUSPI_LINK" HeaderText="Link">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px" />
                                </asp:BoundField>
                                <asp:ImageField DataImageUrlField="AUSPI_IMAGEN_NOMBRE" DataImageUrlFormatString="~/MenuWeb/ImagesAusp_1TE/{0}"
                                    HeaderText="Imagen">
                                    <ControlStyle Height="50px" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                </asp:ImageField>
                                <asp:BoundField DataField="AUSPI_CODIGO">
                                    <ItemStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AUSPI_IMAGEN_NOMBRE">
                                    <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </div></contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="btnListar" EventName="Click"></asp:AsyncPostBackTrigger>
<asp:AsyncPostBackTrigger ControlID="Flex" EventName="RowCommand"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; height: 22px" valign="top" colspan="3">
                    <div style="text-align: left">
                        <table id="lblIngreso" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 550px" visible="false">
                            <tr>
                                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:Label ID="lblIngEtiqueta" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Maroon" Text="Nuevo Auspiciador"></asp:Label></td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Nombre"></asp:Label></td>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="200" Width="470px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: top; width: 70px; height: 44px" valign="top">
                                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Decsripción"></asp:Label></td>
                                <td align="left" colspan="3" style="vertical-align: top; height: 44px" valign="top">
                                    <asp:TextBox ID="txtDescripcion" runat="server" Font-Names="Arial" Font-Size="8pt" Height="40px" MaxLength="500" TextMode="MultiLine" Width="470px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Página Web"></asp:Label></td>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:TextBox ID="txtPagina" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="200" Width="470px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Imagen"></asp:Label></td>
                                <td align="left" colspan="3" style="vertical-align: middle; height: 22px" valign="top">
                                    <asp:FileUpload ID="fuImagen" runat="server" Font-Names="Arial" Font-Size="8pt" Width="476px" /></td>
                            </tr>
                            <tr>
                                <td align="left" style="vertical-align: middle; width: 70px; height: 22px" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 320px; height: 22px" valign="top">
                                </td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Button ID="btnGuardar" runat="server" CssClass="EstiloBoton_Ac" Text="Guardar" OnClick="btnGuardar_Click"
                                        Width="76px" /></td>
                                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                                    <asp:Button ID="btnCancelar" runat="server" CssClass="EstiloBoton_Ac" Text="Cancelar"
                                        Width="76px" /></td>
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
                <td align="left" style="vertical-align: middle; width: 60px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 410px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

