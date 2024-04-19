import random
from tkinter import *
from tkinter import font

scalingF=500
flagW=scalingF*11/7
flagH=flagW/2
window=Tk()
window.title("Images")
window['bg']="#FFFFFF"

# Harjutus. Lipud
# Estonia
Estonia=Canvas(window,width=flagW,height=scalingF,background="#FFFFFF")
Estonia.grid(row=0,column=0,sticky=S)
Estonia.create_rectangle(0,0,flagW,scalingF,fill="#FFFFFF")
Estonia.create_rectangle(0,0,flagW,scalingF/3*2,fill="#000000")
Estonia.create_rectangle(0,0,flagW,scalingF/3,fill="#0072CE")
# Bahamas
Bahamas=Canvas(window,width=flagW,height=flagH,background="#00778B")
Bahamas.grid(row=1,column=0,sticky=S)
Bahamas.create_rectangle(0,0,flagW,flagH,fill="#00778B")
Bahamas.create_rectangle(0,flagW/3,flagW,flagH/3,fill="#FFC72C")
triangle=[0,0,0,flagH,flagH,flagW/4]
Bahamas.create_polygon(triangle,fill="#000000")

# Harjutus. Muster
# Red Square,Yellow Circle
rsyc=Canvas(window,width=scalingF/2,height=scalingF/2,background="#FFFFFF")
rsyc.grid(row=0,column=1)
def draw_rsyc(canvas,x,y,size,depth):
    """
    """
    if depth<=0:
        return
    cR=size/2
    cX=x+cR
    cY=y+cR
    iSS=cR*2/(2**0.5)
    iSX=cX-iSS/2
    iSY=cY-iSS/2
    canvas.create_rectangle(x,y,x+size,y+size,outline="#000000",fill="#FF0000")
    canvas.create_oval(cX-cR,cY-cR,cX+cR,cY+cR,outline="#000000",fill="#FFFF00")
    draw_rsyc(canvas,iSX,iSY,iSS,depth-1)
draw_rsyc(rsyc,0,0,scalingF/2,20)
# Chess board
cB=Canvas(window,width=scalingF/2,height=scalingF/2,background="#FFFFFF")
cB.grid(row=0,column=2)
cB.create_rectangle(0,0,scalingF/2,scalingF/2,fill="#FFFFFF")
cS=scalingF/2/8
for i in range(8):
    for j in range(8):
        if (i+j)%2 == 0:
            cB.create_rectangle(i*cS,j*cS,(i+1)*cS,(j+1)*cS,fill="#000000")
# Whatever that is
colors=["black","cyan","magenta","red","blue","gray","yellow","green","lightblue","pink","gold"]
x0=0
y0=0
x1=scalingF
y1=scalingF
idk=Canvas(window,width=scalingF,height=scalingF,background="#FFFFFF")
idk.grid(row=0,column=1,rowspan=2,columnspan=2,sticky=S)
for i in range(150):
    x0+=2
    y0=x0
    x1-=2
    y1=x1
    idk.create_oval(x0,y0,x1,y1,fill=random.choice(colors))

# Harjutus. Valgusfoor
mayTheForceBeWithYou=Canvas(window,width=scalingF/2,height=scalingF+flagH,background="#FFFFFF")
mayTheForceBeWithYou.grid(row=0,column=3,rowspan=2,sticky=S)
suur_font = font.Font(family='Algerian',size=scalingF//25,weight='bold')
mayTheForceBeWithYou.create_text(scalingF/4,scalingF//25,text="VALGUSFOOR",font=suur_font,anchor=CENTER)

mayTheForceBeWithYou.create_rectangle(scalingF/2/3,scalingF//25*2,scalingF/3,scalingF*2/3,fill="#000000")
mayTheForceBeWithYou.create_rectangle(scalingF/2/5+scalingF//10,scalingF//25*2,scalingF/5*2-scalingF//10,scalingF*2,fill="#000000")
red=mayTheForceBeWithYou.create_oval(scalingF/2/3,scalingF//25*2,scalingF/3,scalingF//2/2,fill="#7F0000")
yellow=mayTheForceBeWithYou.create_oval(scalingF/2/3,scalingF//25*2+(scalingF*2/3-scalingF//2/2)/(3+1)*2,scalingF/3,scalingF//2/2+(scalingF*2/3-scalingF//2/2)/(3+1)*2,fill="#7F7F00")
green=mayTheForceBeWithYou.create_oval(scalingF/2/3,scalingF//25*2+2*(scalingF*2/3-scalingF//2/2)/(3+1)*2,scalingF/3,scalingF//2/2+2*(scalingF*2/3-scalingF//2/2)/(3+1)*2,fill="#007F00")

var=IntVar()

haveYouHeardOfTheGreatPlaguesTheWise=Canvas(window)
haveYouHeardOfTheGreatPlaguesTheWise.grid(row=1,column=3)

def choice(var:int):
    """
    """
    if var==1:
        red.configure(fill="#000000")
    elif var==2:
        pass
    elif var==3:
        pass
    else:
        pass


redRB=Radiobutton(haveYouHeardOfTheGreatPlaguesTheWise,
    text="Red",
    bg="#FFFFFF",
    fg="#000000",
    font=f"Algerian {scalingF//25}",
    height=0,
    width=10,
    variable=var,
    value=1,
    command=choice)
yellowRB=Radiobutton(haveYouHeardOfTheGreatPlaguesTheWise,
    text="Yellow",
    bg="#FFFFFF",
    fg="#000000",
    font=f"Algerian {scalingF//25}",
    height=0,
    width=10,
    variable=var,
    value=2,
    command=choice)
greenRB=Radiobutton(haveYouHeardOfTheGreatPlaguesTheWise,
    text="Green",
    bg="#FFFFFF",
    fg="#000000",
    font=f"Algerian {scalingF//25}",
    height=0,
    width=10,
    variable=var,
    value=3,
    command=choice)

redRB.pack()
yellowRB.pack()
greenRB.pack()

window.mainloop()