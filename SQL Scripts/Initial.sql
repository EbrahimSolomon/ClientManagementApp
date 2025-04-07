-- Create database
CREATE DATABASE ClientsDb;
GO

USE ClientsDb;
GO

-- Create Client table
CREATE TABLE Client (
    ClientId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    Gender VARCHAR(20),
    DateOfBirth NVARCHAR(20);
);

-- Create Address table
CREATE TABLE Address (
    AddressId INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT FOREIGN KEY REFERENCES Client(ClientId),
    AddressType VARCHAR(50),
    Street VARCHAR(255),
    City VARCHAR(100),
    Province VARCHAR(100),
    PostalCode VARCHAR(20),
    Country VARCHAR(100)
);

-- Create Contact table
CREATE TABLE Contact (
    ContactId INT IDENTITY(1,1) PRIMARY KEY,
    ClientId INT FOREIGN KEY REFERENCES Client(ClientId),
    ContactType VARCHAR(50),
    ContactValue VARCHAR(100)
);
