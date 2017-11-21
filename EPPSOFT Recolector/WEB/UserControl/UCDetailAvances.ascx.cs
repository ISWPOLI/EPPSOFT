using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using LogicaNegocio;

//class TerceroDetalleGrupo
//{
//    public int IdTercero { get; set; }
//    public List<Struct.Avance> detalle { get; set; }
//}
//class ProcesoDetalleGrupo
//{
//    public string Proceso { get; set; }
//    public List<Struct.Avance> detalle { get; set; }


/*class TerceroDetalle
{
    public int IdNodoProyecto { get; set; }
    public int IdTercero { get; set; }
    public string NombreTercero { get; set; }
    public string FechaReporte { get; set; }
    public string FechaRevision { get; set; }
    public byte IdEstadoAvance { get; set; }
    public DateTime FechaEjecucion { get; set; }
    public int IdProceso { get; set; }
    public string NombreProceso { get; set; }  
}*/

public partial class UserControl_UCDetailAvances : System.Web.UI.UserControl
{
    public int IdDepartamento { get; set; }
    public string NombreDepto { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IdDepartamento!=0)
        {
            lblNombre.Text = NombreDepto.ToUpper();
            DataTable dt = Herramientas.IList_A_DataTable((IList)LogicaNegocio.ReporteMapaBLL.ListadoCasosDeptos(IdDepartamento));

            double CasosT = dt.Rows.Count;
            lblCasos.Text =" " + CasosT.ToString() + " CASO(S) EN EJECUCIÓN.";

            double[] CasosC = new double[1];
            double[] CasosA1 = new double[1];
            double[] CasosA2 = new double[1];

            var q1 = (from T in dt.AsEnumerable()
                      where Convert.ToInt32(T.Field<string>("fecha")) <= 0
                      select new { T }).Count();
            
            CasosC[0] = Convert.ToDouble(q1);

            var q2 = (from T in dt.AsEnumerable()
                      where Convert.ToInt32(T.Field<string>("fecha")) > 0 && Convert.ToInt32(T.Field<string>("fecha")) <= 48
                      select new { T }).Count();

            CasosA1[0] = Convert.ToDouble(q2);

            var q3 = (from T in dt.AsEnumerable()
                      where Convert.ToInt32(T.Field<string>("fecha")) > 48
                      select new { T }).Count();

            CasosA2[0] = Convert.ToDouble(q3);

            double CasosCP, CasosA1P, CasosA2P;

            CasosCP = (CasosC[0] / CasosT) * 100;
            CasosA1P = (CasosA1[0] / CasosT) * 100;
            CasosA2P = (CasosA2[0] / CasosT) * 100;

            if (CasosCP != 0)
            {
                Chart1.Series[0].Points[0].YValues = CasosC;
                Chart1.Series[0].Points[0].Label = Math.Round(CasosCP, 2) + "%";
                Chart1.Series[0].Points[0].LegendToolTip = CasosC[0].ToString() + " caso(s)";
                Chart1.Series[0].Points[0].LabelToolTip = CasosC[0].ToString() + " caso(s)";
            }
            if (CasosA1P != 0)
            {
                Chart1.Series[0].Points[1].YValues = CasosA1;
                Chart1.Series[0].Points[1].Label = Math.Round(CasosA1P, 2) + "%";
                Chart1.Series[0].Points[1].LegendToolTip = CasosA1[0].ToString() + " caso(s)";
                Chart1.Series[0].Points[1].LabelToolTip = CasosA1[0].ToString() + " caso(s)";
            }
            if (CasosA2P != 0)
            {
                Chart1.Series[0].Points[2].YValues = CasosA2;
                Chart1.Series[0].Points[2].Label = Math.Round(CasosA2P, 2) + "%";
                Chart1.Series[0].Points[2].LegendToolTip = CasosA2[0].ToString() + " caso(s)";
                Chart1.Series[0].Points[2].LabelToolTip = CasosA2[0].ToString() + " caso(s)";
            }

            Chart1.ChartAreas[0].BackImage = "~/Images/Colombia/" + IdDepartamento.ToString() + ".jpg";
        }    
    }
}