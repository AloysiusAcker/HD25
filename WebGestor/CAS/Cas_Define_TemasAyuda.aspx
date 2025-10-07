<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Cas_Define_TemasAyuda.aspx.vb" Inherits="Cas_Define_TemasAyuda" title="CAS" %>
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
        Dim objCas As New ModuloCas
        Dim dt As New Data.DataTable
        Dim CodTemaAyuda As Double = 0
        lblErrorTA.Text = ""
        If cboClasif.SelectedValue = "< Seleccionar >" Then lblErrorTA.Text = "Es necesario seleccionar la Clasificación del Tema" : Exit Sub
        If cboTipo.SelectedValue = "< Seleccionar >" Then lblErrorTA.Text = "Es necesario seleccionar el Tipo de Archivo" : Exit Sub
        'archivo
        'strSaveFileAs = Server.MapPath("uploads/" & Upload.FileName)
        If (Upload.HasFile) Then
            Dim FileName As String = Server.HtmlEncode(Upload.FileName)
            Dim Extensión As String = ""
            FileName = System.IO.Path.GetExtension(FileName)
            Extensión = FileName
            For i = 1 To Len(Upload.PostedFile.FileName)
                If Mid(Upload.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
            Next
            Variable = UCase(Upload.PostedFile.FileName)
            For i = 1 To NCant
                posicion = InStr(Variable, "\")
                Variable = Mid(Variable, posicion + 1)
                If i = NCant Then NombreArchivo = Variable
            Next
            If NombreArchivo = "" Then NombreArchivo = Upload.PostedFile.FileName
        Else
            lblErrorTA.Text = "No hay Archivo que guardar" : Exit Sub
        End If
        'Upload.SaveAs("\\DATA\\Archivos\" + NombreArchivo)
        Try
            If lblEtiqueta.Text = "Ingresar Tema de Ayuda" Then
                dt = objCas.CasConsulta_ExisteTemaAyuda(UCase(NombreArchivo), Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then
                    If Mensaje <> "" Then Mensaje = Mensaje & Chr(13)
                    Mensaje = Mensaje & "        " & NombreArchivo
                    If MsgBox("Se ha encontrado archivos con nombres similares :" & Chr(13) & Chr(13) & Mensaje & Chr(13) & Chr(13) & "¿De todas maneras desea guardar?", vbQuestion + vbYesNo, "Temas de Ayuda") = vbYes Then
                        strSaveFileAs = Server.MapPath("Temas/" & Upload.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                        Upload.SaveAs(strSaveFileAs)
                        objCas.InsUpd_TemaAyuda(CodTemaAyuda, cboClasif.SelectedValue.Trim, cboTipo.SelectedValue.Trim, NombreArchivo, txtTADescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"))
                    Else
                        Exit Sub
                    End If
                Else
                    strSaveFileAs = Server.MapPath("Temas/" & Upload.FileName)
                    Upload.SaveAs(strSaveFileAs)
                    objCas.InsUpd_TemaAyuda(CodTemaAyuda, cboClasif.SelectedValue.Trim, cboTipo.SelectedValue.Trim, NombreArchivo, txtTADescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"))
                End If
                dt = Nothing
            ElseIf lblEtiqueta.Text = "Editar Tema de Ayuda" Then
                objCas.InsUpd_TemaAyuda(CodTemaAyuda, cboClasif.SelectedValue.Trim, cboTipo.SelectedValue.Trim, NombreArchivo, txtTADescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "2", Session("Ruta_Emp"))
            End If
            btnCancelarTA_Click(sender, e)
            Call cmdListarTA_Click(sender, e)
        Catch Ex As Data.SqlClient.SqlException
            lblErrorTA.Text = "Unable to save the uploaded file.The error was: " & Ex.Message
            lblErrorTA.Visible = True
            'lblErrorTA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTA.Visible = True
            lblErrorTA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 600px">
            <tr>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
                <td align="left" colspan="5" style="height: 50px; text-align: center;" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitleMenu" style="font-weight: bold;
                        font-size: 14pt; left: 225px; vertical-align: middle; width: 241px; color: seagreen;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; top: 284px;
                        height: 1px; text-align: center">
                        Define Temas de Ayuda</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="7" style="height: 11px" valign="top">
                    <img src="../Fotos/lineaCas.JPG" /></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 12px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 12px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Button ID="btnTANuevo" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" Height="19px" OnClick="btnTANuevo_Click"
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Nuevo" Width="80px" /></td>
                <td align="left" style="vertical-align: middle; width: 80px; color: yellow; height: 22px"
                    valign="top">
                    <asp:Button ID="cmdListarTA" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="cmdListarTA_Click"
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Listar" Width="80px" /></td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: bottom; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<DIV style="BORDER-RIGHT: darkgray 1px outset; BORDER-TOP: darkgray 1px outset; OVERFLOW: auto; BORDER-LEFT: darkgray 1px outset; WIDTH: 550px; BORDER-BOTTOM: darkgray 1px outset; POSITION: static; HEIGHT: 284px"><asp:GridView id="FlexTA" runat="server" Width="930px" Font-Size="8pt" Font-Names="Arial" PageSize="8" AutoGenerateColumns="False" AllowPaging="True"><Columns>
<asp:ButtonField Text="Quitar" ButtonType="Button" CommandName="Quitar">
<ControlStyle BackColor="LightGray" BorderStyle="Outset" Width="38px" ForeColor="Gray" BorderWidth="1px" BorderColor="Gray" Font-Size="8pt" Font-Names="Arial"></ControlStyle>

<ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:ButtonField>
<asp:BoundField DataField="CLASSE" HeaderText="Clasificaci&#243;n">
<ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
<ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Nombre del Documento"><ItemTemplate>
<div id="Doc" runat="server" style="width: 150px; height: 22px">
</div>                                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="TEMA_DESCRIPCION" HeaderText="Descripci&#243;n">
<ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Persona" HeaderText="Nombre de Creaci&#243;n ">
<ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="FECHA_INGRESO" HeaderText="F. Ingreso">
<ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_CODIGO">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_TIPO_DOC">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_USUARIO">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TEMA_CLASIFICACION">
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
<asp:BoundField>
<ItemStyle Width="0px" ForeColor="White"></ItemStyle>
</asp:BoundField>
</Columns>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
</asp:GridView> </DIV>
</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmdListarTA" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="FlexTA" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblErrorTA" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="5" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblEtiqueta" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Maroon"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblTA1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Tipo de Archivo"></asp:Label></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px"
                    valign="top">
                    <asp:DropDownList ID="cboTipo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="474px" Enabled="False">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblTA" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Clasificación"></asp:Label></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px"
                    valign="top">
                    <asp:DropDownList ID="cboClasif" runat="server" Font-Names="Arial" Font-Size="8pt"
                         Width="474px" Enabled="False">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lbl7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Archivo"></asp:Label></td>
                <td align="left" colspan="4" style="vertical-align: middle; height: 22px"
                    valign="top">
                    <asp:FileUpload ID="Upload" runat="server" Font-Names="Arial" Font-Size="8pt" Width="475px" Enabled="False" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top" >
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top"></td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                    <asp:Label ID="lblTA5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Descripción"></asp:Label>
                </td>
                <td align="left" colspan="4" style="vertical-align: middle;  height: 22px"
                    valign="top">
                    <asp:TextBox ID="txtTADescripcion" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="468px" Enabled="False"></asp:TextBox>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; color: yellow; height: 22px"
                    valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: right"
                    valign="top">
                    <asp:Button ID="btnGuardarTA" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" 
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Guardar" Width="75px" Enabled="False" OnClick="btnGuardarTA_Click"/><asp:Button ID="btnCancelarTA" runat="server" BackColor="LightGray" BorderColor="Gray"
                        BorderStyle="Outset" BorderWidth="1px" CssClass="EstiloBoton" EnableTheming="True"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Gray" OnClick="btnCancelarTA_Click"
                        onmouseout="this.style.fontWeight='normal'" onmouseover="this.style.fontWeight='bolder'"
                        Text="Cancelar" Width="75px" Enabled="False" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 80px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 130px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

