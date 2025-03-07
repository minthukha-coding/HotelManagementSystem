namespace HotelManagementSystem.Domain.Features.Customer.Auth;

public class CustomerServices
{
    private readonly AppDbContext _context;

    public CustomerServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CustomerModel>> Register(CustomerModel reqModel)
    {
        try
        {
            // Validate the request model
            if (reqModel == null)
                return Result<CustomerModel>.FailureResult("Request model is null.");

            // Check if the email is already registered
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == reqModel.Email);

            if (existingCustomer != null)
                return Result<CustomerModel>.FailureResult("Email is already registered.");

            // Map the request model to the Customer entity
            var customer = new Database.Db.Customer()
            {
                CustomerId = Guid.NewGuid().ToString(),
                FullName = reqModel.FullName,
                PhoneNumber = reqModel.PhoneNumber,
                Address = reqModel.Address,
                Email = reqModel.Email,
                CreatedAt = DateTime.UtcNow
            };

            // Add the customer to the database
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Return success result
            return Result<CustomerModel>.SuccessResult(reqModel, "Customer registered successfully.");
        }
        catch (Exception ex)
        {
            // Handle exceptions
            return Result<CustomerModel>.FailureResult(ex);
        }
    }
    
    public async Task<Result<CustomerModel>?> Login(string email, string password)
    {
        try
        {
            // Validate the input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return Result<CustomerModel>.FailureResult("Email and password are required.");

            // Find the customer by email
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
                return Result<CustomerModel>.FailureResult("Customer not found.");

            // Validate the password (you should use a proper password hashing mechanism)
            // For simplicity, this example assumes the password is stored in plain text (not recommended for production)
            // if (custome.pas != password) // Replace with proper password hashing logic
            //     return Result<CustomerModel>.FailureResult("Invalid password.");

            // Map the Customer entity to the CustomerModel
            var customerModel = new CustomerModel
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };

            // Return success result
            return Result<CustomerModel>.SuccessResult(customerModel, "Login successful.");
        }
        catch (Exception ex)
        {
            // Handle exceptions
            return Result<CustomerModel>.FailureResult(ex);
        }
    }
}