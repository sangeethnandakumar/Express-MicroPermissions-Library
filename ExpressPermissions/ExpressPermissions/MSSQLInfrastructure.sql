-- Microsoft SQL Server Infrastructre
 
IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'tblPermissions' AND Type = N'U')
BEGIN
     CREATE TABLE [dbo].[tblPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Permission] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[IsEnabled] [bit] NULL,
	[VersionSupport] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[Deleted] [datetime] NULL,
	CONSTRAINT [PK_tblPermissions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	)
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END


IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'tblPermissionBindings' AND Type = N'U')
BEGIN
	CREATE TABLE [dbo].[tblPermissionBindings](
		[UserId] [int] NOT NULL,
		[PermissionId] [int] NULL,
		[IsEnabled] [bit] NULL,
		[LastBinded] [datetime] NULL
	) ON [PRIMARY]
END


IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'tblPermissionGroups' AND Type = N'U')
BEGIN
	CREATE TABLE [dbo].[tblPermissionGroups](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Name] [varchar](50) NULL,
		[Created] [datetime] NULL,
		[Updated] [datetime] NULL,
	 CONSTRAINT [PK_tblPermissionGroups] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END


IF NOT EXISTS(SELECT 1 FROM sys.Tables WHERE  Name = N'tblPermissionGroupBindings' AND Type = N'U')
BEGIN
	CREATE TABLE [dbo].[tblPermissionGroupBindings](
		[PermissionGroupId] [int] NULL,
		[PermisssionId] [int] NULL
	) ON [PRIMARY]
END

SELECT '1';