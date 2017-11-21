using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Manage : BasePage
{
    protected string SuccessMessage
    {
        get;
        private set;
    }

    protected void Page_Load()
    {
        if (Modelo.Usuario.uEntity.ContraseniaAsignada)
        {

            if (!IsPostBack)
            {
                // Render success message
                var message = Request.QueryString["m"];
                if (message != null)
                {
                    // Strip the query string from action
                    Form.Action = ResolveUrl("~/Account/Manage.aspx");

                    if (message == "ChangePwdSuccess")
                    {
                        Modelo.Usuario.uEntity.ContraseniaAsignada = false;
                    } 

                    SuccessMessage =
                        message == "ChangePwdSuccess" ? "La contraseña ha sido cambiada."
                        : message == "SetPwdSuccess" ? "Ha establecido contraseña."
                        : String.Empty;
                    successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                }
            }
        }
        else
        {
            Response.Redirect("~/Casos/MisCasos.aspx");
        }

    }

    protected static string ConvertToDisplayDateTime(DateTime? utcDateTime)
    {
        return utcDateTime.HasValue ? utcDateTime.Value.ToLocalTime().ToString("G") : "[never]";
    }
}