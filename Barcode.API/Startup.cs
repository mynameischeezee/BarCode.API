using Barcode.Services.Abstracitons;
using Barcode.Services.Implementations;
using DataAccess;
using DataAccess.Daos;
using DataAccess.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
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
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IBarcodeConverter, BarcodeConverter>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IScanService, ScanService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ICommentValidator, CommentValidator>();
            services.AddTransient<IRatingService, RatingService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Barcode.API", Version = "v1"});
            });
            services.AddEf();
            services.Inject();
            #region Database-Update (Just uncomment and run)

            //services.AddDbContext<BarcodeContext>();
            //var provider = services.BuildServiceProvider();
            //var context = provider.GetService<BarcodeContext>();
            //context.Database.Migrate();
            //context.SaveChanges();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Barcode.API v1"));
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseAuthorization();
    
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}