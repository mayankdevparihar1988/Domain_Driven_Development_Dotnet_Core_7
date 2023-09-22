using System;
using Application.Dto.Image;
using Application.PipelineBehaviours.Contract;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Images.Queries
{
    public class GetImagesRequest : IRequest<List<ImageResponseDto>>, ICacheable, ILogging
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan SlidingExpiration { get; set; }

        public GetImagesRequest()
        {
            CacheKey = "GetImages";
        }
    }

    public class GetImagesRequestHandler : IRequestHandler<GetImagesRequest, List<ImageResponseDto>>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public GetImagesRequestHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<List<ImageResponseDto>> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            List<Image> images = await _imageRepo.GetAllAsync();
            if (images != null)
            {
                List<ImageResponseDto> imageDtos = _mapper.Map<List<ImageResponseDto>>(images);
                return imageDtos;
            }
            return null;
        }
    }
}

