using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
              name: "ImdbInfo",
              url: "ViewPage1/{title}",
              defaults: new { controller = "Home", action = "Index", title = "Green mile" }
          );

            routes.MapRoute(
          name: "generate",
          url: "Downloader/{getUrl}",
          defaults: new { controller = "Home", action = "Index", getUrl = "adres rapidu" }
      );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = "" }
                );
           
        }
    }
}
