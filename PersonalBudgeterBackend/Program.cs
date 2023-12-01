using PersonalBudgeterBackend;

namespace RecruBuddy
{
    public class PersonalBudgeterBackend
    {
        private static void Main()
        {
            using BudgeterContext db = new BudgeterContext();
            var BudgeterService = new BudgeterService(db);
            MainMenuCommands.ConsoleLogic(BudgeterService);
        }
    }
}