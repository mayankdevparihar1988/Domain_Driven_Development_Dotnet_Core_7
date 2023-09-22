using System;
using Application.Dto;
using Application.Repositories;
using Domain;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
	public class UpdatePropertyValidator : AbstractValidator<UpdatePropertyRequestDto>
	{
		public UpdatePropertyValidator(IPropertyRepo propertyRepo)
		{
			RuleFor(upr => upr.Address)
				.NotEmpty()
				.WithMessage("Address is required!");
			RuleFor(upr => upr.Id)
				.MustAsync(async (id, ct) => await propertyRepo.GetByIdAsync(id) is Property existingPropert && existingPropert.Id == id)
				.WithMessage("Do not find the given ID");
				
		}
	}
}

