using Application.Interfaces;
using Confluent.Kafka;
using Domain.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infra.Messaging;

public class KafkaEmailConsumer(
    IServiceScopeFactory scopeFactory,
    ILogger<KafkaEmailConsumer> logger,
    IConfiguration configuration
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var bootstrapServers = configuration["Kafka:BootstrapServers"];
        var groupId = configuration["Kafka:GroupId"];
        var passwordResetTopic = configuration["Kafka:Topics:aiteacher"];

        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            EnableAutoOffsetStore = false
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        consumer.Subscribe(new[]
        {
            passwordResetTopic
        });

        logger.LogInformation("Consumidor de e-mails iniciado para o grupo {GroupId} ouvindo o tópico {Topic}.", groupId, passwordResetTopic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                try
                {
                    using var scope = scopeFactory.CreateScope();

                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                    if (result.Topic == passwordResetTopic)
                    {
                        var emailEvent = JsonSerializer.Deserialize<PasswordResetRequestedEvent>(result.Message.Value);

                        if (emailEvent is null ||
                            string.IsNullOrWhiteSpace(emailEvent.Email) ||
                            string.IsNullOrWhiteSpace(emailEvent.Code))
                        {
                            logger.LogWarning("Evento de recuperação inválido.");
                            consumer.Commit(result);
                            continue;
                        }

                        await emailService.SendPasswordResetEmail(
                            emailEvent.Email,
                            emailEvent.Code);

                        logger.LogInformation("E-mail de recuperação processado com sucesso.");
                    }

                    consumer.Commit(result);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Falha ao processar mensagem do tópico {Topic}.",
                        result.Topic);

                    consumer.Seek(result.TopicPartitionOffset);

                    await Task.Delay(
                        TimeSpan.FromSeconds(3),
                        stoppingToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Consumidor de e-mails encerrado.");
        }
        finally
        {
            consumer.Close();
        }
    }
}