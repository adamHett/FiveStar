USE [master]
GO
/****** Object:  Database [FiveStarDB]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE DATABASE [FiveStarDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FiveStarDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\FiveStarDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FiveStarDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\FiveStarDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FiveStarDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FiveStarDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FiveStarDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FiveStarDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FiveStarDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FiveStarDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FiveStarDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FiveStarDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FiveStarDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FiveStarDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FiveStarDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FiveStarDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FiveStarDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FiveStarDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FiveStarDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FiveStarDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FiveStarDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FiveStarDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FiveStarDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FiveStarDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FiveStarDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FiveStarDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FiveStarDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FiveStarDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FiveStarDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FiveStarDB] SET  MULTI_USER 
GO
ALTER DATABASE [FiveStarDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FiveStarDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FiveStarDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FiveStarDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FiveStarDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FiveStarDB] SET QUERY_STORE = OFF
GO
USE [FiveStarDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [FiveStarDB]
GO
/****** Object:  User [unf2016]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE USER [unf2016] FOR LOGIN [unf2016] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] NOT NULL,
	[UserName] [nchar](10) NOT NULL,
	[Password] [nchar](10) NOT NULL,
	[Role] [nchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FiveStarApplicantInformation]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiveStarApplicantInformation](
	[AppInfo_ID] [int] IDENTITY(21,1) NOT NULL,
	[AppInfo_Status] [nvarchar](max) NOT NULL,
	[AppInfo_Date] [date] NOT NULL,
	[AppInfo_Type] [nvarchar](max) NULL,
	[AppInfo_SSN] [nvarchar](max) NULL,
	[AppInfo_Fname] [nvarchar](max) NULL,
	[AppInfo_Lname] [nvarchar](max) NULL,
	[AppInfo_DOB] [date] NULL,
	[AppInfo_Age] [int] NULL,
	[AppInfo_Email] [nvarchar](max) NULL,
	[AppInfo_Phone] [nvarchar](max) NULL,
	[AppInfo_Gender] [nvarchar](max) NULL,
	[AppInfo_Need1] [nvarchar](max) NULL,
	[AppInfo_Need2] [nvarchar](max) NULL,
	[AppInfo_LTGoal] [nvarchar](max) NULL,
	[AppInfo_ReferAgency] [nvarchar](max) NULL,
	[AppInfo_ReferAgent] [nvarchar](max) NULL,
	[AppInfo_NonRefer] [nvarchar](max) NULL,
	[AppInfo_HousingStatus] [nvarchar](max) NULL,
	[AppInfo_Facility] [nvarchar](max) NULL,
	[AppInfo_StayLength] [nvarchar](max) NULL,
	[AppInfo_EContFname] [nvarchar](max) NULL,
	[AppInfo_EContLname] [nvarchar](max) NULL,
	[AppInfo_EContRelation] [nvarchar](max) NULL,
	[AppInfo_EContStAddress] [nvarchar](max) NULL,
	[AppInfo_EContCity] [nvarchar](max) NULL,
	[AppInfo_EContState] [nvarchar](max) NULL,
	[AppInfo_EContZip] [nvarchar](max) NULL,
	[AppInfo_EContPH] [nvarchar](max) NULL,
	[AppInfo_MaritalStatus] [nvarchar](max) NULL,
	[AppInfo_Children] [int] NULL,
	[AppInfo_ChildrenMale] [int] NULL,
	[AppInfo_ChildrenFem] [int] NULL,
	[AppInfo_TrainLevel] [nvarchar](max) NULL,
	[AppInfo_TrainSkills] [nvarchar](max) NULL,
	[AppInfo_MilServDD214] [bit] NULL,
	[AppInfo_MilServBranch] [nvarchar](max) NULL,
	[AppInfo_MilServBeginDate] [date] NULL,
	[AppInfo_MilServEndDate] [date] NULL,
	[AppInfo_MilServDischargeRnk] [nvarchar](max) NULL,
	[AppInfo_MilServMOS] [nvarchar](max) NULL,
	[AppInfo_MilServCombat] [nvarchar](max) NULL,
	[AppInfo_MilServSepCode] [nvarchar](max) NULL,
	[AppInfo_MilServPurpHrt] [nvarchar](max) NULL,
	[AppInfo_MilServTOD] [nvarchar](max) NULL,
	[AppInfo_MilServDischargeStatus] [nvarchar](max) NULL,
	[AppInfo_MoIncServiceDisability] [nvarchar](max) NULL,
	[AppInfo_MoIncSDPercent] [nvarchar](max) NULL,
	[AppInfo_MoIncSDMoAmt] [nvarchar](max) NULL,
	[AppInfo_MoIncCurrEmployed] [nvarchar](max) NULL,
	[AppInfo_MoIncCEMoAmt] [nvarchar](max) NULL,
	[AppInfo_MoIncSSI] [nvarchar](max) NULL,
	[AppInfo_MoIncSSDI] [nvarchar](max) NULL,
	[AppInfo_MoIncFoodStamps] [nvarchar](max) NULL,
	[AppInfo_MoIncOther] [nvarchar](max) NULL,
	[AppInfo_MoIncMoChildSupport] [nvarchar](max) NULL,
	[AppInfo_MoIncTotalMoAmt] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobTitle] [nvarchar](max) NULL,
	[AppInfo_EmpHistEmployer] [nvarchar](max) NULL,
	[AppInfo_EmpHistCity] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobStart] [date] NULL,
	[AppInfo_EmpHistJobEnd] [date] NULL,
	[AppInfo_EmpHistMoWage] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobTitle2] [nvarchar](max) NULL,
	[AppInfo_EmpHistEmployer2] [nvarchar](max) NULL,
	[AppInfo_EmpHistCity2] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobStart2] [date] NULL,
	[AppInfo_EmpHistJobEnd2] [date] NULL,
	[AppInfo_EmpHistMoWage2] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobTitle3] [nvarchar](max) NULL,
	[AppInfo_EmpHistEmployer3] [nvarchar](max) NULL,
	[AppInfo_EmpHistCity3] [nvarchar](max) NULL,
	[AppInfo_EmpHistJobStart3] [date] NULL,
	[AppInfo_EmpHistJobEnd3] [date] NULL,
	[AppInfo_EmpHistMoWage3] [nvarchar](max) NULL,
	[AppInfo_LegInfoMisdemeanor] [nvarchar](max) NULL,
	[AppInfo_LegInfoFelony] [nvarchar](max) NULL,
	[AppInfo_LegInfoState] [nvarchar](max) NULL,
	[AppInfo_LegInfoCounty] [nvarchar](max) NULL,
	[AppInfo_LegInfoCharge_Details] [nvarchar](max) NULL,
	[AppInfo_LegInfoCharge_Timeframe] [nvarchar](max) NULL,
	[AppInfo_LegInfoProbation_Status] [nvarchar](max) NULL,
	[AppInfo_LegInfoProbation_Officer] [nvarchar](max) NULL,
	[AppInfo_LegInfoVetTreatCourt_Status] [nvarchar](max) NULL,
	[AppInfo_LegInfoVetTreatCourt_Phase] [nvarchar](max) NULL,
	[AppInfo_LegInfoVetTreatCourt_GradStauts] [nvarchar](max) NULL,
	[AppInfo_LegInfoLegal_List] [nvarchar](max) NULL,
	[AppInfo_AppAgree_SignDate1] [nvarchar](max) NULL,
	[AppInfo_AppAgree_SignDate2] [nvarchar](max) NULL,
 CONSTRAINT [PK_FiveStarApplicantInformation] PRIMARY KEY CLUSTERED 
(
	[AppInfo_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FiveStarApplicantInterviewInformation]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiveStarApplicantInterviewInformation](
	[AppIntInfo_ID] [int] IDENTITY(1,1) NOT NULL,
	[AppIntInfo_Date] [date] NOT NULL,
	[AppIntInfo_Status] [nvarchar](max) NOT NULL,
	[AppIntInfo_SSN] [nvarchar](max) NOT NULL,
	[AppIntInfo_Fname] [nvarchar](max) NOT NULL,
	[AppIntInfo_Lname] [nvarchar](max) NOT NULL,
	[AppIntInfo_DOB] [date] NOT NULL,
	[AppIntInfo_Age] [int] NULL,
	[AppIntInfo_MHCurrentHarm] [bit] NULL,
	[AppIntInfo_MHPastHarm] [bit] NULL,
	[AppIntInfo_MHPastHarm_Details] [nvarchar](max) NULL,
	[AppIntInfo_MHTBI] [bit] NULL,
	[AppIntInfo_MHPTS] [bit] NULL,
	[AppIntInfo_MHSubAbuse] [bit] NULL,
	[AppIntInfo_MHBipoDisorder] [bit] NULL,
	[AppIntInfo_MHSchizo] [bit] NULL,
	[AppIntInfo_MHDepression] [bit] NULL,
	[AppIntInfo_MHAnxiety] [bit] NULL,
	[AppIntInfo_MHPersDisorder] [bit] NULL,
	[AppIntInfo_MHOther] [nvarchar](max) NULL,
	[AppIntInfo_ASAAlcohol] [bit] NULL,
	[AppIntInfo_ASAGambling] [bit] NULL,
	[AppIntInfo_ASAPrescriptDrug] [bit] NULL,
	[AppIntInfo_ASAStreetDrug] [bit] NULL,
	[AppIntInfo_ASATobacco] [bit] NULL,
	[AppIntInfo_ASADrugChoice1] [nvarchar](max) NULL,
	[AppIntInfo_ASADrugChoice2] [nvarchar](max) NULL,
	[AppIntInfo_ASADrugChoice3] [nvarchar](max) NULL,
	[AppIntInfo_ASACurrent_TimeSober] [nvarchar](max) NULL,
	[AppIntInfo_ASAPast_TimeSober] [nvarchar](max) NULL,
	[AppIntInfo_ASADUI_DWI] [bit] NULL,
	[AppIntInfo_ASAPossess_Conviction] [bit] NULL,
	[AppIntInfo_ASASell_Conviction] [bit] NULL,
	[AppIntInfo_PHPCP_Fname] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_Lname] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_Phone] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_StAddress] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_City] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_State] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_Zip] [nvarchar](max) NULL,
	[AppIntInfo_PHPCP_ContactOK] [bit] NULL,
	[AppIntInfo_PHSeizures] [bit] NULL,
	[AppIntInfo_PHAllergies] [nvarchar](max) NULL,
	[AppIntInfo_PHOther] [nvarchar](max) NULL,
	[AppIntInfo_MedsCondition1] [nvarchar](max) NULL,
	[AppIntInfo_MedsMedication1] [nvarchar](max) NULL,
	[AppIntInfo_MedsDosage1] [nvarchar](max) NULL,
	[AppIntInfo_MedsFrequency1] [nvarchar](max) NULL,
	[AppIntInfo_MedsCondition2] [nvarchar](max) NULL,
	[AppIntInfo_MedsMedication2] [nvarchar](max) NULL,
	[AppIntInfo_MedsDosage2] [nvarchar](max) NULL,
	[AppIntInfo_MedsFrequency2] [nvarchar](max) NULL,
	[AppIntInfo_MedsCondition3] [nvarchar](max) NULL,
	[AppIntInfo_MedsMedication3] [nvarchar](max) NULL,
	[AppIntInfo_MedsDosage3] [nvarchar](max) NULL,
	[AppIntInfo_MedsFrequency3] [nvarchar](max) NULL,
	[AppIntInfo_MedsCondition4] [nvarchar](max) NULL,
	[AppIntInfo_MedsMedication4] [nvarchar](max) NULL,
	[AppIntInfo_MedsDosage4] [nvarchar](max) NULL,
	[AppIntInfo_MedsFrequency4] [nvarchar](max) NULL,
	[AppIntInfo_MedsCondition5] [nvarchar](max) NULL,
	[AppIntInfo_MedsMedication5] [nvarchar](max) NULL,
	[AppIntInfo_MedsDosage5] [nvarchar](max) NULL,
	[AppIntInfo_MedsFrequency5] [nvarchar](max) NULL,
	[AppIntInfo_MedsOther] [nvarchar](max) NULL,
	[AppIntInfo_AgreeSignDate] [date] NULL,
	[AppInfo_ID] [int] NULL,
 CONSTRAINT [PK_FiveStarApplicantInterviewInformation] PRIMARY KEY CLUSTERED 
(
	[AppIntInfo_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FiveStarResidentInformation]    Script Date: 4/18/2017 8:49:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FiveStarResidentInformation](
	[ResInfo_ID] [int] IDENTITY(30,1) NOT NULL,
	[ResInfo_Status] [nvarchar](max) NOT NULL,
	[ResInfo_AcceptDate] [date] NOT NULL,
	[ResInfo_DeclineDate] [date] NULL,
	[ResInfo_RoomID] [int] NOT NULL,
	[ResInfo_DD214] [bit] NOT NULL,
	[ResInfo_TBTest] [bit] NOT NULL,
	[ResInfo_PhotoID] [bit] NOT NULL,
	[ResInfo_HUDVASH_Vouch] [bit] NOT NULL,
	[ResInfo_BackCheck] [bit] NOT NULL,
	[ResInfo_CommRecomendation] [nvarchar](max) NULL,
	[ResInfo_SSN] [nvarchar](max) NULL,
	[ResInfo_Fname] [nvarchar](max) NULL,
	[ResInfo_Lname] [nvarchar](max) NULL,
	[ResInfo_DOB] [date] NULL,
	[ResInfo_Age] [int] NULL,
	[ResInfo_Phone] [nvarchar](max) NULL,
	[ResInfo_Gender] [nvarchar](max) NULL,
	[AppInfo_ID] [int] NOT NULL,
	[ResInfo_ExitDate] [date] NULL,
	[ResInfo_ExitSummary] [nvarchar](max) NULL,
	[AppIntInfo_ID] [int] NULL,
 CONSTRAINT [PK_FiveStarResidentInformation] PRIMARY KEY CLUSTERED 
(
	[ResInfo_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/18/2017 8:49:37 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[FiveStarApplicantInterviewInformation]  WITH CHECK ADD  CONSTRAINT [FK_FiveStarApplicantInterviewInformation_ToTable] FOREIGN KEY([AppInfo_ID])
REFERENCES [dbo].[FiveStarApplicantInformation] ([AppInfo_ID])
GO
ALTER TABLE [dbo].[FiveStarApplicantInterviewInformation] CHECK CONSTRAINT [FK_FiveStarApplicantInterviewInformation_ToTable]
GO
ALTER TABLE [dbo].[FiveStarResidentInformation]  WITH CHECK ADD  CONSTRAINT [FK_FiveStarResidentInformation_ToTable] FOREIGN KEY([AppInfo_ID])
REFERENCES [dbo].[FiveStarApplicantInformation] ([AppInfo_ID])
GO
ALTER TABLE [dbo].[FiveStarResidentInformation] CHECK CONSTRAINT [FK_FiveStarResidentInformation_ToTable]
GO
ALTER TABLE [dbo].[FiveStarResidentInformation]  WITH CHECK ADD  CONSTRAINT [FK_FiveStarResidentInformation_ToTable1] FOREIGN KEY([AppIntInfo_ID])
REFERENCES [dbo].[FiveStarApplicantInterviewInformation] ([AppIntInfo_ID])
GO
ALTER TABLE [dbo].[FiveStarResidentInformation] CHECK CONSTRAINT [FK_FiveStarResidentInformation_ToTable1]
GO
USE [master]
GO
ALTER DATABASE [FiveStarDB] SET  READ_WRITE 
GO
