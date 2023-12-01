using static PersonalBudgeterBackend.Category;

namespace PersonalBudgeterBackend
{
    internal class MainMenuCommands
    {
        public static void ConsoleLogic(BudgeterService budgeterService)
        {
            bool shouldAppBeRunnign = true;

            Console.WriteLine("Hello! What Can I do for you?");
            while (shouldAppBeRunnign)
            {
                WriteOptionsToConsole();

                var userInput = Console.ReadKey();
                Console.WriteLine("");
                try
                {
                    switch (userInput.KeyChar)
                    {
                        case '1':

                            AddExpense(budgeterService);
                            Console.WriteLine("Expense added");
                            break;

                        case '2':

                            Console.WriteLine("Please enter id of expense to edit:");
                            EditExpense(budgeterService);
                            Console.WriteLine("Job Offer edited");
                            break;

                        case '3':

                            ConsoleWriteAllExpenses(budgeterService);
                            break;

                        case '4':

                            DeleteExpense(budgeterService);
                            Console.WriteLine("Expense deleted");
                            break;

                        case '5':

                            ShowCategories();
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

        public static void WriteOptionsToConsole()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Type 1 to add new expense");
            Console.WriteLine("Type 2 to edit expenses");
            Console.WriteLine("Type 3 to show all expense");
            Console.WriteLine("Type 4 to delete expense");
            Console.WriteLine("Type 5 to show categories");
            Console.WriteLine("Type 9 to exit");
        }

        public static void AddExpense(BudgeterService budgeterService)
        {
            (string expenseName, float amountInt, DateTime dateTime, CategoryEnum category) NewExpenseData = GetDataForNewExpense();
            Expense expenseToAdd = new Expense(NewExpenseData.expenseName, NewExpenseData.amountInt, NewExpenseData.dateTime, NewExpenseData.category);
            budgeterService.AddNewExpense(expenseToAdd);
            //budgeterService.AddNewExpense(expenseToAdd);
        }

        //public static string GetIdFromUser()
        //{
        //    var idJobToEdit = Console.ReadLine();

        //    if (String.IsNullOrEmpty(idJobToEdit))
        //    {
        //        throw new Exception(
        //            "An error occured. I cannot add this job offer"
        //        );
        //    }

        //    return idJobToEdit;
        //}

        public static void EditExpense(BudgeterService budgeterService)
        {
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int idJobToEdit))
            {
                throw new Exception("An error occured. I cannot add this job offer");
            }

            var exsitingOffer = budgeterService.FindExpenseById(idJobToEdit);

            exsitingOffer = GetDataEditExpense(exsitingOffer);

            budgeterService.OverrideExpense(exsitingOffer);
        }

        public static void ConsoleWriteAllExpenses(BudgeterService budgeterService)
        {
            List<Expense> expenses = budgeterService.GetExpensesList();
            if (expenses is null)
            {
                throw new Exception("There is currently no job offers.");
            }
            foreach (Expense Expense in expenses)
            {
                Console.WriteLine(Expense);
                Console.WriteLine("");
            }
        }

        public static (string, float, DateTime, CategoryEnum) GetDataForNewExpense()
        {
            //Expense ExpenseToEdit = new Expense("Unknown Expense", 1, DateTime.Now, CategoryEnum.Other);

            Console.WriteLine("Please enter expense name:");
            string expenseName = Console.ReadLine();
            if (string.IsNullOrEmpty(expenseName.Trim()))
            {
                throw new Exception("Expense name cannot be empty");
            };
            //ExpenseToEdit.Name = expenseName;

            Console.WriteLine("Please enter expense amount:");
            string? amount = Console.ReadLine();
            bool parseAmountSucceded = float.TryParse(amount.Trim(), out float amountInt);
            if (string.IsNullOrEmpty(amount.Trim()) || !parseAmountSucceded)
            {
                throw new Exception("Expense amount cannot be empty");
            };
            //ExpenseToEdit.Amount = amountInt;

            Console.WriteLine("Please enter date:");
            string? date = Console.ReadLine();
            bool parseDateSucceded = DateTime.TryParse(date.Trim(), out DateTime dateTime);
            if (string.IsNullOrEmpty(date.Trim()) || !parseDateSucceded)
            {
                //ExpenseToEdit.Date = dateTime;
                throw new Exception("Date is invalid");
            };

            Console.WriteLine("Please enter Category:");
            string? status = Console.ReadLine();
            status = status.Trim();
            bool parseCategorySucceded = Enum.TryParse<CategoryEnum>(status, true, out CategoryEnum category);
            if (string.IsNullOrEmpty(status) || !parseCategorySucceded)
            {
                //ExpenseToEdit.Category = category;
                throw new Exception("Status is invalid");
            };

            return (expenseName, amountInt, dateTime, category);
            //budgeterService.AddNewExpense(ExpenseToEdit);
            //Expense Expense = budgeterService.FindExpense(ExpenseToEdit);
            //return Expense;
        }

        public static Expense GetDataEditExpense(Expense expenseToEdit)
        {
            Console.WriteLine("Please enter expense name:");
            string? expenseName = Console.ReadLine();
            expenseName = expenseName.Trim();
            if (!string.IsNullOrEmpty(expenseName))
            {
                expenseToEdit.Name = expenseName;
            };

            Console.WriteLine("Please enter expense amount:");
            string? amount = Console.ReadLine();
            amount = amount.Trim();
            if (!string.IsNullOrEmpty(amount) && float.TryParse(amount, out float amountInt))
            {
                expenseToEdit.Amount = amountInt;
            };

            Console.WriteLine("Please enter date:");
            string? date = Console.ReadLine();
            date = date.Trim();
            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime dateTime))
            {
                expenseToEdit.Date = dateTime;
            };

            Console.WriteLine("Please enter Category:");
            string? status = Console.ReadLine();
            status = status.Trim();
            if (!string.IsNullOrEmpty(status) && Enum.TryParse<CategoryEnum>(status, true, out CategoryEnum category))
            {
                expenseToEdit.Category = category;
            };
            return expenseToEdit;
        }

        public static void DeleteExpense(BudgeterService budgeterService)
        {
            //Expense jobOfferToDelete = null;
            Console.WriteLine("Please enter job offer id:");
            string? jobIdToDelete = Console.ReadLine();

            if (!int.TryParse(jobIdToDelete, out int jobId))
            {
                throw new Exception("Invalid id format");
            }

            //int idElementToDelete = budgeterService.FindIdOfExpense(
            //    JobToDelete
            //);

            //List<Expense> CurrentExpenseList = budgeterService.GetExpenseList();
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

            budgeterService.DeleteExpense(jobId);
        }

        //static public void AddToFile(object objectToArchive)
        //{
        //    string objectSerialized = JsonConvert.SerializeObject(objectToArchive);
        //    File.WriteAllText("./../../../../objectSerialized.json", objectSerialized);
        //}

        public static void ShowCategories()
        {
            CategoryEnum[] values = (CategoryEnum[])Enum.GetValues(typeof(CategoryEnum));
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i} : {values[i]}");
            }
            //foreach (var category in Enum.GetValues(typeof(CategoryEnum)))
            //{
            //}
        }
    }
}