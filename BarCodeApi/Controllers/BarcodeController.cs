using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        [HttpGet]
        public IActionResult Get(string barcode)
        {
            var barcodeData = BarcodeWriter.CreateBarcode(barcode, BarcodeEncoding.Code128);
            var barcodeImageBinary = barcodeData.ToJpegBinaryData();
            return File(barcodeImageBinary, "image/jpeg"); 
        }
        // [HttpPost]
        // public IActionResult Post(string barcode)
        // {
        //     
        // }
    }
}