import streamlit as st

if "tasks" not in st.session_state:
    st.session_state.tasks = []

st.title("To-do List")

def addTask():
    taskName = st.text_input("Task Name")
    taskDescription = st.text_area("Task Description")
    taskStatus = st.selectbox("Task Status", ["Not Started", "In Progress", "Completed"])
    
    if st.button("Add Task"):
        newTask = {
            "name": taskName,
            "description": taskDescription,
            "status": taskStatus
        }
        st.session_state.tasks.append(newTask)
        st.success(f"Task '{taskName}' added.")