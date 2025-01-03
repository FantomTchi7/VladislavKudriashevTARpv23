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
    Väljalaskeaeg DATE NOT NULL,
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
    KinoID INT NOT NULL,
    SaalVeerud INT NOT NULL,
    SaalRead INT NOT NULL,
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

CREATE TABLE Istmed (
    ID INT PRIMARY KEY IDENTITY,
    SaalID INT NOT NULL,
    Rida INT NOT NULL,
    Veerg INT NOT NULL,
    FOREIGN KEY (SaalID) REFERENCES Saalid(ID),
    UNIQUE (SaalID, Rida, Veerg)
);

CREATE TABLE SeanssiIstmed (
    ID INT PRIMARY KEY IDENTITY,
    SeanssID INT NOT NULL,
    IstmedID INT NOT NULL,
    KasOnHõivatud BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (SeanssID) REFERENCES Seanssid(ID),
    FOREIGN KEY (IstmedID) REFERENCES Istmed(ID),
    UNIQUE (SeanssID, IstmedID)
);

CREATE TABLE Kontod (
    ID INT PRIMARY KEY IDENTITY,
    Huudnimi NVARCHAR(255) NOT NULL UNIQUE,
    Email NVARCHAR(255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL UNIQUE,
    Parool NVARCHAR(MAX) NOT NULL,
	Tüüp NVARCHAR(25)
);

CREATE TABLE Piletid (
    ID INT PRIMARY KEY IDENTITY,
    SeanssiIstmedID INT NOT NULL,
    KontoID INT NOT NULL,
    Hind DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (SeanssiIstmedID) REFERENCES SeanssiIstmed(ID),
    FOREIGN KEY (KontoID) REFERENCES Kontod(ID)
);

CREATE INDEX idx_FilmID ON Seanssid(FilmID);
CREATE INDEX idx_SaalID ON Istmed(SaalID);
CREATE INDEX idx_SeanssID ON SeanssiIstmed(SeanssID);
CREATE INDEX idx_KontoID ON Piletid(KontoID);

INSERT INTO Kontod (Huudnimi, Email, Parool, Tüüp) 
VALUES ('Kasutaja', 'Kasutaja', '1234', 'Kasutaja');

INSERT INTO Kontod (Huudnimi, Email, Parool, Tüüp) 
VALUES ('Admin', 'Admin', '1234', 'Admin');

INSERT INTO Kontod (Huudnimi, Email, Parool, Tüüp) 
VALUES ('Vaataja', '', '', 'Vaataja');