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