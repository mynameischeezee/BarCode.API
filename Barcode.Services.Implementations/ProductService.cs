using System.Collections.Generic;
using System.Linq;
using Barcode.Services.Abstracitons;
using DataAccess;
using DataAccess.Daos;

namespace Barcode.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly BarcodeContext _context;

        public ProductService(BarcodeContext context)
        {
            _context = context;
        }

        public Product Get(string code)
        {
            return _context.Products.FirstOrDefault(p=> p.Code==code);
        }

        public void Create(string code, string name)
        {
            var product = new Product() {Code = code, Name = name};
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public Product Remove(string code)
        {
            var product = Get(code);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public Product Edit(string code, string name)
        {
            var product = Get(code);
            if (product != null)
            {
                product.Name = name;
            }

            _context.SaveChanges();
            return product;
        }
    }
}