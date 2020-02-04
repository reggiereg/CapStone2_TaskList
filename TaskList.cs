using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Capstone_TaskList
{
    /*Team member’s name
2. Brief description
3. Due date
4. Whether it’s been completed or not*/
    class TaskList
    {
        private int taskNum;
        private string name;
        private string description;
        private DateTime dueDate;
        private bool completed;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public bool Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        public int TaskNum
        {
            get { return taskNum; }
            set { taskNum = value; }
        }

        public TaskList()
        {

        }

        public TaskList(string name, string description, int taskNum)
        {
            this.name = name;
            this.description = description;
            this.taskNum = taskNum;
        }

        public TaskList(string name, string description, DateTime dueDate, bool completed, int taskNum)
        {
            this.name = name;
            this.description = description;
            this.dueDate = dueDate;
            this.completed = completed;
            this.taskNum = taskNum;
        }

        public static void ListTasks(List<TaskList> listOfTask)
        {
            foreach(TaskList items in listOfTask)
            {
                Console.WriteLine($"Task{items.taskNum}:\n     Assigned to: {items.name}\n     Description: {items.description}\n     Due Date: {items.dueDate}\n     Completed: {items.completed}");
            }
        }

        public static List<TaskList> AddTask(List<TaskList> listOfTask)
        {
            string name;
            string description;
            DateTime dueDate;
            bool isCompleted = false;
            int taskNum = listOfTask.Count + 1;

            name = CheckUserNameFormat(newTaskData("Who will this task be assigned to (enter in first and last name.  i.e Jon Doe): "));
            description = newTaskData("Please enter in a description for this task: ");
            dueDate = Date_D(newTaskData("What is the due date for this task (format should match xx/xx/xxxx): "));


            TaskList newItem = new TaskList(name, description, dueDate, isCompleted, taskNum);
            listOfTask.Add(newItem);

            return listOfTask;
        }

        public static List<TaskList> DeleteTask(string errorMessage, int itemNum, List<TaskList> listOfTasks)
        {
            string userInput;
            userInput = newTaskData($"Are you sure you want to delete this task:\n     Assigned to: {listOfTasks[itemNum].Name}\n     Description: {listOfTasks[itemNum].Description}\n     Due on: {listOfTasks[itemNum].Description}?\n[y/n]: ");


            if (userInput == "y")
            {
                listOfTasks.RemoveAt(itemNum);
                return listOfTasks;
            }

            else
            {
                Console.WriteLine("Cancelling the delete operation");
                return listOfTasks;
            }
        }

        private static string CheckUserNameFormat(string name)
        {
            if (!Regex.IsMatch(name, @"([A-Z])([a-z]+) ([A-Z])([a-z]){1,30}"))
            {
                return CheckUserNameFormat(newTaskData("Invalid format for name entry.  (ex.  Reginald Richardson).\nPlease type in the name again with the specified format"));
            }

            else
            {
                return name;
            }
        }

        public static bool ConfirmDelete(string errorMessage, string userInput)
        {
            if (userInput == "y")
            {
                Console.Clear();
                return true;
            }

            else if (userInput == "n")
            {
                return false;
            }

            else
            {
                return ConfirmDelete(errorMessage, newTaskData(errorMessage));
            }
        }

        private static DateTime Date_D(string date)
        {
            int count = 0;
            string userInput = "";
            DateTime newDate;
            //check if date is properly formatted (i.e. xx/xx/xxxx)
            if (!Regex.IsMatch(date, @"([0-9]){2}[/]([0-9]{1,2})([/])([0-9]){4}") && count < 3)
            {
                Console.Write("Invalid format.  Please enter in a date ex.(09/21/2022):  ");
                userInput = Console.ReadLine();
                count++;
                return Date_D(userInput);
            }

            //returns the input if correctly formatted
            else
            {
                newDate = DateTime.Parse(date);
                return newDate;
            }
        }

        private static string newTaskData(string message)
        {
            string userInput;
            Console.Write(message);
            userInput = Console.ReadLine();
            return userInput;
        }

        public static List<TaskList> MarkTaskComplete(List<TaskList> existingTasks, int itemNumber)
        {
            existingTasks[itemNumber -1].completed = true;

            Console.WriteLine("Task has been marked as completed successfully.");
            Thread.Sleep(3500);
            return existingTasks;
        }

    }
}
