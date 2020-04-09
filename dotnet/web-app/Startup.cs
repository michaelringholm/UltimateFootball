using System;
using System.Collections.Generic;
using dk.opusmagus.fd.bl;
using dk.opusmagus.fd.dal;
using dk.opusmagus.fd.dal.local;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace com.opusmagus.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });            

            addSwaggerDocument(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<ILeagueDAO, LocalFileStoreLeagueDAO>();
            services.AddSingleton<ShowLeagueTableCommand, ShowLeagueTableCommand>();
            services.AddSingleton<IManagerDAO, LocalFileStoreManagerDAO>();
            services.AddSingleton<ShowManagerDetailsCommand, ShowManagerDetailsCommand>();
        }

        private void addSwaggerDocument(IServiceCollection services)
        {
            services.AddOpenApiDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Football Dreams";
                    document.Info.Description = "Football Dreams";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Michael Sundgaard",
                        Email = string.Empty,
                        Url = "https://fd.opusmagus.com"
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "MIT",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
            });
        }        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseOpenApi(settings => settings.PostProcess = OpenAPIPostProcess);
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
