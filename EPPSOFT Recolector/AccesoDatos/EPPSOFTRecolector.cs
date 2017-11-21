using System.Web;

namespace Entidades
{
    public partial class EPPSOFTRecolectorEntities
    {
        public static EPPSOFTRecolectorEntities Context
        {
            get
            {
                string objectContextKey = HttpContext.Current.GetHashCode().ToString("x");
                if (!HttpContext.Current.Items.Contains(objectContextKey))
                {
                    HttpContext.Current.Items.Add(objectContextKey, new EPPSOFTRecolectorEntities());
                }
                return HttpContext.Current.Items[objectContextKey] as EPPSOFTRecolectorEntities;
            }
        }
    }
}