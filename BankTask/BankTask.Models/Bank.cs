using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class Bank
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees=new List<Employee>();
        public List<AccountHolder> AccountHolders=new List<AccountHolder>();
        public List<Account> Accounts=new List<Account>();

        public Dictionary<ChargeType, decimal> ChargeForSameBank { get; set; }
        public Dictionary<ChargeType, decimal> ChargeForOtherBank { get; set; }

        public Dictionary<string, decimal> Currency { get; set; }
    }
}
