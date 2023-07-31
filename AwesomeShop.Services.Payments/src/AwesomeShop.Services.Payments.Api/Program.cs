using AwesomeShop.Services.Payments.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMongo()
    .AddRepositories()
    .AddPaymentGateway()
    .AddSubscribers();

var app = builder.Build();

app.MapGet("/", () => "Payment Services Working!");

app.Run();