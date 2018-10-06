using DAL.Dto;
using DAL.Mapper.Interfaces;
using Models;
using MapBuilder = AutoMapper.Mapper;

namespace DAL.Mapper
{
    public class EntityBuilder : IEntityBuilder
    {
        public EntityBuilder()
        {
            MapInitialization();
        }

        private void MapInitialization()
        {
            System.Action<AutoMapper.IMapperConfigurationExpression> config = cfg =>
            {
                cfg.CreateMap<CategoryDto, Category>().ReverseMap();
                cfg.CreateMap<CustomerCustomerDemoDto, CustomerCustomerDemo>().ReverseMap();
                cfg.CreateMap<CustomerDemographicsDto, CustomerDemographics>().ReverseMap();
                cfg.CreateMap<CustomerDto, Customer>().ReverseMap();
                cfg.CreateMap<EmployeeDto, Employee>().ReverseMap();
                cfg.CreateMap<EmployeeTerritoriesDto, EmployeeTerritories>().ReverseMap();
                cfg.CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
                cfg.CreateMap<OrderDto, Order>().ReverseMap();
                cfg.CreateMap<ProductDto, Product>().ReverseMap();
                cfg.CreateMap<RegionDto, Region>().ReverseMap();
                cfg.CreateMap<ShipperDto, Shipper>().ReverseMap();
                cfg.CreateMap<SupplierDto, Supplier>().ReverseMap();
                cfg.CreateMap<TerritoryDto, Territory>().ReverseMap();
            };
                
            MapBuilder.Initialize(config);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return MapBuilder.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, System.Action<TSource, TDestination> afterOptionsMap)
        {
            return MapBuilder.Map<TSource, TDestination>(source, opt => opt.AfterMap(afterOptionsMap));
        }
    }
}
