using System;
using Application.Dto;
using Application.Features.Properties.Commands;
using Application.Repositories;
using FluentValidation;

namespace Application.Features.Properties.Validators
{
	public class UpdatePropertyRequestValidator : AbstractValidator<UpdatePropertyRequest>
	{
        public UpdatePropertyRequestValidator(IPropertyRepo propertyRepo) => RuleFor(request => request._updatePropertyRequestDto)
                .SetValidator(new UpdatePropertyValidator(propertyRepo));
    }
}

