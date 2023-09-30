using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sample.WebApi.Data.DTO;
using Sample.WebApi.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IConfiguration configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, UserManager<User> userManager,IConfiguration configuration)
        {
            this.logger = logger;
            UserManager = userManager;
            this.configuration = configuration;
        }

        public UserManager<User> UserManager { get; }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<ResponseModel>> Register(UserDTO dto)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                User model = new User();
                model.Email = dto.Email;
                model.FirstName = dto.FirstName;
                model.LastName = dto.LastName;
                model.UserName = dto.Email;
                var result = await UserManager.CreateAsync(model, dto.Password);
                if (result.Succeeded == false)
                {
                    responseModel.IsSuccess = false;
                    responseModel.ResponseMessge = result.Errors.FirstOrDefault().Description.ToString();
                    responseModel.Data = dto;
                    return responseModel;
                }
                responseModel.IsSuccess = true;
                responseModel.ResponseMessge = "Success";
                responseModel.Data = dto;
            }
            catch (Exception ex)
            {

                responseModel.IsSuccess = false;
                responseModel.ResponseMessge = ex.ToString();
                responseModel.Data = dto;

            }
            return responseModel;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ResponseModel>> Login(LoginDTO dto)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var user = await UserManager.FindByEmailAsync(dto.Email);
                var passwordValidate = UserManager.CheckPasswordAsync(user, dto.Password);
                if (user == null && passwordValidate.Result == false)
                {
                    responseModel.IsSuccess = false;
                    responseModel.ResponseMessge = "Invalid User";
                    responseModel.Data = dto;
                    return responseModel;
                }
                responseModel.IsSuccess = true;
                responseModel.ResponseMessge = "Success";
                 responseModel.Token=await GenerateToken(user);
                responseModel.Data = dto;
            }
            catch (Exception ex)
            {

                responseModel.IsSuccess = false;
                responseModel.ResponseMessge = ex.ToString();
                responseModel.Data = dto;
            }
            return responseModel;
        }
       private async Task<string> GenerateToken(User user)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),
            };
            var token = new JwtSecurityToken(
                issuer: configuration["JwtSetting:Issuer"],
                audience: configuration["JwtSetting:audince"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSetting:Duration"]))
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
