using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Domain.Features.Customer.Auth;
using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.Domain.Features.User;

public partial class UserServices
{
    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtService;
    private readonly ILogger<UserServices> _logger;

    public UserServices(AppDbContext context, ILogger<UserServices> logger, JwtTokenService jwtService)
    {
        _context = context;
        _logger = logger;
        _jwtService = jwtService;
    }

    public async Task<Result<UserModel>> Register(UserModel reqModel)
    {
        var item = await _context.Users.FirstOrDefaultAsync(u => u.Email == reqModel.Email);
        if (item != null)
        {
            return Result<UserModel>.FailureResult("User already exists.");
        }

        var user = new Database.Db.User
        {
            UserId = Guid.NewGuid().ToString(),
            Username = reqModel.Username,
            PasswordHash = reqModel.PasswordHash,
            Email = reqModel.Email,
            CreatedAt = DateTime.Now,
            Role = EnumRole.User.ToString()
        };


        _context.Users.Add(user);
        int response = await _context.SaveChangesAsync();

        if (response > 0)
        {
            return Result<UserModel>.SuccessResult("User registered successfully.");
        }
        else
        {
            return Result<UserModel>.FailureResult("Failed to register user.");
        }
    }

    public async Task<Result<LoginResponse>?> Login(string email, string password)
    {
        var model = new Result<LoginResponse>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                model = Result<LoginResponse>.FailureResult("User doesn't exists.");
                goto Result;
            }

            if (user.PasswordHash != password)
            {
                model = Result<LoginResponse>.FailureResult("Invalid password.");
                goto Result;
            }

            var reqTokenModel = new AccessTokenRequestModel
            {
                UserId = user.UserId,
                Email = user.Email!
            };

            var token = _jwtService.GenerateJwtToken(reqTokenModel);

            var response = new LoginResponse
            {
                Token = token,
                Customer = new UserModel
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                }
            };

            return Result<LoginResponse>.SuccessResult(response, "Login successful.");

        }
        catch (Exception ex)
        {
            _logger.LogError($"This is an error log message.{ex}");
        }
    Result:
        return model;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public UserModel Customer { get; set; } = new();
    }

}