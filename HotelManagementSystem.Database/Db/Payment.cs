using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public decimal Tax { get; set; }

    public decimal Discount { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? PaymentDate { get; set; }
}
