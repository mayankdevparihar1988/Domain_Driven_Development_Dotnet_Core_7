using System;
using Application.Dto;
using Application.PipelineBehaviours.Contract;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
namespace Application.Features.Properties.Commands
{
	public class DeletePropertyRequest : IRequest<int>, IValidateable
	{
        public int _id { get; set; }

        public DeletePropertyRequest( int id)
        {
			_id = id;
		}
    }

    public class DeletePropertyHandler : IRequestHandler<DeletePropertyRequest, int>
    {

        public IMapper _mapper { get; set; }

        public IPropertyRepo _propertyRepo { get; set; }

        public DeletePropertyHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
           var propertyToDelete = await _propertyRepo.GetByIdAsync(request._id);

            if(propertyToDelete is null)
            {
                return 0;
            }
            
            return await _propertyRepo.DeleteAsync(propertyToDelete);

        }
    }
}

