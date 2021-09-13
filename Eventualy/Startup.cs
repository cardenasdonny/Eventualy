using Eventualy.Business.Abstract;
using Eventualy.Business.Services;
using Eventualy.DAL;
using Eventualy.Model.Entities.Usuarios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventualy
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
            services.AddControllersWithViews();
            //Conexión a la DB

            /*
            var conexion = Configuration["ConnectionStrings:SqlServer"]; //Obtenemos la cadena de conexión

             Convertimos nuestro contexto en un servicio para poderlo utilizar en todas partes

            /* SQLSERVER
            services.AddDbContext<AppDbContext>(option =>
                option.UseSqlServer(conexion)                
            );
            */

            //MYSQL
            var conexion = Configuration["ConnectionStrings:MySql"]; //Obtenemos la cadena de conexión
            services.AddDbContext<AppDbContext>(option =>
                option.UseMySql(conexion, ServerVersion.AutoDetect(conexion))
            );
            services.AddScoped<IClienteService, ClienteService >();
            services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            //Indentity

            services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
              //.AddDefaultUI()
              .AddDefaultTokenProviders() //para trabajar con la confirmación de email
              .AddEntityFrameworkStores<AppDbContext>();
              //.AddClaimsPrincipalFactory<UsuarioClaimsPrincipalFactory>();

            //configuración del password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 9;
                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
                    name: "default",
                    pattern: "{controller=Clientes}/{action=Index}/{id?}");
            });
        }
    }
}
