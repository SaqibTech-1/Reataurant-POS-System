using AutoMapper;
using RestaurantPOS.API.DTOs;
using RestaurantPOS.API.Entities;

namespace RestaurantPOS.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            
            CreateMap<Table, TableDto>();
            CreateMap<CreateTableDto, Table>();
            
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();
            
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Role, RoleDto>();
            CreateMap<CreateRoleDto, Role>();


        }
    }
}
