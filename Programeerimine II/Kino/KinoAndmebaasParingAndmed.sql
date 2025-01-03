-- Insert data into Zanrid
INSERT INTO Zanrid (Nimetus) VALUES 
('Draama'), 
('Komöödia'), 
('Õudus'), 
('Seiklus'), 
('Fantaasia'), 
('Sci-Fi'), 
('Romantika'), 
('Dokumentaal'), 
('Animatsioon'), 
('Märul');

-- Insert data into Vanusepiirangud
INSERT INTO Vanusepiirangud (Nimetus) VALUES 
('K-0'), 
('K-6'), 
('K-12'), 
('K-14'), 
('K-16'), 
('K-18'), 
('K-21'), 
('K-7'), 
('K-10'), 
('K-15');

-- Insert data into Rezissoorid
INSERT INTO Rezissoorid (Taisnimi) VALUES 
('Christopher Nolan'), 
('Steven Spielberg'), 
('Quentin Tarantino'), 
('Martin Scorsese'), 
('Ridley Scott'), 
('James Cameron'), 
('Peter Jackson'), 
('Tim Burton'), 
('Wes Anderson'), 
('Sofia Coppola');

-- Insert data into Kinod
INSERT INTO Kinod (Nimetus, Aadress) VALUES 
('Kino Kosmos', 'Pärnu mnt 45, Tallinn'), 
('Kino Artis', 'Estonia pst 9, Tallinn'), 
('Kino Sõprus', 'Vana-Posti 8, Tallinn'), 
('Kino Apollo', 'Solaris Keskus, Tallinn'), 
('Kino Forum', 'Ülemiste Keskus, Tallinn'), 
('Kino Ekraan', 'Tartu mnt 18, Tartu'), 
('Kino Centrum', 'Narva mnt 1, Narva'), 
('Kino Plaza', 'Rävala pst 4, Tallinn'), 
('Kino Solaris', 'Estonia pst 9, Tallinn'), 
('Kino Tartu', 'Riia 10, Tartu');

-- Insert data into Saalid
INSERT INTO Saalid (Nimetus, Tuup, KinoID, SaalVeerud, SaalRead) VALUES 
('Saal 1', 'IMAX', 1, 10, 15), 
('Saal 2', '3D', 1, 8, 12), 
('Saal 3', 'Standard', 1, 12, 20), 
('Saal 1', 'Standard', 2, 10, 15), 
('Saal 2', '3D', 2, 8, 12), 
('Saal 1', 'IMAX', 3, 10, 15), 
('Saal 2', 'Standard', 3, 12, 20), 
('Saal 1', '3D', 4, 8, 12), 
('Saal 2', 'Standard', 4, 12, 20), 
('Saal 1', 'IMAX', 5, 10, 15);

-- Insert data into Keeled
INSERT INTO Keeled (Nimetus) VALUES 
('Eesti'), 
('Inglise'), 
('Vene'), 
('Saksa'), 
('Prantsuse'), 
('Hispaania'), 
('Itaalia'), 
('Jaapani'), 
('Hiina'), 
('Korea');

