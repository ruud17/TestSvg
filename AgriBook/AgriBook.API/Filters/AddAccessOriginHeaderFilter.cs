using System.Web.Mvc;
//using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace AgriBook.API.Filters
{
    public class AddAccessOriginHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}