<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NuevaPlanilla.aspx.cs" Inherits="Casos_MisCasos" MasterPageFile="~/Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }
        .auto-style2 {
            height: 40px;
            width: 25%;
        }
        .auto-style3 {
            height: 40px;
            width: 25%;
        }
        .auto-style4 {
            height: 40px;
            width: 25%;
        }
        .auto-style5 {
            width: 25%;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Nueva Planilla</h3>
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
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style2" >
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Código de la planilla:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:Label ID="l_CodigoPlanilla" runat="server" Text="Label"></asp:Label></td>
                        <td class="auto-style4"><asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Fecha y hora:"></asp:Label></td>
                        <td class="auto-style1"><asp:Label ID="l_FechaHora" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Vehículo recolector:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="ddl_vehiculo" runat="server" Width="80px">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4"><asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Clase de producto:"></asp:Label></td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="ddl_claseprod" runat="server" Width="80px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Reportado por Usuario Kg.:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txt_reportadouser" runat="server" Width="60px"></asp:TextBox>
                        </td>
                        <td class="auto-style4"><asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Recolectado en Kg.:"></asp:Label></td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txt_recolectado" runat="server" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Generador:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="ddl_cliente" runat="server" Width="90%"></asp:DropDownList>
                        </td>
                        <td class="auto-style5"><asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Recipiente:"></asp:Label></td>
                        <td><asp:TextBox ID="txt_recipientes" runat="server" Width="80%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Encontrados:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:CheckBox ID="chb_encontrados" runat="server" />
                        </td>
                        <td class="auto-style4"><asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Recogidos:"></asp:Label></td>
                        <td class="auto-style1">
                            <asp:CheckBox ID="chb_recogidos" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Novedades del servicio:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:DropDownList ID="ddl_novedades" runat="server" Width="90%">
                            </asp:DropDownList>
                        </td>
                        <td class="auto-style4"><asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Otras novedades:"></asp:Label></td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txt_otrasNovedades" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Nombre de quien entrega:"></asp:Label>
                        </td>
                        <td class="auto-style3">
                            <asp:TextBox ID="txt_quienEntrega" runat="server" Width="80%"></asp:TextBox>
                        </td>
                        <td class="auto-style5">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4"  align="center">
                            <asp:Button ID="btn_RegistrarPlanilla" runat="server" Text="Registrar Planilla" OnClick="btn_RegistrarPlanilla_Click" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
