namespace HotelManagementSystem.Domain.Features.User;

public partial class UserServices
{
    private readonly AppDbContext _context;
    private readonly ILogger<UserServices> _logger;

    public UserServices(AppDbContext context, ILogger<UserServices> logger)
    {
        _context = context;
        _logger = logger;
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

    public async Task<Result<UserModel>?> Login(string email, string password)
    {
        var model = new Result<UserModel>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                model = Result<UserModel>.FailureResult("User doesn't exists.");
                goto Result;
            }

            if (user.PasswordHash != password)
            {
                model = Result<UserModel>.FailureResult("Invalid password.");
                goto Result;
            }
            model = Result<UserModel>.SuccessResult(new UserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            }, "Login successful.");
            _logger.LogError("This is an error log message.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"This is an error log message.{ex}");
        }
    Result:
        return model;
    }
}