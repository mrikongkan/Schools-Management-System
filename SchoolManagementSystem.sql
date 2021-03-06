USE [master]
GO
/****** Object:  Database [schoolmanagementsystem]    Script Date: 5/27/2020 2:40:48 AM ******/
CREATE DATABASE [schoolmanagementsystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'schoolmanagementsystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.ANSOLUTIONS\MSSQL\DATA\schoolmanagementsystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'schoolmanagementsystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.ANSOLUTIONS\MSSQL\DATA\schoolmanagementsystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [schoolmanagementsystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [schoolmanagementsystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [schoolmanagementsystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [schoolmanagementsystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [schoolmanagementsystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [schoolmanagementsystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [schoolmanagementsystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [schoolmanagementsystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [schoolmanagementsystem] SET  MULTI_USER 
GO
ALTER DATABASE [schoolmanagementsystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [schoolmanagementsystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [schoolmanagementsystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [schoolmanagementsystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [schoolmanagementsystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [schoolmanagementsystem] SET QUERY_STORE = OFF
GO
USE [schoolmanagementsystem]
GO
/****** Object:  Table [dbo].[District]    Script Date: 5/27/2020 2:40:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[District](
	[DistrictID] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [varchar](50) NULL,
	[DivisionID] [int] NULL,
 CONSTRAINT [PK_Table_District] PRIMARY KEY CLUSTERED 
(
	[DistrictID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Division]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Division](
	[DivisionID] [int] IDENTITY(1,1) NOT NULL,
	[DivisionName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Table_Division] PRIMARY KEY CLUSTERED 
(
	[DivisionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GuirdianInformation]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuirdianInformation](
	[GuirdianID] [int] IDENTITY(1,1) NOT NULL,
	[GuirdianName] [varchar](100) NOT NULL,
	[GuirdianGender] [varchar](10) NOT NULL,
	[GuirdianOccupation] [varchar](50) NOT NULL,
	[GuirdianRelation] [varchar](50) NOT NULL,
	[GuirdianEmail] [varchar](100) NOT NULL,
	[GuirdianMobile] [varchar](15) NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Table_Guirdian_Informations] PRIMARY KEY CLUSTERED 
(
	[GuirdianID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImagesForAll]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImagesForAll](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermanentAddress]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermanentAddress](
	[PermanentAddressID] [int] IDENTITY(1,1) NOT NULL,
	[PermanentVillage] [varchar](50) NOT NULL,
	[PermanentRoad] [varchar](100) NOT NULL,
	[PermanentDistrict] [varchar](100) NOT NULL,
	[PermanentUpazila] [varchar](100) NOT NULL,
	[PermanentPostOffice] [varchar](50) NOT NULL,
	[PermanentPostCode] [varchar](10) NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Table_Student_Permanent_Info] PRIMARY KEY CLUSTERED 
(
	[PermanentAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PresentAddress]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PresentAddress](
	[PresentAddressID] [int] IDENTITY(1,1) NOT NULL,
	[PresentVllage] [varchar](50) NOT NULL,
	[PresentRoad] [varchar](100) NOT NULL,
	[PresentDistrict] [varchar](100) NOT NULL,
	[PresentUpazila] [varchar](100) NOT NULL,
	[PresentPostOffice] [varchar](50) NOT NULL,
	[PresentPostCode] [varchar](10) NOT NULL,
	[StudentID] [int] NOT NULL,
 CONSTRAINT [PK_Table_Student_Present_Info] PRIMARY KEY CLUSTERED 
(
	[PresentAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityQuestions]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityQuestions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionDetails] [varchar](500) NOT NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_SecurityQuestions] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentInformation]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentInformation](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [varchar](100) NOT NULL,
	[StudentFather'sName] [varchar](100) NOT NULL,
	[StudentMother'sName] [varchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[StudentAge] [varchar](50) NOT NULL,
	[StudentStandar] [varchar](10) NOT NULL,
	[SudentRollNo] [varchar](50) NOT NULL,
	[StudentBirthReg] [varchar](20) NOT NULL,
	[StudentEmail] [varchar](100) NOT NULL,
	[SudentSession] [varchar](10) NOT NULL,
	[StudentGender] [varchar](10) NOT NULL,
	[StudentMobile] [varchar](15) NULL,
	[StudentNationality] [varchar](50) NOT NULL,
	[ShiftID] [int] NOT NULL,
	[StudentAdmissionDate] [date] NOT NULL,
	[StudentDurations] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Table_Student_Informations] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentShiftInfo]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentShiftInfo](
	[ShiftID] [int] IDENTITY(1,1) NOT NULL,
	[ShiftName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_StudentShiftInfo] PRIMARY KEY CLUSTERED 
(
	[ShiftID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Upazila]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Upazila](
	[UpazilaID] [int] IDENTITY(1,1) NOT NULL,
	[UpazilaName] [varchar](50) NULL,
	[DistrictID] [int] NULL,
 CONSTRAINT [PK_Table_Upazila] PRIMARY KEY CLUSTERED 
(
	[UpazilaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserFullName] [varchar](100) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPassword] [varchar](100) NOT NULL,
	[UserGmail] [varchar](100) NOT NULL,
	[UserPhoneNo] [varchar](15) NOT NULL,
	[UserDesignation] [varchar](50) NOT NULL,
	[UserBirthDate] [date] NOT NULL,
	[UserRole] [varchar](10) NOT NULL,
	[UserRegDate] [date] NOT NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserQuestions]    Script Date: 5/27/2020 2:40:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQuestions](
	[SecurityQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionOne] [varchar](500) NULL,
	[AnswerOne] [varchar](100) NULL,
	[QuestionTwo] [varchar](500) NULL,
	[AnswerTwo] [varchar](100) NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_UserQuestions] PRIMARY KEY CLUSTERED 
(
	[SecurityQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GuirdianInformation]  WITH CHECK ADD  CONSTRAINT [FK_GuirdianInformation_StudentInformation] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentInformation] ([StudentID])
GO
ALTER TABLE [dbo].[GuirdianInformation] CHECK CONSTRAINT [FK_GuirdianInformation_StudentInformation]
GO
ALTER TABLE [dbo].[ImagesForAll]  WITH CHECK ADD  CONSTRAINT [FK_ImagesForAll_StudentInformation] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentInformation] ([StudentID])
GO
ALTER TABLE [dbo].[ImagesForAll] CHECK CONSTRAINT [FK_ImagesForAll_StudentInformation]
GO
ALTER TABLE [dbo].[PermanentAddress]  WITH CHECK ADD  CONSTRAINT [FK_PermanentAddress_StudentInformation] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentInformation] ([StudentID])
GO
ALTER TABLE [dbo].[PermanentAddress] CHECK CONSTRAINT [FK_PermanentAddress_StudentInformation]
GO
ALTER TABLE [dbo].[PresentAddress]  WITH CHECK ADD  CONSTRAINT [FK_PresentAddress_StudentInformation] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentInformation] ([StudentID])
GO
ALTER TABLE [dbo].[PresentAddress] CHECK CONSTRAINT [FK_PresentAddress_StudentInformation]
GO
ALTER TABLE [dbo].[StudentInformation]  WITH CHECK ADD  CONSTRAINT [FK_StudentInformation_StudentShiftInfo] FOREIGN KEY([ShiftID])
REFERENCES [dbo].[StudentShiftInfo] ([ShiftID])
GO
ALTER TABLE [dbo].[StudentInformation] CHECK CONSTRAINT [FK_StudentInformation_StudentShiftInfo]
GO
ALTER TABLE [dbo].[UserQuestions]  WITH CHECK ADD  CONSTRAINT [FK_UserQuestions_UserProfiles] FOREIGN KEY([UserID])
REFERENCES [dbo].[UserProfiles] ([UserId])
GO
ALTER TABLE [dbo].[UserQuestions] CHECK CONSTRAINT [FK_UserQuestions_UserProfiles]
GO
USE [master]
GO
ALTER DATABASE [schoolmanagementsystem] SET  READ_WRITE 
GO
