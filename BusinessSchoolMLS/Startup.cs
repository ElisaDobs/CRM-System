using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MRTD.Core.Common;
using MRTD.Core.Models;
using MRTD.Core.Encryption;
using BusinessSchoolMLS.SchoolBusinessComponent;

namespace BusinessSchoolMLS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ApplicationSession appSession = new ApplicationSession();
            AppBusinessLogic appBusinessLogic = new AppBusinessLogic(Settings.ConfigurationSettings.GetConfig("ApplicationId"));
            appSession.Set("DatabaseConnectionString", Settings.ConfigurationSettings.GetConfig("ConnectionString"));
            appSession.Set("ACADEMIC_RECORD_LETTER", Settings.ConfigurationSettings.GetConfig("ACADEMIC_RECORD_TEMPLETE"));
            appSession.Set("MailServer", Settings.ConfigurationSettings.GetConfig("SMTPServer"));
            appSession.Set("Port", Settings.ConfigurationSettings.GetConfig("SMTPServerPort"));
            appSession.Set("FromUsername", Settings.ConfigurationSettings.GetConfig("FromEmail"));
            appSession.Set("FromPassword", Settings.ConfigurationSettings.GetConfig("FromPassword"));
            appSession.Set("FromEmailHead", Settings.ConfigurationSettings.GetConfig("FromEmailName"));
            appSession.Set("Uploads", Settings.ConfigurationSettings.GetConfig("Uploads"));
            appSession.Set("ApplicationId", Settings.ConfigurationSettings.GetConfig("ApplicationId"));
            appSession.Set("ClientId", Settings.ConfigurationSettings.GetConfig("ClientId"));
            appSession.Set("AjaxRedirect", Settings.ConfigurationSettings.GetConfig("AjaxRedirect"));
            Session.AppSession = appSession;
            var appfunctionalities = appBusinessLogic.GetFunctionalityByApplicationId();
            Session.AppFunctionality = new ApplicationSession();
            appfunctionalities.ForEach(delegate (ApplicationFunctionalityModel functionality) 
            {
                Session.AppFunctionality.Set(functionality.ApplicationMethod, functionality);
            });
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
                /*template: "{controller=Home}/{action=Index}/{id?}");*/
            });
        }
    }
}
