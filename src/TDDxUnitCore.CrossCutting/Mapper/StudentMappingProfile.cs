using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.CrossCutting.Mapper
{
    public class StudentMappingProfile : Profile
    {

        public StudentMappingProfile()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(
                    dest => dest.Audience
                    , opt => opt.MapFrom(p => p.Audience.ToString())
                );

            CreateMap<StudentDTO, Student>()
                .ForMember(
                    dest => dest.Audience
                    , opt => opt.MapFrom(p =>Audience.CTO)
                );
        }
    }
}
