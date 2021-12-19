using System;
using System.Linq;
using DataAccess.Daos;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extentions
{
    public static class Injection
    {
        public static void Inject(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var context = provider.GetService<BarcodeContext>();

            var user = context.Users.First();

            // Product prod1;
            //
            // context.Products.Add(prod1 = new Product()
            //     {
            //         Code = "code2",
            //         Name = "potato",
            //         OverallRatingSum = 0,
            //         CountOfRatings = 0
            //     }
            // );
            //
            // context.Products.Add(new Product()
            //     {
            //         Code = "code3",
            //         Name = "batato",
            //         OverallRatingSum = 0,
            //         CountOfRatings = 0
            //     }
            // );

            // context.Users.Add(new User()
            // {
            //     Name = "name",
            //     PassHash = "dsfdf23", 
            //     Scans = new List<Scan>()
            //     {
            //         new Scan()
            //         {
            //             ProductId = "code1",
            //             ScanTime = DateTime.Now
            //         }
            //     }
            // });

            // context.Scans.Add(
            //     new Scan()
            //         {
            //             UserId = 2,
            //             Product = prod1,
            //             ScanTime = DateTime.Now
            //         }
            //     );
            // context.Scans.Add(
            //     new Scan()
            //     {
            //         UserId = 2,
            //         ProductId = "code3",
            //         ScanTime = DateTime.Now
            //     }
            // );
            // context.SaveChanges();
        }
    }
}