using System.Collections.Generic;
using DataAccess.Daos;

namespace Barcode.Services.Abstracitons
{
    public interface IHistoryService
    {
        public ICollection<Scan> GetScansForUser(User user);
        public ICollection<Scan> GetScansForUser(int id);
    }
}