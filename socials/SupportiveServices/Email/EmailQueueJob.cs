namespace socials.Services;

using Quartz;
using System.Threading.Tasks;

public class EmailQueueJob : IJob
{
    private readonly EmailQueueProcessor _emailQueueProcessor;

    public EmailQueueJob(EmailQueueProcessor emailQueueProcessor)
    {
        _emailQueueProcessor = emailQueueProcessor;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _emailQueueProcessor.ProcessQueueAsync();
    }
}