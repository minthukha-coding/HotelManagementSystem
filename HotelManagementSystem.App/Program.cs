using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using System.Net.Mail;
using System.Net;
using HotelManagementSystem.Shared.Services;
using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.FileProviders;
using HotelManagementSystem.Domain.Features.Admin.Customer;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add Serilog to logging
builder.Host.UseSerilog();

builder.Services
    .AddFluentEmail("minthukha.dev@gmail.com") // Default sender email
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        Port = 587, // Change based on your SMTP provider
        Credentials = new NetworkCredential("minthukha.dev@gmail.com", "zaax qfki nure hslh"),
        EnableSsl = true
    });

try
{
    builder.Services.AddScoped<JwtSecurityTokenHandler>();
    builder.Services.AddScoped<JwtTokenService>();
    builder.Services.AddScoped<LocalStorageService>();
    builder.Services.AddScoped<JwtAuthStateProviderService>();
    builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProviderService>();
    builder.Services.AddScoped<CustomerService>();
    builder.Services.AddScoped<EmailService>();
    builder.Services.AddScoped<UserServices>();
    builder.Services.AddScoped<RoomService>();
    builder.Services.AddScoped<BookingService>();
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<DapperService>(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return new DapperService(connectionString!);
    });

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

// Middleware to log requests
app.UseSerilogRequestLogging();

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"D:\SharedUploads"),
    RequestPath = "/uploads"
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();