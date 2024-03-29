﻿using Calabonga.RulesValidatorDemo.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Calabonga.RulesValidatorDemo.Web.AppStart
{
    /// <summary>
    /// Pipeline configuration
    /// </summary>
    public static class ConfigureApplication
    {
        /// <summary>
        /// Configure pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }
            });

            app.UseAuthentication();

            app.UseResponseCaching();

            app.UseETagger();

            // app.Map($"{AppData.AuthUrl}", authServer => { authServer.UseIdentityServer(); });

            app.UseSwagger();
            app.UseSwaggerUI(ConfigureServicesSwagger.SwaggerSettings);

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseCors("CorsPolicy");

            app.UseMvc(RoutesMaps.MapRoutes);
        }
    }
}
