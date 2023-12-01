namespace PersonalBudgeterBackend
{
    internal class BudgeterService
    {
        private readonly BudgeterContext _db;

        public BudgeterService(BudgeterContext db)
        {
            _db = db;
        }

        public List<Expense> GetExpensesList()
        {
            return _db.Budgeter.ToList();
        }

        public void AddNewExpense(Expense expense)
        {
            _db.Add(expense);
            _db.SaveChanges();
        }

        public void OverrideExpense(Expense expenseToEdit)
        {
            Expense? expenseInDb = _db.Budgeter.FirstOrDefault(j => j.Id == expenseToEdit.Id);
            if (expenseInDb is null)
            {
                throw new Exception("entry does not exist on database");
            }
            ;
            expenseInDb.Name = expenseToEdit.Name;
            expenseInDb.Amount = expenseToEdit.Amount;
            expenseInDb.Date = expenseToEdit.Date;
            expenseInDb.Category = expenseToEdit.Category;

            _db.SaveChanges();
        }

        //public int FindExpenseId(string idParameter)
        //{
        //    int idExpenseToProceed;

        //    if (!int.TryParse(idParameter, out idExpenseToProceed))
        //    {
        //        throw new Exception("An error occured. I cannot proceed with this Expense");
        //    }

        //    List<Expense> Expenses = GetExpensesList();
        //    for (int i = 0; i < Expenses.Count; i++)
        //    {
        //        if (idExpenseToProceed == Expenses[i].Id)
        //        {
        //            return Expenses[i].Id;
        //        }
        //    }

        //    throw new Exception("No such Expense was found");
        //}

        public void DeleteExpense(int expenseId)
        {
            Expense? jobOfferToProceed = _db.Budgeter.FirstOrDefault(
                j => j.Id == expenseId
            );
            if (jobOfferToProceed is null)
            {
                throw new Exception("An error occured. I cannot proceed with this job offer");
            }

            //MainMenuCommands.addToFile(jobOfferToProceed);

            _db.Remove(jobOfferToProceed);
            _db.SaveChanges();
        }

        public Expense FindExpenseById(int id)
        {
            Expense? expense = _db.Budgeter.FirstOrDefault(o => o.Id == id);

            if (expense is null)
            {
                throw new Exception(
                    "An error occured. I cannot find this job offer"
                );
            }

            return expense;
        }

        public Expense FindExpense(Expense expenseParameter)
        {
            Expense? expense = _db.Budgeter.FirstOrDefault(o => (o.Name == expenseParameter.Name && o.Amount == expenseParameter.Amount));

            if (expense is null)
            {
                throw new Exception(
                    "An error occured. I cannot find this job offer"
                );
            }

            return expense;
        }
    }
}