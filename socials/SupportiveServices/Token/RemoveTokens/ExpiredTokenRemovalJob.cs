using socials.DBContext;

namespace socials.Services;

using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ExpiredTokenRemovalJob : IJob
{
    private readonly AppDbcontext dbcontext;

    public ExpiredTokenRemovalJob(AppDbcontext dbcontext)
    {
        this.dbcontext = dbcontext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await RemoveExpiredTokensAsync();
    }

    private async Task RemoveExpiredTokensAsync()
    {
        var expirationTime = DateTime.UtcNow.AddDays(-1); // 24 часа назад
        var expiredTokens = dbcontext.BlackTokens.Where(t => t.CreatedAt < expirationTime);

        if (expiredTokens.Any())
        {
            dbcontext.BlackTokens.RemoveRange(expiredTokens);
            await dbcontext.SaveChangesAsync();
        }
    }
}