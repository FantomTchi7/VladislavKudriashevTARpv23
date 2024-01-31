import random

print("Ülesanne #1")

try:
    n=int(input("Sisestage majade arv (1 kuni 9):\n"))
    if not (n<1 or n>9):
        for i in range(n):
            print("  ~~~~~ ")
            print(" /_____\ ")
            print(" | []  | ")
            print("  ----- ")
    else:
        print("Viga")
except ValueError:
    print("Viga")

print("Ülesanne #2")

try:
    klassi1õpilasedarv=int(input("Sisesta 1. klassi õpilaste arv:\n"))
    try:
        klassi2õpilasedarv=int(input("Sisesta 2. klassi õpilaste arv:\n"))
        klassi1hindedarv=0
        klassi2hindedarv=0
        for i in range(klassi1õpilasedarv):
            klassi1hindedarv=klassi1hindedarv+random.randint(2,5)
        for i in range(klassi2õpilasedarv):
            klassi2hindedarv=klassi2hindedarv+random.randint(2,5)
        print("1. klassi õpilaste keskmine hinne on:",klassi1hindedarv/klassi1õpilasedarv)
        print("2. klassi õpilaste keskmine hinne on:",klassi2hindedarv/klassi2õpilasedarv)
    except ValueError:
        print("Viga")
except ValueError:
    print("Viga")

print("Ülesanne #5")

minimaalne_x=1
maksimaalne_x=3
samm=0.5
print("x | y")
praegu_x=minimaalne_x
while praegu_x<=maksimaalne_x:
    y=-0.5*praegu_x+praegu_x
    print(f"{praegu_x} | {y}")
    praegu_x+=samm