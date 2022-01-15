using System.Threading.Tasks;
using Barcode.Services.Abstracitons;
using Barcode.Services.Implementations;
using Barcode.Services.Implementations.Helpers;
using DataAccess.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        
        [HttpGet("~/GetProduct")]
        public string GetProdct(string barcodeNumber)
        {
            Product product = _productService.Get(barcodeNumber);
            return JsonConvert.SerializeObject(product);
        }

        [HttpPost("~/AddProduct")]
        public ActionResult AddProduct(string barcodeNumber, string name)
        {
            _productService.Create(barcodeNumber, name);
            return new OkResult();
        }
        
        [HttpPost("~/EditProduct")]
        public ActionResult EditProduct(string barcodeNumber, string name)
        {
            _productService.Edit(barcodeNumber, name);
            return new OkResult();
        }
        
        [HttpPost("~/RemoveProduct")]
        public ActionResult RemoveProduct(string barcodeNumber)
        {
            _productService.Remove(barcodeNumber);
            return new OkResult();
        }
    }
}