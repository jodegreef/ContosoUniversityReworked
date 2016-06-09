using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Features.Student
{
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Models.Student, Index.Model>();
        }
    }
}