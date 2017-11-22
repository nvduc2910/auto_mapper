using AutoMapper_Demo.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using AutoMapper;
using AutoMapper_Demo.Models.Entities;

namespace AutoMapper_Demo.ViewModels
{
    public class StudentDetailViewModel : IHaveCustomMappings
    {

        public string CustomName { get; set; }
        public string CustomAge { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<StudentDetailViewModel,Student>()
                .ForMember(m => m.Name, opt => opt.MapFrom(u => u.CustomName))
                .ForMember(m => m.Name, opt => opt.MapFrom(u => u.CustomAge));
        }
    }
}
