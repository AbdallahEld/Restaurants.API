using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
    {
        public string? seachPhrase { get; set; }
    }
}
