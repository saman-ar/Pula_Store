using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebFramework.ActionFilters
{
	public class AjaxAttribute : ActionFilterAttribute
	{
		//private string[] allHttpMethods=new string[]{"Get","Post"};
		private IEnumerable<string> acceptableHttpMethods;
		public AjaxAttribute()
		{
			acceptableHttpMethods = new string[] { "Get" }.AsEnumerable<string>();
		}

		public AjaxAttribute(params string[] httpMethods)
		{
			acceptableHttpMethods = httpMethods.AsEnumerable<string>();
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			string requestedHttpMethod = context.HttpContext.Request.Method;
			string requestedWith = context.HttpContext.Request.Headers["X-Requested-With"];

            if (!acceptableHttpMethods.Any(h=> h.Equals(requestedHttpMethod, StringComparison.OrdinalIgnoreCase)) || requestedWith != "XMLHttpRequest")
                context.Result = new BadRequestResult();
		}
	}
}
