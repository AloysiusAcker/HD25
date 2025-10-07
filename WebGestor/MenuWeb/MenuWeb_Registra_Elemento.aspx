<%@ Page Language="VB" MasterPageFile="~/MenuWeb/PagPrincipal_MenuWeb.master" AutoEventWireup="false" CodeFile="MenuWeb_Registra_Elemento.aspx.vb" Inherits="MenuWeb_MenuWeb_Registra_Elemento" title="Untitled Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp2" %>
<script runat = "Server" >
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
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
        Dim NombreImagen As String = ""
        Dim Mensaje As String = ""
        Dim i As Integer = 0
        Dim pdCodGrupo As Double = 0
        Dim pdCodAuspiciador As Double = 0
        Dim fso = CreateObject("scripting.filesystemobject")
        Dim psFileName As String = Server.HtmlEncode(flImagen.FileName)
        Dim psExtension As String = ""
        Dim pdGrupo As Double = 0
        Dim pdCodItem As Double = 0
        Dim pdCodCategoria As Double = 0
        Dim psFecha1 As String = ""
        Dim psFecha2 As String = ""
        Dim psFecha3 As String = ""
        Dim psComentario As String = "N"
        lblError.Text = ""
        Try
            If cboGrupo.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Grupo."
            If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
            If cboItem.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Item."
            If Right(lblCategoria.Text, 1) = "*" And cboCategoria.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Categoria."
            If Right(lblNombre.Text, 1) = "*" And txtNombre.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblNombre.Text & "."
            If Right(lblNombreHtml.Text, 1) = "*" And txtNombreHtml.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblNombreHtml.Text & "."
            If Right(lblDescripcion.Text, 1) = "*" And txtDescripcion.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblDescripcion.Text & "."
            If Right(lblDetalle.Text, 1) = "*" And txtNombre.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblDetalle.Text & "."
            If Right(lblImagen.Text, 1) = "*" Then
                If (flImagen.HasFile) Then
                    psFileName = System.IO.Path.GetExtension(psFileName)
                    psExtension = psFileName
                    If psExtension = ".jpg" Or psExtension = ".JPG" Or psExtension = ".gif" Or psExtension = ".GIF" Then
                    Else
                        lblError.Text = lblError.Text & "<br> - Debe ser una imagen."
                    End If
                Else
                    lblError.Text = lblError.Text & "<br> - No hay Imagen que guardar"
                End If
            End If
            If Right(lblArchivo.Text, 1) = "*" Then
                If (flArchivo.HasFile) Then
                Else
                    lblError.Text = lblError.Text & "<br> - No hay Archivo que guardar"
                End If
            End If
            If Right(lblWeb1.Text, 1) = "*" And txtPagina1.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblWeb1.Text & "."
            If Right(lblWeb2.Text, 1) = "*" And txtPagina2.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblWeb2.Text & "."
            If Right(lblFecha1.Text, 1) = "*" And txtFecha1.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblFecha1.Text & "."
            If Right(lblFecha2.Text, 1) = "*" And txtFecha2.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblFecha2.Text & "."
            If Right(lblFecha3.Text, 1) = "*" And txtFecha3.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblFecha3.Text & "."
            If Right(lblCom1.Text, 1) = "*" And txtCom1.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblCom1.Text & "."
            If Right(lblCom2.Text, 1) = "*" And txtCom2.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblCom2.Text & "."
            If Right(lblCom3.Text, 1) = "*" And txtCom3.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblCom3.Text & "."
            If Right(lblCom4.Text, 1) = "*" And txtCom4.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblCom4.Text & "."
            If Right(lblCom5.Text, 1) = "*" And txtCom5.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar " & lblCom5.Text & "."
            If lblError.Text <> "" Then
                lblError.Text = " Existen las sgtes. observaciones: <br>" & lblError.Text
                Exit Sub
            End If
            'imagen
            If (flImagen.HasFile) Then
                NCant = 0
                For i = 1 To Len(flImagen.PostedFile.FileName)
                    If Mid(flImagen.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                Next
                Variable = flImagen.PostedFile.FileName
                For i = 1 To NCant
                    posicion = InStr(Variable, "\")
                    Variable = Mid(Variable, posicion + 1)
                    If i = NCant Then NombreImagen = Variable
                Next
                NombreImagen = Variable
                'verificar si existe la carpeta donde se alojaran las imagenes 
                If Not (fso.folderexists(Server.MapPath("Imagenes_" & Session("SiglaGrupoEmpresa")))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("Imagenes_" & Session("SiglaGrupoEmpresa")))
                End If
                'guardar imagen
                strSaveFileAs = Server.MapPath("Imagenes_" & Session("SiglaGrupoEmpresa") & "/" & flImagen.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                flImagen.SaveAs(strSaveFileAs)
            End If
            'archivo
            If (flArchivo.HasFile) Then
                NCant = 0
                For i = 1 To Len(flArchivo.PostedFile.FileName)
                    If Mid(flArchivo.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                Next
                Variable = flArchivo.PostedFile.FileName
                For i = 1 To NCant
                    posicion = InStr(Variable, "\")
                    Variable = Mid(Variable, posicion + 1)
                    If i = NCant Then NombreArchivo = Variable
                Next
                NombreArchivo = Variable
                'verificar si existe la carpeta donde se alojaran los archivos
                If Not (fso.folderexists(Server.MapPath("Archivos_" & Session("SiglaGrupoEmpresa")))) Then
                    System.IO.Directory.CreateDirectory(Server.MapPath("Archivos_" & Session("SiglaGrupoEmpresa")))
                End If
                'guardar Archivo
                strSaveFileAs = Server.MapPath("Archivos_" & Session("SiglaGrupoEmpresa") & "/" & flArchivo.FileName) ' "\\DATA\\Archivos\" + Upload.FileName 
                flArchivo.SaveAs(strSaveFileAs)
            End If
            'guardar datos
            pdGrupo = cboGrupo.SelectedValue.Trim
            pdCodItem = cboItem.SelectedValue.Trim
            If cboCategoria.SelectedValue <> "< Seleccionar >" Then
                pdCodCategoria = cboCategoria.SelectedValue.Trim
            End If
            If chkPermite.Checked = True Then psComentario = "S"
            obj.Ins_Elementos(pdGrupo, cboEmpresa.SelectedValue.Trim, pdCodItem, pdCodCategoria, txtNombre.Text.Trim, _
                              txtNombreHtml.Text.Trim, txtDescripcion.Text, txtDetalle.Text.Trim, txtPagina1.Text.Trim, txtPagina2.Text.Trim, _
                              NombreImagen, psFecha1, psFecha2, psFecha3, txtCom1.Text.Trim, txtCom2.Text.Trim, txtCom3.Text.Trim, txtCom4.Text.Trim, _
                              txtCom5.Text.Trim, NombreArchivo, "", psComentario, HttpContext.Current.User.Identity.Name)
            btnLimpiar_Click(sender, e)
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
                <td align="left" colspan="2" style="height: 50px; text-align: center" valign="top">
                    <div id="Div1" runat="server" class="EstiloTitleMenu" style="font-weight: bold; font-size: 14pt;
                        left: 253px; vertical-align: middle; width: 536px; color: gray; font-family: 'Bell MT', Broadway, Arial, Serif;
                        top: 275px; height: 2px; text-align: center">
                        Registro de Elementos de los Items del Menu</div>
                </td>
                <td align="left" style="width: 25px; height: 50px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="4" style="background-image: url(../Fotos/Linea_Gris.bmp);
                    height: 11px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 400px" valign="top">
                </td>
                <td align="left" style="width: 25px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
                        Width="536px"></asp:Label></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:Label ID="lblGrupo" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Grupo *"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:DropDownList ID="cboGrupo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="392px" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:Label ID="lblEmpresa" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa *"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboEmpresa" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboGrupo" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:Label ID="lblItem" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Item *"></asp:Label></td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboItem" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" AutoPostBack="True"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboEmpresa" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                    </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <contenttemplate>
<asp:Label id="lblCategoria" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" Text="Categoria" __designer:wfdid="w70"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <contenttemplate>
<asp:DropDownList id="cboCategoria" runat="server" Width="392px" Font-Size="8pt" Font-Names="Arial" Enabled="False"></asp:DropDownList> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <contenttemplate>
<asp:Label id="lblImagen" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" Text="Imagen" __designer:wfdid="w71"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:FileUpload ID="flImagen" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="392px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <contenttemplate>
<asp:Label id="lblArchivo" runat="server" Width="152px" Font-Size="8pt" Font-Names="Arial" Text="Archivo" __designer:wfdid="w72"></asp:Label> 
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    <asp:FileUpload ID="flArchivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="392px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <div style="text-align: left">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <contenttemplate>
<TABLE style="WIDTH: 550px" id="lblCampos" cellSpacing=0 cellPadding=0 border=0 runat="server"><TBODY><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblNombre" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Nombre" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtNombre" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblNombreHtml" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Nombre (Html)" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtNombreHtml" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblDescripcion" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Descripción" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtDescripcion" runat="server" Width="386px" Height="40px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="400" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: top; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblDetalle" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Detalle" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtDetalle" runat="server" Width="386px" Height="40px" Font-Size="8pt" Font-Names="Arial" TextMode="MultiLine" MaxLength="400" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblWeb1" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Pagina 1" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtPagina1" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblWeb2" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Pagina 2" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtPagina2" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblFecha1" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Fecha 1" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtFecha1" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:Label id="lblFormato1" runat="server" Width="112px" Font-Size="8pt" Font-Names="Arial" Text="Formato dd/mm/yyyy" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblFecha2" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Fecha 2" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtFecha2" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:Label id="lblFormato2" runat="server" Width="112px" Font-Size="8pt" Font-Names="Arial" Text="Formato dd/mm/yyyy" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblFecha3" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Fecha 3" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtFecha3" runat="server" Width="120px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox> <asp:Label id="lblFormato3" runat="server" Width="112px" Font-Size="8pt" Font-Names="Arial" Text="Formato dd/mm/yyyy" Visible="False"></asp:Label></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblCom1" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Completar 1" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtCom1" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblCom2" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Completar 2" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtCom2" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblCom3" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Completar 3" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtCom3" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px" vAlign=top align=left><asp:Label id="lblCom4" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Completar 4" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px" vAlign=top align=left><asp:TextBox id="txtCom4" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR><TR><TD style="VERTICAL-ALIGN: middle; WIDTH: 150px; HEIGHT: 22px" vAlign=top align=left><asp:Label id="lblCom5" runat="server" Width="144px" Font-Size="8pt" Font-Names="Arial" Text="Completar 5" Visible="False"></asp:Label></TD><TD style="VERTICAL-ALIGN: middle; WIDTH: 400px; HEIGHT: 22px" vAlign=top align=left><asp:TextBox id="txtCom5" runat="server" Width="386px" Font-Size="8pt" Font-Names="Arial" Visible="False"></asp:TextBox></TD></TR></TBODY></TABLE>
</contenttemplate>
                        <triggers>
<asp:AsyncPostBackTrigger ControlID="cboItem" EventName="SelectedIndexChanged"></asp:AsyncPostBackTrigger>
</triggers>
                    </asp:UpdatePanel>&nbsp;</div>
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px" valign="top">
                    <asp:CheckBox ID="chkPermite" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Permite hacer comentarios" Width="160px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" colspan="2" style="vertical-align: middle; height: 22px; text-align: center"
                    valign="top">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="EstiloBoton_Ac" Text="Guardar" OnClick = "btnGuardar_Click"
                        Width="86px" />
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="EstiloBoton_Ac" Text="Limpiar"
                        Width="86px" />
                    <asp:Button ID="btnRegresar" runat="server" CssClass="EstiloBoton_Ac" Text="Regresar"
                        Width="86px" /></td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 150px; height: 22px" valign="top">
                </td>
                <td align="left" style="vertical-align: middle; width: 400px; height: 22px" valign="top">
                    &nbsp;
                </td>
                <td align="left" style="width: 25px; height: 22px" valign="top">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

