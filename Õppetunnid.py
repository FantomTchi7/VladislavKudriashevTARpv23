import random,gtts,os,json
from tkinter import *
from datetime import *

# Avaldised ja lihtlaused

print("Tund on alatud.")
hilinemine=input("Kas õpilane on hilinenud?\n")
# "JAH"-a.upper(), "jah"-a.lower(), "Jah"-a.capitalize()
if hilinemine.capitalize()=="Jah":
    print("Õpilane ootab 30 min.")
print("Õpilane astub klassi.\n")

arv=random.randint(0,100) # juhuslik täisarv vahemikust 0,...100
if arv%2==0:
    print(arv,"on paaris arv.\n")
else:
    print(arv,"on paaritu arv.\n")

# Valikud

protsent=random.randint(-100,500) #0-100 0-49-"2" 50-74-"3", 75-89-"4", 90-100-"5"
if protsent<0 or protsent>100:
    tulemus="Valed andmed."
elif protsent<=49:
    tulemus="Hinne 2."
elif protsent<=74:
    tulemus="Hinne 3."
elif protsent<=89:
    tulemus="Hinne 4."
else:
    tulemus="Hinne 5."
print(str(protsent)+"% on testi tulemus.",tulemus)

# Järjend/List

nimed=["Mati","Meelis","Kati","Mati"]
while True:
    print("***********************")
    v=input("N-näita andmed\nL-lisada andmeid\nK-andmete kutsutamine\nH-Andmete haldus\nI-Positsiooni otsing\nE-Välju\n***********************\n")
    if v.upper()=="N":
        v=input("Kas juhuslik (j) nimi või loetelu (t)?")
        if v.upper()=="T":
            print(nimed)
        elif v.upper()=="J":
            print(random.choice(nimed))
    elif v.upper()=="L":
        v=input("Kas nimekirja lõppu (l) või positsioonlile (p)?")
        if v.upper()=="L":
            nimi=input("Sisesta nimi: ")
            nimed.append(nimi)
        elif v.upper()=="P":
            nimi=input("Sisesta nimi: ")
            ind=int(input("Mis kohale: "))
            nimed.insert(ind-1,nimi)
    elif v.upper()=="K":
        v=input("Kas nimi järgi (n), indeksi järgi (i) või kõik nimed (k)?")
        if v.upper()=="I":
            ind=int(input("Sisesta indeks: "))
            nimed.pop(ind-1)
        elif v.upper()=="K":
            nimed.clear()
        elif v.upper()=="N":
            nimi=input("Sisesta nimi: ")
            mitu=nimed.count(nimi)
            if mitu>0:
                for i in range(mitu):
                    nimed.remove(nimi)
            else:
                print(f"{nimi} ei ole loetelus.")
    elif v.upper()=="H":
        v=input("Sorteerimine (s), kopeerimine (k) või ümber pööramine (p)?")
        if v.upper()=="S":
            v=int(input("A-Z (1) või Z-A (2)?"))
            if v==1:
                nimed.sort()
            elif v==2:
                nimed.sort(reverse=True)
        elif v.upper()=="K":
            nimed.copy()
        elif v.upper()=="P":
            nimed.reverse()
    elif v.upper()=="I":
        nimi=input("Sisesta nimi: ")
        mitu=nimed.count(nimi)
        if mitu>0:
            print(f"Seal on {mitu} {nimi}")
            for i in range(mitu):
                print(f"{nimi} on {i+1} positsioonil")
        else:
            print(f"{nimi} ei ole loetelus.")
    elif v.upper()=="E":
        break

# Töö failidega

def loe_failist(fail:str)->list:
    """Loeme failist read ja salvestame järjendisse. Funktsioon tagastab järjend.

    param str fail:
    :rtype: list
    """
    f=open(fail,'r',encoding="utf-8") #try
    järjend=[]
    for rida in f:
        järjend.append(rida.strip())
    f.close()
    return järjend

