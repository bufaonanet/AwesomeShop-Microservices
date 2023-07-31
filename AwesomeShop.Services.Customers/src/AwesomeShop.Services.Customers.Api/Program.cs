using AwesomeShop.Services.Customers.Application;
using AwesomeShop.Services.Customers.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddMediator()
    .AddMongo()
    .AddRepositories()
   // .AddRabbitMq()
    //.AddSubscribers()
    ;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();