using Autofac;
using Autofac.Extensions.DependencyInjection;
using EShopTI.Product.Command.Api.Models.ViewModels;
using EShopTI.Product.Command.Api.Models.ViewModels.Category;
using EShopTI.Product.Command.Api.Models.ViewModels.Product;
using EShopTI.Product.Command.Api.Models.ViewModels.Product.Variant;
using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Command.Core.UseCases.Category;
using EShopTI.Product.Command.Core.UseCases.Product;
using EShopTI.Product.Command.Core.UseCases.Product.ProductVariant;
using EShopTI.Product.Command.Infrastructure.Config;
using EShopTI.Product.Command.Infrastructure.Data.Repositories;
using EShopTI.Product.Command.Infrastructure.EventSourcing;
using EShopTI.Product.Common.Config;
using EShopTI.Product.Common.Domain.ValueObjects;
using EShopTI.Product.Common.MessageBus;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterGeneric(typeof(MongoDBCommandRepository<>)).As(typeof(ICommandRepository<>)).InstancePerLifetimeScope();
    builder.RegisterGeneric(typeof(MongoDBEventSourcingCommandRepository<>)).As(typeof(IEventSourcingCommandRepository<>)).InstancePerLifetimeScope();
    builder.RegisterGeneric(typeof(EventSourcing<>)).As(typeof(IEventSourcing<>)).InstancePerLifetimeScope();
    builder.RegisterType<MessageBusClient>().As<IMessageBusClient>().InstancePerLifetimeScope();
});

builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<RabitMQConfig>(builder.Configuration.GetSection(nameof(RabitMQConfig)));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CategoryAddCommand).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region APIs

#region Category

app.MapPost("/Category", async (CategoryAddVM model, IValidator<CategoryAddVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new CategoryAddCommand(model.Title);
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("AddCategory")
    .WithOpenApi();

app.MapPut("/Category", async (CategoryEditVM model, IValidator<CategoryEditVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new CategoryEditCommand(model.Id, model.Title);
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("EditCategory")
    .WithOpenApi();

app.MapDelete("/Category", async ([FromBody] CategoryRemoveVM model, IValidator<CategoryRemoveVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new CategoryRemoveCommand(model.Id);
    await mediator.Send(request);
    return new ApiResponse(ResponseStatus.Ok);
})
    .WithName("RemoveCategory")
    .WithOpenApi();

#endregion

#region Product

app.MapPost("/Product", async (ProductAddVM model, IValidator<ProductAddVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductAddCommand(model.Name, model.Categoryid, model.Quantity, new Money(model.PriceCurrency, model.PriceAmount));
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("AddProduct")
    .WithOpenApi();

app.MapPut("/Product", async (ProductEditVM model, IValidator<ProductEditVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductEditCommand(model.Id, model.Name, model.Categoryid, model.Quantity, new Money(model.PriceCurrency, model.PriceAmount));
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("EditProduct")
    .WithOpenApi();

app.MapDelete("/Product", async ([FromBody] ProductRemoveVM model, IValidator<ProductRemoveVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductRemoveCommand(model.Id);
    await mediator.Send(request);
    return new ApiResponse(ResponseStatus.Ok);
})
    .WithName("RemoveProduct")
    .WithOpenApi();

#region Product Variants

app.MapPost("/Product/Variant", async (ProductVariantAddVM model, IValidator<ProductVariantAddVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductVariantAddCommand(model.Color, model.Size, model.ProductId, model.Quantity, new Money(model.PriceCurrency, model.PriceAmount));
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("AddProductVariant")
    .WithOpenApi();

app.MapPut("/Product/Variant", async (ProductVariantEditVM model, IValidator<ProductVariantEditVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductVariantEditCommand(model.Id, model.Color, model.Size, model.ProductId, model.Quantity, new Money(model.PriceCurrency, model.PriceAmount));
    return new ApiResponse(ResponseStatus.Ok, await mediator.Send(request));
})
    .WithName("EditProductVariant")
    .WithOpenApi();

app.MapDelete("/Product/Variant", async ([FromBody]ProductVariantRemoveVM model, IValidator<ProductVariantRemoveVM> validator, IMediator mediator) =>
{
    var validation = await validator.ValidateAsync(model);
    if (!validation.IsValid)
        return new ApiResponse(ResponseStatus.BadRequest, errors: validation.Errors.Select(x => x.ErrorMessage).ToArray());

    var request = new ProductVariantRemoveCommand(model.Id, model.ProductId);
    await mediator.Send(request);
    return new ApiResponse(ResponseStatus.Ok);
})
    .WithName("RemoveProductVariant")
    .WithOpenApi();

#endregion

#endregion

#endregion

app.Run();