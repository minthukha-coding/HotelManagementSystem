namespace HotelManagementSystem.Domain.Features.Booking
{
    public class BookingService
    {
        private AppDbContext _context;
        private DapperService _dapperService;

        public BookingService(AppDbContext context, DapperService dapperService)
        {
            _context = context;
            _dapperService = dapperService;
        }

        //public async Task<Result<List<BookingResponseModel>>> GetAllBookinAsync()
        //{
        //    try
        //    {
        //        var booking = _dapperService.ExecuteQueryStoreProcedure<BookingModel>("GetAllBookingDetails");

        //        return Result<List<BookingResponseModel>>.SuccessResult("BookingModels retrieved successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Result<List<BookingResponseModel>>.FailureResult(ex);
        //    }
        //}

        public async Task<Result<List<BookingResponseModel>>> GetAllBookinAsync()
        {
            var bookings = await _dapperService.ExecuteQueryStoreProcedure<BookingModel>("GetAllBookingDetails");

            if (!bookings.IsSuccess)
            {
                return Result<List<BookingResponseModel>>.FailureResult(bookings.Message);
            }

            // If you need to transform BookingModel to BookingResponseModel
            var responseModels = bookings.Data.Select(b => new BookingResponseModel
            {
                BookingId = b.BookingId,
                CustomerName = b.CustomerName,
                Phone = b.Phone,
                PhoneNumber = b.Phone,
                RoomNumber = b.RoomNumber,
                Category = b.Category,
                Price = b.Price,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                Status = b.Status
            }).ToList();

            return Result<List<BookingResponseModel>>.SuccessResult(responseModels, "BookingModels retrieved successfully.");
        }

        public async Task<Result<bool>> BookRoom(BookingModel reqModel)
        {
            try
            {
                var booking = new Database.Db.Booking
                {
                    BookingId = NUlid.Ulid.NewUlid().ToString(),
                    RoomId = reqModel.RoomId,
                    CustomerId = reqModel.CustomerId, // This should be the actual user ID
                    CheckInDate = reqModel.CheckInDate ?? DateTime.UtcNow,
                    CheckOutDate = reqModel.CheckOutDate ?? DateTime.UtcNow,
                    Status = "Booked"
                };
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return Result<bool>.SuccessResult(true, "Room booked successfully.");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return Result<bool>.SuccessResult(true);
        }

    }
}
