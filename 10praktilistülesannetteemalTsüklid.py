# Ülesanne #1
#for

r=0
for i in range(1,16):
    arv=float(input("Sisesta {0} arv:\n".format(i)))
    if arv==int(arv):
        r+=1
print("Täisarvude arv on "+str(r))

#while True
#
#r=0
#i=0
#while True:
#    i+=1
#    arv=float(input("Sisesta {0} arv:\n".format(i)))
#    if arv==int(arv):
#        r+=1
#        if i==15: break
#print("Täisarvude arv on "+str(r))


#while tingimustega
#
#r=0
#i=0
#while i<15:
#    i+=1
#    arv=float(input("Sisesta {0} arv:\n".format(i)))
#    if arv==int(arv):
#        r+=1        
#print("Täisarvude arv on "+str(r))

# Ülesanne #2
#for

A = int(input("Sisestage arv:\n"))
sum_for = 0
for i in range(1, A+1):
    sum_for += i
print("While-tsüklit kasutav summa:", sum_for)

#while True
#
#A = int(input("Sisestage arv:\n"))
#sum_while_true = 0
#i = 1
#while True:
#    sum_while_true += i
#    i += 1
#    if i > A:
#        break
#print("While-tsüklit kasutav summa:", sum_while_true)


#while tingimustega
#
#A = int(input("Sisestage arv:\n"))
#sum_while = 0
#i = 1
#while i <= A:
#    sum_while += i
#    i += 1
#print("While-tsüklit kasutav summa:", sum_while)

# Ülesanne #3
#for

product_for = 1
for _ in range(8):
    num = int(input("Sisestage arv:\n"))
    if num > 0:
        product_for *= num
print("Toode, mis kasutab silmust:", product_for)

#while True
#
#product_while_true = 1
#i = 0
#while True:
#    num = int(input("Sisestage arv:\n"))
#    if num > 0:
#        product_while_true *= num
#    i += 1
#    if i == 8:
#        break
#print("Toode, mida kasutatakse True loopi ajal:", product_while_true)


#while tingimustega
#
#product_while = 1
#i = 0
#while i < 8:
#    num = int(input("Sisestage arv:\n"))
#    if num > 0:
#        product_while *= num
#    i += 1
#print("Toode, mis kasutab while tsüklit:", product_while)