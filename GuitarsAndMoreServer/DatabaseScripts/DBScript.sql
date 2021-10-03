Use master
Create Database GuitarsAndMore
Go

CREATE TABLE Users (
    UserID INT identity(1,1) PRIMARY KEY NOT NULL,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Nickname NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    VerPassword INT NOT NULL,
    PhoneNum INT NOT NULL,
    Gender INT NOT NULL,
    FavBand NVARCHAR(255) NULL,
    JoinDate DATETIME NOT NULL
);
