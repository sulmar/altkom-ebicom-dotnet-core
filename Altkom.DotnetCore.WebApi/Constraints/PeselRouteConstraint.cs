using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.WebApi.Constraints
{
    public class PeselRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object routeValue))
            {
                string pesel = routeValue.ToString();

                return pesel.Length == 13;
            }
            else
                return false;
        }
    }
}
