using HotelManagementSystem.CustomerApp.Components;
using HotelManagementSystem.Shared;
using HotelManagementSystem.Shared.Services;
using HotelManagementSystem.Shared.Services.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using System.Text;
using HotelManagementSystem.Domain.Features.Admin.Room;
using HotelManagementSystem.Domain.Features.Customer.CustomerInfo;
using HotelManagementSystem.Shared.Services.SignalRService;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

try
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true);
        });
    });
    builder.Services.AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    });
    builder.Services.AddScoped<ChatService>();
    
    builder.Services.AddScoped<JwtSecurityTokenHandler>();
    builder.Services.AddScoped<JwtTokenService>();
    builder.Services.AddScoped<LocalStorageService>();
    builder.Services.AddScoped<JwtAuthStateProviderService>();
    builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProviderService>();
    builder.Services.AddScoped<CustomerInfoService>();
    builder.Services.AddScoped<BookingService>();
    builder.Services.AddScoped<RoomService>();
    builder.Services.AddScoped<CustomerAuthServices>();
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<DapperService>(provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        return new DapperService(connectionString);
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

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCors("CorsPolicy");

app.MapHub<ChatHub>("/chatHub");

string folderPath = @"D:\SharedUploads";
Directory.CreateDirectory(folderPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(folderPath),
    RequestPath = "/uploads"
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseAuthorization();

app.Run();