using System;
using AutoMapper;
using Domain;
using Application.Dto;

namespace Application.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewPropertyRequest, Property>();

        }
    }
}

