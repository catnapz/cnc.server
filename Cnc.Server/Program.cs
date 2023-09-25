using Cnc.Server.GraphQl;
using Cnc.Data;
using Cnc.Server.GraphQl.Resolvers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsql<CncContext>(builder.Configuration.GetValue<string>("ConnectionStrings:Database"));

// GraphQL
builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<CncContext>(DbContextKind.Synchronized)
    .AddQueryType<QueryType>()
    .AddType<UserType>()
    .AddType<CampaignType>();

// Basic Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/healthz");
app.MapGraphQL("/api");
app.Run();