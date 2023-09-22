using System;
using Application.Dto.Image;
using FluentValidation;

namespace Application.Features.Properties.Validators.ImageValidator
{
	public class NewImageValidator: AbstractValidator<NewImageRequestDto>
	{
		public NewImageValidator()
		{
			RuleFor(newImageRequestDto => newImageRequestDto.Name)
				.NotEmpty()
				.WithMessage("Image name is a required field");
			RuleFor(newImageRequestDto => newImageRequestDto.Path)
                    .NotEmpty()
                .WithMessage("Image path is a required field");

		}
	}
}

