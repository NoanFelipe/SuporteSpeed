using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using SuporteSpeed.API.Data;
using SuporteSpeed.API.DTOs.User;

namespace SuporteSpeed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            logger.LogInformation($"Registration Attempt for {userDto.UserName}");
            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded) 
                { 
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, "User");

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginUserDto userDto)
        {
            /* Used to generate valid PasswordHash static value
            logger.LogInformation("Beginning auth test for hash password!");

            logger.LogInformation(new PasswordHasher<ApiUser>().HashPassword(null, "P@ssword1"));

            return Ok();
            */
            logger.LogInformation($"Login Attempt for {userDto.UserName}");
            try
            {
                var user = await userManager.FindByNameAsync(userDto.UserName);
                var passwordValid = await userManager.CheckPasswordAsync(user, userDto.Password);

                if (user == null || passwordValid == false)
                    return NotFound();

                return Accepted();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
