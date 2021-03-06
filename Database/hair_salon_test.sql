CREATE DATABASE HairSalon_test
GO

CREATE DATABASE HairSalon_test
GO

USE [HairSalon_test]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 6/9/2017 9:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phone#] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[stylistId] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stylists]    Script Date: 6/9/2017 9:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phone#] [varchar](255) NULL,
	[email] [varchar](255) NULL
) ON [PRIMARY]

GO
