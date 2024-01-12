import math
import os
from random import *
print("Ülesanne #1\n\n"+"Tere maailm!")
try:
    nimi=input("Mis on sinu nimi?\n")
    vanus=int(input("Kui vana sa oled?\n"))
    print("Tere tulemast!",nimi,"sa oled",vanus,"aastat vana")
    #print("Tere tulemast! "+nimi+" sa oled "+str(vanus)+" aastat vana")
    #print("Tere tulemast! {0} sa oled {1} aastat vana.".format(nimi,vanus))
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #2\n")
nimi2="Jääk"
vanus2=18
pikkus=16.5
kas_käib_koolis=True
print("Muutuja nimi =",nimi2,"tüüp on:",type(nimi2))
print("Muutuja vanus =",vanus2,"tüüp on:",type(vanus2))
print("Muutuja pikkus =",pikkus,"tüüp on:",type(pikkus))
print("Muutuja kas_käib_koolis =",kas_käib_koolis,"tüüp on:",type(kas_käib_koolis))
print("\nÜlesanne #3\n")
try:
    kokku=randint(10,100)
    print("Kokku: ",kokku)
    mitu=int(input("Mitu kommi tahad võtta?\n"))
    kokku-=mitu
    if kokku<=0:
        kokku=0
    print("Nüüd laua peal on",kokku,"kommid.")
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #4\n")
try:
    ringjoonepikkus=int(input("Kirjuta puu ümbermõõdu pikkus. (sentimeetrites)\n"))
    diam=ringjoonepikkus/math.pi
    print("Puu läbimõõt on:",diam,"cm")
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #5\n")
try:
    N=float(input("Sisestage ristkülikukujulise krundi pikkus (meetrites):\n"))
    M=float(input("Sisestage ristkülikukujulise krundi laius (meetrites):\n"))
    diagonal=math.sqrt(N**2+M**2)
    print("Ristkülikukujulise krundi diagonaal on:",diagonal,"m")
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #6\n")
try:
    aeg=float(input("Mitu tundi läks sõitmiseks?\n"))
    kaugus=float(input("Mitu kilomeetrit sa sõitsid?\n"))
    kiirus=kaugus/aeg
    print("Sinu kiirus oli",str(kiirus),"km/h")
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #7\n")
try:
    number1=(float(input("Sisesta number #1:\n")))
    number2=(float(input("Sisesta number #2:\n")))
    number3=(float(input("Sisesta number #3:\n")))
    number4=(float(input("Sisesta number #4:\n")))
    number5=(float(input("Sisesta number #5:\n")))
    keskmine=(number1+number2+number3+number4+number5)/5
    print("Aritmeetiline keskmine on:",keskmine)
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #8\n")
print("  @..@\n"+" (----)\n"+"( \__/ )\n"+" ^^ "" ^^")
print("\nÜlesanne #9\n")
try:
    a=int(input("Sisesta külje pikkus a (sentimeetrites):\n"))
    b=int(input("Sisesta külje pikkus b (sentimeetrites):\n"))
    c=int(input("Sisesta külje pikkus c (sentimeetrites):\n"))
    ümbermõõt=a+b+c
    print("Kolmnurga ümbermõõt on:",ümbermõõt,"cm")
except:
    print("Viga andmetüübiga.")
print("\nÜlesanne #10\n")
P=randint(1,5)
hind=12.90
hind*=1.1
osa=round(hind/P,2)
print("Iga sõber maskab:",osa,"€")
os.system("pause")