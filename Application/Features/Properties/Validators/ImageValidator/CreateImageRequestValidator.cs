using System;
using Application.Features.Images.Commands;
using FluentValidation;

namespace Application.Features.Properties.Validators.ImageValidator
{
	public class CreateImageRequestValidator : AbstractValidator<CreateImageRequest>
	{
		public CreateImageRequestValidator()
		{
			RuleFor(req => req._newImageRequestDto)
				.SetValidator(new NewImageValidator());
		}
	}
}

