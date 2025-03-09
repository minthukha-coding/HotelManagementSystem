using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using System.Net.Mail;
using System.Net;
using HotelManagementSystem.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add Serilog to logging
builder.Host.UseSerilog();

builder.Services
    .AddFluentEmail("studyplannerhub@gmail.com") // Default sender email
    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
    {
        Port = 587, // Change based on your SMTP provider
        Credentials = new NetworkCredential("studyplannerhub@gmail.com", "nhyr ysyd owwk jama"),
        EnableSsl = true
    });

try
{
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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();