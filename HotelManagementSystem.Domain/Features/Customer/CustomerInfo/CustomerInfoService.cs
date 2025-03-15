using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Domain.Features.Customer.CustomerInfo;

public class CustomerInfoService
{
    private readonly AppDbContext _context;

    public CustomerInfoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CustomerInfoModel>> GetCustomerData(string customerId)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        if (customer == null) { return Result<CustomerInfoModel>.FailureResult("Customer not found."); }
        return Result<CustomerInfoModel>.SuccessResult(new CustomerInfoModel
        {
            CustomerId = customer.CustomerId,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            Email = customer.Email
        });
    }
}
