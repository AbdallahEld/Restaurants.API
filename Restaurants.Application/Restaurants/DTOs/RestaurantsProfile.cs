using AutoMapper;
using Restaurants.Domain;

namespace Restaurants.Application
{
    public class RestaurantsProfile : Profile
    {
        public RestaurantsProfile() 
        {
            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, opt => opt.MapFrom(
                src => new Address
                {
                    Street = src.Street,
                    City = src.City,
                    PostalCode = src.PostalCode,
                }));

            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(d => d.AddressCity, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.AddressStreet, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.AddressPostalCode, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
