print("*** NUMBRIDEGA MÄNGUD ***") # Перевёл на эстонский
print()
#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
while 1:
    try:
        a = abs(int(input("Sisestage täisarv => "))) # Перевёл на эстонский, исправил скобки
        break
    except ValueError:
        print("See ei ole täisarv") # Перевёл на эстонский
#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
if a==0:
    print("Nulliga pole mõtet midagi ette võtta") # Перевёл на эстонский
else:
#'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    print("Määrake, kui palju paaris ja mitu paaritu numbrit on arvus") # Перевёл на эстонский
    print()
    c=b=a #####
    paaris=0 ## Исправил на =
    paaritu=0 #
    while b > 0: # Заменил ; на :
            if b % 2 == 0: # Исправил на ==
                    paaris =+ 1
            else:
                    paaritu =+ 1
            b = b // 10 # Исправил отступ и исправил на =
    
    print("Paarisarvud:",str(paaris)) ##### Перевёл на эстонский, 
    print("Paaritud arvud:",str(paaritu)) # добавил соеденитель
    print()
#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    print("*Pöörake* sisestatud number ümber") # Перевёл на эстонский
    print()
    b=0
    while a > 0: # Добавил :
        number = a % 10
        a = a // 10
        b = b * 10
        b =+ number # Исправил отступ
    print("*Pööratud* number", b) # Перевёл на эстонский
    print()
#''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    print("Siracuse hüpoteesi testimine") # Перевёл на эстонский, убрал лишнюю скобку
    print()
    if c % 2 == 0: # Исправил на ==
        print("с - paarisarv. Jagage 2-ga.") # Перевёл на эстонский
    else:
        print("с - paaritu number. Korrutage 3-ga, lisage 1 ja jagage 2-ga.") # Перевёл на эстонский
    while c != 1:
            if c % 2 == 0:  # Исправил на ==
                    c = c / 2 # Исправил на =
            else:
                    c = (3*c + 1) / 2 # Исправил на =
            print("end =",c) # Исправил кавычки
    print()
    print("Hüpotees on õige") # Перевёл на эстонский, исправил кавычки