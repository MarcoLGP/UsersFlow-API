using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersFlow_API.DTOs;
using UsersFlow_API.Services;

namespace UsersFlow_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IUserRefreshTokenService _userRefreshTokenService;

        public AuthController(IUserService userService,
            ITokenService tokenService,
            IConfiguration configuration,
            IUserRefreshTokenService userRefreshTokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _configuration = configuration;
            _userRefreshTokenService = userRefreshTokenService;
        }

        private string GenerateStringToken(int userId)
        {
            var authClaims = new List<Claim>
                {
                new("Id", userId.ToString())
            };

            var token = _tokenService.GenerateToken(authClaims, _configuration);
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }

        [HttpPost]
        [Route(template: "sign-up")]
        public async Task<ActionResult<TokenDTO>> SignUp([FromBody] SignUpDTO signUpDTO)
        {
            try
            {
                var newUser = await _userService.signUpUser(signUpDTO.Name, signUpDTO.Email, signUpDTO.Password);

                if (newUser is null)
                    return Conflict("Usuário já cadastrado");

                var token = GenerateStringToken(newUser.UserId);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _userRefreshTokenService.addUserRefreshToken(refreshToken, newUser.UserId);

                return Ok(new TokenDTO { Token = token, RefreshToken = refreshToken });
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpPost]
        [Route(template: "sign-in")]
        public async Task<ActionResult<TokenDTO>> SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                var userSigned = await _userService.signInUser(signInDTO.Email, signInDTO.Password);

                if (userSigned is null)
                    return Unauthorized("Usuário não encontrado");

                var token = GenerateStringToken(userSigned.UserId);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _userRefreshTokenService.addUserRefreshToken(refreshToken, userSigned.UserId);

                return Ok(new TokenDTO { Token = token, RefreshToken = refreshToken });
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpPost]
        [Route(template: "check-token")]
        public ActionResult CheckToken([FromBody] string Token)
        {
            try
            {
                _ = _tokenService.GetClaimsPrincipalFromExpiredToken(Token, _configuration);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpPost]
        [Route(template: "refresh-token")]
        public async Task<ActionResult<TokenDTO>> ValidateToken([FromBody] TokenDTO tokenDTO)
        {
            try
            {
                ClaimsPrincipal principal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokenDTO.Token, _configuration);
                var idUserToken = principal.FindFirstValue("Id");

                if (idUserToken is null)
                    return BadRequest("Token inválido");

                if (!int.TryParse(idUserToken, out int intIdUserToken))
                    return BadRequest("Token inválido");

                var userRefreshToken = await _userRefreshTokenService.getUserRefreshToken(tokenDTO.RefreshToken, intIdUserToken);
                
                if (userRefreshToken is null)
                    return BadRequest("Token inválido");

                var newUserRefreshToken = _tokenService.GenerateRefreshToken();
                await _userRefreshTokenService.updateUserRefreshToken(userRefreshToken, newUserRefreshToken, intIdUserToken);

                var newUserToken = GenerateStringToken(intIdUserToken);

                return Ok(new TokenDTO { Token = newUserToken, RefreshToken = newUserRefreshToken });
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }
    }
}
