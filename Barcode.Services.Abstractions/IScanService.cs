using System.IO;
using DataAccess.Daos;

namespace Barcode.Services.Abstracitons
{
    public interface IScanService
    {
        public Scan AddScan(MemoryStream ms);
    }
}