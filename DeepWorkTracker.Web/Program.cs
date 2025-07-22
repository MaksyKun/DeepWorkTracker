using DeepWorkTracker.Repository;
using DeepWorkTracker.Service;
using DeepWorkTracker.Web.Components;
using DeepWorkTracker.Web.Components.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

RepositoryInitializer.Initialize(builder.Services);
ServiceInitializer.Initialize(builder.Services);

builder.Services.AddScoped<DeepWorkSessionTracker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

ApplicationDbContext context;
using (var scope = app.Services.CreateScope())
{
    context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DBInitializer.Initialize(context);
}

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
