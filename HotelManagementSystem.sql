USE [master]
GO
/****** Object:  Database [HotelManagementSystem]    Script Date: 3/14/2025 7:53:23 PM ******/
CREATE DATABASE [HotelManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HotelManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HotelManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HotelManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HotelManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HotelManagementSystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HotelManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HotelManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HotelManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HotelManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HotelManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HotelManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET RECOVERY FULL 
GO
ALTER DATABASE [HotelManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [HotelManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HotelManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HotelManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HotelManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HotelManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HotelManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HotelManagementSystem', N'ON'
GO
ALTER DATABASE [HotelManagementSystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [HotelManagementSystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HotelManagementSystem]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [varchar](200) NOT NULL,
	[CustomerId] [varchar](200) NOT NULL,
	[RoomID] [varchar](200) NOT NULL,
	[CheckInDate] [datetime] NOT NULL,
	[CheckOutDate] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Price] [decimal](18, 0) NULL,
	[NumberOfDays] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [nvarchar](255) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[Email] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[DelFlag] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationID] [nvarchar](255) NOT NULL,
	[UserID] [int] NOT NULL,
	[Message] [nvarchar](255) NOT NULL,
	[SentAt] [datetime] NULL,
	[Status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [nvarchar](255) NOT NULL,
	[BookingID] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Tax] [decimal](10, 2) NOT NULL,
	[Discount] [decimal](10, 2) NOT NULL,
	[TotalAmount] [decimal](10, 2) NOT NULL,
	[PaymentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[ReviewID] [nvarchar](255) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomPhotos]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomPhotos](
	[PhotoId] [nvarchar](255) NOT NULL,
	[RoomId] [nvarchar](255) NOT NULL,
	[PhotoUrl] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomID] [nvarchar](255) NOT NULL,
	[RoomNumber] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Description] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [nvarchar](200) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings] ADD  CONSTRAINT [DF__Bookings__Create__4E88ABD4]  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getdate()) FOR [SentAt]
GO
/****** Object:  StoredProcedure [dbo].[GetAllBookingDetails]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllBookingDetails]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        b.BookingId,
        c.FullName AS CustomerName,
        c.PhoneNumber AS Phone,  -- Changed to match model property
        r.RoomNumber,
        r.Category,
        r.Price,
        b.CheckInDate,
        b.CheckOutDate,
        b.Status
    FROM Bookings b
    INNER JOIN Customers c ON b.CustomerId = c.CustomerID
    INNER JOIN Rooms r ON b.RoomId = r.RoomId
    WHERE b.Status = 'Booked';
END
GO
/****** Object:  StoredProcedure [dbo].[GetBookingDeatilsById]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBookingDeatilsById]
    @BookingId VARCHAR(100) 
AS  
BEGIN  
    SET NOCOUNT ON;  

    SELECT  
        b.BookingId,  
        c.FullName AS CustomerName,  
        c.PhoneNumber AS Phone,  
        r.RoomNumber,  
        r.Category,  
        r.Price,  
        b.CheckInDate,  
        b.CheckOutDate,  
        b.Status  
    FROM Bookings b  
    INNER JOIN Customers c ON b.CustomerId = c.CustomerID  
    INNER JOIN Rooms r ON b.RoomId = r.RoomId  
    WHERE @BookingId IS NULL OR b.BookingId = @BookingId;  
END
GO
/****** Object:  StoredProcedure [dbo].[GetConfirmedBookings]    Script Date: 3/14/2025 7:53:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetConfirmedBookings]
AS
BEGIN
    -- Select confirmed bookings with customer and room details
    SELECT
        bk.BookingID,
        cs.FullName AS CustomerName,
        cs.PhoneNumber AS Phone,
        rm.RoomNumber AS RoomNumber,
        rm.Category,
        bk.Price,
        bk.CheckInDate,
        bk.CheckOutDate,
        bk.Status,
        bk.NumberOfDays
    FROM 
        Bookings bk
    LEFT JOIN 
        Rooms rm ON bk.RoomID = rm.RoomID
    LEFT JOIN 
        Customers cs ON cs.CustomerID = bk.CustomerID
    WHERE 
        bk.Status = 'Confirmed';
END;
GO
USE [master]
GO
ALTER DATABASE [HotelManagementSystem] SET  READ_WRITE 
GO
