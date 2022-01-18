namespace Barcode.Services.Abstracitons
{
    public interface IRatingService
    {
        void AddRating(string code, int rating);
        int GetAverageRating(string code);
    }
}