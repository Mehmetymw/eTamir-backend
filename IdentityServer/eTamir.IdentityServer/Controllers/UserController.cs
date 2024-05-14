using System.Threading.Tasks;
using eTamir.IdentityServer.Dtos;
using eTamir.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using eTamir.Shared.Dtos;
using System.Linq;
using static IdentityServer4.IdentityServerConstants;
using Microsoft.AspNetCore.Authorization;
using eTamir.IdentityServer.Data;
using System.IdentityModel.Tokens.Jwt;

namespace eTamir.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            if (signUpDto is null)
            {
                return BadRequest(Response<NoContent>.Fail("User info is null for signup", 400));
            }

            if (!string.Equals(signUpDto.Password, signUpDto.PasswordConfirmation))
            {
                return BadRequest(Response<NoContent>.Fail("Passwords are doesn't match", 400));
            }

            var user = new ApplicationUser
            {
                UserName = signUpDto.Email,
                Name = signUpDto.Name,
                Surname = signUpDto.Surname,
                Email = signUpDto.Email,
            };

            var result = await userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(t => t.Description).ToList(), 400));
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim is null) return BadRequest();

            var user = await userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null) return BadRequest();

            var appUser = new
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Id = user.Id
            };

            return Ok(appUser);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null) return BadRequest();

            var appUser = new
            {
                Name = user.Name,
                Surname = user.Surname,
            };

            return Ok(appUser);
        }

        // [HttpPost]
        // public async Task<IActionResult> GoogleLogin(string idToken)
        // {
        //     var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

        //     var user = new ApplicationUser
        //     {
        //         UserName = payload.Email,
        //         Email = payload.Email,
        //         Name = payload.GivenName,
        //         Surname = payload.FamilyName
        //     };

        //     var result = await _userManager.CreateAsync(user);

        //     if (result.Succeeded)
        //     {
        //         var claims = new[]
        //         {
        //             new Claim(ClaimTypes.NameIdentifier, user.Id),
        //             new Claim(ClaimTypes.Email, user.Email),
        //         };

        //         var claimsIdentity = new ClaimsIdentity(
        //             claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //         var authProperties = new AuthenticationProperties
        //         {
        //         };

        //         await HttpContext.SignInAsync(
        //             CookieAuthenticationDefaults.AuthenticationScheme,
        //             new ClaimsPrincipal(claimsIdentity),
        //             authProperties);

        //         return Ok();
        //     }
        //     else
        //     {
        //         return BadRequest();
        //     }
        // }
    }
}
