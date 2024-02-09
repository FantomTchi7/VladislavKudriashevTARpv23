import string

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

for d in range(len(nimed)):
    if nimed[d]==nimed[d+1]:
        lmaoidk

nimed.remove(nimi)
nimed.append(nimi)
for n in range(len(nimed)):
    print(f"{n+1}. {nimed[n]}")