using System.IO;
using Barcode.Services.Abstracitons;
using DataAccess;
using DataAccess.Daos;

namespace Barcode.Services.Implementations
{
    public class ScanService : IScanService
    {
        private readonly BarcodeContext _context;
        private readonly IBarcodeConverter _converter;
        
        public ScanService(BarcodeContext context, IBarcodeConverter converter)
        {
            _converter = converter;
            _context = context;
        }
        public Scan AddScan(MemoryStream ms)
        {
            var product = _context.Products.Find(_converter.Convert(ms));
            return _context.Scans.Add(
                new Scan()
                {
                    
                }).Entity;
        }
    }
}