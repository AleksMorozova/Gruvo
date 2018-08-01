
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
                if(TokenUserPairs.GetInstance().GetPairs().ContainsKey(_httpContext
                    .Request
                    .Cookies["Gruvo"]))
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
