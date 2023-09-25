using System.Security.Claims;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;

namespace Cnc.Server.GraphQl;

public class UserIdRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context,
        IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        var userId =
            context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";

        requestBuilder.SetGlobalState("UserId", userId);

        return base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
    }
}