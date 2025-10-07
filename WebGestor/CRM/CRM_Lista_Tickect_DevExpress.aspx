<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CRM_Lista_Tickect_DevExpress.aspx.vb" Inherits="CRM_CRM_Lista_Tickect_DevExpress" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v19.1, Version=19.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestor</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="HandheldFriendly" content="true" />
    <link href="../Css_WebGestor.css" rel="stylesheet" />
    <script type="text/javascript">
        function OnGridFocusedRowChanged() {
            // Query the server for the "EmployeeID" and "Notes" fields from the focused row
            // The values will be returned to the OnGetRowValues() function
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'TICKET_codigo',  OnGetRowValues);
        }
        function OnGetRowValues(values) {
            Listar_Ticket(values[1]);
        }
        // Value array contains "EmployeeID" and "Notes" field values returned from the server
        function OnGridEstadoFocusedRowChanged() {
            // Query the server for the "EmployeeID" and "Notes" fields from the focused row
            // The values will be returned to the OnGetRowValues() function
            gridestado.GetRowValues(gridestado.GetFocusedRowIndex(), 'TICKET_ESTADO', OnGetRowValues);
        }
        function OnCloseUp(s, e) {
            btnShowHide.SetVisible(true);
        }
        function OnShowHideClick(s, e) {
            dockPanel.Show();
            btnShowHide.SetVisible(false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

            <section id="web">
                <div id="lblBanner" class="title" >
                <img src="../Fotos/LOGO%20WEBCASH-06.jpg" />
                </div> 
                <div id="lblTitulo" class="title" >
                    <asp:Label ID="lblTitle" runat="server" Text="CRM" Font-Names ="Arial" Font-Size ="14px"></asp:Label>        
                </div>
                <div id="lblLinea" class="title" >
                <img src="../Fotos/lineaCas.JPG" />
                </div>           
            </section>  
            <br />
            <br />
            <dx:ASPxGridView ID="GridEstado" runat="server" AutoGenerateColumns="False">

                <SettingsPager Visible="False">
                </SettingsPager>
                <Settings ShowHeaderFilterBlankItems="False" />

                <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                <SettingsPopup>
                <HeaderFilter MinHeight="140px"></HeaderFilter>
                </SettingsPopup>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="estado" VisibleIndex="0" >
                                  <Settings AllowSort="True" SortMode="Value" AllowHeaderFilter="False" />
                                  <HeaderStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TICKET_ESTADO" Visible="False" VisibleIndex="1">
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Styles>
                    <FocusedRow BackColor="#CCCCCC" ForeColor="Black">
                    </FocusedRow>
                </Styles>
                <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                <SettingsBehavior AllowFocusedRow="true" />
                <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridEstadoFocusedRowChanged(); }" />

            </dx:ASPxGridView>



                <div id="lblCRM"  >
                
                      <dx:ASPxGridView ID="grid" runat="server" 
                            AutoGenerateColumns="False">
                            <SettingsPager Visible="False">
                            </SettingsPager>
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <SettingsPopup>
                            <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <StylesPopup>
                                <HeaderFilter>
                                    <Header BackColor="White">
                                    </Header>
                                </HeaderFilter>
                            </StylesPopup>
                            <Columns>
                              <dx:GridViewDataTextColumn  FieldName ="TICKET_CODIGO" VisibleIndex="0" Caption="Nro. Ticket" SortOrder="Ascending">
                                  <Settings AllowSort="True" SortMode="Value" />
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="TICKET_ESTADO" VisibleIndex="1" Visible="False">
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="estado" VisibleIndex="2" Caption="Estado" >
                                  <Settings AllowSort="True" SortMode="Value" />
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="TICKET_PROCESO" VisibleIndex="3" Visible="False">
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="proceso" VisibleIndex="4" Caption="Proceso" >
                                  <Settings AllowSort="True" SortMode="Value" />
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="TICKET_MOTIVO" VisibleIndex="5" Caption="Motivo" >
                                  <Settings AllowSort="True" SortMode="Value" />
                                  <HeaderStyle BackColor="White" ForeColor="black" HorizontalAlign="Center" />
                              </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <GroupRow BackColor="White" Font-Names="Arial" Font-Size="8pt">
                                </GroupRow>
                                <FocusedGroupRow BackColor="White" ForeColor="Black">
                                </FocusedGroupRow>
                                <SelectedRow BackColor="#3399FF" ForeColor="Black">
                                </SelectedRow>
                                <FilterRow BackColor="White">
                                </FilterRow>
                                <FilterCell BackColor="#66CCFF" Border-BorderColor="White" Border-BorderStyle="None" ForeColor="Black">
                                </FilterCell>
                                <EditForm BackColor="White">
                                </EditForm>
                                <HeaderFilterItem BackColor="White">
                                </HeaderFilterItem>
                            </Styles>
                            <Border BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
                            <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                            <SettingsBehavior AllowFocusedRow="true" />
                            <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                      </dx:ASPxGridView>
                     <%-- <asp:SqlDataSource ID="sql_dsTicket" runat="server" ConnectionString="<%$ ConnectionStrings:ConexBBVA %>" SelectCommand="SELECT TBTICKET.TICKET_CODIGO, TBTICKET.TICKET_ESTADO, TBOPC475.ELEMEN_VALOR, TBTICKET.TICKET_PROCESO, TBOPC473.ELEMEN_VALOR AS Expr1, TBTICKET.TICKET_MOTIVO, TBTICKET.TICKET_DESCRIPCION FROM TBTICKET LEFT OUTER JOIN BDGrupoEmpresas.dbo.TBCELEMEN AS TBOPC475 ON TBOPC475.ELEMEN_TABLA = 'TBOPC475' AND TBOPC475.ELEMEN_CODIGO = TBTICKET.TICKET_ESTADO LEFT OUTER JOIN BDGrupoEmpresas.dbo.TBCELEMEN AS TBOPC473 ON TBOPC473.ELEMEN_TABLA = 'TBOPC473' AND TBOPC473.ELEMEN_CODIGO = TBTICKET.TICKET_PROCESO WHERE (TBTICKET.TICKET_SYS_EST = '0') order by TBTICKET.TICKET_CODIGO asc"></asp:SqlDataSource>--%>
                </div>
                </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridEstado" EventName="SelectionChanged" />
            </Triggers>
            </asp:UpdatePanel>
    </form>
</body>
</html>
