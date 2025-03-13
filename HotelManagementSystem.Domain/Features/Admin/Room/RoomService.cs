using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Domain.Features.Booking;
using HotelManagementSystem.Shared;

namespace HotelManagementSystem.Domain.Features.Room;

public class RoomService
{
    private AppDbContext _context;
    private DapperService _dapperService;

    public RoomService(AppDbContext context, DapperService dapperService)
    {
        _context = context;
        _dapperService = dapperService;
    }

    public async Task<Result<List<RoomModel>>> GetRoomModelsAsync()
    {
        try
        {
            var roomModels = await _context.Rooms
                       .Select(room => new RoomModel
                       {
                           RoomId = room.RoomId,
                           RoomNumber = room.RoomNumber,
                           Category = room.Category,
                           Status = room.Status,
                           Price = room.Price,
                           Description = room.Description
                       })
                       .ToListAsync();
            return Result<List<RoomModel>>.SuccessResult(roomModels, "RoomModels retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Result<List<RoomModel>>.FailureResult(ex);
        }
    }

    public async Task<Result<RoomModel>> AddRoomAsync(RoomModel roomModel)
    {
        if (roomModel == null)
        {
            return Result<RoomModel>.FailureResult("Room model cannot be null.");
        }

        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(roomModel.RoomNumber))
            {
                return Result<RoomModel>.FailureResult("Room number is required.");
            }

            if (roomModel.Price <= 0)
            {
                return Result<RoomModel>.FailureResult("Price must be greater than zero.");
            }

            if (roomModel.PhotoUrls == null || !roomModel.PhotoUrls.Any())
            {
                return Result<RoomModel>.FailureResult("At least one photo URL is required.");
            }

            // Create a new Room entity
            var roomId = Ulid.NewUlid().ToString();
            var room = new Database.Db.Room
            {
                RoomId = roomId,
                RoomNumber = roomModel.RoomNumber.Trim(), // Clean input
                Category = roomModel.Category, // Consider making this configurable or part of RoomModel
                Status = "Available",
                Price = roomModel.Price,
                Description = roomModel.Description?.Trim() // Handle null description
            };

            // Use a transaction for atomic operations
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Add room
                await _context.AddAsync(room);

                // Add photos
                var photoEntities = roomModel.PhotoUrls.Select(photoUrl => new Database.Db.RoomPhoto
                {
                    PhotoId = Ulid.NewUlid().ToString(),
                    RoomId = roomId,
                    PhotoUrl = photoUrl.PhotoUrl?.Trim(), // Clean URL
                    Description = photoUrl.Description?.Trim() ?? roomModel.Description?.Trim() // Use photo-specific desc if available
                }).ToList();

                if (photoEntities.Any(p => string.IsNullOrWhiteSpace(p.PhotoUrl)))
                {
                    return Result<RoomModel>.FailureResult("Photo URLs cannot be empty.");
                }

                await _context.AddRangeAsync(photoEntities);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // Update RoomModel with generated ID
                roomModel.RoomId = roomId;

                return Result<RoomModel>.SuccessResult(roomModel, "Room added successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw; // Re-throw to be caught by outer catch
            }
        }
        catch (Exception ex)
        {
            // Log the exception (assuming you have a logger)
            //_logger.LogError(ex, "Failed to add room: {RoomNumber}", roomModel.RoomNumber);

            return Result<RoomModel>.FailureResult(ex.Message);
        }
    }

    public async Task<Result<RoomModel>> UpdateRoomAsync(RoomModel roomModel)
    {
        try
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomId == roomModel.RoomId);
            if (room == null)
                return Result<RoomModel>.FailureResult(new Exception("Room not found."));

            room.RoomNumber = roomModel.RoomNumber;
            room.Category = roomModel.Category;
            room.Status = roomModel.Status;
            room.Price = roomModel.Price;
            room.Description = roomModel.Description;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return Result<RoomModel>.SuccessResult(roomModel, "Room updated successfully.");
        }
        catch (Exception ex)
        {
            return Result<RoomModel>.FailureResult(ex);
        }
    }

    public async Task<Result<bool>> DeleteRoomAsync(string roomId)
    {
        try
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.RoomId == roomId);
            if (room == null)
                return Result<bool>.FailureResult(new Exception("Room not found."));

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Result<bool>.SuccessResult(true, "Room deleted successfully.");
        }
        catch (Exception ex)
        {
            return Result<bool>.FailureResult(ex);
        }
    }

    public async Task<Result<List<RoomModel>>> GetAvailableRoomsByCategoryAsync(RoomModel reqModel)
    {
        try
        {
            var roomModels = await _context.Rooms
                .Where(room => room.Status == EnumRoomStatus.Available.ToString() && room.Category == reqModel.Category)
                .Select(room => new RoomModel
                {
                    RoomId = room.RoomId,
                    RoomNumber = room.RoomNumber,
                    Category = room.Category,
                    Status = room.Status,
                    Price = room.Price,
                    Description = room.Description
                })
                .ToListAsync();

            return Result<List<RoomModel>>.SuccessResult(roomModels, "Available rooms retrieved successfully.");
        }
        catch (Exception ex)
        {
            return Result<List<RoomModel>>.FailureResult(ex);
        }

    }

    public async Task<string> UploadFileAsync(IBrowserFile file)
    {
        // Define the local directory to save the images
        //var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "roomphotos");
        var uploadDirectory = Path.Combine("D:\\SharedUploads", "roomphotos");

        // Ensure the directory exists
        if (!Directory.Exists(uploadDirectory))
        {
            Directory.CreateDirectory(uploadDirectory);
        }

        // Generate a unique file name to avoid conflicts
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);
        var filePath = Path.Combine(uploadDirectory, fileName);

        // Save the file to the local path
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.OpenReadStream().CopyToAsync(stream);
        }

        // Return the relative path to the file (for use in the database)
        return $"/uploads/roomphotos/{fileName}";
    }

    public async Task<Result<RoomModel>> GetRoomByIdAsync(string roomId)
    {
        try
        {
            var roomDetails = _context.Rooms
                            .Where(room => room.RoomId == roomId) // Filter by roomId
                            .GroupJoin(
                                _context.RoomPhotos, // The collection to join with
                                room => room.RoomId, // Key from the Rooms table
                                photo => photo.RoomId, // Key from the RoomPhotos table
                                (room, photos) => new RoomModel
                                {
                                    RoomId = room.RoomId,
                                    RoomNumber = room.RoomNumber,
                                    Category = room.Category,
                                    Status = room.Status,
                                    Price = room.Price,
                                    Description = room.Description,
                                    PhotoUrls = photos.Select(p => new RoomPhotoModel // Project the photos
                                    {
                                        PhotoUrl = p.PhotoUrl
                                    }).ToList()
                                })
                            .FirstOrDefault();

            return Result<RoomModel>.SuccessResult(roomDetails, "Room details fetched successfully.");
        }
        catch (Exception ex)
        {
            return Result<RoomModel>.FailureResult(ex);
        }
    }

    public async Task<Result<List<RoomModel>>> GetAllRoomForUserRoomListing()
    {
        try
        {
            // Fetch all rooms with their photos
            var roomDetails = await _context.Rooms
                .GroupJoin(
                    _context.RoomPhotos, // The collection to join with
                    room => room.RoomId, // Key from the Rooms table
                    photo => photo.RoomId, // Key from the RoomPhotos table
                    (room, photos) => new RoomModel
                    {
                        RoomId = room.RoomId,
                        RoomNumber = room.RoomNumber,
                        Category = room.Category,
                        Status = room.Status,
                        Price = room.Price,
                        Description = room.Description,
                        PhotoUrls = photos.Select(p => new RoomPhotoModel // Project the photos
                        {
                            PhotoUrl = p.PhotoUrl
                        }).ToList()
                    })
                .ToListAsync(); // Fetch all rooms as a list

            return Result<List<RoomModel>>.SuccessResult(roomDetails, "Room details fetched successfully.");
        }
        catch (Exception ex)
        {
            return Result<List<RoomModel>>.FailureResult(ex);
        }
    }

    public async Task UpdateRoomStatusesAsync()
    {
        var currentDate = DateTime.Now;

        // Get all rooms that are currently booked
        var occupiedRoomIds = await _context.Bookings
            .Where(b => b.CheckInDate <= currentDate && b.CheckOutDate >= currentDate)
            .Select(b => b.RoomId)
            .Distinct()
            .ToListAsync();

        // Update rooms to 'Occupied' if they are booked
        var roomsToOccupy = await _context.Rooms
            .Where(r => occupiedRoomIds.Contains(r.RoomId))
            .ToListAsync();

        foreach (var room in roomsToOccupy)
        {
            room.Status = "Occupied";
        }

        // Update rooms to 'Available' if they are not booked
        var roomsToMakeAvailable = await _context.Rooms
            .Where(r => !occupiedRoomIds.Contains(r.RoomId))
            .ToListAsync();

        foreach (var room in roomsToMakeAvailable)
        {
            room.Status = "Available";
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
    }

}