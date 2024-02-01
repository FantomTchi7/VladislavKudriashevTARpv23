import random

# Ülesanne 1

print("Mis on sinu nimi?")
name=input()
if name.capitalize()=="Juku":
   print("Kui vana Juku on?")
   age=int(input())
   if age<0 or age>100:
       print("Yuku pole "+str(age)+"-aastane.")
   elif age<6:
       print("Te peate ostma tasuta pileti.")
   elif age<=14:
       print("Te peate ostma lastepileti.")
   elif age<=65:
       print("Te peate ostma täispileti.")
   else:
       print("Te peate ostma sooduspileti.")

# Ülesanne 2

print("Mis on esimese naabri name?")
name1=str(input())
print("Mis on teise naabri name?")
name2=str(input())
print("Täna "+name1+" ja "+name2+" on pinginaabrid.")

# Ülesanne 3

print("Kui pikk on esimene sein (meetrites)?")
wall1=int(input())
print("Kui pikk on teine sein (meetrites)?")
wall2=int(input())
floor=wall1*wall2
print("Teie põranda suurus on "+str(floor)+"m²")
print("Kas soovite renoveerimist?")
agreed=str(input())
if agreed.capitalize()=="Jah":
   print("Kui palju maksab ruutmeeter?")
   squared=int(input())
   price=squared*floor
   print("Põranda vahetamise hind on "+str(price)+"€")

# Ülesanne 4

price=int(input("Sisesta toote hind:\n"))
if price>=700:
    price=price-price*0.3
print("Toote hind soodustusega: "+str(price))

# Ülesanne 5

temperature=int(input("Mis on teie toatemperatuur?\n"))
if temperature>18:
    print("Temperatuur üle 18 kraadi.")
elif temperature==18:
    print("Temperatuur on 18 kraadi.")
else:
    print("Temperatuur alla 18 kraadi.")

# Ülesanne 6 ja 7

height=input("Kas sa on lühike, keskmine või pikk?\n")
gender=input("Kas sa oled mees või naine?\n")
print("Sa oled "+str(height),str(gender))

# Ülesanne 8

milk=random.randint(1,20)
bread=random.randint(1,10)
cheese=random.randint(1,50)
print("Piim maksab "+str(milk)+"€") 
print("Leib maksab "+str(bread)+"€")
print("Juust maksab "+str(cheese)+"€")
agreed=input("Kas soovite piima, leiba ja juustu osta?\n")
if agreed.capitalize()=="Jah":
    milk2=int(input("Mitu piima tükki sa osta?\n"))
    bread2=int(input("Mitu leiba tükki sa osta?\n"))
    cheese2=int(input("Mitu juustu tükki sa osta?\n"))
    print("Hind on "+str(milk*milk2+bread*bread2+cheese*cheese2)+"€")

# Ülesanne 9

side1=int(input("Esimine pool\n"))
side2=int(input("Teine pool\n"))
side3=int(input("Kolmas pool\n"))
side4=int(input("Neljas pool\n"))
if side1==side2==side3==side4:
    print("See on ruut.")

# Ülesanne 10

number1=int(input("Sisestage esimene number:\n"))
number2=int(input("Sisestage teine ​​number:\n"))
agreed=input("Mida sa tahad teha ( +, -, * või / )?\n")
if agreed=="+":
    print(number1+number2)
elif agreed=="-":
    print(number1-number2)
elif agreed=="*":
    print(number1*number2)
elif agreed=="/":
    print(number1/number2)
else:
    pass

# Ülesanne 11

day1=int(input("Mis päev täna on?\n"))
month1=input("Mis kuu täna on?\n")
year1=int(input("Mis aasta täna on?\n"))
day2=int(input("Mis on teie sünnipäevade päev?\n"))
month2=input("Mis on teie sünnipäevade kuu?\n")
year2=int(input("Mis on teie sünnipäevade aasta?\n"))
if (year1-year2)%5==0:
    print("Head juubelit!")
if day1==day2 and month1==month2:
    print("Palju õnne sünnipäevaks!")

# Ülesanne 12

price=int(input("Sisesta toote hind:\n"))
if price<=10:
    print("Toote hind soodustusega: "+str(price-price*0.1)+"€\n")
elif price>10:
    print("Toote hind soodustusega: "+str(price-price*0.2)+"€\n")

# Ülesanne 13

gender=input("Kas sa oled mees või naine?\n")
if gender.capitalize()=="Mees":
    age=int(input("Kui vana sa oled?\n"))
    if age>16 and age<18:
        print("Sa läbisid!\n")
    else:
        print("Sa ei läbinud!\n")
else:
    print("Sa ei läbinud!\n")

# Ülesanne 14

volume=int(input("Bussi maht:\n"))
ppl=int(input("Inimesi arv:\n"))
ba=round(ppl/volume)
ba=1/volume
if ppl%volume==0:
    ba+=1
vb=ppl%volume
print("Kokku on vaja "+str(ba)+" bussi ja viimasel sõidavadm "+str(vb)+" inimest.")