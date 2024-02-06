using CartesianSquare.Services.SquareService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CartesianSquare.Shared.Response;
using CartesianSquare.Shared.Squares;
using System.Security.Claims;

namespace CartesianSquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SquaresController : ControllerBase
    {
        private readonly ISquareService squereService;

        public SquaresController(ISquareService squereService)
        {
            this.squereService=squereService;
        }
        [HttpPost]
        public async Task<ServerResponse<Square>> CreateSquare(CreateSquareDto data)
        {
            data.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var resp = await squereService.CreateSquareAsync(data);
            return resp;
        }
    }
}
