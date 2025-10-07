<%@ Page Language="VB" MasterPageFile="~/PagPrincipal_A.master" AutoEventWireup="false" CodeFile="PaginaPrincipal.aspx.vb" Inherits="PaginaPrincipal" title="GestorPlus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align: left">
                <TABLE style="WIDTH: 600px" cellSpacing=0 cellPadding=0 border=0>
                  <TBODY>
                    <TR>
                        <TD style="WIDTH: 25px; HEIGHT: 50px" vAlign=top align=left>
                        </TD>
                        <TD style="VERTICAL-ALIGN: middle; WIDTH: 550px; HEIGHT: 50px; TEXT-ALIGN: left" vAlign=top align=left><asp:Label id="lblEtiq1" runat="server" Width="60px" Height="18px" ForeColor="DimGray" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" Text="Empresa"></asp:Label>&nbsp; 
                            <asp:DropDownList id="cboEmpresa" tabIndex=1 runat="server" Width="440px" Height="20px" ForeColor="DimGray" Font-Size="8pt" Font-Names="Arial" Font-Bold="True" AutoPostBack="True">
                             </asp:DropDownList></TD>
                        <TD style="WIDTH: 25px; HEIGHT: 50px" vAlign=top align=left>
                        </TD>
                    </TR>
                      <tr>
                          <td align="left" style="height: 6px;" valign="top" colspan="3">
                              <img src="Fotos/lineaCas.JPG" /></td>
                      </tr>
                      <tr>
                          <td align="left" colspan="3" style="height: 10px" valign="top">
                          </td>
                      </tr>
                     <TR>
                        <TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=top align=left>
                        </TD>
                        <TD style="WIDTH: 550px; HEIGHT: 30px" vAlign=top align=left>
                            <asp:DataList id="MyDataList" runat="server" Width="550px"  RepeatDirection="Horizontal" RepeatColumns="1" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div style="padding-right: 3px; padding-left: 6px; font-size: 9pt; padding-bottom: 3px;
                                            padding-top: 3px; font-family: Verdana">
                                            <div style=" font-weight: bold; font-size: 15pt;  color: SeaGreen; font-style: italic;  font-family: 'Bell MT', Broadway, Arial, Serif;">
                                                <b>
                                                    <%# DataBinder.Eval(Container.DataItem, "PARRAFO_TITULO") %>
                                                </b>
                                            </div>
                                            <%# DataBinder.Eval(Container.DataItem, "PARRAFO_DESCRIP") %>
                                            <p>
                                            </p>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemStyle Font-Italic="False" Font-Names="Arial" Font-Size="8pt" />
                                    <ItemStyle Font-Names="Arial" Font-Size="8pt" />
                                    <SelectedItemStyle Font-Names="Arial" Font-Size="8pt" />
                                    <HeaderStyle Font-Names="Arial" Font-Size="8pt" />
                            </asp:DataList>
                          </TD>
                          <TD style="WIDTH: 25px; HEIGHT: 30px" vAlign=top align=left>
                          </TD>
                    </TR>
                    <TR>
                        <TD style="WIDTH: 25px; height: 19px;" vAlign=top align=left>
                        </TD>
                        <TD style="WIDTH: 550px; height: 19px;" vAlign=top align=left>
                            <asp:Label id="lblMensaje" runat="server" Width="364px" ForeColor="Red" Font-Size="9pt" Font-Names="Tahoma" Visible="False">
                            </asp:Label>
                        </TD>
                        <TD style="WIDTH: 25px; height: 19px;" vAlign=top align=left>
                        </TD>
                    </TR>
                    <TR>
                        <TD style="WIDTH: 25px; height: 19px;" vAlign=top align=left>
                        </TD>
                        <TD style="WIDTH: 550px; height: 19px;" vAlign=top align=left>
                            &nbsp;</TD>
                        <TD style="WIDTH: 25px; height: 19px;" vAlign=top align=left>
                        </TD>
                    </TR>
                  </TBODY>
                </TABLE>
</div>
  
</asp:Content>

