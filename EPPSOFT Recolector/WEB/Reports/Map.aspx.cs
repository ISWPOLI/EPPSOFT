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

public partial class Reports_Map : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Modelo.Usuario.uEntity.Perfil.NombrePerfil == "Reporteador")
        {

            GMap.addControl(new GControl(GControl.preBuilt.LargeMapControl));

            GMap.addControl(new GControl(GControl.preBuilt.MapTypeControl));

            if (!this.IsPostBack)
            {

                cargarCombos();
                CargarMapa();
                TabContainer1.Visible = true;

                string value = Request.QueryString["IdDepto"];

                if (value != null)
                {
                    string[] values = Regex.Split(value, "-");
                    int IdDepto = Convert.ToInt16(values[0]);
                    string NombreDepto = values[1];
                    UCDetails1.IdDepartamento = IdDepto;
                    UCDetails1.NombreDepto = NombreDepto;
                    ModalPopupExtender1.Show();
                }
            }

        }
        else
        {
            Response.Redirect("~/AccesoDenegado.aspx");
        }
    }


    private void CargarMapa(CustomEntity oEntity = null)
    {
        DataTable dt = null;

        if (Session["dataMapa"] != null)
        {
            dt = (DataTable)Session["dataMapa"];
        }

        GMap.reset();
        GMap.enableDragging = true;
        GMap.BackColor = Color.White;
        GMap.setCenter(new GLatLng(5, 285), 6);
        GMap.enableDoubleClickZoom = false;
        GMap.enableHookMouseWheelToZoom = false;
        //GMap.enableContinuousZoom = false;
        //GMap.enableScrollWheelZoom = false;
        GMap.mapType = GMapType.GTypes.Satellite;
        GMap.Add(GMapType.GTypes.Physical);
        GMap.Add(new GControl(GControl.preBuilt.MapTypeControl));
        GMap.enableRotation = true;
        GMap.Add(new GMapUI());

        //cargar categorias de marcadores en el mapa
        List<Categoria> listCategorias = LogicaNegocio.ReporteMapaBLL.ListarCategorias();


        if (oEntity != null)
        {
            dt = Herramientas.IList_A_DataTable((IList)LogicaNegocio.ReporteMapaBLL.ListadoMapa(oEntity));
            Session["dataMapa"] = dt;
        }

        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {

                DataTable dt2 = Herramientas.IList_A_DataTable((IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosDeptos(Convert.ToInt16(dr["IdDepartamento"])));

                var q2 = dt2.AsEnumerable()
                 .Select(x => Convert.ToInt32(x.Field<string>("fecha")))
                 .DefaultIfEmpty(0)
                 .Max(x => x);

                Int32 avance = Convert.ToInt32(q2);

                Int32 CasosT = dt2.Rows.Count;

                var q3 = (from T in dt2.AsEnumerable()
                          where Convert.ToInt32(T.Field<string>("fecha")) <= 0
                          select new { T }).Count();

                Int32 CasosC = Convert.ToInt32(q3);

                double Porcentaje = Convert.ToDouble(Convert.ToDouble(CasosC) / Convert.ToDouble(CasosT)) * 100;

                //obtener la categoria correspondiente al avance
                Categoria categ = listCategorias.Where(x => x.ValorMinimo <= avance && avance <= x.ValorMaximo).FirstOrDefault();

                string coord = dr["Coordenadas"].ToString();

                if (!string.IsNullOrEmpty(coord) && coord.Contains(","))
                {
                    Color color = Color.Black;

                    if (categ != null)
                    {
                        string strColor = categ.Color;
                        color = System.Drawing.ColorTranslator.FromHtml("#" + strColor);
                    }

                    PinIcon pinIcon = new PinIcon(PinIcons.location, color);
                    GIcon icono = new GIcon(pinIcon.ToString(), pinIcon.Shadow());

                    string slatitud = coord.Split(',')[0];
                    string slongitud = coord.Split(',')[1];

                    double latitud = double.Parse(slatitud.Replace(".", ","));
                    double longitud = double.Parse(slongitud.Replace(".", ","));

                    if (-90 > latitud || latitud > 90) latitud = double.Parse(slatitud);
                    if (-180 > longitud || longitud > 180) longitud = double.Parse(slongitud);


                    GMarker marker = new GMarker(new GLatLng(latitud, longitud), new GMarkerOptions(icono));
                    string strMarker = "<div id='pointNodo' style='width: 165px; height: 55px'><b>" +
                     "<span style='color:#ff7e00'>" + dr["Nombre"].ToString().ToUpper() + "</span></b>" +
                     "<br><b> Número de Casos: " + dr["conteoCasos"].ToString() +
                     "</b><br>Cumplimiento: " + Math.Round(Porcentaje, 2) + "%" +
                     "<br /><a  href='?IdDepto=" + dr["IdDepartamento"] + "-" + dr["Nombre"] + "'>Ver más</a> </div>";

                    HyperLink hyp = new HyperLink();
                    hyp.ID = "hypABD";
                    hyp.NavigateUrl = "";
                    Page.Controls.Add(hyp);

                    GInfoWindow window = new GInfoWindow(marker, strMarker, false, GListener.Event.mouseover);
                    GMap.Add(window);
                }
            }
        }
    }

    private void cargarCombos()
    {
        ddlDepartamentos.DataSource = LogicaNegocio.ListasBLL.ListarDepartamentos();
        ddlDepartamentos.DataTextField = "Nombre";
        ddlDepartamentos.DataValueField = "IdDepartamento";
        ddlDepartamentos.DataBind();
        ddlDepartamentos.Items.Insert(0, new ListItem("TODOS", "0"));

        DdldeptoBusqueda.DataSource = LogicaNegocio.ListasBLL.ListarDepartamentos();
        DdldeptoBusqueda.DataTextField = "Nombre";
        DdldeptoBusqueda.DataValueField = "IdDepartamento";
        DdldeptoBusqueda.DataBind();
        DdldeptoBusqueda.Items.Insert(0, new ListItem("Seleccione...", "0"));

        DdlCiudadBusqueda.Items.Insert(0, new ListItem("Seleccione...", "0"));

        DdlEstadoAvance.DataSource = LogicaNegocio.ListasBLL.ListarCategorias();
        DdlEstadoAvance.DataTextField = "Nombre";
        DdlEstadoAvance.DataValueField = "IdCategoria";
        DdlEstadoAvance.DataBind();
        DdlEstadoAvance.Items.Insert(0, new ListItem("Seleccione...", "0"));

        ddlDepartamentos_SelectedIndexChanged(null, null);
    }

    protected void ibtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CustomEntity oCustom = new CustomEntity();
            oCustom.IdPrincipal = int.Parse(ddlDepartamentos.SelectedValue);
            CargarMapa(oCustom);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        PbusquedaAvanzada.Visible = true; PbusquedaAvanzada.Enabled = true;
        PbusquedaBasica.Visible = false; PbusquedaBasica.Enabled = false;
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        PbusquedaAvanzada.Visible = false; PbusquedaAvanzada.Enabled = false;
        PbusquedaBasica.Visible = true; PbusquedaBasica.Enabled = true;
    }
    protected void DdldeptoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdldeptoBusqueda.SelectedValue != "0")
        {
            DdlCiudadBusqueda.Enabled = true;
            int deptoSelect = Convert.ToInt16(DdldeptoBusqueda.SelectedValue);

            /*DdlCiudadBusqueda.DataSource = LogicaNegocio.ListasBLL.ListarCiudades(deptoSelect);
            DdlCiudadBusqueda.DataTextField = "Nombre";
            DdlCiudadBusqueda.DataValueField = "Id_ciudad";
            DdlCiudadBusqueda.DataBind();
            DdlCiudadBusqueda.Items.Insert(0, new ListItem("Seleccione...", "0"));*/
        }
        else
        {
            DdlCiudadBusqueda.Items.Clear();
            DdlCiudadBusqueda.Items.Insert(0, new ListItem("Seleccione...", "0"));
            DdlCiudadBusqueda.Enabled = false;
        }
    }
    protected void ibtnFind2_Click(object sender, ImageClickEventArgs e)
    {
        int buscar = 1;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", @"document.getElementById('" + lnkExport.ClientID + "').style.display = 'none';", true);
        Gridhitos.DataSource = null;
        Gridhitos.DataBind();
        LCasoSeleccionado.Text = "";

        switch (PbusquedaBasica.Visible)
        {
            case true:
                if (TxtCedula.Text == string.Empty)
                {
                    buscar = 0;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Debe ingresar un número de cédula');", true);
                }

                break;
            case false:
                if (TxtNodecaso.Text == string.Empty && DdlCiudadBusqueda.SelectedValue == "0" && DdldeptoBusqueda.SelectedValue == "0" && DdlEstadoAvance.SelectedValue == "0")
                {
                    buscar = 0;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", @"alert('Debe seleccionar un criterio de búsqueda como mínimo!');", true);
                }
                else
                {
                    buscar = 2;
                }
                break;
        }

        switch (buscar)
        {
            case 1:
                this.Modelo.CurrentObject2 = (IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosBusqueda(Convert.ToInt64(TxtCedula.Text));
                gvCasos.DataSource = this.Modelo.CurrentObject2;
                break;
            case 2:
                this.Modelo.CurrentObject2 = (IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosBusquedaAvanzada(TxtNodecaso.Text, DdldeptoBusqueda.SelectedValue, DdlCiudadBusqueda.SelectedValue, DdlEstadoAvance.SelectedValue);
                gvCasos.DataSource = this.Modelo.CurrentObject2;
                break;
        }
        gvCasos.DataBind();
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
        try
        {
            HiddenField valoractual = (HiddenField)e.Row.FindControl("HiddEsperaFecha");
            int valact = Convert.ToInt16(valoractual.Value);
            ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletWhite32.png";
            if (valact <= 0)
            {
                ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletGreen32.png";
            }
            else
            {
                if (valact > 0 & valact <= 48)
                {
                    ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletYellow32.png";
                }
                else
                {
                    if (valact > 48)
                    {
                        ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletRed32.png";
                    }
                }
            }
        }
        catch (Exception)
        {
            try
            {
                ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletWhite32.png";

            }
            catch (Exception)
            {
            }
        }


    }
    protected void gvCasos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "IrAHitos")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCasos.Rows[index];

            string Caso = ((HiddenField)row.FindControl("HiddCaso")).Value;

            LCasoSeleccionado.Text = "CASO " + Caso + ".";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", @"document.getElementById('" + lnkExport.ClientID + "').style.display = 'inherit';", true);
            this.Modelo.CurrentObject3 = (IList)LogicaNegocio.ReporteMapaBLL.ListadohitosXCaso(Caso);
            Gridhitos.DataSource = this.Modelo.CurrentObject3;
            Gridhitos.DataBind();
            Gridhitos.Focus();
        }
    }
    protected void Gridhitos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField valoractual = (HiddenField)e.Row.FindControl("HiddEsperaFecha2");
            int valact = Convert.ToInt16(valoractual.Value);
            ((ImageButton)e.Row.FindControl("ImgAvance2")).ImageUrl = "~/Images/BulletWhite32.png";
            if (valact <= 0)
            {
                ((ImageButton)e.Row.FindControl("ImgAvance2")).ImageUrl = "~/Images/BulletGreen32.png";
            }
            else
            {
                if (valact > 0 & valact <= 48)
                {
                    ((ImageButton)e.Row.FindControl("ImgAvance2")).ImageUrl = "~/Images/BulletYellow32.png";
                }
                else
                {
                    if (valact > 48)
                    {
                        ((ImageButton)e.Row.FindControl("ImgAvance2")).ImageUrl = "~/Images/BulletRed32.png";
                    }
                }
            }
        }
        catch (Exception)
        {
            try
            {
                ((ImageButton)e.Row.FindControl("ImgAvance2")).ImageUrl = "~/Images/BulletWhite32.png";

            }
            catch (Exception)
            {
            }

        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {

        ExportExcel(Gridhitos, this.Modelo.CurrentObject3 as IList);
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
    protected void lnkExportDetalle_Click(object sender, EventArgs e)
    {
        ExportExcel(GvDetalleCaso, this.Modelo.CurrentObject4 as IList);
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        if (txtidentificaDetalles.Text == string.Empty)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Error", @"alert('El campo Número de cédula es obligatorio');", true);
            lnkExportDetalle.Visible = false;
        }
        else
        {

            
            this.Modelo.CurrentObject4 = (IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosbusquedaXCaso();
            if (((IList)this.Modelo.CurrentObject4).Count != 0)
            {
                lnkExportDetalle.Visible = true;
            }
            else
            {
                lnkExportDetalle.Visible = false;
            }
            GvDetalleCaso.DataSource = this.Modelo.CurrentObject4;
            GvDetalleCaso.DataBind();
        }
    }
    protected void GvDetalleCaso_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvDetalleCaso.DataSource = this.Modelo.CurrentObject4;
        GvDetalleCaso.PageIndex = e.NewPageIndex;
        GvDetalleCaso.DataBind();
    }
    protected void GvDetalleCaso_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].ColumnSpan = 4;
            int totalRegistros = 0;
            IList ds = this.Modelo.CurrentObject4 as IList;
            if (ds != null) totalRegistros = ds.Count;

            int size = GvDetalleCaso.AllowPaging ? GvDetalleCaso.PageSize : totalRegistros;
            int final = (GvDetalleCaso.PageIndex + 1) * GvDetalleCaso.PageSize;
            int inicial = final - size + 1;
            //puede cambiar final
            if (final > totalRegistros) final = totalRegistros;

            e.Row.Cells[0].Text = "Mostrando registros " + inicial + "-" + final + " de " + totalRegistros.ToString();
        }
    }
    protected void GvDetalleCaso_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField valoractual = (HiddenField)e.Row.FindControl("HiddEsperaFecha");
            int valact = Convert.ToInt16(valoractual.Value);
            ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletWhite32.png";
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
        {
            try
            {
                ((ImageButton)e.Row.FindControl("ImgAvance")).ImageUrl = "~/Images/BulletWhite32.png";

            }
            catch (Exception)
            {
            }
        }
    }
}