using Discount.Grpc;
using Discount.Grpc.Interfaces;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = configuration["ConnectionStrings:NpgsqlConnection"];
    Console.WriteLine(connection);
    if (string.IsNullOrEmpty(connection))
    {
        throw new ArgumentNullException("Connection string not found");
    }

    options.UseNpgsql(connection, subOptions => subOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});


builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddGrpc();

var app = builder.Build();


app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
