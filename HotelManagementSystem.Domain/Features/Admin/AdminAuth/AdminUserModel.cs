namespace HotelManagementSystem.Domain.Features.User
{
    public class AdminUserModel
    {
        public string UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

    }
}
