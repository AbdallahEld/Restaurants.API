using MediatR;
using Restaurants.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDTO>>
    {
        public string? seachPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
