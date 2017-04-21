using Homely.CustomRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homely
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

           

            

            //routes.Add("ProductDetails", new SeoFriendlyRoute("{id}",
            //new RouteValueDictionary(new { controller = "Home", action = "Details", id = UrlParameter.Optional }),
            //new Mvc_RouteHandler()));



            routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            //routes.MapRoute


            //RouteValueDictionary defaults = new RouteValueDictionary();
            //defaults.Add("action", "Index");
            //defaults.Add("id", UrlParameter.Optional);
            //var customRoute = new Route("{controller}/{action}/{id}", defaults, new MvcRouteHandler());
            //routes.Add(customRoute);





        }
    }
}
