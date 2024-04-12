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
K=True

# Functions
def solve():
    aa=a.get()
    bb=b.get()
    cc=c.get()
    if not (aa=="" or bb=="" or cc==""):
        D=round((float(bb)**2)-(4*float(aa)*float(cc)),roundTo)
        if D>0:
            x1=round((-(float(bb))+math.sqrt(D))/(2*float(aa)),roundTo)
            x2=round((-(float(bb))-math.sqrt(D))/(2*float(aa)),roundTo)
            solution.configure(text=f"D={D}\nX₁={x1}\nX₂={x2}")
            graph()
        elif D==0:
            x=round(-(float(bb))/(2*float(aa)),roundTo)
            solution.configure(text=f"D={D}\nX={x}")
            graph()
        else:
            solution.configure(text=f"D={D}\nLahendusi pole")
    else:
        mb.showwarning("Tähelepanu!","On vaja sisestada numbreid!")

def graph():
    """
    """
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

def extend():
    """
    """
    global K, whale, frog, butterfly, solve2B, choice
    if K==True:
        K=not K
        extendB.config(text="Shrink window")
        choice=IntVar()
        whale=Radiobutton(window,
            text="Whale",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=1,
            command=choice)
        frog=Radiobutton(window,
            text="Frog",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=2,
            command=choice)
        butterfly=Radiobutton(window,
            text="Butterfly",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x,
            variable=choice,
            value=3,
            command=choice)
        solve2B=Button(window,
            text="Lahenda2",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=7,
            command=solve2)
        whale.pack(side="top")
        frog.pack(side="top")
        butterfly.pack(side="top")
        solve2B.pack(side="top")
    elif K==False:
        K=not K
        extendB.config(text="Extend window")
        whale.destroy()
        frog.destroy()
        butterfly.destroy()
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
    plt.plot(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5,x6,y6,x7,y7,x8,y8,x9,y9,x10,y10)
    plt.title('Whale')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show()

def frog():
    """
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
    frogGraph=plt.figure()
    plt.plot(x1,y1,x2,y2,x3,y3,x4,y4,x5,y5,x6,y6,x7,y7,x8,y8,x9,y9,x10,y10)
    plt.title('Frog')
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
solution=Label(window,
    text="",
    bg=bg,
    fg=fg,
    font="Arial 24",
    height=height,
    width=x)
extendB=Button(window,
    text="Extend window",
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

# Display window

window.mainloop()