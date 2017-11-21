<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecordarContraseña.ascx.cs" Inherits="UserControl_RecordarContraseña" %>
<link href="../Content/Site.css" rel="stylesheet" type="text/css" />

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
    .auto-style1
    {
        width: 10%;
        height: 37px;
    }
    .auto-style2
    {
        height: 37px;
    }
    .auto-style3
    {
        height: 42px;
    }
    .auto-style4
    {
        width: 10%;
        height: 30px;
    }
    .auto-style5
    {
        height: 30px;
    }
</style>

<table style="width: 100%">
    <tr>
        <td colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td class="auto-style1"></td>
        <td colspan="3" style="text-align:center; background-color:#00385D; " class="auto-style2">
            <asp:Label ID="lblNombre" CssClass="TituloDetalle" runat="server" Text="Asignar nueva contraseña"></asp:Label>
        </td>

        <td class="auto-style1"></td>
    </tr>
    <tr>
        <td class="auto-style4"></td>
        <td colspan="3" class="auto-style5"></td>

        <td class="auto-style4"></td>
    </tr>
    <tr>
        <td class="auto-style3"></td>
        <td style="vertical-align: middle;" class="auto-style3">
            <asp:Label ID="Label1" runat="server" Text="Ingrese su número de cedula o Nit:" Font-Size="Small"></asp:Label>
        </td>
        <td style="vertical-align: bottom;" class="auto-style3">
            <asp:TextBox ID="txtCedula" CssClass="NormalTextBox" runat="server" Height="20px"></asp:TextBox>
            <asp:FilteredTextBoxExtender ID="ftbeNumeros" runat="server" FilterType="Numbers" TargetControlID="txtCedula" Enabled="True">
            </asp:FilteredTextBoxExtender>
        </td>
        <td class="auto-style3">
            <asp:Button ID="BtnCrear" runat="server" Text="Asignar" CssClass="shadow_bottom" CausesValidation="False" OnClick="BtnCrear_Click"/>
        </td>
        <td class="auto-style3"></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td colspan="3">&nbsp;</td>

        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">&nbsp;</td>
    </tr>
</table>
