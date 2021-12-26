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
        /// <summary>
        /// Generates barcode image from barcode number
        /// </summary>
        /// <param name="barcodeNumber">Barcode value as string</param>
        /// <returns>Barcode image: IActionResult</returns>
        [HttpGet("~/GetBarcodeImage")]
        public IActionResult GetBarcodeImage(string barcodeNumber)
        {
            if (string.IsNullOrWhiteSpace(barcodeNumber) || barcodeNumber.Length > 12) return StatusCode(400);
            var barcodeData = BarcodeWriter.CreateBarcode(barcodeNumber, BarcodeEncoding.Code128);
            var barcodeImageBinary = barcodeData.ToJpegBinaryData();
            return File(barcodeImageBinary, "image/jpeg");
        }
        /// <summary>
        /// Generates barcode value from string
        /// </summary>
        /// <param name="barcodeNumber">Barcode value: string</param>
        /// <returns>Barcode value: string</returns>
        [HttpGet("~/GetBarcodeData")]
        public string GetBarcodeData(string barcodeNumber)
        {
            if (string.IsNullOrWhiteSpace(barcodeNumber) || barcodeNumber.Length > 12) return "Bad request";
            var barcodeData = BarcodeWriter.CreateBarcode(barcodeNumber, BarcodeEncoding.Code128);
            return barcodeData.Value;
        }
        /// <summary>
        /// Decode`s image with barcode and return it`s value
        /// </summary>
        /// <param name="url">Url of image: string</param>
        /// <returns>Barcode value: string</returns>
        [HttpPost("~/GetBarcodeValueFromUrl")]
        public string GetBarcodeValueFromUrl(string url)
        {
            var wc = new WebClient();
            var bytes = wc.DownloadData(url);
            var ms = new MemoryStream(bytes);
            var barcode = BarcodeReader.QuicklyReadOneBarcode(ms, BarcodeEncoding.All, true);
            return barcode.Value;
        }
    }
}