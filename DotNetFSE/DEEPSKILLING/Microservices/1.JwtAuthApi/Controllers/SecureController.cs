using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureController : ControllerBase
    {
        [Authorize]

        [HttpGet]

        public IActionResult GetSecret()
        {
            return Ok("JWT Authentication Successful");
        }
    }
}