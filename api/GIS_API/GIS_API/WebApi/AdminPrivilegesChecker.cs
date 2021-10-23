using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GIS_API
{
    public class AdminPrivilegesChecker : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["token"].ToString();

            if (isAdmin(token))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new StatusCodeResult(403);
            }
        }

        private bool isAdmin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var claim = jwtToken.Claims.First(x => x.Type == "isAdmin").Value;
            return claim.ToLower() == "true";
        }
    }
}
