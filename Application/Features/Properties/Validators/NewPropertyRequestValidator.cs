using System;
using Application.Dto;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
	public class NewPropertyRequestValidator : AbstractValidator<NewPropertyRequest>
	{
		public NewPropertyRequestValidator()
		{

			RuleFor(npr => npr.Name)
				.NotEmpty().WithMessage("Name is Required!!")
				.MaximumLength(200).WithMessage("Max Lenght is 200")
				.MinimumLength(5).WithMessage("MinmumLenght 5 Character");
		}
	}
}

