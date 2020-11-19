/****** Object:  Schema [business]    Script Date: 19/11/2020 10:18:11 ******/
CREATE SCHEMA [business]
GO
/****** Object:  Schema [common]    Script Date: 19/11/2020 10:18:11 ******/
CREATE SCHEMA [common]
GO
/****** Object:  Table [business].[Complaint]    Script Date: 19/11/2020 10:18:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[Complaint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserAgentId] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[SourceIP] [nvarchar](20) NULL,
	[LatLng] [nvarchar](50) NULL,
	[ProductId] [int] NULL,
	[ProductPriceId] [int] NULL,
	[StoreId] [int] NULL,
	[InterestId] [int] NULL,
	[Note] [nvarchar](255) NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [business].[Interesse]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[Interesse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Interesse] [nvarchar](255) NULL,
	[CreationDate] [datetime] NOT NULL,
	[url] [nvarchar](255) NULL,
	[urlanterior] [nvarchar](255) NULL,
	[SessionID] [nvarchar](255) NULL,
	[LatLng] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[Product]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [business].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [nvarchar](15) NULL,
	[Name] [nvarchar](100) NULL,
	[Details] [nvarchar](1024) NULL,
	[Picture] [varbinary](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[Source] [nvarchar](5) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[UserAgentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [business].[ProductFile]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductFile](
	[ProductFileId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Sku] [nvarchar](15) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL DEFAULT ('DEF'),
	[StatusId] [nvarchar](5) NOT NULL DEFAULT ('ACT'),
	[VirtualPath] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ProductLink]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductLink](
	[ProductLinkId] [int] IDENTITY(1,1) NOT NULL,
	[LatLng] [nvarchar](50) NOT NULL,
	[ProductId] [int] NULL,
	[StoreId] [int] NULL,
	[Sku] [nvarchar](15) NULL,
	[Price] [decimal](14, 2) NOT NULL,
	[Amount] [int] NULL,
	[Measurement] [nvarchar](20) NULL,
	[Checked] [int] NULL,
	[NotChecked] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[Link] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductLinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ProductPremiere]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductPremiere](
	[ProductPremiereId] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [nvarchar](15) NULL,
	[Name] [nvarchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[SupplyChainId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ProductPrice]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductPrice](
	[ProductPriceId] [int] IDENTITY(1,1) NOT NULL,
	[UserAgentId] [int] NOT NULL,
	[LatLng] [nvarchar](50) NOT NULL,
	[ProductId] [int] NULL,
	[StoreId] [int] NULL,
	[Sku] [nvarchar](15) NULL,
	[Price] [decimal](14, 2) NOT NULL,
	[Amount] [int] NULL,
	[Measurement] [nvarchar](20) NULL,
	[Checked] [int] NULL,
	[NotChecked] [int] NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL DEFAULT ('DEF'),
	[StatusId] [nvarchar](5) NOT NULL DEFAULT ('ACT'),
	[ShelfLife] [datetime] NULL,
	[Deadline] [datetime] NULL,
	[ActivityKey] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductPriceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ActivityKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ProductReferenced]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductReferenced](
	[ReferencedId] [int] IDENTITY(1,1) NOT NULL,
	[UserAgentId] [int] NOT NULL,
	[Sku] [nvarchar](15) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ProductTemp]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [business].[ProductTemp](
	[ProductTempId] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [nvarchar](15) NULL,
	[Name] [nvarchar](50) NULL,
	[Details] [nvarchar](1024) NULL,
	[Picture] [varbinary](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[Source] [nvarchar](5) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[UserAgentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductTempId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [business].[ProductXSupplyChain]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ProductXSupplyChain](
	[ProductId] [int] NOT NULL,
	[SupplyChainId] [int] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[LevelOfNeed] [nvarchar](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[SupplyChainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[Score]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[Score](
	[UserAgentId] [int] NOT NULL,
	[Store] [int] NOT NULL,
	[Product] [int] NOT NULL,
	[ProductPrice] [int] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [business].[ShoppingList]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[ShoppingList](
	[ListId] [int] IDENTITY(1,1) NOT NULL,
	[UserAgentId] [int] NOT NULL,
	[TargetName] [nvarchar](10) NOT NULL,
	[Sku] [nvarchar](15) NOT NULL,
	[Amount] [int] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[Store]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[Store](
	[StoreId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Source] [nvarchar](5) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[UserAgentId] [int] NULL,
	[DDI] [nvarchar](3) NULL,
	[DDD] [nvarchar](4) NULL,
	[Phone] [nvarchar](20) NULL,
	[WebSite] [nvarchar](255) NULL,
	[Address1] [nvarchar](50) NULL,
	[Number] [nvarchar](5) NULL,
	[Address2] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](10) NULL,
	[City] [nvarchar](50) NULL,
	[StateName] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[ActivityKey] [nvarchar](100) NULL,
	[Lat] [decimal](13, 10) NULL,
	[Lng] [decimal](13, 10) NULL,
PRIMARY KEY CLUSTERED 
(
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [business].[SupplyChain]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [business].[SupplyChain](
	[SupplyChainId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](6) NULL,
	[Name] [nvarchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[Category] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplyChainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [common].[Email]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[Email](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[ToEmail] [nvarchar](256) NOT NULL,
	[FromEmail] [nvarchar](256) NOT NULL,
	[Subject] [nvarchar](256) NOT NULL,
	[Body] [text] NULL,
	[CreationDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[ReferenceCode] [nvarchar](256) NULL,
	[LastModifiedDate] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [common].[EmailTemplate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[EmailTemplate](
	[EmailTemplateId] [int] NOT NULL,
	[LanguageId] [smallint] NOT NULL,
	[EmailSubejct] [nvarchar](125) NOT NULL,
	[EmailMessage] [nvarchar](512) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmailTemplateId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [common].[Language]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[Language](
	[LanguageId] [smallint] IDENTITY(1,1) NOT NULL,
	[LanguageCode] [nvarchar](10) NOT NULL,
	[LanguageName] [nvarchar](255) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NOT NULL,
	[StatusId] [nvarchar](5) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[TranslationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [common].[UserAgent]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[UserAgent](
	[UserAgentId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneDDI] [nvarchar](3) NULL,
	[PhoneDDDNumber] [nvarchar](15) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NULL,
	[StatusId] [nvarchar](5) NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL DEFAULT (getdate()),
	[ActivityKey] [nvarchar](100) NULL,
	[UserLanguageCode] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
 CONSTRAINT [UC_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [common].[UserAgentTemp]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[UserAgentTemp](
	[UserAgentId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](25) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PhoneDDI] [nvarchar](3) NOT NULL,
	[PhoneDDDNumber] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NULL,
	[StatusId] [nvarchar](5) NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[ActivityKey] [nvarchar](100) NULL,
	[UserLanguageCode] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY],
 CONSTRAINT [UC_CodeTemp] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [common].[UserEmailToken]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [common].[UserEmailToken](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](6) NOT NULL,
	[ObjectTypeId] [nvarchar](5) NULL,
	[StatusId] [nvarchar](5) NULL,
	[CreationDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL DEFAULT (getdate()),
	[DeadlineDate] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [business].[Complaint] ADD  DEFAULT ('DEF') FOR [ObjectTypeId]
GO
ALTER TABLE [business].[Complaint] ADD  DEFAULT ('ACT') FOR [StatusId]
GO
ALTER TABLE [business].[ProductLink] ADD  DEFAULT ('DEF') FOR [ObjectTypeId]
GO
ALTER TABLE [business].[ProductLink] ADD  DEFAULT ('ACT') FOR [StatusId]
GO
ALTER TABLE [common].[Language] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [common].[UserAgentTemp] ADD  DEFAULT (getdate()) FOR [LastModifiedDate]
GO
ALTER TABLE [business].[ProductPremiere]  WITH CHECK ADD  CONSTRAINT [FK_SupplyChainId] FOREIGN KEY([SupplyChainId])
REFERENCES [business].[SupplyChain] ([SupplyChainId])
GO
ALTER TABLE [business].[ProductPremiere] CHECK CONSTRAINT [FK_SupplyChainId]
GO
ALTER TABLE [common].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([LanguageId])
REFERENCES [common].[Language] ([LanguageId])
GO
/****** Object:  StoredProcedure [business].[ComplaintInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [business].[ComplaintInsert]
(
@IN_LatLng [nvarchar](50),
@IN_UserAgentId int=  null,
@IN_ProductId int = null,
@IN_ProductPriceId int = null,
@IN_StoreId int = null,
@IN_Note nvarchar(255)  = null,
@IN_SourceIP nvarchar(20) = null,
@IN_InterestId int =  null
)
as

INSERT INTO [business].[Complaint]
           (CreationDate
		   ,UserAgentId
		   ,SourceIP
           ,[LatLng]
           ,[ProductId]
           ,[ProductPriceId]
           ,[StoreId]
		   ,InterestId
		   ,Note)
     VALUES
           (getDate(),
		   @IN_UserAgentId,
		   @IN_SourceIP,
           @IN_LatLng,
           @IN_ProductId,
           @IN_ProductPriceId,
           @IN_StoreId,
		   @IN_InterestId,
		   @IN_Note
		   )





GO
/****** Object:  StoredProcedure [business].[ComplaintSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*


exec  [business].[ComplaintSelect]


select * from [business].[Complaint]

*/

CREATE procedure  [business].[ComplaintSelect]
(
 @IN_StatusId nvarchar(5) = 'ACT'
)
as

SELECT
 SubjecType
,Code UserDenounced
,Name SubjectReview
,[SourceIP]
,[LatLng]
,max([IdIn]) IdIn
FROM 
(
SELECT '[Product]' SubjecType
        ,u.Code
	   ,p.[Name] as Name
      ,c.[SourceIP]
      ,c.[LatLng]
      ,c.[ProductId] as IdIn
  FROM [business].[Complaint] c (NoLock)
  inner join [business].[Product] p (NoLock)
  on c.ProductId = p.ProductId
    left join [common].[UserAgent] u (NoLock)
on p.[UserAgentId] = u.[UserAgentId]
  where
  c.[StatusId] = 'ACT'

  union all

  SELECT '[store]' SubjecType
        ,u.Code
	   ,e.Name as Name
      ,c.[SourceIP]
      ,c.[LatLng]
      ,c.[StoreId] as IdIn
  FROM [business].[Complaint] c (NoLock)
  inner join [business].store e (NoLock)
  on c.[StoreId] = e.[StoreId]
    left join [common].[UserAgent] u (NoLock)
on e.[UserAgentId] = u.[UserAgentId]
  where
  c.[StatusId] = @IN_StatusId

  union all

  SELECT '[interest]' SubjecType
       ,'Anomino' as Code
	   ,i.[Interesse] as Name
      ,c.[SourceIP]
      ,c.[LatLng]
      ,i.[Id] as IdIn
        FROM [business].[Complaint] c (NoLock)
  inner join [business].[Interesse] i (NoLock)
  on c.[InterestId] = i.[Id]
    where
  c.[StatusId] = @IN_StatusId
) aaa 
group by
 Code 
