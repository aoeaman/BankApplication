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

        public List<Employee> Employees { get; set; }
        public List<AccountHolder> AccountHolders { get; set; }
        public List<Account> Accounts { get; set; }

        public Dictionary<ChargeType, decimal> ChargeForSameBank { get; set; }
        public Dictionary<ChargeType, decimal> ChargeForOtherBank { get; set; }

        public Dictionary<string, decimal> Currency { get; set; }
    }
}
