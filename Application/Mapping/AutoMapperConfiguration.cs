using Application.ModelDto;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public static IMapper Initialize()
           => new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Employee, EmployeeDto>().ReverseMap();
               cfg.CreateMap<Request, RequestDto>().ReverseMap();

           }).CreateMapper();
    }
}