-- Insert data into Filmid
INSERT INTO Filmid (Nimetus, Kirjeldus, Kestus, Väljalaskeaeg, Poster, RezissoorID, ZanrID, VanusepiirangID) VALUES 
('Inception', 'A mind-bending thriller', 148, '2010-07-16', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Inception.jpg', SINGLE_BLOB) AS Poster), 1, 1, 3),
('Jurassic Park', 'Dinosaurs come to life', 127, '1993-06-11', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Jurassic Park.jpg', SINGLE_BLOB) AS Poster), 2, 4, 2),
('Pulp Fiction', 'A cult classic', 154, '1994-10-14', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Pulp Fiction.jpg', SINGLE_BLOB) AS Poster), 3, 1, 5),
('The Dark Knight', 'Batman faces the Joker', 152, '2008-07-18', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\The Dark Knight.jpg', SINGLE_BLOB) AS Poster), 1, 10, 6),
('Avatar', 'A marine on an alien planet', 162, '2009-12-18', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Avatar.jpg', SINGLE_BLOB) AS Poster), 6, 6, 4),
('The Lord of the Rings', 'Epic fantasy adventure', 178, '2001-12-19', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\The Lord of the Rings.jpg', SINGLE_BLOB) AS Poster), 7, 5, 3),
('Titanic', 'A love story on the doomed ship', 195, '1997-12-19', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Titanic.jpg', SINGLE_BLOB) AS Poster), 6, 7, 5),
('The Matrix', 'A hacker discovers reality', 136, '1999-03-31', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\The Matrix.jpg', SINGLE_BLOB) AS Poster), 5, 6, 6),
('Forrest Gump', 'Life story of a simple man', 142, '1994-07-06', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Forrest Gump.jpg', SINGLE_BLOB) AS Poster), 4, 1, 2),
('The Grand Budapest Hotel', 'Adventures of a concierge', 99, '2014-03-28', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\The Grand Budapest Hotel.jpg', SINGLE_BLOB) AS Poster), 9, 2, 3);

-- Insert data into Seanssid
INSERT INTO Seanssid (Aeg, FilmID, KeelID, SaalID) VALUES 
('2023-10-01 14:00:00', 1, 1, 1), 
('2023-10-01 17:00:00', 2, 2, 2), 
('2023-10-01 20:00:00', 3, 3, 3), 
('2023-10-02 14:00:00', 4, 4, 4), 
('2023-10-02 17:00:00', 5, 5, 5), 
('2023-10-02 20:00:00', 6, 6, 6), 
('2023-10-03 14:00:00', 7, 7, 7), 
('2023-10-03 17:00:00', 8, 8, 8), 
('2023-10-03 20:00:00', 9, 9, 9), 
('2023-10-04 14:00:00', 10, 10, 10), 
('2023-10-04 17:00:00', 1, 1, 1), 
('2023-10-04 20:00:00', 2, 2, 2), 
('2023-10-05 14:00:00', 3, 3, 3), 
('2023-10-05 17:00:00', 4, 4, 4), 
('2023-10-05 20:00:00', 5, 5, 5), 
('2023-10-06 14:00:00', 6, 6, 6), 
('2023-10-06 17:00:00', 7, 7, 7), 
('2023-10-06 20:00:00', 8, 8, 8), 
('2023-10-07 14:00:00', 9, 9, 9), 
('2023-10-07 17:00:00', 10, 10, 10), 
('2023-10-07 20:00:00', 1, 1, 1), 
('2023-10-08 14:00:00', 2, 2, 2), 
('2023-10-08 17:00:00', 3, 3, 3), 
('2023-10-08 20:00:00', 4, 4, 4), 
('2023-10-09 14:00:00', 5, 5, 5), 
('2023-10-09 17:00:00', 6, 6, 6), 
('2023-10-09 20:00:00', 7, 7, 7), 
('2023-10-10 14:00:00', 8, 8, 8), 
('2023-10-10 17:00:00', 9, 9, 9), 
('2023-10-10 20:00:00', 10, 10, 10), 
('2023-10-11 14:00:00', 1, 1, 1), 
('2023-10-11 17:00:00', 2, 2, 2), 
('2023-10-11 20:00:00', 3, 3, 3), 
('2023-10-12 14:00:00', 4, 4, 4), 
('2023-10-12 17:00:00', 5, 5, 5), 
('2023-10-12 20:00:00', 6, 6, 6), 
('2023-10-13 14:00:00', 7, 7, 7), 
('2023-10-13 17:00:00', 8, 8, 8), 
('2023-10-13 20:00:00', 9, 9, 9), 
('2023-10-14 14:00:00', 10, 10, 10), 
('2023-10-14 17:00:00', 1, 1, 1), 
('2023-10-14 20:00:00', 2, 2, 2), 
('2023-10-15 14:00:00', 3, 3, 3), 
('2023-10-15 17:00:00', 4, 4, 4), 
('2023-10-15 20:00:00', 5, 5, 5), 
('2023-10-16 14:00:00', 6, 6, 6), 
('2023-10-16 17:00:00', 7, 7, 7), 
('2023-10-16 20:00:00', 8, 8, 8), 
('2023-10-17 14:00:00', 9, 9, 9), 
('2023-10-17 17:00:00', 10, 10, 10), 
('2023-10-17 20:00:00', 1, 1, 1), 
('2023-10-18 14:00:00', 2, 2, 2), 
('2023-10-18 17:00:00', 3, 3, 3), 
('2023-10-18 20:00:00', 4, 4, 4), 
('2023-10-19 14:00:00', 5, 5, 5), 
('2023-10-19 17:00:00', 6, 6, 6), 
('2023-10-19 20:00:00', 7, 7, 7), 
('2023-10-20 14:00:00', 8, 8, 8), 
('2023-10-20 17:00:00', 9, 9, 9), 
('2023-10-20 20:00:00', 10, 10, 10), 
('2023-10-21 14:00:00', 1, 1, 1), 
('2023-10-21 17:00:00', 2, 2, 2), 
('2023-10-21 20:00:00', 3, 3, 3), 
('2023-10-22 14:00:00', 4, 4, 4), 
('2023-10-22 17:00:00', 5, 5, 5), 
('2023-10-22 20:00:00', 6, 6, 6), 
('2023-10-23 14:00:00', 7, 7, 7), 
('2023-10-23 17:00:00', 8, 8, 8), 
('2023-10-23 20:00:00', 9, 9, 9), 
('2023-10-24 14:00:00', 10, 10, 10), 
('2023-10-24 17:00:00', 1, 1, 1), 
('2023-10-24 20:00:00', 2, 2, 2), 
('2023-10-25 14:00:00', 3, 3, 3), 
('2023-10-25 17:00:00', 4, 4, 4), 
('2023-10-25 20:00:00', 5, 5, 5), 
('2023-10-26 14:00:00', 6, 6, 6), 
('2023-10-26 17:00:00', 7, 7, 7), 
('2023-10-26 20:00:00', 8, 8, 8), 
('2023-10-27 14:00:00', 9, 9, 9), 
('2023-10-27 17:00:00', 10, 10, 10), 
('2023-10-27 20:00:00', 1, 1, 1), 
('2023-10-28 14:00:00', 2, 2, 2), 
('2023-10-28 17:00:00', 3, 3, 3), 
('2023-10-28 20:00:00', 4, 4, 4), 
('2023-10-29 14:00:00', 5, 5, 5), 
('2023-10-29 17:00:00', 6, 6, 6), 
('2023-10-29 20:00:00', 7, 7, 7), 
('2023-10-30 14:00:00', 8, 8, 8), 
('2023-10-30 17:00:00', 9, 9, 9), 
('2023-10-30 20:00:00', 10, 10, 10), 
('2023-10-31 14:00:00', 1, 1, 1), 
('2023-10-31 17:00:00', 2, 2, 2), 
('2023-10-31 20:00:00', 3, 3, 3);

-- Insert data into Istmed
INSERT INTO Istmed (SaalID, Rida, Veerg) VALUES 
(1, 1, 1), (1, 1, 2), (1, 1, 3), (1, 1, 4), (1, 1, 5), 
(1, 2, 1), (1, 2, 2), (1, 2, 3), (1, 2, 4), (1, 2, 5), 
(2, 1, 1), (2, 1, 2), (2, 1, 3), (2, 1, 4), (2, 1, 5), 
(2, 2, 1), (2, 2, 2), (2, 2, 3), (2, 2, 4), (2, 2, 5), 
(3, 1, 1), (3, 1, 2), (3, 1, 3), (3, 1, 4), (3, 1, 5), 
(3, 2, 1), (3, 2, 2), (3, 2, 3), (3, 2, 4), (3, 2, 5), 
(4, 1, 1), (4, 1, 2), (4, 1, 3), (4, 1, 4), (4, 1, 5), 
(4, 2, 1), (4, 2, 2), (4, 2, 3), (4, 2, 4), (4, 2, 5), 
(5, 1, 1), (5, 1, 2), (5, 1, 3), (5, 1, 4), (5, 1, 5), 
(5, 2, 1), (5, 2, 2), (5, 2, 3), (5, 2, 4), (5, 2, 5);

-- Insert data into SeanssiIstmed
INSERT INTO SeanssiIstmed (SeanssID, IstmedID, KasOnHõivatud) VALUES 
(1, 1, 0), (1, 2, 0), (1, 3, 0), (1, 4, 0), (1, 5, 0), 
(2, 6, 0), (2, 7, 0), (2, 8, 0), (2, 9, 0), (2, 10, 0), 
(3, 11, 0), (3, 12, 0), (3, 13, 0), (3, 14, 0), (3, 15, 0), 
(4, 16, 0), (4, 17, 0), (4, 18, 0), (4, 19, 0), (4, 20, 0), 
(5, 21, 0), (5, 22, 0), (5, 23, 0), (5, 24, 0), (5, 25, 0), 
(6, 26, 0), (6, 27, 0), (6, 28, 0), (6, 29, 0), (6, 30, 0), 
(7, 31, 0), (7, 32, 0), (7, 33, 0), (7, 34, 0), (7, 35, 0), 
(8, 36, 0), (8, 37, 0), (8, 38, 0), (8, 39, 0), (8, 40, 0), 
(9, 41, 0), (9, 42, 0), (9, 43, 0), (9, 44, 0), (9, 45, 0), 
(10, 46, 0), (10, 47, 0), (10, 48, 0), (10, 49, 0), (10, 50, 0);


-- Insert data into Filmid
INSERT INTO Filmid (Nimetus, Kirjeldus, Kestus, Väljalaskeaeg, Poster, RezissoorID, ZanrID, VanusepiirangID) VALUES 
('Drive', 'A Hollywood stunt driver moonlights as a getaway driver.', 100, '2011-09-16', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Drive.jpg', SINGLE_BLOB) AS Poster), 10, 10, 5),
('Dune', 'A noble family becomes embroiled in a war for a desert planet.', 155, '2021-10-22', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Dune.jpg', SINGLE_BLOB) AS Poster), 5, 6, 6),
('Tron: Legacy', 'A man is trapped in a digital world and must escape.', 125, '2010-12-17', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Tron Legacy.jpg', SINGLE_BLOB) AS Poster), 6, 6, 4),
('Oppenheimer', 'The story of J. Robert Oppenheimer and the atomic bomb.', 180, '2023-07-21', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Oppenheimer.jpg', SINGLE_BLOB) AS Poster), 1, 1, 7),
('Interstellar', 'A team of astronauts travels through a wormhole in space.', 169, '2014-11-07', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Interstellar.jpg', SINGLE_BLOB) AS Poster), 1, 6, 4),
('Tenet', 'A secret agent manipulates time to prevent a catastrophe.', 150, '2020-08-26', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\Tenet.jpg', SINGLE_BLOB) AS Poster), 1, 6, 6),
('John Wick', 'An ex-hitman comes out of retirement for revenge.', 101, '2014-10-24', (SELECT * FROM OPENROWSET(BULK N'C:\Users\Alena\source\repos\FantomTchi7\VladislavKudriashevTARpv23\Programeerimine II\Kino\Poster\John Wick.jpg', SINGLE_BLOB) AS Poster), 5, 10, 5);

-- Insert data into Seanssid
INSERT INTO Seanssid (Aeg, FilmID, KeelID, SaalID) VALUES 
('2023-11-01 14:00:00', 11, 1, 1), -- Drive
('2023-11-01 17:00:00', 12, 2, 2), -- Dune
('2023-11-01 20:00:00', 13, 3, 3), -- Tron: Legacy
('2023-11-02 14:00:00', 14, 4, 4), -- Oppenheimer
('2023-11-02 17:00:00', 15, 5, 5), -- Interstellar
('2023-11-02 20:00:00', 16, 6, 6), -- Tenet
('2023-11-03 14:00:00', 17, 7, 7), -- John Wick
('2023-11-03 17:00:00', 11, 8, 8), 
('2023-11-03 20:00:00', 12, 9, 9), 
('2023-11-04 14:00:00', 13, 10, 10),
('2023-11-04 17:00:00', 14, 1, 1),
('2023-11-04 20:00:00', 15, 2, 2);