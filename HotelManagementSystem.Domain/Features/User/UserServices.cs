using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Shared;
using HotelManagementSystem.Shared.Enum;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelManagementSystem.Domain.Features.User;

public class UserServices
{
    private readonly AppDbContext _context;

    public UserServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<UserModel>> Register(UserModel reqModel)
    {
        var model = new Result<UserModel>();

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

        model = Result<UserModel>.Success("Register");

        return model;
    }

    public async Task<Result<UserModel>?> Login(string email, string password)
    {
        var model = new Result<UserModel>();
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        if (user == null)
        {
            model = new Result<UserModel>
            {
                IsSuccess = false,
                Message = "Invalid email or password"
            };
            goto Result;
        }
        model = new Result<UserModel>
        {
            IsSuccess = true,
            Data = new UserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            }
        };
    Result:
        return model;
    }

    public async Task<Result<UserModel>> GetUserDetails()
    {
        var model = new Result<UserModel>
        {
            IsSuccess = true,
            Data = new UserModel
            {
            }
        };
        return model;
    }
}