,SubjecType
,Name
,[SourceIP]
,[LatLng]




GO
/****** Object:  StoredProcedure [business].[InteresseInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [business].[InteresseInsert]
(
@IN_Interesse nvarchar(255) = null,
@IN_ur nvarchar(255) = null,
@IN_urlanterior nvarchar(255) = null,
@IN_SessionID nvarchar(255) = null,
@IN_LatLng nvarchar(50) = null
)
as


if not exists (select 1 from business.Interesse (NoLock) where [Interesse] =  @IN_Interesse)
begin
	insert into business.Interesse ([Interesse], [CreationDate], url, urlanterior, SessionID, LatLng) values(@IN_Interesse, getdate(), @IN_ur, @IN_urlanterior, @IN_SessionID, @IN_LatLng)
end






GO
/****** Object:  StoredProcedure [business].[InteresseSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









/*

exec [business].[InteresseSelect]

*/

CREATE procedure [business].[InteresseSelect]
as

SELECT  top 50
		max(i.Id) Id
        ,i.[Interesse]
	   ,Convert(char(10),i.CreationDate,111) as CreationDateView
      ,i.[LatLng]
	  ,Count(*)
	  ,(case when c.InterestId is null then 0 else 1 end) Complained
  FROM [business].[Interesse] i (NoLock) 
  left join business.Complaint c (NoLock)
  on i.Id = c.InterestId
  where i.[LatLng] is not null and i.[LatLng] <> '' and i.[LatLng] <> '0,0'
  and  CHARINDEX('receita', isNull(i.url,'')) = 0  -- não mostrar searh de receita na pagina de tendência...
  and i.[Interesse] not like '%palavrão1%'
  and i.[Interesse] not like '%palavrão2%'
  and i.[Interesse] not like '%palavrãoEtc%'

  group by
		  i.[Interesse]
		,Convert(char(10),i.CreationDate,111)
      ,i.[LatLng]
	  ,c.InterestId
  order by CreationDateView desc, i.[Interesse] asc






GO
/****** Object:  StoredProcedure [business].[ProducByIdSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [business].[ProducByIdSelect]
(
@IN_ProductId int
)
as


--header - dados do produto
SELECT top 10
       [Sku]
      ,IsNull([Name],'') Name
      ,[CreationDate]
      ,[LastModifiedDate]
      ,[Source]
      ,[ObjectTypeId]
      ,[StatusId]
      ,IsNUll([Details],'') Details
      ,IsNUll([Picture],0) Picture
	  ,IsNull(UserAgentId,0) UserAgentId
  FROM [business].[Product] (NoLock)
where @IN_ProductId = ProductId














GO
/****** Object:  StoredProcedure [business].[ProductBySkuSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE procedure [business].[ProductBySkuSelect]
(
 @IN_Sku nvarchar(14)
)
as

--header - dados do produto
SELECT top 10
       p.[Sku]
      ,IsNull(p.[Name],'') Name
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
      ,p.[Source]
      ,p.[ObjectTypeId]
      ,p.[StatusId]
      ,IsNUll(p.[Details],'') Details
      ,IsNUll(p.[Picture],0) Picture
	  ,IsNull(p.UserAgentId,0) UserAgentId
	  ,(case when c.[ProductId] is null then 0 else 1 end) Complained
	  ,p.[ProductId]
	  ,IsNUll(f.VirtualPath,'') VirtualPath
  FROM [business].[Product] p (NoLock)
  left join [business].[Complaint] c (NoLock)
  on c.[ProductId] = p.[ProductId]
    left join [business].[ProductFile] f (NoLock)
  on f.[ProductId] = p.[ProductId]
where @IN_Sku = p.[Sku]










GO
/****** Object:  StoredProcedure [business].[ProductByUserSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*

exec [business].[ProductByUserSelect]
@IN_UserAgentId=3

*/


CREATE procedure [business].[ProductByUserSelect]
(
 @IN_UserAgentId Int
)
as

--header - dados do produto
SELECT top 100
       p.[Sku]
      ,IsNull(p.[Name],'') Name
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
      ,p.[Source]
      ,p.[ObjectTypeId]
      ,p.[StatusId]
      ,IsNUll(p.[Details],'') Details
      ,IsNUll(p.[Picture],0) Picture --PictureBase64 --Picture
	  ,IsNull(p.UserAgentId,0) UserAgentId
	  ,p.[ProductId]
  FROM [business].[Product]  p (NoLock)
  left join [business].[ProductReferenced] r (NoLock)
  on r.sku = p.sku and r.UserAgentId = @IN_UserAgentId
where (@IN_UserAgentId = r.UserAgentId ) or 
		(p.sku in ( SELECT [Sku] --> para que qualquer usuário possa adicionar preços ao grupo de produto premiere - em destaque - de muita procura
		FROM [business].[ProductPremiere] (noLock)
		))
order by p.[ProductId] desc





GO
/****** Object:  StoredProcedure [business].[ProductFileInsertOrUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


 CREATE procedure [business].[ProductFileInsertOrUpdate]
(
 @IN_ProductId int
,@IN_VirtualPath nvarchar(255) 
)
as

declare @P_Sku nvarchar(15)

if exists 
(select 1 from  [business].[ProductFile] (NoLock) p 
 WHERE p.ProductId = @IN_ProductId
 )
begin 

update business.ProductFileUpdate
set  VirtualPath = @IN_VirtualPath
    WHERE ProductId = @IN_ProductId


end
else
begin

select @P_Sku = sku from [business].[Product] (NoLock) where [ProductId] = @IN_ProductId

INSERT INTO [business].[ProductFile]
           ([ProductId]
           ,[Sku]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[VirtualPath])
     VALUES
           (@IN_ProductId
           ,@P_Sku
           ,getdate()
           ,getdate()
           ,'DEF'
           ,'ACT'
           ,@IN_VirtualPath)

end




GO
/****** Object:  StoredProcedure [business].[ProductInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [business].[ProductInsert]
(
 @IN_Sku nvarchar(14)
,@IN_Name nvarchar(100) = null
,@IN_Details nvarchar(1024) = null
,@IN_Picture varbinary(max) = null
,@IN_SupplyChainCode nvarchar(6) = null
,@IN_UserAgentId int = null
)
as
 
 INSERT INTO [business].[Product]
           ([Sku]
           ,[Name]
           ,[Details]
           ,[Picture]
		   ,[UserAgentId]
		   )
     VALUES
           (
		    @IN_Sku
           ,@IN_Name
           ,@IN_Details
           ,@IN_Picture
		   ,@IN_UserAgentId
		   )


  if ( @IN_SupplyChainCode  is not null)
  begin

		declare @P_ProductId int
		declare @p_SupplyChainId int

		select @p_SupplyChainId = [SupplyChainId] from [business].[SupplyChain] where [Code] =  @IN_SupplyChainCode 
		Set @P_ProductId = Scope_Identity();

		if not exists (select 1 from [business].[SupplyChainSelect] (NoLock) where [ProductId] = @P_ProductId and [SupplyChainId]= @p_SupplyChainId)
		begin

			INSERT INTO [business].[ProductXSupplyChain]
					   ([ProductId]
					   ,[SupplyChainId]
					   ,[ObjectTypeId]
					   ,[StatusId]
					   ,[LevelOfNeed])
				 VALUES
					   ( @P_ProductId
						,@p_SupplyChainId
						,'DEF'
						,'ACT'
						,'0'
					   )

		end

  end











GO
/****** Object:  StoredProcedure [business].[ProductNoFileSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*


exec [business].[ProductNoFileSelect]
@IN_Name = "7891"


*/

CREATE procedure [business].[ProductNoFileSelect]
(
 @IN_Sku nvarchar(14) = null
)
as

SELECT top 10
       p.[ProductId]
      ,p.[Sku]
      ,p.[Source]
      ,p.[ObjectTypeId]
      ,p.[StatusId]
      ,IsNUll(p.[Picture],0) Picture
  FROM [business].[Product] p (NoLock)
  left join [business].[ProductFile] f
  on f.[Sku] = p.[Sku]
where	p.StatusId = 'ACT'
and		f.[VirtualPath] is null
and     p.[Picture] is not null
and		( @IN_Sku is null or @IN_Sku = p.[Sku] )
order by p.LastModifiedDate asc






GO
/****** Object:  StoredProcedure [business].[ProductPremiereSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [business].[ProductPremiereSelect]
(
@IN_Code nvarchar(6) = null
)
as

SELECT [ProductPremiereId]
      ,p.[Sku]
      ,p.[Name] 
	  ,s.[Code] SupplyChainCode
	  ,s.[Name] SupplyChainName
  FROM [business].[ProductPremiere] p (NoLock)
  left join [business].[SupplyChain] s (NoLock)
  on p.[SupplyChainId] = s.[SupplyChainId]
  where ( @IN_Code  is null or  (s.[Code] = @IN_Code) ) 
  order by p.[Name] desc






GO
/****** Object:  StoredProcedure [business].[ProductPriceByDaysAgoSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











/*

exec [business].[ProductPriceByDaysAgoSelect]
 @IN_Sku = '7891008109849'
,@IN_DaysAgo= 1

*/

CREATE procedure [business].[ProductPriceByDaysAgoSelect]
(--> Web
 @IN_Sku nvarchar(14)
,@IN_DaysAgo smallint = 30
,@IN_Lat decimal(13,10) = 0
,@IN_Lng decimal(13,10) = 0
,@IN_Radius decimal(13,10) = 1
)
as

/**********************************************************************************
ESTE SET FIXO EH DEVIDO DADOS ESCASSOS DE PREÇOS NESTA FASE DE TESTE E EXPOSIÇÃO 
**********************************************************************************/

set @IN_DaysAgo = 365

declare @P_DateLimit as datetime

set @P_DateLimit = dateadd(day,(@IN_DaysAgo * -1), replace(convert(char(10),getdate(),121),'-','') )

