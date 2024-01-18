error=str("Vale väärtus.")
print("Tere! Olen sinu uus sõber - Python!")
print("Mis on sinu nimi?")
nimi=str(input())
print(nimi+", oi kui ilus nimi!")
print(nimi+"! Kas leian Sinu keha indeksi? 0-ei, 1-jah => ")
try:
    nõus=bool(int(input()))
    # bool() выдаёт True при любом ответе, int() делает так что-бы 0 возвращало False, а 1 (или больше) возвращало True
    if nõus==True:
        print("Mis on sinu pikkus?")
        try:
            pikkus=int(input())
            print("Mis on sinu mass?")
            try:
                mass=int(input())
                indeks=mass/(0.01*pikkus)**2
                print(str(nimi)+"! Sinu keha indeks on:",round(indeks,1))
                if indeks<16:
                    print("Sul on tervisele ohtlik alakaal.")
                elif indeks<=19:
                    print("Sul on alakaal.")
                elif indeks<=25:
                    print("Sul on normaalkaal.")
                elif indeks<=30:
                    print("Sul on ülekaal.")
                elif indeks<=35:
                    print("Sul on rasvumine.")
                elif indeks<=40:
                    print("Sul on tugev rasvumine.")
                elif indeks>40:
                    print("Sul on tervisele ohtlik rasvumine.")
                else:
                    print("Kahju! See on väga kasulik info!")
                    print()
            except:
                print(error)
        except:
            print(error)
except:
    print(error)
# Нету ValueError так как я на VSCode, а в VSCode дебаггер имеет приоретет над интерпритатором Python
print("Kohtumiseni, " + nimi + "! Igavesti Sinu, Python!")