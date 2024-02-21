import string
viga="Vale väärtus."

print("\nÜlesanne #1\n")

vokaali=["A","E","I","O","U","Õ","Ä","Ö","Ü"]
konsonanti=["Q","W","R","T","P","S","D","F","G","H","K","L","Z","X","C","V","B","N","M"]
märkid=list(string.punctuation)
sõna=list(input("Sisesta sõna või lause:\n").upper())
v=k=m=t=0
for sümbol in sõna:
    if sümbol in vokaali:
        v+=1
    elif sümbol in konsonanti:
        k+=1
    elif sümbol in märkid:
        m+=1
    elif sümbol ==" ":
        t+=1
print("Vokaali:",v,"\nKonsonanti:",k,"\nKirjuvahemärgid:",m,"\nTühikud",t)

print("\nÜlesanne #2\n")

nimed=[]
for i in range(5):
    nimi=input(f"Sisestage nimi №{i+1}:\n").capitalize()
    nimed.append(nimi)
nimed.sort()
töödeldud = [] 
for i in nimed: 
    if i not in töödeldud: 
        töödeldud.append(i) 
töödeldud.remove(nimi)
töödeldud.append(nimi)
for i in range(len(töödeldud)):
    print(f"{i+1}. {töödeldud[i]}")
vanused = töödeldud.copy()
try:
    for i in range(len(vanused)):
        vanus=int(input(f"Sisestage {töödeldud[i]}i vanus: "))
        vanused.insert(i,vanus)
        vanused.pop()
except ValueError:
    print(viga)
for i in range(len(vanused)):
    print(f"{i+1}. {töödeldud[i]}i vanus on {vanused[i]}")
print("Maksimaalne vanus:",max(vanused))
print("Minimaalne vanus:",min(vanused))
print("Summaarne vanus:",sum(vanused))
vanus=0
for i in range(len(vanused)):
    vanus+=vanused[i]
vanus=vanus/(i+1)
print("Keskmine vanus:",vanus)

print("\nÜlesanne #7\n")

numbrid=[]
try:
    numbriarv=int(input("Sisestage numbrite arv: "))
    for i in range(numbriarv):
        try:
            number=abs(int(input(f"Sisestage number {i+1}: ")))
            numbrid.append(number)
        except ValueError:
            print(viga)
    numbrid.sort()
    print(numbrid)
    numbrid.reverse()
    print(numbrid)
except ValueError:
    print(viga)

print("\nÜlesanne #9\n")

nimi=input("Mis on sinu nimi?\n")
if nimi.isalpha():
    print("Tere,",nimi.capitalize()+"!")
    tähtarv=len(nimi)
    print("Tähtede arv:",tähtarv)
    v=k=0
    for sümbol in nimi.upper():
        if sümbol in vokaali:
            v+=1
        elif sümbol in konsonanti:
            k+=1
    print("Vokaali:",v,"\nKonsonanti:",k)
    tähtsorteeritud=sorted(nimi.lower())
    print("Tähed tähestiku järjekorras:",tähtsorteeritud)
else:
    print(viga)

print("\nÜlesanne #11\n")

abc=list(string.ascii_lowercase)
tähtarv=int(input("Sisestage tähesarjade arv:\n"))
for i in range(len(abc)):
    if i >= tähtarv:
        break
    täht=abc[i]*(i+1)
    print(täht)