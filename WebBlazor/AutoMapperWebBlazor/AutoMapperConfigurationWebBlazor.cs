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
               cfg.CreateMap<EmployeeModel, CreateEmployeeCommand>().ReverseMap();
               cfg.CreateMap<CreateEmployeeCommand, EmployeeDto>().ReverseMap();
               cfg.CreateMap<EmployeeDto, EmployeeModel>().ReverseMap();
               cfg.CreateMap<EmployeeModel, EditEmployeeCommand>().ReverseMap();
               cfg.CreateMap<EditEmployeeCommand, Employee>().ReverseMap();



               cfg.CreateMap<Request, RequestDto>().ReverseMap();
               cfg.CreateMap<RequestDto, RequestModel>().ReverseMap();


           }).CreateMapper();
    }
}
