using HotelManagementSystem.App.Components.Pages.User.Booking;
using HotelManagementSystem.Database.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HotelManagementSystem.Domain.Features.Booking
{
    public class BookingService
    {
        private AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<BookingResponseModel>>> GetAllBookinAsync()
        {
            try
            {
                //var bookingModels = await _context.Bookings
                //                         .Join(
                //                             _context.Customers,
                //                             booking => booking.UserId, // Ensure this matches the column name in the Bookings table
                //                             customer => customer.CustomerId, // Ensure this matches the column name in the Customers table
                //                             (booking, customer) => new { Booking = booking, Customer = customer }
                //                         )
                //                         .Join(
                //                             _context.Rooms,
                //                             combined => combined.Booking.RoomId, // Ensure this matches the column name in the Bookings table
                //                             room => room.RoomId, // Ensure this matches the column name in the Rooms table
                //                             (combined, room) => new BookingResponseModel
                //                             {
                //                                 CustomerName = combined.Customer.FullName, // Fetch from Customers table
                //                                 PhoneNumber = combined.Customer.PhoneNumber!, // Use actual phone field
                //                                 RoomNumber = room.RoomNumber,
                //                                 Category = room.Category,
                //                                 Price = room.Price,
                //                                 CheckInDate = combined.Booking.CheckInDate,
                //                                 CheckOutDate = combined.Booking.CheckOutDate,
                //                                 Status = combined.Booking.Status
                //                             }
                //                         )
                //                         .ToListAsync();

                //return Result<List<BookingResponseModel>>.SuccessResult(bookingModels, "BookingModels retrieved successfully.");
                return Result<List<BookingResponseModel>>.SuccessResult("BookingModels retrieved successfully.");
            }
            catch (Exception ex)
            {
                return Result<List<BookingResponseModel>>.FailureResult(ex);
            }
        }

        public async Task<Result<bool>> BookRoom(int roomId)
        {
             var booking =  new Database.Db.Booking
             {
                RoomId = roomId,
                UserId = 1, // This should be the actual user ID
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                Status = "Booked"
            };
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return Result<bool>.SuccessResult(true, "Room booked successfully.");
        }
        
    }
}
