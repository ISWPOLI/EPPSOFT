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

public partial class Constructoras_ReporteConstructora : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Modelo.Usuario.uEntity.Perfil.NombrePerfil == "UsuarioConstructora")
        {
            if (Modelo.Usuario.uEntity.ContraseniaAsignada)
            {
                Response.Redirect("~/Account/Manage.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    /*ddlProyectos.DataSource = LogicaNegocio.ListasBLL.ListarProyectos(Convert.ToInt64(this.Modelo.Usuario.uEntity.NombreUsuario));
                    ddlProyectos.DataTextField = "Nombre";
                    ddlProyectos.DataValueField = "Nombre";
                    ddlProyectos.DataBind();
                    ddlProyectos.Items.Insert(0, new ListItem("TODOS", ""));*/

                    /*this.Modelo.CurrentObject2 = (IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosXConstructora(this.Modelo.Usuario.uEntity.NombreCompleto, ddlProyectos.SelectedValue);
                    gvCasos.DataSource = this.Modelo.CurrentObject2;
                    gvCasos.DataBind();*/
                   
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
        gvCasos.DataSource = this.Modelo.CurrentObject2;
        gvCasos.PageIndex = e.NewPageIndex;
        gvCasos.DataBind();
    }

    protected void gvCasos_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
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
        }
    }
    protected void gvCasos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DataTable dt = Tools.ConvertirADatatable(gvCasos);
        GridView gv1 = new GridView();
        ExportExcel(gvCasos, this.Modelo.CurrentObject2 as IList);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    private void ExportExcel(GridView gv, IList ds)
    {
        StringBuilder sb = new StringBuilder();
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
        Response.End();
    }
    protected void ibtnFind_Click(object sender, ImageClickEventArgs e)
    {
        /*this.Modelo.CurrentObject2 = (IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosXConstructora(this.Modelo.Usuario.uEntity.NombreCompleto, ddlProyectos.SelectedValue);
        gvCasos.DataSource = this.Modelo.CurrentObject2;
        gvCasos.DataBind();*/
    }
}