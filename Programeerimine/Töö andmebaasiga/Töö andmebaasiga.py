from os import *
from sqlite3 import *
from sqlite3 import Error

def createConnection(path:str):
    connection = None
    try:
        connection = connect(path)
        # print("Ühendus on olemas!")
    except Error as e:
        print(f"Tekkis viga: {e}")
    return connection

def executeQuery(connection, query):
    try:
        cursor = connection.cursor()
        cursor.execute(query)
        connection.commit()
        # print("Tabel on loodud.")
    except Error as e:
        print(f"Tekkis viga: {e}")

def readQuery(connection, query):
    cursor = connection.cursor()
    result = None
    try:
        cursor.execute(query)
        result = cursor.fetchall()
        return result
    except Error as e:
        print(f"Tekkis viga: {e}")

def dropTable(connection, table):
    try:
        cursor = connection.cursor()
        cursor.execute(f"DROP TABLE IF EXISTS {table}")
        connection.commit()
        # print("Tabel on kustutatud.")
    except Error as e:
        print(f"Tekkis viga: {e}")

def columnsFromTable(connection, table):
    cursor = connection.cursor()
    cursor.execute(f"PRAGMA table_info('{table}')")
    columnNames = [i[1] for i in cursor.fetchall()]
    return columnNames

def findLetterInTable(connection, table, letter):
    try:
        cursor = connection.cursor()
        column_names = columnsFromTable(connection, table)
        query = f"SELECT * FROM {table} WHERE "
        conditions = []
        for column in column_names:
            conditions.append(f"{column} LIKE '%{letter}%'")
        query += " OR ".join(conditions)
        cursor.execute(query)
        result = cursor.fetchall()
        return result
    except Error as e:
        print(f"Tekkis viga: {e}")

createUsersTable = """CREATE TABLE IF NOT EXISTS Users(
ID INTEGER PRIMARY KEY AUTOINCREMENT,
Name TEXT NOT NULL,
LastName TEXT NOT NULL,
Gender TEXT NOT NULL,
Born DATATIME NOT NULL,
BirthCountry TEXT NOT NULL,
PassportCountry TEXT NOT NULL)"""

insertUsers = """INSERT INTO Users(Name,LastName,Gender,Born,BirthCountry,PassportCountry) VALUES
("Vladislav","Kudriashev","Male","2007-02-25","Russia","Estonia"),
("Lev","Egorov","Male","2007-06-28","Estonia","Estonia"),
("Timur","Baširov","Male","2005-06-10","Estonia","Estonia"),
("Enes","Albarazanchi","Male","2010-02-24","Iraq","Iraq"),
("Damian","Kryuk","Male","2009-03-16","Belarus","Estonia")"""

selectUsers = "SELECT * FROM Users"

createCarsTable = """CREATE TABLE IF NOT EXISTS Cars(
ID INTEGER PRIMARY KEY AUTOINCREMENT,
UsersID INTEGER,
Mark TEXT NOT NULL,
Model TEXT NOT NULL,
Year INTEGER NOT NULL,
MadeIn TEXT NOT NULL,
FOREIGN KEY (UsersID) REFERENCES Users(ID))"""

insertCars = """INSERT INTO Cars(UsersID,Mark,Model,Year,MadeIn) VALUES
(3,"Toyota","Corolla",2019,"England"),
(4,"Toyota","AE86",1986,"Japan"),
(1,"Nissan","370z",2008,"Japan"),
(5,"Lada","Riva",2002,"Russia"),
(2,"Volvo","S80 II",2006,"Sweden")"""

selectCars = "SELECT Cars.ID,Users.LastName,Cars.Mark,Cars.Model,Cars.Year,Cars.MadeIn FROM Cars INNER JOIN Users ON Cars.UsersID=Users.ID"

filename = path.abspath(__file__)
dbDirectory = filename.rstrip('Töö andmebaasiga.py')
dbPath = path.join(dbDirectory, "data.db")
connection = createConnection(dbPath)

executeQuery(connection, createUsersTable)
executeQuery(connection, insertUsers)
users = readQuery(connection, selectUsers)
executeQuery(connection, createCarsTable)
executeQuery(connection, insertCars)
users = findLetterInTable(connection, "Users", "K")
cars = readQuery(connection, selectCars)
print("Tabel Users:")
for user in users:
    print(user)
print("Tabel Cars:")
for car in cars:
    print(car)

# dropTable(connection, "Users")
# dropTable(connection, "Cars")
# users = readQuery(connection, selectUsers)
# cars = readQuery(connection, selectCars)
# print(users)
# print(cars)