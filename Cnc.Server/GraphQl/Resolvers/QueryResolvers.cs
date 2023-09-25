using Cnc.Data;
using Cnc.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cnc.Server.GraphQl.Resolvers;

public class QueryResolvers
{
    public async Task<User> GetUser(CncContext dbContext, CancellationToken ct)
    {
        return await dbContext
            .Users
            .AsNoTracking()
            .SingleAsync(user => user.Id == "test", ct);
    }
}