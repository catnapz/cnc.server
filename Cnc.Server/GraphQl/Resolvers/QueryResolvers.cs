using Cnc.Data;
using Cnc.Data.Models;

namespace Cnc.Server.GraphQl.Resolvers;

public class QueryResolvers
{
    /// <summary>
    /// Gets the current authenticated (via Firebase) user from the database.
    /// </summary>
    /// <remarks>
    /// Has side-effect of adding the user to the database when first accessed
    /// </remarks>
    /// <param name="userId">The user-id of the currently authenticated user</param>
    /// <param name="dbContext">EntityFramework Database context</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<User> GetUser([GlobalState("UserId")] string userId, CncContext dbContext, CancellationToken ct)
    {
        var user = await dbContext.Users.FindAsync(new object[] { userId }, ct);
        if (user != null) return user;

        var entry = await dbContext.Users.AddAsync(new User { Id = userId }, ct);
        await dbContext.SaveChangesAsync(ct);

        return entry.Entity;
    }
}