--detail - preços listados - o menor preço primeiro
SELECT distinct top 50  
--       IsNUll(p.[ProductId],0) ProductId
--      ,IsNUll(p.[UserAgentId],0) UserAgentId
       IsNUll(p.[Sku],'') Sku
      ,p.[Price] as Price
      ,IsNUll(p.[Amount],0) Amount
      ,IsNull(p.[Measurement],'') Measurement
      ,Cast(s.[Lat] as nvarchar) + ',' + Cast(s.[Lng] as nvarchar)  LatLng
--      ,IsNull(p.[Checked],0) Checked
--      ,IsNull(p.[NotChecked],0) NotChecked
	  ,p.StoreId
	  ,s.Name as Store
	  ,Convert(char(10),p.[CreationDate],121) as CreationDateView
	  ,IsNull(p.ProductPriceId,0) ProductPriceId
	  ,Convert(char(10),p.ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),p.Deadline,121) as DeadlineView
	  , u.[Code] as UserCode
  FROM [business].[ProductPrice] p (NoLock)
  inner join [common].[UserAgent] u (NoLock)
  on u.[UserAgentId] = p.[UserAgentId]
  inner join business.Store s (NoLock) on
  s.storeId = p.storeId and s.storeId <> 0 --> Loja associada eh requisito obrigatorio...
 where @IN_Sku = p.[Sku]
 and ( @P_DateLimit is null or p.[CreationDate] >= @P_DateLimit )
 and (
	( @IN_Lat = 0 and @IN_Lng = 0 ) or  
	(( @IN_Lat between (s.Lat - @IN_Radius) and (s.Lat + @IN_Radius) ) and (@IN_Lng  between (s.Lng - @IN_Radius) and (s.Lng + @IN_Radius)) ) --localizar lojas num raio de 111,12 km
 )
 order by
 p.[Price]  asc




GO
/****** Object:  StoredProcedure [business].[ProductPriceByIdSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE procedure [business].[ProductPriceByIdSelect]
(
@IN_ProductId int
)
as

--detail - preços listados
SELECT top 10 IsNUll([ProductId],0) ProductId
      ,IsNUll([UserAgentId],0) UserAgentId
      ,IsNUll([Sku],'') Sku
      ,[Price] as Price
      ,IsNUll([Amount],0) Amount
      ,IsNull([Measurement],'') Measurement
      ,[LatLng]
      ,IsNull([Checked],0) Checked
      ,IsNull([NotChecked],0) NotChecked
      ,[CreationDate]
      ,[LastModifiedDate]
	  ,StoreId
	  ,IsNull(ProductPriceId,0) ProductPriceId
	   ,Convert(char(10),ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),Deadline,121) as DeadlineView
  FROM [business].[ProductPrice] (NoLock)
 where @IN_ProductId = ProductId

















GO
/****** Object:  StoredProcedure [business].[ProductPriceBySkuSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*

 exec [business].[ProductPriceBySkuSelect]
 @IN_Sku = '_LK8HB7YLGK4EU'
,@IN_DaysAgo = 10
,@IN_UserAgentId = 1
,@IN_Radius = 1
,@IN_Lat = 2.0
,@IN_Lng = 1.5

select * from [business].[ProductPrice] 

*/


CREATE procedure [business].[ProductPriceBySkuSelect]
( --> Mobile
 @IN_Sku nvarchar(14)
,@IN_DaysAgo smallint = 30
,@IN_UserAgentId int
,@IN_Lat decimal(13,10) = 0
,@IN_Lng decimal(13,10) = 0
,@IN_Radius decimal(13,10) = 1
)
as


declare @P_DateLimit as datetime
set @P_DateLimit = dateadd(day,(@IN_DaysAgo * -1), replace(convert(char(10),getdate(),121),'-','') )

--detail - preços listados - o menor preço primeiro
SELECT distinct top 50
       IsNUll(p.[ProductId],0) ProductId
      ,IsNUll(p.[UserAgentId],0) UserAgentId
      ,IsNUll(p.[Sku],'') Sku
      ,p.[Price] as Price
      ,IsNUll(p.[Amount],0) Amount
      ,IsNull(p.[Measurement],'') Measurement
      ,p.[LatLng]
      ,IsNull(p.[Checked],0) Checked
      ,IsNull(p.[NotChecked],0) NotChecked
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
	  ,p.StoreId
	  ,ISnULL(s.Name,'') as Store
	  ,IsNull(ProductPriceId,0) ProductPriceId
	  ,CAST(CASE WHEN @IN_UserAgentId = p.UserAgentId THEN 1 ELSE 0 END as bit) AS Editable
	  ,Convert(char(10),p.ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),p.Deadline,121) as DeadlineView
	  ,iSnuLL(s.Lat,0) Lat
 	  ,iSnULL(s.Lng,0) Lng
	  ,CAST(Lat as NVARCHAR(15) ) + ',' + CAST(Lng as NVARCHAR(15)) LatLng
  FROM [business].[ProductPrice] p  (NoLock)
  inner join business.Store s (NoLock) on
  s.storeId = p.storeId and s.storeId <> 0 --> Loja associada eh requisito obrigatorio...
 where @IN_Sku = p.[Sku]
  and ( @P_DateLimit is null or p.[CreationDate] >= @P_DateLimit )
 and (
	( @IN_Lat = 0 and @IN_Lng = 0 ) or  
	(( @IN_Lat between (s.Lat - @IN_Radius) and (s.Lat + @IN_Radius) ) and (@IN_Lng  between (s.Lng - @IN_Radius) and (s.Lng + @IN_Radius)) ) --localizar lojas num raio de 111,12 km
 )
 order by
 p.[Price]  asc










GO
/****** Object:  StoredProcedure [business].[ProductPriceDelete]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [business].[ProductPriceDelete]
(
  @IN_ProductPriceId int
 ,@IN_UserAgentId int
)
as

declare @P_sku nvarchar(15)
select top 1 @P_sku  = [Sku] from [business].[ProductPrice] (NoLock) where ProductPriceId = @IN_ProductPriceId and UserAgentId = @IN_UserAgentId

DELETE [business].[ProductPrice] WHERE  ProductPriceId = @IN_ProductPriceId and UserAgentId = @IN_UserAgentId

if ( @P_sku is not null)
begin

	if not exists (select 1 from [business].[ProductPrice] (noLock) where sku =  @P_sku )
	begin
	-- soh apaga se nao tiver preço associado e foi criado pelo mesmo usuario
	delete from [business].[Product] where UserAgentId = @IN_UserAgentId and sku =  @P_sku
	end

end








GO
/****** Object:  StoredProcedure [business].[ProductPriceDeletes]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [business].[ProductPriceDeletes]
(
  @IN_UserAgentId int
 ,@IN_ProductPriceXMLids xml
)
as

DELETE [business].[ProductPrice] 
	WHERE  UserAgentId = @IN_UserAgentId 
	and ProductPriceId in	(SELECT 
					ParamValues.id.value('.','VARCHAR(20)')
					FROM @IN_ProductPriceXMLids.nodes('/ProductPrice/id') as ParamValues(id) 
					)







GO
/****** Object:  StoredProcedure [business].[ProductPriceInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








/*

exec  [business].[ProductPriceInsert]
 @IN_UserAgentId = 1
,@IN_Sku = "_LK8HB7YLGK4EU"
,@IN_LatLng = "-23.5722218,-46.4835862"
,@IN_Price = "5656"
,@IN_Amount =  1
,@IN_Measurement = "Unid(s)"
,@IN_StoreId = 1
,@IN_Name  = NULL
,@IN_ShelfLife  = NULL
,@IN_Deadline  = NULL
,@IN_IsShoppingList = 1
,@IN_TargetName = 'PADRAO'
,@IN_ActivityKey = "f3fc480366a504761e993fc183252245LjFq9NS1Q1Du2XxWBluZpF37RM=\n"
,@IN_ERROR =0


*/

CREATE procedure [business].[ProductPriceInsert]
(
@IN_UserAgentId [int],
@IN_Sku [nvarchar](14) =  NULL,
@IN_Name [nvarchar](50) = NULL,
@IN_Price [decimal](14, 2),
@IN_Amount [int] = 1,
@IN_Measurement [nvarchar](20) = NULL,
@IN_LatLng [nvarchar](100),
@IN_Checked [int] = NULL,
@IN_NotChecked [int] = NULL,
@IN_StoreId int  = null,
@IN_ShelfLife nvarchar(25) = NULL,
@IN_Deadline nvarchar(25) = NULL,
@IN_IsShoppingList smallint = null,
@IN_TargetName nvarchar(10) = null,
@IN_ActivityKey nvarchar(100),
@IN_ERROR int output
)
as

-- CRIAR UM LIMITE DIÁRIO DE INDICAÇOES DE PREÇOS POR USUÁRIO

set @IN_ERROR = 999

if exists (select 1 from [business].[ProductPrice] (NoLock) where ActivityKey = @IN_ActivityKey )
begin
set @IN_ERROR = 500 --> Transação ja cadastrada... com mesmo ActivityKey
end
else
begin
INSERT INTO [business].[ProductPrice]
           ([UserAgentId]
           ,[Sku]
		   ,[Price]
           ,[Amount]
           ,[Measurement]
           ,[LatLng]
           ,[Checked]
           ,[NotChecked]
           ,[CreationDate]
           ,[LastModifiedDate]
		   ,StoreId
		   ,ShelfLife
		   ,Deadline
		   ,ActivityKey
		   )
     VALUES
           (@IN_UserAgentId
           ,@IN_Sku
           ,@IN_Price
           ,@IN_Amount
           ,@IN_Measurement
		   ,@IN_LatLng
           ,@IN_Checked
           ,@IN_NotChecked
           ,GetDate()
           ,GetDate()
		   ,@IN_StoreId
		   ,case when @IN_ShelfLife is null or @IN_ShelfLife = '' or @IN_ShelfLife = 'null'  then null else Convert(datetime,@IN_ShelfLife,121) end
		   ,case when @IN_Deadline is null or @IN_Deadline = ''  or @IN_Deadline = 'null' then null else Convert(datetime,@IN_Deadline,121) end
		   ,@IN_ActivityKey
		   )

