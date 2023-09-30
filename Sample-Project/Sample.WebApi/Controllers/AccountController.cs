using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly IConfiguration configuration;



        public AccountController(IConfiguration configuration, ILogger<AccountController> logger, IMapper mapper, UserManager<UserPofile> userManager, SignInManager<UserPofile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
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
                return await LoginUser(userDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in  the {nameof(Login)} with user {userDto.Email}");
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
                return await RegisterUser(userDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Something went wrong in  the {nameof(Register)} with user {userDto.Email}");

            }
            return Ok();
        }
        [HttpGet]
        [Route("ExternalLogin")]
        public async Task<ActionResult<List<ExterLogin>>> onGeAsync()
        {
            List<ExterLogin> logins = new List<ExterLogin>();
            var obj = new ExterLogin();

            obj.RedirectUrl = "https://localhost:7206/signin-google";
            //clear the external cookie to ensure the clear login
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var Externallogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            obj.loginName = Externallogins[0].DisplayName;
            // obj.HandlerType= logins[0].HandlerType.ToString();
            logins.Add(obj);
            return Ok(logins);
        }
        [HttpGet]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            //HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            //var Externallogins =  signInManager.GetExternalAuthenticationSchemesAsync();
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback"),
                Items = { { "scheme", "Google" } }
            };

            return Challenge(properties, "Google");
        }

        [HttpGet("google-login-callback")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Google");

            if (authenticateResult.Succeeded)
            {
                var user = new UserDto();
                Guid newGuid = Guid.NewGuid();
                // Claims associated with the authenticated user
                var claims = authenticateResult.Principal.Claims;
                user.Email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
                user.FirstName = authenticateResult.Principal.FindFirstValue(ClaimTypes.GivenName);
                user.Lastname = authenticateResult.Principal.FindFirstValue(ClaimTypes.Surname);
                var users = await userManager.FindByEmailAsync(user.Email);
                if (users == null)
                {
                    user.Password = "M1@" + newGuid.ToString();
                    var response = await RegisterUser(user);
                }
                else
                {
                    user.Password = users.UserPassword;
                }
                //if (response.ToString()==200)
                //{

                //}
                //if (response.)
                //{

                //}
                //LoginDTO dto = new LoginDTO();
                //dto.Email = user.Email;
                //dto.Password = user.Password;
                //var ResponseModel= LoginUser(dto);
                return Redirect($"https://localhost:7206/ExternalLoginCallback/{user.Email}/{user.Password}");
            }
            //var googleUser=  this.User.Identities.FirstOrDefault();
            //if (googleUser.IsAuthenticated)
            //{
            //    var name = googleUser.Name;
            //    var claims = googleUser.Claims;
            //    var roles = googleUser.RoleClaimType;
            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(googleUser));
            //}
            //var info = await signInManager.GetExternalLoginInfoAsync();
            //var props = new AuthenticationProperties();
            //props.StoreTokens(info.AuthenticationTokens);
            // Authenticate user and generate an authentication token (JWT)

            return Redirect($"https://localhost:7206/ExternalLoginCallback/?Email={""}&&Password={""}");
        }
        private async Task<IActionResult> RegisterUser(UserDto userDto)
        {

            logger.LogInformation($"Attempt User Register via {userDto.Email}");
            var user = mapper.Map<UserPofile>(userDto);
            user.UserName = userDto.Email;
            user.ProfilePicture = " ";
            user.UserPassword = userDto.Password;
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
            return Ok();
        }
        private async Task<ActionResult<ResponseDto>> LoginUser(LoginDTO userDto)
        {

            var user = await userManager.FindByEmailAsync(userDto.Email);
            if (user == null)
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
