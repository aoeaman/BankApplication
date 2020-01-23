using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class Account
    {
        public string BankID { get; set; }
        public string ID { get; set; }
        public decimal Funds { get; set; }
        public string Password { get; internal set; }

        public List<Transaction> Transactions=new List<Transaction>();
    }
}
