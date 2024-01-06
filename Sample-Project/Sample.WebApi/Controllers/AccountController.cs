using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sample.WebApi.DTO;
using Sample.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserPofile> userManager;
        private readonly SignInManager<UserPofile> signInManager;
        private readonly IMapper mapper; 
        private readonly ILogger<AccountController> logger;
        private readonly IConfiguration  configuration;


        public AccountController(IConfiguration configuration, ILogger<AccountController> logger,IMapper mapper, UserManager<UserPofile> userManager, SignInManager<UserPofile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper=mapper;
            this.logger = logger;
            this.configuration = configuration;
        }
        [HttpPost]
        [Route("UserLogin")]
        public async Task<ActionResult<ResponseDto>> Login(LoginDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }
            try
            {
                logger.LogInformation($"Attempt User Logn via {userDto.Email}");
                var user = await userManager.FindByEmailAsync(userDto.Email);
                if (user==null)
                {
                    logger.LogError($"Something went wrong with the {userDto.Email}");
                    return BadRequest();
                }
                var validatePassword = await userManager.CheckPasswordAsync(user, userDto.Password);
                if (validatePassword == false)
                {
                    return Unauthorized(userDto);
                }
                string token = await GenerateToken(user);
                var response = new ResponseDto
                {
                    Email = user.Email,
                    TokenString = token,
                    Userid = user.Id
                };
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex ,$"Something went wrong in  the {nameof(Login)} with user {userDto.Email}");
                return Problem(ex.ToString());
            }
            return Ok();
        }
        [HttpPost]
        [Route("UserRegister")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }
            try
            {
                logger.LogInformation($"Attempt User Register via {userDto.Email}");
                var user = mapper.Map<UserPofile>(userDto);
                user.UserName = userDto.Email;
                user.ProfilePicture = " ";
                var suceess = await userManager.CreateAsync(user, userDto.Password);
                if (suceess.Succeeded == false)
                {
                    foreach (var item in suceess.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                        return BadRequest(ModelState);
                    }
                }
                await userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in  the {nameof(Register)} with user {userDto.Email}");
               
            }
            return Ok();
        }
        private async Task<string> GenerateToken(UserPofile user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jWTSetting:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: configuration["jWTSetting:Issuer"],
                audience: configuration["jWTSetting:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["jWTSetting:Duration"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
