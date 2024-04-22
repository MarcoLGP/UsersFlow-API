using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UsersFlow_API.DTOs;
using UsersFlow_API.Services;
using UsersFlow_API.Utils;

namespace UsersFlow_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IUserRefreshTokenService _userRefreshTokenService;

        public AuthController(
            IAuthService authService, 
            ITokenService tokenService, 
            IConfiguration configuration, 
            IUserRefreshTokenService userRefreshTokenService
            )
        {
            _authService = authService;
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
                var newUser = await _authService.signUpUser(signUpDTO.Name, signUpDTO.Email, signUpDTO.Password);

                if (newUser is null)
                    return Conflict(new MessageReturnDTO { Message = "Usuário já cadastrado" });

                var token = GenerateStringToken(newUser.UserId);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _userRefreshTokenService.addUserRefreshToken(refreshToken, newUser.UserId);

                return Ok(new TokenDTO { Token = token, RefreshToken = refreshToken });
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possível processar a operação" });
            }
        }

        [HttpPost]
        [Route(template: "sign-in")]
        public async Task<ActionResult<SignInResponseDTO>> SignIn([FromBody] SignInDTO signInDTO)
        {
            try
            {
                var userSigned = await _authService.signInUser(signInDTO.Email, signInDTO.Password);

                if (userSigned is null)
                    return Unauthorized(new MessageReturnDTO { Message = "Credenciais fornecidas inválidas" });

                var token = GenerateStringToken(userSigned.UserId);
                var refreshToken = _tokenService.GenerateRefreshToken();

                await _userRefreshTokenService.addUserRefreshToken(refreshToken, userSigned.UserId);

                return Ok(new SignInResponseDTO { Token = token, RefreshToken = refreshToken, Name = userSigned.Name });
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possível processar a operação" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route(template: "sign-out")]
        public async Task<ActionResult> SignOut([FromBody] SignOutDTO signOutDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var userId = AppUtils.GetIntTokenUserId(tokenRequest, _tokenService, _configuration);

                await _userRefreshTokenService.removeUserRefreshToken(signOutDTO.RefreshToken, userId);
                
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possível processar a operação" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route(template: "refresh-token")]
        public async Task<ActionResult<TokenDTO>> ValidateToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                ClaimsPrincipal principal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokenRequest, _configuration);
                var idUserToken = principal.FindFirstValue("Id");

                if (idUserToken is null)
                    return BadRequest(new MessageReturnDTO { Message = "Token inválido" });

                if (!int.TryParse(idUserToken, out int intIdUserToken))
                    return BadRequest(new MessageReturnDTO { Message = "Token inválido" });

                var userRefreshToken = await _userRefreshTokenService.getUserRefreshToken(refreshTokenDTO.RefreshToken, intIdUserToken);
                
                if (userRefreshToken is null)
                    return BadRequest(new MessageReturnDTO { Message = "Token inválido" });

                var newUserRefreshToken = _tokenService.GenerateRefreshToken();
                await _userRefreshTokenService.updateUserRefreshToken(userRefreshToken, newUserRefreshToken, intIdUserToken);

                var newUserToken = GenerateStringToken(intIdUserToken);

                return Ok(new TokenDTO { Token = newUserToken, RefreshToken = newUserRefreshToken });
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possível processar a operação" });
            }
        }
    }
}
