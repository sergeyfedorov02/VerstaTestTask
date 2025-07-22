using Microsoft.EntityFrameworkCore;
using Radzen;
using VerstaTestTask;
using VerstaTestTask.Components;
using VerstaTestTask.Data;
using VerstaTestTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);

builder.Services.AddControllers();
builder.Services.AddRadzenComponents();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "VerstaTestTaskTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();

//Регистрация сервиса
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddDbContext<VerstaDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("VerstaConnection"));
});
builder.Services.AddTransient<Func<VerstaDbContext>>(provider =>
               () => provider.CreateScope().ServiceProvider.GetRequiredService<VerstaDbContext>());

var app = builder.Build();


var forwardingOptions = new ForwardedHeadersOptions()
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
};
forwardingOptions.KnownNetworks.Clear();
forwardingOptions.KnownProxies.Clear();

app.UseForwardedHeaders(forwardingOptions);
    

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

// Миграция базы данных
var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<VerstaDbContext>();
db.Database.SetCommandTimeout(60);
db.Database.Migrate();

app.Run();