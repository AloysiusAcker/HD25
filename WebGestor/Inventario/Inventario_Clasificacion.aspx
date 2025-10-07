<%@ Page Title="" Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="Inventario_Clasificacion.aspx.vb" Inherits="Inventario_Inventario_Clasificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <br />
        <asp:Label runat="server" Text="Define Clasificación" CssClass="Titulos"></asp:Label>
        <br />
        <br />
    </div>

    <div>
        <div style="display: initial; position: relative; width: 39%; float: left;">
            <asp:TreeView ID="trvClasificacion" runat="server" ShowExpandCollapse="true"
                ShowLines="True" PopulateNodesFromClient="true" ExpandDepth="0">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <Nodes>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#5555DD" />
            </asp:TreeView>
        </div>

        <div style="display: initial; position: relative; width: 55%; float: right;">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Button ID="BtnAregarSubNivel" runat="server" Text="Agregar Sub Nivel" CssClass="btn btn-default" />
                    <asp:Button ID="BtnEditar" runat="server" Text="Editar" CssClass="btn btn-default" />
                    <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-default" />
                    <asp:Button ID="BtnAgregarNivel" runat="server" Text="Agregar Nivel" CssClass="btn btn-default" />
                </div>
                <div class="form-group">
                    <asp:Label runat="server" Text="Clasificación :" CssClass="col-lg-3 control-label" />
                    <div class="col-lg-9">
                        <asp:TextBox ID="LblClasificacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblDescripción" runat="server" Text="Descripción :" CssClass="col-lg-3 control-label" Visible="False" />
                    <div class="col-lg-7">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Button ID="BtnGrabar" runat="server" Text="Grabar" Visible="False" ControlStyle-CssClass=" btn btn-default" />
                    <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" Visible="False" ControlStyle-CssClass=" btn btn-default" />
                </div>
                <div class="form-group">
                    <asp:Label ID="LblCodClas" runat="server" CssClass="col-lg-2 control-label" Visible="false" />
                    <asp:Label ID="LblCodClasAyuda" runat="server" CssClass="col-lg-2 control-label" Visible="false" />
                    <asp:Label ID="Nivel" runat="server" Visible="false" />
                    <asp:Label ID="Nivel1" runat="server" Visible="false" />
                    <asp:Label ID="Nivel2" runat="server" Visible="false" />
                    <asp:Label ID="Nivel3" runat="server" Visible="false" />
                    <asp:Label ID="Nivel4" runat="server" Visible="false" />
                    <asp:Label ID="Nivel5" runat="server" Visible="false" />
                    <asp:Label ID="Nivel6" runat="server" Visible="false" />
                    <asp:Label ID="Nivel7" runat="server" Visible="false" />
                    <asp:Label ID="Nivel8" runat="server" Visible="false" />
                    <asp:Label ID="Nivel9" runat="server" Visible="false" />
                    <asp:Label ID="Nivel10" runat="server" Visible="false" />
                </div>
            </div>
        </div>

        <%--        <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="CLAS_CODIGO" CssClass="table table-bordered GridView"
            OnRowDataBound="gvEmployeeDetails_OnRowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <img alt="" style="cursor: pointer" src="../Icono/plus.gif" />
                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                            <asp:GridView ID="gv_Child" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered GridView" DataKeyNames="CLAS_CODIGO"
                                OnRowDataBound="gv_Child_OnRowDataBound" OnRowEditing="gv_Child_RowEditing" OnRowCancelingEdit="gv_Child_RowCancelingEdit"
                                OnRowUpdating="gv_Child_RowUpdating" OnRowDeleted="gv_Child_RowDeleting">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <img alt="" style="cursor: pointer" src="../Icono/plus.gif" />
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="gv_NestedChild" runat="server" CssClass="table table-bordered GridView" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="CLAS_NUMERO" HeaderText="NUMERACION" />
                                                        <asp:BoundField DataField="CLAS_NOMBRE" HeaderText="NOMBRE" />
                                                        <asp:BoundField DataField="CLAS_CODIGO" HeaderText="" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NUMERACION">
                                        <ItemTemplate>
                                            <asp:Label ID="LblClasNumero" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NUMERO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NOMBRE">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtClasNombre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NOMBRE") %>' CssClass="form-control-grid"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LblClasNombre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NOMBRE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_CODIGO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />&nbsp;
                                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text="Cancel" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ShowHeader="true" />
                                    <asp:TemplateField HeaderText="Salary ID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSalaryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_CODIGO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </ItemTemplate>
                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NUMERACION">
                    <ItemTemplate>
                        <asp:Label ID="LblClasNumero" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NUMERO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NOMBRE">
                    <EditItemTemplate>
                        <asp:TextBox ID="TxtClasNombre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NOMBRE") %>' CssClass="form-control-grid"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LblClasNombre" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_NOMBRE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:Label ID="lblEmpID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "CLAS_CODIGO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CausesValidation="false" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />&nbsp;
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ShowHeader="true" />
            </Columns>
        </asp:GridView>--%>
    </div>

</asp:Content>

