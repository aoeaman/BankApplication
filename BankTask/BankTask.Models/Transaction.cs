using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class Transaction
    {

        public decimal Amount { get; set; }
        public decimal Charges { get; set; }
        public decimal FundsAvailable { get; set; }

        public string ID { get; set; }
        public string SenderID { get; set; }
        public string RecieverID { get; set; }
        public TransactionType Type { get; set; }

        public override string ToString()
        {
            return ID + "\t" + Amount + "\t" + Charges + "\t" + Type + "\t" + SenderID + "\t" + RecieverID + "\t" + FundsAvailable;
        }
    }
}
