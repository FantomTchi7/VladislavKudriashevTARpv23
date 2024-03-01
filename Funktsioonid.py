import MuMoodul
#b=int(input("Sisesta arv2: "))
#summa_3=MuMoodul.summa(3,b,int(input("Kolmas arv: ")))
#summa_31=MuMoodul.summa(100,100)
#
#print(summa_3)
#print(summa_31)
#
#a=input()
#print(MuMoodul.tüüpkontroll(a))
#
# Praktiline töö "Palgad"
palgad=[1200,2500,750,395,1200]
inimesed=["A","B","C","D","E"]

while True:
    print("0 - Näita andmed\n1 - Andmete lisamine\n2 - Andmete kutsutamine\n3 - Suurim palk\n4 - Väiksem palk\n5 - Sorteerimine (A-Z)\n6 - Sorteerimine (Z-A)\n7 - Sama palk\n8 - Otsi nimi palkiga")
    valik=int(input())
    if valik==1:
        inimesed,palgad=MuMoodul.inimesete_ja_palkade_lisamine(inimesed,palgad,int(input("Mitu inimest lisame? ")))
        MuMoodul.andmed_veerudes(inimesed,palgad)
    elif valik==0:
        MuMoodul.andmed_veerudes(inimesed,palgad)
    elif valik==2:
        MuMoodul.andmete_eemaldamine_nimi_jargi(inimesed,palgad)
    elif valik==3:
        MuMoodul.kellel_on_suurim_palk(inimesed,palgad)
    elif valik==4:
        MuMoodul.kellel_on_vaiksem_palk(inimesed,palgad)
    elif valik==5:
        MuMoodul.sorteerimineA_Z(inimesed,palgad)
    elif valik==6:
        MuMoodul.sorteerimineZ_A(inimesed,palgad)
    elif valik==7:
        MuMoodul.vordsed_palgad(inimesed,palgad)
    elif valik==8:
        MuMoodul.palk_nimi_jargi(inimesed,palgad)