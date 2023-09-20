using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;
using Application.PipelineBehaviours;

namespace Application
{
    public static class ServiceCollectionExtension
    {
		public static void AddApplicationServices(this IServiceCollection services)
        {
            services
                  .AddAutoMapper(Assembly.GetExecutingAssembly())
                  .AddMediatR(Assembly.GetExecutingAssembly())
                  .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                  .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>))
                  .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>));

        }
	}
}

