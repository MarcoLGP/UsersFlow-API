using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UsersFlow_API.DTOs;
using UsersFlow_API.Services;
using UsersFlow_API.Utils;


namespace UsersFlow_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPut]
        [Route(template: "name")]
        public async Task<ActionResult> Put([FromBody] UserNameDTO userNameDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var userId = AppUtils.GetIntTokenUserId(tokenRequest, _tokenService, _configuration);

                var isUserUpdated = await _userService.updateUserName(userId, userNameDTO.Name);
                
                if (isUserUpdated is null)
                {
                    return NotFound(new MessageReturnDTO { Message = "Não foi possível localizar o usuário" });
                }

                return Created();
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possivel processar a operação" });
            }
        }

        [HttpPut]
        [Route(template: "email")]
        public async Task<ActionResult> PutEmail([FromBody] UserEmailDTO userEmailDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var userId = AppUtils.GetIntTokenUserId(tokenRequest, _tokenService, _configuration);

                var isUserUpdated = await _userService.updateUserEmail(userId, userEmailDTO.Email);

                if (isUserUpdated is null)
                    return NotFound(new MessageReturnDTO { Message = "Não foi possível localizar o usuário" });

                return Created();

            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possivel processar a operação" });
            }
        }


        [HttpPut]
        [Route(template: "password")]
        public async Task<ActionResult> PutPassword([FromBody] UserPasswordDTO userPasswordDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var userId = AppUtils.GetIntTokenUserId(tokenRequest, _tokenService, _configuration);

                var isUserUpdated = await _userService.updateUserPassword(userId, userPasswordDTO.NewPassword, userPasswordDTO.OldPassword);

                if (isUserUpdated is null)
                    return NotFound(new MessageReturnDTO { Message = "Não foi possível localizar o usuário" });

                if (isUserUpdated == false)
                    return Conflict(new MessageReturnDTO { Message = "Senha incorreta" });

                return Created();
            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possivel processar a operação" });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var userId = AppUtils.GetIntTokenUserId(tokenRequest, _tokenService, _configuration);

                var isUserRemoved = await _userService.removeUser(userId);

                if (isUserRemoved is null)
                {
                    return NotFound(new MessageReturnDTO { Message = "Não foi possível localizar o usuário" });
                }

                return Created();

            }
            catch (Exception)
            {
                return BadRequest(new MessageReturnDTO { Message = "Não foi possivel processar a operação" });
            }
        }
    }
}
