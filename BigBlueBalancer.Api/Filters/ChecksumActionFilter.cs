using BigBlueBalancer.Api.DTOs;
using BigBlueButton.Client.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace BigBlueBalancer.Api.Filters
{
    public class ChecksumActionFilter : IActionFilter
    {
        private readonly IConfiguration _configuration;

        public ChecksumActionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.StartsWithSegments("/bigbluebutton"))
                return;

            var checksum = context.HttpContext.Request.Query["checksum"];
            if (checksum.Count == 0)
            {
                context.Result = new OkObjectResult(BaseBBBResponse.ChecksumError);
                return;
            }

            var actionName = (context.ActionDescriptor as ControllerActionDescriptor) .ActionName;
            var callName = $"{char.ToLower(actionName[0])}{actionName[1..]}";
            var query = context.HttpContext.Request.QueryString.ToString();
            query = Regex.Replace(query[1..], "&?checksum=[^&]+", "");
            var realChecksum = ChecksumGenerator.Generate(callName, _configuration["Secret"], query);

            if (checksum != realChecksum)
            {
                context.Result = new OkObjectResult(BaseBBBResponse.ChecksumError);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
