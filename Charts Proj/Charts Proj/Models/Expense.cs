using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Charts_Proj.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public string Payee { get; set; }
        public double Total { get; set; }
        public DateTime Date { get; set; }
        public string RegisteredUserID { get; set; }
        public virtual RegisteredUser RegisteredUser { get; set; }
        public int ExpenseCategoryID { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }

    }
}