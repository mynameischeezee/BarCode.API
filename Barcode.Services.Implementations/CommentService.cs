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

        public CommentService(BarcodeContext context)
        {
            _context = context;
        }

        public Comment Get(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void Create(string data, int rating, int scanId)
        {
            var comment = new Comment() {Data = data, Rating = rating, LeftAt = DateTime.Now, ScanId = scanId};
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public Comment Remove(int id)
        {
            var comment = Get(id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment Edit(int id, string data, int rating, int scanId)
        {
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