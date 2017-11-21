using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Hitos.Business.Facade;
using Entidades;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page, IDisposable
{
    private Modelo _Modelo;

    public Modelo Modelo
    {
        get
        {
            if (Session["Modelo"] == null)
            {
                _Modelo = new Modelo();
                Session.Add("Modelo", _Modelo);
            }
            else
            {
                _Modelo = (Modelo)Session["Modelo"];
            }
            return _Modelo;
        }

        set
        {
            Session["Modelo"] = _Modelo;
        }
    }

    //private ProyectoFacade _ProyectoFacade;

    //public ProyectoFacade ProyectoFacade
    //{
    //    get
    //    {
    //        if (Session["ProyectoFacade"] == null)
    //        {
    //            _ProyectoFacade = new ProyectoFacade();
    //            Session.Add("ProyectoFacade", _ProyectoFacade);
    //        }
    //        else
    //        {
    //            _ProyectoFacade = (ProyectoFacade)Session["ProyectoFacade"];
    //        }
    //        return _ProyectoFacade;
    //    }

    //    set
    //    {
    //        Session["ProyectoFacade"] = _ProyectoFacade;
    //    }
    //}

    //private CommonFacade _CommonFacade;

    //public CommonFacade CommonFacade
    //{
    //    get
    //    {
    //        if (Session["CommonFacade"] == null)
    //        {
    //            _CommonFacade = new CommonFacade();
    //            Session.Add("CommonFacade", _CommonFacade);
    //        }
    //        else
    //        {
    //            _CommonFacade = (CommonFacade)Session["CommonFacade"];
    //        }
    //        return _CommonFacade;
    //    }

    //    set
    //    {
    //        Session["CommonFacade"] = _CommonFacade;
    //    }
    //}

    //private AdminFacade _AdminFacade;

    //public AdminFacade AdminFacade
    //{
    //    get
    //    {
    //        if (Session["AdminFacade"] == null)
    //        {
    //            _AdminFacade = new AdminFacade();
    //            Session.Add("AdminFacade", _AdminFacade);
    //        }
    //        else
    //        {
    //            _AdminFacade = (AdminFacade)Session["AdminFacade"];
    //        }
    //        return _AdminFacade;
    //    }

    //    set
    //    {
    //        Session["AdminFacade"] = _AdminFacade;
    //    }
    //}

    //private SecurityFacade _SecurityFacade;

    //public SecurityFacade SecurityFacade
    //{
    //    get
    //    {
    //        if (Session["SecurityFacade"] == null)
    //        {
    //            _SecurityFacade = new SecurityFacade();
    //            Session.Add("SecurityFacade", _SecurityFacade);
    //        }
    //        else
    //        {
    //            _SecurityFacade = (SecurityFacade)Session["SecurityFacade"];
    //        }
    //        return _SecurityFacade;
    //    }

    //    set
    //    {
    //        Session["SecurityFacade"] = _SecurityFacade;
    //    }
    //}

    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.Theme = "UTDisproel";
    }


}