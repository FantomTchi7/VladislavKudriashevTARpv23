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

# Functions
def solve():
    aa=a.get()
    bb=b.get()
    cc=c.get()
    if not (aa=="" or bb=="" or cc==""):
        D=round((float(bb)**2)-(4*float(aa)*float(cc)),roundTo)
        if D>0:
            x1=round((abs(float(bb))+math.sqrt(D))/(2*float(aa)),roundTo)
            x2=round((abs(float(bb))-math.sqrt(D))/(2*float(aa)),roundTo)
            solution.configure(text=f"D={D}\nX₁={x1}\nX₂={x2}")
            graph()
        elif D==0:
            x=round(abs(float(bb))/(2*float(aa)),roundTo)
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
    x=round(abs(bb)/(2*aa),roundTo)
    sizing=[aa,bb,cc,x]
    x1=numpy.arange(min(sizing),max(sizing),0.5)
    y=aa*x**2+bb*x1+cc
    y1=aa*x1**2+bb*x1+cc
    fig=plt.figure()
    plt.plot(x1,y1,'r-d')
    plt.title('Ruutvõrrand')
    plt.ylabel('y')
    plt.xlabel('x')
    plt.grid(True)
    plt.show()

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
solve=Button(frame,
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

label.pack(side="top")
frame.pack(side="top")
solution.pack(side="top")

a.grid(row=0,column=1)
first.grid(row=0,column=2)
b.grid(row=0,column=3)
second.grid(row=0,column=4)
c.grid(row=0,column=5)
third.grid(row=0,column=6)
solve.grid(row=0,column=7)

window.mainloop()