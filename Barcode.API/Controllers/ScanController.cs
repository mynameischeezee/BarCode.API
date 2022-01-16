using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Barcode.Services.Abstracitons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IScanService _scanService;
        private readonly IBarcodeConverter _barcodeConverter;
        
        public ScanController(IScanService scanService, IBarcodeConverter barcodeConverter, ILogger<ProductController> logger)
        {
            _barcodeConverter = barcodeConverter;
            _logger = logger;
            _scanService = scanService;
        }
        [HttpPost("~/AddScanUrl/{userName}")]
        public ActionResult AddScan(string userName, string url)
        {
            var barcode = _barcodeConverter.Convert(url);
            var scan = _scanService.AddScan(barcode, userName);
            return Ok(scan.Id);
        }
        
        [HttpPost("~/AddScan/{userName}")]
        public async Task<ActionResult> AddScan(string userName)
        {
            var res = HttpContext.Request.Form.Files;
            using (var buffer = new MemoryStream())
            {
                var file = res.FirstOrDefault();
                file.CopyTo(buffer);
                var barcode = _barcodeConverter.Convert(buffer);
                var scan = _scanService.AddScan(barcode, userName);
                return Ok(scan.Id);
            };
        }
        
        // [HttpPost("~/AddScan/{userName}/{barcode}")]
        // public async Task<ActionResult> AddScan(string userName, string barcode)
        // {
        //     var scan = _scanService.AddScan(barcode, userName);
        //     return Ok(scan.Id);
        // }
        
        
    }
}