<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteConstructora.aspx.cs" Inherits="Constructoras_ReporteConstructora" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Reporte Constructora</h3>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InicioContent" runat="Server">

    <table style="width: 100%">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 5%">&nbsp;</td>
            <td>
                <table style="width: 60%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="Label1" runat="server" Text="Proyecto:"></asp:Label></td>
                        <td style="width: 30%">
                            <asp:DropDownList ID="ddlProyectos" runat="server" Width="250px"></asp:DropDownList></td>
                        <td style="width: 20%"></td>
                        <td style="width: 30%">
                            <asp:ImageButton ID="ibtnFind" ValidationGroup="Proyecto" runat="server" ImageUrl="~/Images/btn_search.png" EnableTheming="True" AlternateText="Aplicar filtros de busqueda" OnClick="ibtnFind_Click" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 5%">&nbsp;</td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 5%">&nbsp;</td>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" Font-Size="Small" CssClass="ClassExtra" OnClick="LinkButton1_Click">Exportar a Excel</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="pnaelPest2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal">
                        <table style="width: 200%">
                            <tr>
                                <td style="width: 0%"></td>
                                <td style="align-content: center">
                                    
                                    <asp:GridView ID="gvCasos" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="No se han encontrado casos coincidentes." AllowPaging="True"
                                        PageSize="8" OnPageIndexChanging="gvCasos_PageIndexChanging" OnRowCreated="gvCasos_RowCreated" ShowFooter="True" OnRowDataBound="gvCasos_RowDataBound">
                                        <Columns>

                                            <asp:BoundField DataField="NoDeCaso" HeaderText="Número de Radicado" />
                                            <asp:BoundField DataField="Numero_identificacion" HeaderText="Cédula" />
                                            <asp:BoundField DataField="Constructora" HeaderText="Constructora" />
                                            <asp:BoundField DataField="Proyecto" HeaderText="Proyecto" />
                                            <asp:BoundField DataField="Ciudad" HeaderText="ciudad" />
                                            <asp:BoundField DataField="NombreActividad" HeaderText="estado" />
                                            <asp:BoundField DataField="subestado" HeaderText="Subestado" />
                                            <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha de creación de actividad" />
                                            <asp:BoundField DataField="FechaFinalizacion" HeaderText="Fecha de finalización" />
                                            <asp:BoundField DataField="FechaVencimiento" HeaderText="fecha de vencimiento" />
                                            <asp:BoundField DataField="Duracion" HeaderText="Duración" />
                                            <asp:BoundField DataField="Observaciones" HeaderText="Observación" />
                                            <asp:BoundField DataField="Adjuntos" HeaderText="Datos cargados" />

                                        </Columns>
                                    </asp:GridView>
                                        
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
