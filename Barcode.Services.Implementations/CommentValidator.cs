using System.Linq;
using Barcode.Services.Abstracitons;
using DataAccess;

namespace Barcode.Services.Implementations
{
    public class CommentValidator : ICommentValidator
    {
        private readonly BarcodeContext _context;

        public CommentValidator(BarcodeContext context)
        {
            _context = context;
        }

        public bool ValidateScan(int id)
        {
            return _context.Scans.Any(s => s.Id == id);
        }

        public bool ValidateProduct(string code)
        {
            return _context.Products.Any(p => p.Code == code);
        }

        public bool ValidateRating(int rating)
        {
            return rating is > 0 and <= 5;
        }
    }
}