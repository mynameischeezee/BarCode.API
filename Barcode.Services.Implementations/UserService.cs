using System;
using System.Security.Claims;
using System.Text;
using Barcode.Services.Abstracitons;
using DataAccess.Daos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Barcode.Services.Implementations.Helpers;
using DataAccess;
using DataAccess.Resource;
using Microsoft.IdentityModel.Tokens;

namespace Barcode.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly BarcodeContext _context;
        
        public UserService(BarcodeContext context)
        {
            _context = context;
        }
        
        public Task<string> SignIn(string name, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Name == name);
            
            if (ValidatePasswordInternal(user, password))
            {
                return generateJwtToken(user);
            }
            throw new Exception("User not validated");
        }

        public async Task<string> SignUp(string name, string password)
        {
            var salt = Hasher.GetSalt();
            var passhash = Hasher.GetHash(salt, password);
            var user = _context.Users.Add(new User() {Name = name, PassHash = passhash, PassSalt = salt, RoleId = 1});
            _context.SaveChanges();
            var res = generateJwtToken(user.Entity);
            return generateJwtToken(user.Entity).Result;
        }
        
        private Task<string> generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("some-secret-phrase-ddd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.Run(() => tokenHandler.WriteToken(token));
        }

        private bool ValidatePasswordInternal(User user, string password)
        {
            //throw new NotImplementedException();
            //var salt1 = Hasher.GetSalt();
            //var sringSalt = Convert.ToBase64String(salt1);
            
            var salt = user.PassSalt;
             if (Hasher.GetHash(salt, password) == user.PassHash)
             {
                 return true;
             }
             return false;
        }
    }
}