import MuMoodul
names,dates,firstRows,secondRows,emails=MuMoodul.loe_pas_ja_log("konto")
while True:
    valik=int(input("Valige\n0) Lugege andmebaasi\n1) Lisage konto ja saatke e-postiga\n2) Saatke nende tulemused mis tahes kontole\n3) Välju\n"))
    if valik==0:
        print(MuMoodul.loe_pas_ja_log("konto"))
    elif valik==1:
        MuMoodul.konto_loomine("konto",names,dates,firstRows,secondRows,emails)
    elif valik==2:
        indeks=names.index(input("Kellele peaksite selle saatma?\n"))
        MuMoodul.saatke_iseloomustus(names[indeks], firstRows[indeks], secondRows[indeks], emails[indeks])
    elif valik==3:
        break