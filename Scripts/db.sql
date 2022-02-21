USE [master]
GO
/****** Object:  Database [ShoppingDatabase]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Cart_Products]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Carts]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 2/21/2022 11:39:56 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2/21/2022 11:39:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Password] [varbinary](max) NULL,
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

INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (8, 7, 23, CAST(1197.00 AS Decimal(10, 2)), CAST(2753.10 AS Decimal(10, 2)), CAST(24777.90 AS Decimal(10, 2)), 1)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (8, 10, 23, CAST(11.97 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(275.31 AS Decimal(10, 2)), 2)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (8, 3, 23, CAST(36.99 AS Decimal(10, 2)), CAST(42.54 AS Decimal(10, 2)), CAST(808.23 AS Decimal(10, 2)), 3)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (9, 8, 23, CAST(26.99 AS Decimal(10, 2)), CAST(62.08 AS Decimal(10, 2)), CAST(558.69 AS Decimal(10, 2)), 4)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (9, 3, 54, CAST(36.99 AS Decimal(10, 2)), CAST(99.87 AS Decimal(10, 2)), CAST(1897.59 AS Decimal(10, 2)), 5)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (10, 7, 38, CAST(1197.00 AS Decimal(10, 2)), CAST(4548.60 AS Decimal(10, 2)), CAST(40937.40 AS Decimal(10, 2)), 6)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (10, 8, 5, CAST(26.99 AS Decimal(10, 2)), CAST(13.50 AS Decimal(10, 2)), CAST(121.46 AS Decimal(10, 2)), 7)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (10, 10, 6, CAST(11.97 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(71.82 AS Decimal(10, 2)), 8)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (11, 8, 70, CAST(26.99 AS Decimal(10, 2)), CAST(188.93 AS Decimal(10, 2)), CAST(1700.37 AS Decimal(10, 2)), 9)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (12, 7, 23, CAST(1197.00 AS Decimal(10, 2)), CAST(2753.10 AS Decimal(10, 2)), CAST(24777.90 AS Decimal(10, 2)), 10)
INSERT [dbo].[Cart_Products] ([FK_IdCart], [FK_IdProduct], [Quantity], [Price], [Discount], [Subtotal], [IdCart_Product]) VALUES (12, 10, 70, CAST(11.97 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)), CAST(837.90 AS Decimal(10, 2)), 11)
SET IDENTITY_INSERT [dbo].[Cart_Products] OFF
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (1, CAST(359.10 AS Decimal(10, 2)), CAST(19003.58 AS Decimal(10, 2)), CAST(2845.79 AS Decimal(10, 2)), CAST(21849.37 AS Decimal(10, 2)), CAST(N'2022-02-21T15:57:02.7830000' AS DateTime2), CAST(N'2022-02-21T15:57:02.783' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (2, CAST(258.10 AS Decimal(10, 2)), CAST(2598.17 AS Decimal(10, 2)), CAST(389.08 AS Decimal(10, 2)), CAST(2987.25 AS Decimal(10, 2)), CAST(N'2022-02-21T16:01:38.8900000' AS DateTime2), CAST(N'2022-02-21T16:01:38.890' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (4, CAST(62.08 AS Decimal(10, 2)), CAST(622.69 AS Decimal(10, 2)), CAST(93.25 AS Decimal(10, 2)), CAST(715.94 AS Decimal(10, 2)), CAST(N'2022-02-21T16:04:53.0860000' AS DateTime2), CAST(N'2022-02-21T16:04:53.087' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (5, CAST(16613.10 AS Decimal(10, 2)), CAST(158893.58 AS Decimal(10, 2)), CAST(23794.31 AS Decimal(10, 2)), CAST(182687.89 AS Decimal(10, 2)), CAST(N'2022-02-21T16:05:56.5610000' AS DateTime2), CAST(N'2022-02-21T16:05:56.560' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (6, CAST(816.85 AS Decimal(10, 2)), CAST(6320.12 AS Decimal(10, 2)), CAST(946.44 AS Decimal(10, 2)), CAST(7266.56 AS Decimal(10, 2)), CAST(N'2022-02-21T16:08:25.1310000' AS DateTime2), CAST(N'2022-02-21T16:08:25.130' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (7, CAST(4169.96 AS Decimal(10, 2)), CAST(37501.68 AS Decimal(10, 2)), CAST(5615.88 AS Decimal(10, 2)), CAST(43117.55 AS Decimal(10, 2)), CAST(N'2022-02-21T16:09:39.2360000' AS DateTime2), CAST(N'2022-02-21T16:09:39.237' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (8, CAST(2795.64 AS Decimal(10, 2)), CAST(25861.44 AS Decimal(10, 2)), CAST(3872.75 AS Decimal(10, 2)), CAST(29734.19 AS Decimal(10, 2)), CAST(N'2022-02-21T16:12:54.7620000' AS DateTime2), CAST(N'2022-02-21T16:12:54.763' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (9, CAST(161.95 AS Decimal(10, 2)), CAST(2456.28 AS Decimal(10, 2)), CAST(367.83 AS Decimal(10, 2)), CAST(2824.11 AS Decimal(10, 2)), CAST(N'2022-02-21T16:29:36.7750000' AS DateTime2), CAST(N'2022-02-21T16:29:36.773' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (10, CAST(4562.10 AS Decimal(10, 2)), CAST(41130.68 AS Decimal(10, 2)), CAST(6159.32 AS Decimal(10, 2)), CAST(47289.99 AS Decimal(10, 2)), CAST(N'2022-02-21T16:31:14.0570000' AS DateTime2), CAST(N'2022-02-21T16:31:14.057' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (11, CAST(188.93 AS Decimal(10, 2)), CAST(1700.37 AS Decimal(10, 2)), CAST(254.63 AS Decimal(10, 2)), CAST(1955.00 AS Decimal(10, 2)), CAST(N'2022-02-21T16:31:26.7600000' AS DateTime2), CAST(N'2022-02-21T16:31:26.760' AS DateTime), 2)
INSERT [dbo].[Carts] ([IdCart], [Discount], [Subtotal], [Taxes], [Total], [Created], [Updated], [FK_IdUser]) VALUES (12, CAST(2753.10 AS Decimal(10, 2)), CAST(25615.80 AS Decimal(10, 2)), CAST(3835.97 AS Decimal(10, 2)), CAST(29451.77 AS Decimal(10, 2)), CAST(N'2022-02-21T16:35:14.0730000' AS DateTime2), CAST(N'2022-02-21T16:35:14.073' AS DateTime), 2)
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

INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (1, N'Robot Vacuum', CAST(239.99 AS Decimal(10, 2)), 23, 0, CAST(25.00 AS Decimal(10, 2)), 1, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (2, N'Carpet Shampoo', CAST(27.99 AS Decimal(10, 2)), 76, 0, CAST(15.00 AS Decimal(10, 2)), 1, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (3, N'Talks Toy', CAST(36.99 AS Decimal(10, 2)), 0, 54, CAST(5.00 AS Decimal(10, 2)), 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:29:36.810' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (4, N'Lego City Street', CAST(32.00 AS Decimal(10, 2)), 21, 0, CAST(0.00 AS Decimal(10, 2)), 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (5, N'Macbook Pro 14"', CAST(2499.99 AS Decimal(10, 2)), 5, 1, CAST(10.00 AS Decimal(10, 2)), 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (6, N'Lenovo Thinkpad P73', CAST(7837.84 AS Decimal(10, 2)), 3, 0, CAST(0.00 AS Decimal(10, 2)), 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (7, N'Turmeric 3050mg 120', CAST(1197.00 AS Decimal(10, 2)), 77, 109, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:35:14.103' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (8, N'Chlorophyll Drops 100ml', CAST(26.99 AS Decimal(10, 2)), 0, 101, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:31:26.767' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (10, N'Sunscreen Lotion', CAST(11.97 AS Decimal(10, 2)), 0, 108, NULL, 5, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2022-02-21T16:35:14.110' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (13, N'Gaming Chair', CAST(319.99 AS Decimal(10, 2)), 43, 32, CAST(15.00 AS Decimal(10, 2)), 6, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (14, N'Winter Jacket Woman', CAST(110.00 AS Decimal(10, 2)), 34, 23, CAST(0.00 AS Decimal(10, 2)), 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (15, N'Winter Jacket Man', CAST(110.00 AS Decimal(10, 2)), 32, 12, CAST(0.00 AS Decimal(10, 2)), 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (16, N'Bike Treck', CAST(2699.00 AS Decimal(10, 2)), 2, 1, CAST(10.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (18, N'Bike Specialized', CAST(4000.00 AS Decimal(10, 2)), 1, 1, CAST(0.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (20, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 15, 0, CAST(15.00 AS Decimal(10, 2)), 3, CAST(N'2022-02-20T19:15:30.3400000' AS DateTime2), CAST(N'2022-02-20T19:15:30.340' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (21, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 15, 0, NULL, 3, CAST(N'2022-02-21T00:25:45.8290000' AS DateTime2), CAST(N'2022-02-21T00:25:45.830' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (22, N'Iphone', CAST(1050.00 AS Decimal(10, 2)), 15, 0, NULL, 3, CAST(N'2022-02-21T00:26:26.7520000' AS DateTime2), CAST(N'2022-02-21T00:26:26.753' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (23, N'Ipad', CAST(1050.00 AS Decimal(10, 2)), 15, 0, NULL, 3, CAST(N'2022-02-21T00:27:42.4250000' AS DateTime2), CAST(N'2022-02-21T00:39:06.980' AS DateTime))
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([IdRole], [Name]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([IdRole], [Name]) VALUES (2, N'Client')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([IdUser], [Username], [Name], [Password], [FK_IdRole], [Created], [Updated]) VALUES (1, N'XimenaC', N'Ximane', NULL, 1, CAST(N'2007-05-08T12:35:29.123' AS DateTime), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [Password], [FK_IdRole], [Created], [Updated]) VALUES (2, N'JairF', N'Jair', NULL, 1, CAST(N'2007-05-08T12:35:29.123' AS DateTime), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [Password], [FK_IdRole], [Created], [Updated]) VALUES (16, N'NicolasL', N'Nicolas', NULL, 2, CAST(N'2007-05-08T12:35:29.123' AS DateTime), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Users] ([IdUser], [Username], [Name], [Password], [FK_IdRole], [Created], [Updated]) VALUES (17, N'SharmineB', N'Sharmine', NULL, 2, CAST(N'2007-05-08T12:35:29.123' AS DateTime), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
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
