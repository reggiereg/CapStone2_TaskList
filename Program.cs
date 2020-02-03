using System;
using System.Collections.Generic;
using System.Threading;

namespace Capstone_TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Continue = true;
            int switchCase;
                
            List<string> errorMessages = new List<string>
            {
                new string("Value is not a number or at least I can't convert it!! Returning to main menu"),
                new string("Invalid input of "),
                new string("Invalid input. Please enter y to continue or n to exit: ")
            };
            string userOptionToTest = " ";
            int userOption;
            List<TaskList> tasks = new List<TaskList>
            {
                new TaskList("Reginald Richardson", "Create the Print Menu for Tasks Capstone", DateTime.Parse("01/31/2020"), true, 1),
                new TaskList("Reginald Richardson", "Create the Task class", DateTime.Parse("01/31/2020"), true, 2),
                new TaskList("Reginald Richardson", "Create the default Task list in my program", DateTime.Parse("01/31/2020"), true, 3),
                new TaskList("Reginald Richardson", "Create the validation methods for user input", DateTime.Parse("01/31/2020"), true, 4),
                new TaskList("Reginald Richardson", "Create the add task method", DateTime.Parse("01/31/2020"), true, 5)
            };

            while (Continue)
            {
                PrintMenu();
                userOptionToTest = SelectTaskOption("ENTER YOUR OPTION: ");
                userOption = TaskOptSelectValidation(errorMessages, userOptionToTest, 1, 5);
                
                switch(userOption)
                {
                    case 1:
                        Console.Clear();
                        TaskList.ListTasks(tasks);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\n");
                        Continue = ContinueProgram(errorMessages[2], SelectTaskOption("Would you like to continue the program?: "));
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 2:
                        Console.Clear();
                        TaskList.AddTask(tasks);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"New task added successfully:\n{tasks[tasks.Count - 1].Name}\n" +
                            $"{tasks[tasks.Count - 1].Description}\n{tasks[tasks.Count - 1].DueDate}\n{tasks[tasks.Count - 1].Completed}\n");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\n");
                        Continue = ContinueProgram(errorMessages[2], SelectTaskOption("Would you like to continue the program?: "));
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case 3:
                        int i = 1;
                        int originalCount = tasks.Count;
                        int taskToDelete;
                        foreach(TaskList task in tasks)
                        {
                            Console.WriteLine($"{i}. {task.Description}");
                            i++;
                        }
                        taskToDelete = TaskOptSelectValidation(errorMessages, SelectTaskOption("Enter in the corresponding number for the task you would like to delete: "), 1, tasks.Count);
                        TaskList.DeleteTask(errorMessages[2],taskToDelete -1, tasks);
                        if (originalCount > tasks.Count)
                        {
                            Console.WriteLine($"Task {taskToDelete} deleted successfully");
                        }
                        break;
                    case 5:
                        Continue = false;
                        break;
                }
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine("TASK MANAGER MENU\n===========================\n1. List Tasks\n2. Add Task\n3. Delete Task\n4. Mark Task Complete\n5. Quit\n");
        }

        public static string SelectTaskOption(string message)
        {
            string userInput;
            Console.Write(message);
            userInput = Console.ReadLine();
            return userInput;
        }

        public static int TaskOptSelectValidation(List<string> errorMessages, string userInput, int option1, int optionMax)
        {
            int userOption;
            try
            {
                userOption = int.Parse(userInput);
            }

            catch(FormatException)
            {
                string userOptionError;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine(errorMessages[0]);
                Thread.Sleep(3500);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                PrintMenu();
                userOptionError = SelectTaskOption("ENTER YOUR OPTION: ");
                return TaskOptSelectValidation(errorMessages, userOptionError, 1, 5);
            }

            if(userOption >= option1 && userOption <= optionMax)
            {
                return userOption;
            }
            else
            {
                string userOptionError;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine($"{errorMessages[1]}{userOption}.");
                Thread.Sleep(3500);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                PrintMenu();
                userOptionError = SelectTaskOption("ENTER YOUR OPTION: ");
                return TaskOptSelectValidation(errorMessages, userOptionError, 1, 5);
            }
            return userOption;
        }

        public static bool ContinueProgram(string errorMessage, string userInput)
        {
            if(userInput == "y")
            {
                Console.Clear();
                return true;
            }

            else if(userInput == "n")
            {
                return false;
            }

            else
            {
                return ContinueProgram(errorMessage, SelectTaskOption(errorMessage));
            }
        }
    }
    
}
