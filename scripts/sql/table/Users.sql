USE MyDatabase
GO

CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [dbo].[Users] (Id, DateCreated, DateModified, UserName, Password)
VALUES ('dbf8646d-e708-41aa-2af7-08d72597487c','2019-08-21 19:58:29.5509530','2019-08-21 19:58:29.5509530','john','1234');
GO