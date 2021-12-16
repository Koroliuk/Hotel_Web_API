using System.Web.Http;
using WebActivatorEx;
using Hotel.Web_API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Hotel.Web_API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Hotel.Web_API");
                        c.PrettyPrint();
                    })
                .EnableSwaggerUi();
        }
    }
}
