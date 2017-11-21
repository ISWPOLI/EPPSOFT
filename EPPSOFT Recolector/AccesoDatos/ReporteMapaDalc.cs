using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos
{
    public class ReporteMapaDalc
    {
        EPPSOFTRecolectorEntities ContextoHitos;

        public IList ListadoMapa(CustomEntity oEntity)
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                int idDepartamento = oEntity.IdPrincipal;
                var query = new object();

                try
                {
                    query = (from D in ContextoHitos.Departamento
                             join Ci in ContextoHitos.Ciudad on D.IdDepartamento equals Ci.IdDepartamento
                             join Ca in ContextoHitos.Caso on Ci.Id_ciudad equals Ca.IdCiudadUtilizacion
                             where (idDepartamento == 0 || D.IdDepartamento == idDepartamento)
                             group Ca.IdCaso by new { D.IdDepartamento, D.Codigo, D.Nombre, D.Coordenadas } into g
                             select new
                             {
                                 g.Key.IdDepartamento,
                                 g.Key.Nombre,
                                 g.Key.Coordenadas,
                                 conteoCasos = g.Count()
                             }
                            ).ToList();
                }
                catch (InvalidOperationException e)
                {

                    throw;
                }

                return (IList)query;
            }
        }

        public IList ListadoCasosDeptos(int IdDepto)
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                var query = (from Ca in ContextoHitos.Caso
                             join RA in ContextoHitos.RegistroActividad on Ca.IdCaso equals RA.IdCaso
                             join A in ContextoHitos.Actividad on RA.IdActividad equals A.IdActividad
                             join Ci in ContextoHitos.Ciudad on Ca.IdCiudadUtilizacion equals Ci.Id_ciudad
                             join T1 in
                                 (
                                    from Ca1 in ContextoHitos.Caso
                                    join RA1 in ContextoHitos.RegistroActividad on Ca1.IdCaso equals RA1.IdCaso
                                    join A1 in ContextoHitos.Actividad on RA1.IdActividad equals A1.IdActividad
                                    join Ci1 in ContextoHitos.Ciudad on Ca1.IdCiudadUtilizacion equals Ci1.Id_ciudad
                                    where A1.EsHito == true && RA1.HitoFinalizado == true
                                    && Ci1.IdDepartamento == IdDepto
                                    group RA1 by Ca1.NoDeCaso into g
                                    select new
                                    {
                                        NoDeCaso = g.Key,
                                        fechaFinActiv = g.Max(x => x.FechaFinalizacionActividad)
                                    }
                                 ) on new { Ca.NoDeCaso, RA.FechaFinalizacionActividad } equals new { NoDeCaso = T1.NoDeCaso, FechaFinalizacionActividad=T1.fechaFinActiv }
                             where Ci.IdDepartamento == IdDepto
                             select new
                             {
                                 Ca.IdCaso,
                                 Ca.NoDeCaso,
                                 Ca.FechaInicioProceso,
                                 A.NombreActividad,
                                 FechaVenc = DbFunctions.AddHours(Ca.FechaInicioProceso ,A.DuracionActividad.Value),
                                 T1.fechaFinActiv,
                                 fecha = DbFunctions.DiffHours((DbFunctions.AddHours(Ca.FechaInicioProceso ,A.DuracionActividad.Value)), T1.fechaFinActiv)
                             }
                            ).ToList();

                return query;
                
            }
        }

        public List<Categoria> ListarCategorias()
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                var q = from c in ContextoHitos.Categoria
                        select c;
                return q.ToList();
            }
        }

        public IList ListadoCasosbusqueda(Int64 NumIdentificacion)
        {

            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                var query = (from Ca in ContextoHitos.Caso
                             join RA in ContextoHitos.RegistroActividad on Ca.IdCaso equals RA.IdCaso
                             join A in ContextoHitos.Actividad on RA.IdActividad equals A.IdActividad
                             join Ci in ContextoHitos.Ciudad on Ca.IdCiudadUtilizacion equals Ci.Id_ciudad
                             join Cl in ContextoHitos.Cliente on Ca.IdCliente equals Cl.IdCliente
                             join T1 in
                                 (
                                    from Ca1 in ContextoHitos.Caso
                                    join RA1 in ContextoHitos.RegistroActividad on Ca1.IdCaso equals RA1.IdCaso
                                    join A1 in ContextoHitos.Actividad on RA1.IdActividad equals A1.IdActividad
                                    join Cl1 in ContextoHitos.Cliente on Ca1.IdCliente equals Cl1.IdCliente
                                    where A1.EsHito == true && RA1.HitoFinalizado == true
                                    && (Cl1.NumeroIdentificacion == NumIdentificacion.ToString() || Cl1.NumeroIdentificacionCliente2 == NumIdentificacion.ToString())
                                    group RA1 by Ca1.NoDeCaso into g
                                    select new
                                    {
                                        NoDeCaso = g.Key,
                                        fechaFinActiv = g.Max(x => x.FechaFinalizacionActividad)
                                    }
                                 ) on new { Ca.NoDeCaso, RA.FechaFinalizacionActividad } equals new { NoDeCaso = T1.NoDeCaso, FechaFinalizacionActividad = T1.fechaFinActiv }
                             where (Cl.NumeroIdentificacion == NumIdentificacion.ToString() || Cl.NumeroIdentificacionCliente2 == NumIdentificacion.ToString())
                             select new
                             {
                                 Ca.IdCaso,
                                 Ca.NoDeCaso,
                                 Ca.FechaInicioProceso,
                                 A.NombreActividad,
                                 Cl.NombreCliente,
                                 FechaVenc = DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value),
                                 T1.fechaFinActiv,
                                 fecha = DbFunctions.DiffHours((DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value)), T1.fechaFinActiv)
                             }
                            ).ToList();

                return query;

            }
        }

        public IList ListadoCasosbusquedaAvanzada(string NodeCaso, string IdDepto, string IdCiudad, string Estado)
        {
            int id_ciudad = Convert.ToInt16(IdCiudad);
            int id_departamento = Convert.ToInt16(IdDepto);
            int estadobusq = Convert.ToInt16(Estado);

            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                var query = (from Ca in ContextoHitos.Caso
                             join RA in ContextoHitos.RegistroActividad on Ca.IdCaso equals RA.IdCaso
                             join A in ContextoHitos.Actividad on RA.IdActividad equals A.IdActividad
                             join Ci in ContextoHitos.Ciudad on Ca.IdCiudadUtilizacion equals Ci.Id_ciudad
                             join Cl in ContextoHitos.Cliente on Ca.IdCliente equals Cl.IdCliente
                             join T1 in
                                 (
                                    from Ca1 in ContextoHitos.Caso
                                    join RA1 in ContextoHitos.RegistroActividad on Ca1.IdCaso equals RA1.IdCaso
                                    join A1 in ContextoHitos.Actividad on RA1.IdActividad equals A1.IdActividad
                                    join Ci1 in ContextoHitos.Ciudad on Ca1.IdCiudadUtilizacion equals Ci1.Id_ciudad
                                    where A1.EsHito == true && RA1.HitoFinalizado == true
                                    &&  (NodeCaso == "" || Ca1.NoDeCaso == NodeCaso)
                                    && (id_departamento == 0 || Ci1.IdDepartamento == id_departamento)
                                    && (id_ciudad == 0 || Ca1.IdCiudadUtilizacion == id_ciudad)
                                    group RA1 by Ca1.NoDeCaso into g
                                    select new
                                    {
                                        NoDeCaso = g.Key,
                                        fechaFinActiv = g.Max(x => x.FechaFinalizacionActividad)
                                    }
                                 ) on new { Ca.NoDeCaso, RA.FechaFinalizacionActividad } equals new { NoDeCaso = T1.NoDeCaso, FechaFinalizacionActividad = T1.fechaFinActiv }
                             where (NodeCaso == "" || Ca.NoDeCaso == NodeCaso)
                                && (id_departamento == 0 || Ci.IdDepartamento == id_departamento)
                                && (id_ciudad == 0 || Ca.IdCiudadUtilizacion == id_ciudad)
                             select new
                             {
                                 Ca.IdCaso,
                                 Ca.NoDeCaso,
                                 Ca.FechaInicioProceso,
                                 A.NombreActividad,
                                 Cl.NombreCliente,
                                 FechaVenc = DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value),
                                 T1.fechaFinActiv,
                                 fecha = DbFunctions.DiffHours((DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value)), T1.fechaFinActiv)
                             }
                            ).ToList();
                
                switch (estadobusq)
                {
                    case 1:
                        query = query.Where(x => x.fecha > 48).ToList();
                        break;
                    case 2:
                        query = query.Where(x => x.fecha > 0 && x.fecha <= 48).ToList();
                        break;
                    case 3:
                        query = query.Where(x => x.fecha <= 0).ToList();
                        break;
                }
                return query;

            }
        }

        public IList ListadohitosXCaso(string NoCaso)
        {
            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                var query = (from RA in ContextoHitos.RegistroActividad
                             join Ca in ContextoHitos.Caso on RA.IdCaso equals Ca.IdCaso
                             join A in ContextoHitos.Actividad on RA.IdActividad equals A.IdActividad
                             where Ca.NoDeCaso == NoCaso
                             && A.EsHito == true
                             select new
                             {
                                 A.NombreActividad,
                                 RA.FechaInicioActividad,
                                 RA.FechaFinalizacionActividad,
                                 HitoCumplido = RA.HitoFinalizado == true ? "SI": "NO",
                                 fecha = DbFunctions.DiffHours((DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value)), RA.FechaFinalizacionActividad)
                             }
                            ).ToList();

                return query.OrderBy(e => e.FechaInicioActividad).ToList();
            }
        }

        public IList ListadoCasosbusquedaXCaso()
        {

            using (ContextoHitos = new EPPSOFTRecolectorEntities())
            {
                ContextoHitos.Database.CommandTimeout = 5000;
                var query = (from Ca in ContextoHitos.Planilla

                             select new
                             {
                                 /* Ca.IdCaso,
                                  Ca.NoDeCaso,
                                  Ca.FechaInicioProceso,
                                  A.NombreActividad,
                                  Cl.NombreCliente,
                                  Cl.NombreCliente2,
                                  Ca.DestinacionPrestamo,
                                  Cl.NumeroIdentificacion,
                                  Cl.NumeroIdentificacionCliente2,
                                  Ca.NumeroDeTramite,
                                  FechaVenc = DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value),
                                  T1.fechaFinActiv,
                                  fecha = DbFunctions.DiffHours((DbFunctions.AddHours(Ca.FechaInicioProceso, A.DuracionActividad.Value)), T1.fechaFinActiv)*/

                                 Ca.iCodigoPlanilla,
                                 Ca.dFechaRecolección,
                                 Ca.iReportadoUsuarioKg,
                                 Ca.iRecolectadoKg,
                                 Ca.sRecipientes,
                                 Ca.bEncontrados,
                                 Ca.bRecogidos,
                             }
                            ).ToList();

                return query;

            }
        }

    }
}