-- acesso rapido a lista de produtos para os quais o usuaio informou preços
if not exists (select 1 from [business].[ProductReferenced] (NoLock) where [UserAgentId] = @IN_UserAgentId and [Sku] = @IN_Sku)
begin
INSERT INTO [business].[ProductReferenced]
           ([UserAgentId]
           ,[Sku]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[ObjectTypeId]
           ,[StatusId])
     VALUES
           (
		    @IN_UserAgentId
           ,@IN_Sku
           ,GetDate()
           ,GetDate()
           ,'DEF'
           ,'ACT'
		   )
end

-- Lista de Preços
if @IN_IsShoppingList = 1 -- Verdadeiro
begin

	if not exists (select 1 from [business].[ShoppingList] (NoLock) where UserAgentId  = @IN_UserAgentId and [TargetName]  = @IN_TargetName and  [Sku] = @IN_Sku )
	begin
	INSERT INTO [business].[ShoppingList]
			   ([UserAgentId]
			   ,[TargetName] 
			   ,[Sku]
			   ,[Amount]
			   ,[ObjectTypeId]
			   ,[StatusId]
			   ,[CreationDate]
			   ,[LastModifiedDate])
		 VALUES
			  ( @IN_UserAgentId
			   ,@IN_TargetName 
			   ,@IN_Sku
			   ,@IN_Amount
			   ,'DEF'
			   ,'ACT'
			   ,getdate()
			   ,getdate()
			  )
	end
	else
	begin

	UPDATE [business].[ShoppingList]
	   SET [Amount] = @IN_Amount
		  ,[ObjectTypeId] = 'DEF'
		  ,[StatusId] = 'ACT'
		  ,[LastModifiedDate] = getDate()
	  where UserAgentId  = @IN_UserAgentId and [TargetName] = @IN_TargetName and  [Sku] = @IN_Sku 
  
	end


end


set @IN_ERROR = 0 -- ok

end







GO
/****** Object:  StoredProcedure [business].[ProductPriceSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





/*

exec [business].[ProductPriceSelect]

*/





CREATE procedure [business].[ProductPriceSelect]
(
@IN_Sku nvarchar(14) = null,
@IN_Name nvarchar(255) = null
)
as

--detail - preços listados
SELECT top 100 IsNUll(e.[ProductId],0) ProductId
      ,IsNUll(e.[UserAgentId],0) UserAgentId
      ,IsNUll(e.[Sku],'') Sku
      ,IsNUll(t.[Name],'') Name
      ,e.[Price] as Price
      ,IsNUll(e.[Amount],0) Amount
      ,IsNull(e.[Measurement],'') Measurement
      ,e.[LatLng]
      ,IsNull(e.[Checked],0) Checked
      ,IsNull(e.[NotChecked],0) NotChecked
      ,e.[CreationDate]
      ,e.[LastModifiedDate]
	  ,e.StoreId
      ,IsNull(ProductPriceId,0) ProductPriceId
 	  ,Convert(char(10),e.ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),e.Deadline,121) as DeadlineView
  FROM [business].[ProductPrice] e (NoLock)
  left join [business].[Product] t (NoLock)
  on e.sku = t.sku
  where ( @IN_Sku is null or @IN_Sku = e.[Sku])
 and   ( @IN_Name is null or t.Name like '%@IN_Name%') 






GO
/****** Object:  StoredProcedure [business].[ProductPriceSkuNoSignSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*

exec [business].[ProductPriceSkuNoSignSelect]
@IN_Sku = '15060-08KS0000'
,@IN_Lat  = -23.5723077
,@IN_Lng = -46.4835206
,@IN_Radius = 1

*/

CREATE procedure [business].[ProductPriceSkuNoSignSelect]
(
 @IN_Sku nvarchar(14)
,@IN_Lat decimal(13,10) = 0
,@IN_Lng decimal(13,10) = 0
,@IN_Radius decimal(13,10) = 1
)
as

--detail - preços listados - o menor preço primeiro
SELECT distinct top 50
       IsNUll(p.[ProductId],0) ProductId
      ,IsNUll(p.[UserAgentId],0) UserAgentId
      ,IsNUll(p.[Sku],'') Sku
      ,p.[Price] as Price
      ,IsNUll(p.[Amount],0) Amount
      ,IsNull(p.[Measurement],'') Measurement
      ,p.[LatLng]
      ,IsNull(p.[Checked],0) Checked
      ,IsNull(p.[NotChecked],0) NotChecked
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
	  ,p.StoreId
	  ,s.Name as Store
	  ,IsNull(ProductPriceId,0) ProductPriceId
	  ,0 AS Editable
	  ,Convert(char(10),p.ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),p.Deadline,121) as DeadlineView
  FROM [business].[ProductPrice] p (NoLock)
  inner join business.Store s (NoLock) on
  s.storeId = p.storeId
 where @IN_Sku = p.[Sku]
  and (
	( @IN_Lat = 0 and @IN_Lng = 0 ) or  
	(( @IN_Lat between (s.Lat - @IN_Radius) and (s.Lat + @IN_Radius) ) and (@IN_Lng  between (s.Lng - @IN_Radius) and (s.Lng + @IN_Radius)) ) --localizar lojas num raio de 111,12 km
 )

 order by
 p.[Price]  asc







GO
/****** Object:  StoredProcedure [business].[ProductPriceStoreSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*

exec [business].[ProductPriceStoreSelect]
@IN_ProductPriceId = 17
,@IN_UserAgentId = 3


select * from business.ProductPrice


*/

CREATE procedure [business].[ProductPriceStoreSelect]
(
  @IN_ProductPriceId int
 ,@IN_UserAgentId int
)
as

--detail - preços listados - o menor preço primeiro
SELECT 
       p.ProductPriceId
      ,IsNUll(p.[ProductId],0) ProductId
      ,IsNUll(p.[UserAgentId],0) UserAgentId
      ,IsNUll(p.[Sku],'') Sku
      ,p.[Price] as Price
      ,IsNUll(p.[Amount],0) Amount
      ,IsNull(p.[Measurement],'') Measurement
      ,p.[LatLng]
      ,IsNull(p.[Checked],0) Checked
      ,IsNull(p.[NotChecked],0) NotChecked
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
	  ,p.StoreId
	  ,CAST(CASE WHEN @IN_UserAgentId = p.UserAgentId THEN 1 ELSE 0 END as bit) AS Editable
	  ,Convert(char(10),p.ShelfLife,121) as ShelfLifeView
	  ,Convert(char(10),p.Deadline,121) as DeadlineView
	  ,IsNUll(s.[Name],'') StoreName
      ,IsNUll(s.[FullName],'') FullName
      ,Cast(s.[Lat] as nvarchar) + ',' + Cast(s.[Lng] as nvarchar) LatLng
      ,IsNUll(s.[Source],'') [Source]
      ,IsNUll(s.[Phone],'') Phone
      ,IsNUll(s.[WebSite],'') WebSite
      ,IsNUll(s.[Address1],'') Address1
      ,IsNUll(s.[Number],'') Number
      ,IsNUll(s.[Address2],'') Address2
      ,IsNUll(s.[ZipCode],'') ZipCode
      ,IsNUll(s.[City],'') City
      ,IsNUll(s.[StateName],'') StateName
      ,IsNUll(s.[Country],'') Country
	  ,IsNUll(u.[Code],'') UserCode
	  ,IsNUll(d.[Name],'') ProductName
  FROM [business].[ProductPrice] p (NoLock)
  inner join business.Store s (NoLock) on
  s.storeId = p.storeId
  inner join [common].[UserAgent] u (NoLock) on
  u.UserAgentId = p.UserAgentId
    inner join [business].[Product] d (NoLock) on
  p.sku = d.sku

 where @IN_ProductPriceId = p.ProductPriceId







GO
/****** Object:  StoredProcedure [business].[ProductSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*

exec [business].[ProductSelect]
@IN_Name = "7891"



*/

CREATE procedure [business].[ProductSelect]
(
 @IN_Sku nvarchar(14) = null
,@IN_Name nvarchar(255) = null
)
as

SELECT top 10
       p.[ProductId]
       ,p.[Sku]
      ,IsNull(p.[Name],'') Name
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
      ,p.[Source]
      ,p.[ObjectTypeId]
      ,p.[StatusId]
      ,IsNUll(p.[Details],'') Details
      ,IsNUll(p.[Picture],0) Picture
	  ,IsNull(p.UserAgentId,0) UserAgentId
	  ,IsNUll(f.VirtualPath,'') VirtualPath
  FROM [business].[Product] p (NoLock)
  left join [business].[ProductFile] f (NoLock)
  on f.[ProductId] = p.[ProductId]
where 
    (@IN_Sku is null or @IN_Sku = p.[Sku] )
and ( (@IN_Name is null or p.Name like '%' + @IN_Name + '%' )
      or (@IN_Name is null or p.Sku like '%' + @IN_Name + '%' ) -- se informar o sku no nome tambem vale para pesquisa
    )
order by p.ProductId desc










GO
/****** Object:  StoredProcedure [business].[ProductTempInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*

exec [business].[ProductTempInsert]
 @IN_Sku= '1Ce1X1'
,@IN_Name  = 'teste'
,@IN_Details = null
,@IN_Picture = null
,@IN_Source = 'USER'
,@IN_ObjectTypeId  = 'DEF'
,@IN_StatusId = 'ACT'
,@IN_UserAgentId  = null
,@IN_SupplyChainCode  = 'CHURAS'



declare @P_ProductId int
select top 1 @P_ProductId = ProductId from [business].[Product] where [Sku] ='1Ce1X1'
PRINT @P_ProductId


declare @p_SupplyChainId int
	select @p_SupplyChainId = [SupplyChainId] from [business].[SupplyChain] where [Code] =  'CHURAS'
	PRINT  @p_SupplyChainId 

*/



CREATE procedure [business].[ProductTempInsert]
(
 @IN_Sku nvarchar(14)
,@IN_Name nvarchar(50) = null
,@IN_Details nvarchar(1024) = null
,@IN_Picture varbinary(max) = null
,@IN_Source nvarchar(5) = 'USER'
,@IN_ObjectTypeId nvarchar(5) = 'DEF'
,@IN_StatusId nvarchar(5) = 'ACT'
,@IN_UserAgentId int = null
,@IN_SupplyChainCode nvarchar(6) = null
)
as

	declare @P_ProductId int
	-- NOTA: Caso o sku ja exista na base, então somente o usuário que fez o cadastro pode altra-o... ou o adminstrador do sistema por outros meios, obviamente...
if not exists (select 1 from [business].[Product] (NoLock) where [Sku] = @IN_Sku)
begin

