using System;
using System.Web;
using System.Web.Http;
using Swagger.Net;
using Swagger.Net.Application;
using WebAnime.API2;
using WebAnime.API2.Components;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebAnime.API2
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            //var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "WebAnime.API2");
                        c.OperationFilter<HeaderFilter>();
                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.ShowCommonExtensions(true);
                        c.ShowExtensions(true);
                    });

        }
    }
}
