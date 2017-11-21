<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CrearUsuario.ascx.cs" Inherits="UserControl_CrearUsuario" %>
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
            <asp:Label ID="lblNombre" CssClass="TituloDetalle" runat="server" Text="Creación de cuenta"></asp:Label>
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
            <asp:Label ID="Label1" runat="server" Text="Ingrese su número de cédula o Nit:" Font-Size="Small"></asp:Label>
        </td>
        <td style="vertical-align: bottom;" class="auto-style3">
            <asp:TextBox ID="txtCedula" CssClass="NormalTextBox" runat="server" Height="20px"></asp:TextBox>
            <asp:FilteredTextBoxExtender ID="ftbeNumeros" runat="server" FilterType="Numbers" TargetControlID="txtCedula" Enabled="True">
            </asp:FilteredTextBoxExtender>
        </td>
        <td class="auto-style3">
            <asp:Button ID="BtnCrear" runat="server" Text="Crear Cuenta" CssClass="shadow_bottom" CausesValidation="False" OnClick="BtnCrear_Click"/>
        </td>
        <td class="auto-style3"></td>
    </tr>
    <tr>
        <td class="auto-style3"></td>
        <td style="vertical-align: middle;" class="auto-style3">
            
        </td>
        <td style="vertical-align: central; text-align:center" class="auto-style3">
            
            <asp:RadioButtonList ID="rblTipo" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">Afiliado</asp:ListItem>
                <asp:ListItem>Constructora</asp:ListItem>
            </asp:RadioButtonList>
            
        </td>
        <td class="auto-style3">
            
        </td>
        <td class="auto-style3"></td>
    </tr>
    
    <tr>
        <td colspan="5">&nbsp;</td>
    </tr>
</table>
