using AdeptusMart01.Core.Entities;
using AdeptusMart04.Api.Context;
using AdeptusMart04.Api.DTOs;
using AdeptusMart05.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AdeptusMart04.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginApiContext _context;
        public LoginController(LoginApiContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] AccountDTO accountDTO)
        {
            var response = await _context.LogInAsync(accountDTO.Username, accountDTO.Password);

            if (response == null)
            {
                return NotFound("No account found.");
            }
            return Ok(response);
        }
    }
}
