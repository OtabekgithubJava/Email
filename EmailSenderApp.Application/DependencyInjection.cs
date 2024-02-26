using EmailSenderApp.Application.Services.Email;
using Microsoft.Extensions.DependencyInjection;

namespace EmailSenderApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}