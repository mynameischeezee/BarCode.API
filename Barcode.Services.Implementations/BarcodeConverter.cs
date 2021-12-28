using System.IO;
using System.Net;
using Barcode.Services.Abstracitons;
using IronBarCode;

namespace Barcode.Services.Implementations
{
    public class BarcodeConverter : IBarcodeConverter
    {
        public string Convert(MemoryStream ms)
        {
            var barcode = BarcodeReader.QuicklyReadOneBarcode(ms, BarcodeEncoding.All, true);
            return barcode.Value;
        }

        public string Convert(string url)
        {
            var wc = new WebClient();
            var bytes = wc.DownloadData(url);
            var ms = new MemoryStream(bytes);
            var barcode = BarcodeReader.QuicklyReadOneBarcode(ms, BarcodeEncoding.All, true);
            return barcode.Value;
        }
    }
}