using ApiRestCampoDijital.Layout;
using ApiRestCampoDijital.Model;
using ApiRestCampoDijital.ModelDTO;
using ApiRestCampoDijital.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ApiRestCampoDijital.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IEmployerService employerService;
        private readonly IFarmSupervisorService farmSupervisorService;

        private readonly string secretKey;

        public AuthenticationController(IEmployerService employerService,
                                        IFarmSupervisorService farmSupervisorService,
                                        IConfiguration config)
        {
            this.farmSupervisorService = farmSupervisorService;
            this.employerService = employerService;
            this.secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost("supervisor")]
        public async Task<IActionResult> AuthenticateEmployee([FromBody] UserAuthentication userAuthentication)
        {
            var httpsAuthentication = new HttpVMResponsesAuthentication<FarmSupervisorDTO>();
            try
            {
                FarmSupervisorDTO farmSupervisor =await farmSupervisorService.EmployeeAutentication(userAuthentication);
                if (farmSupervisor!=null)
                {
                    string tokenCreated = CreatedTokenBearer(userAuthentication);

                    httpsAuthentication.Token = tokenCreated;
                    httpsAuthentication.Datos = farmSupervisor;
                    return Ok(httpsAuthentication);
                }
                else
                {
                    httpsAuthentication.StatusCode = HttpStatusCode.Unauthorized;
                    httpsAuthentication.Token = "";
                    httpsAuthentication.Datos = null;
                    return Ok(httpsAuthentication);
                }
                
            }
            catch (Exception ex)
            {
                httpsAuthentication.StatusCode = HttpStatusCode.InternalServerError;
                httpsAuthentication.Token = "";
                httpsAuthentication.Datos = null;
                httpsAuthentication.Messages.Add("Error al en el login");
                httpsAuthentication.Messages.Add($"{ex.Message}");
                return Ok(httpsAuthentication);
            }
        }

        [HttpPost("empleador")]
        public async Task<IActionResult> AuthenticateEmployer([FromBody] UserAuthentication userAuthentication)
        {
            var httpVMAuthentication = new HttpVMResponsesAuthentication<EmployerDTO>();
            try
            {
                EmployerDTO employerDTO =await this.employerService.EmployerAutentication(userAuthentication);
                if (employerDTO!=null)
                {
                    string tokenCreated = CreatedTokenBearer(userAuthentication);

                    httpVMAuthentication.Token = tokenCreated;
                    httpVMAuthentication.Datos = employerDTO;
                    return Ok(httpVMAuthentication);
                }
                else
                {
                    httpVMAuthentication.StatusCode = HttpStatusCode.Unauthorized;
                    httpVMAuthentication.Token = "";
                    httpVMAuthentication.Datos = null;
                    return Ok(httpVMAuthentication);
                }                   
            }
            catch (Exception ex)
            {
                httpVMAuthentication.StatusCode = HttpStatusCode.InternalServerError;
                httpVMAuthentication.Token = "";
                httpVMAuthentication.Datos = null;
                httpVMAuthentication.Messages.Add("Error al en el login");
                httpVMAuthentication.Messages.Add($"{ex.Message}");
                return Ok(httpVMAuthentication);
            }
        }

        private string CreatedTokenBearer(UserAuthentication userAuthentication)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, userAuthentication.dniOrCuit.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenConfig);
        }
       
    }
}
