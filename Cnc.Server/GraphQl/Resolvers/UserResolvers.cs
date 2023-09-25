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
    public Campaign GetCampaignById([Parent] User user, int campaignId) => user.Campaigns
        .Single(campaign => campaign.Id == campaignId);
}