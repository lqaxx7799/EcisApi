using EcisApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var account = (Account)context.HttpContext.Items["Account"];
            if (account == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            if (Roles.Length != 0)
            {
                var role = (Role)context.HttpContext.Items["Role"];
                if (!Roles.Contains(role?.RoleName))
                {
                    // account have no role
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
