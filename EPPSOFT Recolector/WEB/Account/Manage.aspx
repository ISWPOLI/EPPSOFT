<%@ Page Title="Hitos - Administrar Cuenta" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Account_Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="InicioContent" runat="server">
    <div class="wrapper_title">   
        <div class="section_title user_l">
            <h3><%: Title %></h3>
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
  
        <asp:PlaceHolder runat="server" ID="successMessage" ViewStateMode="Disabled">
            <p class="message-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>

        <p>Está autenticado como <strong><%: User.Identity.Name %></strong>.</p>

        <asp:PlaceHolder runat="server" ID="changePassword">
            <h3>Cambiar contraseña</h3>
            <asp:ChangePassword ID="ChangePassword1" runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled" RenderOuterTable="false" SuccessPageUrl="Manage.aspx?m=ChangePwdSuccess" SuccessText="La contraseña ha sido cambiada">
                <ChangePasswordTemplate>
                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <fieldset class="changePassword">
                        <legend>Change password details</legend>
                        <ol>
                            <li>
                                <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Contraseña actual</asp:Label>
                                <asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="field-validation-error" ErrorMessage="*" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewPassword"
                                    CssClass="field-validation-error" ErrorMessage="*" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Confirme nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry" TextMode="Password" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="*" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La nueva contraseña y su confirmación no coinciden." />
                            </li>
                        </ol>
                        <asp:Button ID="Button1" runat="server" CommandName="ChangePassword" Text="Cambiar contraseña" />
                    </fieldset>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </asp:PlaceHolder>
                </div>
            </div>
        </div>
        <div class="shadow_bottom">
			<div class="shadow_bottom_l">&nbsp;</div>
			<div class="shadow_bottom_r">&nbsp;</div>
		</div>
    </div>
    
</asp:Content>
