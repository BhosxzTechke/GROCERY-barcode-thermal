USE [master]
GO
/****** Object:  Database [JimbospharmaDB]    Script Date: 19/05/2025 8:38:50 pm ******/
CREATE DATABASE [JimbospharmaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JimbospharmaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\JimbospharmaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JimbospharmaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\JimbospharmaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [JimbospharmaDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JimbospharmaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JimbospharmaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [JimbospharmaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JimbospharmaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JimbospharmaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JimbospharmaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JimbospharmaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [JimbospharmaDB] SET  MULTI_USER 
GO
ALTER DATABASE [JimbospharmaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JimbospharmaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JimbospharmaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JimbospharmaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JimbospharmaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JimbospharmaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JimbospharmaDB', N'ON'
GO
ALTER DATABASE [JimbospharmaDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [JimbospharmaDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [JimbospharmaDB]
GO
/****** Object:  User [tique]    Script Date: 19/05/2025 8:38:50 pm ******/
CREATE USER [tique] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [admins]    Script Date: 19/05/2025 8:38:50 pm ******/
CREATE USER [admins] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [admin]    Script Date: 19/05/2025 8:38:50 pm ******/
CREATE USER [admin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [tique]
GO
ALTER ROLE [db_owner] ADD MEMBER [admin]
GO
/****** Object:  Table [dbo].[auditlog]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[auditlog](
	[AuditID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Action] [nvarchar](100) NOT NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[RecordID] [int] NOT NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[ActionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AuditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbladjustment]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbladjustment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[InvID] [int] NULL,
	[pid] [int] NULL,
	[qty] [int] NULL,
	[stype] [varchar](50) NULL,
	[reason] [text] NULL,
	[adjusted_by] [varchar](50) NULL,
	[sdate] [date] NULL,
 CONSTRAINT [PK__tbladjus__3213E83F56E8E7AB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAuditLogin]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuditLogin](
	[AuditLoginID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Action] [nvarchar](10) NULL,
	[OldValue] [nvarchar](255) NULL,
	[NewValue] [nvarchar](255) NULL,
	[ActionDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AuditLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblbrand]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblbrand](
	[brandID] [int] IDENTITY(1,1) NOT NULL,
	[brand] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[brandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcart]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoice] [varchar](255) NULL,
	[pid] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[qty] [int] NULL,
	[total] [decimal](10, 2) NULL,
	[sdate] [date] NULL,
	[users] [varchar](50) NULL,
	[status] [varchar](50) NULL,
	[wholeprice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcategory]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcategory](
	[catID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](45) NULL,
PRIMARY KEY CLUSTERED 
(
	[catID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcontactperson]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcontactperson](
	[ContactPersonID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ContactName] [varchar](100) NOT NULL,
	[Position] [varchar](50) NULL,
	[Phone] [varchar](15) NULL,
	[Email] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDelivery]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDelivery](
	[DeliveryID] [int] IDENTITY(1,1) NOT NULL,
	[BatchNumber] [varchar](50) NULL,
	[Supplier] [varchar](100) NULL,
	[DeliveryDate] [date] NULL,
	[CostPrice] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[DeliveryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDeliveryLineItem]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDeliveryLineItem](
	[LineItemID] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryID] [int] NULL,
	[id] [int] NULL,
	[ProductName] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[CostPrice] [decimal](18, 2) NULL,
	[SupplierID] [int] NULL,
	[ExpirationDate] [date] NULL,
 CONSTRAINT [PK__tblDeliv__8A871BEE01D345B0] PRIMARY KEY CLUSTERED 
(
	[LineItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbldiscount]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbldiscount](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description_disc] [varchar](50) NULL,
	[Discount] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbldosage]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbldosage](
	[dosageID] [int] IDENTITY(1,1) NOT NULL,
	[Dosage] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblformulations]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblformulations](
	[formID] [int] IDENTITY(1,1) NOT NULL,
	[Formulations] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[formID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblgeneric]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblgeneric](
	[genericID] [int] IDENTITY(1,1) NOT NULL,
	[generic] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[genericID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblInventory]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblInventory](
	[InventoryID] [int] IDENTITY(1,1) NOT NULL,
	[id] [int] NULL,
	[Quantity] [int] NULL,
	[ExpirationDate] [date] NULL,
	[DeliveryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[InventoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblpayment]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblpayment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoice] [varchar](50) NULL,
	[subtotal] [decimal](10, 2) NOT NULL,
	[vat] [decimal](10, 2) NOT NULL,
	[discount] [decimal](10, 2) NOT NULL,
	[amountdue] [decimal](10, 2) NULL,
	[sdate] [date] NULL,
	[users] [varchar](50) NULL,
 CONSTRAINT [PK__tblpayme__3213E83F3F115E1A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblproduct]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblproduct](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[barcode] [varchar](50) NULL,
	[item_des] [varchar](50) NULL,
	[cid] [int] NULL,
	[price] [decimal](10, 2) NULL,
	[imagepath] [varbinary](max) NULL,
	[uid] [int] NULL,
	[barcode_image] [varbinary](max) NULL,
	[costprice] [decimal](10, 2) NULL,
	[WholePrice] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK__tblprodu__3213E83F25869641] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblstockin]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblstockin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[refno] [varchar](50) NOT NULL,
	[receivedby] [varchar](100) NOT NULL,
	[pid] [varchar](50) NOT NULL,
	[sid] [int] NULL,
	[qty] [int] NOT NULL,
	[sdate] [date] NOT NULL,
 CONSTRAINT [PK__tblstock__3213E83F5070F446] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStockSettings]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStockSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StockLevel] [int] NOT NULL,
	[CriticalLevel] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupplier]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplier](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[ImageSpl] [varbinary](max) NULL,
	[Contact_Person] [varchar](255) NULL,
	[Mobile_no] [varchar](15) NULL,
	[Tel_no] [varchar](15) NULL,
 CONSTRAINT [PK__tblSuppl__4BE666946754599E] PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblType]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblType](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblunit]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblunit](
	[unitID] [int] IDENTITY(1,1) NOT NULL,
	[unit] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[unitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbluser]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbluser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Contact] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Confirm_Password] [nvarchar](50) NULL,
	[User_Type] [nvarchar](50) NULL,
	[Status] [varchar](50) NULL,
	[imagepath] [varbinary](max) NULL,
 CONSTRAINT [PK__tbluser__3214EC270425A276] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVat]    Script Date: 19/05/2025 8:38:50 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVat](
	[vat] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[vat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[auditlog] ADD  DEFAULT (getdate()) FOR [ActionDate]
GO
ALTER TABLE [dbo].[tblAuditLogin] ADD  DEFAULT (getdate()) FOR [ActionDate]
GO
ALTER TABLE [dbo].[tblcart] ADD  DEFAULT ('pending') FOR [status]
GO
ALTER TABLE [dbo].[tblproduct] ADD  DEFAULT ((0)) FOR [WholePrice]
GO
ALTER TABLE [dbo].[tbluser] ADD  CONSTRAINT [DF__tbluser__Status__07020F21]  DEFAULT ('active') FOR [Status]
GO
ALTER TABLE [dbo].[tblcart]  WITH CHECK ADD  CONSTRAINT [FK_tblcart_tblcart] FOREIGN KEY([id])
REFERENCES [dbo].[tblcart] ([id])
GO
ALTER TABLE [dbo].[tblcart] CHECK CONSTRAINT [FK_tblcart_tblcart]
GO
ALTER TABLE [dbo].[tblcart]  WITH CHECK ADD  CONSTRAINT [FK_tblcart_tblcart1] FOREIGN KEY([id])
REFERENCES [dbo].[tblcart] ([id])
GO
ALTER TABLE [dbo].[tblcart] CHECK CONSTRAINT [FK_tblcart_tblcart1]
GO
ALTER TABLE [dbo].[tblcart]  WITH CHECK ADD  CONSTRAINT [FK_tblcart_tblcart2] FOREIGN KEY([id])
REFERENCES [dbo].[tblcart] ([id])
GO
ALTER TABLE [dbo].[tblcart] CHECK CONSTRAINT [FK_tblcart_tblcart2]
GO
ALTER TABLE [dbo].[tblcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblcategory_tblcategory] FOREIGN KEY([catID])
REFERENCES [dbo].[tblcategory] ([catID])
GO
ALTER TABLE [dbo].[tblcategory] CHECK CONSTRAINT [FK_tblcategory_tblcategory]
GO
ALTER TABLE [dbo].[tblcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblcategory_tblcategory1] FOREIGN KEY([catID])
REFERENCES [dbo].[tblcategory] ([catID])
GO
ALTER TABLE [dbo].[tblcategory] CHECK CONSTRAINT [FK_tblcategory_tblcategory1]
GO
ALTER TABLE [dbo].[tblcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblcategory_tblcategory2] FOREIGN KEY([catID])
REFERENCES [dbo].[tblcategory] ([catID])
GO
ALTER TABLE [dbo].[tblcategory] CHECK CONSTRAINT [FK_tblcategory_tblcategory2]
GO
ALTER TABLE [dbo].[tblcontactperson]  WITH CHECK ADD  CONSTRAINT [FK__tblcontac__Suppl__6166761E] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[tblSupplier] ([SupplierID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblcontactperson] CHECK CONSTRAINT [FK__tblcontac__Suppl__6166761E]
GO
ALTER TABLE [dbo].[tblDeliveryLineItem]  WITH CHECK ADD  CONSTRAINT [FK_LineItem_Delivery] FOREIGN KEY([DeliveryID])
REFERENCES [dbo].[tblDelivery] ([DeliveryID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblDeliveryLineItem] CHECK CONSTRAINT [FK_LineItem_Delivery]
GO
ALTER TABLE [dbo].[tblDeliveryLineItem]  WITH CHECK ADD  CONSTRAINT [FK_LineItem_Product] FOREIGN KEY([id])
REFERENCES [dbo].[tblproduct] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblDeliveryLineItem] CHECK CONSTRAINT [FK_LineItem_Product]
GO
ALTER TABLE [dbo].[tblInventory]  WITH CHECK ADD FOREIGN KEY([id])
REFERENCES [dbo].[tblproduct] ([id])
GO
ALTER TABLE [dbo].[tblInventory]  WITH CHECK ADD  CONSTRAINT [FK_tblInventory_id] FOREIGN KEY([id])
REFERENCES [dbo].[tblproduct] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblInventory] CHECK CONSTRAINT [FK_tblInventory_id]
GO
USE [master]
GO
ALTER DATABASE [JimbospharmaDB] SET  READ_WRITE 
GO
