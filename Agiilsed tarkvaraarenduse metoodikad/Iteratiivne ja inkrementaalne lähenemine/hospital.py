class Hospital:
    meetingsList = []
    patientsList = []
    doctorsList = []

    def showAllPatients(self, patientsList):
        for i, patient in enumerate(patientsList):
            if i == 0: print("-----------------------------")
            print("Patient " + str(i) + ":")
            print("Name: " + str(patient.name))
            print("Age: " + str(patient.age))
            print("-----------------------------")

    def showAllDoctors(self, doctorsList):
        for i, doctor in enumerate(doctorsList):
            if i == 0: print("-----------------------------")
            print("Doctor " + str(i) + ":")
            print("Name: " + str(doctor.name))
            print("Specialty: " + str(doctor.specialty))
            print("-----------------------------")

    def showAllMeetings(self, meetingsList):
        for i, meeting in enumerate(meetingsList):
            if i == 0: print("-----------------------------")
            print("Meeting " + str(i) + ":")
            print("Doctor's name: " + str(meeting.doctor.name))
            print("Patient's name: " + str(meeting.patient.name))
            print("Time: " + str(meeting.time))
            print("-----------------------------")

class Meeting:
    def __init__(self, patient, doctor, time):
        self.patient = patient
        self.doctor = doctor
        self.time = time

class Patient:
    def __init__(self, name, age):
        self.name = name
        self.age = age

class Doctor:
    def __init__(self, name, specialty):
        self.name = name
        self.specialty = specialty

hospital = Hospital()

Patient1 = Patient("Vlad", 18)
Patient2 = Patient("Erik", 17)
Patient3 = Patient("David", 17)
Patient4 = Patient("Bogdan", 17)
Patient5 = Patient("Timur", 20)

Doctor1 = Doctor("Octavius", "Neurosurgeon")
Doctor2 = Doctor("Doofenshmertz", "Surgeon")

Meeting1 = Meeting(Patient1, Doctor1, "10:00")
Meeting2 = Meeting(Patient2, Doctor2, "12:00")

hospital.patientsList.append(Patient1)
hospital.patientsList.append(Patient2)
hospital.patientsList.append(Patient3)
hospital.patientsList.append(Patient4)
hospital.patientsList.append(Patient5)

hospital.doctorsList.append(Doctor1)
hospital.doctorsList.append(Doctor2)

hospital.meetingsList.append(Meeting1)
hospital.meetingsList.append(Meeting2)

hospital.showAllDoctors(hospital.doctorsList)
hospital.showAllPatients(hospital.patientsList)
hospital.showAllMeetings(hospital.meetingsList)

f = open("hospitalDoctors", "a")
for doctor in hospital.doctorsList:
    f.writelines(doctor.name + " | " + doctor.specialty + "\n")
f.close()
f = open("hospitalPatients", "a")
for patient in hospital.patientsList:
    f.writelines(patient.name + " | " + "patient.age" + "\n")
f.close()
f = open("hospitalMeetings", "a")
for meeting in hospital.meetingsList:
    f.writelines(meeting.doctor.name + " | " + meeting.patient.name + " | " + meeting.time + "\n")
f.close()