﻿using System.Threading.Tasks;
using Barcode.Services.Abstracitons;
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

        [HttpPost("~/SignIn")]
        public async Task<IActionResult> SignIn([FromBody] AuthModel data)
        {
            //var res = _userService.SignIn(data.name, data.password);
            return Ok(_userService.SignIn(data.name, data.password));
        }

        [HttpPost("~/SignUp")]
        public IActionResult SignUp([FromBody] AuthModel data)
        {
            var res = _userService.SignUp(data.name, data.password);
            return Ok(res);
        }

        public class AuthModel
        {
            public string name { get; set; }
            public string password { get; set; }
        }
    }
}