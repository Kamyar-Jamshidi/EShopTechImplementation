using Autofac.Extensions.DependencyInjection;
using Autofac;
using EShopTI.Product.Common.Config;
using EShopTI.Product.Query.Core.UseCases.Category;
using EShopTI.Product.Query.Core.Data.Repositories;
using EShopTI.Product.Query.Infrastructure.Data.Repositories;
using EShopTI.Product.Query.Core.Data;
using EShopTI.Product.Query.Infrastructure.Data;
using EShopTI.Product.Query.Infrastructure.EventProcessing;
using EShopTI.Product.Query.Infrastructure.EventHandler;
using EShopTI.Product.Query.Infrastructure;
using GraphQL.Server.Ui.Voyager;
using EShopTI.Product.Query.Api.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterGeneric(typeof(PostgreSqlRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    builder.RegisterType<EventProcessor>().As<IEventProcessor>().SingleInstance();
    builder.RegisterType<EShopTI.Product.Query.Infrastructure.EventHandler.EventHandler>().As<IEventHandler>().InstancePerLifetimeScope();
});

builder.Services.Configure<RabitMQConfig>(builder.Configuration.GetSection(nameof(RabitMQConfig)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CategoryAddCommand).Assembly));
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddDbContext<ProductQueryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ProductQueryDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.);
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var context = builder.Services.BuildServiceProvider().GetService<ProductQueryDbContext>())
    await context.Database.EnsureCreatedAsync();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGraphQL();

app.UseGraphQLVoyager("/graphql-voyager", new VoyagerOptions()
{
    GraphQLEndPoint = "/graphql"
});

app.Run();
