using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using SocialMediaClean.Application.Common.Behaviors;


namespace SocialMediaClean.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        /*
         * MediatR.Extensions.Microsoft.DependencyInjectionExtensions
         * FluentValidation.DependencyInjectionExtensions
         */

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


        return services;
    }
}

