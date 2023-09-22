using System;
using Domain;
using Application.Dto.Image;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Application.PipelineBehaviours.Contract;

namespace Application.Features.Images.Commands
{
	public class CreateImageRequest : IRequest<bool>, IValidateable
	{

		public NewImageRequestDto _newImageRequestDto { get; set; }

		public CreateImageRequest(NewImageRequestDto newImageRequestDto)
		{
			_newImageRequestDto = newImageRequestDto;
		}

		
	}

    public class CreateImageRequestHandler : IRequestHandler<CreateImageRequest, bool>
    {
		private readonly IImageRepo _imageRepo;

		private readonly IMapper _mapper;

		public CreateImageRequestHandler(IImageRepo imageRepo, IMapper mapper)
		{
			_imageRepo = imageRepo;
			_mapper = mapper;
		}

        public async Task<bool> Handle(CreateImageRequest request, CancellationToken cancellationToken)
        {

			Image image = _mapper.Map<Image>(request._newImageRequestDto);

            await _imageRepo.AddNewAsync(image);

            return true;


        }
    }
}

