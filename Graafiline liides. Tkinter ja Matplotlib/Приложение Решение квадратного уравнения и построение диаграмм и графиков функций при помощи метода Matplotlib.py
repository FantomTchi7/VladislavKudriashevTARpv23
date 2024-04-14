from tkinter import *
from tkinter import messagebox as mb
import matplotlib.pyplot as plt
import math,numpy

# Variables
x=500
y=500
bg="#000000"
fg="#00FF00"
height=0
roundTo=2
step=5
canGraph=False
K=True

# Functions
def solve():
    global canGraph
    aa=a.get()
    bb=b.get()
    cc=c.get()
    if not (aa=="" or bb=="" or cc==""):
        D=round((float(bb)**2)-(4*float(aa)*float(cc)),roundTo)
        if D>0:
            x1=round((-(float(bb))+math.sqrt(D))/(2*float(aa)),roundTo)
            x2=round((-(float(bb))-math.sqrt(D))/(2*float(aa)),roundTo)
            solution.configure(text=f"D={D}\nX₁={x1}\nX₂={x2}")
            canGraph=True
        elif D==0:
            x=round(-(float(bb))/(2*float(aa)),roundTo)
            solution.configure(text=f"D={D}\nX={x}")
            canGraph=True
        else:
            solution.configure(text=f"D={D}\nLahendusi pole")
            canGraph=False
    else:
        mb.showwarning("Tähelepanu!","On vaja sisestada numbreid!")

def graph():
    """
    """
    if canGraph==True:
        aa=float(a.get())
        bb=float(b.get())
        cc=float(c.get())
        x=round(-(bb)/(2*aa),roundTo)
        x1=numpy.arange(x-step,x+step,float(f"0.{step}"))
        y=aa*x**2+bb*x1+cc
        y1=aa*x1**2+bb*x1+cc
        fig=plt.figure()
        plt.plot(x1,y1,'b-d')
        plt.title('Ruutvõrrand')
        plt.ylabel('y')
        plt.xlabel('x')
        plt.grid(True)
        plt.show()
    elif canGraph==False:
        mb.showwarning("Tähelepanu!","Ei saa lahendada.")

def extend():
    """
    """
    global K, whaleRB, frogRB, butterflyRB, solve2B, choice
    if K==True:
        K=not K
        extendB.config(text="Kahanda aken")
        choice=IntVar()
        whaleRB=Radiobutton(window,
            text="Vaal",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=1,
            command=choice)
        frogRB=Radiobutton(window,
            text="Konn",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=2,
            command=choice)
        butterflyRB=Radiobutton(window,
            text="Liblikas",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=3,
            command=choice)
        solve2B=Button(window,
            text="Näita graafikut",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            command=solve2)
        whaleRB.pack(side="top")
        frogRB.pack(side="top")
        butterflyRB.pack(side="top")
        solve2B.pack(side="top")
    elif K==False:
        K=not K
        extendB.config(text="Pikenda aken")
        whaleRB.destroy()
        frogRB.destroy()
        butterflyRB.destroy()
        solve2B.destroy()

def solve2():
    """
    """
    var=choice.get()
    if var==1:
        fish()
    elif var==2:
        frog()
    elif var==3:
        butterfly()
    else:
        pass

def fish():
    """
             .
            ":"
          ___:____   |"\/"|
      ,'        `.    \  /
      |  O        \___/  |
    ~^~^~^~^~^~^~^~^~^~^~^~^~
    """
    x1=numpy.arange(0,9.5,0.5)
    y1=(2/27)*x1**2-3
    x2=numpy.arange(-10,0.5,0.5)
    y2=0.04*x2**2-3
    x3=numpy.arange(-9,-2.5,0.5)
    y3=(2/9)*(x3+6)**2+1
    x4=numpy.arange(-3,9.5,0.5)
    y4=(-1/12)*(x4-3)**2+6
    x5=numpy.arange(5,9,0.5)
    y5=(1/9)*(x5-5)**2+2
    x6=numpy.arange(5,7.95,0.05)
    y6=(1/8)*(x6-7)**2+1.5
    x7=numpy.arange(-13,-8.5,0.5)
    y7=(-0.75)*(x7+11)**2+6
    x8=numpy.arange(-15,-12.5,0.5)
    y8=(-0.5)*(x8+13)**2+3
    x9=numpy.arange(-15,-9.5,0.5)
    y9=[1]*len(x9)
    x10=numpy.arange(3,4,0.5)
    y10=[3]*len(x10)
    whaleGraph=plt.figure()
    for i in range(10):
        plt.plot(locals()[f'x{i+1}'],locals()[f'y{i+1}'])
    plt.title('Vaal')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show()

