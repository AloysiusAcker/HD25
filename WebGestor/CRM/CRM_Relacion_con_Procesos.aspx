<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.master" CodeFile="CRM_Relacion_con_Procesos.aspx.vb" Inherits="CRM_Relacion_con_Procesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<asp:Label ID="LblTitulo" runat="server" Text="Relación con Procesos" CssClass="Titulos"></asp:Label><br />
    <br />
    <div class="form-horizontal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-group">
                    <asp:Button ID="BtnRelacionar" runat="server" CssClass="btn btn-group" Text="Relacionar" />
                    <asp:Button ID="BtnListar" runat="server" CssClass="btn btn-group" Text="Listar" />
                </div>
            </ContentTemplate>
            <Triggers>
            </Triggers>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="LblRelacionProcesos" runat="server" Text="Relación de Procesos" CssClass="subTitulos" Visible="false"></asp:Label><br />
                <br />
                <div class="form-group">
                    <asp:Label ID="LblNivel1" runat="server" Text="Nivel 1 :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlNivel1" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="ChkNivel2" runat="server" Text="Nivel 2 :" AutoPostBack="True" CssClass="col-lg-2 control-label" Visible="False" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlNivel2" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False" Enabled="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="ChkNivel3" runat="server" Text="Nivel 3 :" AutoPostBack="True" CssClass="col-lg-2 control-label" Visible="False" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlNivel3" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False" Enabled="false">
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="BtnGrabar" runat="server" CssClass="btn btn-group" Text="Grabar" Visible="false" />
                </div>
                <div class="form-group">
                    <asp:Label ID="LblProceso" runat="server" Text="Proceso :" Class="col-lg-2 control-label" Visible="false" />
                    <div class="col-lg-5">
                        <asp:DropDownList ID="DdlProceso" runat="server" AutoPostBack="True" CssClass="form-control" Visible="False">
                        </asp:DropDownList>
                    </div>
                    <asp:Button ID="BtnCancelar" runat="server" CssClass="btn btn-group" Text="Cancelar" Visible="false" />
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="BtnRelacionar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            Cargando, por favor espere......
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-group">
                <p id="LblTotalRelacionProcesos" class="control-label" style="margin-left: 25px; color: darkred; font-weight: bold" runat="server" visible="false">Total de Registros :</p>
                <p id="LblTotalRelacionProcesosL" class="control-label" style="width: 15px; color: darkred; font-weight: bold" runat="server" visible="false"></p>
            </div>
            <div class="form-group">
                <div class="col-lg-12">
                    <asp:GridView ID="GvListaRelacionProcesos" AutoGenerateColumns="false" runat="server" CssClass="table table-bordered GridView">
                        <Columns>
                            <asp:BoundField DataField="TBGTP_NIVEL1" HeaderText="" SortExpression="TBGTP_NIVEL1">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:ButtonField CommandName="QuitarRelacion" Text="" ButtonType="Image" ImageUrl="~/icono/delete2_opt.png">
                                <ItemStyle Height="10px" Width="10px" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="TBGTP_NIVEL2" HeaderText="" SortExpression="TBGTP_NIVEL2">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PROCESO_CODIGO" HeaderText="Codigo" SortExpression="PROCESO_CODIGO" />
                            <asp:BoundField DataField="PROCESO_NOMBRE" HeaderText="Proceso" SortExpression="PROCESO_NOMBRE" />
                            <asp:BoundField DataField="NIVEL1_DESCRIP" HeaderText="TBESP_GTP1" SortExpression="NIVEL1_DESCRIP" />
                            <asp:BoundField DataField="NIVEL2_DESCRIP" HeaderText="TBESP_GTP2" SortExpression="NIVEL2_DESCRIP" />
                            <asp:BoundField DataField="NIVEL3_DESCRIP" HeaderText="TBESP_GTP3" SortExpression="NIVEL3_DESCRIP" />
                            <asp:BoundField DataField="TBGTP_NIVEL3" HeaderText="" SortExpression="TBGTP_NIVEL3">
                                <ItemStyle ForeColor="White"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnListar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="BtnGrabar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GvListaRelacionProcesos" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <br />
</asp:Content>
