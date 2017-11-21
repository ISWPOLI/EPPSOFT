using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

public partial class UserControl_CrearUsuario : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void BtnCrear_Click(object sender, EventArgs e)
    {
        string nombreUs = "";
        string cedulaUs = "";
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
                if (!LogicaNegocio.ClientesBLL.UsuarioExistente(txtCedula.Text))
                {

                    switch (rblTipo.SelectedValue)
                    {
                        case "Afiliado":

                           Cliente Cliente1 = LogicaNegocio.ClientesBLL.ValidarCreacionCliente(txtCedula.Text);

                            if (Cliente1 == null)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('No se han realizado solicitudes de crédito con este número de cédula');", true);
                            }
                            else
                            {
                                if (Cliente1.CorreoElectronico == null || Cliente1.CorreoElectronico == string.Empty)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('No hay un correo electrónico registrado para este número de cédula');", true);
                                }
                                else
                                {
                                    if (txtCedula.Text == Cliente1.NumeroIdentificacion)
                                    {
                                        nombreUs = Cliente1.NombreCliente;
                                        cedulaUs = Cliente1.NumeroIdentificacion;
                                    }
                                    else
                                    {
                                        nombreUs = Cliente1.NombreCliente2;
                                        cedulaUs = Cliente1.NumeroIdentificacionCliente2;
                                    }

                                    CustomMembershipProvider NewCustom = new CustomMembershipProvider();

                                    LogicaNegocio.ClientesBLL.CrearUsuario(Cliente1.CorreoElectronico, NewCustom.CodificarContraseña(ContraseñaUs)
                                                      , nombreUs, cedulaUs,2);



                                    string envioCorreo = Tools.EnviarCorreo(Cliente1.CorreoElectronico, nombreUs, cedulaUs, ContraseñaUs, "NuevoUsuario");

                                    switch (envioCorreo)
                                    {
                                        case "Correcto":
                                            string[] correos = Tools.correosRegistrados(Cliente1.CorreoElectronico);
                                            foreach (string word in correos)
                                            {
                                                CorreoMensaje += "xxxxx" + word.Substring(4, word.Length - 4) + ", ";
                                            }
                                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Bienvenido(a) " + nombreUs + ".\\n\\n Se ha enviado la contraseña asignada a los siguientes correos: " + CorreoMensaje + "');", true);
                                            break;
                                        default:
                                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('Ocurrió un error enviando correo de notificación de creación de cuenta.\\n\\n Por favor comuníquese con la linea de atención al cliente');", true);
                                            break;
                                    }

                                }
                            }

                            break;

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('Ya existe un usuario registrado con este número de cédula o Nit');", true);
                }
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('Ocurrió un error realizando la creación de cuenta.\\n\\n Por favor comuníquese con la linea de atención al cliente');", true);
        }
        
    }
}