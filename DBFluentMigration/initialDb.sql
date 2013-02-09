/****** Object:  StoredProcedure [dbo].[TasksByField]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

GO
/****** Object:  Table [dbo].[BaseProduct]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseProduct](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[InstantiatingType] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_BaseProduct] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BaseProductToFieldVendor]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseProductToFieldVendor](
	[Vendor_id] [int] NOT NULL,
	[BaseProduct_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Calculator]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calculator](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_Calculator] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Chemical]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chemical](
	[BaseProduct_id] [int] NOT NULL,
	[ActiveIngredient] [nvarchar](255) NULL,
	[ActiveIngredientPercent] [decimal](19, 5) NULL,
	[EPAEstNumber] [nvarchar](255) NULL,
	[EPARegNumber] [nvarchar](255) NULL,
 CONSTRAINT [PK_Chemical] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[TaxRate] [float] NULL,
	[NumberOfSites] [int] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[DocumentCategory_id] [int] NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentCategory]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentCategory](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentToEquipment]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentToEquipment](
	[Equipment_id] [int] NOT NULL,
	[Document_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentToField]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentToField](
	[Field_id] [int] NOT NULL,
	[Document_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailJob]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailJob](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[Sender] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Frequency] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[EmailTemplate_id] [int] NULL,
 CONSTRAINT [PK_EmailJob] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Template] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailTemplatesToSubscribers]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplatesToSubscribers](
	[EmailJob_id] [int] NOT NULL,
	[User_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeToEquipmentTask]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeToEquipmentTask](
	[EquipmentTask_id] [int] NOT NULL,
	[User_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeToTask]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeToTask](
	[Task_id] [int] NOT NULL,
	[User_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[TotalHours] [float] NULL,
	[Threshold] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[Make] [nvarchar](255) NULL,
	[Model] [nvarchar](255) NULL,
	[SerialNumber] [nvarchar](255) NULL,
	[WarrentyInfo] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Vendor_id] [int] NULL,
	[EquipmentType_id] [int] NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentTask]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentTask](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[ScheduledDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ActualTimeSpent] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[Deleted] [bit] NULL,
	[Complete] [bit] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[TaskType_id] [int] NULL,
	[Equipment_id] [int] NULL,
 CONSTRAINT [PK_EquipmentTask] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentTaskType]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentTaskType](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_EquipmentTaskType] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentToTask]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentToTask](
	[Task_id] [int] NOT NULL,
	[Equipment_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentType]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentType](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_EquipmentType] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentTypeToEquipmentVendor]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentTypeToEquipmentVendor](
	[Vendor_id] [int] NOT NULL,
	[EquipmentType_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[ScheduledDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Notes] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[EventType_id] [int] NULL,
	[Field_id] [int] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventType]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[EventColor] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fertilizer]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fertilizer](
	[BaseProduct_id] [int] NOT NULL,
	[N] [float] NULL,
	[P] [float] NULL,
	[K] [float] NULL,
 CONSTRAINT [PK_Fertilizer] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Field]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Abbreviation] [nvarchar](255) NULL,
	[Size] [int] NULL,
	[Status] [nvarchar](255) NULL,
	[FieldColor] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Site_id] [int] NULL,
 CONSTRAINT [PK_Field] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryProduct]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryProduct](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Discriminator] [nvarchar](255) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Quantity] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[DatePurchased] [datetime] NULL,
	[LastUsed] [datetime] NULL,
	[Cost] [float] NULL,
	[UnitType] [nvarchar](255) NULL,
	[SizeOfUnit] [int] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[LastVendor_id] [int] NULL,
	[Product_id] [int] NULL,
 CONSTRAINT [PK_InventoryProduct] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedEnumeration]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedEnumeration](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[ValueType] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[Tooltip] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_LocalizedEnumeration] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedProperty]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedProperty](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[ParentType] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[Tooltip] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_LocalizedProperty] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedText]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedText](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_LocalizedText] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Material]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[BaseProduct_id] [int] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Part]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Part](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[Vendor] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_Part] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PartToEquipmentTask]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartToEquipmentTask](
	[EquipmentTask_id] [int] NOT NULL,
	[Part_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Photo]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[PhotoCategory_id] [int] NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoCategory]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoCategory](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_PhotoCategory] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoToEquipment]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoToEquipment](
	[Equipment_id] [int] NOT NULL,
	[Photo_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoToField]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoToField](
	[Field_id] [int] NOT NULL,
	[Photo_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[SubTotal] [float] NULL,
	[Completed] [bit] NULL,
	[Fees] [float] NULL,
	[Tax] [float] NULL,
	[Total] [float] NULL,
	[DateReceived] [datetime] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Vendor_id] [int] NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseOrderLineItem]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderLineItem](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Price] [float] NULL,
	[QuantityOrdered] [int] NULL,
	[TotalReceived] [int] NULL,
	[SubTotal] [float] NULL,
	[Tax] [float] NULL,
	[Completed] [bit] NULL,
	[DateRecieved] [datetime] NULL,
	[UnitType] [nvarchar](255) NULL,
	[Taxable] [bit] NULL,
	[SizeOfUnit] [int] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Product_id] [int] NULL,
	[PurchaseOrder_id] [int] NULL,
 CONSTRAINT [PK_PurchaseOrderLineItem] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Operations]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Operations](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_security_Operations] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Permissions]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Permissions](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Allow] [bit] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Level] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
	[UserId] [int] NULL,
	[UsersGroupId] [int] NULL,
 CONSTRAINT [PK_security_Permissions] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroups]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroups](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ParentId] [int] NULL,
	[Parent] [int] NULL,
 CONSTRAINT [PK_security_UsersGroups] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroupsHierarchy]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroupsHierarchy](
	[ParentGroup] [int] NOT NULL,
	[ChildGroup] [int] NOT NULL,
 CONSTRAINT [PK_security_UsersGroupsHierarchy] PRIMARY KEY CLUSTERED 
(
	[ParentGroup] ASC,
	[ChildGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersToUsersGroups]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersToUsersGroups](
	[GroupId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_security_UsersToUsersGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Site]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Company_id] [int] NULL,
	[SiteOperation] [nvarchar](255) NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[ScheduledDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ActualTimeSpent] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[Deleted] [bit] NULL,
	[Complete] [bit] NULL,
	[QuantityNeeded] [float] NULL,
	[QuantityUsed] [float] NULL,
	[UnitType] [nvarchar](255) NULL,
	[InventoryDecremented] [bit] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[TaskType_id] [int] NULL,
	[Field_id] [int] NULL,
	[InventoryProduct_id] [int] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskType]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskType](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PhoneMobile] [nvarchar](255) NULL,
	[PhoneHome] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[BirthDate] [datetime] NULL,
	[Notes] [nvarchar](255) NULL,
	[LanguageDefault] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[EmergencyContact] [nvarchar](255) NULL,
	[EmergencyContactPhone] [nvarchar](255) NULL,
	[EmployeeId] [nvarchar](255) NULL,
	[SystemSupport] [bit] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Company_id] [int] NULL,
	[UserLoginInfo_id] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[LoginName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Salt] [nvarchar](255) NULL,
	[ByPassToken] [uniqueidentifier] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[User_Id] [int] NULL,
 CONSTRAINT [PK_UserLoginInfo] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoleToUser]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleToUser](
	[User_id] [int] NOT NULL,
	[UserRole_id] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendorBase]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorBase](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[VendorType] [nvarchar](255) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Company] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Fax] [nvarchar](255) NULL,
	[LogoUrl] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[Website] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_VendorBase] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendorContact]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorContact](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](255) NULL,
	[State] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](255) NULL,
	[Country] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Fax] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[Phone] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
	[Vendor_id] [int] NULL,
 CONSTRAINT [PK_VendorContact] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Weather]    Script Date: 2/3/2013 2:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weather](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[ChangedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [int] NULL,
	[Date] [datetime] NULL,
	[HighTemperature] [float] NULL,
	[LowTemperature] [float] NULL,
	[WindSpeed] [float] NULL,
	[RainPrecipitation] [float] NULL,
	[Humidity] [float] NULL,
	[DewPoint] [float] NULL,
	[CreatedBy_id] [int] NULL,
	[ChangedBy_id] [int] NULL,
 CONSTRAINT [PK_Weather] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__security__737584F659063A47]    Script Date: 2/3/2013 2:29:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__security__737584F659063A47] ON [dbo].[security_Operations]
(
	[EntityId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__security__737584F610566F31]    Script Date: 2/3/2013 2:29:36 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__security__737584F610566F31] ON [dbo].[security_UsersGroups]
(
	[EntityId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BaseProduct]  WITH CHECK ADD  CONSTRAINT [FKAE1B102E20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProduct] CHECK CONSTRAINT [FKAE1B102E20B5AF9B]
GO
ALTER TABLE [dbo].[BaseProduct]  WITH CHECK ADD  CONSTRAINT [FKAE1B102E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProduct] CHECK CONSTRAINT [FKAE1B102E8C596B01]
GO
ALTER TABLE [dbo].[BaseProductToFieldVendor]  WITH CHECK ADD  CONSTRAINT [FKAF1CBC9B7F0BAA51] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProductToFieldVendor] CHECK CONSTRAINT [FKAF1CBC9B7F0BAA51]
GO
ALTER TABLE [dbo].[BaseProductToFieldVendor]  WITH CHECK ADD  CONSTRAINT [FKAF1CBC9BFC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProductToFieldVendor] CHECK CONSTRAINT [FKAF1CBC9BFC68D351]
GO
ALTER TABLE [dbo].[Calculator]  WITH CHECK ADD  CONSTRAINT [FK5DB881220B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Calculator] CHECK CONSTRAINT [FK5DB881220B5AF9B]
GO
ALTER TABLE [dbo].[Calculator]  WITH CHECK ADD  CONSTRAINT [FK5DB88128C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Calculator] CHECK CONSTRAINT [FK5DB88128C596B01]
GO
ALTER TABLE [dbo].[Chemical]  WITH CHECK ADD  CONSTRAINT [FK27BDC5F6FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Chemical] CHECK CONSTRAINT [FK27BDC5F6FC68D351]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK2646DB9720B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK2646DB9720B5AF9B]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK2646DB978C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK2646DB978C596B01]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D64720B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D64720B5AF9B]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D6473C3C4C2B] FOREIGN KEY([DocumentCategory_id])
REFERENCES [dbo].[DocumentCategory] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D6473C3C4C2B]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D6478C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D6478C596B01]
GO
ALTER TABLE [dbo].[DocumentCategory]  WITH CHECK ADD  CONSTRAINT [FK9873FDAF20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentCategory] CHECK CONSTRAINT [FK9873FDAF20B5AF9B]
GO
ALTER TABLE [dbo].[DocumentCategory]  WITH CHECK ADD  CONSTRAINT [FK9873FDAF8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentCategory] CHECK CONSTRAINT [FK9873FDAF8C596B01]
GO
ALTER TABLE [dbo].[DocumentToEquipment]  WITH CHECK ADD  CONSTRAINT [FKFA069C2631645407] FOREIGN KEY([Document_id])
REFERENCES [dbo].[Document] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentToEquipment] CHECK CONSTRAINT [FKFA069C2631645407]
GO
ALTER TABLE [dbo].[DocumentToEquipment]  WITH CHECK ADD  CONSTRAINT [FKFA069C26DD09CEF5] FOREIGN KEY([Equipment_id])
REFERENCES [dbo].[Equipment] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentToEquipment] CHECK CONSTRAINT [FKFA069C26DD09CEF5]
GO
ALTER TABLE [dbo].[DocumentToField]  WITH CHECK ADD  CONSTRAINT [FK3149B50631645407] FOREIGN KEY([Document_id])
REFERENCES [dbo].[Document] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentToField] CHECK CONSTRAINT [FK3149B50631645407]
GO
ALTER TABLE [dbo].[DocumentToField]  WITH CHECK ADD  CONSTRAINT [FK3149B5067AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentToField] CHECK CONSTRAINT [FK3149B5067AF94981]
GO
ALTER TABLE [dbo].[EmailJob]  WITH CHECK ADD  CONSTRAINT [FKA5B4730F20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailJob] CHECK CONSTRAINT [FKA5B4730F20B5AF9B]
GO
ALTER TABLE [dbo].[EmailJob]  WITH CHECK ADD  CONSTRAINT [FKA5B4730F871F84B1] FOREIGN KEY([EmailTemplate_id])
REFERENCES [dbo].[EmailTemplate] ([EntityId])
GO
ALTER TABLE [dbo].[EmailJob] CHECK CONSTRAINT [FKA5B4730F871F84B1]
GO
ALTER TABLE [dbo].[EmailJob]  WITH CHECK ADD  CONSTRAINT [FKA5B4730F8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailJob] CHECK CONSTRAINT [FKA5B4730F8C596B01]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK9E50581C20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK9E50581C20B5AF9B]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK9E50581C8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK9E50581C8C596B01]
GO
ALTER TABLE [dbo].[EmailTemplatesToSubscribers]  WITH CHECK ADD  CONSTRAINT [FK5890D2DD532CB363] FOREIGN KEY([EmailJob_id])
REFERENCES [dbo].[EmailJob] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplatesToSubscribers] CHECK CONSTRAINT [FK5890D2DD532CB363]
GO
ALTER TABLE [dbo].[EmailTemplatesToSubscribers]  WITH CHECK ADD  CONSTRAINT [FK5890D2DD63E72CDB] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplatesToSubscribers] CHECK CONSTRAINT [FK5890D2DD63E72CDB]
GO
ALTER TABLE [dbo].[EmployeeToEquipmentTask]  WITH CHECK ADD  CONSTRAINT [FKC27F39AC63E72CDB] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmployeeToEquipmentTask] CHECK CONSTRAINT [FKC27F39AC63E72CDB]
GO
ALTER TABLE [dbo].[EmployeeToEquipmentTask]  WITH CHECK ADD  CONSTRAINT [FKC27F39ACF428DE4F] FOREIGN KEY([EquipmentTask_id])
REFERENCES [dbo].[EquipmentTask] ([EntityId])
GO
ALTER TABLE [dbo].[EmployeeToEquipmentTask] CHECK CONSTRAINT [FKC27F39ACF428DE4F]
GO
ALTER TABLE [dbo].[EmployeeToTask]  WITH CHECK ADD  CONSTRAINT [FK1B8C947A63E72CDB] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmployeeToTask] CHECK CONSTRAINT [FK1B8C947A63E72CDB]
GO
ALTER TABLE [dbo].[EmployeeToTask]  WITH CHECK ADD  CONSTRAINT [FK1B8C947A9836496F] FOREIGN KEY([Task_id])
REFERENCES [dbo].[Task] ([EntityId])
GO
ALTER TABLE [dbo].[EmployeeToTask] CHECK CONSTRAINT [FK1B8C947A9836496F]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A1F37DC5D] FOREIGN KEY([EquipmentType_id])
REFERENCES [dbo].[EquipmentType] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A1F37DC5D]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A20B5AF9B]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A78123386] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A78123386]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A8C596B01]
GO
ALTER TABLE [dbo].[EquipmentTask]  WITH CHECK ADD  CONSTRAINT [FK4173AF9B20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTask] CHECK CONSTRAINT [FK4173AF9B20B5AF9B]
GO
ALTER TABLE [dbo].[EquipmentTask]  WITH CHECK ADD  CONSTRAINT [FK4173AF9B366C8CB1] FOREIGN KEY([TaskType_id])
REFERENCES [dbo].[EquipmentTaskType] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTask] CHECK CONSTRAINT [FK4173AF9B366C8CB1]
GO
ALTER TABLE [dbo].[EquipmentTask]  WITH CHECK ADD  CONSTRAINT [FK4173AF9B8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTask] CHECK CONSTRAINT [FK4173AF9B8C596B01]
GO
ALTER TABLE [dbo].[EquipmentTask]  WITH CHECK ADD  CONSTRAINT [FK4173AF9BDD09CEF5] FOREIGN KEY([Equipment_id])
REFERENCES [dbo].[Equipment] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTask] CHECK CONSTRAINT [FK4173AF9BDD09CEF5]
GO
ALTER TABLE [dbo].[EquipmentTaskType]  WITH CHECK ADD  CONSTRAINT [FKF6F8DD1320B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTaskType] CHECK CONSTRAINT [FKF6F8DD1320B5AF9B]
GO
ALTER TABLE [dbo].[EquipmentTaskType]  WITH CHECK ADD  CONSTRAINT [FKF6F8DD138C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTaskType] CHECK CONSTRAINT [FKF6F8DD138C596B01]
GO
ALTER TABLE [dbo].[EquipmentToTask]  WITH CHECK ADD  CONSTRAINT [FK5B0F86329836496F] FOREIGN KEY([Task_id])
REFERENCES [dbo].[Task] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentToTask] CHECK CONSTRAINT [FK5B0F86329836496F]
GO
ALTER TABLE [dbo].[EquipmentToTask]  WITH CHECK ADD  CONSTRAINT [FK5B0F8632DD09CEF5] FOREIGN KEY([Equipment_id])
REFERENCES [dbo].[Equipment] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentToTask] CHECK CONSTRAINT [FK5B0F8632DD09CEF5]
GO
ALTER TABLE [dbo].[EquipmentType]  WITH CHECK ADD  CONSTRAINT [FK9ECC39EA20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentType] CHECK CONSTRAINT [FK9ECC39EA20B5AF9B]
GO
ALTER TABLE [dbo].[EquipmentType]  WITH CHECK ADD  CONSTRAINT [FK9ECC39EA8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentType] CHECK CONSTRAINT [FK9ECC39EA8C596B01]
GO
ALTER TABLE [dbo].[EquipmentTypeToEquipmentVendor]  WITH CHECK ADD  CONSTRAINT [FKFE3B3DC71F37DC5D] FOREIGN KEY([EquipmentType_id])
REFERENCES [dbo].[EquipmentType] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTypeToEquipmentVendor] CHECK CONSTRAINT [FKFE3B3DC71F37DC5D]
GO
ALTER TABLE [dbo].[EquipmentTypeToEquipmentVendor]  WITH CHECK ADD  CONSTRAINT [FKFE3B3DC778123386] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[EquipmentTypeToEquipmentVendor] CHECK CONSTRAINT [FKFE3B3DC778123386]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB760220B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB760220B5AF9B]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB760232D346DD] FOREIGN KEY([EventType_id])
REFERENCES [dbo].[EventType] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB760232D346DD]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB76027AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB76027AF94981]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB76028C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB76028C596B01]
GO
ALTER TABLE [dbo].[EventType]  WITH CHECK ADD  CONSTRAINT [FKB032D96A20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EventType] CHECK CONSTRAINT [FKB032D96A20B5AF9B]
GO
ALTER TABLE [dbo].[EventType]  WITH CHECK ADD  CONSTRAINT [FKB032D96A8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EventType] CHECK CONSTRAINT [FKB032D96A8C596B01]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FKCAAF5E64FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FKCAAF5E64FC68D351]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK9126EE221226A183] FOREIGN KEY([Site_id])
REFERENCES [dbo].[Site] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK9126EE221226A183]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK9126EE2220B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK9126EE2220B5AF9B]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK9126EE228C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK9126EE228C596B01]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A512D28416] FOREIGN KEY([Product_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A512D28416]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A520B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A520B5AF9B]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A5213E7D9D] FOREIGN KEY([LastVendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A5213E7D9D]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A58C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A58C596B01]
GO
ALTER TABLE [dbo].[LocalizedEnumeration]  WITH CHECK ADD  CONSTRAINT [FK27E02DF620B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedEnumeration] CHECK CONSTRAINT [FK27E02DF620B5AF9B]
GO
ALTER TABLE [dbo].[LocalizedEnumeration]  WITH CHECK ADD  CONSTRAINT [FK27E02DF68C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedEnumeration] CHECK CONSTRAINT [FK27E02DF68C596B01]
GO
ALTER TABLE [dbo].[LocalizedProperty]  WITH CHECK ADD  CONSTRAINT [FK57B6865E20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedProperty] CHECK CONSTRAINT [FK57B6865E20B5AF9B]
GO
ALTER TABLE [dbo].[LocalizedProperty]  WITH CHECK ADD  CONSTRAINT [FK57B6865E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedProperty] CHECK CONSTRAINT [FK57B6865E8C596B01]
GO
ALTER TABLE [dbo].[LocalizedText]  WITH CHECK ADD  CONSTRAINT [FK364F452020B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedText] CHECK CONSTRAINT [FK364F452020B5AF9B]
GO
ALTER TABLE [dbo].[LocalizedText]  WITH CHECK ADD  CONSTRAINT [FK364F45208C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedText] CHECK CONSTRAINT [FK364F45208C596B01]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FKB6D28E23FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FKB6D28E23FC68D351]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK2F5FDD720B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK2F5FDD720B5AF9B]
GO
ALTER TABLE [dbo].[Part]  WITH CHECK ADD  CONSTRAINT [FK2F5FDD78C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Part] CHECK CONSTRAINT [FK2F5FDD78C596B01]
GO
ALTER TABLE [dbo].[PartToEquipmentTask]  WITH CHECK ADD  CONSTRAINT [FKAA6B51D75FA91583] FOREIGN KEY([Part_id])
REFERENCES [dbo].[Part] ([EntityId])
GO
ALTER TABLE [dbo].[PartToEquipmentTask] CHECK CONSTRAINT [FKAA6B51D75FA91583]
GO
ALTER TABLE [dbo].[PartToEquipmentTask]  WITH CHECK ADD  CONSTRAINT [FKAA6B51D7F428DE4F] FOREIGN KEY([EquipmentTask_id])
REFERENCES [dbo].[EquipmentTask] ([EntityId])
GO
ALTER TABLE [dbo].[PartToEquipmentTask] CHECK CONSTRAINT [FKAA6B51D7F428DE4F]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C3213D2FD41] FOREIGN KEY([PhotoCategory_id])
REFERENCES [dbo].[PhotoCategory] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C3213D2FD41]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C3220B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C3220B5AF9B]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C328C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C328C596B01]
GO
ALTER TABLE [dbo].[PhotoCategory]  WITH CHECK ADD  CONSTRAINT [FKED86B64E20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoCategory] CHECK CONSTRAINT [FKED86B64E20B5AF9B]
GO
ALTER TABLE [dbo].[PhotoCategory]  WITH CHECK ADD  CONSTRAINT [FKED86B64E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoCategory] CHECK CONSTRAINT [FKED86B64E8C596B01]
GO
ALTER TABLE [dbo].[PhotoToEquipment]  WITH CHECK ADD  CONSTRAINT [FK38579D0DDD09CEF5] FOREIGN KEY([Equipment_id])
REFERENCES [dbo].[Equipment] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoToEquipment] CHECK CONSTRAINT [FK38579D0DDD09CEF5]
GO
ALTER TABLE [dbo].[PhotoToEquipment]  WITH CHECK ADD  CONSTRAINT [FK38579D0DF79AC10D] FOREIGN KEY([Photo_id])
REFERENCES [dbo].[Photo] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoToEquipment] CHECK CONSTRAINT [FK38579D0DF79AC10D]
GO
ALTER TABLE [dbo].[PhotoToField]  WITH CHECK ADD  CONSTRAINT [FK1CB8A1D17AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoToField] CHECK CONSTRAINT [FK1CB8A1D17AF94981]
GO
ALTER TABLE [dbo].[PhotoToField]  WITH CHECK ADD  CONSTRAINT [FK1CB8A1D1F79AC10D] FOREIGN KEY([Photo_id])
REFERENCES [dbo].[Photo] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoToField] CHECK CONSTRAINT [FK1CB8A1D1F79AC10D]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A520B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A520B5AF9B]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A57F0BAA51] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A57F0BAA51]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A58C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A58C596B01]
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB612D28416] FOREIGN KEY([Product_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB612D28416]
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB620B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB620B5AF9B]
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB635D27A53] FOREIGN KEY([PurchaseOrder_id])
REFERENCES [dbo].[PurchaseOrder] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB635D27A53]
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB68C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB68C596B01]
GO
ALTER TABLE [dbo].[security_Operations]  WITH CHECK ADD  CONSTRAINT [FK77852F468851360] FOREIGN KEY([ParentId])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Operations] CHECK CONSTRAINT [FK77852F468851360]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKAE00533E2957FE83] FOREIGN KEY([OperationId])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKAE00533E2957FE83]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKAE00533EDBB3D07F] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKAE00533EDBB3D07F]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKAE00533EF9705D5D] FOREIGN KEY([UsersGroupId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKAE00533EF9705D5D]
GO
ALTER TABLE [dbo].[security_UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK354957DEFCFA86A8] FOREIGN KEY([ParentId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroups] CHECK CONSTRAINT [FK354957DEFCFA86A8]
GO
ALTER TABLE [dbo].[security_UsersGroups]  WITH CHECK ADD  CONSTRAINT [FK904D780FB1A5BA4F] FOREIGN KEY([Parent])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroups] CHECK CONSTRAINT [FK904D780FB1A5BA4F]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FKCD82CCE25BCBBC14] FOREIGN KEY([ChildGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FKCD82CCE25BCBBC14]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FKCD82CCE2919F22D6] FOREIGN KEY([ParentGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FKCD82CCE2919F22D6]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7654C67E8EB1A08D] FOREIGN KEY([GroupId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7654C67E8EB1A08D]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7654C67EDBB3D07F] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7654C67EDBB3D07F]
GO
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FKCE1592EF20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FKCE1592EF20B5AF9B]
GO
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FKCE1592EF74070A17] FOREIGN KEY([Company_id])
REFERENCES [dbo].[Company] ([EntityId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FKCE1592EF74070A17]
GO
ALTER TABLE [dbo].[Site]  WITH CHECK ADD  CONSTRAINT [FKCE1592EF8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Site] CHECK CONSTRAINT [FKCE1592EF8C596B01]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71ED20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71ED20B5AF9B]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71ED7AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71ED7AF94981]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71ED8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71ED8C596B01]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71EDAA87C6F] FOREIGN KEY([InventoryProduct_id])
REFERENCES [dbo].[InventoryProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71EDAA87C6F]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71EDDF108C47] FOREIGN KEY([TaskType_id])
REFERENCES [dbo].[TaskType] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71EDDF108C47]
GO
ALTER TABLE [dbo].[TaskType]  WITH CHECK ADD  CONSTRAINT [FK786C408520B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TaskType] CHECK CONSTRAINT [FK786C408520B5AF9B]
GO
ALTER TABLE [dbo].[TaskType]  WITH CHECK ADD  CONSTRAINT [FK786C40858C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TaskType] CHECK CONSTRAINT [FK786C40858C596B01]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC98920B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC98920B5AF9B]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC98974070A17] FOREIGN KEY([Company_id])
REFERENCES [dbo].[Company] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC98974070A17]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC9898C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC9898C596B01]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC989F10E77C5] FOREIGN KEY([UserLoginInfo_id])
REFERENCES [dbo].[UserLoginInfo] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC989F10E77C5]
GO
ALTER TABLE [dbo].[UserLoginInfo]  WITH CHECK ADD  CONSTRAINT [FK_User_OneToOne_UserLoginInfo] FOREIGN KEY([User_Id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserLoginInfo] CHECK CONSTRAINT [FK_User_OneToOne_UserLoginInfo]
GO
ALTER TABLE [dbo].[UserLoginInfo]  WITH CHECK ADD  CONSTRAINT [FKA685463220B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserLoginInfo] CHECK CONSTRAINT [FKA685463220B5AF9B]
GO
ALTER TABLE [dbo].[UserLoginInfo]  WITH CHECK ADD  CONSTRAINT [FKA68546328C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserLoginInfo] CHECK CONSTRAINT [FKA68546328C596B01]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK4F2578B120B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK4F2578B120B5AF9B]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK4F2578B18C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK4F2578B18C596B01]
GO
ALTER TABLE [dbo].[UserRoleToUser]  WITH CHECK ADD  CONSTRAINT [FK9CDDA1CF4210CF8F] FOREIGN KEY([UserRole_id])
REFERENCES [dbo].[UserRole] ([EntityId])
GO
ALTER TABLE [dbo].[UserRoleToUser] CHECK CONSTRAINT [FK9CDDA1CF4210CF8F]
GO
ALTER TABLE [dbo].[UserRoleToUser]  WITH CHECK ADD  CONSTRAINT [FK9CDDA1CF63E72CDB] FOREIGN KEY([User_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRoleToUser] CHECK CONSTRAINT [FK9CDDA1CF63E72CDB]
GO
ALTER TABLE [dbo].[VendorBase]  WITH CHECK ADD  CONSTRAINT [FKAD9CED7D20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorBase] CHECK CONSTRAINT [FKAD9CED7D20B5AF9B]
GO
ALTER TABLE [dbo].[VendorBase]  WITH CHECK ADD  CONSTRAINT [FKAD9CED7D8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorBase] CHECK CONSTRAINT [FKAD9CED7D8C596B01]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [FK6A8399B020B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [FK6A8399B020B5AF9B]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [FK6A8399B08C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [FK6A8399B08C596B01]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [Vendor_id] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[VendorBase] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [Vendor_id]
GO
ALTER TABLE [dbo].[Weather]  WITH CHECK ADD  CONSTRAINT [FK5269293E20B5AF9B] FOREIGN KEY([ChangedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Weather] CHECK CONSTRAINT [FK5269293E20B5AF9B]
GO
ALTER TABLE [dbo].[Weather]  WITH CHECK ADD  CONSTRAINT [FK5269293E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Weather] CHECK CONSTRAINT [FK5269293E8C596B01]
GO
