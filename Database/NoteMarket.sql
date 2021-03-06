USE [Notes-Marketplace]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 06-04-2021 16:59:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[CountryCode] [int] NULL,
	[PhoneNumber] [varchar](15) NULL,
	[ProfilePicture] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buyer]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buyer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsPaid] [bit] NOT NULL,
	[Price] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[RequestDate] [datetime] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contactus]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contactus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Subject] [varchar](100) NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[SubmitDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Contactus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inappropriate]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inappropriate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Remarks] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
	[DeactivateDate] [datetime] NULL,
	[DeactivateBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Inappropriate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManageSystem]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageSystem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupportEmail] [varchar](100) NOT NULL,
	[SupportContactNumber] [varchar](20) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[NoteDisplayPicture] [varchar](max) NULL,
	[MemberDisplayPicture] [varchar](max) NULL,
	[FacebookLink] [varchar](100) NULL,
	[TwitterLink] [varchar](100) NULL,
	[LinkedlnLink] [varchar](100) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ManageSystem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[EmailVerified] [bit] NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[DeactivateDate] [datetime] NULL,
	[DeactivateBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberProfile]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MemberId] [int] NOT NULL,
	[BirthDate] [date] NULL,
	[Gender] [varchar](20) NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](max) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Zipcode] [int] NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_MemberProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[NoteId] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[DisplayPicture] [varchar](max) NULL,
	[UploadNotes] [varchar](max) NOT NULL,
	[Type] [varchar](50) NULL,
	[TotalPages] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[Country] [varchar](50) NULL,
	[InstitutionName] [varchar](200) NULL,
	[CourseName] [varchar](100) NULL,
	[CourseCode] [int] NULL,
	[Professor] [varchar](100) NULL,
	[SellType] [varchar](20) NOT NULL,
	[SellPrice] [int] NOT NULL,
	[NotePreview] [varchar](max) NULL,
	[Status] [varchar](100) NOT NULL,
	[AdminRemarks] [varchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ApprovedDate] [datetime] NULL,
	[ApprovedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NoteId] [int] NOT NULL,
	[OwnerId] [int] NOT NULL,
	[MemberId] [int] NOT NULL,
	[Rate] [int] NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 06-04-2021 16:59:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([AdminId], [FirstName], [LastName], [SecondaryEmail], [Email], [Password], [Type], [CountryCode], [PhoneNumber], [ProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Dhruvil', N'Shah', NULL, N'dhruvilshah191999@gmail.com', N'123456', N'SuperAdmin', NULL, NULL, NULL, CAST(N'2021-02-15T11:19:03.890' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Admin] ([AdminId], [FirstName], [LastName], [SecondaryEmail], [Email], [Password], [Type], [CountryCode], [PhoneNumber], [ProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Harshil', N'Patel', NULL, N'harshil@gmail.com', N'Admin@123', N'Admin', 1, N'6354883566', N'Harshil_210644881.png', CAST(N'2021-03-15T16:25:48.910' AS DateTime), NULL, CAST(N'2021-03-25T15:09:20.423' AS DateTime), NULL, 1)
INSERT [dbo].[Admin] ([AdminId], [FirstName], [LastName], [SecondaryEmail], [Email], [Password], [Type], [CountryCode], [PhoneNumber], [ProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Umang', N'Patel', NULL, N'umang@gmail.com', N'Umang@123', N'Admin', 93, N'9825647029', N'Umang_214615059.jpg', CAST(N'2021-04-02T14:01:16.667' AS DateTime), 1, CAST(N'2021-04-06T12:46:15.060' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Buyer] ON 

INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1005, 2028, 3009, 3015, 1, N'211722995/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211723004.pdf', 1, 0, N'Hands-On Machine Learning with Scikit-Learn', N'EC', CAST(N'2021-04-06T15:07:51.277' AS DateTime), CAST(N'2021-04-06T15:07:51.277' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1006, 2026, 3016, 3015, 0, N'211552180/UploadedNotes/170170116049_Assignment1_211552198.pdf', 0, 70, N'My Hero Academia', N'Story Book', CAST(N'2021-04-06T15:09:44.923' AS DateTime), CAST(N'2021-04-06T15:09:44.923' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1007, 2031, 3013, 3015, 1, N'211154625/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211154637.pdf', 1, 200, N'The Gray Man', N'Story Book', CAST(N'2021-04-06T15:22:25.563' AS DateTime), CAST(N'2021-04-06T16:00:15.273' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1008, 2019, 3011, 3015, 1, N'215029167/UploadedNotes/death-note-chapter-1-manga_compress_215029171.pdf', 1, 100, N'Death Note', N'Story Book', CAST(N'2021-04-06T15:47:02.723' AS DateTime), CAST(N'2021-04-06T15:54:41.787' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1009, 2019, 3011, 3014, 1, N'215029167/UploadedNotes/death-note-chapter-1-manga_compress_215029171.pdf', 1, 100, N'Death Note', N'Story Book', CAST(N'2021-04-06T15:47:49.740' AS DateTime), CAST(N'2021-04-06T15:54:33.923' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1010, 2022, 3016, 3014, 1, N'214049333/UploadedNotes/170170116049_Practical3_214049339.pdf', 1, 170, N'Designing Evolvable Web APIs with ASP.NET', N'CA', CAST(N'2021-04-06T15:48:07.877' AS DateTime), CAST(N'2021-04-06T15:57:50.713' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1011, 2026, 3016, 3014, 1, N'211552180/UploadedNotes/170170116049_Assignment1_211552198.pdf', 1, 70, N'My Hero Academia', N'Story Book', CAST(N'2021-04-06T15:48:44.510' AS DateTime), CAST(N'2021-04-06T15:57:44.863' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1012, 2032, 3013, 3014, 0, N'212118395/UploadedNotes/170170116049_AI_Answer_212118410.pdf', 0, 50, N'Sinbad and the Trumpet of Israfil', N'Action', CAST(N'2021-04-06T15:49:32.130' AS DateTime), CAST(N'2021-04-06T15:49:32.130' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1013, 2019, 3011, 3010, 1, N'215029167/UploadedNotes/death-note-chapter-1-manga_compress_215029171.pdf', 1, 100, N'Death Note', N'Story Book', CAST(N'2021-04-06T15:50:41.583' AS DateTime), CAST(N'2021-04-06T15:54:25.697' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1014, 2017, 3011, 3010, 1, N'212015389/UploadedNotes/170170116035_AIassign2_212015401.pdf', 1, 50, N'Eloquent JavaScript, Second Edition', N'IT', CAST(N'2021-04-06T15:50:54.173' AS DateTime), CAST(N'2021-04-06T15:53:59.907' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1015, 2027, 3009, 3010, 1, N'211002776/UploadedNotes/TSC Training Plan for Batch 2020-2021_211002791.pdf', 1, 120, N'Data Science For Dummies', N'IT', CAST(N'2021-04-06T15:51:13.920' AS DateTime), CAST(N'2021-04-06T15:59:33.647' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1016, 2029, 3009, 3010, 1, N'212227041/UploadedNotes/TSC Training Plan for Batch 2020-2021_212227057.pdf', 1, 250, N'Clean Code: A Handbook of Agile Software', N'IT', CAST(N'2021-04-06T15:51:30.650' AS DateTime), CAST(N'2021-04-06T15:59:27.577' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1017, 2026, 3016, 3011, 1, N'211552180/UploadedNotes/170170116049_Assignment1_211552198.pdf', 1, 70, N'My Hero Academia', N'Story Book', CAST(N'2021-04-06T16:16:46.067' AS DateTime), CAST(N'2021-04-06T16:18:00.113' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1018, 2031, 3013, 3011, 1, N'211154625/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211154637.pdf', 1, 200, N'The Gray Man', N'Story Book', CAST(N'2021-04-06T16:17:22.270' AS DateTime), CAST(N'2021-04-06T16:18:25.587' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Buyer] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'IT', N'Lorem', CAST(N'2021-02-15T11:26:33.313' AS DateTime), 1, CAST(N'2021-03-16T21:22:13.853' AS DateTime), 2, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'CA', N'Lorem Lorem', CAST(N'2021-02-15T11:26:41.203' AS DateTime), 1, CAST(N'2021-04-06T12:48:16.927' AS DateTime), 3, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'MBA', N'Lorem Ipsum is simply dummy text Lorem Ipsum is simply dummy text', CAST(N'2021-02-15T11:26:46.647' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Story Book', N'Lorem', CAST(N'2021-02-15T11:26:55.020' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Action', N'Lorem', CAST(N'2021-02-15T11:27:04.777' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'EC', N'Lorem', CAST(N'2021-03-16T19:58:29.617' AS DateTime), 2, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Contactus] ON 

INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (1, N'Dhruvil Shah', N'dhruvilshah191999@gmail.com', N'About Website Loding', N'The Website Image Load the Time', CAST(N'2021-02-11T11:27:54.363' AS DateTime))
INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (2, N'Harsh Patel', N'dhruvil@gmail.com', N'Testing', N'tyujykau.hlksjJ:skadjsfckla;sadjsfdls;asakcscmj,ska;lcfjnclkscckjlds;a', CAST(N'2021-02-21T11:35:30.820' AS DateTime))
INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (1002, N'Dhruvil Shah', N'darkgoku191999@gmail.com', N'About Website Loding', N'sdsdasdasd', CAST(N'2021-02-21T17:12:47.507' AS DateTime))
INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (2002, N'Dhruvil Shah', N'dhruvilshah191999@gmail.com', N'Hellodzfdf', N'sdfcgvhgxdfsaddsfcgvhgxcfdsasdfxcgfdsa', CAST(N'2021-02-25T22:13:04.150' AS DateTime))
INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (2004, N'Dhruvil Shah', N'dhruvilshah191999@gmail.com', N'About Website Loding', N'sxzdxfcxdsafd', CAST(N'2021-03-02T20:33:54.320' AS DateTime))
INSERT [dbo].[Contactus] ([Id], [Name], [Email], [Subject], [Comment], [SubmitDate]) VALUES (3002, N'Harshil Shah', N'harshil@gmail.com', N'Mi TV ', N'I like to share thought about mi tv', CAST(N'2021-03-06T16:11:30.183' AS DateTime))
SET IDENTITY_INSERT [dbo].[Contactus] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', 91, CAST(N'2021-02-11T21:53:14.447' AS DateTime), 2, CAST(N'2021-03-15T16:50:45.777' AS DateTime), 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Australia', 61, CAST(N'2021-02-11T21:53:30.757' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'New Zealand', 64, CAST(N'2021-02-11T21:53:44.437' AS DateTime), 1, CAST(N'2021-04-06T14:42:51.773' AS DateTime), 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'United States', 1, CAST(N'2021-02-11T21:54:18.110' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Afghanistan', 93, CAST(N'2021-02-14T15:51:36.030' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Bangladesh', 880, CAST(N'2021-02-14T15:51:54.877' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Brazil', 55, CAST(N'2021-02-14T15:52:09.053' AS DateTime), 1, CAST(N'2021-03-15T22:49:44.467' AS DateTime), 2, 1)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Inappropriate] ON 

INSERT [dbo].[Inappropriate] ([Id], [NoteId], [OwnerId], [MemberId], [Remarks], [Date], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (1003, 2022, 3016, 3014, N'This is the worst book I have seen .there are not understanding any topic ', CAST(N'2021-04-06T16:15:52.823' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Inappropriate] OFF
GO
SET IDENTITY_INSERT [dbo].[ManageSystem] ON 

INSERT [dbo].[ManageSystem] ([Id], [SupportEmail], [SupportContactNumber], [EmailAddress], [NoteDisplayPicture], [MemberDisplayPicture], [FacebookLink], [TwitterLink], [LinkedlnLink], [ModifiedDate], [ModifiedBy]) VALUES (1, N'notemarketplace44@gmail.com', N'6354883566', N'notemarketplace44@gmail.com', N'default-book.jpg', N'Travel.jpg', NULL, NULL, NULL, CAST(N'2021-04-06T14:52:28.590' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[ManageSystem] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3009, N'Note ', N'Market', N'notemarketplace44@gmail.com', 1, N'Note@123', CAST(N'2021-04-05T19:46:25.113' AS DateTime), NULL, CAST(N'2021-04-05T19:47:03.910' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3010, N'Umang', N'Patel', N'umang@gmail.com', 1, N'umang@123', CAST(N'2021-04-05T20:09:01.007' AS DateTime), NULL, CAST(N'2021-04-05T20:57:33.560' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3011, N'Dhruvil', N'Shah', N'dhruvilshah191999@gmail.com', 1, N'Dhruvil@123', CAST(N'2021-04-05T20:42:32.087' AS DateTime), NULL, CAST(N'2021-04-05T20:54:05.097' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3013, N'Crossify', N'Solanki', N'crossify.vgec@gmail.com', 1, N'Harshil@123', CAST(N'2021-04-05T20:50:33.140' AS DateTime), NULL, CAST(N'2021-04-06T14:35:02.283' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3014, N'Harshil', N'Patel', N'digi5technologies@gmail.com', 1, N'harshil@123', CAST(N'2021-04-05T20:53:02.457' AS DateTime), NULL, CAST(N'2021-04-05T20:54:55.807' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3015, N'Sagar', N'Solanki', N'silentsaguu@gmail.com', 1, N'sagar@123', CAST(N'2021-04-05T20:57:06.603' AS DateTime), NULL, CAST(N'2021-04-05T20:57:18.227' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3016, N'Dark', N'Goku', N'darkgoku191999@gmail.com', 1, N'Goku@123', CAST(N'2021-04-05T20:58:43.670' AS DateTime), NULL, CAST(N'2021-04-05T20:59:05.360' AS DateTime), NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberProfile] ON 

INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1005, 3011, CAST(N'1999-09-19' AS Date), N'Male', N'+91', N'6354883566', N'Dhruvil_210221703.png', N'Sendhi Mata', N'Tapal Chowk,Sanand', N'Ahmedabad', N'Gujarat', 382110, N'India', N'Gujarat Technological University', N'Vishwakarma Government Engineering College', CAST(N'2021-04-05T21:01:07.690' AS DateTime), NULL, CAST(N'2021-04-05T21:02:21.703' AS DateTime), NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1006, 3013, CAST(N'2001-05-05' AS Date), N'Female', N'+1', N'1538647921', NULL, N'156 Shadowmar Drive', N'New York', N'New York', N'NY', 85012, N'United States', NULL, NULL, CAST(N'2021-04-05T21:38:59.860' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1007, 3009, CAST(N'1996-05-05' AS Date), N'Male', N'+91', N'9825644029', N'Note _214059903.jpg', N'TatvaSoft House, Opp. Kensville Golf Academy', N'Near Shivalik Business Center, Sarkhej - Gandhinagar Highway', N'Ahmedabad', N'Gujarat', 380054, N'India', NULL, NULL, CAST(N'2021-04-05T21:40:59.907' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1008, 3010, NULL, N'Male', N'+1', N'9246582421', N'Umang_215012740.jpg', N'15 , Shivneri Bldg, ', N'Sector a, Kopar Khairne', N'Mumbai', N'Maharashtra', 400709, N'India', NULL, NULL, CAST(N'2021-04-05T21:50:12.747' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1009, 3014, CAST(N'1987-06-19' AS Date), N'Male', N'+91', N'4428230131', NULL, N'B-14, 1st Flr, Gemini Parsn Cp', N'Kodambakkam High Road', N'Chennai', N'Tamil Nadu', 600034, N'India', NULL, NULL, CAST(N'2021-04-05T21:52:51.203' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1010, 3015, CAST(N'2008-10-08' AS Date), N'Male', N'+91', N'5823649171', N'Sagar_215431198.jpg', N'49 ,m.narayanardyindl,', N'oldmadrrd,b-, Kr Puram', N'Bangalore', N'Karnataka', 560016, N'India', NULL, NULL, CAST(N'2021-04-05T21:54:31.200' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1011, 3016, NULL, N'Male', N'+91', N'9409146504', NULL, N'102 Manor Impress', N'Lotus School,Jodhpur gam', N'Ahmedabad', N'Gujarat', 380005, N'India', N'Nirma University', N'Nirma University', CAST(N'2021-04-05T21:56:02.047' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MemberProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[Notes] ON 

INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2017, 3011, N'Eloquent JavaScript, Second Edition', N'IT', N'212015389/Displaypicture/open-book_212015399.jpg', N'212015389/UploadedNotes/170170116035_AIassign2_212015401.pdf', N'handwritten books', 50, N'JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based games. Though simple for beginners to pick up and play with, JavaScript is a flexible, complex language that you can use to build full-scale applications.', N'India', N'GTU', N'Javascript', 382411, N'No Starch Press', N'Paid', 50, N'212015389/NotesPreview/EventInfo (1)_212015414.pdf', N'Published', NULL, CAST(N'2021-04-05T22:20:15.337' AS DateTime), CAST(N'2021-04-05T23:00:10.483' AS DateTime), 2, CAST(N'2021-04-05T22:21:22.317' AS DateTime), NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2018, 3011, N'Git Pocket Guide', N'IT', NULL, N'213758022/UploadedNotes/ai paper 170170116019_213758035.pdf', N'university note', 120, N'No matter how much experience you have with JavaScript, odds are you don’t fully understand the language. As part of the \"You Don’t Know JS\" series, this compact guide focuses on new features available in ECMAScript 6 (ES6), the latest version of the standard upon which JavaScript is built.', NULL, NULL, NULL, NULL, NULL, N'Free', 0, N'213758022/NotesPreview/170170116049_AI_Answer_213758038.pdf', N'Draft', NULL, CAST(N'2021-04-05T22:37:57.970' AS DateTime), NULL, NULL, CAST(N'2021-04-05T22:46:01.827' AS DateTime), NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2019, 3011, N'Death Note', N'Story Book', NULL, N'215029167/UploadedNotes/death-note-chapter-1-manga_compress_215029171.pdf', N'manga', 120, N'Death Note is a manga series created by writer Tsugumi Ohba and illustrator Takeshi Obata. The series centers around a high school student who discovers a supernatural notebook that allows him to kill anyone by writing the victim''s name while picturing their face.', N'United States', N'Takeshi Obata', NULL, NULL, NULL, N'Paid', 100, N'215029167/NotesPreview/death-note-box-set-vols-113-volumes-1-13_215029183.pdf', N'Published', NULL, CAST(N'2021-04-05T22:50:29.133' AS DateTime), CAST(N'2021-04-05T23:00:15.537' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2020, 3011, N'Human Resource Management Notes', N'MBA', NULL, N'215316082/UploadedNotes/Travel_215316090.pdf', N'university note', 150, N'This course requires an ability to analyse and understand the concepts and a keen eye for observing how Human Resource Management policies and practises affect organisational working.', NULL, NULL, NULL, NULL, NULL, N'Paid', 50, N'215316082/NotesPreview/170170116049_Practical1_215316093.pdf', N'Rejected', N'This is Not a MBA Book and Upload File is Not Valid', CAST(N'2021-04-05T22:53:16.043' AS DateTime), NULL, NULL, CAST(N'2021-04-05T23:02:07.220' AS DateTime), 1, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2021, 3011, N'Python Assignment PDF', N'CA', N'215752676/Displaypicture/Books-on-a-table-next-to-plant_215752684.jpg', N'215752676/UploadedNotes/170170116049_Practical1_215752685.pdf', N'self-write', 20, N'Python Assignment PDf', N'India', N'VGEC', N'Python ', 35211, N'Jasvant Patel', N'Free', 0, N'215752676/NotesPreview/170170116049_Practical2_215752687.pdf', N'In Review', NULL, CAST(N'2021-04-05T22:57:52.643' AS DateTime), NULL, NULL, CAST(N'2021-04-05T23:01:47.103' AS DateTime), 1, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2022, 3016, N'Designing Evolvable Web APIs with ASP.NET', N'CA', N'214049333/Displaypicture/download_214049338.jpg', N'214049333/UploadedNotes/170170116049_Practical3_214049339.pdf', N'self-write', 100, N'Design and build Web APIs for a broad range of clients—including browsers and mobile devices—that can adapt to change over time. This practical, hands-on guide takes you through the theory and tools you need to build evolvable HTTP services with Microsoft’s ASP.NET Web API framework. In the process, you’ll learn how design and implement a real-world Web API.', NULL, NULL, NULL, NULL, NULL, N'Paid', 170, N'214049333/NotesPreview/certificate_214049342.pdf', N'Published', NULL, CAST(N'2021-04-06T11:40:49.290' AS DateTime), CAST(N'2021-04-06T12:18:20.613' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2023, 3016, N'You Don''t Know JS', N'EC', NULL, N'214234871/UploadedNotes/Post Gate Guidance-COAP-2021_214234876.pdf', N'novel', 250, N'No matter how much experience you have with JavaScript, odds are you don’t fully understand the language. As part of the \"You Don’t Know JS\" series, this compact guide focuses on new features available in ECMAScript 6 (ES6), the latest version of the standard upon which JavaScript is built.', N'India', N'DAIICT', N'Javascript', 38245, N'Kyle Simpson', N'Free', 0, N'214234871/NotesPreview/170170116049_AI_Answer_214234884.pdf', N'Draft', NULL, CAST(N'2021-04-06T11:42:34.840' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2024, 3016, N'The Definitive Guide for JavaScript Developers', N'CA', N'215021687/Displaypicture/photo-1580537659466-0a9bfa916a54_215021695.jpg', N'215021687/UploadedNotes/Adani Gas Payment Receipt2_215021696.pdf', N'university note', 120, N'ECMAScript 6 represents the biggest update to the core of JavaScript in the history of the language. In Understanding ECMAScript 6, expert developer Nicholas C. Zakas provides a complete guide to the object types, syntax, and other exciting changes that ECMAScript 6 brings to JavaScript.', NULL, NULL, NULL, NULL, NULL, N'Free', 0, N'215021687/NotesPreview/EventInfo_215021698.pdf', N'Published', NULL, CAST(N'2021-04-06T11:50:21.657' AS DateTime), CAST(N'2021-04-06T12:18:17.663' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2025, 3016, N'Programming JavaScript Applications', N'IT', N'210356869/Displaypicture/Screenshot (9)_210356877.png', N'210356869/UploadedNotes/zack-snyders-justice-league-2021-english-yify-316606_210356878.zip', N'handwritten books', 120, N'Take advantage of JavaScript''s power to build robust web-scale or enterprise applications that are easy to extend and maintain. By applying the design patterns outlined in this practical book, experienced JavaScript developers will learn how to write flexible and resilient code that''s easier-yes, easier-to work with as your code base grows.', NULL, NULL, NULL, NULL, NULL, N'Paid', 150, N'210356869/NotesPreview/death-note-box-set-vols-113-volumes-1-13_210356880.pdf', N'Submitted for Review', NULL, CAST(N'2021-04-06T12:03:56.823' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2026, 3016, N'My Hero Academia', N'Story Book', N'211552180/Displaypicture/MY-HERO_211552192.jpg', N'211552180/UploadedNotes/170170116049_Assignment1_211552198.pdf', N'manga', 120, N'My Hero Academia is a Japanese superhero manga series written and illustrated by Kohei Horikoshi. The story follows Izuku Midoriya, a boy born without superpowers in a world where they have become commonplace, but who still dreams of becoming a superhero himself.', NULL, NULL, NULL, NULL, NULL, N'Paid', 70, N'211552180/NotesPreview/170170116049_Prac123_211552200.pdf', N'Published', NULL, CAST(N'2021-04-06T12:15:52.127' AS DateTime), CAST(N'2021-04-06T12:17:48.370' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2027, 3009, N'Data Science For Dummies', N'IT', N'211002776/Displaypicture/download (2)_211002790.jpg', N'211002776/UploadedNotes/TSC Training Plan for Batch 2020-2021_211002791.pdf', N'university note', 120, N'Jobs in data science abound, but few people have the data science skills needed to fill these increasingly important roles in organizations. Data Science For Dummies is the perfect starting point for IT professionals and students interested in making sense of their organization''s massive data sets and applying their findings to real-world business scenarios.

', N'United States', N'Westland', N' Data Science', 182414, N' Dummies', N'Paid', 120, N'211002776/NotesPreview/ai paper 170170116019_211002792.pdf', N'Published', NULL, CAST(N'2021-04-06T13:10:02.733' AS DateTime), CAST(N'2021-04-06T13:23:57.123' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2028, 3009, N'Hands-On Machine Learning with Scikit-Learn', N'EC', NULL, N'211722995/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211723004.pdf', N'self-write', 120, N'By using concrete examples, minimal theory, and two production-ready Python frameworks—Scikit-Learn and TensorFlow—author Aurélien Géron helps you gain an intuitive understanding of the concepts and tools for building intelligent systems. You’ll learn a range of techniques, starting with simple linear regression and progressing to deep neural networks. With exercises in each chapter to help you apply what you’ve learned, all you need is programming experience to get started.', NULL, NULL, N'Python', 12456, N'O''Reilly Media', N'Free', 0, N'211722995/NotesPreview/TSC Training Plan for Batch 2020-2021_211723005.pdf', N'Published', NULL, CAST(N'2021-04-06T13:17:22.963' AS DateTime), CAST(N'2021-04-06T13:23:53.160' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2029, 3009, N'Clean Code: A Handbook of Agile Software', N'IT', N'212227041/Displaypicture/download (3)_212227052.jpg', N'212227041/UploadedNotes/TSC Training Plan for Batch 2020-2021_212227057.pdf', N'handwritten books', 210, N'Noted software expert Robert C. Martin presents a revolutionary paradigm with Clean Code: A Handbook of Agile Software Craftsmanship. Martin has teamed up with his colleagues from Object Mentor to distill their best agile practice of cleaning code "on the fly" into a book that will instill within you the values of a software craftsman and make you a better programmer—but only if you work at it.', N'India', N'GTU', N'Agile Software Craftsmanship', 865472, N'Robert C. Martin', N'Paid', 250, N'212227041/NotesPreview/TSC Training Plan for Batch 2020-2021 (1)_212227059.pdf', N'Published', NULL, CAST(N'2021-04-06T13:22:26.983' AS DateTime), CAST(N'2021-04-06T13:23:48.847' AS DateTime), 3, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2030, 3013, N'Spring in Action', N'CA', N'210746175/Displaypicture/Walls-Spring-5ed-HI_210746186.png', N'210746175/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_210746189.pdf', N'university note', 150, N'Spring Framework makes life easier for Java developers. New features in Spring 5 bring its productivity-focused approach to microservices, reactive development, and other modern application designs. With Spring Boot now fully integrated, you can start even complex projects with minimal configuration code. And the upgraded WebFlux framework supports reactive apps right out of the box!', NULL, NULL, NULL, NULL, NULL, N'Paid', 40, N'210746175/NotesPreview/certificate_210746190.pdf', N'Submitted for Review', NULL, CAST(N'2021-04-06T14:07:46.123' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2031, 3013, N'The Gray Man', N'Story Book', N'211154625/Displaypicture/617vuErXqL_211154636.jpg', N'211154625/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211154637.pdf', N'novel', 464, N'THE FIRST GRAY MAN NOVEL FROM #1 NEW YORK TIMES BESTSELLING AUTHOR MARK GREANEY—Soon to Be a Netflix Original Film Starring Ryan Gosling and Chris Evans', N'United States', N'Berkley', NULL, NULL, NULL, N'Paid', 200, N'211154625/NotesPreview/death-note-box-set-vols-113-volumes-1-13_211154638.pdf', N'Published', NULL, CAST(N'2021-04-06T14:11:54.580' AS DateTime), CAST(N'2021-04-06T14:26:29.480' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2032, 3013, N'Sinbad and the Trumpet of Israfil', N'Action', N'212118395/Displaypicture/81F5vVLGfpL_212118406.jpg', N'212118395/UploadedNotes/170170116049_AI_Answer_212118410.pdf', N'novel', 150, N'Kevin Missal has given the age-old Sinbad tale a delightful spin. Beware, this fast-paced, adrenaline-pumping adventure is going to rob you of your sleep  Anand Neelakantan, author of the bestselling Baahubali series and Asura: Tale of the Vanquished
', NULL, NULL, NULL, NULL, NULL, N'Paid', 50, N'212118395/NotesPreview/ai paper 170170116019_212118412.pdf', N'Published', N'This Note is Not Perfect Their Are Some Problem', CAST(N'2021-04-06T14:21:18.343' AS DateTime), CAST(N'2021-04-06T14:25:28.230' AS DateTime), 1, CAST(N'2021-04-06T16:05:52.117' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Notes] OFF
GO
SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1013, 2019, 3011, 3010, 5, N'Best Manga Ever in Best Price', CAST(N'2021-04-06T16:09:10.873' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1014, 2029, 3009, 3010, 4, N'This is Nice Book', CAST(N'2021-04-06T16:09:48.103' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1015, 2019, 3011, 3015, 4, N'This is Best Manga I have Read', CAST(N'2021-04-06T16:11:07.680' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1016, 2028, 3009, 3015, 3, N'This is Not Great Book But this is free so give three stars', CAST(N'2021-04-06T16:11:48.463' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1017, 2031, 3013, 3015, 4, N'Nice Book ', CAST(N'2021-04-06T16:12:35.877' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1018, 2026, 3016, 3014, 4, N'This is Nice Manga', CAST(N'2021-04-06T16:14:02.003' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1019, 2022, 3016, 3014, 1, N'This is the worst book I have seen .there are not understanding any topic ', CAST(N'2021-04-06T16:15:47.250' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Review] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'handwritten books', N'Loram', CAST(N'2021-02-15T12:49:57.757' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'university note', N'To sort documents in MongoDB, you need to use sort() method. The method accepts a document containing a list of fields along with their sorting order. To specify sorting order 1 and -1 are used. 1 is used for ascending order while -1
', CAST(N'2021-02-15T12:50:06.987' AS DateTime), 1, CAST(N'2021-03-15T22:34:59.090' AS DateTime), 2, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'self-write', N'Loram', CAST(N'2021-02-15T12:50:25.100' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'novel', N'Loram Loram', CAST(N'2021-02-15T12:50:42.703' AS DateTime), 1, CAST(N'2021-03-15T22:35:35.887' AS DateTime), 2, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'manga', N'Loram', CAST(N'2021-02-15T12:50:47.470' AS DateTime), 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admin]    Script Date: 06-04-2021 16:59:41 ******/
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [IX_Admin] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Member]    Script Date: 06-04-2021 16:59:41 ******/
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [IX_Member] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [DF_Admin_Type]  DEFAULT ('Admin') FOR [Type]
GO
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [DF_Admin_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [DF_Admin_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [DF_Buyer_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [DF_Buyer_IsPaid]  DEFAULT ((0)) FOR [IsPaid]
GO
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [DF_Buyer_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [DF_Buyer_RequestDate]  DEFAULT (getdate()) FOR [RequestDate]
GO
ALTER TABLE [dbo].[Buyer] ADD  CONSTRAINT [DF_Buyer_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Contactus] ADD  CONSTRAINT [DF_Contactus_SubmitDate]  DEFAULT (getdate()) FOR [SubmitDate]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Inappropriate] ADD  CONSTRAINT [DF_Inappropriate_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Inappropriate] ADD  CONSTRAINT [DF_Inappropriate_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_EmailVerified]  DEFAULT ((0)) FOR [EmailVerified]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MemberProfile] ADD  CONSTRAINT [DF_MemberProfile_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Notes] ADD  CONSTRAINT [DF_Notes_Status]  DEFAULT ('Draft') FOR [Status]
GO
ALTER TABLE [dbo].[Notes] ADD  CONSTRAINT [DF_Notes_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Notes] ADD  CONSTRAINT [DF_Notes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Review] ADD  CONSTRAINT [DF_Review_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [DF_Type_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [DF_Type_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_Member] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_Member]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_Member1] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_Member1]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_Notes]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Admin1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Admin] ([AdminId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Admin1]
GO
ALTER TABLE [dbo].[Country]  WITH CHECK ADD  CONSTRAINT [FK_Country_Admin1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Admin] ([AdminId])
GO
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_Country_Admin1]
GO
ALTER TABLE [dbo].[Inappropriate]  WITH CHECK ADD  CONSTRAINT [FK_Inappropriate_Member] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Inappropriate] CHECK CONSTRAINT [FK_Inappropriate_Member]
GO
ALTER TABLE [dbo].[Inappropriate]  WITH CHECK ADD  CONSTRAINT [FK_Inappropriate_Member1] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Inappropriate] CHECK CONSTRAINT [FK_Inappropriate_Member1]
GO
ALTER TABLE [dbo].[Inappropriate]  WITH CHECK ADD  CONSTRAINT [FK_Inappropriate_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Inappropriate] CHECK CONSTRAINT [FK_Inappropriate_Notes]
GO
ALTER TABLE [dbo].[MemberProfile]  WITH CHECK ADD  CONSTRAINT [FK_MemberProfile_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[MemberProfile] CHECK CONSTRAINT [FK_MemberProfile_Member]
GO
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Member] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Member]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Member] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Member]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Member1] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Member1]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Notes] FOREIGN KEY([NoteId])
REFERENCES [dbo].[Notes] ([NoteId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Notes]
GO
ALTER TABLE [dbo].[Type]  WITH CHECK ADD  CONSTRAINT [FK_Type_Admin1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Admin] ([AdminId])
GO
ALTER TABLE [dbo].[Type] CHECK CONSTRAINT [FK_Type_Admin1]
GO
