using Cnc.Data.Models;

namespace Cnc.Server.GraphQl.Types;

public class CampaignType : ObjectType<Campaign>
{
    protected override void Configure(IObjectTypeDescriptor<Campaign> descriptor)
    {
        descriptor.BindFields(BindingBehavior.Implicit);
        descriptor.Ignore(campaign => campaign.User);
        descriptor.Ignore(campaign => campaign.UserId);
    }
}