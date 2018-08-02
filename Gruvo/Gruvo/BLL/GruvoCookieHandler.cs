using Gruvo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gruvo.BLL
{
    public class GruvoCookieHandler : AuthorizationHandler<GruvoCookieRequirement>
    {
        private HttpContext _httpContext;

        public GruvoCookieHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GruvoCookieRequirement requirement)
        {
            if (_httpContext.Request.Cookies.ContainsKey("Gruvo"))
            {
                if (TokenUserPairs.GetInstance().GetPairs().ContainsKey(_httpContext.Request.Cookies["Gruvo"]))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
