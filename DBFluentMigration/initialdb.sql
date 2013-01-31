/****** Object:  Table [dbo].[BaseProduct]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseProduct](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[InstantiatingType] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[Notes] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__BaseProd__9C892F9D7F60ED59] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BaseProductToVendor]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseProductToVendor](
	[Vendor_id] [bigint] NOT NULL,
	[BaseProduct_id] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Calculator]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calculator](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Calculat__9C892F9D178D7CA5] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[Company_id] [bigint] NULL,
 CONSTRAINT [PK__Category__9C892F9D1273C1CD] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Chemical]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chemical](
	[BaseProduct_id] [bigint] NOT NULL,
	[ActiveIngredient] [nvarchar](255) NULL,
	[ActiveIngredientPercent] [decimal](19, 5) NULL,
	[EPAEstNumber] [nvarchar](255) NULL,
	[EPARegNumber] [nvarchar](255) NULL,
 CONSTRAINT [PK__Chemical__5910CAE31AC9DC03] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[ZipCode] [nvarchar](255) NULL,
	[TaxRate] [float] NULL,
	[NumberOfCategories] [int] NULL,
 CONSTRAINT [PK__Company__9C892F9D1273C1CD] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Document]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[DocumentCategory_id] [bigint] NULL,
	[Field_id] [bigint] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Document__9C892F9D22FF2F51] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DocumentCategory]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentCategory](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Document__9C892F9D1F2E9E6D] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailJob]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailJob](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Subject] [nvarchar](255) NULL,
	[Sender] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Frequency] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[EmailTemplate_id] [bigint] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[EmailJobType_id] [bigint] NULL,
 CONSTRAINT [PK__EmailJob__9C892F9D26CFC035] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailJobType]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailJobType](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__EmailJob__9C892F9D267ABA7A] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[Template] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__EmailTem__9C892F9D2B947552] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmailTemplatesToSubscribers]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplatesToSubscribers](
	[EmailJob_id] [bigint] NOT NULL,
	[User_id] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeToTask]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeToTask](
	[Task_id] [bigint] NOT NULL,
	[User_id] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[TotalHours] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[Vendor_id] [bigint] NULL,
 CONSTRAINT [PK__Equipmen__9C892F9D25869641] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EquipmentToTask]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentToTask](
	[Task_id] [bigint] NOT NULL,
	[Equipment_id] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[ScheduledDate] [datetime] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Notes] [nvarchar](255) NULL,
	[EventType_id] [bigint] NULL,
	[Field_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[Category_id] [bigint] NULL,
 CONSTRAINT [PK__Event__9C892F9D3335971A] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventType]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[EventColor] [nvarchar](255) NULL,
 CONSTRAINT [PK__EventTyp__9C892F9D2D27B809] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Fertilizer]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fertilizer](
	[BaseProduct_id] [bigint] NOT NULL,
	[N] [float] NULL,
	[P] [float] NULL,
	[K] [float] NULL,
 CONSTRAINT [PK__Fertiliz__5910CAE31E9A6CE7] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Field]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Field](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Size] [int] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[Abbreviation] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[FieldColor] [nvarchar](255) NULL,
	[Category_id] [bigint] NULL,
 CONSTRAINT [PK__Field__9C892F9D30F848ED] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InventoryProduct]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryProduct](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[Discriminator] [nvarchar](255) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Quantity] [float] NULL,
	[Description] [nvarchar](255) NULL,
	[DatePurchased] [datetime] NULL,
	[LastUsed] [datetime] NULL,
	[Cost] [float] NULL,
	[UnitType] [nvarchar](255) NULL,
	[SizeOfUnit] [int] NULL,
	[LastVendor_id] [bigint] NULL,
	[Product_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Inventor__9C892F9D34C8D9D1] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedEnumeration]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedEnumeration](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[ValueType] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[Tooltip] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Localize__9C892F9D4277DAAA] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedProperty]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedProperty](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[ParentType] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[Tooltip] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Localize__9C892F9D46486B8E] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedText]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedText](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Culture] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[Text] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Localize__9C892F9D4A18FC72] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Material]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[BaseProduct_id] [bigint] NOT NULL,
 CONSTRAINT [PK__Material__5910CAE3226AFDCB] PRIMARY KEY CLUSTERED 
(
	[BaseProduct_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Photo]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](255) NULL,
	[PhotoCategory_id] [bigint] NULL,
	[Field_id] [bigint] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Photo__9C892F9D51BA1E3A] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PhotoCategory]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoCategory](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__PhotoCat__9C892F9D4DE98D56] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[SubTotal] [float] NULL,
	[Completed] [bit] NULL,
	[Fees] [float] NULL,
	[Tax] [float] NULL,
	[Total] [float] NULL,
	[DateReceived] [datetime] NULL,
	[Vendor_id] [bigint] NULL,
	[CreatedBy_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Purchase__9C892F9D4F7CD00D] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseOrderLineItem]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderLineItem](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Price] [float] NULL,
	[QuantityOrdered] [int] NULL,
	[TotalReceived] [int] NULL,
	[SubTotal] [float] NULL,
	[Tax] [float] NULL,
	[Completed] [bit] NULL,
	[DateRecieved] [datetime] NULL,
	[UnitType] [nvarchar](255) NULL,
	[Product_id] [bigint] NULL,
	[PurchaseOrder_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[Taxable] [bit] NULL,
	[SizeOfUnit] [int] NULL,
 CONSTRAINT [PK__Purchase__9C892F9D4BAC3F29] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[ScheduledDate] [datetime] NULL,
	[ScheduledStartTime] [datetime] NULL,
	[ScheduledEndTime] [datetime] NULL,
	[ActualTimeSpent] [nvarchar](255) NULL,
	[Notes] [nvarchar](255) NULL,
	[Deleted] [bit] NULL,
	[Complete] [bit] NULL,
	[QuantityNeeded] [float] NULL,
	[QuantityUsed] [float] NULL,
	[UnitType] [nvarchar](255) NULL,
	[TaskType_id] [bigint] NULL,
	[Field_id] [bigint] NULL,
	[InventoryProduct_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[InventoryDecremented] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[Category_id] [bigint] NULL,
 CONSTRAINT [PK__Task__9C892F9D534D60F1] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskType]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskType](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__TaskType__9C892F9D62E4AA3C] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[EmployeeId] [nvarchar](255) NULL,
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
	[LanguageDefault] [nvarchar](5) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[IsDeleted] [bit] NULL,
	[Company_id] [bigint] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
	[EmergencyContact] [nvarchar](255) NULL,
	[EmergencyContactPhone] [nvarchar](255) NULL,
	[UserLoginInfo_id] [bigint] NULL,
 CONSTRAINT [PK__User__9C892F9D5CD6CB2B] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLoginInfo]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginInfo](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[CompanyId] [bigint] NULL,
	[LoginName] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[ByPassToken] [uniqueidentifier] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__UserLogi__9C892F9D628FA481] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__UserRole__9C892F9D66603565] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoleToUser]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleToUser](
	[User_id] [bigint] NOT NULL,
	[UserRole_id] [bigint] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
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
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Vendor__9C892F9D7226EDCC] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendorContact]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendorContact](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
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
	[Vendor_id] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__VendorCo__9C892F9D6E565CE8] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Weather]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weather](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[LastModified] [datetime] NULL,
	[DateCreated] [datetime] NULL,
	[CompanyId] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[Date] [datetime] NULL,
	[HighTemperature] [float] NULL,
	[LowTemperature] [float] NULL,
	[WindSpeed] [float] NULL,
	[RainPrecipitation] [float] NULL,
	[Humidity] [float] NULL,
	[EvaporationRate] [float] NULL,
	[DewPoint] [float] NULL,
	[CreatedBy_id] [bigint] NULL,
	[ModifiedBy_id] [bigint] NULL,
 CONSTRAINT [PK__Weather__9C892F9D76EBA2E9] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Operations]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Operations](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Parent] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Permissions]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Permissions](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[Allow] [bit] NOT NULL,
	[Level] [int] NOT NULL,
	[Operation] [bigint] NOT NULL,
	[User] [bigint] NULL,
	[UsersGroup] [bigint] NULL,
	[Description] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroups]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroups](
	[EntityId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Parent] [bigint] NULL,
	[Description] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroupsHierarchy]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroupsHierarchy](
	[ParentGroup] [bigint] NOT NULL,
	[ChildGroup] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChildGroup] ASC,
	[ParentGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersToUsersGroups]    Script Date: 1/11/2013 8:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersToUsersGroups](
	[GroupId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BaseProduct]  WITH CHECK ADD  CONSTRAINT [FKAE1B102E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProduct] CHECK CONSTRAINT [FKAE1B102E8C596B01]
GO
ALTER TABLE [dbo].[BaseProduct]  WITH CHECK ADD  CONSTRAINT [FKAE1B102EBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProduct] CHECK CONSTRAINT [FKAE1B102EBF541B78]
GO
ALTER TABLE [dbo].[BaseProductToVendor]  WITH CHECK ADD  CONSTRAINT [FK41C8A9C9768BE715] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[Vendor] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProductToVendor] CHECK CONSTRAINT [FK41C8A9C9768BE715]
GO
ALTER TABLE [dbo].[BaseProductToVendor]  WITH CHECK ADD  CONSTRAINT [FK41C8A9C9FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[BaseProductToVendor] CHECK CONSTRAINT [FK41C8A9C9FC68D351]
GO
ALTER TABLE [dbo].[Calculator]  WITH CHECK ADD  CONSTRAINT [FK5DB88128C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Calculator] CHECK CONSTRAINT [FK5DB88128C596B01]
GO
ALTER TABLE [dbo].[Calculator]  WITH CHECK ADD  CONSTRAINT [FK5DB8812BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Calculator] CHECK CONSTRAINT [FK5DB8812BF541B78]
GO
ALTER TABLE [dbo].[Chemical]  WITH CHECK ADD  CONSTRAINT [FK27BDC5F6FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Chemical] CHECK CONSTRAINT [FK27BDC5F6FC68D351]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK2646DB978C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK2646DB978C596B01]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK2646DB97BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK2646DB97BF541B78]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D6477AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D6477AF94981]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D6478C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D6478C596B01]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D6479D815AA3] FOREIGN KEY([DocumentCategory_id])
REFERENCES [dbo].[DocumentCategory] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D6479D815AA3]
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK57D3D647BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK57D3D647BF541B78]
GO
ALTER TABLE [dbo].[DocumentCategory]  WITH CHECK ADD  CONSTRAINT [FK9873FDAF8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentCategory] CHECK CONSTRAINT [FK9873FDAF8C596B01]
GO
ALTER TABLE [dbo].[DocumentCategory]  WITH CHECK ADD  CONSTRAINT [FK9873FDAFBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[DocumentCategory] CHECK CONSTRAINT [FK9873FDAFBF541B78]
GO
ALTER TABLE [dbo].[EmailJob]  WITH CHECK ADD  CONSTRAINT [FK_EmailJob_EmailJobType] FOREIGN KEY([EmailJobType_id])
REFERENCES [dbo].[EmailJobType] ([EntityId])
GO
ALTER TABLE [dbo].[EmailJob] CHECK CONSTRAINT [FK_EmailJob_EmailJobType]
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
ALTER TABLE [dbo].[EmailJob]  WITH CHECK ADD  CONSTRAINT [FKA5B4730FBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailJob] CHECK CONSTRAINT [FKA5B4730FBF541B78]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK9E50581C8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK9E50581C8C596B01]
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD  CONSTRAINT [FK9E50581CBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EmailTemplate] CHECK CONSTRAINT [FK9E50581CBF541B78]
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
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A768BE715] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[Vendor] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A768BE715]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8A8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8A8C596B01]
GO
ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD  CONSTRAINT [FKDEB29F8ABF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Equipment] CHECK CONSTRAINT [FKDEB29F8ABF541B78]
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
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Category] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Category] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Category]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB76021F49C463] FOREIGN KEY([EventType_id])
REFERENCES [dbo].[EventType] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB76021F49C463]
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
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK27CB7602BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK27CB7602BF541B78]
GO
ALTER TABLE [dbo].[EventType]  WITH CHECK ADD  CONSTRAINT [FKB032D96A8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EventType] CHECK CONSTRAINT [FKB032D96A8C596B01]
GO
ALTER TABLE [dbo].[EventType]  WITH CHECK ADD  CONSTRAINT [FKB032D96ABF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[EventType] CHECK CONSTRAINT [FKB032D96ABF541B78]
GO
ALTER TABLE [dbo].[Fertilizer]  WITH CHECK ADD  CONSTRAINT [FKCAAF5E64FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Fertilizer] CHECK CONSTRAINT [FKCAAF5E64FC68D351]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK_Field_Category] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Category] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK_Field_Category]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK9126EE228C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK9126EE228C596B01]
GO
ALTER TABLE [dbo].[Field]  WITH CHECK ADD  CONSTRAINT [FK9126EE22BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Field] CHECK CONSTRAINT [FK9126EE22BF541B78]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A512D28416] FOREIGN KEY([Product_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A512D28416]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A528BE30D9] FOREIGN KEY([LastVendor_id])
REFERENCES [dbo].[Vendor] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A528BE30D9]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A58C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A58C596B01]
GO
ALTER TABLE [dbo].[InventoryProduct]  WITH CHECK ADD  CONSTRAINT [FK9E1B01A5BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[InventoryProduct] CHECK CONSTRAINT [FK9E1B01A5BF541B78]
GO
ALTER TABLE [dbo].[LocalizedEnumeration]  WITH CHECK ADD  CONSTRAINT [FK27E02DF68C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedEnumeration] CHECK CONSTRAINT [FK27E02DF68C596B01]
GO
ALTER TABLE [dbo].[LocalizedEnumeration]  WITH CHECK ADD  CONSTRAINT [FK27E02DF6BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedEnumeration] CHECK CONSTRAINT [FK27E02DF6BF541B78]
GO
ALTER TABLE [dbo].[LocalizedProperty]  WITH CHECK ADD  CONSTRAINT [FK57B6865E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedProperty] CHECK CONSTRAINT [FK57B6865E8C596B01]
GO
ALTER TABLE [dbo].[LocalizedProperty]  WITH CHECK ADD  CONSTRAINT [FK57B6865EBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedProperty] CHECK CONSTRAINT [FK57B6865EBF541B78]
GO
ALTER TABLE [dbo].[LocalizedText]  WITH CHECK ADD  CONSTRAINT [FK364F45208C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedText] CHECK CONSTRAINT [FK364F45208C596B01]
GO
ALTER TABLE [dbo].[LocalizedText]  WITH CHECK ADD  CONSTRAINT [FK364F4520BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[LocalizedText] CHECK CONSTRAINT [FK364F4520BF541B78]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FKB6D28E23FC68D351] FOREIGN KEY([BaseProduct_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FKB6D28E23FC68D351]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C327AF94981] FOREIGN KEY([Field_id])
REFERENCES [dbo].[Field] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C327AF94981]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C328C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C328C596B01]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C32BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C32BF541B78]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK3BB7C32D89B5D7F] FOREIGN KEY([PhotoCategory_id])
REFERENCES [dbo].[PhotoCategory] ([EntityId])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK3BB7C32D89B5D7F]
GO
ALTER TABLE [dbo].[PhotoCategory]  WITH CHECK ADD  CONSTRAINT [FKED86B64E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoCategory] CHECK CONSTRAINT [FKED86B64E8C596B01]
GO
ALTER TABLE [dbo].[PhotoCategory]  WITH CHECK ADD  CONSTRAINT [FKED86B64EBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PhotoCategory] CHECK CONSTRAINT [FKED86B64EBF541B78]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A5768BE715] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[Vendor] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A5768BE715]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A58C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A58C596B01]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK8BCCD4A5BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK8BCCD4A5BF541B78]
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB612D28416] FOREIGN KEY([Product_id])
REFERENCES [dbo].[BaseProduct] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB612D28416]
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
ALTER TABLE [dbo].[PurchaseOrderLineItem]  WITH CHECK ADD  CONSTRAINT [FK89D81FB6BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[PurchaseOrderLineItem] CHECK CONSTRAINT [FK89D81FB6BF541B78]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Category] FOREIGN KEY([Category_id])
REFERENCES [dbo].[Category] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Category]
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
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71EDAC812EDB] FOREIGN KEY([TaskType_id])
REFERENCES [dbo].[TaskType] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71EDAC812EDB]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FKA59D71EDBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FKA59D71EDBF541B78]
GO
ALTER TABLE [dbo].[TaskType]  WITH CHECK ADD  CONSTRAINT [FK786C40858C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TaskType] CHECK CONSTRAINT [FK786C40858C596B01]
GO
ALTER TABLE [dbo].[TaskType]  WITH CHECK ADD  CONSTRAINT [FK786C4085BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[TaskType] CHECK CONSTRAINT [FK786C4085BF541B78]
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
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC989BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC989BF541B78]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK564EC989F10E77C5] FOREIGN KEY([UserLoginInfo_id])
REFERENCES [dbo].[UserLoginInfo] ([EntityId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK564EC989F10E77C5]
GO
ALTER TABLE [dbo].[UserLoginInfo]  WITH CHECK ADD  CONSTRAINT [FKA68546328C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserLoginInfo] CHECK CONSTRAINT [FKA68546328C596B01]
GO
ALTER TABLE [dbo].[UserLoginInfo]  WITH CHECK ADD  CONSTRAINT [FKA6854632BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserLoginInfo] CHECK CONSTRAINT [FKA6854632BF541B78]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK4F2578B18C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK4F2578B18C596B01]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK4F2578B1BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK4F2578B1BF541B78]
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
ALTER TABLE [dbo].[Vendor]  WITH CHECK ADD  CONSTRAINT [FK30D39A08C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Vendor] CHECK CONSTRAINT [FK30D39A08C596B01]
GO
ALTER TABLE [dbo].[Vendor]  WITH CHECK ADD  CONSTRAINT [FK30D39A0BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Vendor] CHECK CONSTRAINT [FK30D39A0BF541B78]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [FK6A8399B0768BE715] FOREIGN KEY([Vendor_id])
REFERENCES [dbo].[Vendor] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [FK6A8399B0768BE715]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [FK6A8399B08C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [FK6A8399B08C596B01]
GO
ALTER TABLE [dbo].[VendorContact]  WITH CHECK ADD  CONSTRAINT [FK6A8399B0BF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[VendorContact] CHECK CONSTRAINT [FK6A8399B0BF541B78]
GO
ALTER TABLE [dbo].[Weather]  WITH CHECK ADD  CONSTRAINT [FK5269293E8C596B01] FOREIGN KEY([CreatedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Weather] CHECK CONSTRAINT [FK5269293E8C596B01]
GO
ALTER TABLE [dbo].[Weather]  WITH CHECK ADD  CONSTRAINT [FK5269293EBF541B78] FOREIGN KEY([ModifiedBy_id])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[Weather] CHECK CONSTRAINT [FK5269293EBF541B78]
GO
ALTER TABLE [dbo].[security_Operations]  WITH CHECK ADD  CONSTRAINT [FKE58BBFF82B7CDCD3] FOREIGN KEY([Parent])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Operations] CHECK CONSTRAINT [FKE58BBFF82B7CDCD3]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C2EE8F612] FOREIGN KEY([UsersGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C2EE8F612]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C71C937C7] FOREIGN KEY([Operation])
REFERENCES [dbo].[security_Operations] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C71C937C7]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4CFC8C2B95] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4CFC8C2B95]
GO
ALTER TABLE [dbo].[security_UsersGroups]  WITH CHECK ADD  CONSTRAINT [FKEC3AF233D0CB87D0] FOREIGN KEY([Parent])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroups] CHECK CONSTRAINT [FKEC3AF233D0CB87D0]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA860AB70] FOREIGN KEY([ChildGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA860AB70]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA87BAE50] FOREIGN KEY([ParentGroup])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA87BAE50]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27A1238D4D4] FOREIGN KEY([GroupId])
REFERENCES [dbo].[security_UsersGroups] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27A1238D4D4]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27AA6C99102] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([EntityId])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27AA6C99102]
GO
USE [master]
GO
ALTER DATABASE [KnowYourTurf_Prod] SET  READ_WRITE 
GO
