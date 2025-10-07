<%@ Page Title="" Language="VB" MasterPageFile="~/EvaluacionProcesos/PagPrincipal_EvalProceso.master" AutoEventWireup="false" CodeFile="EvalProcesos.aspx.vb" Inherits="EvaluacionProcesos_EvalProcesos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../icono/minus.gif");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../icono/plus.gif");
            $(this).closest("tr").next().remove();
        });
    </script>
    
      <div style="text-align: center">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
            <tr>
                <td align="left" colspan="3" style="height: 50px" valign="top">
                    <div id="lblTitulo" runat="server" class="EstiloTitle" style="display: inline;
                        font-weight: bold; font-size: 14pt; vertical-align: middle; width: 750px; color: gray;
                        font-style: italic; font-family: 'Bell MT', Broadway, Arial, Serif; position: absolute;
                        height: 1px; text-align: center">
                        Evaluación de Procesos</div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" style="height: 11px; background-image: url(../Fotos/linea.JPG);" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="top">
                    <asp:Label ID="lblError" runat="server" Font-Size="8" Font-Names="arial" ForeColor="Red"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Button ID="BtnListar" runat="server" CssClass="EstiloBoton" Text="Listar" Height="19px" />
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label4" runat="server" CssClass="EstiloLabel" Text="Año"></asp:Label>
                    <asp:DropDownList ID="DdlAño" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label2" runat="server" CssClass="EstiloLabel" Text="Responsable"></asp:Label>
                    <asp:DropDownList ID="DdlBusResponsable" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label1" runat="server" CssClass="EstiloLabel" Text="Proceso"></asp:Label>
                    <asp:DropDownList ID="DdlProceso" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="Label3" runat="server" CssClass="EstiloLabel" Text="Estado"></asp:Label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="EstiloDropDownList" AutoPostBack="True">
                        <asp:ListItem Selected="True">&lt; Todos &gt;</asp:ListItem>
                        <asp:ListItem Value="1">Programado</asp:ListItem>
                        <asp:ListItem Value="2">En Proceso</asp:ListItem>
                        <asp:ListItem Value="3">Realizado</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <div id="divLista">
                        <asp:GridView ID="gwLista" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                            <Columns>
                                <asp:ButtonField CommandName="Evaluar" Text="Evaluar" />
                                <asp:ButtonField CommandName="Accion" Text="Plan Acción" />
                                <asp:BoundField DataField="CodProceso">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NombreProceso" HeaderText="Nombre Proceso">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FrecuenciaProceso" HeaderText="Frecuencia Proceso">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EVALUACION_CODIGO" HeaderText="Nro. Eval." >
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Oficina" HeaderText="Oficina" />
                                <asp:BoundField DataField="fecha_eval" HeaderText="Fecha Eval." />
                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                                <asp:BoundField DataField="PromedioFinal" HeaderText="Resultado">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="evaluacion_estado">
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="planestado" HeaderText="Plan Acción" />
                                <asp:BoundField DataField="EVALUACION_OFICINA">
                                <ItemStyle ForeColor="White" Width="0px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    &nbsp;</td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="lblEtiqueta" runat="server" Text="" Font-Names="arial" Font-Size="8"></asp:Label></td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; " valign="middle">
                    <asp:LinkButton ID="GuardarRptas" runat="server" Font-Names="Arial" Font-Size="9pt" onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                        Font-Underline="False" ForeColor="Gray" Height="22px" Width="122px" Font-Italic="false" Visible="False">Guardar Respuestas</asp:LinkButton>
                    <asp:LinkButton ID="Cancelar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"  onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                            ForeColor="Gray" Height="22px" Font-Italic="false" Visible="False">Cancelar</asp:LinkButton>
                    &nbsp;
                    <asp:LinkButton ID="Cerrar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"  onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                            ForeColor="Gray" Height="22px" Font-Italic="false" Visible="False">Cerrar</asp:LinkButton>
                     <asp:LinkButton ID="Exportar" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Underline="False"  onmouseover="this.style.fontWeight='bolder'" onmouseout="this.style.fontWeight='normal'"
                            ForeColor="Gray" Height="22px" Font-Italic="false" Visible="False">Exportar a Excel</asp:LinkButton>
                </td>
                   

                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; " valign="middle">
                    <asp:Label ID="lblEtiqueta2" runat="server" CssClass="EstiloLabel" Text="Resultado Final" Visible="False"></asp:Label>
                    <asp:TextBox ID="txtResultado" runat="server" CssClass="bordeTexboxPag" Font-Names="Arial" Font-Size="14pt" Visible="False" Width="82px"></asp:TextBox>
                    <div>
                        <asp:GridView ID="GvPuntaje" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt">
                            <Columns>
                                <asp:BoundField DataField="Puntaje" HeaderText="Puntaje" />
                                <asp:BoundField DataField="Total" HeaderText="Total" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px;" valign="middle">
                    <asp:GridView ID="gwListaDetalle" runat="server" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False" Height="100%">
                        <Columns>
                            <asp:BoundField DataField="c1" HeaderText="Pregunta" />
                            <asp:BoundField DataField="c2" HeaderText="Ultima Rpta">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Eval1">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtRpta" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c3") %>'></asp:TextBox>--%>
                                    <asp:DropDownList ID="cmbRpta" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observación">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtObs" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c4") %>' MaxLength="1500"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="c5">
                            <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="White" />
                            </asp:BoundField>
                            <asp:BoundField DataField="c6">
                            <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle ForeColor="White" Width="0px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Eval. 2" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta2" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 3" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta3" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 4" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta4" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 5" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta5" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 6" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta6" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 7" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta7" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 8" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta8" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 9" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta9" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eval. 10" Visible="False">
                                <ItemTemplate>
                                    <asp:DropDownList ID="cmbRpta10" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" src="../Icono/download.gif" style="width: 21px; height: 21px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" src="../Icono/plus.gif" style="width: 21px; height: 21px" />
                                    <asp:Panel ID="pnlOrders2" runat="server" Style="display: none">
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Archivo"  />
                                        <br />
                                    </asp:Panel>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="gwListaDetallePlan" runat="server" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False" Height="100%">
                        <Columns>
                            <asp:BoundField DataField="EVALPRO_PREGUNTA" HeaderText="Pregunta Nro.">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TAREADET_DESCRIPCION" HeaderText="Descripción" >
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EVALPRO_RESPUESTA" HeaderText="Respuesta">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EVALPRO_OBSERVACION" HeaderText="Observacion">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="lblEtiqueta3" runat="server" CssClass="EstiloLabel" Text="Plan de Acción" Visible="False"></asp:Label>
                    </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:LinkButton ID="LnkGuardar" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Gray" Visible="False">Guardar       </asp:LinkButton>
                    <asp:LinkButton ID="LnkCancelar" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Gray" Visible="False">Cancelar</asp:LinkButton>
                    </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:GridView ID="gwListaPlanAccion" runat="server" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False" Height="100%">
                        <Columns>
                            <asp:BoundField DataField="c1" HeaderText="Pregunta Nro." />
                            <asp:TemplateField HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAccion" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAccion" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c2") %>' Width="300px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quien">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtRpta" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c3") %>'></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlQuien" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20"  >
                                </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cuando">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" BorderWidth="0px" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c4") %>' Height="17px" Width="110px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha" PopupButtonID="txtfecha"></cc1:CalendarExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtRpta" runat="server"  BorderColor="White" Font-Names="Arial" Font-Size="8pt" Text='<%# Bind("c3") %>'></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlEstado" runat="server" BorderColor="White" Font-Names="Arial" Font-Size="8pt"  Height="20" SelectedValue='<%# Bind("c5") %>'  >
                                    <asp:ListItem Text=" " Value="" />
                                    <asp:ListItem Text="No Iniciado" Value="1" />
                                    <asp:ListItem Text="Proceso" Value="2" />
                                    <asp:ListItem Text="Completado" Value="3" />
                                </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Image" CommandName="Guardar" Text="Guardar" ImageUrl="~/Fotos/grabar.png" />
                        </Columns>
                    </asp:GridView>
                    </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    <asp:Label ID="lblCodProceso" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodEval" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                    <asp:Label ID="lblEstado" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False"></asp:Label>
                    <asp:Label ID="lblCodOficina" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Label" Visible="False"></asp:Label>
                </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
            <tr>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
                <td align="left" style="width: 750px; height: 20px;" valign="middle">
                    
                    <div id="divExportar" runat="server" visible ="false">
                        <asp:GridView ID="gwListaExportar" runat="server" Font-Names="Arial" Font-Size="8pt" AutoGenerateColumns="False" Height="100%">
                        <Columns>
                            <asp:BoundField DataField="c1" HeaderText="Pregunta" />
                            <asp:BoundField DataField="c2" HeaderText="Ultima Rpta">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="c3" HeaderText="Eval. 1">
                            </asp:BoundField>
                            <asp:BoundField DataField="c4" HeaderText="Observación" />
                            <asp:BoundField DataField="c0">
                            <ItemStyle ForeColor="White" Width="0px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="c5" HeaderText="Eval. 2" Visible="False" />
                            <asp:BoundField DataField="c6" HeaderText="Eval. 3" Visible="False" />
                            <asp:BoundField DataField="c7" HeaderText="Eval. 4" Visible="False" />
                            <asp:BoundField DataField="c8" HeaderText="Eval. 5" Visible="False" />
                            <asp:BoundField DataField="c9" HeaderText="Eval. 6" Visible="False" />
                            <asp:BoundField DataField="c10" HeaderText="Eval. 7" Visible="False" />
                            <asp:BoundField DataField="c11" HeaderText="Eval. 8" Visible="False" />
                            <asp:BoundField DataField="c12" HeaderText="Eval. 9" Visible="False" />
                            <asp:BoundField DataField="c13" HeaderText="Eval. 10" Visible="False" />
                        </Columns>
                    </asp:GridView>
                    </div>
                    </td>
                <td align="left" style="width: 25px; height: 20px;" valign="top"></td>
            </tr>
        </table>
    </div>
</asp:Content>

