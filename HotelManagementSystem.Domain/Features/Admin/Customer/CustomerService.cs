using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.Domain.Features.Admin.Customer;

public class CustomerService
{
    private readonly AppDbContext _context;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(AppDbContext context, ILogger<CustomerService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<List<CustomerModel>>> GetCustomerUserList()
    {
        var customers = await _context.Customers
            .Where(x => x.DelFlag == 0)
            .ToListAsync();
        if (customers == null)
        {
            return Result<List<CustomerModel>>.FailureResult("No customer found.");
        }
        var customerList = customers.Select(c => new CustomerModel
        {
            CustomerId = c.CustomerId,
            CustomerFullName = c.FullName,
            Email = c.Email,
            Phone = c.PhoneNumber,
            Address = c.Address,
            CreatedAt = c.CreatedAt
        }).ToList();

        return Result<List<CustomerModel>>.SuccessResult(customerList, "Customer list fetched successfully.");
    }

    public async Task<Result<string>> DeleteCustomer(string customerId)
    {
        try
        {
            var customer = await _context.Customers
                .Where(x=>x.DelFlag == 0)
                .FirstOrDefaultAsync();
            if (customer == null)
            {
                return Result<string>.FailureResult("No customer found.");
            }
            customer.DelFlag = 1;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Result<string>.SuccessResult("Customer deleted successfully.");
        }
        catch (Exception ex)
        {
            return Result<string>.FailureResult($"Customer deleted fail with details - {ex.ToString()}");
        }
    }
}