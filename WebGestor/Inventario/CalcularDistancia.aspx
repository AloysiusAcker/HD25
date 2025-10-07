<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/PagPrincipal_A.Master" CodeFile="CalcularDistancia.aspx.vb" Inherits="CalcularDistancia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="LblEtiq1" runat="server" Text="Las 2 Oficinas mas cercanas a la central" CssClass="Titulos" />
                </div> 
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Repeater ID="rptGroups" runat="server">
                        <ItemTemplate>
                            <div class="form-control group">
                                <ul>
                                    <li><%# Container.DataItem.Item1 %></li>
                                    <li><%# Container.DataItem.Item2 %></li>
                                </ul>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div> 
            </div>
        </div>
</asp:Content>
