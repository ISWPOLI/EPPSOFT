using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

public partial class UserControl_RecordarContraseña : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnCrear_Click(object sender, EventArgs e)
    {

        string ContraseñaUs = LogicaNegocio.Herramientas.GenerarContraseña();
        string CorreoMensaje = "";

        try
        {

            if (txtCedula.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('El campo Número de cédula o Nit es obligatorio');", true);
            }
            else
            {
                Usuario User = new Usuario();

                User = LogicaNegocio.ClientesBLL.DatosUsuario(txtCedula.Text);

                if (User != null)
                {
                    CustomMembershipProvider NewCustom = new CustomMembershipProvider();

                    LogicaNegocio.ClientesBLL.NuevaClaveUsuario(txtCedula.Text, NewCustom.CodificarContraseña(ContraseñaUs));

                    string envioCorreo = Tools.EnviarCorreo(User.CorreoRegistro, User.NombreCompleto, User.NombreUsuario, ContraseñaUs, "OlvidoClave");

                    switch (envioCorreo)
                    {
                        case "Correcto":
                            string[] correos = Tools.correosRegistrados(User.CorreoRegistro);
                            foreach (string word in correos)
                            {
                                CorreoMensaje += "xxxxx" + word.Substring(4, word.Length - 4) + ", ";
                            }
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Bienvenido(a) " + User.NombreCompleto + ".\\n\\n Se ha enviado la contraseña asignada a los siguientes correos: " + CorreoMensaje + "');", true);
                            break;
                        default:
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('Ocurrió un error enviando correo de notificación de creación de cuenta.\\n\\n Por favor comuníquese con la linea de atención al cliente');", true);
                            break;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('No hay un usuario con este número de cédula o Nit');", true);
                }
            }
            
        }
        catch(Exception exc)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('Ocurrió un error asignando nueva contraseña.\\n\\n Por favor comuníquese con la linea de atención al cliente');", true);
        }

    }
}