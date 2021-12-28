using System.Collections.Generic;
using Barcode.Services.Abstracitons;
using DataAccess;
using DataAccess.Daos;

namespace Barcode.Services.Implementations
{
    public class HistoryService : IHistoryService
    {
        private readonly BarcodeContext _context;
        public HistoryService(BarcodeContext context)
        {
            _context = context;
        }
        public ICollection<Scan> GetScansForUser(User user)
        {
            return user.Scans;
        }

        public ICollection<Scan> GetScansForUser(int id)
        {
            return _context.Users.Find(id).Scans;
        }
    }
}