using Cnc.Data;
using Cnc.Server.GraphQl;
using Cnc.Server.GraphQl.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Firebase Auth
// https://blog.markvincze.com/secure-an-asp-net-core-api-with-firebase/
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var projectId = builder.Configuration.GetSection("Firebase")["ProjectId"];
        var domain = $"https://securetoken.google.com/{projectId}";

        options.Authority = domain;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = domain,
            ValidateAudience = true,
            ValidAudience = projectId,
            ValidateLifetime = true
        };
    });

// Database
builder.Services.AddNpgsql<CncContext>(builder.Configuration.GetValue<string>("ConnectionStrings:Database"));

// GraphQL
builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<CncContext>(DbContextKind.Synchronized)
    .AddAuthorization()
    .AddHttpRequestInterceptor<UserIdRequestInterceptor>()
    .AddQueryType<QueryType>()
    .AddType<UserType>()
    .AddType<CampaignType>();

// Basic Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz");

app.MapGraphQL("/api");

app.Run();