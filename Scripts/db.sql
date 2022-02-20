USE [master]
GO
/****** Object:  Database [ShoppingDatabase]    Script Date: 2/19/2022 9:11:07 PM ******/
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
/****** Object:  Table [dbo].[Cart_Products]    Script Date: 2/19/2022 9:11:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart_Products](
	[FK_IdCart] [bigint] NOT NULL,
	[FK_IdProduct] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Discount] [decimal](10, 2) NULL,
	[Subtotal] [decimal](10, 2) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 2/19/2022 9:11:07 PM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 2/19/2022 9:11:07 PM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 2/19/2022 9:11:07 PM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 2/19/2022 9:11:07 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 2/19/2022 9:11:07 PM ******/
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
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (3, N'Talks Toy', CAST(36.99 AS Decimal(10, 2)), 54, 0, CAST(5.00 AS Decimal(10, 2)), 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (4, N'Lego City Street', CAST(32.00 AS Decimal(10, 2)), 21, 0, CAST(0.00 AS Decimal(10, 2)), 2, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (5, N'Macbook Pro 14"', CAST(2499.99 AS Decimal(10, 2)), 5, 1, CAST(10.00 AS Decimal(10, 2)), 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (6, N'Lenovo Thinkpad P73', CAST(7837.84 AS Decimal(10, 2)), 3, 0, CAST(0.00 AS Decimal(10, 2)), 3, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (7, N'Turmeric 3050mg 120', CAST(1197.00 AS Decimal(10, 2)), 138, 48, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (8, N'Chlorophyll Drops 100ml', CAST(26.99 AS Decimal(10, 2)), 98, 3, CAST(10.00 AS Decimal(10, 2)), 4, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (10, N'Sunscreen Lotion', CAST(11.97 AS Decimal(10, 2)), 76, 32, CAST(0.00 AS Decimal(10, 2)), 5, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (11, N'Makeup Remover', CAST(8.98 AS Decimal(10, 2)), 32, 1, CAST(0.00 AS Decimal(10, 2)), 5, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (12, N'PlayStation 5', CAST(1049.00 AS Decimal(10, 2)), 2, 0, CAST(0.00 AS Decimal(10, 2)), 6, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (13, N'Gaming Chair', CAST(319.99 AS Decimal(10, 2)), 43, 32, CAST(15.00 AS Decimal(10, 2)), 6, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (14, N'Winter Jacket Woman', CAST(110.00 AS Decimal(10, 2)), 34, 23, CAST(0.00 AS Decimal(10, 2)), 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (15, N'Winter Jacket Man', CAST(110.00 AS Decimal(10, 2)), 32, 12, CAST(0.00 AS Decimal(10, 2)), 7, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (16, N'Bike Treck', CAST(2699.00 AS Decimal(10, 2)), 2, 1, CAST(10.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
INSERT [dbo].[Products] ([IdProduct], [Name], [Price], [Stock], [UnitSold], [Discount], [FK_IdCategory], [Created], [Updated]) VALUES (18, N'Bike Specialized', CAST(4000.00 AS Decimal(10, 2)), 1, 1, CAST(0.00 AS Decimal(10, 2)), 8, CAST(N'2007-05-08T12:35:29.1230000' AS DateTime2), CAST(N'2007-05-08T12:35:29.123' AS DateTime))
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
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_Carts_Users] FOREIGN KEY([IdCart])
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
