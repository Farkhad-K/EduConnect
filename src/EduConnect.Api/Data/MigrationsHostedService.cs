using Microsoft.EntityFrameworkCore;

namespace EduConnect.Api.Data;

public class MigrationsHostedService(
    IServiceScopeFactory scopeFactory,
    ILogger<MigrationsHostedService> logger,
    IConfiguration configuration) : IHostedService
{
    private IEduConnectDbContext context = default!;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        context = scope.ServiceProvider.GetRequiredService<IEduConnectDbContext>();
        if (configuration.GetValue<bool>("MigrateDatabase"))
        {
            logger.LogInformation("Migrating database.");
            await context.Database.MigrateAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