def kirjuta_failisse(fail:str,jarjend=[]):
    """Funktsioon ümberkirjustab andmefailis.
    
    param str fail:
    param list jarjend
    """
    n=int(input("Sisesta mitu elemendi: "))
    for i in range(n):
        jarjend.append(input(f"{i+1}. element: "))
    f=open(fail,'w',encoding="utf-8")
    for el in jarjend:
        f.write(el+"\n")
    f.close()

def heli(tekst:str,keel:str):
    """
    """
    obj=gtts.gTTS(text=tekst,lang=keel,slow=False).save("heli.mp3")
    os.system("./heli.mp3")
    # В линуксе лучше указать конкретную программу и путь ./

tekst=input("Sisesta tekst: ")
heli(tekst,"et")

kirjuta_failisse("Text")

paevad=loe_failist("Paevad")
print(paevad)

# Graafiline liides. Tkinter ja Matplotlib.

# Variables
x=225
y=225
bg="#000000"
fg="#00FF00"

# Functions
k=0
def click():
    global k
    k+=1
    button.configure(text=k)
def click_(event): # Без ивента bind будет ругаться
    global k
    k-=1
    button.configure(text=k)
def tst_psse(event):
    t=textbox.get()
    label.configure(text=t)
    textbox.delete(0,END)
def choice():
    number=var.get()
    textbox.insert(END,number)

window=Tk()
window.geometry(f"{x}x{y}")
window.title(date.today())
#import os
#if "nt" == os.name: # Я сижу на GNOME, поэтому даже не знаю есть ли иконка или нет, на гноме иконки не отображаются.
#    window.iconbitmap(bitmap="myicon.ico")
#else:
#    window.iconbitmap(bitmap="@myicon.xbm")

# Elements
text="Hello world!"
label=Label(window,
            text=text,
            bg=bg,
            fg=fg,
            font="Times 20",
            height=3,
            width=x)
textbox=Entry(window,
              bg=bg,
              fg=fg,
              font="Times 20",
              width=x,
              justify=CENTER)
button=Button(window,
              text=k,
              font="Arial 20",
              height=3,
              width=x,
              relief=RAISED,
              command=click)
fr=Frame(window)
var=IntVar()
f=Radiobutton(fr,text="First",variable=var,value=1,command=choice)
s=Radiobutton(fr,text="Second",variable=var,value=2,command=choice)
t=Radiobutton(fr,text="Third",variable=var,value=3,command=choice)
# Events
button.bind("<Button-3>",click_)
textbox.bind("<Return>",tst_psse)
# Positioning

obj=[label,textbox,button,fr]
for i in obj:
    i.pack(side="top")
obj2=[f,s,t]
for i in range(len(obj2)):
    obj2[i].grid(row=0,column=i)

window.mainloop()

jsonData = '{"nimi": "Vladislav Kudriashev", "vanus": 17, "prillid": false}'
data = json.loads(jsonData)

print(data)
print(data["nimi"])
for id_, dat in enumerate(data):
    print(id_, "-", dat)

for key, value in data.items():
    print(f"{key}: {value}")

data2 = {
    "nimi": "Anna",
    "vanus": 55,
    "abielus": True,
    "lapsed": [
        "Inna",
        "Mati"
    ],
    "koduloomad": None,
    "autod": [
        {
            "muudel": "BMW",
            "varv": "punane",
            "joud": 256,
            "number": "123 ABC"
        },
        {
            "muudel": "Ford",
            "varv": "must",
            "joud": 128,
            "number": "321 CBA"
        }
    ]}
print(json.dumps(data2, indent=2))

with open(".\Töö andmebaasiga\data_file.json","w") as wFile:
    json.dump(data2, wFile)
print("Andmed failist:")
with open(".\Töö andmebaasiga\data_file.json","r") as rFile:
    data2 = json.load(rFile)
print(data2)