using Owin;
using System;
using System.Security.Claims;
using DataModels.Helpers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Serilog;
using WebAnime.API2.Components;
namespace WebAnime.API2
{

    public class OwinConfig
    {
        public static OAuthAuthorizationServerOptions OAuthAuthorizationOptions = new OAuthAuthorizationServerOptions()
        {
            //TokenEndpointPath = new PathString("/oauth/token"),
            Provider = new AuthorizationServerProvider(),
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
            AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(5),
            AllowInsecureHttp = true,
        };
        public static void AuthConfig(IAppBuilder app)
        {

            app.UseOAuthAuthorizationServer(OAuthAuthorizationOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        RoleClaimType = ClaimTypes.Role,


                        ValidIssuer = JwtConstants.Issuer,
                        ValidAudience = JwtConstants.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = JwtProvider.SecurityKey,

                    },

                });

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            // Use Serilog as the OWIN middleware logger
            app.Use(async (context, next) =>
            {
                Log.Information("Request received");
                await next.Invoke();
                Log.Information("Response sent");
            });

        }



    }
}