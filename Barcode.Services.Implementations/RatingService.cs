using System.Linq;
using Barcode.Services.Abstracitons;
using DataAccess;

namespace Barcode.Services.Implementations
{
    public class RatingService : IRatingService
    {
        private readonly BarcodeContext _context;

        public RatingService(BarcodeContext context)
        {
            _context = context;
        }

        public void AddRating(string code, int rating)
        {
            var product = _context.Products.FirstOrDefault(p => p.Code == code);
            product.CountOfRatings += 1;
            product.OverallRatingSum += rating;
        }

        public int GetAverageRating(string code)
        {
            var product = _context.Products.FirstOrDefault(p => p.Code == code);
            return product.OverallRatingSum / product.CountOfRatings;
        }
    }
}