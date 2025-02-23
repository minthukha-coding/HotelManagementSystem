using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Shared.Enum;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HotelManagementSystem.Domain.Features.Room;

public class RoomService
{
    private AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
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
        try
        {
            // Create a new Room entity
            var room = new Database.Db.Room
            {
                RoomNumber = roomModel.RoomNumber,
                Category = "Standard",
                Status = roomModel.Status,
                Price = roomModel.Price,
                Description = roomModel.Description
            };

            // Add the room to the context and save changes
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();

            // Get the newly generated RoomId
            var roomId = room.RoomId;

            // Ensure that PhotoUrls is not null and contains at least one photo
            if (roomModel.PhotoUrls == null || !roomModel.PhotoUrls.Any())
            {
                return Result<RoomModel>.FailureResult("At least one photo URL is required.");
            }

            // Create a new RoomPhoto entity
            var photo = new Database.Db.RoomPhoto
            {
                RoomId = roomId,
                PhotoUrl = roomModel.PhotoUrls[0].PhotoUrl,
                Description = roomModel.Description
            };

            // Add the photo to the context and save changes
            await _context.AddAsync(photo);
            await _context.SaveChangesAsync();

            // Update the RoomModel with the new RoomId
            roomModel.RoomId = roomId;

            // Return success result with the updated RoomModel
            return Result<RoomModel>.SuccessResult(roomModel, "Room added successfully.");
        }
        catch (Exception ex)
        {
            // Return failure result with the exception
            return Result<RoomModel>.FailureResult(ex);
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

    public async Task<Result<bool>> DeleteRoomAsync(int roomId)
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
        var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "roomphotos");

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

    public async Task<Result<RoomModel>> GetRoomByIdAsync(int roomId)
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

}