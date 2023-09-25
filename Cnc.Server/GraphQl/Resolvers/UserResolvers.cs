using Cnc.Data;
using Cnc.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cnc.Server.GraphQl.Resolvers;

public class UserResolvers
{
    /// <summary>
    /// Return a list of all campaigns under User query
    /// </summary>
    public async Task<IEnumerable<Campaign>> GetCampaigns(CncContext dbContext, [Parent] User user, CancellationToken ct) => 
        await dbContext
            .Campaigns
            .AsNoTracking()
            .Where(campaign => campaign.User.Id == user.Id)
            .ToListAsync(ct);

    /// <summary>
    /// Return a campaigns by campaignId under User query
    /// </summary>
    public async Task<Campaign> GetCampaignById(CncContext dbContext, [Parent] User user, int campaignId)
    {
        return await dbContext
            .Campaigns
            .AsNoTracking()
            .SingleAsync(campaign =>
                campaign.User.Id == user.Id &&
                campaign.Id == campaignId);
    }
}