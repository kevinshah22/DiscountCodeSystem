using CodeClientApp.Components;
using DiscountCodeService;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddServerSideBlazor();

///Register gRPC client end points.
builder.Services.AddSingleton(services =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5000");
    return new DiscountService.DiscountServiceClient(channel);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapBlazorHub();

app.MapRazorComponents<App>();

app.Run();
