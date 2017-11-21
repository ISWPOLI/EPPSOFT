<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Map.aspx.cs" Inherits="Reports_Map" %>

<%@ Register Src="~/UserControl/UCDetailAvances.ascx" TagName="UCDetails" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style7
        {
        }

        .auto-style8
        {
            text-align: right;
            width: 30%;
        }

        .auto-style10
        {
            height: 30px;
        }

        .auto-style11
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Mediciones</h3>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InicioContent" runat="Server">

    <div id="main_hitos">
        <div class="shadow_top">
            <div class="shadow_top_l">&nbsp;</div>
            <div class="shadow_top_r">&nbsp;</div>
        </div>
        <div class="main_right">
            <div class="main_left">
                <div id="main_content_hitos">


                    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
                        <asp:TabPanel runat="server" ID="Panel1" HeaderText="Mapa">
                            <ContentTemplate>
                                <table class="table_m_m" style="width: 100%">
                                    <tr>
                                        <td>
                                            <div id="div0" class="box_content">
                                                <asp:Panel runat="server" DefaultButton="ibtnFind" ID="tabs" Style="width: 100%;">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="width: 20%">
                                                                <asp:Label ID="Label1" runat="server" Text="Departamento:"></asp:Label></td>
                                                            <td style="width: 30%">
                                                                <asp:DropDownList ID="ddlDepartamentos" runat="server" Width="250px"></asp:DropDownList></td>
                                                            <td style="width: 20%"></td>
                                                            <td style="width: 30%">
                                                                <asp:ImageButton ID="ibtnFind" ValidationGroup="Proyecto" runat="server" ImageUrl="~/Images/btn_search.png" EnableTheming="True" AlternateText="Aplicar filtros de busqueda" OnClick="ibtnFind_Click" /></td>
                                                            <tr>
                                                                <td colspan="4" style="text-align: right"></td>
                                                            </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="map_min">
                                                <cc2:GMap ID="GMap" Language="es" runat="server" Width="100%" Height="900px" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel runat="server" ID="Panel2" HeaderText="Reporte">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <table class="table_m_m" style="width: 100%">
                                            <tr>
                                                <td>
                                                    <div id="Div1" class="box_content">
                                                        <asp:Panel runat="server" DefaultButton="ibtnFind2" ID="Panel3" Style="width: 100%;">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:Panel ID="PbusquedaBasica" runat="server" HorizontalAlign="Center" Width="80%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td style="text-align: right; vertical-align: inherit; width: 40%" class="auto-style7">
                                                                                        <asp:Label ID="Label2" runat="server" Text="Cédula de ciudadanía:"></asp:Label>

                                                                                    </td>
                                                                                    <td class="auto-style8">
                                                                                        <asp:TextBox ID="TxtCedula" runat="server"></asp:TextBox>
                                                                                        <asp:FilteredTextBoxExtender ID="ftbeNumeros" runat="server" FilterType="Numbers" TargetControlID="TxtCedula" Enabled="True">
                                                                                        </asp:FilteredTextBoxExtender>

                                                                                    </td>
                                                                                    <td style="text-align: left; vertical-align: bottom" class="auto-style7">
                                                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Búsqueda avanzada" OnClick="LinkButton1_Click"></asp:LinkButton>

                                                                                    </td>

                                                                                </tr>

                                                                            </table>

                                                                        </asp:Panel>
                                                                        <asp:Panel ID="PbusquedaAvanzada" runat="server" Visible="False" Enabled="False" Width="80%">
                                                                            <table style="width: 100%;">
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:Label ID="Label3" runat="server" Text="Departamento:"></asp:Label>

                                                                                    </td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:DropDownList ID="DdldeptoBusqueda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdldeptoBusqueda_SelectedIndexChanged" Width="183px">
                                                                                        </asp:DropDownList>

                                                                                    </td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:Label ID="Label4" runat="server" Text="Ciudad:"></asp:Label>

                                                                                    </td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:DropDownList ID="DdlCiudadBusqueda" runat="server" Enabled="False" Width="183px">
                                                                                        </asp:DropDownList>

                                                                                    </td>

                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:Label ID="Label5" runat="server" Text="Número de caso:"></asp:Label>

                                                                                    </td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:TextBox ID="TxtNodecaso" runat="server" Width="160px"></asp:TextBox>
                                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="TxtNodecaso" Enabled="True"></asp:FilteredTextBoxExtender>

                                                                                    </td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:Label ID="Label6" runat="server" Text="Estado de avance:"></asp:Label></td>
                                                                                    <td style="text-align: left">
                                                                                        <asp:DropDownList ID="DdlEstadoAvance" runat="server" Width="183px"></asp:DropDownList></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="text-align: right">
                                                                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Volver a la búsqueda básica" OnClick="LinkButton2_Click"></asp:LinkButton></td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                    <tr>
                                                                        <td style="text-align: right">
                                                                            <asp:ImageButton ID="ibtnFind2" runat="server" AlternateText="Aplicar filtros de busqueda" EnableTheming="True" ImageUrl="~/Images/btn_search.png" OnClick="ibtnFind2_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvCasos" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        EmptyDataText="No se han encontrado casos coincidentes." AllowPaging="True"
                                                        PageSize="5" OnPageIndexChanging="gvCasos_PageIndexChanging" OnRowCreated="gvCasos_RowCreated" ShowFooter="True" OnRowDataBound="gvCasos_RowDataBound" OnRowCommand="gvCasos_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HiddCaso" runat="server" Value='<%# Eval("NoDeCaso") %>' />
                                                                    <asp:HiddenField ID="HiddEsperaFecha" runat="server" Value='<%# Eval("fecha") %>' />
                                                                    <asp:ImageButton ID="ImgHitos" runat="server" ImageUrl="~/Images/hist.png" CommandName="IrAHitos" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Ver actividades" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="NoDeCaso" HeaderText="Número de caso" />
                                                            <asp:BoundField DataField="NombreCliente" HeaderText="Nombre del cliente" />
                                                            <asp:BoundField DataField="Nombre" HeaderText="Ciudad utilización" />
                                                            <asp:BoundField DataField="FechaInicioProceso" HeaderText="Fecha de inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:TemplateField HeaderText="Estado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgAvance" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <FooterStyle CssClass="FooterGrid" />

                                                    </asp:GridView>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style10">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style11">
                                                    <asp:Label ID="LCasoSeleccionado" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Gray"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="Gridhitos" runat="server" AutoGenerateColumns="False" Width="100%"
                                                        AllowPaging="false"
                                                        PageSize="40" ShowFooter="True" OnRowDataBound="Gridhitos_RowDataBound">
                                                        <Columns>
                                                            <asp:BoundField DataField="NombreActividad" HeaderText="Nombre de la actividad" />
                                                            <asp:BoundField DataField="FechaInicioActividad" HeaderText="Fecha de inicio" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                            <asp:BoundField DataField="FechaFinalizacionActividad" HeaderText="Fecha de Finalización" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                            <asp:BoundField DataField="HitoCumplido" HeaderText="Hito cumplido" />
                                                            <asp:TemplateField HeaderText="Estado">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgAvance2" runat="server" /><asp:HiddenField ID="HiddEsperaFecha2" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fecha") %>' />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="FooterGrid" />

                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lnkExport" runat="server" Font-Size="Small" CssClass="ClassExtra" Style="display: none;" OnClick="lnkExport_Click">Exportar a Excel</asp:LinkButton>
                                        </td>

                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="Panel_3" runat="server" HeaderText="Buscador">
                            <ContentTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <table style="width:70%">
                                                <tr>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Ingrese un número de cedula:" Font-Size="Small"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtidentificaDetalles" CssClass="NormalTextBox" runat="server" Height="20px"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtidentificaDetalles" Enabled="True">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                    <td >
                                                        <asp:ImageButton ID="ImageButton1" runat="server" AlternateText="Aplicar filtros de busqueda" EnableTheming="True" ImageUrl="~/Images/btn_search.png" OnClick="ImageButton1_Click"/>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">&nbsp;</td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 2%">&nbsp;</td>
                                        <td>
                                            <asp:LinkButton ID="lnkExportDetalle" Visible="False" runat="server" Font-Bold="False" Font-Size="Small" CssClass="ClassExtra" OnClick="lnkExportDetalle_Click">Exportar a Excel</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="pnaelPest2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 2%">&nbsp;</td>
                                                            <td style="align-content: center">
                                                                <asp:GridView ID="GvDetalleCaso" runat="server" AutoGenerateColumns="False" Font-Size="Smaller"
                                                                    EmptyDataText="No se han encontrado casos coincidentes." AllowPaging="True"
                                                                    PageSize="5" OnPageIndexChanging="GvDetalleCaso_PageIndexChanging" OnRowCreated="GvDetalleCaso_RowCreated" ShowFooter="True" OnRowDataBound="GvDetalleCaso_RowDataBound">
                                                                    <Columns>

                                                                        <asp:BoundField DataField="NoDeCaso" HeaderText="Número de caso" />
                                                                        <asp:BoundField DataField="NumeroDeTramite" HeaderText="Número de trámite" />
                                                                        <asp:BoundField DataField="DestinacionPrestamo" HeaderText="Destinación del préstamo" />
                                                                        <asp:BoundField DataField="NombreCliente" HeaderText="Nombre del primer afiliado" />
                                                                        <asp:BoundField DataField="NumeroIdentificacion" HeaderText="Identificación del primer afiliado" />
                                                                        <asp:BoundField DataField="NombreCliente2" HeaderText="Nombre del segundo afiliado" />
                                                                        <asp:BoundField DataField="NumeroIdentificacionCliente2" HeaderText="Identificación del segundo afiliado" />
                                                                        <asp:BoundField DataField="FechaInicioProceso" HeaderText="Fecha de recepción" DataFormatString="{0:dd/MM/yyyy}" />
                                                                        <asp:BoundField DataField="NombreActividad" HeaderText="Estado actual" />
                                                                        <asp:TemplateField HeaderText="Estado">
                                                                            <ItemTemplate>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td><asp:Label ID="txtEstadoAct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NombreActividad") %>' /></td>
                                                                                        <td><asp:ImageButton ID="ImgAvance" runat="server" /></td>
                                                                                    </tr>
                                                                                </table> 
                                                                                <asp:HiddenField ID="HiddCaso" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "NoDeCaso") %>' />
                                                                                <asp:HiddenField ID="HiddEsperaFecha" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "fecha") %>' />                                                 
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>

                    <div class="shadow_bottom_tab">&nbsp;</div>
                </div>
            </div>
        </div>
        <div class="shadow_bottom">
            <div class="shadow_bottom_l">&nbsp;</div>
            <div class="shadow_bottom_r">&nbsp;</div>
        </div>
    </div>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfIdAvance"
        PopupControlID="pnlChart" CancelControlID="btnCancelEnt" BackgroundCssClass="modalBackground" />

    <asp:Panel ID="pnlChart" runat="server" Style="display: block; width: 750px; height: 550px; background-color: White; border-width: 2px; border-color: Black; border-style: solid;" ScrollBars="Auto">

        <table style="width: 100%;">
            <tr>
                <td>
                    <uc1:UCDetails ID="UCDetails1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:HiddenField ID="hfIdAvance" runat="server" />
                                <asp:HiddenField ID="hfIdEntregable" runat="server" />
                            </td>
                            <td style="text-align: center">
                                <asp:Button ID="btnCancelEnt" runat="server" Text="Cerrar" Width="80px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </asp:Panel>
</asp:Content>
