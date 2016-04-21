using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.ActionConstraints;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Routing;

namespace Bodegas
{
    public class NamespaceConstraint : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dataTokenNamespace = (string)context.RouteData.DataTokens.FirstOrDefault(dt => dt.Key == "Namespace").Value;
            var actionNamespace = ((ControllerActionDescriptor)context.ActionDescriptor).MethodInfo.DeclaringType?.Namespace;
            if (dataTokenNamespace != actionNamespace)
            {
                context.Result = new HttpNotFoundResult();
                return;
            }
            base.OnActionExecuting(context);
        }
        
    }
}
