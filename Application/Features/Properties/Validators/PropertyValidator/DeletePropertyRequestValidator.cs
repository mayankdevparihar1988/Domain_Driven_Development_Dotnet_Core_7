using System;
using Application.Features.Properties.Commands;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
	public class DeletePropertyRequestValidator : AbstractValidator<DeletePropertyRequest>
	{
		public DeletePropertyRequestValidator(IPropertyRepo propertyRepo)
		{
			RuleFor(request => request._id)
				.SetValidator(new DeletePropertyValidator(propertyRepo));
		}
	}
}

