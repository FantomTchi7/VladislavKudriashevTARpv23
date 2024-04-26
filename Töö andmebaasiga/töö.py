import json
from tkinter.ttk import Separator

jsonData = '{"nimi": "Vladislav Kudriashev", "vanus": 17, "prillid": false}'
data = json.loads(jsonData)

print(data)
print(data["nimi"])
for id_, dat in enumerate(data):
    print(id_, "-", dat)

for key, value in data.items():
    print(f"{key}: {value}")

data2 = {
    "nimi": "Anna",
    "vanus": 55,
    "abielus": True,
    "lapsed": [
        "Inna",
        "Mati"
    ],
    "koduloomad": None,
    "autod": [
        {
            "muudel": "BMW",
            "varv": "punane",
            "joud": 256,
            "number": "123 ABC"
        },
        {
            "muudel": "Ford",
            "varv": "must",
            "joud": 128,
            "number": "321 CBA"
        }
    ]}
print(json.dumps(data2, indent=2))

with open(".\Töö andmebaasiga\data_file.json","w") as wFile:
    json.dump(data2, wFile)
print("Andmed failist:")
with open(".\Töö andmebaasiga\data_file.json","r") as rFile:
    data2 = json.load(rFile)
print(data2)