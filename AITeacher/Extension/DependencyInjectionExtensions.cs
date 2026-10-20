using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infra.Messaging;
using Infra.Repositories;
using Infra.Services;
using Infrastructure;
using Infrastructure.Messaging;
using Microsoft.AspNetCore.Identity;

namespace AITeacher.Extension
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddHostedService<KafkaEmailConsumer>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>(); services.AddHttpClient<IEmailService, EmailService>();
            services.AddHttpClient<IGeminiService, GeminiService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
