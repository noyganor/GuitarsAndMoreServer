

Use master
Create Database GuitarsAndMoreDB
Go

Use GuitarsAndMoreDB
Go


CREATE TABLE Models(
    ModelID INT identity(1,1) PRIMARY KEY NOT NULL,
    ModelName NVARCHAR(255) NOT NULL,
    ProducerID INT NOT NULL
);

CREATE TABLE Producers(
    ProducerID INT identity(1,1) PRIMARY KEY NOT NULL,
    Producer NVARCHAR(255) NOT NULL
);


CREATE TABLE Areas(
    AreaID INT identity(1,1) PRIMARY KEY NOT NULL ,
    Area NVARCHAR(50) NOT NULL
);


CREATE TABLE Towns(
    TownID INT identity(1,1) PRIMARY KEY NOT NULL ,
    Town NVARCHAR(50) NOT NULL
);

CREATE TABLE TownsInAreas(
    AreaID INT identity(1,1) NOT NULL,
	CONSTRAINT FK_TownsInAreas_AreaID FOREIGN KEY (AreaID) 
	REFERENCES Areas(AreaID),
    TownID INT NOT NULL,
	CONSTRAINT FK_TownsInAreas_TownID FOREIGN KEY(TownID) 
	REFERENCES Towns(TownID),
	CONSTRAINT PK_TownsInAreas PRIMARY KEY (AreaID,TownID)
);

CREATE TABLE Categories(
    CategoryID INT identity(1,1) PRIMARY KEY NOT NULL ,
    Category NVARCHAR(50) NOT NULL
);

CREATE TABLE Gender(
    GenderID INT identity(1,1) PRIMARY KEY NOT NULL,
    Gender NVARCHAR(50) NOT NULL
);

CREATE TABLE Users(
    UserID INT identity(1,1) PRIMARY KEY NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Nickname NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    VerPassword INT NOT NULL,
    PhoneNum NVARCHAR(255) NOT NULL,
    GenderID INT NOT NULL,
	CONSTRAINT FK_Users_GenderID FOREIGN KEY (GenderID)
	REFERENCES Gender(GenderID),
    FavBand NVARCHAR(255) NULL,
    JoinDate DATETIME DEFAULT GETDATE() NOT NULL
);

CREATE TABLE ModelReviews(
    ModelReviewID INT identity(1,1) PRIMARY KEY NOT NULL,
    ModelID INT NOT NULL,
	CONSTRAINT FK_ModelReviews_ModelID FOREIGN KEY(ModelID) 
	REFERENCES Models(ModelID),
	UserID INT NOT NULL,
	CONSTRAINT FK_ModelReviews_UserID FOREIGN KEY(UserID) 
	REFERENCES Users(UserID),
    ModelReview NVARCHAR(200) NOT NULL,
    ModelRate FLOAT NOT NULL  
);

CREATE TABLE UserReviews(
    UserReviewID INT identity(1,1) PRIMARY KEY NOT NULL,
    UserID INT NOT NULL,
	CONSTRAINT FK_UserReviews_UserID FOREIGN KEY(userID) 
	REFERENCES Users(UserID),
	SellerID INT NOT NULL,
	CONSTRAINT FK_UserReviews_SellerID FOREIGN KEY(SellerID) 
	REFERENCES Users(UserID),
    UserReview NVARCHAR(200) NOT NULL,
    UserRate FLOAT NOT NULL 
);

CREATE TABLE Post(
    PostID INT identity(1,1) PRIMARY KEY NOT NULL,
	ReviewID INT NULL,
    CONSTRAINT FK_Post_ReviewID FOREIGN KEY (ReviewID)
	REFERENCES UserReviews(UserReviewID),
    CategoryID INT NOT NULL,
	CONSTRAINT FK_Post_CategoryID FOREIGN KEY (CategoryID)
	REFERENCES Categories(CategoryID),
    UserID INT NOT NULL,
	CONSTRAINT FK_Post_UserID FOREIGN KEY (UserID)
	REFERENCES Users(UserID),
    ModelID INT NULL,
	CONSTRAINT FK_Post_ModelID FOREIGN KEY (ModelID)
	REFERENCES Models(ModelID),
    TownID INT NOT NULL,
	CONSTRAINT FK_Post_TownID FOREIGN KEY (TownID)
	REFERENCES Towns(TownID),
    Price FLOAT NOT NULL,
    PDescription NVARCHAR(255) NOT NULL,
    Link NVARCHAR(255) NULL,  
);

