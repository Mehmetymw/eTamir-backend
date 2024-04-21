using Microsoft.AspNetCore.Http;

namespace eTamir.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string UserId => httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}