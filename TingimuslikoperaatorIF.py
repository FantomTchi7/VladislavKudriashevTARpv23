print("Mis sinu nimi on?")
nimi=input()
if nimi.capitalize()=="Juku":
    print("Kui vana Juku on?")
    vanus=int(input())
    if vanus<0 or vanus>100:
        print("Yuku pole "+str(vanus)+"-aastane.")
    elif vanus<6:
        print("tasuta pilet")
    elif vanus<=14:
        print("lastepilet")
    elif vanus<=65:
        print("täispilet")
    else:
        print("sooduspilet")