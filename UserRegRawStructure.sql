CREATE TABLE [dbo].[UserReg] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Email]    VARCHAR (320) NOT NULL,
    [Password] CHAR (64)     NOT NULL,
    CONSTRAINT [PK_UserReg] PRIMARY KEY CLUSTERED ([Id] ASC)
);

