using Cnc.Data;
using Cnc.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cnc.Server.GraphQl.Resolvers;

public class UserType: ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.BindFields(BindingBehavior.Implicit);

        descriptor
            .Field("campaign")
            .Argument("id", a => a.Type<NonNullType<IntType>>())
            .ResolveWith<UserResolvers>(r => r.GetCampaignById(default!, default));
        
        descriptor
            .Field("campaigns")
            .ResolveWith<UserResolvers>(r => r.GetCampaigns(default!, default!, default));
    }
}