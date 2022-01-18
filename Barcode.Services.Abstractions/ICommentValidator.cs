namespace Barcode.Services.Abstracitons
{
    public interface ICommentValidator
    {
        bool ValidateScan(int id);
        bool ValidateProduct(string code);
        bool ValidateRating(int rating);
    }
}