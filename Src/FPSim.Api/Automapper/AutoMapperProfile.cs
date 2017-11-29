using System;
using AutoMapper;
using FPSim.Api.Model;
using FPSim.Data.Entity;

namespace FPSim.Api.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>()
                .ForMember(destination => destination.Image,
                           options => options.ResolveUsing(resolver => ImageFromBase64String(resolver.Image)));
        }

        private static byte[] ImageFromBase64String(string base64String)
        {
            // A default is image used when result = null
            byte[] result = null;

            if (!string.IsNullOrEmpty(base64String))
            {
                result = Convert.FromBase64String(base64String);
            }

            return result;
        }
    }
}
