using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Database.Db;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminDashboard> AdminDashboards { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<CustomerProfile> CustomerProfiles { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminDashboard>(entity =>
        {
            entity.HasKey(e => e.DashboardId).HasName("PK__AdminDas__5E2AEAE6AFB623BF");

            entity.ToTable("AdminDashboard");

            entity.Property(e => e.DashboardId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dashboard_id");
            entity.Property(e => e.ActiveCustomers).HasColumnName("active_customers");
            entity.Property(e => e.LastUpdated)
                .HasColumnType("datetime")
                .HasColumnName("last_updated");
            entity.Property(e => e.TotalBookings).HasColumnName("total_bookings");
            entity.Property(e => e.TotalRevenue)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("total_revenue");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__5DE3A5B1607714AB");

            entity.Property(e => e.BookingId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("booking_id");
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("booking_status");
            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.CheckOutDate).HasColumnName("check_out_date");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("room_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__room_i__403A8C7D");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__user_i__3F466844");
        });

        modelBuilder.Entity<CustomerProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Customer__AEBB701F9E4EA950");

            entity.Property(e => e.ProfileId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("profile_id");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerP__user___46E78A0C");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FCE53D582");

            entity.Property(e => e.NotificationId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("notification_id");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.NotificationType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("notification_type");
            entity.Property(e => e.SentAt)
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__user___4AB81AF0");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__ED1FC9EA387B882E");

            entity.Property(e => e.PaymentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_id");
            entity.Property(e => e.BookingId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("booking_id");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.TaxAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__bookin__440B1D61");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D90AC0CA612");

            entity.Property(e => e.ReviewId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("review_id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("room_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Room).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__room_id__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__user_id__4E88ABD4");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__19675A8A36A37096");

            entity.Property(e => e.RoomId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("room_id");
            entity.Property(e => e.AvailabilityStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("availability_status");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.PricePerNight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price_per_night");
            entity.Property(e => e.RoomCategory)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("room_category");
            entity.Property(e => e.RoomNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("room_number");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F190380B9");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
