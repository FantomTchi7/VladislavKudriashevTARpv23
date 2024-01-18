# Ülesanne 1

print("Mis on sinu nimi?")
nimi=input()
if nimi.capitalize()=="Juku":
    print("Kui vana Juku on?")
    vanus=int(input())
    if vanus<0 or vanus>100:
        print("Yuku pole "+str(vanus)+"-aastane.")
    elif vanus<6:
        print("Te peate ostma tasuta pileti.")
    elif vanus<=14:
        print("Te peate ostma lastepileti.")
    elif vanus<=65:
        print("Te peate ostma täispileti.")
    else:
        print("Te peate ostma sooduspileti.")

# Ülesanne 2

print("Mis on esimese naabri nimi?")
nimi1=str(input())
print("Mis on teise naabri nimi?")
nimi2=str(input())
print("Täna "+nimi1+" ja "+nimi2+" on pinginaabrid.")

# Ülesanne 3

print("Kui pikk on esimene sein (meetrites)?")
sein1=int(input())
print("Kui pikk on teine sein (meetrites)?")
sein2=int(input())
põrand=sein1*sein2
print("Teie põranda suurus on "+str(põrand)+"m²")
print("Kas soovite renoveerimist?")
nõus=str(input())
if nõus.capitalize()=="Jah":
    print("Kui palju maksab ruutmeeter?")
    maksruut=int(input())
    hind=maksruut*põrand
    print("Põranda vahetamise hind on "+str(hind)+"€")

# Ülesanne 4