INSERT INTO [business].[Product]
           ([Sku]
           ,[Name]
           ,[Details]
           ,[Picture]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[Source]
           ,[ObjectTypeId]
           ,[StatusId]
		   ,[UserAgentId])
     VALUES
           (@IN_Sku
           ,@IN_Name
           ,@IN_Details
           ,@IN_Picture
           ,getdate()
           ,getdate()
           ,@IN_Source
           ,@IN_ObjectTypeId
           ,@IN_StatusId
		   ,@IN_UserAgentId
		   )
		   Set @P_ProductId = Scope_Identity();

  if ( @IN_SupplyChainCode  is not null)
  begin
		select top 1 @P_ProductId = ProductId from [business].[Product] where [Sku] = @IN_Sku
		declare @p_SupplyChainId int
		select @p_SupplyChainId = [SupplyChainId] from [business].[SupplyChain] where [Code] =  @IN_SupplyChainCode 

		if ( @p_SupplyChainId is not null)
		begin
			if not exists (select 1 from [business].ProductXSupplyChain (NoLock) where [ProductId] = @P_ProductId and [SupplyChainId]= @p_SupplyChainId)
			begin

				INSERT INTO [business].[ProductXSupplyChain]
						   ([ProductId]
						   ,[SupplyChainId]
						   ,[ObjectTypeId]
						   ,[StatusId]
						   ,[LevelOfNeed])
					 VALUES
						   ( @P_ProductId
							,@p_SupplyChainId
							,'DEF'
							,'ACT'
							,'0'
						   )
			end
		end

  end

  end

/*
[TODO] Quando o aplicativo for disponibilizado ao publico tem que gravar
primeiro na tabela temp, para que as fotos sejam liberados por um moderador...

INSERT INTO [business].[ProductTemp]
           (
		    [Sku]
           ,[Name]
           ,[Details]
           ,[Picture]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[Source]
           ,[ObjectTypeId]
           ,[StatusId]
		   ,[UserAgentId]
		   )
     VALUES
           (
		    @IN_Sku
           ,@IN_Name
           ,@IN_Details
           ,@IN_Picture
           ,getdate()
           ,getdate()
           ,@IN_Source
           ,@IN_ObjectTypeId
           ,@IN_StatusId
		   ,@IN_UserAgentId
		   )

*/










GO
/****** Object:  StoredProcedure [business].[ProductUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE procedure [business].[ProductUpdate]
(
 @IN_Sku nvarchar(14)
,@IN_Name nvarchar(50) = null
,@IN_Details nvarchar(1024) = null
,@IN_Picture varbinary(max) = null
,@IN_SupplyChainCodeXml xml = null
,@IN_UserAgentId int = null
)
as
begin tran

if exists 
(select 1 from  [business].[Product] (NoLock) p 
 WHERE p.[Sku] = @IN_Sku and p.UserAgentId =  @IN_UserAgentId
 )
begin 

update p 
set   p.[Name] = @IN_Name
     ,p.[Details] = @IN_Details
     ,p.[Picture] = @IN_Picture
from  business.Product  p
inner join [common].[UserAgent] u (NoLock) on u.[UserAgentId] = p.[UserAgentId]
       WHERE p.[Sku] = @IN_Sku and p.UserAgentId =  @IN_UserAgentId


  if (@IN_SupplyChainCodeXml is not null)
  begin

  declare @P_ProductId int		
  select @P_ProductId = ProductId from [business].[Product] (NoLock) p  where p.[Sku] = @IN_Sku

  delete [business].[ProductXSupplyChain] where  [ProductId] = @P_ProductId 

	INSERT INTO [business].[ProductXSupplyChain] 
           ([ProductId]
           ,[SupplyChainId]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[LevelOfNeed]
	 )
SELECT 
 @P_ProductId
,ParamValues.id.value('.','VARCHAR(20)')
,'DEF'
,'ACT'
,'0'
FROM @IN_SupplyChainCodeXml.nodes('/SupplyChain/id') as ParamValues(id) 

end

end

if @@ERROR <>  0 goto fin

fin:
if @@ERROR <> 0
 begin
  rollback tran
 end
else
 begin
  commit tran
 end







GO
/****** Object:  StoredProcedure [business].[ScoreSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [business].[ScoreSelect]
(
@IN_UserAgentId int = 0
)
as

SELECT [UserAgentId]
      ,[Store]
      ,[Product]
      ,[ProductPrice]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[CreationDate]
      ,[LastModifiedDate]
  FROM [business].[Score] (NoLock)
  --where [UserAgentId] = @IN_UserAgentId






GO
/****** Object:  StoredProcedure [business].[ScoreUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*

exec  [business].[ScoreUpdate]

select * from  [business].[Score] 


*/

CREATE procedure [business].[ScoreUpdate]
as

declare @P_Store int
declare @p_Product int
declare @p_ProductPrice int


select @P_Store = count(*) from [business].[Store] (NoLock)

select @p_Product = count(*) from [business].[Product] (NoLock)

select @p_ProductPrice = count(*) from [business].[ProductPrice] (NoLock)


if not exists (select 1 from [business].[Score] (NoLock) where [UserAgentId] = 0 )
begin
	INSERT INTO [business].[Score]
           (
		   [UserAgentId]
           ,[Store]
           ,[Product]
           ,[ProductPrice]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[CreationDate]
           ,[LastModifiedDate]
		   )
     VALUES
           (
		   0
           ,isNull(@P_Store,0)
           ,isNull(@p_Product,0) 
           ,isNull(@p_ProductPrice,0) 
           ,'DEF'
           ,'ACT'
           ,GETDATE()
           ,GETDATE()
		   )
end
else
begin

	UPDATE [business].[Score]
	   SET [Store] = @P_Store
		  ,[Product] = @p_Product 
		  ,[ProductPrice] = @p_ProductPrice 
		  ,[LastModifiedDate] = GETDATE()
	 WHERE [UserAgentId]  = 0
 
 end








GO
/****** Object:  StoredProcedure [business].[ShoppingListDelete]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [business].[ShoppingListDelete]
(
 @IN_ListId int
,@IN_UserAgentId int
,@IN_ERROR int output
)
as

set @IN_ERROR = 0


if exists (select 1 from [business].[ShoppingList] (NoLock)  where [ListId] = @IN_ListId and UserAgentId = @IN_UserAgentId)
begin
	DELETE FROM [business].[ShoppingList]
		  WHERE [ListId] = @IN_ListId and UserAgentId = @IN_UserAgentId
end
else
begin
	set @IN_ERROR = 1 -- Item não cadastrado....
end



GO
/****** Object:  StoredProcedure [business].[ShoppingListIorU]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [business].[ShoppingListIorU]
(
 @IN_UserAgentId int
,@IN_TargetName nvarchar(10)
,@IN_Sku nvarchar(15)
,@IN_Amount int
,@IN_ObjectTypeId nvarchar(5)
,@IN_StatusId nvarchar(5)
)
as

if not exists (select 1 from [business].[ShoppingList] (NoLock) where UserAgentId  = @IN_UserAgentId and [TargetName]  = @IN_TargetName and  [Sku] = @IN_Sku )
begin
INSERT INTO [business].[ShoppingList]
           ([UserAgentId]
		   ,[TargetName] 
           ,[Sku]
           ,[Amount]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[CreationDate]
           ,[LastModifiedDate])
     VALUES
          ( @IN_UserAgentId
		   ,@IN_TargetName 
           ,@IN_Sku
           ,@IN_Amount
           ,@IN_ObjectTypeId
           ,@IN_StatusId
           ,getdate()
           ,getdate()
		  )
end
else
begin

UPDATE [business].[ShoppingList]
   SET [Amount] = @IN_Amount
      ,[ObjectTypeId] = @IN_ObjectTypeId
      ,[StatusId] = @IN_StatusId
      ,[LastModifiedDate] = getDate()
  where UserAgentId  = @IN_UserAgentId and [TargetName] = @IN_TargetName and  [Sku] = @IN_Sku 
  
end






GO
/****** Object:  StoredProcedure [business].[ShoppingListSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*

exec business.ShoppingListSelect
@IN_UserAgentId = 1

*/

CREATE procedure [business].[ShoppingListSelect]
(
@IN_UserAgentId int
)
as

SELECT s.[ListId]
      ,s.[UserAgentId]
	  ,s.[TargetName] 
      ,s.[Sku]
	  ,IsNull(p.[Name],'Produto') as ProductName
      ,s.[Amount]
      ,s.[ObjectTypeId]
      ,s.[StatusId]
	  ,Convert(char(20),s.CreationDate,121) as CreationDateView
      ,Convert(char(20),s.LastModifiedDate,121) as LastModifiedDateView
  FROM [business].[ShoppingList] s (NoLock)
  left join [business].[Product] p (NoLock)
  on s.[Sku] = p.[Sku]
  where s.[UserAgentId] = @IN_UserAgentId









GO
/****** Object:  StoredProcedure [business].[StoreByIdSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








/*

exec [business].[StoreSelect]
@IN_LatLng= '-23.5721857,-46.4835441'

*/

CREATE procedure [business].[StoreByIdSelect]
(
 @IN_StoreId int
)
as

SELECT s.[StoreId]
      ,s.[Name]
      ,s.[Source]
      ,s.[ObjectTypeId]
      ,s.[StatusId]
      ,s.[LastModifiedDate]
      ,s.[CreationDate]
      ,s.[UserAgentId]
      ,s.[DDI]
      ,s.[DDD]
      ,s.[Phone]
      ,s.[WebSite]
      ,s.[Address1]
      ,s.[Number]
      ,s.[Address2]
      ,s.[ZipCode]
      ,s.[City]
      ,s.[StateName]
      ,s.[Country]
	  ,s.ActivityKey
	  ,[Lat]
	  ,[Lng]
	  , (Cast(Lat as nvarchar(15)) + ',' +  cast(Lng as nvarchar(15))) LatLng
	  ,(case when c.[StoreId] is null then 0 else 1 end) Complained
  FROM [business].[Store] s (NoLock)
    left join business.Complaint c (NoLock)
  on s.[StoreId] = c.[StoreId]
  where s.[StoreId]  = @IN_StoreId

















GO
/****** Object:  StoredProcedure [business].[StoreByUserSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







/*

exec [business].[StoreByUserSelect]
 @IN_UserAgentId = 1

*/

CREATE procedure [business].[StoreByUserSelect]
(
 @IN_UserAgentId int
)
as

