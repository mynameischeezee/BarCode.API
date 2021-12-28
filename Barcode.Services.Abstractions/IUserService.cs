using System.Threading.Tasks;

namespace Barcode.Services.Abstracitons
{
    public interface IUserService
    {
        public Task<string> SignIn(string name, string password);
        public Task<string> SignUp(string name, string password);
    }
}