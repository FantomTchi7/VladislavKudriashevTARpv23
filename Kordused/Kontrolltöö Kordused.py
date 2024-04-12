import random
viga="Vale väärtus."

print("Ülesanne #1")

try:
    n=int(input("Sisestage majade arv (1 kuni 9):\n"))
    if not (n<1 or n>9):
        for i in range(n):
            print("  ~~~~~ ")
            print(" /_____\ ")
            print(" | []  | ")
            print("  ----- ")
    else:
        print(viga)
except ValueError:
    print(viga)

print("Ülesanne #2")

try:
    klassiõpilasedarv=int(input("Sisesta 1. klassi õpilaste arv:\n"))
    try:
        klassi2õpilasedarv=int(input("Sisesta 2. klassi õpilaste arv:\n"))
        klassihindedarv=0
        klassi2hindedarv=0
        for i in range(klassiõpilasedarv):
            klassihindedarv=klassihindedarv+random.randint(2,5)
        for i in range(klassi2õpilasedarv):
            klassi2hindedarv=klassi2hindedarv+random.randint(2,5)
        print("1. klassi õpilaste keskmine hinne on:",klassihindedarv/klassiõpilasedarv)
        print("2. klassi õpilaste keskmine hinne on:",klassi2hindedarv/klassi2õpilasedarv)
    except ValueError:
        print(viga)
except ValueError:
    print(viga)

print("Ülesanne #3")

klassiõpilasedarv=random.randint(5,45)
maksimaalnehinne=5
minimaalnehinne=2
for i in range(klassiõpilasedarv):
    hinne=random.randint(minimaalnehinne*100,maksimaalnehinne*100)/100
    print(hinne)
    if hinne>maksimaalnehinne:
        hinne=maksimaalnehinne
    elif hinne<minimaalnehinne:
        hinne=minimaalnehinne
print("Klassi õpilaste maksimaalne hinne on:",maksimaalnehinne)
print("Klassi õpilaste minimaalne hinne on:",minimaalnehinne)

print("Ülesanne #4")

S2=0
N2=0
for i in range(13):
    S=random.randint(5,10)
    N=random.randint(50000,100000)
    print(f"Maakond {i}: Inimeste arv on {N} ja piirkond on {S}km².")
    S2=S2+S
    N2=N2+N
print("Piirkonna keskmine asustustihedus on",N2//S2,"inimest ruutkilomeetril.")

print("Ülesanne #5")

try:
    minimaalneX=int(input("Sisestage minimaalne X:\n"))
    try:
        maksimaalneX=int(input("Sisestage maksimaalne X:\n"))
        samm=0.5
        print("   x | y")
        praeguX=minimaalneX
        while praeguX<=maksimaalneX:
            y=-0.5*praeguX+praeguX
            print(f"{praeguX:4}",end=" | ")
            print(y)
            praeguX+=samm
    except:
        print(viga)
except:
    print(viga)

#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

while True:
    try:
        K=int(input("Mitu kotleti sul on? "))
        if K>0: break
    except ValueError:
        print("Vale tüüp.")
while True:
    try:
        M=int(input("Mitu kotleti ühel pannil? "))
        if M>0: break
    except ValueError:
        print("Vale tüüp.")
pann=0
while K>M:
    K-=M
    pann+=1
    print(f"Praetud: {pann} tk")
    if K<M:
        pann+=1
        print(f"Praetud: {pann} tk")
print(f"Kokku oli praetud: {pann} pannid")
print()

N=25
kesk1=0
kesk2=0
for i in range(N):
    h1=random.randint(1,5)
    h2=random.randint(1,5)
    kesk1+=h1
    kesk2+=h2
kesk1/=N
kesk2/=N
print(f"Keskmine hinne 1 klassis on {kesk1}")
print(f"Keskmine hinne 2 klassis on {kesk2}")

sum_num=0
sum_km=0
for i in range(12):
    num=random.randint(1000,100000)
    km=random.randint(1,100)
    sum_num+=num
    sum_km+=km
    print(f"{i}. maakond. \nElanikud: {num}. Pindala: {km}\nKokku: {sum_num}, {sum_km}")
vastus=sum_num/sum_km
print(f"Keskmine: {vastus:.3f}")

mitu=int(input("Mitu?"))
for i in range(mitu):
    print('  /V\ '.center(10,' '))
    print('  / V \ '.center(10,' '))
    print('  / V V \ '.center(10,' '))
    print(' /VV V VV\ '.center(10,' '))
print()