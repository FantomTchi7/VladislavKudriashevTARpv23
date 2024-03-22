from tkinter import *
from datetime import *

# Variables
x=225
y=225
bg="#000000"
fg="#00FF00"

# Functions
k=0
def click():
    global k
    k+=1
    button.configure(text=k)
def click_(event): # Без ивента bind будет ругаться
    global k
    k-=1
    button.configure(text=k)
def tst_psse(event):
    t=textbox.get()
    label.configure(text=t)
    textbox.delete(0,END)
def choice():
    number=var.get()
    textbox.insert(END,number)

window=Tk()
window.geometry(f"{x}x{y}")
window.title(date.today())
#import os
#if "nt" == os.name: # Я сижу на GNOME, поэтому даже не знаю есть ли иконка или нет, на гноме иконки не отображаются.
#    window.iconbitmap(bitmap="myicon.ico")
#else:
#    window.iconbitmap(bitmap="@myicon.xbm")

# Elements
text="Hello world!"
label=Label(window,
            text=text,
            bg=bg,
            fg=fg,
            font="Times 20",
            height=3,
            width=x)
textbox=Entry(window,
              bg=bg,
              fg=fg,
              font="Times 20",
              width=x,
              justify=CENTER)
button=Button(window,
              text=k,
              font="Arial 20",
              height=3,
              width=x,
              relief=RAISED,
              command=click)
fr=Frame(window)
var=IntVar()
f=Radiobutton(fr,text="First",variable=var,value=1,command=choice)
s=Radiobutton(fr,text="Second",variable=var,value=2,command=choice)
t=Radiobutton(fr,text="Third",variable=var,value=3,command=choice)
# Events
button.bind("<Button-3>",click_)
textbox.bind("<Return>",tst_psse)
# Positioning

obj=[label,textbox,button,fr]
for i in obj:
    i.pack(side="top")
obj2=[f,s,t]
for i in range(len(obj2)):
    obj2[i].grid(row=0,column=i)

window.mainloop()