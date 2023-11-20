namespace PersonalBudgeterBackend
{
     class BudgeterService
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

        public void AddNewExpense(Expense Expense)
        {
            _db.Add(Expense);
            _db.SaveChanges();
        }

        public void OverrideExpense(int id, Expense ExpenseToEdit)
        {
            Expense ExpenseInDb = _db.Budgeter.FirstOrDefault(j => j.Id == id);
            if (ExpenseInDb is null)
            {
                throw new Exception("entry does not exist on database");
            }
            ;
            ExpenseInDb.Name = ExpenseToEdit.Name;
            ExpenseInDb.Amount = ExpenseToEdit.Amount;
            ExpenseInDb.Date = ExpenseToEdit.Date;
            ExpenseInDb.Category = ExpenseToEdit.Category;

            _db.SaveChanges();
        }

        public int FindExpenseId(string idParameter)
        {
            int idExpenseToProceed;

            if (!int.TryParse(idParameter, out idExpenseToProceed))
            {
                throw new Exception("An error occured. I cannot proceed with this Expense");
            }

            List<Expense> Expenses = GetExpensesList();
            for (int i = 0; i < Expenses.Count; i++)
            {
                if (idExpenseToProceed == Expenses[i].Id)
                {
                    return Expenses[i].Id;
                }
            }

            throw new Exception("No such Expense was found");
        }

        public void DeleteExpense(int ExpenseId)
        {
            Expense? JobOfferToProceed = _db.Budgeter.FirstOrDefault(
                j => j.Id == ExpenseId
            ); ;
            if (JobOfferToProceed == null)
            {
                throw new Exception("An error occured. I cannot proceed with this job offer");
            }

            //MainMenuCommands.addToFile(JobOfferToProceed);

            _db.Remove(JobOfferToProceed);
            _db.SaveChanges();
        }

        public Expense FindExpenseById(int id)
        {
            Expense Expense = _db.Budgeter.FirstOrDefault(o => o.Id == id);

            if (Expense is null)
            {
                throw new Exception(
                    "An error occured. I cannot find this job offer"
                );
            }

            return Expense;
        }
    }
}