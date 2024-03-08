import string
import random

def summa(arv1:int,arv2:int,arv3:int=0)->int:
    """ Funktsioon tagastab kolme arvu summa
        summa(arv1,arv2,arv3)

    :param int arv1: Arv 1 sisestab kasutaja
    :param int arv2: Arv 2 sisestab kasutaja
    :param int arv3: Arv 3 vaikimisi arv 3 võrdub nulliga
    :rtype: int
    """
    s=arv1+arv2+arv3
    return s

def tüüpkontroll(inp) -> str:
    """ Funktsioon prindib väärtuse tüübi inp
    
    :param inp: inp sisestab kasutaja
    :rtype: str
    """
    try:
        inp=int(inp)
        return int
    except:
        try:
            inp=float(inp)
            return float
        except:
            try:
                inp=str(inp)
                return str
            except:
                pass

def arithmetic(arv1:int,arv2:int,operatsioon:str)->any:
    """võtab 3 argumenti: esimesed 2 on numbrid, kolmas on toiming, mis tuleb nendega sooritada. Kui kolmas argument on +, lisa need; kui -, siis lahuta; * - korrutada; / - jaga (esimene teisega). Muudel juhtudel tagastage string "Tundmatu toiming".
    
    :param int arv1: Arv 1 sisestab kasutaja
    :param int arv2: Arv 2 sisestab kasutaja
    :param str operatsioon: operatsioon sisestab kasutaja
    :rtype: any
    """
    if operatsioon=="+":
        return arv1+arv2
    elif operatsioon=="-":
        return arv1-arv2
    elif operatsioon=="*":
        return arv1*arv2
    elif operatsioon=="/":
        if not(arv2==0):
            return arv1/arv2
        else:
            return "Nulliga jagada ei saa"
    else:
        return "Tundmatu toiming"
    
def is_year_leap(aasta:int)->bool:
    """võtab 1 argumendi - aasta ja tagastab Tõene, kui aasta on liigaasta, ja False muul juhul.

    :param int aasta: Aasta sisestab kasutaja
    :rtype: bool
    """

# Praktiline töö "Palgad"

def andmed_veerudes(inimesed:list,palgad:list):
    """Funktsioon kuvab ekraanile kahe järjendite andmed veerudes
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    """
    for i in range(len(inimesed)):
        print(inimesed[i],"-",palgad[i])

def inimesete_ja_palkade_lisamine(inimesed:list,palgad:list,n=1)->any:
    """Funktsioon tagastab uuendatud loendid, kus lisatud inimesi ja palka
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :param int n: Inimeste arv
    :rtype: list,list
    """
    if n>0:
        for i in range(n):
            nimi=input("Nimi: ").capitalize()
            palk=int(input("Palk: "))
            inimesed.append(nimi)
            palgad.append(palk)
    return inimesed,palgad

def andmete_eemaldamine_nimi_jargi(inimesed:list,palgad:list)->any:
    """Funktsioon kustutab andmeid ja tagastab listid.
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: list,list
    """
    nimi=input("Sisesta nimi: ")
    for i in range(len(inimesed)):
        if nimi in inimesed:
            inimesed.remove(nimi)
            palgad.pop(i)
    return inimesed,palgad

def kellel_on_suurim_palk(inimesed:list,palgad:list)->int:
    """Funktsioon näitab kellel on suurim palk
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: int
    """
    suurim_palk=max(palgad)
    for i in range(len(palgad)):
        if palgad[i]==suurim_palk:
            print(inimesed[i],"palk on",suurim_palk)
    return suurim_palk

def kellel_on_vaiksem_palk(inimesed:list,palgad:list)->int:
    """Funktsioon näitab kellel on väiksem palk
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: int
    """
    vaiksem_palk=min(palgad)
    for i in range(len(palgad)):
        if palgad[i]==vaiksem_palk:
            print(inimesed[i],"palk on",vaiksem_palk)
    return vaiksem_palk

def sorteerimineA_Z(inimesed:list,palgad:list)->any:
    """Funktsioon sorteerib mõlemad loendid.
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: list,list
    """
    for i in range(0,len(inimesed)):
        for j in range(i,len(inimesed)):
            if palgad[i]>palgad[j]:
                palgad[j],palgad[i]=palgad[i],palgad[j]
                inimesed[j],inimesed[i]=inimesed[i],inimesed[j]
    return inimesed,palgad

def sorteerimineZ_A(inimesed:list,palgad:list)->any:
    """Funktsioon sorteerib mõlemad loendid.
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: list,list
    """
    for i in range(0,len(inimesed)):
        for j in range(i,len(inimesed)):
            if palgad[i]<palgad[j]:
                palgad[j],palgad[i]=palgad[i],palgad[j]
                inimesed[j],inimesed[i]=inimesed[i],inimesed[j]
    return inimesed,palgad

def vordsed_palgad(inimesed:list,palgad:list)->list:
    """Uurib välja, kes sama palka saavad, leiab, kui palju selliseid inimesi ja kuvab nende andmed ekraanile.
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: list
    """
    nimed=[]
    for palk in palgad:
        n=palgad.count(palk)
        i=palgad.index(palk)
        if n>1:
            subnimed=[]
            for j in range(n):
                nimi=inimesed[palgad.index(palk,i)]
                subnimed.append(nimi)
                palgad.pop(i)
                inimesed.pop(i)
                i+=1
            nimed.append(subnimed)
    print(nimed)
    return nimed

