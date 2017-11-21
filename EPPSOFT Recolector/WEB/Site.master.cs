using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using LogicaNegocio;
using Entidades;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }

        Modelo modelo = ((BasePage)this.Page).Modelo;
        if (modelo.Usuario == null
            && ((System.Web.UI.TemplateControl)(this.Page)).AppRelativeVirtualPath != "~/Login.aspx")
        {
            Response.Redirect("~/Login.aspx");
            lNombre.Text = string.Empty;
        }
        else
        {
            try
            {
                lNombre.Text = "Bienvenido(a): " + modelo.Usuario.NombreUsuario;
            }
            catch (Exception)
            {
                lNombre.Text = string.Empty;
            }
            
        }
        if (((System.Web.UI.TemplateControl)(this.Page)).AppRelativeVirtualPath == "~/Login.aspx")
        {
            lNombre.Text = string.Empty;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void mMenu_DataBound(object sender, EventArgs e)
    {
        foreach (MenuItem item in mMenu.Items)
            item.Selectable = false;

        if (((Modelo)Session["Modelo"]).Usuario != null)
        {
            navigation.Style["Display"] = "";

            switch (((Modelo)Session["Modelo"]).Usuario.uEntity.Perfil.NombrePerfil)
            {
                case "Reporteador":
                    mMenu.Items[5].Selectable = true;
                    break;
                case "UsuarioCliente":
                    mMenu.Items[3].Selectable = true;
                    break;
                case "UsuarioRecolector":
                    mMenu.Items[4].Selectable = true;
                    break;
            }
            


            for (int i = mMenu.Items.Count - 1; i >= 0; i--)
            {
                if (!mMenu.Items[i].Selectable)
                    mMenu.Items.Remove(mMenu.Items[i]);
            }

        }

    }
}