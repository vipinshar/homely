using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Homely.CustomRoute
{
    public class SeoFriendlyRoute : Route
    {
        public SeoFriendlyRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);

            if (routeData != null)
            {
                if (routeData.Values.ContainsKey("id"))
                    routeData.Values["id"] = GetIdValue(routeData.Values["id"]);
            }

            return routeData;
        }

        private object GetIdValue(object id)
        {
            if (id != null)
            {
                string idValue = id.ToString();
                if (!string.IsNullOrEmpty(idValue))
                {

                    var regex = new Regex(@"^(?<id>\d+).*$");
                    var match = regex.Match(idValue);

                    if (match.Success)
                    {
                        return match.Groups["id"].Value;
                    }
                }
            }

            return id;
        }
    }

  


    public class Mvc_RouteHandler : IRouteHandler
    {
        /// <summary>
        ///Use in case of mock testing
        /// </summary>
        private IControllerFactory _controllerFactory;

        public Mvc_RouteHandler()
        {
        }

        /// <summary>
        /// Inject into colntroller.
        /// </summary>
        /// <param name="controllerFactory"></param>
        public Mvc_RouteHandler(IControllerFactory controllerFactory)
        {
            _controllerFactory = controllerFactory;
        }

        protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //Dictionary<int, string> lst = new Dictionary<int, string>();
            //lst.Add(1, "abc");
            string s = requestContext.RouteData.Values["id"].ToString();
            string[] data = s.Split('/');//lst.Where(x => x.Value == requestContext.RouteData.Values["id"]).Select(x => x.Key).SingleOrDefault();
            if (data != null && data.Count() > 0)
            {
                requestContext.RouteData.Values["id"] = data;
            }
       
            return new MvcHandler(requestContext);
        }

        #region IRouteHandler Members

        IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
        {
            return GetHttpHandler(requestContext);
        }




        #endregion
    }
}