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
        
        public ScanController(IScanService scanService, IBarcodeConverter barcodeConverter)
        {
            _barcodeConverter = barcodeConverter;
            _scanService = scanService;
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
    }
}