using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Swashbuckle.Application;
using WebAnime.API2;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: PreApplicationStartMethod(typeof(SwashbuckleConfig), "Register")]
#pragma warning restore CS0612 // Type or member is obsolete

namespace WebAnime.API2
{
    [Obsolete]
    public class SwashbuckleConfig
    {
        public static void Register()
        {

            //GlobalConfiguration.Configuration
            //    .EnableSwagger(c =>
            //        {
            //            c.SingleApiVersion("v1", "WebAnime.API2.OldSwaggerUI");
                        
                       
            //        })
            //    .EnableSwaggerUi(c =>
            //        {
                        
            //        });

        }
    }
}
