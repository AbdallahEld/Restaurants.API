using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class GetAllRestaurantsQueryHandler(
        ILogger<GetAllRestaurantsQueryHandler> logger,
        IMapper mapper,
        IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDTO>>
    {
        public async Task<PagedResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantRepository.GetAllMatching(request.seachPhrase, request.PageSize, request.PageNumber);

            var restaurantsDTO = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            var result = new PagedResult<RestaurantDTO>(restaurantsDTO, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
