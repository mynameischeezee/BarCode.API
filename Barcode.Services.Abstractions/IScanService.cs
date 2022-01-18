using System.Collections.Generic;
using System.IO;
using DataAccess.Daos;

namespace Barcode.Services.Abstracitons
{
    public interface IScanService
    {
        public Scan AddScan(string barcode, string userName);
        public Scan AddScan(MemoryStream ms, string userName);
        public Scan Remove(int id);
        public ICollection<Scan> GetScansForUser(int userId);
        public Scan Get(int id);
    }
}