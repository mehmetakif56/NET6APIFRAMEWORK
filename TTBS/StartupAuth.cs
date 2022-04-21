using Microsoft.AspNetCore.Authentication.Cookies;

namespace TTBS
{
    public static class StartupAuth
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.AccessDeniedPath = "/Error/403";
                });

            return services;
        }
    }
}