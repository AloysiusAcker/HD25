<%@ Control Language="vb" AutoEventWireup="false" Inherits="MProyRight" CodeFile="MProyRight.ascx.vb" %>
<asp:DataGrid id="MyDataGrid" runat="server" ShowHeader="False" CellPadding="0" GridLines="None"
	PageSize="4" AllowPaging="True" Height="400px" Width="100px" AutoGenerateColumns="False">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<table style="BORDER-RIGHT: darkgray 1px solid; BORDER-TOP: darkgray 1px solid; FONT-SIZE:8pt; BORDER-LEFT: darkgray 1px solid; BORDER-BOTTOM: darkgray 1px solid; FONT-FAMILY: verdana; height: 100px; width: 95px;"
					cellPadding="3" cols="0" border="0" cellSpacing="0">
					<tr>
						<td style="width: 100px">
							<a href='<%# DataBinder.Eval(Container.DataItem, "AUSPI_LINK") %>' target="_blank" >
								<img align="top" border="1" alt='<%# DataBinder.Eval(Container.DataItem, "AUSPI_NOMBRE")%>' src='<%# DataBinder.Eval(Container.DataItem, "IMAGEN") %>' style="width: 95px; height: 100px"/>
							</a>
						</td>
					</tr>
					<tr>
						<td align="left" style="width: 100px"><%# DataBinder.Eval(Container.DataItem, "AUSPI_DESCRIP") %>
						</td>
					</tr>
				</table>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle VerticalAlign="Middle" Font-Size="8pt" Font-Names="Verdana" HorizontalAlign="Center" Wrap="True"></PagerStyle>
</asp:DataGrid>
<asp:Label ID="Label1" runat="server"></asp:Label>
