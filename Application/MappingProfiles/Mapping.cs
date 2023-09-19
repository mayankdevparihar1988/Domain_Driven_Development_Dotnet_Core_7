using System;
using AutoMapper;
using Domain;
using Application.Dto;
using Application.Dto.Image;

namespace Application.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewPropertyRequest, Property>();
            CreateMap<Property, PropertyResponseDto>();

            CreateMap<NewImageRequestDto, Image>();
            CreateMap<Image, ImageResponseDto>();

        }
    }
}

