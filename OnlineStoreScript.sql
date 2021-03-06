USE [master]
GO
/****** Object:  Database [OnlineStore]    Script Date: 09.02.2021 16:53:04 ******/
CREATE DATABASE [OnlineStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OnlineStore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OnlineStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\OnlineStore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OnlineStore] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineStore] SET RECOVERY FULL 
GO
ALTER DATABASE [OnlineStore] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'OnlineStore', N'ON'
GO
ALTER DATABASE [OnlineStore] SET QUERY_STORE = OFF
GO
USE [OnlineStore]
GO
/****** Object:  Table [dbo].[ClientProducts]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StatusId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameManager] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[SubscriptionTypesId] [int] NOT NULL,
	[SubscriptionTermId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameStatus] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionTerm]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SubscriptionsName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubscriptionTypes]    Script Date: 09.02.2021 16:53:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ClientProducts] ON 

INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (1, 1, 9)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (2, 2, 10)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (3, 1, 12)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (4, 3, 13)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (5, 3, 11)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (6, 4, 9)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (7, 4, 10)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (8, 4, 12)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (9, 5, 10)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (10, 5, 13)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (11, 6, 10)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (12, 7, 9)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (13, 7, 12)
INSERT [dbo].[ClientProducts] ([Id], [ClientId], [ProductId]) VALUES (14, 7, 13)
SET IDENTITY_INSERT [dbo].[ClientProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (1, N'Victoriya', 2, 1)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (2, N'Jayden', 1, 5)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (3, N'Kira', 2, 2)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (4, N'Mariya', 2, 5)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (5, N'Irina', 1, 4)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (6, N'Gleb', 1, 3)
INSERT [dbo].[Clients] ([Id], [Name], [StatusId], [ManagerId]) VALUES (7, N'Oleg', 1, 1)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Managers] ON 

INSERT [dbo].[Managers] ([Id], [NameManager]) VALUES (1, N'John')
INSERT [dbo].[Managers] ([Id], [NameManager]) VALUES (2, N'Mike')
INSERT [dbo].[Managers] ([Id], [NameManager]) VALUES (3, N'Nick')
INSERT [dbo].[Managers] ([Id], [NameManager]) VALUES (4, N'Frank')
INSERT [dbo].[Managers] ([Id], [NameManager]) VALUES (5, N'Leonid')
SET IDENTITY_INSERT [dbo].[Managers] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Price], [SubscriptionTypesId], [SubscriptionTermId]) VALUES (9, N'Spotify', 300.0000, 1, 1)
INSERT [dbo].[Products] ([Id], [Name], [Price], [SubscriptionTypesId], [SubscriptionTermId]) VALUES (10, N'Netflix', 500.0000, 1, 1)
INSERT [dbo].[Products] ([Id], [Name], [Price], [SubscriptionTypesId], [SubscriptionTermId]) VALUES (11, N'Antivirus', 250.0000, 2, NULL)
INSERT [dbo].[Products] ([Id], [Name], [Price], [SubscriptionTypesId], [SubscriptionTermId]) VALUES (12, N'HH', 30.0000, 1, 2)
INSERT [dbo].[Products] ([Id], [Name], [Price], [SubscriptionTypesId], [SubscriptionTermId]) VALUES (13, N'HBO Max', 700.0000, 1, 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [NameStatus]) VALUES (1, N'Premium')
INSERT [dbo].[Status] ([Id], [NameStatus]) VALUES (2, N'Standart')
SET IDENTITY_INSERT [dbo].[Status] OFF
GO
SET IDENTITY_INSERT [dbo].[SubscriptionTerm] ON 

INSERT [dbo].[SubscriptionTerm] ([Id], [SubscriptionsName]) VALUES (1, N'Month')
INSERT [dbo].[SubscriptionTerm] ([Id], [SubscriptionsName]) VALUES (2, N'Quarte')
INSERT [dbo].[SubscriptionTerm] ([Id], [SubscriptionsName]) VALUES (3, N'Year')
SET IDENTITY_INSERT [dbo].[SubscriptionTerm] OFF
GO
SET IDENTITY_INSERT [dbo].[SubscriptionTypes] ON 

INSERT [dbo].[SubscriptionTypes] ([Id], [Name]) VALUES (1, N'Subscription')
INSERT [dbo].[SubscriptionTypes] ([Id], [Name]) VALUES (2, N'License')
SET IDENTITY_INSERT [dbo].[SubscriptionTypes] OFF
GO
ALTER TABLE [dbo].[ClientProducts]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[ClientProducts]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([Id])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([SubscriptionTypesId])
REFERENCES [dbo].[SubscriptionTypes] ([Id])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([SubscriptionTermId])
REFERENCES [dbo].[SubscriptionTerm] ([Id])
GO
USE [master]
GO
ALTER DATABASE [OnlineStore] SET  READ_WRITE 
GO
