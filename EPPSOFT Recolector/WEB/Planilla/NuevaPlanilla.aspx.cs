using Entidades;
using LogicaNegocio;
using Subgurim.Controles;
using Subgurim.Controles.GoogleChartIconMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;

public partial class Casos_MisCasos : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Modelo.Usuario.uEntity.Perfil.NombrePerfil == "UsuarioRecolector")
        {
            if (Modelo.Usuario.uEntity.ContraseniaAsignada)
            {
                Response.Redirect("~/Account/Manage.aspx");
            }
            else
            {
                if (!IsPostBack)
                {

                    cargarListas();
                    Random rnd = new Random();
                    l_CodigoPlanilla.Text = Convert.ToString(rnd.Next(100, 999));
                    l_FechaHora.Text = DateTime.Now.ToString("dd ' de ' MMMM ' de ' yyyy hh:mm:ss tt");
                }
            }
        }
        else
        {
            Response.Redirect("~/AccesoDenegado.aspx");
        }
    }


    protected void gvCasos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /*gvCasos.DataSource = this.Modelo.CurrentObject2;
        gvCasos.PageIndex = e.NewPageIndex;
        gvCasos.DataBind();*/
    }

    protected void gvCasos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        /*if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 4;
            int totalRegistros = 0;
            IList ds = this.Modelo.CurrentObject2 as IList;
            if (ds != null) totalRegistros = ds.Count;

            int size = gvCasos.AllowPaging ? gvCasos.PageSize : totalRegistros;
            int final = (gvCasos.PageIndex + 1) * gvCasos.PageSize;
            int inicial = final - size + 1;
            //puede cambiar final
            if (final > totalRegistros) final = totalRegistros;

            e.Row.Cells[0].Text = "Mostrando registros " + inicial + "-" + final + " de " + totalRegistros.ToString();
        }*/
    }

    protected void cargarListas()
    {
        ddl_vehiculo.DataSource = LogicaNegocio.ListasBLL.ListarVehiculos();
        ddl_vehiculo.DataTextField = "sPlaca";
        ddl_vehiculo.DataValueField = "idVehiculo";
        ddl_vehiculo.DataBind();

        ddl_novedades.DataSource = LogicaNegocio.ListasBLL.ListarNovedadesServicio();
        ddl_novedades.DataTextField = "sNovedadServicio";
        ddl_novedades.DataValueField = "idNovedadServicio";
        ddl_novedades.DataBind();

        ddl_claseprod.DataSource = LogicaNegocio.ListasBLL.ListarClaseProductos();
        ddl_claseprod.DataTextField = "sClaseProducto";
        ddl_claseprod.DataValueField = "idClaseProducto";
        ddl_claseprod.DataBind();

        ddl_cliente.DataSource = LogicaNegocio.ListasBLL.ListarClientes();
        ddl_cliente.DataTextField = "NombreCompleto";
        ddl_cliente.DataValueField = "IdUsuario";
        ddl_cliente.DataBind();
    }

    protected void gvCasos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /*try
        {
            HiddenField valoractual = (HiddenField)e.Row.FindControl("HiddEsperaFecha");
            int valact = Convert.ToInt16(valoractual.Value);
            if (valact <= 0)
            {
                ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletGreen32.png";
                ((ImageButton)e.Row.FindControl("ImgAvance")).ToolTip = "Realizado a tiempo";
            }
            else
            {
                if (valact > 0 & valact <= 48)
                {
                    ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletYellow32.png";
                    ((ImageButton)e.Row.FindControl("ImgAvance")).ToolTip = "Realizado hasta con 48 horas de retraso";
                }
                else
                {
                    if (valact > 48)
                    {
                        ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletRed32.png";
                        ((ImageButton)e.Row.FindControl("ImgAvance")).ToolTip = "Realizado con más de 48 horas de retraso";
                    }
                }
            }
        }
        catch (Exception)
        { }*/
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    private void ExportExcel(GridView gv, IList ds)
    {
        /*StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        Page page = new Page();
        HtmlForm form = new HtmlForm();

        gv.EnableViewState = false;

        // Deshabilitar la validación de eventos, sólo asp.net 2
        page.EnableEventValidation = false;

        // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
        page.DesignerInitialize();

        page.Controls.Add(form);
        form.Controls.Add(gv);

        //To Export all pages
        gv.AllowPaging = false;

        gv.DataSource = ds;
        gv.DataBind();

        page.RenderControl(htw);

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=Export.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        Response.Write(sb.ToString());
        Response.End();*/
    }

    protected void btnCargarDocumentos_Click(object sender, EventArgs e)
    {
        /*try 
	    {	        
		    string encriptar = Tools.EncriptarUsuario(this.Modelo.Usuario.uEntity.NombreUsuario);
            string url = ConfigurationManager.AppSettings["CargarDocumentos"] + encriptar;

            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}','_blank');</script>", url));
	    }
	    catch (Exception)
	    {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Ocurrió un error redireccionando');", true);
	    }*/
              
    }

    protected void btn_RegistrarPlanilla_Click(object sender, EventArgs e)
    {

        int iCodigoPlanilla = Convert.ToInt16(l_CodigoPlanilla.Text);
        int idGenerador = Convert.ToInt16(ddl_cliente.SelectedValue);
        System.DateTime dFechaRecolección = DateTime.Now;
        int idVehiculoRecoleccion = Convert.ToInt16(ddl_vehiculo.SelectedValue);
        int iReportadoUsuarioKg = Convert.ToInt16(txt_reportadouser.Text);
        int iRecolectadoKg = Convert.ToInt16(txt_recolectado.Text);
        string sRecipientes = txt_recipientes.Text;
        bool bEncontrados = chb_encontrados.Checked;
        bool bRecogidos = chb_recogidos.Checked;
        string sNombreUsuarioEntrega =  txt_quienEntrega.Text;
        int idNovedadesServicio = Convert.ToInt16(ddl_novedades.SelectedValue);
        string sOtrasNovedades = txt_otrasNovedades.Text;
        int idClaseProducto =  Convert.ToInt16(ddl_claseprod.SelectedValue);

        PlanillasBLL.CrearPlanilla(iCodigoPlanilla, idGenerador, dFechaRecolección, idVehiculoRecoleccion, iReportadoUsuarioKg, iRecolectadoKg
            , sRecipientes, bEncontrados, bRecogidos, sNombreUsuarioEntrega, idNovedadesServicio, sOtrasNovedades, idClaseProducto);
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Planilla registrada exitosamente!');", true);
        Response.Redirect("NuevaPlanilla.aspx");
    }
}