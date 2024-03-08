import gtts
import os

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