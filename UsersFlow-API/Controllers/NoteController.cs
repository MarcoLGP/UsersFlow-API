using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersFlow_API.DTOs;
using UsersFlow_API.Models;
using UsersFlow_API.Services;
using UsersFlow_API.Utils;

namespace UsersFlow_API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public NoteController(INoteService noteService, ITokenService tokenService, IConfiguration configuration)
        {
            _noteService = noteService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        private int GetIntTokenUserId(string tokenRequest)
        {
            var principal = _tokenService.GetClaimsPrincipalFromExpiredToken(tokenRequest, _configuration);
            var idUserToken = principal.FindFirstValue("Id");
            var intIdUser = int.Parse(idUserToken!);

            return intIdUser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDTO>>> Get()
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var intIdUser = GetIntTokenUserId(tokenRequest);
                var notes = await _noteService.getAllNotesByUser(intIdUser);

                return Ok(notes);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpGet]
        [Route(template: "{noteId:int}")]
        public async Task<ActionResult<IEnumerable<string>>> GetNoteContent(int noteId)
        {
            try
            {
                var content = await _noteService.getNoteContent(noteId);

                if (content is null)
                    return NotFound("Nota não encontrada");

                return Ok(content);
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewNoteDTO newNoteDTO)
        {
            try
            {
                var tokenRequest = AppUtils.RemovePrefixBearer(Request.Headers["Authorization"]!);
                var intIdUser = GetIntTokenUserId(tokenRequest);

                Note newNote = new Note()
                {
                    Title = newNoteDTO.Title,
                    Content = newNoteDTO.Content,
                    Created = newNoteDTO.Created,
                    UserId = intIdUser
                };

                await _noteService.addNote(newNote);
                return Created();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpDelete]
        [Route(template: "{noteId:int}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            try
            {
                var isDeleted = await _noteService.removeNote(noteId);

                if (isDeleted is null)
                    return NotFound("Nota não encontrada");

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

        [HttpPut]
        [Route(template: "{noteId:int}")]
        public async Task<ActionResult> PutNote(int noteId, [FromBody] NoteResumeDTO noteResumeDTO)
        {
            try
            {
                var isUpdated = await _noteService.updateNote(noteResumeDTO, noteId);

                if (isUpdated is null)
                    return NotFound("Nota não encontrada");

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível processar a operação");
            }
        }

    }
}
