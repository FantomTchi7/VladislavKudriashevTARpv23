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