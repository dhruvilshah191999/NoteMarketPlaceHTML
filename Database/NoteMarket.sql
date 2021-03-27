USE [Notes-Marketplace]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Buyer]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Contactus]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Country]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Inappropriate]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[ManageSystem]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Member]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[MemberProfile]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Notes]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Review]    Script Date: 27-03-2021 12:17:39 ******/
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
/****** Object:  Table [dbo].[Type]    Script Date: 27-03-2021 12:17:39 ******/
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

INSERT [dbo].[Admin] ([AdminId], [FirstName], [LastName], [SecondaryEmail], [Email], [Password], [Type], [CountryCode], [PhoneNumber], [ProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Dhruvil', N'Shah', NULL, N'dhruvilshah191999@gmail.com', N'123456', N'Admin', NULL, NULL, NULL, CAST(N'2021-02-15T11:19:03.890' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Admin] ([AdminId], [FirstName], [LastName], [SecondaryEmail], [Email], [Password], [Type], [CountryCode], [PhoneNumber], [ProfilePicture], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Harshil', N'Patel', NULL, N'harshil@gmail.com', N'Admin@123', N'Admin', 1, N'6354883566', N'Harshil_210644881.png', CAST(N'2021-03-15T16:25:48.910' AS DateTime), NULL, CAST(N'2021-03-25T15:09:20.423' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Buyer] ON 

INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (2, 5, 3, 1, 1, N'211919489/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211919501.pdf', 1, 0, N'A Handbook for Computer Science /IT Engineering', N'IT', CAST(N'2021-03-02T21:09:16.220' AS DateTime), CAST(N'2021-03-02T21:09:16.220' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (3, 6, 3, 1, 1, N'212202697/UploadedNotes/DMBI IMP_212202719.pdf', 1, 100, N'Handbook of Computer Science / IT', N'IT', CAST(N'2021-03-02T21:43:40.100' AS DateTime), CAST(N'2021-03-03T15:23:35.423' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (4, 9, 3, 1, 0, N'213224122/UploadedNotes/BE-SEM7WinterExamination-2020_170170116049_213224128.pdf', 0, 100, N'Database Management System For MU B.E.IT Engineering Sem 3 ', N'CA', CAST(N'2021-03-02T21:49:23.800' AS DateTime), CAST(N'2021-03-02T21:49:23.800' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (5, 6, 3, 1008, 1, N'212202697/UploadedNotes/DMBI IMP_212202719.pdf', 1, 100, N'Handbook of Computer Science / IT', N'IT', CAST(N'2021-03-03T13:14:23.440' AS DateTime), CAST(N'2021-03-04T15:57:04.883' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (6, 7, 3, 1, 1, N'212547309/UploadedNotes/DMBI_IMP_Questions_212547318.pdf', 1, 10, N'101 Fairy Tales Book', N'Story Book', CAST(N'2021-03-03T20:37:13.403' AS DateTime), CAST(N'2021-03-04T15:41:51.987' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (7, 18, 3, 1, 1, N'215406701/UploadedNotes/BillHTML_215406710.pdf', 1, 0, N'Free Text Book', N'Action', CAST(N'2021-03-05T11:55:55.203' AS DateTime), CAST(N'2021-03-05T11:55:55.203' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (8, 18, 3, 1008, 1, N'215406701/UploadedNotes/BillHTML_215406710.pdf', 1, 0, N'Free Text Book', N'Action', CAST(N'2021-03-05T12:01:02.997' AS DateTime), CAST(N'2021-03-11T11:35:38.583' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (9, 17, 1, 3, 1, N'215933007/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_215933028.pdf', 1, 110, N'Attack on titan', N'IT', CAST(N'2021-03-06T11:28:28.630' AS DateTime), CAST(N'2021-03-06T11:29:42.820' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1002, 14, 3, 1, 0, N'212145822/UploadedNotes/TSC Training Plan for Batch 2020-2021_212145832.pdf', 0, 20, N'Action: The Teachings of J. Krishnamurti', N'Action', CAST(N'2021-03-11T14:26:19.313' AS DateTime), CAST(N'2021-03-11T14:26:19.313' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1003, 18, 3, 4, 1, N'215406701/UploadedNotes/BillHTML_215406710.pdf', 1, 0, N'Free Text Book', N'Action', CAST(N'2021-03-11T15:43:04.810' AS DateTime), CAST(N'2021-03-11T15:43:04.810' AS DateTime), 1)
INSERT [dbo].[Buyer] ([Id], [NoteId], [OwnerId], [MemberId], [Status], [AttachmentPath], [IsPaid], [Price], [NoteTitle], [NoteCategory], [RequestDate], [ApprovedDate], [IsActive]) VALUES (1004, 11, 3, 1, 1, N'211636266/UploadedNotes/170170116049_Practical1_211636292.pdf', 1, 100, N'GATE 2021: Computer Science and IT Engineering ', N'Action', CAST(N'2021-03-20T21:41:52.877' AS DateTime), CAST(N'2021-03-20T21:43:05.633' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Buyer] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'IT', N'Lorem', CAST(N'2021-02-15T11:26:33.313' AS DateTime), 1, CAST(N'2021-03-16T21:22:13.853' AS DateTime), 2, 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'CA', N'Lorem', CAST(N'2021-02-15T11:26:41.203' AS DateTime), 1, NULL, NULL, 1)
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
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'New Zealand', 64, CAST(N'2021-02-11T21:53:44.437' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'United States', 1, CAST(N'2021-02-11T21:54:18.110' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Afghanistan', 93, CAST(N'2021-02-14T15:51:36.030' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Bangladesh', 880, CAST(N'2021-02-14T15:51:54.877' AS DateTime), 1, NULL, 1, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Brazil', 55, CAST(N'2021-02-14T15:52:09.053' AS DateTime), 1, CAST(N'2021-03-15T22:49:44.467' AS DateTime), 2, 1)
INSERT [dbo].[Country] ([Id], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, N'Dhruvil', 123, CAST(N'2021-03-15T17:25:32.927' AS DateTime), 1, CAST(N'2021-03-15T18:41:08.713' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Inappropriate] ON 

INSERT [dbo].[Inappropriate] ([Id], [NoteId], [OwnerId], [MemberId], [Remarks], [Date], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (1, 7, 3, 1, N'This is not the Fairy Tales Book', CAST(N'2021-03-11T18:01:51.060' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Inappropriate] ([Id], [NoteId], [OwnerId], [MemberId], [Remarks], [Date], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (2, 18, 3, 1, N'This is really bad book', CAST(N'2021-03-12T17:54:02.607' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Inappropriate] ([Id], [NoteId], [OwnerId], [MemberId], [Remarks], [Date], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3, 17, 1, 3, N'not perfect', CAST(N'2021-03-15T14:41:00.747' AS DateTime), CAST(N'2021-03-17T13:50:04.857' AS DateTime), 2, 0)
SET IDENTITY_INSERT [dbo].[Inappropriate] OFF
GO
SET IDENTITY_INSERT [dbo].[ManageSystem] ON 

INSERT [dbo].[ManageSystem] ([Id], [SupportEmail], [SupportContactNumber], [EmailAddress], [NoteDisplayPicture], [MemberDisplayPicture], [FacebookLink], [TwitterLink], [LinkedlnLink], [ModifiedDate], [ModifiedBy]) VALUES (1, N'digi5technologies@gmail.com', N'6354883566', N'darkgoku191999@gmail.com', N'download.png', N'Default.jpg', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ManageSystem] OFF
GO
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (1, N'Dhruvil', N'Patel', N'dhruvilshah191999@gmail.com', 1, N'8n4B6Xm1', CAST(N'2021-02-11T13:33:46.960' AS DateTime), NULL, CAST(N'2021-03-20T21:40:55.167' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (3, N'Dhruvil', N'Shah', N'darkgoku191999@gmail.com', 1, N'1234', CAST(N'2021-02-11T13:46:21.833' AS DateTime), NULL, CAST(N'2021-03-10T18:39:46.030' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (4, N'Sagar', N'Shah', N'sagarsolanki749@gmail.com', 1, N'c6431qGM', CAST(N'2021-02-13T17:20:51.900' AS DateTime), NULL, CAST(N'2021-02-17T20:30:33.557' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (1008, N'Dummy', N'Shah', N'digi5technologies@gmail.com', 1, N'123', CAST(N'2021-03-01T20:07:36.643' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Member] ([MemberId], [FirstName], [LastName], [Email], [EmailVerified], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [DeactivateDate], [DeactivateBy], [IsActive]) VALUES (2009, N'Note', N'MarketPlace', N'dhruvinsinhjadav658@gmail.com', 0, N'123456', CAST(N'2021-03-10T15:52:59.503' AS DateTime), NULL, NULL, NULL, CAST(N'2021-03-18T19:11:59.480' AS DateTime), 2, 0)
SET IDENTITY_INSERT [dbo].[Member] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberProfile] ON 

INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 1, CAST(N'2021-03-19' AS Date), N'Male', N'+91', N'6354883566', N'Dhruvil_215211621.jpg', N'Sandhi Mata', N'Tapal Chowk', N'Ahmedabad', N'Gujarat', 380005, N'India', NULL, NULL, CAST(N'2021-02-11T21:33:29.767' AS DateTime), NULL, CAST(N'2021-03-02T14:53:55.567' AS DateTime), NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 3, CAST(N'2021-02-13' AS Date), N'Male', N'+1', N'6354883566', N'Dhruvil_212600374.jpg', N'Sendhi Mata', N'Tapal Chowk,Sanand', N'Ahmedabad', N'Gujarat', 382005, N'United States', N'GTU', N'VGEC', CAST(N'2021-02-13T15:49:30.843' AS DateTime), NULL, CAST(N'2021-03-25T15:26:00.373' AS DateTime), NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, 4, CAST(N'2021-02-06' AS Date), N'Male', N'+91', N'6354883566', N'Sagar_213033555.png', N'Sendhi Mata', N'Tapal Chowk,Sanand', N'Ahmedabad', N'Gujarat', 382110, N'India', NULL, NULL, CAST(N'2021-02-17T20:30:33.570' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[MemberProfile] ([Id], [MemberId], [BirthDate], [Gender], [CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [Zipcode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 1008, CAST(N'2021-02-04' AS Date), N'Female', N'+91', N'9409146504', N'Dummy_211239748.jpg', N'102 Manor Impress', N'Tapal Chowk,Sanand', N'Ahmedabad', N'Gujarat', 380015, N'India', NULL, NULL, CAST(N'2021-03-03T13:12:39.757' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MemberProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[Notes] ON 

INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 3, N'A Handbook for Computer Science /IT Engineering', N'IT', NULL, N'211919489/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_211919501.pdf', N'university note', 210, N'Me editorial Board a handbook for computer Science/It engineering made easy publications English A handbook for computer Science/It engineering.', N'India', N'VGEC', N'Information Technology', 210254, N'Madeeasy Publications', N'Free', 0, N'211919489/NotesPreview/Light-Bill-Sanad_211919543.pdf', N'Published', NULL, CAST(N'2021-02-17T18:19:19.443' AS DateTime), CAST(N'2021-03-17T21:53:30.987' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 3, N'Handbook of Computer Science / IT', N'IT', NULL, N'212202697/UploadedNotes/DMBI IMP_212202719.pdf', N'university note', 500, N'This handbook has been designed for the aspirants of IES, GATE, PSUs and other competitive examinations. This specialized book for Mechanical Engineering has been divided into 13 units each containing detailed theoretical content. Key terms in each unit have been given with their definitions. Every topic is taken up separately along with Key Points and notes. All the formulae used have been well illustrated and diagrams have been given for theoretical analysis. This book covers almost 100% syllabus of Mechanical Engineering making it the only book for multipurpose quick revision and ensuring success in IES, GATE, PSUs and other competitive examinations. Appendix has been given at the end of the book', NULL, NULL, NULL, NULL, NULL, N'Paid', 100, N'212202697/NotesPreview/J_IMP_Twisted_General Question bank_212202735.pdf', N'Published', NULL, CAST(N'2021-02-17T18:22:02.693' AS DateTime), CAST(N'2021-03-17T21:53:30.987' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 3, N'101 Fairy Tales Book', N'Story Book', NULL, N'212547309/UploadedNotes/DMBI_IMP_Questions_212547318.pdf', N'novel', 200, N'101 Fairy Tales- a collection of best fairy tales that include all your cherished favourites- Little Red Riding Hood, Sleeping Beauty, Cinderella, Hansel and Gretal, Magic Tinder Box and many more. Each Fairy tale is a simple retelling that will delight young readers and the charming colourful illustrations will captivate their minds.', NULL, NULL, NULL, NULL, NULL, N'Paid', 10, N'212547309/NotesPreview/DMBI IMP_212547351.pdf', N'Published', NULL, CAST(N'2021-02-17T18:25:47.310' AS DateTime), CAST(N'2021-03-17T21:53:30.987' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 3, N'Database Management System For MU B.E.IT Engineering Sem 3 ', N'CA', NULL, N'213224122/UploadedNotes/BE-SEM7WinterExamination-2020_170170116049_213224128.pdf', N'self-write', 220, N'Course', NULL, N'Gujarat Techonology University ', N'Information Technology', 648383, NULL, N'Paid', 100, N'213224122/NotesPreview/2170709-INS-Frequently Asked Questions_213224142.pdf', N'Published', NULL, CAST(N'2021-02-17T18:32:24.120' AS DateTime), CAST(N'2021-03-17T21:53:30.987' AS DateTime), 2, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 3, N'GATE 2021: Computer Science and IT Engineering ', N'Action', N'211636266/Displaypicture/scanner_20170924_202046_211636276.jpg', N'211636266/UploadedNotes/170170116049_Practical1_211636292.pdf', N'manga', 500, N'The new edition of GATE 2021 Solved Papers: computer Science & It has been fully revised, updated and edited. The whole book has been divided into topic wise sections. At the beginning of each subject, analysis of previous papers are given to improve the understanding of the subject.', N'India', N'Gujarat Techonology University ', N'Information Technology', 324564, N'Madeeasy Publications', N'Paid', 100, N'211636266/NotesPreview/ArrangeFilledChoicesN_211636311.pdf', N'In Review', NULL, CAST(N'2021-02-17T19:16:36.263' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 3, N'Missing in Action: The Prisoners Who Never Came Back', N'Action', NULL, N'211819645/UploadedNotes/20BE7_170170116019_2_211819653.pdf', N'self-write', 524, N'The story of India''s soldiers missing in action is one that remains unfinished, a spillover of the wars with Pakistan. These are men who went missing in enemy territory while on daring missions during the 1965 and 1971 Indo-Pak wars. The nation has forgotten them, though successive governments continue to make token acknowledgements about their missing status.', NULL, NULL, NULL, NULL, NULL, N'Paid', 50, N'211819645/NotesPreview/ArrangeFilledChoicesN_211819676.pdf', N'In Review', NULL, CAST(N'2021-02-17T19:18:19.643' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 3, N'12 Years A Slave: A True Story', N'Action', NULL, N'211956860/UploadedNotes/MIMP_QUE_INS_211956866.pdf', N'handwritten books', 50, N'And What Difference is There in the Colour of the Soul?
A hardworking farmer and an exceptional fiddler, Solomon Northup is born a free man of colour. He lives happily with his wife and children in Saratoga County, New York, until the day an employment offer at a circus changes his life. Tricked, drugged and kidnapped, Northup gets sold as a slave. And for the twelve years that follow, he lives in bondage under different masters . . .', NULL, NULL, NULL, NULL, NULL, N'Paid', 150, N'211956860/NotesPreview/Unit - 4 Spark_211956887.pdf', N'Rejected', N'Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem', CAST(N'2021-02-17T19:19:56.860' AS DateTime), NULL, NULL, CAST(N'2021-03-18T19:35:58.180' AS DateTime), 2, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 3, N'Action: The Teachings of J. Krishnamurti', N'Action', NULL, N'212145822/UploadedNotes/TSC Training Plan for Batch 2020-2021_212145832.pdf', N'handwritten books', 150, N'Krishnamurti offers radically different answers to questions about our relationship with others and why we do not act with clarity and intelligence.', N'Brazil', N'Universidade de São Paulo', NULL, NULL, NULL, N'Paid', 20, N'212145822/NotesPreview/DMBI IMP_212145859.pdf', N'Draft', NULL, CAST(N'2021-02-17T19:21:45.820' AS DateTime), NULL, NULL, CAST(N'2021-03-15T21:41:17.797' AS DateTime), NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 3, N'Eating in the Age of Dieting', N'IT', N'211341271/Displaypicture/golu_BDA_213211178.jpg', N'211341271/UploadedNotes/DMBI Chapter 8_211341279.pdf', N'handwritten books', 150, N'lorem', N'New Zealand', N'Westland', NULL, NULL, NULL, N'Free', 0, N'211341271/NotesPreview/TSC Training Plan for Batch 2020-2021_210843052.pdf', N'Draft', NULL, CAST(N'2021-02-17T21:13:41.227' AS DateTime), NULL, NULL, CAST(N'2021-03-06T11:32:11.197' AS DateTime), NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 4, N'sdfgdsaSDF', N'Story Book', N'215433571/Displaypicture/download_215433578.jpg', N'215433571/UploadedNotes/Light-Bill-Sanad_215433580.pdf', N'university note', 120, N'SAERDTYUTRDFSDADFGHJHGFDSADFGHJKJHGFDSADFGHJKJ', NULL, NULL, NULL, NULL, NULL, N'Free', 0, N'215433571/NotesPreview/TSC Training Plan for Batch 2020-2021_215433598.pdf', N'Rejected', NULL, CAST(N'2021-02-28T18:54:33.523' AS DateTime), NULL, NULL, CAST(N'2021-03-18T19:35:58.180' AS DateTime), 2, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 1, N'Attack on titan', N'IT', N'215933007/Displaypicture/download_215933017.jpg', N'215933007/UploadedNotes/TSC Training Plan for Batch 2020-2021 (1)_215933028.pdf', N'novel', 150, N'Hellos jjdksj sckjdskjds sdkdjkdsd', N'India', N'Westland', N'Computer Engineering', NULL, NULL, N'Paid', 110, N'215933007/NotesPreview/TSC Training Plan for Batch 2020-2021_215933054.pdf', N'Draft', NULL, CAST(N'2021-03-02T20:59:32.943' AS DateTime), NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Notes] ([NoteId], [OwnerId], [NoteTitle], [Category], [DisplayPicture], [UploadNotes], [Type], [TotalPages], [Description], [Country], [InstitutionName], [CourseName], [CourseCode], [Professor], [SellType], [SellPrice], [NotePreview], [Status], [AdminRemarks], [CreatedDate], [ApprovedDate], [ApprovedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 3, N'Free Text Book', N'Action', NULL, N'215406701/UploadedNotes/BillHTML_215406710.pdf', N'handwritten books', 100, N' vbn hgj hgjk gbhn', NULL, NULL, NULL, NULL, NULL, N'Free', 0, N'215406701/NotesPreview/100pt_Certificate_170170116049_215406742.pdf', N'Draft', NULL, CAST(N'2021-03-05T11:54:06.613' AS DateTime), NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Notes] OFF
GO
SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 6, 3, 1, 4, N'Lorem Lorem Lorem', CAST(N'2021-02-18T13:36:46.280' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 6, 3, 4, 5, N'Lorem Lorem Lorem', CAST(N'2021-02-18T13:39:12.457' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 6, 3, 3, 3, N'Lorem Lorem Lorem', CAST(N'2021-02-18T13:42:45.583' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 6, 3, 4, 4, N'Lorem Lorem Lorem', CAST(N'2021-02-18T13:42:54.613' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 18, 3, 1, 4, N'this is nice book', CAST(N'2021-03-11T15:41:10.543' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 18, 3, 1, 2, N'this is bad book', CAST(N'2021-03-11T15:41:32.340' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 18, 3, 4, 5, N'Best book i even see', CAST(N'2021-03-11T15:43:49.063' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 7, 3, 1, 2, N'this is not a best book', CAST(N'2021-03-11T15:56:18.960' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 5, 3, 1, 1, N'really bad book', CAST(N'2021-03-11T15:57:00.780' AS DateTime), CAST(N'2021-03-18T18:54:27.287' AS DateTime), 2, 0)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 18, 3, 1, 1, N'bad book', CAST(N'2021-03-12T17:55:05.420' AS DateTime), CAST(N'2021-03-18T18:48:19.823' AS DateTime), 2, 0)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 7, 3, 1, 5, N'best', CAST(N'2021-03-15T14:38:46.873' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Review] ([Id], [NoteId], [OwnerId], [MemberId], [Rate], [Description], [CreatedDate], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 17, 1, 3, 4, N'best book', CAST(N'2021-03-15T14:40:39.907' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Review] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'handwritten books', N'Loram', CAST(N'2021-02-15T12:49:57.757' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'university note', N'To sort documents in MongoDB, you need to use sort() method. The method accepts a document containing a list of fields along with their sorting order. To specify sorting order 1 and -1 are used. 1 is used for ascending order while -1
', CAST(N'2021-02-15T12:50:06.987' AS DateTime), 1, CAST(N'2021-03-15T22:34:59.090' AS DateTime), 2, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'self-write', N'Loram', CAST(N'2021-02-15T12:50:25.100' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'novel', N'Loram Loram', CAST(N'2021-02-15T12:50:42.703' AS DateTime), 1, CAST(N'2021-03-15T22:35:35.887' AS DateTime), 2, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'manga', N'Loram', CAST(N'2021-02-15T12:50:47.470' AS DateTime), 1, NULL, NULL, 1)
INSERT [dbo].[Type] ([Id], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Romance', N'Romace Books', CAST(N'2021-03-15T22:19:46.247' AS DateTime), 2, CAST(N'2021-03-16T19:49:30.200' AS DateTime), 2, 0)
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admin]    Script Date: 27-03-2021 12:17:40 ******/
ALTER TABLE [dbo].[Admin] ADD  CONSTRAINT [IX_Admin] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Member]    Script Date: 27-03-2021 12:17:40 ******/
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
