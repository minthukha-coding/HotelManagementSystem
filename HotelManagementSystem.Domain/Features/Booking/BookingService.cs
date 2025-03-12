using HotelManagementSystem.Database.Db;

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

        public async Task<Result<BookingResponseModel>> GetBookingDeatilsById(string bookingId)
        {
            var bookings = await _dapperService.ExecuteQueryStoreProcedure<BookingModel>("GetBookingDeatilsById", new { BookingId = bookingId });

            if (!bookings.IsSuccess)
            {
                return Result<BookingResponseModel>.FailureResult(bookings.Message);
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
            }).FirstOrDefault();

            return Result<BookingResponseModel>.SuccessResult(responseModels, "BookingModels retrieved successfully.");
        }

        public async Task<Result<bool>> UserBookRoom(BookingModel reqModel)
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

        public async Task<Result<BookingConfirmationResponseModel>> BookingConfirm(BookingModel reqModel)
        {
            try
            {
                var item = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == reqModel.BookingId);
                if (item == null)
                {
                    return Result<BookingConfirmationResponseModel>.FailureResult("Booking not found.");
                }

                var duplicateBook = await _context.Bookings
                    .FirstOrDefaultAsync(x => x.RoomId == item.RoomId &&
                                         x.Status == "Confirmed" &&
                                         x.CheckInDate == item.CheckInDate &&
                                         x.CheckOutDate == item.CheckOutDate);
                if (duplicateBook != null)
                {
                    return Result<BookingConfirmationResponseModel>.FailureResult("Room already booked by other customer.");
                }

                item.Status = "Confirmed";
                _context.Bookings.Update(item);
                await _context.SaveChangesAsync();

                var bookingUser = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == item.CustomerId);

                var room = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomId == item.RoomId);

                var model = new BookingConfirmationResponseModel
                {
                    CustomerName = bookingUser.FullName,
                    BookingId = item.BookingId,
                    RoomType = room.Category,
                    BookingDate = item.CheckInDate,
                    BookingDetailsUrl = "https://hotelmanagement.com/booking-details/" + item.BookingId
                };
                return Result<BookingConfirmationResponseModel>.SuccessResult(model,"Booking Success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return Result<BookingConfirmationResponseModel>.SuccessResult();
        }

        public class BookingConfirmationResponseModel
        {
            public string CustomerName { get; set; }
            public string BookingId { get; set; }
            public string RoomType { get; set; }
            public DateTime BookingDate { get; set; }
            public string BookingDetailsUrl { get; set; }
        }
    }
}