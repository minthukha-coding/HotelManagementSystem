using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Database.Db;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime? PaymentDate { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
