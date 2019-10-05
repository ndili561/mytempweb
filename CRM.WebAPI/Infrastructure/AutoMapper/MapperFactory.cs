using AutoMapper;
using CRM.DAL.Database.Entities.Customer;
using CRM.Dto.Customer;

namespace CRM.WebAPI.Infrastructure.AutoMapper
{
    public class MapperFactory : IServiceFactory<IMapper>
    {
        public IMapper Build()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<Person, PersonDto>().ReverseMap();
            });

            return configuration.CreateMapper();
        }
    }
}
