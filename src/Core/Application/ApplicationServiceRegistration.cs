using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Application.Pipelines.Validation;
using Application.Pipelines.Caching;
using Application.Services.TokenService;
using Domain.Models.Mail;
using Azure.Communication.Email;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ------------------------- RabbitMQ ------------------------- 
        //var factory = new ConnectionFactory()
        //{
        //    HostName = configuration["RabbitMq:HostName"],
        //    Port = string.IsNullOrEmpty(configuration["RabbitMq:Port"]) == false ? Int16.Parse(configuration["RabbitMq:Port"]!) : 5672,
        //    UserName = configuration["RabbitMq:UserName"],
        //    Password = configuration["RabbitMq:Password"],
        //    DispatchConsumersAsync = true
        //};


        // ------------------------- AutoMapper ------------------------- 
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        

        // ------------------------- Fluent Validation ------------------------- 
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        // ------------------------- MediatR------------------------- 
        services.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            //// ****** Pipeline Injection ****** 
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
        });


        // ------------------------- Azure Mail Service ------------------------- 
        AzureMailServiceSettings azureMailServiceSettings = new()
        {
            ConnectionStringMailService = configuration["Azure:ConnectionStringMailService"] ?? throw new Exception("Azure Mail Service Could not read access connection informations!"),
            DefaultMailSenderAddress = configuration["Azure:DefaultMailSenderAddress"] ?? throw new Exception("Azure Default Mail Sender Address Could not read!"),
        }; 
        services.AddSingleton<EmailClient>(new EmailClient(azureMailServiceSettings.ConnectionStringMailService));
        services.AddSingleton<AzureMailServiceSettings>(azureMailServiceSettings);


        // ------------------------- Service Injections ------------------------- 
        services.AddScoped<TokenService>();
        return services;
    }
}
