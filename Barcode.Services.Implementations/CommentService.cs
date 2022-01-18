using System;
using System.Linq;
using Barcode.Services.Abstracitons;
using DataAccess;
using DataAccess.Daos;

namespace Barcode.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly BarcodeContext _context;
        private readonly ICommentValidator _commentValidator;
        private readonly IRatingService _ratingService;

        public CommentService(BarcodeContext context, ICommentValidator commentValidator, IRatingService ratingService)
        {
            _context = context;
            _commentValidator = commentValidator;
            _ratingService = ratingService;
        }

        public Comment Get(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id))
            {
                throw new ArgumentException($"Given id is wrong.");
            }
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Create(string data, int rating, int scanId)
        {
            var productId = _context.Scans.Find(scanId).ProductId;
            if (!_commentValidator.ValidateRating(rating) ||
                !_commentValidator.ValidateProduct(productId) ||
                !_commentValidator.ValidateScan(scanId))
            {
                throw new ArgumentException($"One of given arguments is wrong.");
            }
            var comment = new Comment() {Data = data, Rating = rating, LeftAt = DateTime.Now, ScanId = scanId};
            _context.Comments.Add(comment);
            _ratingService.AddRating(productId, rating);
            _context.SaveChanges();
        }

        public Comment Remove(int id)
        {
            if (!_context.Comments.Any(c => c.Id == id))
            {
                throw new ArgumentException($"Given id is wrong.");
            }
            var comment = Get(id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment Edit(int id, string data, int rating, int scanId)
        {
            if (!_commentValidator.ValidateRating(rating) ||
                !_commentValidator.ValidateProduct(data) ||
                !_commentValidator.ValidateScan(scanId) ||
                !_context.Comments.Any(c => c.Id == id))
            {
                throw new ArgumentException($"One of given arguments is wrong.");
            }
            var comment = Get(id);
            if (comment != null)
            {
                comment.Data = data;
                comment.Rating = rating;
                comment.ScanId = scanId;
            }
            _context.SaveChanges();
            return comment;
        }
    }
}