def palk_nimi_jargi(inimesed:list,palgad:list)->list:
    """Teeb palgaotsingu inimese nime järgi. Arvestab, et nimed võivad korduda.
    
    :param list inimesed: Inimeste järjend
    :param list palgad: Palgade järjend
    :rtype: list
    """
    nimi=input("Sisesta nimi: ")
    nimed=[]
    for i in range(len(palgad)):
        if nimi in inimesed[i]:
            nimed.append([inimesed[i],palgad[i]])
    print(nimed)
    return nimed

# Iseseisevtöö "Registreerimine ja autoriseerimine"

def konto_loomine(nimed:list,paroolid:list)->any:
    """Selleks kulub kaks loendit, inimeste paroolid, väljundis redigeeritakse neid ja luuakse kasutaja, kus kasutajal ja paroolil on sama indeks.
    
    :param list nimed: Nimete järjend
    :param list paroolid: Paroolide järjend
    :rtype: list,list
    """
    nimi=input("Sisesta nimi: ")
    if nimi in nimed:
        print("Konto on juba olemas.")
    else:
        valik=int(input("1) Sisestage parool ise. 2) Looge juhuslikult parool.\n"))
        if valik==1:
            parool=input("Sisesta parool: ")
        elif valik==2:
            randomparool=list(string.printable)
            random.shuffle(randomparool)
            parool = ''.join([random.choice(randomparool) for x in range(12)])
            print(parool)
            randomparool=list(string.printable)
            random.shuffle(randomparool)
            parool = ''.join([random.choice(randomparool) for x in range(12)])
            print(parool)
        nimed.append(nimi)
        paroolid.append(parool)
        print("Konto loomine õnnestus.")
        print([nimi,parool])
    return nimed,paroolid

def kontole_sisse_logida(nimed:list,paroolid:list,on_autoriseeritud:str)->str:
    """Konto sisselogimise funktsioon kontrollib loendis olevaid kasutajaid, tagastab sisestatud kasutaja nime "on_autoriseeritud".
    
    :param list nimed: Nimete järjend
    :param list paroolid: Paroolide järjend
    :param str on_autoriseeritud: Sisselogitud kasutaja nimi; kui kontole sisselogimist pole, tagastab see "False"
    :rtype: str
    """
    if on_autoriseeritud=="False":
        nimi=input("Sisesta nimi: ")
        if not(nimi in nimed):
            print("Selle nimega kontot pole.")
        for i in range(len(nimed)):
            if nimed[i]==nimi:
                parool=input("Sisesta parool: ")
                if paroolid[i]==parool:
                    print("Login successful")
                    on_autoriseeritud=nimi
                else:
                    print("Vale parool.")
    return on_autoriseeritud

def muuda_salasona(nimed:list,paroolid:list,on_autoriseeritud:str)->any:
    """Sisselogitud kasutaja parooli muutmine.
    
    :param list nimed: Nimete järjend
    :param list paroolid: Paroolide järjend
    :param str on_autoriseeritud: Kontrollib, kas kasutaja on sisse logitud
    :rtype: list,list
    """
    if not(on_autoriseeritud=="False"):
        indeks=nimed.index(on_autoriseeritud)
        parool=input("Sisesta oma parool: ")
        if parool==paroolid[indeks]:
            valik=int(input("1) Sisestage parool ise. 2) Looge juhuslikult parool.\n"))
            if valik==1:
                parool=input("Sisesta uus parool: ")
            elif valik==2:
                randomparool=list(string.printable)
                random.shuffle(randomparool)
                parool = ''.join([random.choice(randomparool) for x in range(12)])
                print(parool)
            nimi=nimed[indeks]
            nimed.pop(indeks)
            paroolid.pop(indeks)
            nimed.append(nimi)
            paroolid.append(parool)
        else:
            print("Vale parool.")
    else:
        print("Pole autoriseeritud")
    return nimed,paroolid

def muuda_nimi(nimed:list,paroolid:list,on_autoriseeritud:str)->str:
    """Muudab kasutajanime ja lisab selle uusimasse indeksisse. Tagastab uue kasutajanime "on_autoriseritud".
    
    :param list nimed: Nimete järjend
    :param list paroolid: Paroolide järjend
    :param str on_autoriseeritud: Kontrollib, kas kasutaja on sisse logitud
    :rtype: str
    """
    if not(on_autoriseeritud=="False"):
        indeks=nimed.index(on_autoriseeritud)
        parool=input("Sisesta oma parool: ")
        if parool==paroolid[indeks]:
            nimi=input("Sisesta uus nimi: ")
            parool=paroolid[indeks]
            paroolid.pop(indeks)
            nimed.pop(indeks)
            nimed.append(nimi)
            paroolid.append(parool)
            on_autoriseeritud=nimi
        else:
            print("Vale parool.")
    else:
        print("Pole autoriseeritud")
    return on_autoriseeritud