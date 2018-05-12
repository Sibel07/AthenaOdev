using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace sibelApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                string token = sibel.Login("test", "123456", "146116");
                if (token != "-1")
                {
                    var loginResult = JsonConvert.DeserializeObject<Entity.Login>(token);
                    var restoranListe = sibel.RestoranListesiGetir(loginResult.token);
                    await context.Response.WriteAsync("test");
                }
                else
                {
                    await context.Response.WriteAsync("Kullanıcı adı şifreniz hatalı!");

                    //Geçersiz kullanıcı adı & şifre
                }

            });
        }
    }
}
