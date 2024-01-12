import random

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