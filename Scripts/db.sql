USE [master]
GO
/****** Object:  Database [ShoppingDatabase]    Script Date: 21/02/2022 8:54:13 PM ******/
CREATE DATABASE [ShoppingDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoppingDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ShoppingDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShoppingDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ShoppingDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ShoppingDatabase] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoppingDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoppingDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoppingDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoppingDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShoppingDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoppingDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShoppingDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [ShoppingDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoppingDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoppingDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoppingDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShoppingDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShoppingDatabase] SET QUERY_STORE = OFF
GO
USE [ShoppingDatabase]
GO
/****** Object:  Table [dbo].[Cart_Products]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart_Products](
	[FK_IdCart] [bigint] NOT NULL,
	[FK_IdProduct] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Discount] [decimal](10, 2) NOT NULL,
	[Subtotal] [decimal](10, 2) NOT NULL,
	[IdCart_Product] [bigint] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[IdCart] [bigint] IDENTITY(1,1) NOT NULL,
	[Discount] [decimal](10, 2) NOT NULL,
	[Subtotal] [decimal](10, 2) NOT NULL,
	[Taxes] [decimal](10, 2) NOT NULL,
	[Total] [decimal](10, 2) NOT NULL,
	[Created] [datetime2](3) NOT NULL,
	[Updated] [datetime] NOT NULL,
	[FK_IdUser] [bigint] NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[IdCart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[IdCategory] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[IdProduct] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[UnitSold] [int] NOT NULL,
	[Discount] [decimal](10, 2) NULL,
	[FK_IdCategory] [bigint] NOT NULL,
	[Created] [datetime2](3) NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[IdRole] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/02/2022 8:54:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[FK_IdRole] [bigint] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cart_Products] ON 

INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (16, 5, 3, CAST(2499.99 AS Decimal(10, 2)), CAST(750.00 AS Decimal(10, 2)), CAST(6749.97 AS Decimal(10, 2)), 18)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (16, 7, 30, CAST(1197.00 AS Decimal(10, 2)), CAST(3591.00 AS Decimal(10, 2)), CAST(32319.00 AS Decimal(10, 2)), 19)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (16, 2, 2, CAST(27.99 AS Decimal(10, 2)), CAST(8.40 AS Decimal(10, 2)), CAST(47.58 AS Decimal(10, 2)), 20)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (17, 2, 3, CAST(27.99 AS Decimal(10, 2)), CAST(12.60 AS Decimal(10, 2)), CAST(71.37 AS Decimal(10, 2)), 21)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (17, 6, 1, CAST(7837.84 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(7837.84 AS Decimal(10, 2)), 22)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (17, 7, 45, CAST(1197.00 AS Decimal(10, 2)), CAST(5386.50 AS Decimal(10, 2)), CAST(48478.50 AS Decimal(10, 2)), 23)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (17, 22, 2, CAST(1050.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(2100.00 AS Decimal(10, 2)), 24)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (18, 2, 34, CAST(27.99 AS Decimal(10, 2)), CAST(142.75 AS Decimal(10, 2)), CAST(808.91 AS Decimal(10, 2)), 25)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (18, 22, 5, CAST(1050.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(5250.00 AS Decimal(10, 2)), 26)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (19, 15, 5, CAST(110.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(550.00 AS Decimal(10, 2)), 27)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (20, 2, 4, CAST(27.99 AS Decimal(10, 2)), CAST(16.79 AS Decimal(10, 2)), CAST(95.17 AS Decimal(10, 2)), 28)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (21, 2, 1, CAST(27.99 AS Decimal(10, 2)), CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), 29)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (22, 1, 1, CAST(239.99 AS Decimal(10, 2)), CAST(60.00 AS Decimal(10, 2)), CAST(179.99 AS Decimal(10, 2)), 30)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (23, 2, 1, CAST(27.99 AS Decimal(10, 2)), CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), 31)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (24, 2, 1, CAST(27.99 AS Decimal(10, 2)), CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), 32)
SET IDENTITY_INSERT [dbo].[Cart_Products] OFF
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (16, CAST(4349.39 AS Decimal(10, 2)), CAST(39116.56 AS Decimal(10, 2)), CAST(5857.70 AS Decimal(10, 2)), CAST(44974.26 AS Decimal(10, 2)), CAST(N'2022-02-22T00:11:25.8150000' AS DateTime2), CAST(N'2022-02-22T00:11:25.817' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (17, CAST(5399.10 AS Decimal(10, 2)), CAST(58487.71 AS Decimal(10, 2)), CAST(8758.54 AS Decimal(10, 2)), CAST(67246.25 AS Decimal(10, 2)), CAST(N'2022-02-22T00:14:57.3810000' AS DateTime2), CAST(N'2022-02-22T00:14:57.380' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (18, CAST(142.75 AS Decimal(10, 2)), CAST(6058.91 AS Decimal(10, 2)), CAST(907.32 AS Decimal(10, 2)), CAST(6966.23 AS Decimal(10, 2)), CAST(N'2022-02-22T00:29:23.7140000' AS DateTime2), CAST(N'2022-02-22T00:29:23.713' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (19, CAST(0.00 AS Decimal(10, 2)), CAST(550.00 AS Decimal(10, 2)), CAST(82.36 AS Decimal(10, 2)), CAST(632.36 AS Decimal(10, 2)), CAST(N'2022-02-22T00:29:57.8620000' AS DateTime2), CAST(N'2022-02-22T00:29:57.863' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (20, CAST(16.79 AS Decimal(10, 2)), CAST(95.17 AS Decimal(10, 2)), CAST(14.25 AS Decimal(10, 2)), CAST(109.42 AS Decimal(10, 2)), CAST(N'2022-02-22T00:32:37.5350000' AS DateTime2), CAST(N'2022-02-22T00:32:37.537' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (21, CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), CAST(3.56 AS Decimal(10, 2)), CAST(27.35 AS Decimal(10, 2)), CAST(N'2022-02-22T00:32:49.1540000' AS DateTime2), CAST(N'2022-02-22T00:32:49.153' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (22, CAST(60.00 AS Decimal(10, 2)), CAST(179.99 AS Decimal(10, 2)), CAST(26.95 AS Decimal(10, 2)), CAST(206.95 AS Decimal(10, 2)), CAST(N'2022-02-22T00:34:43.6000000' AS DateTime2), CAST(N'2022-02-22T00:34:43.600' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (23, CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), CAST(3.56 AS Decimal(10, 2)), CAST(27.35 AS Decimal(10, 2)), CAST(N'2022-02-22T00:34:56.1220000' AS DateTime2), CAST(N'2022-02-22T00:34:56.123' AS DateTime), 4)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (24, CAST(4.20 AS Decimal(10, 2)), CAST(23.79 AS Decimal(10, 2)), CAST(3.56 AS Decimal(10, 2)), CAST(27.35 AS Decimal(10, 2)), CAST(N'2022-02-22T00:40:37.4370000' AS DateTime2), CAST(N'2022-02-22T00:40:37.437' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[Carts] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (1, N'Home')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (2, N'Kids')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (3, N'Tech')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (4, N'Health')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (5, N'Personal Care')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (6, N'VideoGames')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (7, N'Clothing')
INSERT [dbo].[Categories] ([IdCategory], [Name]) VALUES (8, N'Sports')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (1, N'Robot Vacuum', CAST(239.99 AS Decimal(10, 2)), 22, 1, CAST(25.00 AS Decimal(10, 2)), 1, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:34:43.617' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (2, N'Carpet Shampoo', CAST(27.99 AS Decimal(10, 2)), 25, 51, CAST(15.00 AS Decimal(10, 2)), 1, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:40:37.453' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (3, N'Talks Toy', CAST(36.99 AS Decimal(10, 2)), 0, 54, CAST(5.00 AS Decimal(10, 2)), 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:29:36.810' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (4, N'Lego City Street', CAST(32.00 AS Decimal(10, 2)), 6, 15, NULL, 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T20:16:21.900' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (5, N'Macbook Pro 14"', CAST(2499.99 AS Decimal(10, 2)), 0, 6, CAST(10.00 AS Decimal(10, 2)), 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:11:25.847' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (6, N'Lenovo Thinkpad P73', CAST(7837.84 AS Decimal(10, 2)), 1, 2, NULL, 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:14:57.427' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (7, N'Turmeric 3050mg 120', CAST(1197.00 AS Decimal(10, 2)), 0, 186, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:14:57.427' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (8, N'Chlorophyll Drops 100ml', CAST(26.99 AS Decimal(10, 2)), 0, 101, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:31:26.767' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (10, N'Sunscreen Lotion', CAST(11.97 AS Decimal(10, 2)), 0, 108, NULL, 5, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:35:14.110' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (13, N'Gaming Chair', CAST(319.99 AS Decimal(10, 2)), 43, 32, CAST(15.00 AS Decimal(10, 2)), 6, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (14, N'Winter Jacket Woman', CAST(110.00 AS Decimal(10, 2)), 34, 23, CAST(0.00 AS Decimal(10, 2)), 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (15, N'Winter Jacket Man', CAST(110.00 AS Decimal(10, 2)), 27, 17, NULL, 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-22T00:29:57.867' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (16, N'Bike Treck', CAST(2699.00 AS Decimal(10, 2)), 2, 1, CAST(10.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (18, N'Bike Specialized', CAST(4000.00 AS Decimal(10, 2)), 1, 1, CAST(0.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (20, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 15, 0, CAST(15.00 AS Decimal(10, 2)), 3, CAST(N'2022-02-20T19:15:30.3400000' AS DateTime2), CAST(N'2022-02-20T19:15:30.340' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (21, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 15, 0, NULL, 3, CAST(N'2022-02-21T00:25:45.8290000' AS DateTime2), CAST(N'2022-02-21T00:25:45.830' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (22, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 8, 7, NULL, 3, CAST(N'2022-02-21T00:26:26.7520000' AS DateTime2), CAST(N'2022-02-22T00:29:23.740' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (23, N'Ipad', CAST(1050.00 AS Decimal(10, 2)), 15, 0, NULL, 3, CAST(N'2022-02-21T00:27:42.4250000' AS DateTime2), CAST(N'2022-02-21T00:39:06.980' AS DateTime))
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRole], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([IdRole], [Name]) VALUES (2, N'Client')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([IdUser], [Username], [Name], [PasswordHash], [PasswordSalt], [FK_IdRole], [Created], [Updated]) VALUES (1, N'Admin', N'Admin', 0x4CE231B31DC4ED0017A6E0F89A627F6EADA99E29F5153742584173DB6076A9E3, 0x2916B278F64A5A56306DAD4C56BF024AD3B848E8B7EDB4DAE5E1DE8501886A0B, 1, CAST(N'2022-02-21T22:43:42.567' AS DateTime), CAST(N'2022-02-21T22:43:42.567' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [PasswordHash], [PasswordSalt], [FK_IdRole], [Created], [Updated]) VALUES (2, N'ximenac', N'Ximena', 0xBAAD545A3976DD95AC912C5F9F1D6763B20587B7CE65D427A65E2B9003D5D18D, 0x6459AE1C65F6FC9C138784BF91444EEBA549CABCF4AF51910C8971813B074B34, 2, CAST(N'2022-02-21T22:54:12.783' AS DateTime), CAST(N'2022-02-21T22:54:12.783' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [PasswordHash], [PasswordSalt], [FK_IdRole], [Created], [Updated]) VALUES (3, N'JairF', N'Jair', 0x5B1E455E7C06D0219B19CEC2ACF3858EE1C6E9483B5F70A4ACA49EBFFC076487, 0xFF8F0BD9AECD220497DDB63668E26A5C51F93352C5E9544A40B756E8EA09FB1E, 1, CAST(N'2022-02-21T22:56:50.643' AS DateTime), CAST(N'2022-02-21T22:56:50.643' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [PasswordHash], [PasswordSalt], [FK_IdRole], [Created], [Updated]) VALUES (4, N'JaredC', N'Jared', 0xB06B00790CBEBA7A792B4A7D8029CA5A87F446ECE7726CA37455AB6CFEE35D79, 0xE9214F2DA76F3CDC58248B363772F1C67F40DB4128F892AA9C8804E0397F8860, 2, CAST(N'2022-02-21T22:57:31.063' AS DateTime), CAST(N'2022-02-21T22:57:31.063' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Cart_Products]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Products_Carts] FOREIGN KEY([FK_IdCart])
REFERENCES [dbo].[Carts] ([IdCart])
GO
ALTER TABLE [dbo].[Cart_Products] CHECK CONSTRAINT [FK_Cart_Products_Carts]
GO
ALTER TABLE [dbo].[Cart_Products]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Products_Products] FOREIGN KEY([FK_IdProduct])
REFERENCES [dbo].[Products] ([IdProduct])
GO
ALTER TABLE [dbo].[Cart_Products] CHECK CONSTRAINT [FK_Cart_Products_Products]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users] FOREIGN KEY([FK_IdUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_Carts_Users]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Products] FOREIGN KEY([FK_IdCategory])
REFERENCES [dbo].[Categories] ([IdCategory])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Products]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Roles] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([FK_IdRole])
REFERENCES [dbo].[Roles] ([IdRole])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [ShoppingDatabase] SET  READ_WRITE 
GO
