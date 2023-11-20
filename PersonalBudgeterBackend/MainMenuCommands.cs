using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonalBudgeterBackend.Expense;

namespace PersonalBudgeterBackend
{
    internal class MainMenuCommands
    {
        public static void consoleLogic(BudgeterService BudgeterService)
        {
            bool shouldAppBeRunnign = true;
            Console.WriteLine("Hello! What Can I do for you?");
            while (shouldAppBeRunnign)
            {
                writeOptionsToConsole();

                var userInput = Console.ReadKey();
                Console.WriteLine("");
                try
                {
                    switch (userInput.KeyChar)
                    {
                        case '1':

                            addExpense(BudgeterService);
                            Console.WriteLine("Expense added");
                            break;

                        case '2':


                            editExpense(BudgeterService);
                            Console.WriteLine("Job Offer edited");
                            break;

                        case '3':

                            showAllExpenses(BudgeterService);
                            break;

                        case '4':

                            deleteExpense(BudgeterService);
                            Console.WriteLine("Expense deleted");

                            break;

                        case '9':
                            Console.WriteLine("bye");
                            shouldAppBeRunnign = false;
                            break;

                        default:
                            Console.WriteLine("I'm sorry. I can not recognize such value");
                            break;
                    }
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }
        }

        public static void writeOptionsToConsole()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Type 1 to add new expense");
            Console.WriteLine("Type 2 to edit expenses");
            Console.WriteLine("Type 3 to show all expense");
            Console.WriteLine("Type 4 to delete expense");
            Console.WriteLine("Type 9 to exit");
        }

        public static void addExpense(BudgeterService BudgeterService)
        {
            Expense expenseToAdd = GetDataForExpense(BudgeterService);
            BudgeterService.AddNewExpense(expenseToAdd);
        }

        public static string getIdFromUser()
        {
            var idJobToEdit = Console.ReadLine();

            if (String.IsNullOrEmpty(idJobToEdit))
            {
                throw new Exception(
                    "An error occured. I cannot add this job offer"
                );
            }

            return idJobToEdit;
        }

        public static void editExpense(BudgeterService BudgeterService)
        {

            Console.WriteLine("Please enter id of expense to edit:");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int idJobToEdit))
            {
                throw new Exception("An error occured. I cannot add this job offer");
            }

            var exsitingOffer = BudgeterService.FindExpenseById(idJobToEdit);

            exsitingOffer = GetDataForExpense(BudgeterService, exsitingOffer);

            BudgeterService.OverrideExpense(idJobToEdit, exsitingOffer);
        }

        public static void showAllExpenses(BudgeterService BudgeterService)
        {
            var Expenses = BudgeterService.GetExpensesList();
            if (Expenses == null)
            {
                throw new Exception("There is currently no job offers.");
            }
            foreach (var Expense in Expenses)
            {
                Console.WriteLine(Expense);
            }
        }

        public static Expense GetDataForExpense(BudgeterService BudgeterService)
        {
            Expense ExpenseToEdit = new Expense("Unknown Expense", 1, DateTime.Now, CategoryEnum.Other);

            Console.WriteLine("Please enter expense name:");
            string expenseName = Console.ReadLine();
            if (string.IsNullOrEmpty(expenseName.Trim()))
            {
                throw new Exception("Expense name cannot be empty");
            };
                ExpenseToEdit.Name = expenseName;

            Console.WriteLine("Please enter expense amount:");
            string amount = Console.ReadLine();
            bool parseAmountSucceded = float.TryParse(amount.Trim(), out float amountInt);
            if (string.IsNullOrEmpty(amount.Trim()) || !parseAmountSucceded)
            {
                throw new Exception("Expense amount cannot be empty");
            };
                ExpenseToEdit.Amount = amountInt;
            Console.WriteLine("Please enter date:");
            string date = Console.ReadLine();
            bool parseDateSucceded = DateTime.TryParse(amount.Trim(), out DateTime dateTime);
            if (string.IsNullOrEmpty(date.Trim()) || !parseDateSucceded)
            {
            };
                ExpenseToEdit.Date = dateTime;
            Console.WriteLine("Please enter Category:");
            string status = Console.ReadLine();
            status = status.Trim();
            bool parseCategorySucceded = Enum.TryParse<CategoryEnum>(status, true, out CategoryEnum category);
            if (!string.IsNullOrEmpty(status) || !parseCategorySucceded)
            {
                ExpenseToEdit.Category = category;

            };
                BudgeterService.AddNewExpense(ExpenseToEdit);
            Expense Expense = BudgeterService.FindExpense(ExpenseToEdit);
            return Expense;
        }

        public static Expense GetDataForExpense(BudgeterService BudgeterService, Expense ExpenseToEdit)
        {
            Console.WriteLine("Please enter expense name:");
            string expenseName = Console.ReadLine();
            if (!string.IsNullOrEmpty(expenseName.Trim()))
            {
                ExpenseToEdit.Name = expenseName;

            };

            Console.WriteLine("Please enter expense amount:");
            string amount = Console.ReadLine();
            if (!string.IsNullOrEmpty(amount.Trim()) && !float.TryParse(amount.Trim(), out float amountInt))
            {
                ExpenseToEdit.Amount = amountInt;
            };
            Console.WriteLine("Please enter date:");
            string date = Console.ReadLine();
            if (!string.IsNullOrEmpty(date.Trim()) && !DateTime.TryParse(amount.Trim(), out DateTime dateTime)) {
                ExpenseToEdit.Date = dateTime;
            };
            Console.WriteLine("Please enter Category:");
            string status = Console.ReadLine();
            status = status.Trim();
            if (!string.IsNullOrEmpty(status) && !Enum.TryParse<CategoryEnum>(status, true, out CategoryEnum category)) {
                ExpenseToEdit.Category = category;

            };
            return ExpenseToEdit;

        }

        public static void deleteExpense(BudgeterService BudgeterService)
        {
            Expense jobOfferToDelete = null;
            Console.WriteLine("Please enter job offer id:");
            var JobToDelete = Console.ReadLine();


            if (!int.TryParse(JobToDelete, out int jobId))
            {
                throw new Exception("Invalid id format");
            }

            //int idElementToDelete = BudgeterService.FindIdOfExpense(
            //    JobToDelete
            //);

            //List<Expense> CurrentExpenseList = BudgeterService.GetExpenseList();
            //for (int i = 0; i < CurrentExpenseList.Count; i++)
            //{
            //    if (CurrentExpenseList[i].Id == idElementToDelete)
            //    {
            //        jobOfferToDelete = CurrentExpenseList[i];
            //    }
            //}

            //if (jobOfferToDelete == null)
            //{
            //    throw new Exception(
            //        "An error occured. I cannot find this job offer"
            //    );
            //}

            BudgeterService.DeleteExpense(jobId);
        }

        static public void addToFile(object objectToArchive)
        {
            string objectSerialized = JsonConvert.SerializeObject(objectToArchive);
            File.WriteAllText("./../../../../objectSerialized.json", objectSerialized);
        }
    }
}
