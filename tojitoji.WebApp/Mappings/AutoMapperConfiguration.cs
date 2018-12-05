using AutoMapper;
using tojitoji.Model.Models;
using tojitoji.WebApp.Models;

namespace tojitoji.WebApp.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Account, AccountViewModel>();
                cfg.CreateMap<CompanyInformation, CompanyInformationViewModel>();
            });
        }
    }
}