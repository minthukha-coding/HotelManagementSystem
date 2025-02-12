
using HotelManagementSystem.Database.Db;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Domain.Features.User;

public class UserServices
{

    private readonly AppDbContext _context;

    public UserServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel> Register(UserModel user)
    {
        var model = new UserModel();
        //_context.Users.Add(model);
        var response = await _context.SaveChangesAsync();
        return model;
    }

    public async Task<UserModel?> Login(string email, string password)
    {
        var model = new UserModel();
        var response = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        return model;
    }

    public async Task<UserModel?> GetUserById(int id)
    {
        var model = new UserModel();
        var response = await _context.Users.FindAsync(id);
        return model;
    }
}