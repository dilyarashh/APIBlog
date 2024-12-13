namespace socials.Services;

using Quartz;
using Quartz.Spi;
using System;

public class JobFactory : IJobFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JobFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        return (IJob)scope.ServiceProvider.GetService(bundle.JobDetail.JobType);
    }

    public void ReturnJob(IJob job) { }
}