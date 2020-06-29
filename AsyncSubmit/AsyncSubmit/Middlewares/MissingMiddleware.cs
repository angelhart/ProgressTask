using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AsyncSubmit.Middlewares
{
    public class MissingMiddleware
    {
        private readonly RequestDelegate next;

        public MissingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            await this.next(httpContext);

            if (httpContext.Response.StatusCode == 404)
            {
                httpContext.Response.Redirect("/Home/Missing");
            }
        }
    }
}
