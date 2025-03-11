using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.Domain.Features.Customer.Auth;

public class CustomerServices
{
    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtService;

    public CustomerServices(AppDbContext context, JwtTokenService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<Result<CustomerModel>> Register(CustomerModel reqModel)
    {
        try
        {
            if (reqModel == null!)
                return Result<CustomerModel>.FailureResult("Request model is null.");

            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == reqModel.Email);

            if (existingCustomer != null)
                return Result<CustomerModel>.FailureResult("Email is already registered.");

            var customer = new Database.Db.Customer()
            {
                CustomerId = Guid.NewGuid().ToString(),
                FullName = reqModel.FullName!,
                PhoneNumber = reqModel.PhoneNumber!,
                Address = reqModel.Address!,
                Email = reqModel.Email,
                CreatedAt = DateTime.UtcNow,
                Password = reqModel.Password
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Result<CustomerModel>.SuccessResult(reqModel, "Customer registered successfully.");
        }
        catch (Exception ex)
        {
            return Result<CustomerModel>.FailureResult(ex);
        }
    }

    public async Task<Result<LoginResponse>?> Login(string email, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return Result<LoginResponse>.FailureResult("Email and password are required.");

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email);

            if (customer == null)
                return Result<LoginResponse>.FailureResult("Customer not found.");

            if (customer.Password != password)
                return Result<LoginResponse>.FailureResult("Invalid password.");

            var reqTokenModel = new AccessTokenRequestModel
            {
                UserId = customer.CustomerId,
                Email = customer.Email!
            };

            var token = _jwtService.GenerateJwtToken(reqTokenModel);

            var response = new LoginResponse
            {
                Token = token,
                Customer = new CustomerModel
                {
                    CustomerId = customer.CustomerId,
                    FullName = customer.FullName,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                    Email = customer.Email,
                    CreatedAt = customer.CreatedAt,
                }
            };

            return Result<LoginResponse>.SuccessResult(response, "Login successful.");
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.FailureResult(ex);
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public CustomerModel Customer { get; set; } = new();
    }
}