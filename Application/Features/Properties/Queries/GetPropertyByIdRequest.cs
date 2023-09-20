using System;
using Application.Dto;
using Application.PipelineBehaviours.Contract;
using Application.Repositories;
using AutoMapper;
using MediatR;
namespace Application.Features.Properties.Queries
{
	public class GetPropertyByIdRequest : IRequest<PropertyResponseDto>, ICacheable
	{
		public int _Id { get; set; }
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpiration { get; set; }

        public GetPropertyByIdRequest( int Id)
		{
			_Id = Id;
			CacheKey = $"GetPropertyById:{_Id}";
		}
	}

    public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdRequest, PropertyResponseDto>
    {
		private readonly IMapper _mapper;
		private readonly IPropertyRepo _propertyRepo;

		public GetPropertyByIdRequestHandler(IMapper mapper, IPropertyRepo propertyRepo)
        {
			_mapper = mapper;
			_propertyRepo = propertyRepo;
        }

        public async Task<PropertyResponseDto> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
        {
			var Id = request._Id;

			var retrieveProperty = await _propertyRepo.GetByIdAsync(Id);

            PropertyResponseDto mappedProperty =  _mapper.Map<PropertyResponseDto>(retrieveProperty);

			return mappedProperty;
        }

		
    }
}

