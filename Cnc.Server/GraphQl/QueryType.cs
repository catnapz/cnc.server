using Cnc.Server.GraphQl.Resolvers;

namespace Cnc.Server.GraphQl;

public class QueryType: ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Authorize()
            .Field("user")
            .ResolveWith<QueryResolvers>(r => r.GetUser(default!, default!, default!));
    }
}

