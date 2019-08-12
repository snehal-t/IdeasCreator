using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ideas.Services.Services;
using Ideas.Data.Repositories.Ideas;
using System.Data;
using System.Data.SqlClient;

namespace Ideas.API
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

            //BotAuth
            var msAppIdKey = Configuration.GetSection("MicrosoftAppId")?.Value;
            var msAppPwdKey = Configuration.GetSection("MicrosoftAppPassword")?.Value;
            //AzureADAuth
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
                //.AddBotAuthentication();
           

            //Inject IDBConnection
            services.AddTransient<IDbConnection>((sp) =>
            new SqlConnection(this.Configuration.GetConnectionString("appDbConnection")));

            //Dependency injection for services and repositories
            services.AddScoped<IIdeas, Ideas.Services.Services.Imp.Ideas>();
            services.AddScoped<IIdeasRepository, Ideas.Data.Repositories.Ideas.Imp.IdeasRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}