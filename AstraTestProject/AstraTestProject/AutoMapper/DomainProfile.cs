using AstraTestProject.Contracts.Responses;
using AstraTestProject.Data.AstraTestProjectDb;
using AutoMapper;

namespace AstraTestProject.AutoMapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate.Date.ToShortDateString()))
                .ForMember(dest => dest.CarCost, opt => opt.MapFrom(src => src.Car.Cost))
                .ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Car.Model))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Car.Cost * src.Amount));
        }
    }
}
