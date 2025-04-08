logs = []

def calc(a, b, opr):
    ans = 0

    if not(isinstance(a, int)):
        try:
            a = int(a)
        except:
            print("A is of an unsupported type!")
            return None
    if not(isinstance(b, int)):
        try:
            b = int(b)
        except:
            print("B is of an unsupported type!")
            return None

    if opr == "addition":
        ans = a + b
    elif opr == "subtraction":
        ans = a - b
    elif opr == "multiplication":
        ans = a * b
    elif opr == "division":
        try:
            ans = int(a / b)
        except ZeroDivisionError:
            print("Cannot divide by 0!")
            return None

    logs.append("User has done " + str(opr) + " and got an answer: " + str(ans))

    return ans

def logsShow():
    for i in range(len(logs)):
        print(logs[i])

def main():
    num1 = 0
    num2 = 0
    opr = ""
    while True:
        print("[1] Addition")
        print("[2] Subtraction")
        print("[3] Multiplication")
        print("[4] Division")
        print("[8] Show logs")
        print("[9] Exit")
        print("Input operation type: ")
        opr = int(input())

        if opr != 8 and opr != 9:
            print("Input the first number: ")
            num1 = int(input())
            print("Input the second number: ")
            num2 = int(input())
            
            if opr == 1:
                result = calc(num1, num2, "addition")
            elif opr == 2:
                result = calc(num1, num2, "subtraction")
            elif opr == 3:
                result = calc(num1, num2, "multiplication")
            elif opr == 4:
                result = calc(num1, num2, "division")
            else:
                print("Invalid operation!")
                return None
            
            print("Answer: ")
            print(result)
        elif opr == 9:
            exit()
        else:
            logsShow()

main()