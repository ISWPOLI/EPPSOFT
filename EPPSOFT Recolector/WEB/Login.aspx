<%@ Page Title="eppSoft Recolector - Ingresar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/UserControl/CrearUsuario.ascx" TagName="UCCrear" TagPrefix="ucCrear" %>
<%@ Register Src="~/UserControl/RecordarContraseña.ascx" TagName="UCRecordar" TagPrefix="ucRecordar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="InicioContent">
    <div class="wrapper_title">
        <div class="section_title">
            <h3>Acceso</h3>
        </div>
    </div>
    <div id="main_hitos">
        <div class="shadow_top">
            <div class="shadow_top_l">&nbsp;</div>
            <div class="shadow_top_r">&nbsp;</div>
        </div>
        <div class="main_right">
            <div class="main_left">
                <div id="main_content_hitos">
                    <section id="loginForm">
                        <asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_LoggedIn"
                            FailureText="No es posible iniciar sesión." ViewStateMode="Disabled" RenderOuterTable="false" Style="width: 500px">
                            <LayoutTemplate>
                                <p class="validation-summary-errors">
                                    <asp:Literal runat="server" ID="FailureText" />
                                </p>
                                <fieldset>
                                    <legend>Credenciales de acceso:</legend>
                                    <ol>
                                        <li>
                                            <asp:Label ID="Label1" runat="server" AssociatedControlID="UserName">Usuario:</asp:Label>
                                            <asp:TextBox CssClass="NormalTextBox" runat="server" ID="UserName" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="*" />
                                        </li>
                                        <li>
                                            <asp:Label ID="Label2" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                                            <asp:TextBox CssClass="NormalTextBox" runat="server" ID="Password" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="*" />
                                        </li>

                                    </ol>
                                    <table style="width:100%" >
                                        <tr>
                                            <td style="width:49%"> <asp:Button ID="Button1" runat="server" CommandName="Login" Text="Ingresar" /></td>
                                            <td style="width:2%"></td>
                                            <td style="width:49%"> <asp:Button ID="BtnCrearCuenta" runat="server" Text="Crear Cuenta" CausesValidation="false" OnClick="BtnCrearCuenta_Click" Enabled="False" /></td>
                                        </tr>
                                         <tr>
                                            <td colspan="3" class="auto-style6"></td>
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:LinkButton ID="LbtnRecordarPass" runat="server" Text="¿Olvidó su contraseña?" CssClass="ClassExtra" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" CausesValidation="false" OnClick="LbtnRecordarPass_Click" Enabled="False"></asp:LinkButton></td>
                                            
                                        </tr>

                                    </table>
                                   
                                   
                                </fieldset>
                            </LayoutTemplate>
                        </asp:Login>
                    </section>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
        <div class="shadow_bottom">
            <div class="shadow_bottom_l">&nbsp;</div>
            <div class="shadow_bottom_r">&nbsp;</div>
        </div>
    </div>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnCancelEnt" 
    PopupControlID="pnlChart" CancelControlID="btnCancelEnt" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="pnlChart" runat="server" Style="display: none; width: 700px; height: 220px; background-color: White; border-width: 2px; border-color: Black; border-style: solid;" ScrollBars="None">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ucCrear:UCCrear ID="UC1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center">
                                        
                                        <asp:LinkButton ID="btnCancelEnt" runat="server" Text="Cerrar (X)" Font-Bold="True" Font-Size="Small"></asp:LinkButton>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>

            </asp:Panel>

    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnCancelRec" 
    PopupControlID="pnlChart2" CancelControlID="btnCancelRec" BackgroundCssClass="modalBackground" />

            <asp:Panel ID="pnlChart2" runat="server" Style="display: none; width: 700px; height: 205px; background-color: White; border-width: 2px; border-color: Black; border-style: solid;" ScrollBars="None">

                <table style="width: 100%;">
                    <tr>
                        <td>
                            <ucRecordar:UCRecordar ID="UC2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center">
                                        
                                        <asp:LinkButton ID="btnCancelRec" runat="server" Text="Cerrar (X)" Font-Bold="True" Font-Size="Small"></asp:LinkButton>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>

            </asp:Panel>

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1
        {
            height: 10px;
        }
        .auto-style6
        {
            height: 9px;
            width: 10%;
        }
    </style>
</asp:Content>

