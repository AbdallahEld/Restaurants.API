using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantController (IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "HasNationality")]
        public async Task<ActionResult<RestaurantDTO>> GetById([FromRoute] int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantCommand command)
        {
            command.Id = id;
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }
        [HttpGet("checkAuth")]
        [Authorize(Policy = PolicyNames.CreatedAtleast2Restaurants)]
        public async Task<IActionResult> TestAuthRequirement()
        {
            return Ok("You are authorized to access this endpoint.");
        }
    }
}
