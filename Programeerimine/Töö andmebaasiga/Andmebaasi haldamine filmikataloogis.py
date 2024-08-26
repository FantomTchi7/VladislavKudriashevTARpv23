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
        print(query)
        cursor.execute(query)
        connection.commit()
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

def choiceColor(choice:int):
    global button1, button2, button3
    if choice==1:
        button1.configure(fg=fg)
        button2.configure(fg="#7f7f7f")
        button3.configure(fg="#7f7f7f")
    elif choice==2:
        button1.configure(fg="#7f7f7f")
        button2.configure(fg=fg)
        button3.configure(fg="#7f7f7f")
    elif choice==3:
        button1.configure(fg="#7f7f7f")
        button2.configure(fg="#7f7f7f")
        button3.configure(fg=fg)

def tableButtonsRemove():
    global buttonAutorid, buttonŽanrid, buttonFilmid, outputText
    try:
        buttonAutorid.destroy()
        buttonŽanrid.destroy()
        buttonFilmid.destroy()
        outputText.destroy()
    except:
        pass

def displayTable(tableName):
    for widget in frameRight.winfo_children():
        widget.destroy()
    frameRight.pack_propagate(False)
    tableColumns = {
        "Autorid": ["ID", "Nimi", "Sünnikuupäev"],
        "Žanrid": ["ID", "Nimi"],
        "Filmid": ["ID", "Nimetus", "Väljaandmise kuupäev", "Autor", "Žanr"]
    }
    tableFrame = Frame(frameRight, bg=bg)
    tableFrame.pack(fill="both", expand=True)

    scrollbarX = Scrollbar(tableFrame, orient=HORIZONTAL)
    scrollbarX.pack(side=BOTTOM, fill=X)

    scrollbarY = Scrollbar(tableFrame, orient=VERTICAL)
    scrollbarY.pack(side=RIGHT, fill=Y)

    canvas = Canvas(tableFrame, bg=bg, bd=0, highlightthickness=0, xscrollcommand=scrollbarX.set, yscrollcommand=scrollbarY.set)
    canvas.pack(side=LEFT, fill=BOTH, expand=True)

    scrollbarX.config(command=canvas.xview)
    scrollbarY.config(command=canvas.yview)

    innerFrame = Frame(canvas, bg=bg)
    canvas.create_window((0, 0), window=innerFrame, anchor=NW)

    headers = tableColumns[tableName]
    for col, header in enumerate(headers):
        headerLabel = Label(innerFrame, text=header, bg=bg, fg=fg, font=font)
        headerLabel.grid(row=0, column=col, sticky="nsew")
    
    if tableName == "Filmid":
        query = readQuery(connection, f"SELECT Filmid.film_id, Filmid.nimetus, Filmid.väljaandmise_kuupäev, Autorid.autor_nimi, Žanrid.žanri_nimi FROM {tableName} INNER JOIN Autorid ON Filmid.autor_id = Autorid.autor_id INNER JOIN Žanrid ON Filmid.žanr_id = Žanrid.žanr_id")
    else:
        query = readQuery(connection, f"SELECT * FROM {tableName}")

    for rowIdX, row in enumerate(query, start=1):
        for col, value in enumerate(row):
            if (col == 2 and tableName == "Filmid") or (col == 2 and tableName == "Autorid"):
                try:
                    value = datetime.datetime.strptime(value, "%Y-%m-%d").strftime("%d/%m/%Y")
                except ValueError as e:
                    print(f"Tekkis viga: {e}")
            label = Label(innerFrame, text=value, bg=bg, fg=fg, font=font)
            label.grid(row=rowIdX, column=col, sticky="nw")
    
    innerFrame.update_idletasks()
    canvas.config(scrollregion=canvas.bbox("all"))