SELECT [StoreId]
      ,[Name]
      ,[Source]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[LastModifiedDate]
      ,[CreationDate]
      ,[UserAgentId]
      ,[DDI]
      ,[DDD]
      ,[Phone]
      ,[WebSite]
      ,[Address1]
      ,[Number]
      ,[Address2]
      ,[ZipCode]
      ,[City]
      ,[StateName]
      ,[Country]
	  ,ActivityKey
	  ,[Lat]
	  ,[Lng]
	  , (Cast(Lat as nvarchar(15)) + ',' +  cast(Lng as nvarchar(15))) LatLng
  FROM [business].[Store] (NoLock)
  where 
   UserAgentId = @IN_UserAgentId
















GO
/****** Object:  StoredProcedure [business].[StoreInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*

EXEC [business].[StoreInsert]
 @IN_Name ='ADASSSSSDXX'
,@IN_Source ='USER'
,@IN_StoreId = 0
,@IN_UserAgentId = 11
,@IN_Lat = 100.1
,@IN_Lng = 310.1

 p.Add("@IN_StoreId", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                            p.Add("@IN_Name", productPriceType.Store);
                            p.Add("@IN_UserAgentId", productPriceType.UserAgentId);
                            p.Add("@IN_Source", "USER");
                            p.Add("@IN_ObjectTypeId", "DEF");
                            p.Add("@IN_StatusId", "ACT");
                            p.Add("@IN_Lat", lat);
                            p.Add("@IN_Lng", lng);

SELECT * FROM  [business].[Store] 


*/






CREATE procedure [business].[StoreInsert]
(
 @IN_Name nvarchar(50)
,@IN_Source nvarchar(5)
,@IN_ObjectTypeId nvarchar(5) = 'DEF'
,@IN_StatusId nvarchar(5) = 'ACT'
,@IN_StoreId int out
,@IN_UserAgentId int = null
,@IN_DDI nvarchar(3) = NULL
,@IN_DDD nvarchar(4) = NULL
,@IN_Phone nvarchar(20) = NULL
,@IN_WebSite nvarchar(255) = NULL
,@IN_Address1 nvarchar(50) = NULL
,@IN_Number nvarchar(5) = NULL
,@IN_Address2 nvarchar(50) = NULL
,@IN_ZipCode nvarchar(10) = NULL
,@IN_City nvarchar(50) = NULL
,@IN_StateName nvarchar(50) = NULL
,@IN_Country nvarchar(50) = NULL
,@IN_ActivityKey nvarchar(50) = NULL
,@IN_Lat decimal(13, 10) = 0
,@IN_Lng decimal(13, 10) = 0
)
as

/*

?Decide ser trata-se de uma loja ja cadastrada pelas semelhanças de nome e geo posicionamento
ou de um estabelecimento novo nesta base de dados...

*/

-- como verificar se duas coordenadas de geoposicionamento são proximas
if not exists (select 1 from [business].[Store] (NoLock) where [Name] = lTrim(rTrim(@IN_Name)) and UserAgentId = @IN_UserAgentId )
begin
INSERT INTO [business].[Store]
           ([Name]
           ,[Source]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[LastModifiedDate]
           ,[CreationDate]
		   ,UserAgentId
		   ,DDI
			,DDD
			,Phone
			,WebSite
			,Address1
			,Number
			,Address2
			,ZipCode
			,City
			,StateName
			,Country
			,ActivityKey
			,Lat
			,Lng
			)
     VALUES
(
 @IN_Name
,@IN_Source
,@IN_ObjectTypeId
,@IN_StatusId
,GETDATE()
,GETDATE()
,@IN_UserAgentId
,@IN_DDI 
,@IN_DDD 
,@IN_Phone 
,@IN_WebSite 
,@IN_Address1 
,@IN_Number 
,@IN_Address2 
,@IN_ZipCode
,@IN_City 
,@IN_StateName 
,@IN_Country
,@IN_ActivityKey
,@IN_Lat
,@IN_Lng
)

set @IN_StoreId = Scope_Identity();

end

else

begin
	select @IN_StoreId = storeId from [business].[Store] (NoLock) where [Name] = lTrim(rTrim(@IN_Name)) and UserAgentId = @IN_UserAgentId 
end



SELECT  @IN_StoreId










GO
/****** Object:  StoredProcedure [business].[StoreInsertOrUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE procedure [business].[StoreInsertOrUpdate]
(
 @IN_StoreId int out
,@IN_FullName nvarchar(50)
,@IN_Name nvarchar(50)
,@IN_Source nvarchar(5)
,@IN_ObjectTypeId nvarchar(5) = 'DEF'
,@IN_StatusId nvarchar(5) = 'ACT'
,@IN_UserAgentId int = null
,@IN_DDI nvarchar(3) = NULL
,@IN_DDD nvarchar(4) = NULL
,@IN_Phone nvarchar(20) = NULL
,@IN_WebSite nvarchar(255) = NULL
,@IN_Address1 nvarchar(50) = NULL
,@IN_Number nvarchar(5) = NULL
,@IN_Address2 nvarchar(50) = NULL
,@IN_ZipCode nvarchar(10) = NULL
,@IN_City nvarchar(50) = NULL
,@IN_StateName nvarchar(50) = NULL
,@IN_Country nvarchar(50) = NULL
,@IN_ActivityKey nvarchar(50) = NULL
,@IN_Lat decimal(13, 10)
,@IN_Lng decimal(13, 10)
)
as

if ( @IN_storeId  is null) or (@IN_storeId  = 0 ) or (not exists (select 1 from [business].[Store] (NoLock) where  storeId =  @IN_storeId ))
begin
INSERT INTO [business].[Store]
           ( 
		     Name
			,[FullName]
            ,Source
            ,ObjectTypeId
            ,StatusId
            ,LastModifiedDate
            ,CreationDate
		    ,UserAgentId
		    ,DDI
			,DDD
			,Phone
			,WebSite
			,Address1
			,Number
			,Address2
			,ZipCode
			,City
			,StateName
			,Country
			,ActivityKey
			,Lat
			,Lng
			)
     VALUES
(
 @IN_Name
,@IN_FullName
,@IN_Source
,@IN_ObjectTypeId
,@IN_StatusId
,GETDATE()
,GETDATE()
,@IN_UserAgentId
,@IN_DDI 
,@IN_DDD 
,@IN_Phone 
,@IN_WebSite 
,@IN_Address1 
,@IN_Number 
,@IN_Address2 
,@IN_ZipCode
,@IN_City 
,@IN_StateName 
,@IN_Country
,@IN_ActivityKey
,@IN_Lat
,@IN_Lng
)

set @IN_StoreId = Scope_Identity();

end

else

begin

UPDATE [business].[Store]
   SET [Name] = @IN_Name
      ,[FullName] = @IN_FullName
      --,[Source] = @IN_Source -> Update não deveria alterar o tipo de provedor original de dados da loja
      ,[ObjectTypeId] = @IN_ObjectTypeId
      ,[StatusId] = @IN_StatusId
      ,[LastModifiedDate] = getdate()
      ,[UserAgentId] = @IN_UserAgentId
	  ,[DDI] = @IN_DDI
      ,[DDD] = @IN_DDD
      ,[Phone] = @IN_Phone
      ,[WebSite] = @IN_WebSite
      ,[Address1] = @IN_Address1
      ,[Number] = @IN_Number
      ,[Address2] = @IN_Address2
      ,[ZipCode] = @IN_ZipCode
      ,[City] = @IN_City
      ,[StateName] = @IN_StateName
      ,[Country] = @IN_Country
      ,[ActivityKey] = @IN_ActivityKey
	  ,[Lat] = @IN_Lat
	  ,[Lng] = @IN_Lng
 WHERE storeId =  @IN_storeId
 
end






GO
/****** Object:  StoredProcedure [business].[StoreSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*

exec [business].[StoreSelect]
@IN_LatLng= '-23.5721857,-46.4835441'

*/

CREATE procedure [business].[StoreSelect]
(
 @IN_Name nvarchar(50) = null
,@IN_Source nvarchar(5) = null
,@IN_ObjectTypeId nvarchar(5) = 'DEF'
,@IN_StatusId nvarchar(5) = 'ACT'
)
as

SELECT [StoreId]
      ,[Name]
      ,[Source]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[LastModifiedDate]
      ,[CreationDate]
      ,[UserAgentId]
      ,[DDI]
      ,[DDD]
      ,[Phone]
      ,[WebSite]
      ,[Address1]
      ,[Number]
      ,[Address2]
      ,[ZipCode]
      ,[City]
      ,[StateName]
      ,[Country]
	  ,ActivityKey
	  ,[Lat]
	  ,[Lng]
	  , (Cast(Lat as nvarchar(15)) + ',' +  cast(Lng as nvarchar(15))) LatLng
  FROM [business].[Store] (NoLock)
  where 
    ( @IN_Name is null or Name like '%' + @IN_Name + '%' )
--Precisa criar uma função que retorna um indice de proximidade aceitavel por geo posicionamento
and ( @IN_Source is null or @IN_Source = [Source] )
and ( @IN_ObjectTypeId is null or  @IN_ObjectTypeId =  ObjectTypeId )
and ( @IN_StatusId is null or @IN_StatusId = StatusId )






