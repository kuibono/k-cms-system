USE [kcms20120104]
GO
/****** Object:  Table [dbo].[Relpy]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relpy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModelID] [int] NULL,
	[NewsID] [int] NULL,
	[UserID] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[IP] [nvarchar](50) NULL,
	[ReplyTime] [datetime] NULL,
	[Content] [ntext] NULL,
 CONSTRAINT [PK_Relpy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieUrl]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieUrl](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[MoviewID] [int] NULL,
	[MoviewTitle] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[UpdateTime] [datetime] NULL,
	[Enable] [bit] NULL,
	[HttpUrl] [nvarchar](1000) NULL,
	[MagUrl] [nvarchar](1000) NULL,
	[KuaibUrl] [nvarchar](1000) NULL,
	[BaiduUrl] [nvarchar](1000) NULL,
 CONSTRAINT [PK_MovieUrl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieInfo]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NULL,
	[ClassName] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Director] [nvarchar](50) NULL,
	[Actors] [nvarchar](50) NULL,
	[Tags] [nvarchar](50) NULL,
	[Location] [nvarchar](50) NULL,
	[PublicYear] [nvarchar](50) NULL,
	[Intro] [nvarchar](1000) NULL,
	[IsMove] [bit] NULL,
	[FaceImage] [nvarchar](500) NULL,
	[InsertTime] [datetime] NULL,
	[LastDramaTitle] [datetime] NULL,
	[LastDramaID] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[Status] [int] NULL,
	[Enable] [bit] NULL,
 CONSTRAINT [PK_MovieInfo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoType]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[TemplateIndex] [text] NULL,
	[TemplateList] [text] NULL,
	[TemplateContent] [text] NULL,
	[num1] [nvarchar](50) NULL,
	[num2] [nvarchar](50) NULL,
	[num3] [nvarchar](50) NULL,
	[num4] [nvarchar](50) NULL,
	[num5] [nvarchar](50) NULL,
	[num6] [nvarchar](50) NULL,
	[num7] [nvarchar](50) NULL,
	[num8] [nvarchar](50) NULL,
	[num9] [nvarchar](50) NULL,
	[num10] [nvarchar](50) NULL,
	[nvarchar1] [nvarchar](50) NULL,
	[nvarchar2] [nvarchar](50) NULL,
	[nvarchar3] [nvarchar](50) NULL,
	[nvarchar4] [nvarchar](50) NULL,
	[nvarchar5] [nvarchar](50) NULL,
	[nvarchar6] [nvarchar](50) NULL,
	[nvarchar7] [nvarchar](50) NULL,
	[nvarchar8] [nvarchar](50) NULL,
	[nvarchar9] [nvarchar](50) NULL,
	[nvarchar10] [nvarchar](50) NULL,
	[decimal1] [nvarchar](50) NULL,
	[decimal2] [nvarchar](50) NULL,
	[decimal3] [nvarchar](50) NULL,
	[decimal4] [nvarchar](50) NULL,
	[decimal5] [nvarchar](50) NULL,
	[text1] [nvarchar](50) NULL,
	[text2] [nvarchar](50) NULL,
	[text3] [nvarchar](50) NULL,
	[text4] [nvarchar](50) NULL,
	[text5] [nvarchar](50) NULL,
	[bit1] [nvarchar](50) NULL,
	[bit2] [nvarchar](50) NULL,
	[bit3] [nvarchar](50) NULL,
	[bit4] [nvarchar](50) NULL,
	[bit5] [nvarchar](50) NULL,
 CONSTRAINT [PK_InfoType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InfoImage]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InfoImage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ModelID] [int] NULL,
	[InfoID] [int] NULL,
	[Title] [nvarchar](255) NULL,
	[Url] [nvarchar](500) NULL,
 CONSTRAINT [PK_InfoImage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InfoImage', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图片地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InfoImage', @level2type=N'COLUMN',@level2name=N'Url'
GO
/****** Object:  Table [dbo].[Info]    Script Date: 04/09/2012 14:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Info](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[InfoTypeID] [nvarchar](50) NULL,
	[ClassID] [int] NULL,
	[ClassName] [nvarchar](50) NULL,
	[ZtID] [int] NULL,
	[ZtName] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[UserID] [int] NULL,
	[Contact] [nvarchar](50) NULL,
	[ContactType] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NULL,
	[TelLocation] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[Intro] [nvarchar](1000) NULL,
	[ImageCount] [int] NULL,
	[ReplyCount] [int] NULL,
	[ClickCount] [int] NULL,
	[IsSetTop] [bit] NULL,
	[OutTime] [datetime] NULL,
	[PostTime] [datetime] NULL,
	[num1] [int] NULL,
	[num2] [int] NULL,
	[num3] [int] NULL,
	[num4] [int] NULL,
	[num5] [int] NULL,
	[num6] [int] NULL,
	[num7] [int] NULL,
	[num8] [int] NULL,
	[num9] [int] NULL,
	[num10] [int] NULL,
	[nvarchar1] [nvarchar](255) NULL,
	[nvarchar2] [nvarchar](255) NULL,
	[nvarchar3] [nvarchar](255) NULL,
	[nvarchar4] [nvarchar](255) NULL,
	[nvarchar5] [nvarchar](255) NULL,
	[nvarchar6] [nvarchar](255) NULL,
	[nvarchar7] [nvarchar](255) NULL,
	[nvarchar8] [nvarchar](255) NULL,
	[nvarchar9] [nvarchar](255) NULL,
	[nvarchar10] [nvarchar](255) NULL,
	[decimal1] [decimal](18, 2) NULL,
	[decimal2] [decimal](18, 2) NULL,
	[decimal3] [decimal](18, 2) NULL,
	[decimal4] [decimal](18, 2) NULL,
	[decimal5] [decimal](18, 2) NULL,
	[text1] [nvarchar](1000) NULL,
	[text2] [nvarchar](1000) NULL,
	[text3] [nvarchar](1000) NULL,
	[text4] [nvarchar](1000) NULL,
	[text5] [nvarchar](1000) NULL,
	[bit1] [bit] NULL,
	[bit2] [bit] NULL,
	[bit3] [bit] NULL,
	[bit4] [bit] NULL,
	[bit5] [bit] NULL,
 CONSTRAINT [PK_Info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
