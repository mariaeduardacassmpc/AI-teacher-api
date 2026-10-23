using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infra.Messaging;
using Infra.Repositories;
using Infra.Services;
using Infrastructure;
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
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddHostedService<KafkaEmailConsumer>();
            services.AddSingleton<IKafkaProducer, KafkaProducer>(); services.AddHttpClient<IEmailService, EmailService>();
            services.AddHttpClient<IGeminiService, GeminiService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
