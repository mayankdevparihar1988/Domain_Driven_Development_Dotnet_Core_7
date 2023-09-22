
using System;
using Domain;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
	public class DeletePropertyValidator: AbstractValidator<int>
	{
    

        public DeletePropertyValidator(IPropertyRepo propertyRepo)
		{
            RuleFor(_id => _id)
                .NotNull();
            RuleFor(_id => _id)
                .MustAsync(async (id, ct) =>  await propertyRepo.GetByIdAsync(id) is Property existingProperty && existingProperty.Id == id)
                .WithMessage("Given ID not found");

        }
	}
}