CREATE TABLE UserFavoritePosts(
    PostID INT NOT NULL ,
	CONSTRAINT FK_UserFavoritePosts_PostID FOREIGN KEY(PostID) 
	REFERENCES Post(PostID),
    UserID INT NOT NULL,
	CONSTRAINT FK_UserFavoritePosts_UserID FOREIGN KEY(userID) 
	REFERENCES Users(UserID),
	CONSTRAINT PK_UserFavoritePosts PRIMARY KEY (PostID,UserID)
);

-- Now INSET initial data to the database
SET IDENTITY_INSERT [dbo].[Towns] ON
GO

INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (1, N'?????? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (2, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (3, N'?????? ??????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (4, N'????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (5, N'?????? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (6, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (7, N'???? ????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (8, N'?????????? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (9, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (10, N'???? ????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (11, N'?????? ????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (12, N'??????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (13, N'?????? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (14, N'?????? ????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (15, N'?????? ??????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (16, N'??????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (17, N'??????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (18, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (19, N'????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (20, N'???? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (21, N'????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (22, N'????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (23, N'????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (24, N'?????? ??????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (25, N'????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (26, N'????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (27, N'?????? ????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (28, N'????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (29, N'??????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (30, N'?????????? ????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (31, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (32, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (33, N'??????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (34, N'??????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (35, N'????????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (36, N'?????? ??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (37, N'???????? ????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (38, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (39, N'??????????')
INSERT INTO [dbo].[Towns]
           (TownID, [Town])
     VALUES
           (40, N'?????? ??????????')


GO

SET IDENTITY_INSERT [dbo].[Towns] OFF
GO



SET IDENTITY_INSERT [dbo].[Gender] ON
GO

INSERT INTO [dbo].[Gender]
           (GenderID, [Gender])
     VALUES
           (1, N'????')
INSERT INTO [dbo].[Gender]
           (GenderID, [Gender])
     VALUES
           (2, N'????')
INSERT INTO [dbo].[Gender]
           (GenderID, [Gender])
     VALUES
           (3, N'??????')
GO

SET IDENTITY_INSERT [dbo].[Gender] OFF
GO

SET IDENTITY_INSERT [dbo].[Categories] ON
GO

INSERT INTO [dbo].[Categories]
           (CategoryID, [Category])
     VALUES
           (1, N'????????????')
INSERT INTO [dbo].[Categories]
           (CategoryID, [Category])
     VALUES
           (2, N'??????????')
INSERT INTO [dbo].[Categories]
           (CategoryID, [Category])
     VALUES
           (3, N'????????????')
INSERT INTO [dbo].[Categories]
           (CategoryID, [Category])
     VALUES
           (4, N'??????????????')

GO

SET IDENTITY_INSERT [dbo].[Categories] OFF
GO


ALTER TABLE Towns
ADD AreaID int FOREIGN KEY REFERENCES Areas(AreaID)
GO

INSERT INTO Areas (Area) VALUES (N'????????')
INSERT INTO Areas (Area) VALUES (N'????????')
INSERT INTO Areas (Area) VALUES (N'????????')
GO

UPDATE Towns SET AreaID = 1 
UPDATE Towns SET AreaID = 2 WHERE TownID <= 17 or TownID in (36,37,39,40)
UPDATE Towns SET AreaID = 3 WHERE TownID in (18,19,21,22,24,25,38)
GO

DROP TABLe TownsInAreas
GO

ALTER TABLE Users
ALTER COLUMN VerPassword NVARCHAR(255)
Go

INSERT INTO Users ([Email],[Nickname],[Pass],[VerPassword],[PhoneNum],[GenderID],[FavBand])
VALUES ('noiganor12@gmail.com', 'Noyga', 'nganor', 'nganor', '0505689857', '1', 'Ed Sheeran')
Go

