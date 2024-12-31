import string,random,smtplib,ssl
from email.message import EmailMessage

# Практическая работа "Создание пользовательских функций"

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

def parooli_keerukuse_kontroll(password):
    """Kontrollib, kas antud parool vastab keerukuse kriteeriumidele.

    :param str parool: Kontrollitav parool.
    :return: Tuple, kus esimene element on True või False, olenevalt sellest, kas parool vastab kriteeriumidele või mitte. Teine element on vastav sõnum vastavalt kontrolli tulemusele.
    :rtype: tuple
    """
    if len(password)<8:
        return False,"Parool peab olema vähemalt 8 tähemärki pikk."
    if not any(char.isupper() for char in password):
        return False,"Parool peab sisaldama vähemalt ühte suurtähte."
    if not any(char.islower() for char in password):
        return False,"Parool peab sisaldama vähemalt ühte väiketähte."
    if not any(char.isdigit() for char in password):
        return False,"Parool peab sisaldama vähemalt ühte numbrit."
    if not any(char in string.punctuation for char in password):
        return False,"Parool peab sisaldama vähemalt ühte erimärki."
    return True,"Parool vastab keerukuse nõuetele."

def konto_loomine(nimed:list,paroolid:list,emails:list)->any:
    """Funktsioon loob kasutaja konto, kus kasutajal ja paroolil on sama indeks.

    :param list nimed: Nimede järjend
    :param list paroolid: Paroolide järjend
    :param list emails: Emailide järjend
    :rtype: tuple
    """
    nimi=input("Sisesta nimi: ")
    if nimi in nimed:
        print("Konto on juba olemas.")
    else:
        valik=int(input("1) Sisestage parool ise. 2) Looge juhuslikult parool.\n"))
        if valik==1:
            while True:
                parool=input("Sisesta parool: ")
                keeruline,sonum=parooli_keerukuse_kontroll(parool)
                if keeruline:
                    break
                else:
                    print(sonum)
        elif valik==2:
            parool=''.join(random.choices(string.ascii_letters+string.digits,k=12))
            print(parool)
        nimed.append(nimi)
        paroolid.append(parool)
        email=input("Sisesta email: ")
        emails.append(email)
        print("Konto loomine õnnestus.")
        print(nimi,parool,email)
    return nimed,paroolid,emails

def kontole_sisse_logida(nimed:list,paroolid:list,on_autoriseeritud:str)->str:
    """Funktsioon kontrollib loendis olevaid kasutajaid ja tagastab sisselogitud kasutaja nime.

    :param list nimed: Nimede järjend
    :param list paroolid: Paroolide järjend
    :param str on_autoriseeritud: Sisselogitud kasutaja nimi; kui kontole sisselogimist pole, tagastab "False"
    :rtype: str
    """
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

def muuda_salasona(nimed:list,paroolid:list,emails:list,on_autoriseeritud:str)->any:
    """Funktsioon muudab sisselogitud kasutaja parooli.

    :param list nimed: Nimede järjend
    :param list paroolid: Paroolide järjend
    :param list emails: Emailide järjend
    :param str on_autoriseeritud: Kontrollib, kas kasutaja on sisse logitud
    :rtype: tuple
    """
    if not(on_autoriseeritud=="False"):
        indeks=nimed.index(on_autoriseeritud)
        parool=input("Sisesta oma parool: ")
        if parool==paroolid[indeks]:
            valik=int(input("1) Sisestage parool ise. 2) Looge juhuslikult parool.\n"))
            if valik==1:
                while True:
                    parool=input("Sisesta parool: ")
                    keeruline,sonum=parooli_keerukuse_kontroll(parool)
                    if keeruline:
                        break
                    else:
                        print(sonum)
            elif valik==2:
                parool=''.join(random.choices(string.ascii_letters+string.digits,k=12))
                print(parool)
            nimi=nimed[indeks]
            email=emails[indeks]
            nimed.pop(indeks)
            paroolid.pop(indeks)
            emails.pop(indeks)
            nimed.append(nimi)
            paroolid.append(parool)
            emails.append(email)
        else:
            print("Vale parool.")
    else:
        print("Pole autoriseeritud")
    return nimed,paroolid,emails

