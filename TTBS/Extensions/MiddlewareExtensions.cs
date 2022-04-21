using TTBS.Middlewares;

namespace TTBS.Extensions
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// This is a starting point of setting user session data. 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseContextUserSession(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserSessionMiddleware>();
        }
    }
}
