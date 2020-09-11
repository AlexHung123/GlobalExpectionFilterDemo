using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GlobalExpectionFilterDemo.Filter;
using GlobalExpectionFilterDemo.Log;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GlobalExpectionFilterDemo
{
        public class Startup
        {

                public static ILoggerRepository repository { get; set; }
                public Startup( IConfiguration configuration )
                {
                        Configuration = configuration;

                        //log4net
                        repository = LogManager.CreateRepository("GlobalExpectionFilterDemo");//Your Project Name

                        //Set Config file, if has any problems, then you may use InProcess Host Mode, please search the GlobalExpectionFilterDemo.csproj,and delete it
                        XmlConfigurator.Configure(repository , new FileInfo("log4net.config"));//配置文件
                }

                public IConfiguration Configuration { get; }

                // This method gets called by the runtime. Use this method to add services to the container.
                public void ConfigureServices( IServiceCollection services )
                {
                        services.AddMvc(o =>
                        {
                                o.Filters.Add(typeof(GlobalExceptionsFilter));
                        }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                        services.AddControllersWithViews();

                      

                        services.AddSingleton<ILoggerHelper , LogHelper>();

                }

                // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
                public void Configure( IApplicationBuilder app , IWebHostEnvironment env )
                {
                        if ( env.IsDevelopment() )
                        {
                                app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                                app.UseExceptionHandler("/Home/Error");
                                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                                app.UseHsts();
                        }
                        app.UseHttpsRedirection();
                        app.UseStaticFiles();

                        app.UseRouting();

                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                                endpoints.MapControllerRoute(
                        name: "default" ,
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                        });
                }
        }
}
