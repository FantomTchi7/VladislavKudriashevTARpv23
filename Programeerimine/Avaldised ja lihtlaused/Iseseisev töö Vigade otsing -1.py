import math # Убрал * from
print("Ruudu karakteristikud")
a=float(input('Sisesta ruudu külje pikkus => ')) # Добавил float()
S=a**2
print("Ruudu pindala", round(S,2))
P=4*a
print("Ruudu ümbermõõt", round(P,2)) # Исправил '' на "
di=a*math.sqrt(2) # Исправил название функции
print("Ruudu diagonaal", round(di,2))
print()
print("Ristküliku karakteristikud") # Убрал дополнительную скобку
b=float(input("Sisesta ristküliku 1. külje pikkus => ")) # Добавил float()
c=float(input("Sisesta ristküliku 2. külje pikkus => ")) # Добавил float()
S=b*c
print("Ristküliku pindala", round(S,2)) # Поставил в кавычки
P=2*(b+c) # Добавил *
print("Ristküliku ümbermõõt", round(P,2))
di=math.sqrt(b**2+c**2) # Заменил * на **
print("Ristküliku diagonaal", round(di,2)) # Добавил недостающую скобку, округлил до двух (сотых)
print()
print("Ringi karakteristikud")
r=float(input("Sisesta ringi raadiusi pikkus => ")) # Добавил float() недостающую скобку и исправил кавычки
d=2*r # Добавил символ умножения
print("Ringi läbimõõt",round(d,2)) # Добавил запятую
S=math.pi*r**2 # Добавил референс к модулю math, заменил * на ** и убрал ()
print("Ringi pindala", round(S,2))
C=2*math.pi*r # Добавил референс к модулю math и убрал (), исправил умножение
print("Ringjoone pikkus", round(C,2)) # Добавил недостающую скобку
# Глобально округлил до двух