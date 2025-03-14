namespace HotelManagementSystem.Domain.Features.Admin.Customer;

public class CustomerModel
{
    public string CustomerId { get; set; }
    public string CustomerFullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string Address { get; set; }
}