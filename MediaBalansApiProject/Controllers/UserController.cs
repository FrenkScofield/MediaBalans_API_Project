using MediaBalansApiProject.Models;
using MediaBalansApiProject.Models.Requests;
using MediaBalansApiProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public UserController(UserService userService,
                              UserManager<User> userManager,
                               SignInManager<User> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("userregister")]
        public IActionResult Adduser(AddUserRequest addUserRequest)
        {
            _userService.PostUser(addUserRequest);

            return Ok("success");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserRequest loginUserRequest)
        {
            //_userService.PostLogin(loginUserRequest);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User user = await _userManager.FindByNameAsync(loginUserRequest.Username);



            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return NotFound(loginUserRequest);
            }



            var result = await _signInManager.PasswordSignInAsync(user, loginUserRequest.Password, false, false);

            if (!result.Succeeded)
            {
                return BadRequest("Username or Password is invalid!");
            }
            return Ok("success");

        }

    }
}

