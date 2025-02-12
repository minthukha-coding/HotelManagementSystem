using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Features.User
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

    }
}
