using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PersonalBudgeterBackend.Category;

namespace PersonalBudgeterBackend
{
    internal class Expense
    {

        public Expense(string name, float amount, DateTime date, CategoryEnum category)
        {

            Name = name;
            Amount = amount;
            ExternalId = new Random().Next().ToString();
            Date = date;
            Category = category;

            //DateTime DateTemporary;
            //CategoryEnum CategoryTemporary;
            //if (!DateTime.TryParse(date, out  DateTemporary))
            //{
            //    throw new Exception("An error occured. I cannot proceed with this job offer");
            //}

            //if (!Enum.TryParse<CategoryEnum>(category, out  CategoryTemporary))
            //{
            //    throw new Exception("An error occured. I cannot proceed with this job offer");
            //}

            //Date = DateTemporary;
            //Category = CategoryTemporary;
        }

        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public CategoryEnum Category { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}, \nAmount: {Amount}, Date: {Date}, \nCategory: {Category}";
        }
    }
}
