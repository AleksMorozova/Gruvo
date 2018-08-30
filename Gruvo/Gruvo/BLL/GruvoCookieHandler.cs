using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Gruvo.BLL
{
    public class GruvoCookieHandler : AuthorizationHandler<GruvoCookieRequirement>
    {
        private HttpContext _httpContext;
        private ITokenUserPairs _tokenUserPairs;

        public GruvoCookieHandler(ITokenUserPairs tokenUserPairs)
        {
            _tokenUserPairs = tokenUserPairs;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, GruvoCookieRequirement requirement)
        {
            _httpContext = ((AuthorizationFilterContext)context.Resource).HttpContext;

            if (_httpContext.Request.Cookies.ContainsKey("Gruvo"))
            {
                if (_tokenUserPairs.Pairs.ContainsKey(_httpContext.Request.Cookies["Gruvo"]))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
