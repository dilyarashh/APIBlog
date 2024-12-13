using Quartz;

namespace socials.Services;

public class EmailQueueProcessingJob : IJob
{
    private readonly EmailQueueProcessor _emailQueueProcessor;

    public EmailQueueProcessingJob(EmailQueueProcessor emailQueueProcessor)
    {
        _emailQueueProcessor = emailQueueProcessor;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _emailQueueProcessor.ProcessQueueAsync();
    }
}