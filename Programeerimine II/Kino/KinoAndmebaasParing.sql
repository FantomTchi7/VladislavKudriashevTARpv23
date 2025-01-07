CREATE TABLE Zanrid (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL
);

CREATE TABLE Vanusepiirangud (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL
);

CREATE TABLE Rezissoorid (
    ID INT PRIMARY KEY IDENTITY,
    Taisnimi NVARCHAR(255) NOT NULL
);

CREATE TABLE Filmid (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL,
    Kirjeldus NVARCHAR(MAX) NOT NULL,
    Kestus INT NOT NULL,
    Valjalaskeaeg DATE NOT NULL,
    Poster VARBINARY(MAX) NOT NULL,
    RezissoorID INT NOT NULL,
    ZanrID INT NOT NULL,
    VanusepiirangID INT NOT NULL,
    FOREIGN KEY (RezissoorID) REFERENCES Rezissoorid(ID),
    FOREIGN KEY (ZanrID) REFERENCES Zanrid(ID),
    FOREIGN KEY (VanusepiirangID) REFERENCES Vanusepiirangud(ID)
);

CREATE TABLE Kinod (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL,
    Aadress NVARCHAR(255) NOT NULL
);

CREATE TABLE Saalid (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL,
    Tuup NVARCHAR(255) NOT NULL,
    SaalVeerud INT NOT NULL,
    SaalRead INT NOT NULL,
    KinoID INT NOT NULL,
    FOREIGN KEY (KinoID) REFERENCES Kinod(ID),
    UNIQUE (KinoID, Nimetus)
);

CREATE TABLE Keeled (
    ID INT PRIMARY KEY IDENTITY,
    Nimetus NVARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE Seanssid (
    ID INT PRIMARY KEY IDENTITY,
    Aeg DATETIME NOT NULL,
    FilmID INT NOT NULL,
    KeelID INT NOT NULL,
    SaalID INT NOT NULL,
    FOREIGN KEY (FilmID) REFERENCES Filmid(ID),
    FOREIGN KEY (KeelID) REFERENCES Keeled(ID),
    FOREIGN KEY (SaalID) REFERENCES Saalid(ID)
);

CREATE TABLE Kontod (
    ID INT PRIMARY KEY IDENTITY,
    Huudnimi NVARCHAR(255) NOT NULL UNIQUE,
    Email NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL UNIQUE,
    Parool NVARCHAR(MAX) NOT NULL,
    Tuup NVARCHAR(25)
);

CREATE TABLE Piletituubid (
    ID INT PRIMARY KEY IDENTITY,
    Tuup NVARCHAR(25) NOT NULL,
    Hind DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Piletid (
    ID INT PRIMARY KEY IDENTITY,
    Rida INT NOT NULL,
    Veerg INT NOT NULL,
    Valjastatud DATETIME NOT NULL DEFAULT GETDATE(),
    PiletituupID INT NOT NULL,
    KontoID INT NOT NULL,
    SeanssID INT NOT NULL,
    FOREIGN KEY (PiletituupID) REFERENCES Piletituubid(ID),
    FOREIGN KEY (KontoID) REFERENCES Kontod(ID),
    FOREIGN KEY (SeanssID) REFERENCES Seanssid(ID),
    UNIQUE (SeanssID, Rida, Veerg)
);

INSERT INTO Kontod (Huudnimi, Email, Parool, Tuup) 
VALUES ('Kasutaja', 'Kasutaja', '1234', 'Kasutaja');

INSERT INTO Kontod (Huudnimi, Email, Parool, Tuup) 
VALUES ('Admin', 'Admin', '1234', 'Admin');

INSERT INTO Kontod (Huudnimi, Email, Parool, Tuup) 
VALUES ('Vaataja', '', '', 'Vaataja');