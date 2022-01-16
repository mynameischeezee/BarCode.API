using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using DataAccess;
using DataAccess.Daos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private BarcodeContext _barcodeContext;
        
        public ProductController(ILogger<ProductController> logger, BarcodeContext barcodeContext)
        {
            _logger = logger;
            _barcodeContext = barcodeContext;
        }
        
        [HttpGet("~/GetProductById")]
        public JsonResult GetProductById(string barcode)
        {
            using (_barcodeContext)
            {
                var firstOrDefault = _barcodeContext.Products.FirstOrDefault(product => product.Code == barcode);
                return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(firstOrDefault));
            }
        }
    }
}