def frog():
    """
    """
    x1=numpy.arange(-7,7,0.5)
    y1=(-3/49)*x1**2+8
    x2=numpy.arange(-7,7,0.5)
    y2=(4/49)*x2**2+1
    x3=numpy.arange(-6.8,-2,0.5)
    y3=(-0.75)*(x3+4)**2+11
    x4=numpy.arange(2,6.8,0.5)
    y4=(-0.75)*(x4-4)**2+11
    x5=numpy.arange(-5.8,-2.8,0.5)
    y5=(-x5+4)**2+4
    x6=numpy.arange(2.8,5.8,0.5)
    y6=(-x6-4)**2+4
    x7=numpy.arange(-4,4,0.5)
    y7=(4/9)*x7**2-5
    x8=numpy.arange(-5.2,5.2,0.5)
    y8=(4/9)*x8**2-9
    x9=numpy.arange(-7,-2.8,0.5)
    y9=(-1/16)*(x9+3)**2-6
    x10=numpy.arange(2.8,7,0.5)
    y10=(-1/16)*(x10-3)**2-6
    x11=numpy.arange(7,0,0.5)
    y11=(1/9)*(x11+4)**2-11
    x12=numpy.arange(0,7,0.5)
    y12=(1/9)*(x12-4)**2-11
    x13=numpy.arange(-7,-4.5,0.5)
    y13=(-x13+5)**2
    x14=numpy.arange(4.5,7,0.5)
    y14=(-x14-5)**2
    x15=numpy.arange(-3,3,0.5)
    y15=(2/9)*x15**2+2
    frogGraph=plt.figure()
    for i in range(15):
        plt.plot(locals()[f'x{i+1}'],locals()[f'y{i+1}'])
    plt.title('Konn')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show()

def butterfly():
    """
    """
    x1=numpy.arange(-9,-1,0.5)
    y1=(-1/8)*(x1+9)**2+8
    x2=numpy.arange(1,9,0.5)
    y2=(-1/8)*(x2-9)**2+8
    x3=numpy.arange(-9,-8,0.5)
    y3=7*(x3+8)**2+1
    x4=numpy.arange(8,9,0.5)
    y4=7*(x4-8)**2+1
    x5=numpy.arange(-8,-1,0.5)
    y5=(1/49)*(x5+1)**2
    x6=numpy.arange(1,8,0.5)
    y6=(1/49)*(x6-1)**2
    x7=numpy.arange(-8,-1,0.5)
    y7=(-4/49)*(x7+1)**2
    x8=numpy.arange(1,8,0.5)
    y8=(-4/49)*(x8-1)**2
    x9=numpy.arange(-8,-2,0.5)
    y9=(1/3)*(x9+5)**2-7
    x10=numpy.arange(2,8,0.5)
    y10=(1/3)*(x10-5)**2-7
    x11=numpy.arange(-2,-1,0.5)
    y11=(-2)*(x11+1)**2-2
    x12=numpy.arange(1,2,0.5)
    y12=(-2)*(x12-1)**2-2
    x13=numpy.arange(-1,1,0.5)
    y13=(-1)*(4*x13**2)+2
    x14=numpy.arange(-1,1,0.5)
    y14=4*x14**2-6
    x15=numpy.arange(-2,0,0.5)
    y15=(-1)*(1.5*x15)+2
    x16=numpy.arange(0,2,0.5)
    y16=1.5*x16+2
    butterflyGraph=plt.figure()
    for i in range(16):
        plt.plot(locals()[f'x{i+1}'],locals()[f'y{i+1}'])
    plt.title('Liblikas')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show()

# Create window and stuff

window=Tk()
window.geometry(f"{x}x{y}")
window.title("Ruutvõrrandid")
label=Label(window,
    text="Ruutvõrrandite lahendused",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=x)
frame=Frame(window)
a=Entry(frame,
    bg=bg,
    fg=fg,
    font="Arial 24",
    width=2,
    justify=CENTER)
first=Label(frame,
    text="x²+",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=3)
b=Entry(frame,
    bg=bg,
    fg=fg,
    font="Arial 24",
    width=2,
    justify=CENTER)
second=Label(frame,
    text="x+",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=2)
c=Entry(frame,
    bg=bg,
    fg=fg,
    font="Arial 24",
    width=2,
    justify=CENTER)
third=Label(frame,
    text="=0",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=2)
solveB=Button(frame,
    text="Lahenda",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=7,
    command=solve)
graphB=Button(frame,
    text="Näita graafikut",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=12,
    command=graph)
solution=Label(window,
    text="",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=x)
extendB=Button(window,
    text="Pikenda aken",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=x,
    command=extend)

# Pack stuff

label.pack(side="top")
frame.pack(side="top")
solution.pack(side="top")
extendB.pack(side="top")

# Display stuff

a.grid(row=0,column=1)
first.grid(row=0,column=2)
b.grid(row=0,column=3)
second.grid(row=0,column=4)
c.grid(row=0,column=5)
third.grid(row=0,column=6)
solveB.grid(row=0,column=7)
graphB.grid(row=0,column=8)

# Display window

window.mainloop()