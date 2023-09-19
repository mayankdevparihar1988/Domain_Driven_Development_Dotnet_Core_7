using System;
using Application.Repositories;
using Application.Dto.Image;
using AutoMapper;
using MediatR;
using Domain;

namespace Application.Features.Images.Queries
{
        public class GetImageByIdRequest : IRequest<ImageResponseDto>
        {
            public int ImageId { get; set; }

            public GetImageByIdRequest(int imageId)
            {
                ImageId = imageId;
            }
        }

        public class GetImageByIdRequestHandler : IRequestHandler<GetImageByIdRequest, ImageResponseDto>
        {
            private readonly IImageRepo _imageRepo;
            private readonly IMapper _mapper;

            public GetImageByIdRequestHandler(IImageRepo imageRepo, IMapper mapper)
            {
                _imageRepo = imageRepo;
                _mapper = mapper;
            }

            public async Task<ImageResponseDto> Handle(GetImageByIdRequest request, CancellationToken cancellationToken)
            {
                Image imageInDb = await _imageRepo.GetByIdAsync(request.ImageId);
                if (imageInDb != null)
                {
                ImageResponseDto imageDto = _mapper.Map<ImageResponseDto>(imageInDb);
                    return imageDto;
                }
                return null;
            }
        }
    
}

