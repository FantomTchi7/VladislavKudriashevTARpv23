import random
nimed=["Mati","Meelis","Kati","Mati"]
while True:
    print("***********************")
    v=input("N-näita andmed\nL-lisada andmeid\nK-andmete kutsutamine\nH-Andmete haldus\n***********************\n")
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