def muuda_nimi(nimed:list,paroolid:list,emails:list,on_autoriseeritud:str)->str:
    """Funktsioon muudab kasutajanime ja tagastab uue kasutajanime.

    :param list nimed: Nimede järjend
    :param list paroolid: Paroolide järjend
    :param list emails: Emailide järjend
    :param str on_autoriseeritud: Kontrollib, kas kasutaja on sisse logitud
    :rtype: str
    """
    if not(on_autoriseeritud=="False"):
        indeks=nimed.index(on_autoriseeritud)
        parool=input("Sisesta oma parool: ")
        if parool==paroolid[indeks]:
            nimi=input("Sisesta uus nimi: ")
            parool=paroolid[indeks]
            email=emails[indeks]
            paroolid.pop(indeks)
            nimed.pop(indeks)
            emails.pop(indeks)
            nimed.append(nimi)
            paroolid.append(parool)
            emails.append(email)
            on_autoriseeritud=nimi
        else:
            print("Vale parool.")
    else:
        print("Pole autoriseeritud")
    return on_autoriseeritud

def parooli_taastamine(nimed:list,paroolid:list,emails:list,on_autoriseeritud:str)->any:
    """Funktsioon taastab parooli ja saadab selle e-kirjana.

    :param list nimed: Nimede järjend
    :param list paroolid: Paroolide järjend
    :param list emails: Emailide järjend
    :param str on_autoriseeritud: Kontrollib, kas kasutaja on sisse logitud
    """
    if not(on_autoriseeritud=="False"):
        indeks=nimed.index(on_autoriseeritud)
        valik=int(input("1) Sisestage parool ise. 2) Looge juhuslikult parool.\n"))
        if valik==1:
            while True:
                parool=input("Sisesta parool: ")
                keeruline,sonum=parooli_keerukuse_kontroll(parool)
                if keeruline:
                    break
                else:
                    print(sonum)
        elif valik==2:
            parool=''.join(random.choices(string.ascii_letters+string.digits,k=12))
        else:
            print("Vigane valik.")
        paroolid[indeks]=parool
        try:
            smtp_server="smtp.gmail.com"
            port=587
            sender_email=emails[indeks]
            password="alar oalu xkvu wycl"
            to_email="othermodstactics@gmail.com"
            context=ssl.create_default_context()
            with smtplib.SMTP(smtp_server, port) as server:
                server.ehlo()
                server.starttls(context=context)
                server.login(sender_email,password)
                msg=EmailMessage()
                msg.set_content(f"Sinu uus parool on {parool}")
                msg['Subject']="Sinu uus parool!"
                msg['From']=sender_email
                msg['To']=to_email
                server.send_message(msg)
            print("E-kiri saadetud edukalt.")
        except Exception as e:
            print(f"E-kirja saatmisel tekkis viga: {e}")
    else:
        print("Pole autoriseeritud")

def loe_failist(fail:str)->list:
    """Funktsioon loeb failist read ja salvestab need järjendisse. Tagastab järjendi.

    :param str fail: Faili nimi
    :rtype: list
    """
    try:
        f=open(fail,'r',encoding="utf-8")
        järjend=[]
        for rida in f:
            järjend.append(rida.strip())
        f.close()
    except Exception as e:
        print(e)
    return järjend

def kirjuta_failisse(fail: str, nimed: list, paroolid: list, emails: list):
    """Funktsioon kirjutab andmed faili.

    :param str fail: Faili nimi
    :param list nimed: Kasutajanimede järjend
    :param list paroolid: Paroolide järjend
    :param list emails: Emailide järjend
    """
    try:
        f=open(fail,'w',encoding="utf-8")
        for nimi,parool,email in zip(nimed,paroolid,emails):
            f.write(f"{nimi}:{parool}|{email}\n")
        f.close()
    except Exception as e:
        print(e)

def loe_pas_ja_log(fail: str) -> tuple:
    """Funktsioon loeb failist andmed, mis on formaadis "login:password:email" igas reas eraldi.

    :param str fail: Faili nimi
    :rtype: tuple
    """
    fail=open(fail,"r",encoding="utf-8")
    log=[]
    pas=[]
    mail=[]
    for line in fail:
        n=line.find(":")
        p=line.find("|")
        log.append(line[0:n].strip())
        pas.append(line[n+1:p].strip())
        mail.append(line[p+1:].strip())
    fail.close()
    return log,pas,mail