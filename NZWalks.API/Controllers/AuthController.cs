using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;
using System.Text.RegularExpressions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

           var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                //Add roles to this user
                if(registerRequestDto.Roles is not null && registerRequestDto.Roles.Any())
                {

                 identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login!");
                    }
                }

            }

            return BadRequest("Something went wrong!");

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
          var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user is not null)
            {
              var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(checkPasswordResult)
                {
                    //get roles for this user.
                  var roles = await userManager.GetRolesAsync(user);

                    if (roles is not null)
                    {
                        //create JWT token.
                    var jwtToken = tokenRepository.CreateJWT(user, roles.ToList());

                        var response = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };

                    return Ok(response);
                    }

                }

            }

            return BadRequest("Incorrect username or password!");
        }

    
    }
}
