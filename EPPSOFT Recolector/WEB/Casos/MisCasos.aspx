<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisCasos.aspx.cs" Inherits="Casos_MisCasos" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 50%;
            height: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Reporte De Planillas</h3>
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
                <table style="width: 100%" >
                    <tr>
                        <td style="width: 50%; vertical-align:bottom" >  <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" Font-Size="Small" CssClass="ClassExtra" OnClick="LinkButton1_Click">Exportar a Excel</asp:LinkButton>  </td>
                        <td style="width: 50%; text-align:right; align-content:flex-end">  <asp:Button ID="btnCargarDocumentos" runat="server" Text="CARGAR DOCUMENTOS" OnClick="btnCargarDocumentos_Click" Visible="False" />  </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" >&nbsp;</td>
                        <td class="auto-style1" >&nbsp;</td>
                    </tr>
                    
                </table>
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel ID="pnaelPest2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 5%">&nbsp;</td>
                                <td style="align-content: center">
                                    <asp:GridView ID="gvCasos" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="No se han encontrado casos coincidentes." AllowPaging="True"
                                        PageSize="5" OnPageIndexChanging="gvCasos_PageIndexChanging" OnRowCreated="gvCasos_RowCreated" ShowFooter="True" OnRowDataBound="gvCasos_RowDataBound">
                                        <Columns>

                                            <asp:BoundField DataField="iCodigoPlanilla" HeaderText="Código de la planilla" />
                                            <asp:BoundField DataField="dFechaRecolección" HeaderText="Fecha de recolección" />
                                            <asp:BoundField DataField="iReportadoUsuarioKg" HeaderText="Reportado por Usuario Kg." />
                                            <asp:BoundField DataField="iRecolectadoKg" HeaderText="Recolectado en Kg." />
                                            <asp:BoundField DataField="sRecipientes" HeaderText="Recipientes" />
                                            <asp:BoundField DataField="bEncontrados" HeaderText="¿Encontrados?" />
                                            <asp:BoundField DataField="bRecogidos" HeaderText="¿Recogidos?" />
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
</asp:Content>
