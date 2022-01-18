using DataAccess.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extentions
{
    public static class Ef
    {
        public static IServiceCollection AddEf(this IServiceCollection services)
        {
            return services.AddDbContext<BarcodeContext>(
                options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlite(
                        @$"DataSource={ResourceHelper.GetDbLocation()};"
                    );
                });
            // return services.AddDbContext<BarcodeContext>();
        }
    }
}