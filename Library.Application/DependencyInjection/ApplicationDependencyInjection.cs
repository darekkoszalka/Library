using System;
using Library.Application.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}

