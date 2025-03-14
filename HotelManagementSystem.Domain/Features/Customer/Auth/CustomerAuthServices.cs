using HotelManagementSystem.Shared.Services.JwtService;

namespace HotelManagementSystem.Domain.Features.Customer.Auth;

public class CustomerAuthServices
{
    private readonly AppDbContext _context;
    private readonly JwtTokenService _jwtService;

    public CustomerAuthServices(AppDbContext context, JwtTokenService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<Result<CustomerAuthModel>> Register(CustomerAuthModel reqAuthModel)
    {
        try
        {
            if (reqAuthModel == null!)
                return Result<CustomerAuthModel>.FailureResult("Request model is null.");

            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == reqAuthModel.Email);

            if (existingCustomer != null)
                return Result<CustomerAuthModel>.FailureResult("Email is already registered.");

            var customer = new Database.Db.Customer()
            {
                CustomerId = Ulid.NewUlid().ToString(),
                FullName = reqAuthModel.FullName!,
                PhoneNumber = reqAuthModel.PhoneNumber!,
                Address = reqAuthModel.Address!,
                Email = reqAuthModel.Email,
                CreatedAt = DateTime.UtcNow,
                Password = reqAuthModel.Password
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Result<CustomerAuthModel>.SuccessResult(reqAuthModel, "Customer registered successfully.");
        }
        catch (Exception ex)
        {
            return Result<CustomerAuthModel>.FailureResult(ex);
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
                CustomerAuth = new CustomerAuthModel
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
        public CustomerAuthModel CustomerAuth { get; set; } = new();
    }
}