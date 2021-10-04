Use master
Create Database GuitarsAndMoreDB
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
