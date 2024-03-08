import smtplib, ssl
from email.message import EmailMessage

smtp_server = "smtp.gmail.com"
port = 587
sender_email = "othermodstactics@gmail.com"
to_email = "marina.oleinik@tthk.ee"
password = "rdle rkdb xmbu fygl"

# Create a secure SSL context
context = ssl.create_default_context()

msg=EmailMessage()
msg.set_content("Tere tulemast! See on Vladislav Kudriašev.")
msg['Subject']="TARpv23 Vlad"
msg['From']=sender_email
msg['To']=to_email

# Try to log in server and send email
try:
    server = smtplib.SMTP(smtp_server,port)
    server.ehlo() # Can be omitted
    server.starttls(context=context) # Secure the connection
    server.ehlo() # Can be omitted
    server.login(sender_email, password)
    server.sendmail(msg)
except Exception as e:
    print(e)
finally:
    server.quit()