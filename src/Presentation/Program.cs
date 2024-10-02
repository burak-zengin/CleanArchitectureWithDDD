using MediatR;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.CustomSchemaIds(_ => _.FullName);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/api/products", (
    IMediator mediator,
    Application.Products.Create.Command command,
    CancellationToken cancellationToken) =>
{
    return mediator.Send(command, cancellationToken);
})
.WithOpenApi();

//app.MapGet("/api/products/{id}", (
//    IMediator mediator,
//    Application.Products.GetById.Query query, 
//    CancellationToken cancellationToken) =>
//{
//    return mediator.Send(query, cancellationToken);
//})
//.WithOpenApi();

app.MapGet("/api/products", (
    IMediator mediator,
    int page,
    int take,
    CancellationToken cancellationToken) =>
{
    return mediator.Send(new Application.Products.GetAll.Query(page, take), cancellationToken);
})
.WithOpenApi();

//app.MapPost("/api/products/{id}/productitems", (
//    IMediator mediator,
//    Application.Products.AddProductItem.Command command,
//    CancellationToken cancellationToken) =>
//{
//    return mediator.Send(command, cancellationToken);
//})
//.WithOpenApi();

//app.MapDelete("/api/products/{id}/productitems/{itemId}", (
//    IMediator mediator,
//    Application.Products.RemoveProduct.Command command,
//    CancellationToken cancellationToken) =>
//{
//    return mediator.Send(command, cancellationToken);
//})
//.WithOpenApi();

app.Run();