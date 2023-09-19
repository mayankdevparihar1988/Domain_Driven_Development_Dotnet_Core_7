using System;
using Application.Dto.Image;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Images.Commands
{
    public class UpdateImageRequest : IRequest<bool>
    {
        public UpdateImageRequestDto _updateImage { get; set; }

        public UpdateImageRequest(UpdateImageRequestDto updateImage)
        {
            _updateImage = updateImage;
        }
    }

    public class UpdateImageRequestHandler : IRequestHandler<UpdateImageRequest, bool>
    {
        private readonly IImageRepo _imageRepo;

        public UpdateImageRequestHandler(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<bool> Handle(UpdateImageRequest request, CancellationToken cancellationToken)
        {
            Image imageInDb = await _imageRepo.GetByIdAsync(request._updateImage.Id);
            if (imageInDb != null)
            {
                imageInDb.Name = request._updateImage.Name;
                imageInDb.Path = request._updateImage.Path;

                await _imageRepo.UpdateAsync(imageInDb);
                return true;
            }
            return false;
        }
    }
}

