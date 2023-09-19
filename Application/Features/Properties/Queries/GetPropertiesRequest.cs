using System;
using Application.Dto;
using MediatR;
using AutoMapper;
using Application.Repositories;

namespace Application.Features.Properties.Queries
{
	public class GetPropertiesRequest : IRequest<List<PropertyResponseDto>>
	{
		public GetPropertiesRequest()
		{
		}
	}

    public class GetPropertiesRequestHandler : IRequestHandler<GetPropertiesRequest, List<PropertyResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepo _propertyRepo;

        public GetPropertiesRequestHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;

        }

        public async Task<List<PropertyResponseDto>> Handle(GetPropertiesRequest request, CancellationToken cancellationToken)
        {

            var properties = await _propertyRepo.GetAllAsync();

            if (properties is null) return null;

            List<PropertyResponseDto> mappedProperties = _mapper.Map<List<PropertyResponseDto>>(properties);

            return mappedProperties;

        }
    }
}