GO
/****** Object:  StoredProcedure [business].[StoreUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [business].[StoreUpdate]
(
 @IN_Name nvarchar(50) = null
,@IN_LatLng nvarchar(50) = null
,@IN_Source nvarchar(5) = null
,@IN_ObjectTypeId nvarchar(5) = 'DEF'
,@IN_StatusId nvarchar(5) = 'ACT'
,@IN_UserAgentId int = null
,@IN_DDI nvarchar(3) = NULL
,@IN_DDD nvarchar(4) = NULL
,@IN_Phone nvarchar(10) = NULL
,@IN_WebSite nvarchar(255) = NULL
,@IN_Address1 nvarchar(50) = NULL
,@IN_Number nvarchar(5) = NULL
,@IN_Address2 nvarchar(50) = NULL
,@IN_ZipCode nvarchar(10) = NULL
,@IN_City nvarchar(50) = NULL
,@IN_StateName nvarchar(50) = NULL
,@IN_Country nvarchar(50) = NULL
,@IN_ActivityKey nvarchar(50)
)
as

if  exists (select 1 from [business].[Store] (NoLock) where ActivityKey = lTrim(rTrim(@IN_ActivityKey)) )
begin
UPDATE [business].[Store]
   SET [Name] = @IN_Name
      ,[Source] = @IN_Source
      ,[ObjectTypeId] = @IN_ObjectTypeId
      ,[StatusId] = @IN_StatusId
      ,[LastModifiedDate] = getdate()
      ,[UserAgentId] = @IN_UserAgentId
	  ,[DDI] = @IN_DDI
      ,[DDD] = @IN_DDD
      ,[Phone] = @IN_Phone
      ,[WebSite] = @IN_WebSite
      ,[Address1] = @IN_Address1
      ,[Number] = @IN_Number
      ,[Address2] = @IN_Address2
      ,[ZipCode] = @IN_ZipCode
      ,[City] = @IN_City
	  ,[StateName] = @IN_StateName
      ,[Country] = @IN_Country
      ,[ActivityKey] = @IN_ActivityKey
 WHERE  ActivityKey = lTrim(rTrim(@IN_ActivityKey))

 end





GO
/****** Object:  StoredProcedure [business].[SupplyChainSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [business].[SupplyChainSelect]
(
 @IN_Category nvarchar(10) = null
)
as

SELECT [SupplyChainId]
      ,[Code]
      ,[Name]
      ,[CreationDate]
      ,[LastModifiedDate]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[Category]
  FROM [business].[SupplyChain] (NoLock)
  where
  ( (@IN_Category is null) or  (@IN_Category = '') or ([Category] =  @IN_Category ) )







GO
/****** Object:  StoredProcedure [business].[SupplyProductSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











/*

exec [business].[SupplyProductSelect]
@IN_Code = ''


*/

CREATE procedure [business].[SupplyProductSelect]
(
 @IN_Code nvarchar(6)= null
)
as

/*
[TODO] Esta rotina pode não ser assim tão performática
Somente lista produtos com pelo menos um preço cadastrado
 -- Precisa fazer um procedimento para limpar a base - retirar produtos sem preço
*/

SELECT distinct TOP 100 
       p.[ProductId]
       ,p.[Sku]
      ,IsNull(p.[Name],'') Name
      ,p.[CreationDate]
      ,p.[LastModifiedDate]
      ,p.[Source]
      ,p.[ObjectTypeId]
      ,p.[StatusId]
      ,IsNUll(p.[Details],'') Details
      ,IsNUll(p.[Picture],0) Picture
	  ,(case when c.[ProductId] is null then 0 else 1 end) Complained
	  ,IsNUll(f.VirtualPath,'') VirtualPath
  FROM [business].[Product] p (NoLock)
  inner join [business].[ProductPrice] i (NoLock) on
  i.[sku] = p.[sku]
  left join [business].[ProductXSupplyChain] x on
  x.[ProductId] = p.[ProductId]
  left join [business].[SupplyChain] s on
  s.[SupplyChainId] = x.[SupplyChainId]
  left join business.Complaint c (NoLock)  
  on p.[ProductId] = c.[ProductId]
  left join [business].[ProductFile] f (NoLock)
  on f.[ProductId] = p.[ProductId]
where 
  ( (@IN_Code is null  or @IN_Code ='' and isNull(s.Code,'OUTROS') <> 'TESTE') or (@IN_Code = s.Code) or s.Code = 'TOP' )
  
order by IsNull(p.[Name],'')
  











GO
/****** Object:  StoredProcedure [common].[EmailSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*

exec [common].[EmailSelect]
@IN_StatusId = 'act'

*/

CREATE procedure [common].[EmailSelect]
(
@IN_StatusId nvarchar(5)
)
as

SELECT top 100
       [EmailId]
      ,[ToEmail]
      ,[FromEmail]
      ,[Subject]
      ,[Body]
      ,[CreationDate]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[ReferenceCode]
      ,[LastModifiedDate]
  FROM [common].[Email] (NoLock)
  where @IN_StatusId = [StatusId]
  order by [CreationDate]





GO
/****** Object:  StoredProcedure [common].[EmailStatusUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [common].[EmailStatusUpdate]
(
 @IN_EmailId int
,@IN_StatusId nvarchar(5)
)
as

UPDATE [common].[Email]
   SET [StatusId] = @IN_StatusId
   ,[LastModifiedDate] = getdate()
 WHERE EmailId = @IN_EmailId 






GO
/****** Object:  StoredProcedure [common].[IdentityExternalInsertOrUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
--> Quando o usuário se logar com a conta google pela primeira vez ira gravar o email e a senha
-- vai manter o status de pendente de token
-- ACT PEND DEL TOKEN

Vai criar o registro inicial do usuário ou alterar a senha

p.Add("@@IN_ActivityKey", activityKey);
p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);

exec common.IdentityExternalInsertOrUpdate
 @IN_Email = 'teste@teste.com.br'
,@IN_PasswordNew = '123'
,@IN_Code = '0123456789'
,@IN_Name = 'Teste'
,@IN_ActivityKey  = 'sdasdassdadada'
,@IN_ERROR = 0


                            p.Add("@IN_Email", e);
                            p.Add("@IN_PasswordNew", accessKey);
                            p.Add("@IN_Code", activityKey.Substring(0, 10));
                            p.Add("@IN_ActivityKey", activityKey);
                            p.Add("@IN_ERROR", dbType: DbType.Int32, direction: ParameterDirection.Output);

select * from [common].[UserAgent]

*/

CREATE procedure [common].[IdentityExternalInsertOrUpdate]
(
 @IN_Email nvarchar(50)
,@IN_PasswordNew nvarchar(50)
,@IN_Code nvarchar(25)
,@IN_Name nvarchar(50)
,@IN_ActivityKey  nvarchar(50)
,@IN_ERROR int out 
)
as

if not exists ( SELECT 1 FROM [common].[UserAgent] (NoLock) where [Email] = @IN_Email) 
 begin

 	INSERT INTO [common].[UserAgent]
			   ([Code]
			   ,[Password]
			   ,[Name]
			   ,[PhoneDDI]
			   ,[PhoneDDDNumber]
			   ,[Email]
			   ,[ObjectTypeId]
			   ,[StatusId]
			   ,[CreationDate]
			   ,[LastModifiedDate]
			   ,[ActivityKey]
			   ,[UserLanguageCode])
		 VALUES
			   (@IN_Code
			   ,@IN_PasswordNew 
			   ,@IN_Name
			   ,null
			   ,null
			   ,@IN_Email
			   ,'DEF'
			   ,'PEND'
			   ,getdate()
			   ,getdate()
			   ,@IN_ActivityKey
			   ,null
			   )

 end
else
 begin
	update [common].[UserAgent]
	set 
		 [Password] = @IN_PasswordNew
		,LastModifiedDate = getdate()
		,ObjectTypeId = 'DEF'
		,StatusId = 'PEND'
	where Email= @IN_Email

 end



GO
/****** Object:  StoredProcedure [common].[p_Email_i]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [common].[p_Email_i]
(
 @IN_ToEmail nvarchar(256)
,@IN_FromEmail nvarchar(256)
,@IN_Subject nvarchar(256)
,@IN_Body text
,@IN_ObjectTypeId nvarchar(5)
,@IN_StatusId nvarchar(5)
,@IN_ReferenceCode nvarchar(256)
)
as

INSERT INTO [common].[Email]
		   (
		    [ToEmail]
		   ,[FromEmail]
		   ,[Subject]
		   ,[Body]
		   ,[CreationDate]
		   ,[ObjectTypeId]
		   ,[StatusId]
		   ,[ReferenceCode]
		   ,LastModifiedDate
		   )
	 VALUES
		   (
			@IN_ToEmail
		   ,@IN_FromEmail
		   ,@IN_Subject
		   ,@IN_Body
		   ,getdate()
		   ,@IN_ObjectTypeId
		   ,@IN_StatusId
		   ,@IN_ReferenceCode
		   ,Getdate()
		   ) 











GO
/****** Object:  StoredProcedure [common].[p_EmailTemplate_s]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/*
Author: Laerte do Nascimento
Date: 2014/05/15
Subject: Listar Email Tamplate


Sample:

exec common.p_EmailTemplate_s
 @IN_EmailTemplateId = 1
,@IN_LanguageId = 2

*/

CREATE procedure [common].[p_EmailTemplate_s]
(
 @IN_EmailTemplateId int
,@IN_LanguageId smallint = 2
)
as

SELECT  e.EmailTemplateId 
      , e.LanguageId 
      , e.EmailSubejct 
      , e.EmailMessage 
      , Convert(char(10),e.CreationDate,121) as CreationDate 
      , e.ObjectTypeId 
      , e.StatusId 
      , Convert(char(10),e.LastModifiedDate,121) as LastModifiedDate
      , l.LanguageCode
      , l.LanguageName 
  FROM  common.EmailTemplate  e (NoLock)
  left join common.Language l  (NoLock)
  on l.LanguageId = e.LanguageId and e.LanguageId = @IN_LanguageId
where
e.EmailTemplateId = @IN_EmailTemplateId





GO
/****** Object:  StoredProcedure [common].[p_EmailXml_i]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Author: Laerte do Nascimento
Date: 2014/05/15 
Subject: Inserir email enviado



select * from common.Email


exec  [common].[p_EmailXml_i]
@IN_EmailXml='
<Email><Item><ToEmail>laerte21@gmail.com</ToEmail><FromEmail>true</FromEmail><Subject>Atvação</Subject><Body>Click na url para ativar a conta: UserAgent/CatchResetPassword?email=laerte21@gmail.com?key=158a8fd1-9148-4c1c-8875-7d3456a89e0c_1a0d695e-7133-4787-b34f-68b7b9229dd3 Obrigado</Body><ObjectTypeId>DEF</ObjectTypeId><StatusId>ACT</StatusId><ReferenceCode></ReferenceCode></Item></Email>'



*/
CREATE procedure [common].[p_EmailXml_i]
(
@IN_EmailXml xml
)
as


INSERT INTO common.Email
		   (
		    ToEmail
		   ,FromEmail
		   ,Subject
		   ,Body
		   ,CreationDate
		   ,ObjectTypeId
		   ,StatusId
		   ,ReferenceCode
		   ,LastModifiedDate
		   )
		select
		 MI.value('ToEmail[1]','NVARCHAR(256)') as ToEmail
		,MI.value('FromEmail[1]','NVARCHAR(256)') as FromEmail
		,MI.value('Subject[1]','NVARCHAR(256)') as Subject
		,MI.value('Body[1]','NVARCHAR(1024)') as Body
		,GETDATE()
		,MI.value('ObjectTypeId[1]','NVARCHAR(5)') as ObjectTypeId
		,MI.value('StatusId[1]','NVARCHAR(5)') as StatusId
		,MI.value('ReferenceCode[1]','NVARCHAR(256)') as ReferenceCode
		,GETDATE()
		FROM @IN_EmailXml.nodes('/Email/Item') as ParamValues(MI)		   






