namespace socials.Services;

using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Threading;
using System.Threading.Tasks;

public class QuartzHostedService : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IScheduler _scheduler;

    public QuartzHostedService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
        _scheduler = _schedulerFactory.GetScheduler().Result;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler.Shutdown(cancellationToken);
    }
}