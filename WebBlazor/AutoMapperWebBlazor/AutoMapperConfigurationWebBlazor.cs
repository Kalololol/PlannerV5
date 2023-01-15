using Application.ModelDto;
using Application.Service.Command;
using AutoMapper;
using Domain;
using WebBlazor.ModelWebBlazor;

namespace WebBlazor.AutoMapperWebBlazor
{
    public class AutoMapperConfigurationWebBlazor
    {
        public static IMapper Initialize()
           => new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
               cfg.CreateMap<EmployeeDto, CreateEmployeeCommand>().ReverseMap();
               cfg.CreateMap<AddEmployeeModel, CreateEmployeeCommand>().ReverseMap();

               

               cfg.CreateMap<Request, RequestDto>().ReverseMap();


           }).CreateMapper();
    }
}
