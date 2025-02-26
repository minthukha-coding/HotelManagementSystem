using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Domain.Features.User;

public class AdminUserServices
{
    private readonly AppDbContext _context;

    public AdminUserServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<AdminUserModel>> Register(AdminUserModel reqModel)
    {
        var item = await _context.Users.FirstOrDefaultAsync(u => u.Email == reqModel.Email);
        if (item != null)
        {
            return Result<AdminUserModel>.FailureResult("User already exists.");
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
            return Result<AdminUserModel>.SuccessResult("User registered successfully.");
        }
        else
        {
            return Result<AdminUserModel>.FailureResult("Failed to register user.");
        }
    }

    public async Task<Result<AdminUserModel>?> Login(string email, string password)
    {
        var model = new Result<AdminUserModel>();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            model = Result<AdminUserModel>.FailureResult("User doesn't exists.");
            goto Result;
        }

        if (user.PasswordHash != password)
        {
            model = Result<AdminUserModel>.FailureResult("Invalid password.");
            goto Result;
        }

        model = Result<AdminUserModel>.SuccessResult(new AdminUserModel
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        }, "Login successful.");
    Result:
        return model;
    }
}