GO
/****** Object:  StoredProcedure [common].[TokenNotSentSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

exec [common].[TokenNotSentSelect]


*/

CREATE procedure  [common].[TokenNotSentSelect]
as

SELECT
	   [Email]
      ,[Token]
	  ,[Id]
     ,CONVERT(nvarchar,t.[DeadlineDate],121) DeadlineDate
  FROM [common].[UserEmailToken] t (NoLock)
  where 
	    t.[StatusId] = 'PEND'
	and t.DeadlineDate > GetDate()
order by [DeadlineDate] desc





GO
/****** Object:  StoredProcedure [common].[UserAgentActivity]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [common].[UserAgentActivity]
(
 @IN_ActivityKey  nvarchar(50)
)
as

if not exists (select 1 from [common].[UserAgent] (NoLock) where ActivityKey = @IN_ActivityKey )
begin
INSERT INTO [common].[UserAgent]
           ([Code]
           ,[Password]
           ,[Name]
           ,[PhoneDDI]
           ,[PhoneDDDNumber]
           ,[Email]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[ActivityKey]
           ,[UserLanguageCode])
		   select top 1
            [Code]
           ,[Password]
           ,[Name]
           ,[PhoneDDI]
           ,[PhoneDDDNumber]
           ,[Email]
           ,[ObjectTypeId]
           ,[StatusId]
           ,getdate()
           ,getdate()
           ,[ActivityKey]
           ,[UserLanguageCode]
		   from [common].[UserAgentTemp] (NoLock) where ActivityKey = @IN_ActivityKey 
		   ---
		   delete  from [common].[UserAgentTemp]  where ActivityKey = @IN_ActivityKey 
end






GO
/****** Object:  StoredProcedure [common].[UserAgentChangePw]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [common].[UserAgentChangePw]
(
 @IN_UserAgentId int
,@IN_PasswordNew nvarchar(50)
)
as

update [common].[UserAgent]
set 
  [Password] = @IN_PasswordNew
 ,[LastModifiedDate] = getdate()
where [UserAgentId] = @IN_UserAgentId








GO
/****** Object:  StoredProcedure [common].[UserAgentSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [common].[UserAgentSelect]
(
@IN_CodeOrEmail nvarchar(50),
@IN_Password nvarchar(50)
)
as

SELECT [Code]
      ,[Name]
      ,[PhoneDDI]
      ,[PhoneDDDNumber]
      ,[Email]
      ,[ObjectTypeId]
      ,[StatusId]
      ,[LastModifiedDate]
      ,[UserLanguageCode]
  FROM [common].[UserAgent] (NoLock)
where	( @IN_CodeOrEmail = [Code] or @IN_CodeOrEmail = [Email] )
		and [Password] = @IN_Password






GO
/****** Object:  StoredProcedure [common].[UserAgentTempInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE procedure [common].[UserAgentTempInsert]
(
 @IN_Code nvarchar(25)
,@IN_Name nvarchar(50)
,@IN_PhoneDDI nvarchar(3)
,@IN_PhoneDDDNumber nvarchar(15)
,@IN_Email nvarchar(50)
,@IN_ObjectTypeId nvarchar(5)
,@IN_StatusId nvarchar(5)
,@IN_ActivityKey  nvarchar(50)
,@IN_UserLanguageCode nvarchar(6)
,@IN_ERROR int out 
)
as


/*
[TODO] Deve inserir na tabela temporária, mas emergialmente vai inserir direto na tabela definitiva

*/

SET @IN_ERROR = 0 -- Inicialização

if exists(select 1 from [common].[UserAgent] (NoLock) where Code = @IN_Code)
	begin
		select @IN_ERROR = 101 --> UserCode ja cadastrado
	end
else
	begin
		if exists(select 1 from [common].[UserAgent] (NoLock) where [Email] = @IN_Email)
		begin
		select @IN_ERROR = 201 --> [Email] ja cadastrado
		end
	end

if (@IN_ERROR = 0 )
begin
	INSERT INTO [common].[UserAgent]
			   ([Code]
			   ,[Password]
			   ,[Name]
			   ,[PhoneDDI]
			   ,[PhoneDDDNumber]
			   ,[Email]
			   ,[ObjectTypeId]
			   ,[StatusId]
			   ,[CreationDate]
			   ,[LastModifiedDate]
			   ,[ActivityKey]
			   ,[UserLanguageCode])
		 VALUES
			   (@IN_Code
			   ,Substring('000'+@IN_ActivityKey,1,10)
			   ,@IN_Name
			   ,@IN_PhoneDDI
			   ,@IN_PhoneDDDNumber
			   ,@IN_Email
			   ,@IN_ObjectTypeId
			   ,@IN_StatusId
			   ,getdate()
			   ,getdate()
			   ,@IN_ActivityKey
			   ,@IN_UserLanguageCode
			   )
end







GO
/****** Object:  StoredProcedure [common].[UserAgentTempUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE procedure [common].[UserAgentTempUpdate]
(
 @IN_Code nvarchar(25)
,@IN_Name nvarchar(50)
,@IN_PhoneDDI nvarchar(3)
,@IN_PhoneDDDNumber nvarchar(15)
,@IN_Email nvarchar(50)
,@IN_ObjectTypeId nvarchar(5)
,@IN_StatusId nvarchar(5)
,@IN_ActivityKey  nvarchar(50)
,@IN_UserLanguageCode nvarchar(6)
,@IN_Error int out
)
as

set @IN_Error = 0


/*
[TODO] Deve inserir na tabela temporária, mas emergialmente vai inserir direto na tabela definitiva


*/


if exists (select 1 from [common].[UserAgent] (NoLock) where [Email] = @IN_Email and [Code] <> @IN_Code)
begin
 set @IN_Error = 201 --> Email ja cadastrado para outro usuário...
end
else
begin
	if exists (select 1 from [common].[UserAgent] (NoLock) where [Code] = @IN_Code )
	begin

	UPDATE [common].[UserAgent]
	   SET [Name] = @IN_Name
		  ,[PhoneDDI] = @IN_PhoneDDI
		  ,[PhoneDDDNumber] = @IN_PhoneDDDNumber
		  ,[Email] = @IN_Email
		  ,[ObjectTypeId] = @IN_ObjectTypeId
		  ,[StatusId] = @IN_StatusId
		  ,[LastModifiedDate] = getdate()
		  ,[UserLanguageCode] = @IN_UserLanguageCode
	 WHERE [Code] = @IN_Code

	 end
	 else
	 begin
	    set @IN_Error = 100 --> Codigo não cadastrado
	 end

 end






GO
/****** Object:  StoredProcedure [common].[UserAgentUpdateStatus]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

Atualiza o status do usuário se email, senha e token estiverem cadastrados 

*/


CREATE procedure [common].[UserAgentUpdateStatus]
(
 @IN_Email nvarchar(50)
,@IN_StatusId nvarchar(5)
,@IN_AccessKey  nvarchar(50)
,@IN_Token nvarchar(6)
,@IN_Error int out
)
as

set @IN_Error = 0

if exists (SELECT 1 FROM [common].[UserEmailToken] t (NoLock)
  inner join [common].[UserAgent] u (NoLock)
  on u.Email = t.Email
  where 
		t.[Email]		= @IN_Email
    and t.[Token]		= @IN_Token
	and u.[Password]	= @IN_AccessKey 
	and t.[StatusId]	= 'ACT'
	and DeadlineDate	> GetDate())

	begin
		UPDATE [common].[UserAgent]
	   SET [StatusId] = @IN_StatusId
		  ,[LastModifiedDate] = getdate()
		WHERE [Email] = @IN_Email

	end
else
	 begin
	    set @IN_Error = 100 --> Email, Token ou senha não cadastrado
	 end



GO
/****** Object:  StoredProcedure [common].[UserEmailTokenInsert]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE procedure [common].[UserEmailTokenInsert]
(
 @IN_Email nvarchar(50)
,@IN_Token nvarchar(6)
,@IN_DeadlineMinute int = 30 -- MINUTOS
,@IN_ERROR int out
)
as

if exists ( select 1 from [common].[UserAgent] (NoLOck) where [Email] = @IN_Email )
begin
Declare @P_DeadlineDate datetime

set @P_DeadlineDate =  dateadd(mm,@IN_DeadlineMInute,getdate())

INSERT INTO [common].[UserEmailToken]
           ([Email]
           ,[Token]
           ,[ObjectTypeId]
           ,[StatusId]
           ,[CreationDate]
           ,[LastModifiedDate]
           ,[DeadlineDate])
     VALUES
           (
		    @IN_Email
           ,@IN_Token
           ,'DEF'
           ,'PEND'
           ,GETDATE()
           ,GETDATE()
           ,@P_DeadlineDate
		   )
		   set @IN_ERROR = 0
end
else
begin
 set @IN_ERROR = 1 -- Email nao cadastrado na base de isuários
end








GO
/****** Object:  StoredProcedure [common].[UserEmailTokenSelect]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*

select * from  [common].[UserEmailToken]

exec [common].[UserEmailTokenSelect]
 @IN_Email='laerte21@gmail.com'
,@IN_Token= 602316

*/

CREATE procedure [common].[UserEmailTokenSelect]
( -- soh lista usuario com token dentro do prazo de validade
 @IN_Email nvarchar(50)
,@IN_Token nvarchar(6)
)
as

SELECT top 1
	  [UserAgentId]
	 ,[Code]
     ,CONVERT(nvarchar,t.[DeadlineDate],121) DeadlineDate
  FROM [common].[UserEmailToken] t (NoLock)
  inner join [common].[UserAgent] u (NoLock)
  on u.Email = t.Email
  where 
		t.[Email] = @IN_Email
    and t.[Token] = @IN_Token
	and t.[StatusId] = 'ACT'
	and DeadlineDate > GetDate()

order by [DeadlineDate] desc






GO
/****** Object:  StoredProcedure [common].[UserEmailTokenUpdate]    Script Date: 19/11/2020 10:18:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [common].[UserEmailTokenUpdate]
(
 @IN_Id int
,@IN_StatusId  nvarchar(5)
)
as

UPDATE [common].[UserEmailToken]
   SET 
       [StatusId] = @IN_StatusId
 WHERE Id =  @IN_Id 





GO
