<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccesoDenegado.aspx.cs" Inherits="AccesoDenegado"  MasterPageFile="~/Site.master"%>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="Server">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Acceso denegado</h3>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="InicioContent" runat="Server">
    <table style="width:100%">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:center"> 
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/AccesoDenegado.png" Width="200px" />
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Label ID="lError" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Gray" Text="Usted no tiene acceso a este sitio"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>