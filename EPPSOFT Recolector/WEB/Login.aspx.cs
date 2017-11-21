using System;
using System.Collections.Generic;
using System.Web.Security;
using Entidades;
using System.Linq;
using LogicaNegocio;

public partial class Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Modelo.Usuario != null)
        {
            this.Modelo.Usuario = null;
            Session.Abandon();
        }
    }
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        try
        {
            Session.Clear();
            MembershipUser membershipUser = Membership.GetUser(Login1.UserName);
            if (membershipUser != null)
            {
                Usuario usuario = membershipUser.ProviderUserKey as Usuario;

                //Capturar perfiles del usuario
                CustomEntity entity = new CustomEntity() { IdPrincipal = usuario.IdUsuario };
                //List<PerfilUsuario> perfiles = this.SecurityFacade.GetPerfiles(entity);

                this.Modelo.Usuario = new UsuariosEntity()
                {
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreCompleto,
                    uEntity = usuario,
                    IP = Request.ServerVariables["REMOTE_ADDR"],
                };

                //redireccionar de acuerdo a su perfil
                switch (Modelo.Usuario.uEntity.Perfil.NombrePerfil)
                {
                    case "Reporteador":
                        Response.Redirect(@"Reports\Map.aspx");
                        break;
                    case "UsuarioCliente":
                        Response.Redirect(@"Casos/MisCasos.aspx");
                        break;
                    case "UsuarioRecolector":
                        Response.Redirect(@"Planilla/NuevaPlanilla.aspx");
                        break;
                }
                
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }
    protected void BtnCrearCuenta_Click(object sender, EventArgs e)
    {
       ModalPopupExtender1.Show();
    }
    protected void LbtnRecordarPass_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
}