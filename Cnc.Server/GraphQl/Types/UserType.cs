using Cnc.Data.Models;
using Cnc.Server.GraphQl.Resolvers;

namespace Cnc.Server.GraphQl.Types;

public class UserType: ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.BindFields(BindingBehavior.Implicit);

        descriptor
            .Authorize()
            .Field("campaign")
            .Argument("id", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<UserResolvers>(r => r.GetCampaignById(default!, default!, default));
        
        descriptor
            .Authorize()
            .Field("campaigns")
            .ResolveWith<UserResolvers>(r => r.GetCampaigns(default!, default!, default));
    }
}