USE [PetDB]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryCode] [nvarchar](10) NULL,
	[CountryName] [nvarchar](255) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](3) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DateTimeFormat]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DateTimeFormat](
	[Id] [bigint] NOT NULL,
	[Format] [nvarchar](25) NULL,
	[FormatType] [tinyint] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_DateTimeFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feature]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feature](
	[Id] [bigint] NOT NULL,
	[FeatureName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[FeatureType] [int] NOT NULL,
	[IsFeature] [bit] NULL,
	[Status] [tinyint] NOT NULL,
	[Sequence] [int] NOT NULL,
	[ParentId] [int] NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](3) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_Feature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NULL,
	[Status] [tinyint] NOT NULL,
	[RoleType] [tinyint] NOT NULL,
	[Description] [nvarchar](550) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [Roles_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleFeature]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFeature](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[FeatureId] [bigint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_RoleFeature] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeZone]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeZone](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TimeZoneId] [varchar](100) NOT NULL,
	[TimeZoneName] [nvarchar](100) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_TimeZone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[SaltPassword] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[CountryId] [bigint] NULL,
	[Phone] [nvarchar](50) NULL,
	[City] [nvarchar](255) NULL,
	[StateId] [bigint] NULL,
	[ZipCode] [nvarchar](25) NULL,
	[TimeZoneId] [Bigint] NULL,
	[DateFormatId] [int] NULL,
	[TimeFormatId] [int] NULL,
	[UserType] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[IsLock] [bit] NULL,
	[CountFailSignIn] [int] NULL,
	[PasswordExpiredDate] [datetime2](3) NULL,
	[ImagePath] [nvarchar](250) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](3) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 8/25/2022 14:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedTime] [datetime2](7) NULL,
	[UpdatedBy] [bigint] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [CountryCode], [CountryName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'US', N'US', CAST(N'2021-04-23T09:57:49.5433333' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Country] ([Id], [CountryCode], [CountryName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, N'VN', N'Việt Nam', CAST(N'2022-08-25T14:26:42.5166667' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'MM/dd/yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, N'MM/dd/yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (3, N'MM-dd-yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (4, N'MM-dd-yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (5, N'MM.dd.yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (6, N'MM.dd.yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (7, N'MMddyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (8, N'MMddyyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (9, N'dd/MM/yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (10, N'dd/MM/yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (11, N'dd-MM-yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (12, N'dd-MM-yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (13, N'yy.MM.dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (14, N'yyyy.MM.dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (15, N'yyyy-MM-dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (16, N'yy-MM-dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (17, N'yy/MM/dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (18, N'yyyy/MM/dd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (19, N'yyMMdd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (20, N'yyyyMMdd', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (21, N'dd MMM yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (22, N'dd MMM yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (23, N'MMM dd; yy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (24, N'MMM dd; yyyy', 1, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (25, N'hh:mm tt', 2, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (26, N'hh:mm:ss tt', 2, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (27, N'HH:mm', 2, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[DateTimeFormat] ([Id], [Format], [FormatType], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (28, N'HH:mm:ss', 2, CAST(N'2021-04-14T17:14:09.0966667' AS DateTime2), 1, NULL, NULL)
GO
INSERT [dbo].[Feature] ([Id], [FeatureName], [Description], [FeatureType], [IsFeature], [Status], [Sequence], [ParentId], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'InsertCountry', N'Insert New Country', 1, 1, 1, 1, NULL, CAST(N'2022-08-25T14:15:12.2533333' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Feature] ([Id], [FeatureName], [Description], [FeatureType], [IsFeature], [Status], [Sequence], [ParentId], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, N'UpdateCountry', N'Update Country', 1, 1, 1, 2, NULL, CAST(N'2022-08-25T14:15:45.6266667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Feature] ([Id], [FeatureName], [Description], [FeatureType], [IsFeature], [Status], [Sequence], [ParentId], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (3, N'DeleteCountry', N'Delete Country', 1, 1, 1, 3, NULL, CAST(N'2022-08-25T14:16:16.1933333' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[Feature] ([Id], [FeatureName], [Description], [FeatureType], [IsFeature], [Status], [Sequence], [ParentId], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (4, N'GetCountries', N'Get Countries', 1, 1, 1, 4, NULL, CAST(N'2022-08-25T14:16:46.9266667' AS DateTime2), 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [RoleName], [Status], [RoleType], [Description], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'Country Management', 1, 1, N'Country Management', CAST(N'2022-08-25T14:18:22.8800000' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleFeature] ON 

INSERT [dbo].[RoleFeature] ([Id], [RoleId], [FeatureId], [Status], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, 1, 1, 1, CAST(N'2022-08-25T14:20:24.2766667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[RoleFeature] ([Id], [RoleId], [FeatureId], [Status], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, 1, 2, 1, CAST(N'2022-08-25T14:20:26.4633333' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[RoleFeature] ([Id], [RoleId], [FeatureId], [Status], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (3, 1, 3, 1, CAST(N'2022-08-25T14:20:28.3733333' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[RoleFeature] ([Id], [RoleId], [FeatureId], [Status], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (4, 1, 4, 1, CAST(N'2022-08-25T14:20:30.5366667' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleFeature] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeZone] ON 

INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'Afghanistan Standard Time', N'(GMT+04:30) Kabul', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (2, N'Alaskan Standard Time', N'(GMT-09:00) Alaska', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (3, N'Arab Standard Time', N'(GMT+03:00) Kuwait, Riyadh', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (4, N'Arabian Standard Time', N'(GMT+04:00) Abu Dhabi, Muscat', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (5, N'Arabic Standard Time', N'(GMT+03:00) Baghdad', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (6, N'Argentina Standard Time', N'(GMT-03:00) Buenos Aires', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (7, N'Atlantic Standard Time', N'(GMT-04:00) Atlantic Time (Canada)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (8, N'AUS Central Standard Time', N'(GMT+09:30) Darwin', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (9, N'AUS Eastern Standard Time', N'(GMT+10:00) Canberra, Melbourne, Sydney', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (10, N'Azerbaijan Standard Time', N'(GMT+04:00) Baku', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (11, N'Azores Standard Time', N'(GMT-01:00) Azores', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (12, N'Canada Central Standard Time', N'(GMT-06:00) Saskatchewan', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (13, N'Cape Verde Standard Time', N'(GMT-01:00) Cape Verde Is.', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (14, N'Caucasus Standard Time', N'(GMT+04:00) Yerevan', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (15, N'Cen. Australia Standard Time', N'(GMT+09:30) Adelaide', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (16, N'Central America Standard Time', N'(GMT-06:00) Central America', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (17, N'Central Asia Standard Time', N'(GMT+06:00) Astana, Dhaka', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (18, N'Central Brazilian Standard Time', N'(GMT-04:00) Manaus', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (19, N'Central Europe Standard Time', N'(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (20, N'Central European Standard Time', N'(GMT+01:00) Sarajevo, Skopje, Warsaw, Zagreb', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (21, N'Central Pacific Standard Time', N'(GMT+11:00) Magadan, Solomon Is., New Caledonia', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (22, N'Central Standard Time', N'(GMT-06:00) Central Time (US & Canada)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (23, N'Central Standard Time (Mexico)', N'(GMT-06:00) Guadalajara, Mexico City, Monterrey', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (24, N'China Standard Time', N'(GMT+08:00) Beijing, Chongqing, Hong Kong, Urumqi', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (25, N'Dateline Standard Time', N'(GMT-12:00) International Date Line West', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (26, N'E. Africa Standard Time', N'(GMT+03:00) Nairobi', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (27, N'E. Australia Standard Time', N'(GMT+10:00) Brisbane', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (28, N'E. Europe Standard Time', N'(GMT+02:00) Minsk', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (29, N'E. South America Standard Time', N'(GMT-03:00) Brasilia', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (30, N'Eastern Standard Time', N'(GMT-05:00) Eastern Time (US & Canada)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (31, N'Egypt Standard Time', N'(GMT+02:00) Cairo', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (32, N'Ekaterinburg Standard Time', N'(GMT+05:00) Ekaterinburg', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (33, N'Fiji Standard Time', N'(GMT+12:00) Fiji, Kamchatka, Marshall Is.', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (34, N'FLE Standard Time', N'(GMT+02:00) Helsinki, Kyiv, Riga, Sofia, Tallinn, Vilnius', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (35, N'Georgian Standard Time', N'(GMT+03:00) Tbilisi', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (36, N'GMT Standard Time', N'(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (37, N'Greenland Standard Time', N'(GMT-03:00) Greenland', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (38, N'Greenwich Standard Time', N'(GMT) Monrovia, Reykjavik', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (39, N'GTB Standard Time', N'(GMT+02:00) Athens, Bucharest, Istanbul', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (40, N'Hawaiian Standard Time', N'(GMT-10:00) Hawaii', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (41, N'India Standard Time', N'(GMT+05:30) Chennai, Kolkata, Mumbai, New Delhi', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (42, N'Iran Standard Time', N'(GMT+03:30) Tehran', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (43, N'Israel Standard Time', N'(GMT+02:00) Jerusalem', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (44, N'Jordan Standard Time', N'(GMT+02:00) Amman', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (45, N'Korea Standard Time', N'(GMT+09:00) Seoul', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (46, N'Mauritius Standard Time', N'(GMT+04:00) Port Louis', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (47, N'Mid-Atlantic Standard Time', N'(GMT-02:00) Mid-Atlantic', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (48, N'Middle East Standard Time', N'(GMT+02:00) Beirut', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (49, N'Montevideo Standard Time', N'(GMT-03:00) Montevideo', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (50, N'Morocco Standard Time', N'(GMT) Casablanca', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (51, N'Mountain Standard Time', N'(GMT-07:00) Mountain Time (US & Canada)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (52, N'Mountain Standard Time (Mexico)', N'(GMT-07:00) Chihuahua, La Paz, Mazatlan', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (53, N'Myanmar Standard Time', N'(GMT+06:30) Yangon (Rangoon)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (54, N'N. Central Asia Standard Time', N'(GMT+06:00) Almaty, Novosibirsk', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (55, N'Namibia Standard Time', N'(GMT+02:00) Windhoek', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (56, N'Nepal Standard Time', N'(GMT+05:45) Kathmandu', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (57, N'New Zealand Standard Time', N'(GMT+12:00) Auckland, Wellington', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (58, N'Newfoundland Standard Time', N'(GMT-03:30) Newfoundland', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (59, N'North Asia East Standard Time', N'(GMT+08:00) Irkutsk, Ulaan Bataar', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (60, N'North Asia Standard Time', N'(GMT+07:00) Krasnoyarsk', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (61, N'Pacific SA Standard Time', N'(GMT-04:00) Santiago', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (62, N'Pacific Standard Time', N'(GMT-08:00) Pacific Time (US & Canada)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (63, N'Pacific Standard Time (Mexico)', N'(GMT-08:00) Tijuana, Baja California', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (64, N'Pakistan Standard Time', N'(GMT+05:00) Islamabad, Karachi', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (65, N'Romance Standard Time', N'(GMT+01:00) Brussels, Copenhagen, Madrid, Paris', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (66, N'Russian Standard Time', N'(GMT+03:00) Moscow, St. Petersburg, Volgograd', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (67, N'SA Eastern Standard Time', N'(GMT-03:00) Georgetown', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (68, N'SA Pacific Standard Time', N'(GMT-05:00) Bogota, Lima, Quito, Rio Branco', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (69, N'SA Western Standard Time', N'(GMT-04:00) La Paz', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (70, N'Samoa Standard Time', N'(GMT-11:00) Midway Island, Samoa', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (71, N'SE Asia Standard Time', N'(GMT+07:00) Bangkok, Hanoi, Jakarta', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (72, N'Singapore Standard Time', N'(GMT+08:00) Kuala Lumpur, Singapore', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (73, N'South Africa Standard Time', N'(GMT+02:00) Harare, Pretoria', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (74, N'Sri Lanka Standard Time', N'(GMT+05:30) Sri Jayawardenepura', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (75, N'Taipei Standard Time', N'(GMT+08:00) Taipei', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (76, N'Tasmania Standard Time', N'(GMT+10:00) Hobart', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (77, N'Tokyo Standard Time', N'(GMT+09:00) Osaka, Sapporo, Tokyo', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (78, N'Tonga Standard Time', N'(GMT+13:00) Nuku''alofa', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (79, N'US Eastern Standard Time', N'(GMT-05:00) Indiana (East)', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (80, N'US Mountain Standard Time', N'(GMT-07:00) Arizona', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (81, N'Venezuela Standard Time', N'(GMT-04:30) Caracas', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (82, N'Vladivostok Standard Time', N'(GMT+10:00) Vladivostok', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (83, N'W. Australia Standard Time', N'(GMT+08:00) Perth', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (84, N'W. Central Africa Standard Time', N'(GMT+01:00) West Central Africa', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (85, N'W. Europe Standard Time', N'(GMT+01:00) Amsterdam, Berlin, Bern, Rome, Stockholm, Vienna', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (86, N'West Asia Standard Time', N'(GMT+05:00) Tashkent', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (87, N'West Pacific Standard Time', N'(GMT+10:00) Guam, Port Moresby', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
INSERT [dbo].[TimeZone] ([Id], [TimeZoneId], [TimeZoneName], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (88, N'Yakutsk Standard Time', N'(GMT+09:00) Yakutsk', CAST(N'2021-05-21T11:53:06.3466667' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TimeZone] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Password], [SaltPassword], [FirstName], [LastName], [Address1], [Address2], [CountryId], [Phone], [City], [StateId], [ZipCode], [TimeZoneId], [DateFormatId], [TimeFormatId], [UserType], [Status], [IsLock], [CountFailSignIn], [PasswordExpiredDate], [ImagePath], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, N'sysadmin', N'G+0b/tRkD6OTosxXsV1ed9mSntYcuEB0SDfM1JpO4mg=', N'Dhos2mi7ZDxg3xluwxLZdQ==', N'system', N'administrator', N'', N'', 1, N'0000', N'HCM city', 1, N'ZipCode', 1, 1, 1, 1, 1, 0, 0, NULL, N'', CAST(N'2022-08-25T14:25:15.0200000' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRole] ON 

INSERT [dbo].[UserRole] ([Id], [UserId], [RoleId], [Status], [CreatedTime], [CreatedBy], [UpdatedTime], [UpdatedBy]) VALUES (1, 1, 1, 1, CAST(N'2022-08-25T14:25:45.8733333' AS DateTime2), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserRole] OFF
GO
ALTER TABLE [dbo].[RoleFeature]  WITH CHECK ADD  CONSTRAINT [FK_RoleFeature_Feature] FOREIGN KEY([FeatureId])
REFERENCES [dbo].[Feature] ([Id])
GO
ALTER TABLE [dbo].[RoleFeature] CHECK CONSTRAINT [FK_RoleFeature_Feature]
GO
ALTER TABLE [dbo].[RoleFeature]  WITH CHECK ADD  CONSTRAINT [FK_RoleFeature_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[RoleFeature] CHECK CONSTRAINT [FK_RoleFeature_Role]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Country]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
