import datetime,smtplib,ssl
from email.message import EmailMessage

def IntToListInt(arv:int)->list:
    """Võtab sisendiks täisarvu ja teisendab selle listiks, kus iga elemendiks on arvu kohal olev number.

    :param arv: Täisarv, mida soovitakse muuta listiks.
    :type arv: int
    :return: List täisarvudest, mis moodustavad sisendiks olnud arvu.
    :rtype: list
    """
    arv=list(str(arv))
    for i in range(len(arv)):
        arv[i]=int(arv[i])
    return arv

def CountNumber(arv:int)->list:
    """Funktsioon võtab sisendiks arvu ja tagastab loendi, kus on iga numbri esinemise arv antud arvus.

    :param arv: Täisarv, mida soovite analüüsida.
    :return: Loend, mis sisaldab igast numbrist leitud esinemiste arvu vastavalt antud arvus.
    """
    whateverthefrickthisis=[]
    for i in range(10):
        if not(str(arv).count(str(i))<=0):
            whateverthefrickthisis.append(str(arv).count(str(i))*str(i))
        else:
            whateverthefrickthisis.append(f"Ei ole {i}")
    return whateverthefrickthisis

def kirjuta_failisse(fail:str, kuupaev:datetime.date, nimi:str, esimeneRida:int, teineRida:int, email:str):
    """Kirjutab andmed faili.

    :param fail: Faili nimi, kuhu andmed salvestatakse.
    :param kuupaev: Kuupäev, mis lisatakse andmete hulka.
    :param nimi: Nimi, mida soovite faili lisada.
    :param esimeneRida: Esimene rida, mida soovite faili lisada.
    :param teineRida: Teine rida, mida soovite faili lisada.
    :param email: E-posti aadress, mida soovite faili lisada.
    :return: None
    """
    try:
        f=open(fail,'a',encoding="utf-8")
        f.write(f"{nimi} {kuupaev}  {esimeneRida}   {teineRida}    {email}\n")
        f.close()
    except Exception as e:
        print(e)

def loe_pas_ja_log(fail: str) -> tuple:
    """Funktsioon loeb failist andmed, mis on formaadis "kasutajanimi:parool:meil" iga rea kohta eraldi.

    :param fail: Faili nimi, mida lugeda.
    :type fail: str
    :return: Tuple, mis sisaldab nimekirju nimedest, kuupäevadest, esimestest ridadest, teistest ridadest ja e-posti aadressidest.
    :rtype: tuple
    """
    fail=open(fail,"r",encoding="utf-8")
    name=[]
    dates=[]
    firstRows=[]
    secondRows=[]
    emails=[]
    for line in fail:
        n=line.find(" ")
        p=line.find("  ")
        s=line.find("   ")
        d=line.find("    ")
        name.append(line[0:n].strip())
        dates.append(line[n+1:p].strip())
        firstRows.append(line[p+1:s].strip())
        secondRows.append(line[s+1:d].strip())
        emails.append(line[d+1:].strip())
    fail.close()
    return name,dates,firstRows,secondRows,emails

def konto_loomine(konto:str,names:list,dates:list,firstRows:list,secondRows:list,emails:list):
    """Loo konto kasutajale ja salvesta selle andmed. 
    
    Funktsioon küsib kasutajalt nime, e-posti aadressi ja sünnikuupäeva, 
    arvutab sünnikuupäeva ja e-posti põhjal esimese ja teise rea andmed, 
    lisab need vastavatesse listidesse ning salvestab need faili ja 
    saadab iseloomustuse e-posti teel.
    
    :param konto: Faili nimi, kuhu konto andmed salvestatakse.
    :param names: Nimekiri kontode omanike nimedest.
    :param dates: Nimekiri sünnikuupäevadest.
    :param firstRows: Nimekiri esimestest ridadest.
    :param secondRows: Nimekiri teistest ridadest.
    :param emails: Nimekiri e-posti aadressidest.

    :return: None
    """
    nimi=input("Sisesta nimi: ")
    email=input("Sisesta email: ")
    aasta=int(input("Sisesta aasta: "))
    kuu=int(input("Sisesta kuu: "))
    paev=int(input("Sisesta päev: "))
    kuupaev=datetime.date(aasta,kuu,paev)

    aastaEraldatud=IntToListInt(kuupaev.year)
    kuuEraldatud=IntToListInt(kuupaev.month)
    paevEraldatud=IntToListInt(kuupaev.day)

    aastaSumma=sum(aastaEraldatud)
    kuuSumma=sum(kuuEraldatud)
    paevSumma=sum(paevEraldatud)

    # 1) Складываем цифры дня и месяца рождения: 5+1+2=8.
    # 2) Складываем цифры года: 1+9+7+9=26.
    # 3) Складываем полученные числа: 8+26=34 - это 1 рабочее число.
    int1=paevSumma+kuuSumma+aastaSumma
    # 4) Складываем цифры первого рабочего числа: 3+4=7 - это 2 рабочее число.
    int2=sum(IntToListInt(int1))
    # 5) Из первого рабочего числа вычитаем удвоенную первую цифру дня рождения: 34-2*5=24 - это 3 рабочее число.
    int3=int1-(int(paevEraldatud[0])*2)
    # 6) Складываем цифры третьего рабочего числа: 2+4=6 - это 4 рабочее число. 
    int4=sum(IntToListInt(int3))
    esimeneRida=int(str(kuupaev.day)+str(kuupaev.month)+str(kuupaev.year))
    teineRida=int(str(int1)+str(int2)+str(int3)+str(int4))

    names.append(nimi)
    dates.append(kuupaev)
    firstRows.append(esimeneRida)
    secondRows.append(teineRida)
    emails.append(email)
    kirjuta_failisse(konto, str(kuupaev), nimi, esimeneRida, teineRida, email)
    saatke_iseloomustus(nimi, esimeneRida, teineRida, email)

def saatke_iseloomustus(nimi, esimeneRida, teineRida, email)->any:
    """Saadab iseloomustuse e-kirjana.

    Saadab e-kirja, mis sisaldab isiklikku iseloomustust antud nimele
    ja kahte rida iseloomustusega.

    :param nimi: Isiku nimi, kellele iseloomustus saadetakse.
    :param esimeneRida: Esimene iseloomustuse rida.
    :param teineRida: Teine iseloomustuse rida.
    :param email: Isiku e-posti aadress, kuhu iseloomustus saadetakse.

    :return: None
    """
    try:
        sender_email=email
        to_email="othermodstactics@gmail.com"
        password="alar oalu xkvu wycl"
        smtp_server="smtp.gmail.com"
        port=587
        context=ssl.create_default_context()
        with smtplib.SMTP(smtp_server, port) as server:
            server.ehlo()
            server.starttls(context=context)
            server.login(sender_email,password)
            msg=EmailMessage()
            msg.set_content(f"Tere {nimi},\nSinu iseloomustus on:\n{CountNumber(esimeneRida)}\n ja:\n{CountNumber(teineRida)}")
            msg['Subject']="Sinu iseloomustus!"
            msg['From']=sender_email
            msg['To']=to_email
            server.send_message(msg)
        print("E-kiri saadetud edukalt.")
    except Exception as e:
        print(f"E-kirja saatmisel tekkis viga: {e}")