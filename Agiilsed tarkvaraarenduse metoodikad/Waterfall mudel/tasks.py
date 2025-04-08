class Task:
    def __init__(self, taskName, taskDescription, taskStatus):
        self.taskName = taskName
        self.taskDescription = taskDescription
        self.taskStatus = taskStatus

def printTasks(taskList):
    if len(taskList) == 0:
        print("No tasks available.")
    else:
        for i, task in enumerate(taskList):
            print(f"[{i + 1}] {task.taskName} - {task.taskDescription} - {task.taskStatus}")

def addTask(taskList):
    taskName = input("Enter task name: ")
    taskDescription = input("Enter task description: ")
    taskStatus = input("Enter task status: ")
    newTask = Task(taskName, taskDescription, taskStatus)
    taskList.append(newTask)
    print(f"Task '{taskName}' added.")

def removeTask(taskList):
    printTasks(taskList)
    while True:
        try:
            taskIndex = int(input("Enter the task number to remove: ")) - 1
            if 0 <= taskIndex < len(taskList):
                removedTask = taskList.pop(taskIndex)
                print(f"Task '{removedTask.taskName}' removed.")
                break
            else:
                print("Invalid task number.")
        except ValueError:
            print("Invalid input. Please enter a number.")

def main():
    global taskList
    taskList = []
    while True:
        print("Select operation:")
        print("[1] View tasks")
        print("[2] Add task")
        print("[3] Remove task")
        print("[9] Exit")

        while True:
            try:
                inputValue = int(input())
                break
            except ValueError:
                print("Invalid input. Please enter a number.")

        if inputValue == 1:
            printTasks(taskList)
        elif inputValue == 2:
            addTask(taskList)
        elif inputValue == 3:
            removeTask(taskList)
        elif inputValue == 9:
            exit()

main()