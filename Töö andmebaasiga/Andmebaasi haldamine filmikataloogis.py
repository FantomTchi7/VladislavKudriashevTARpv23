# Imports

from os import *
from sqlite3 import *
from sqlite3 import Error
from tkinter import *
from tkinter import messagebox as mb
import matplotlib.pyplot as plt
import datetime

# Functions

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

def tableButtonsRemove():
    global buttonAutorid, buttonŽanrid, buttonFilmid, output_text
    try:
        buttonAutorid.destroy()
        buttonŽanrid.destroy()
        buttonFilmid.destroy()
        output_text.destroy()
    except:
        pass

def displayTable(table_name):
    for widget in frameRight.winfo_children():
        widget.destroy()
    frameRight.pack_propagate(False)
    table_columns = {
        "Autorid": ["ID", "Nimi", "Sünnikuupäev"],
        "Žanrid": ["ID", "Nimi"],
        "Filmid": ["ID", "Nimetus", "Väljaandmise kuupäev", "Autor", "Žanr"]
    }
    table_frame = Frame(frameRight, bg=bg)
    table_frame.pack(fill="both", expand=True)
    headers = table_columns[table_name]
    for col, header in enumerate(headers):
        header_label = Label(table_frame, text=header, bg=bg, fg=fg, font=font)
        header_label.grid(row=0, column=col, sticky="nsew")
    if table_name == "Filmid":
        query = readQuery(connection, f"SELECT Filmid.film_id, Filmid.nimetus, Filmid.väljaandmise_kuupäev, Autorid.autor_nimi, Žanrid.žanri_nimi FROM {table_name} INNER JOIN Autorid ON Filmid.autor_id = Autorid.autor_id INNER JOIN Žanrid ON Filmid.žanr_id = Žanrid.žanr_id")
    else:
        query = readQuery(connection, f"SELECT * FROM {table_name}")
    for row_idx, row in enumerate(query, start=1):
        for col, value in enumerate(row):
            if (col == 2 and table_name == "Filmid") or (col == 2 and table_name == "Autorid"):
                try:
                    value = datetime.datetime.strptime(value, "%Y-%m-%d").strftime("%d/%m/%Y")
                except ValueError as e:
                    print(f"Tekkis viga: {e}")
            label = Label(table_frame, text=value, bg=bg, fg=fg, font=font)
            label.grid(row=row_idx, column=col, sticky="nw")
    table_frame.grid_columnconfigure(tuple(range(len(headers))), weight=1)
    table_frame.grid_rowconfigure(tuple(range(len(query) + 1)), weight=1)

def tableButtonsSelect():
    for widget in frameRight.winfo_children():
        widget.destroy()
    frameRight.pack_propagate(True)
    buttonAutorid = Button(frameRight,
        text="Autorid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda: displayTable("Autorid"))
    buttonŽanrid = Button(frameRight,
        text="Žanrid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda: displayTable("Žanrid"))
    buttonFilmid = Button(frameRight,
        text="Filmid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda: displayTable("Filmid"))
    buttonAutorid.pack()
    buttonŽanrid.pack()
    buttonFilmid.pack()

# Variables

filename = path.abspath(__file__)
dbDirectory = filename.rstrip('Andmebaasi haldamine filmikataloogis.py')
dbPath = path.join(dbDirectory, "FilmiKataloog.db")
connection = createConnection(dbPath)

### Queries

createAutoridTable = """
CREATE TABLE IF NOT EXISTS Autorid(
autor_id INTEGER PRIMARY KEY AUTOINCREMENT,
autor_nimi TEXT NOT NULL,
sünnikuupäev DATETIME NOT NULL)
"""

createŽanridTable = """
CREATE TABLE IF NOT EXISTS Žanrid(
žanr_id INTEGER PRIMARY KEY AUTOINCREMENT,
žanri_nimi TEXT NOT NULL)
"""

createFilmidTable = """
CREATE TABLE IF NOT EXISTS Filmid(
film_id INTEGER PRIMARY KEY AUTOINCREMENT,
nimetus TEXT NOT NULL,
väljaandmise_kuupäev DATETIME NOT NULL,
autor_id INTEGER NOT NULL,
žanr_id INTEGER NOT NULL,
FOREIGN KEY (autor_id) REFERENCES Autorid(autor_id),
FOREIGN KEY (žanr_id) REFERENCES Žanrid(žanr_id))
"""

insertAutorid = """
INSERT INTO Autorid(autor_nimi,sünnikuupäev) VALUES
("Christopher Nolan","1970-07-30"),
("Denis Villeneuve","1967-10-03"),
("Joseph Kosinski","1974-05-03"),
("Michael Bay","1965-02-17"),
("Sam Raimi","1959-10-23")
"""

insertŽanrid = """
INSERT INTO Žanrid(žanri_nimi) VALUES
("Romaan"),
("Ulmefilm"),
("Märulifilm"),
("Seiklusfilm"),
("Superkangelase film")
"""

insertFilmid = """
INSERT INTO Filmid(nimetus,väljaandmise_kuupäev,autor_id,žanr_id) VALUES
("Transformers","2007-07-03",4,3),
("Dune","2021-09-03",2,1),
("Tron: Legacy","2010-11-30",3,4),
("Spider-Man","2002-04-29",5,5),
("Tenet","2020-08-26",1,2)
"""

## GUI

x=500
y=500
bg="#000000"
fg="#00FF00"
font="Arial 24"

window=Tk()
window.geometry(f"{x}x{y}")
window.title("FilmiKataloog")
window['bg']=bg

frameMain=Frame(window,bg=bg)
frameLeft=Frame(frameMain,bg=bg)
frameRight=Frame(frameMain,bg=bg)

labelMain=Label(window,
    text="Andmebaasi haldamine filmikataloogis",
    bg=bg,
    fg=fg,
    font=font,
    height=1,
    width=x)

button1=Button(frameLeft,
    text="SELECT * FROM",
    bg=bg,
    fg=fg,
    font=font,
    height=1,
    width=x,
    command=tableButtonsSelect)

button2=Button(frameLeft,
    text="INSERT INTO",
    bg=bg,
    fg=fg,
    font=font,
    height=1,
    width=x)

labelMain.pack()
frameMain.pack()
frameMain.grid_columnconfigure(0,weight=1)
frameMain.grid_columnconfigure(1,weight=1)

frameLeft.grid(row=0,column=0)
button1.pack()
button2.pack()
output_text = Text(frameRight, bg=bg, fg=fg, font=font)
frameRight.grid(row=0,column=1)
window.mainloop()

# Debug
# executeQuery(connection, createAutoridTable)
# executeQuery(connection, createŽanridTable)
# executeQuery(connection, createFilmidTable)
# 
# executeQuery(connection, insertAutorid)
# executeQuery(connection, insertŽanrid)
# executeQuery(connection, insertFilmid)
# 
# selectAutorid = "SELECT * FROM Autorid"
# selectŽanrid = "SELECT * FROM Žanrid"
# selectFilmid = "SELECT * FROM Filmid"
# 