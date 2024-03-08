import MuMoodul

nimed=[]
paroolid=[]
on_autoriseeritud="False"
# Не работает если есть одинаковые ники, но я уже устал так что, уф.
while True:
    print("0) Näita kontosid.\n1) Loo konto\n2) Logige oma kontole sisse\n3) Muutke parool\n4) Muuda nime\n5) Parooli taastamine")
    if on_autoriseeritud=="False":
        print("Not logged in.")
    else:
        print("Logged in as",on_autoriseeritud)
    valik=int(input())
    if valik==0:
        print([nimed,paroolid])
    elif valik==1:
        MuMoodul.konto_loomine(nimed,paroolid)
    elif valik==2:
        on_autoriseeritud=MuMoodul.kontole_sisse_logida(nimed,paroolid,on_autoriseeritud)
    elif valik==3:
        MuMoodul.muuda_salasona(nimed,paroolid,on_autoriseeritud)
    elif valik==4:
        on_autoriseeritud=MuMoodul.muuda_nimi(nimed,paroolid,on_autoriseeritud)
    elif valik==5:
        MuMoodul.parooli_taastamine(nimed,paroolid,on_autoriseeritud)