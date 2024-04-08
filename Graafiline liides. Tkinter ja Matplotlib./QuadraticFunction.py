from tkinter import *
import math

# Variables
x=600
y=200
bg="#000000"
fg="#00FF00"
height=0

# Functions
def solve():
    aa=int(a.get())
    bb=int(b.get())
    cc=int(c.get())
    D=(bb**2)-(4*aa*cc)
    if D>0:
        x1=(abs(bb)+math.sqrt(D))/(2*aa)
        x2=(abs(bb)-math.sqrt(D))/(2*aa)
        solution.configure(text=f"D={D}\nX₁={x1}\nX₂={x2}")
    elif D==0:
        x=abs(bb)/(2*aa)
        solution.configure(text=f"D={D}\nX={x}")
    else:
        solution.configure(text=f"D={D}\nРешений нет")

window=Tk()
window.geometry(f"{x}x{y}")
window.title("Квадратные уравнения")
label=Label(window,
            text="Решения квадратного уравнения",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=x)
frame=Frame(window)
a=Entry(frame,
              bg=bg,
              fg=fg,
              font="Times 20",
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
              font="Times 20",
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
              font="Times 20",
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
            text="Решить",
            bg=bg,
            fg=fg,
            font="Arial 24",
            height=height,
            width=6,
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