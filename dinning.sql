USE [dinning]
GO
/****** Object:  Table [dbo].[dish]    Script Date: 7/28/2016 3:27:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dish](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[price] [int] NULL,
	[store_id] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[store]    Script Date: 7/28/2016 3:27:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[store](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[review] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[dish] ON 

INSERT [dbo].[dish] ([id], [name], [price], [store_id]) VALUES (1, N'retrrte', 32, 1)
INSERT [dbo].[dish] ([id], [name], [price], [store_id]) VALUES (2, N'fvsdds', 243243, 1)
SET IDENTITY_INSERT [dbo].[dish] OFF
SET IDENTITY_INSERT [dbo].[store] ON 

INSERT [dbo].[store] ([id], [name], [review]) VALUES (1, N'Burger Queen', 4)
INSERT [dbo].[store] ([id], [name], [review]) VALUES (2, N'russ', 10)
SET IDENTITY_INSERT [dbo].[store] OFF
