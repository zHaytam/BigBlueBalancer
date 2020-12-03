using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Web;

namespace BigBlueBalancer.Api.Filters
{
    public class DecodeParamsFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Count == 0)
                return;

            var model = context.ActionArguments.First().Value;
            if (model.GetType() == typeof(string))
                return;

            foreach (var property in model.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(string))
                    continue;

                var value = property.GetValue(model);
                if (value == null)
                    continue;

                property.SetValue(model, HttpUtility.UrlDecode(value.ToString()));
            }
        }
    }
}