def tableButtonsSelect1():
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
        command=lambda:displayTable("Autorid"))
    buttonŽanrid = Button(frameRight,
        text="Žanrid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:displayTable("Žanrid"))
    buttonFilmid = Button(frameRight,
        text="Filmid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:displayTable("Filmid"))
    buttonAutorid.pack()
    buttonŽanrid.pack()
    buttonFilmid.pack()
    choiceColor(1)

tableColumns = {
    "Autorid": ["autor_id", "autor_nimi", "sünnikuupäev"],
    "Žanrid": ["žanr_id", "žanri_nimi"],
    "Filmid": ["film_id", "nimetus", "väljaandmise_kuupäev", "autor_id", "žanr_id"]
}

def contextMenu(tableName, buttonColumn, columns):
    menu = Menu(window, tearoff=0)
    for column in columns:
        menu.add_command(label=column, command=lambda col=column: buttonColumn.config(text=f"{col}"))
    menu.tk_popup(window.winfo_pointerx(), window.winfo_pointery())

def insertTable(tableName):
    for widget in frameRight.winfo_children():
        widget.destroy()
    frameRight.pack_propagate(False)
    
    def insertData():
        allColumns = tableColumns[tableName]
        insertValues = [insertText.get('1.0', 'end').strip() for insertText in insertTexts]
        insertValues = [f"'{value}'" for value in insertValues]
        query = f"INSERT INTO {tableName}({', '.join(allColumns)}) VALUES ({', '.join(insertValues)})"
        executeQuery(connection, query)

    insertTexts = []
    insertLabels = []
    for i, column in enumerate(tableColumns[tableName]):
        rowFrame = Frame(frameRight, bg=bg)
        rowFrame.pack(side="top", fill="x")

        label = Label(rowFrame, text=column, bg=bg, fg=fg, font=font)
        label.pack(side="left")

        insertText = Text(rowFrame, bg=bg, fg=fg, font=font, height=1)
        insertText.pack(side="right", fill="x", expand=True)

        insertLabels.append(label)
        insertTexts.append(insertText)

    insertButton = Button(frameRight,
        text="Insert",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=insertData)
    insertButton.pack()

def deleteFromTable(tableName):
    for widget in frameRight.winfo_children():
        widget.destroy()
    frameRight.pack_propagate(False)
    
    def deleteData():
        column = tableColumns[tableName][0]  # assuming first column is ID
        value = deleteText.get('1.0', 'end').strip()
        query = f"DELETE FROM {tableName} WHERE {column} = {value}"
        executeQuery(connection, query)

    deleteLabels = []
    deleteTexts = []
    for i, column in enumerate([tableColumns[tableName][0]]):
        rowFrame = Frame(frameRight, bg=bg)
        rowFrame.pack(side="top", fill="x")

        label = Label(rowFrame, text=f"WHERE {column}=", bg=bg, fg=fg, font=font)
        label.pack(side="left")

        deleteText = Text(rowFrame, bg=bg, fg=fg, font=font, height=1)
        deleteText.pack(side="right", fill="x", expand=True)

        deleteLabels.append(label)
        deleteTexts.append(deleteText)

    deleteButton = Button(frameRight,
        text="Delete",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=deleteData)
    deleteButton.pack()

def tableButtonsSelect2():
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
        command=lambda:insertTable("Autorid"))
    buttonŽanrid = Button(frameRight,
        text="Žanrid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:insertTable("Žanrid"))
    buttonFilmid = Button(frameRight,
        text="Filmid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:insertTable("Filmid"))
    buttonAutorid.pack()
    buttonŽanrid.pack()
    buttonFilmid.pack()
    choiceColor(2)

def tableButtonsSelect3():
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
        command=lambda:deleteFromTable("Autorid"))
    buttonŽanrid = Button(frameRight,
        text="Žanrid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:deleteFromTable("Žanrid"))
    buttonFilmid = Button(frameRight,
        text="Filmid",
        bg=bg,
        fg=fg,
        font=font,
        height=1,
        width=x,
        command=lambda:deleteFromTable("Filmid"))
    buttonAutorid.pack()
    buttonŽanrid.pack()
    buttonFilmid.pack()
    choiceColor(3)

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

# GUI

x=900
y=900
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
    command=tableButtonsSelect1)

button2=Button(frameLeft,
    text="INSERT INTO",
    bg=bg,
    fg=fg,
    font=font,
    height=1,
    width=x,
    command=tableButtonsSelect2)

button3=Button(frameLeft,
    text="DELETE FROM",
    bg=bg,
    fg=fg,
    font=font,
    height=1,
    width=x,
    command=tableButtonsSelect3)

labelMain.pack()
frameMain.pack(fill='both', expand=True)
frameMain.grid_rowconfigure(0,weight=1)
frameMain.grid_columnconfigure(0,weight=1)
frameMain.grid_columnconfigure(1,weight=1)

frameLeft.grid(row=0,column=0, sticky='nsew')
button1.pack(fill='both')
button2.pack(fill='both')
button3.pack(fill='both')
outputText = Text(frameRight, bg=bg, fg=fg, font=font)
frameRight.grid(row=0,column=1, sticky='nsew')
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
