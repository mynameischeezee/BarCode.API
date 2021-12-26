using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly ILogger<BarcodeController> _logger;

        public BarcodeController(ILogger<BarcodeController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("~/GetBarcodeImage")]
        public IActionResult GetBarcodeImage(string barcodeNumber)
        {
            if (string.IsNullOrWhiteSpace(barcodeNumber) || barcodeNumber.Length > 12) return StatusCode(400);
            var barcodeData = BarcodeWriter.CreateBarcode(barcodeNumber, BarcodeEncoding.Code128);
            var barcodeImageBinary = barcodeData.ToJpegBinaryData();
            return File(barcodeImageBinary, "image/jpeg");
        }

        [HttpGet("~/GetBarcodeData")]
        public string GetBarcodeData(string barcodeNumber)
        {
            if (string.IsNullOrWhiteSpace(barcodeNumber) || barcodeNumber.Length > 12) return "Bad request";
            var barcodeData = BarcodeWriter.CreateBarcode(barcodeNumber, BarcodeEncoding.Code128);
            return barcodeData.Value;
        }
        //TODO: Finish this method
        //TODO: Test, if this method works with images with objects on picture, exact barcode
        [HttpPost("~/GetBarcodeImageFromUrl")]
        public string GetBarcodeImageFromUrl([FromBody]Bitmap url)
        {
            return url.Height.ToString();
        }
    }
}