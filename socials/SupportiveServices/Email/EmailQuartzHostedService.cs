using Quartz;

namespace socials.Services;

public class EmailQuartzHostedService : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private IScheduler _scheduler;
    private readonly EmailQueueProcessor _emailQueueProcessor; 

    public EmailQuartzHostedService(ISchedulerFactory schedulerFactory, EmailQueueProcessor emailQueueProcessor)
    {
        _schedulerFactory = schedulerFactory;
        _emailQueueProcessor = emailQueueProcessor;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        await _scheduler.Start(cancellationToken);

        var job = JobBuilder.Create<EmailQueueJob>() 
            .WithIdentity("EmailQueueJob", "EmailJobs")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("EmailQueueTrigger", "EmailJobs")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithInterval(TimeSpan.FromMinutes(10)) 
                .RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(job, trigger);

        var queueJob = JobBuilder.Create<EmailQueueProcessingJob>()
            .WithIdentity("EmailQueueProcessingJob", "EmailJobs")
            .Build();

        var nextRunTime = DateTime.Today.AddDays(1); 
        nextRunTime = nextRunTime.AddHours(0); 

        var queueTrigger = TriggerBuilder.Create()
            .WithIdentity("EmailQueueProcessingTrigger", "EmailJobs")
            .StartAt(nextRunTime) 
            .WithSimpleSchedule(x => x
                .WithInterval(TimeSpan.FromDays(1)) 
                .RepeatForever())
            .Build();

        await _scheduler.ScheduleJob(queueJob, queueTrigger);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _scheduler?.Shutdown(cancellationToken);
    }
}