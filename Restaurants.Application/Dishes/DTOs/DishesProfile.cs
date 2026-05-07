using AutoMapper;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class DishesProfile : Profile
    {
        public DishesProfile() 
        {
            CreateMap<CreateDishCommand, Dish>();

            CreateMap<Dish, DishDTO>();
        }
    }
}
