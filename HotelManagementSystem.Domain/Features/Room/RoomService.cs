using HotelManagementSystem.Database.Db;
using HotelManagementSystem.Shared.Enum;
using Microsoft.EntityFrameworkCore;

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
            var room = new Database.Db.Room
            {
                RoomNumber = roomModel.RoomNumber,
                Category = roomModel.Category.ToString(),
                Status = roomModel.Status,
                Price = roomModel.Price,
                Description = roomModel.Description
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            roomModel.RoomId = room.RoomId;
            return Result<RoomModel>.SuccessResult(roomModel, "Room added successfully.");
        }
        catch (Exception ex)
        {
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
}