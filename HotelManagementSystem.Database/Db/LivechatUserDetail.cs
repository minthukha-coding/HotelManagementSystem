using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class LivechatUserDetail
{
    public string? LivechatUserDetailsId { get; set; }

    public string? UserId { get; set; }

    public string? ConnectionId { get; set; }

    public string? ToUserId { get; set; }

    public string? ToUserConnectionId { get; set; }

    public string? Message { get; set; }

    public string? CreatedDateTime { get; set; }
}
