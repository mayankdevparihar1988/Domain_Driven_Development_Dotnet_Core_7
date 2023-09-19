using System;
using MediatR;
using Application.Dto;
using Application.Repositories;
using AutoMapper;
using Domain;

namespace Application.Features.Properties.Commands
{
	public class CreatePropertyRequest : IRequest<bool>
	{
		public NewPropertyRequest _newProperty { get; set; }

        public CreatePropertyRequest(NewPropertyRequest newProperty)
		{
           _newProperty = newProperty;
           
        }
	}

    public class CreatePropertyHandler : IRequestHandler<CreatePropertyRequest, bool>
    {

        public IMapper _mapper { get; set; }

        public IPropertyRepo _propertyRepo { get; set; }

        public CreatePropertyHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatePropertyRequest request, CancellationToken cancellationToken)
        {
            Property property = _mapper.Map<Property>(request._newProperty);

            property.ListDate = DateTime.Now;

            await _propertyRepo.AddNewAsync(property);

            return true;
        }
    }
}

