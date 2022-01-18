using System.Threading.Tasks;
using Barcode.Services.Abstracitons;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("~/SignIn/{name}")]
        public async Task<ActionResult> SignIn(string name)
        {
            return Ok(await _userService.GetUser(name));
        }

        [HttpPost("~/AddUser")]
        public ActionResult AddUser([FromBody] AuthModel model)
        {
            _userService.AddUser(model.Name, model.Pass);
            return Ok();
        }
    }
    
    public class AuthModel
    {
        public string Name { get; set; }
        public string Pass { get; set; }
    }
}