using DiscountCodeService.Services;
using DiscountCodeSystem.Data;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register the DbContext with SQL Server provider.
builder.Services.AddDbContext<DiscountDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DiscountCodeConnection")));

builder.Services.AddScoped<CodeStore>();

//Configure kestrel to use 5000 as port.
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000, o => o.Protocols = HttpProtocols.Http2);
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountDBContext>();
    db.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountServiceImpl>();
app.MapGet("/", () => "gRPC service is running...");

app.Run();
