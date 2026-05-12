using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        [AllowAnonymous]
        public async Task<ActionResult<DishDTO>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(dishId, restaurantId));
            return Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDishForRestaurant([FromRoute] int restaurantId, CreateDishCommand command)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpDelete]  
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
