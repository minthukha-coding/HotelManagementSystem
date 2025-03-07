namespace HotelManagementSystem.Domain.Features.Customer.Auth;

public class CustomerModel
{
    public string? CustomerId { get; set; }
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public DateTime? CreatedAt { get; set; }
}