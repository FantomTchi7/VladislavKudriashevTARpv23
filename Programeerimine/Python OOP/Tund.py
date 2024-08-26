class Vlad:
    def hello(self):
        print(f"Hello {self.name}!")
    
    def greetWithTitle(self):
        print(f"Hello {self.title} {self.name}!")

    def greetFriend(self, friendName):
        print(f"Hello {friendName}")

    def makeOlder(self, byHowMuch):
        self.age += byHowMuch

    def __init__(self, name, title, age):
        print("Initializing student..")
        self.name = name
        self.title = title
        self.age = age

s = Vlad("Vlad","duke",17)
s.greetWithTitle()
print(s.age)
s.makeOlder(5)
print(s.age)

#ago = Student("Ago", "Sir")
#print(ago.name)

#leela = Student("Leela", "Captain")
#print(leela.title)

#s = Vlad()
#print(type(s))
#print(id(s))
#s.hello()
#s.greetFriend("David")
#
#t = Vlad()
#print(type(t))
#print(id(t))
#
#if s==t:
#    y=True
#else:
#    y=False
#print(y)