using System;
using Application.Dto;
using Application.Repositories;
using AutoMapper;
using MediatR;
namespace Application.Features.Properties.Queries
{
	public class GetPropertyByIdRequest : IRequest<PropertyResponseDto>
	{
		public int _Id { get; set; }

		public GetPropertyByIdRequest( int Id)
		{
			_Id = Id;
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

