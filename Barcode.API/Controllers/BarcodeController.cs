using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Barcode.Services.Abstracitons;
using IronBarCode;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarcodeController : ControllerBase
    {
        private readonly ILogger<BarcodeController> _logger;
        private readonly IBarcodeConverter _converter;
        private readonly IUserService _userService;
        

        public BarcodeController(ILogger<BarcodeController> logger, IBarcodeConverter converter, IUserService userService)
        {
            _logger = logger;
            _converter = converter;
            _userService = userService;
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
            return _converter.Convert(url);
        }
        
        [HttpPost("~/GetBarcodeFromImage")]
        public async Task<IActionResult> GetBarcodeFromImage()
        {
            var res1 = HttpContext.Request.Form.Files;
            using var buffer = new System.IO.MemoryStream();
            res1.FirstOrDefault().CopyTo(buffer);
            var res = _converter.Convert(buffer);
            return Ok(res);
        }


        public class UploadImageModel
        {
            public string Image { get; set; }
        }
    }
}