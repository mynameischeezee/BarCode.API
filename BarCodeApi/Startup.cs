using DataAccess.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BarCodeApi
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
        //     services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //         .AddJwtBearer(options => {  
        //             options.TokenValidationParameters =   
        //                 new TokenValidationParameters  
        //                 {  
        //                     ValidateIssuer = true,  
        //                     ValidateAudience = true,  
        //                     ValidateLifetime = true,  
        //                     ValidateIssuerSigningKey = true,  
        //
        //                     ValidIssuer = "Fiver.Security.Bearer",  
        //                     ValidAudience = "Fiver.Security.Bearer",
        //                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secrererert-dds-ss"))  
        //                 };  
        //         });
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "BarCodeApi", Version = "v1"});
            });
            services.AddEf();
            services.Inject();

            #region Database-Update (Just uncomment and run)

            // var provider = services.BuildServiceProvider();
            // var context = provider.GetService<BarcodeContext>();
            // context.Database.Migrate();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarCodeApi v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthorization();
    
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}