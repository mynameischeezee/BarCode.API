using System.IO;

namespace Barcode.Services.Abstracitons
{
    public interface IBarcodeConverter
    {
        public string Convert(MemoryStream ms);
        public string Convert(string url);
    }
}