using System;
using Domain;
using MediatR;
using Application.Dto;
using Application.Repositories;
using AutoMapper;
using Application.PipelineBehaviours.Contract;

namespace Application.Features.Properties.Commands
{
	public class UpdatePropertyRequest : IRequest<bool>, IValidateable
	{
		public UpdatePropertyRequestDto _updatePropertyRequestDto { get; set; }

		public UpdatePropertyRequest(UpdatePropertyRequestDto updatePropertyRequestDto)
		{
			_updatePropertyRequestDto = updatePropertyRequestDto;
		}

	}

    public class UpdatePropertyRequestHandler : IRequestHandler<UpdatePropertyRequest, bool>
    {
        public IMapper _mapper { get; set; }

        public IPropertyRepo _propertyRepo { get; set; }

        public UpdatePropertyRequestHandler(IMapper mapper, IPropertyRepo propertyRepo)
        {
            _mapper = mapper;
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(UpdatePropertyRequest request, CancellationToken cancellationToken)
        {
            var newPropertyData = request._updatePropertyRequestDto;
            if (newPropertyData is null)
            {
                return false;
            }

            var propertyTobeUpdated = await _propertyRepo.GetByIdAsync(newPropertyData.Id);

            if(propertyTobeUpdated != null)
            {
                propertyTobeUpdated.Address = newPropertyData.Address;
                propertyTobeUpdated.Bathrooms = newPropertyData.Bathrooms;
                propertyTobeUpdated.Bedrooms = newPropertyData.Bedrooms;
                propertyTobeUpdated.Description = newPropertyData.Description;
                propertyTobeUpdated.Dining = newPropertyData.Dining;
                propertyTobeUpdated.ErfSize = newPropertyData.ErfSize;
                propertyTobeUpdated.FloorSize = newPropertyData.FloorSize;
                propertyTobeUpdated.Kitchens = newPropertyData.Kitchens;
                propertyTobeUpdated.Levies = newPropertyData.Levies;
                propertyTobeUpdated.Louge = newPropertyData.Louge;
                propertyTobeUpdated.Price = newPropertyData.Price;
                propertyTobeUpdated.Name = newPropertyData.Name;

                await _propertyRepo.UpdateAsync(propertyTobeUpdated);

                return true;
            }

            return false;
        }
    }


}

