using Microsoft.AspNetCore.Mvc;
using UsersFlow_API.Context;


namespace UsersFlow_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public UserController(ApiDbContext context)
        {
            _context = context;
        }
    }
}
