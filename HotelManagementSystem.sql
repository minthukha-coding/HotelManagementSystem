USE [master]
GO
/****** Object:  Database [MARB]    Script Date: 11/12/2024 12:53:20 AM ******/
CREATE DATABASE [MARB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MARB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MARB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MARB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MARB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MARB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MARB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MARB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MARB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MARB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MARB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MARB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MARB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MARB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MARB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MARB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MARB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MARB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MARB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MARB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MARB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MARB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MARB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MARB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MARB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MARB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MARB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MARB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MARB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MARB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MARB] SET  MULTI_USER 
GO
ALTER DATABASE [MARB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MARB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MARB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MARB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MARB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MARB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MARB', N'ON'
GO
ALTER DATABASE [MARB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MARB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MARB]
GO
/****** Object:  Table [dbo].[Tbl_AdminUser]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_AdminUser](
	[AdminUserId] [varchar](100) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[DesignationCode] [varchar](50) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[MobileNo] [varchar](50) NOT NULL,
	[UserType] [varchar](50) NOT NULL,
	[UserStatus] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [varchar](100) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [varchar](100) NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_AdminUser] PRIMARY KEY CLUSTERED 
(
	[AdminUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_ClassRegistration]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_ClassRegistration](
	[ClassId] [varchar](100) NOT NULL,
	[ClassCode] [varchar](100) NOT NULL,
	[ClassName] [varchar](100) NOT NULL,
	[AcademicYear] [varchar](100) NOT NULL,
	[LevelCode] [varchar](50) NOT NULL,
	[Status] [varchar](50) NULL,
	[OpenDate] [datetime] NOT NULL,
	[CloseDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [nvarchar](200) NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_ClassRegistration] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_CompanyInformation]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_CompanyInformation](
	[CompanyInformationId] [int] IDENTITY(1,1) NOT NULL,
	[Owner] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[CompanyType] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](1000) NOT NULL,
	[PhoneNo] [nvarchar](50) NOT NULL,
	[Slogan] [nvarchar](500) NOT NULL,
	[Sign] [nvarchar](100) NULL,
 CONSTRAINT [PK_Tbl_CompanyInformation] PRIMARY KEY CLUSTERED 
(
	[CompanyInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Designation]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Designation](
	[DesignationId] [varchar](100) NOT NULL,
	[DesignationCode] [varchar](50) NOT NULL,
	[DesignationName] [varchar](50) NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_FinalAcademicHistory]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_FinalAcademicHistory](
	[FinalAcademicHistoryId] [varchar](100) NOT NULL,
	[StudentNo] [varchar](100) NULL,
	[LevelStatus] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Year] [varchar](100) NULL,
	[DelFlag] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedUserId] [varchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [varchar](100) NULL,
 CONSTRAINT [PK_Tbl_FinalAcademicHistory] PRIMARY KEY CLUSTERED 
(
	[FinalAcademicHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_HonoursAcademicHistory]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_HonoursAcademicHistory](
	[HonoursAcademicHistoryId] [varchar](100) NOT NULL,
	[StudentNo] [varchar](100) NULL,
	[LevelStatus] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[Year] [varchar](100) NULL,
	[DelFlag] [bigint] NULL,
	[CreatedDate] [date] NULL,
	[CreatedUserId] [varchar](100) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedUserId] [varchar](100) NULL,
 CONSTRAINT [PK_Tbl_HonoursAcademicHistory] PRIMARY KEY CLUSTERED 
(
	[HonoursAcademicHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Level]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Level](
	[LevelId] [varchar](100) NOT NULL,
	[LevelCode] [varchar](50) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_Level] PRIMARY KEY CLUSTERED 
(
	[LevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_OutstandingRecord]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_OutstandingRecord](
	[OutstandingRecordId] [varchar](100) NOT NULL,
	[ClassCode] [varchar](100) NOT NULL,
	[StudentName] [varchar](100) NOT NULL,
	[StudentNo] [varchar](100) NOT NULL,
	[AcademicYear] [varchar](50) NOT NULL,
	[ThesisTitle] [varchar](100) NOT NULL,
	[ThesisDoc] [varchar](100) NULL,
	[Grade] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [nvarchar](200) NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_OutstandingRecord] PRIMARY KEY CLUSTERED 
(
	[OutstandingRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Report]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Report](
	[ReportId] [varchar](225) NOT NULL,
	[ReportName] [varchar](225) NOT NULL,
	[ReportData] [nvarchar](max) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[CreatedUserId] [varchar](225) NOT NULL,
 CONSTRAINT [PK_Tbl_Report] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Sequence]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Sequence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Field] [nvarchar](50) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Length] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[SequenceDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_Sequence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Student]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Student](
	[StudentUserId] [varchar](100) NOT NULL,
	[EnrollmentNo] [varchar](100) NOT NULL,
	[Password] [varchar](300) NOT NULL,
	[StudentInfoId] [varchar](100) NULL,
	[DelFlag] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [varchar](100) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [varchar](100) NULL,
 CONSTRAINT [PK_Tbl_Student] PRIMARY KEY CLUSTERED 
(
	[StudentUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_StudentAcademicHistory]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_StudentAcademicHistory](
	[StudentAcademicHistoryId] [varchar](100) NOT NULL,
	[ClassCode] [varchar](100) NOT NULL,
	[StudentNo] [varchar](100) NOT NULL,
	[LevelCode] [varchar](100) NOT NULL,
	[FirstSemStatus] [varchar](100) NULL,
	[SecondSemStatus] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Year] [varchar](100) NULL,
	[DelFlag] [bigint] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [varchar](100) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [varchar](100) NULL,
 CONSTRAINT [PK_Tbl_AcademicHistory] PRIMARY KEY CLUSTERED 
(
	[StudentAcademicHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_StudentInfo]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_StudentInfo](
	[StudentInfoId] [varchar](100) NOT NULL,
	[UserImage] [varchar](max) NULL,
	[CourseEnrollmentNo] [varchar](100) NOT NULL,
	[EnrollmentNo] [varchar](100) NOT NULL,
	[StudentNo] [varchar](100) NOT NULL,
	[StudentNameMM] [nvarchar](200) NOT NULL,
	[StudentNameEng] [varchar](200) NOT NULL,
	[OldNrc] [varchar](50) NULL,
	[NewNrc] [varchar](50) NULL,
	[Race] [nvarchar](100) NULL,
	[Religion] [nvarchar](100) NULL,
	[ProminentSign] [nvarchar](100) NULL,
	[BloodType] [varchar](10) NULL,
	[FatherName] [nvarchar](100) NULL,
	[MotherName] [nvarchar](100) NULL,
	[EducationQualification] [nvarchar](100) NULL,
	[IsComputer] [tinyint] NULL,
	[IsEnglish] [tinyint] NULL,
	[IsPali] [tinyint] NULL,
	[IsOther] [tinyint] NULL,
	[OtherSpecialization] [nvarchar](100) NULL,
	[IsMarried] [tinyint] NULL,
	[Spouse] [nvarchar](100) NULL,
	[DateOfBirthMMYear] [nvarchar](100) NULL,
	[DateOfBirthMM] [nvarchar](100) NULL,
	[DateOfBirthEng] [nvarchar](100) NULL,
	[TimeOfBirthPeriod] [nvarchar](100) NULL,
	[TOBHr] [int] NULL,
	[TOBMin] [int] NULL,
	[TOBSec] [int] NULL,
	[BirthPlace] [nvarchar](100) NULL,
	[Address] [nvarchar](100) NULL,
	[Job] [nvarchar](100) NULL,
	[OfficeAddress] [nvarchar](100) NULL,
	[OfficeMobileNo] [nvarchar](100) NULL,
	[HomeMobileNo] [nvarchar](100) NULL,
	[Email] [varchar](50) NULL,
	[LevelCode] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[Delflag] [tinyint] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedUserId] [varchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [varchar](100) NULL,
 CONSTRAINT [PK_StudentInfoNew] PRIMARY KEY CLUSTERED 
(
	[StudentInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_TeacherHistory]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_TeacherHistory](
	[TeacherHistoryId] [varchar](100) NOT NULL,
	[ClassCode] [varchar](100) NOT NULL,
	[TeacherCode] [varchar](100) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedUserId] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedUserId] [nvarchar](200) NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_TeacherHistory] PRIMARY KEY CLUSTERED 
(
	[TeacherHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_UserType]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_UserType](
	[UserTypeId] [varchar](100) NOT NULL,
	[UserTypeCode] [varchar](50) NOT NULL,
	[Description] [varchar](200) NOT NULL,
	[DelFlag] [int] NOT NULL,
 CONSTRAINT [PK_Tbl_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SP_GenerateSequenceCode]    Script Date: 11/12/2024 12:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[SP_GenerateSequenceCode] @FieldName VARCHAR(50) = NULL
as
begin

    declare @Seq int, @Code varchar(50), @Length int
    select @Seq = Sequence + 1, @Code = Code, @Length = Length
    from Tbl_Sequence
    where Field = @FieldName

    print (@Seq)
    print (@Code)
    print (@Length)

    update Tbl_Sequence
    set Sequence = @Seq
    where Field = @FieldName

    select @Code + CONCAT(REPLICATE('0', @Length - LEN(@Seq)), @Seq)
end
GO
USE [master]
GO
ALTER DATABASE [MARB] SET  READ_WRITE 
GO
