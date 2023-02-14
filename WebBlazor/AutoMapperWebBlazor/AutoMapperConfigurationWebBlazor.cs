using Application.ModelDto;
using Application.Service.Command;
using Application.Service.Queries;
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
               cfg.CreateMap<EmployeeModel, DeleteEmployeeCommand>().ReverseMap();
               cfg.CreateMap<LoginModel, GetEmployeeByEmailQuery>().ReverseMap();



               cfg.CreateMap<Request, RequestDto>().ReverseMap();
               cfg.CreateMap<RequestDto, RequestModel>().ReverseMap();
               cfg.CreateMap<RequestModel, CreateRequestCommand>().ReverseMap();
               cfg.CreateMap<RequestModel, EditRequestCommand>().ReverseMap();

               cfg.CreateMap<Role, RoleDto>().ReverseMap();
               cfg.CreateMap<RoleDto, RoleModel>().ReverseMap();





           }).CreateMapper();
    }
}
