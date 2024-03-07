import MuMoodul

nimed=[]
paroolid=[]
on_autoriseeritud="False"

while True:
    print("0) Näita kontosid.\n1) Konto loomine.\n2) Lisa konto.\n3) Logi sisse kontole.\n4)")
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