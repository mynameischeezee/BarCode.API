using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Barcode.Services.Abstracitons;
using DataAccess;
using DataAccess.Daos;

namespace Barcode.Services.Implementations
{
    public class ScanService : IScanService
    {
        private readonly BarcodeContext _context;
        private readonly IBarcodeConverter _converter;
        private readonly IUserService _userService;
        
        public ScanService(BarcodeContext context, IBarcodeConverter converter, IUserService userService)
        {
            _converter = converter;
            _context = context;
            _userService = userService;
        }
        public Scan AddScan(string barcode, string userName)
        {
            var userId = _userService.GetUser(userName).Result;
            var added = _context.Scans.Add(
                new Scan()
                {
                    ProductId = barcode,
                    UserId = userId,
                    ScanTime = DateTime.Now
                }).Entity;
            
            _context.SaveChanges();
            return added;
        }

        public Scan AddScan(MemoryStream bytes, string userName)
        {
            var barcode = _converter.Convert(bytes);
            return AddScan(barcode, userName);
        }

        public Scan Remove(int id)
        {
            var removed = _context.Scans.FirstOrDefault(x => x.Id == id);
            _context.Scans.Remove(removed);
            _context.SaveChanges();
            return removed;
        }

        public Scan Get(int id)
        {
            return _context.Scans.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Scan> GetScansForUser(int userId)
        {
            return _context.Scans.Where(x => x.UserId == userId).ToList();
        }
    }
}