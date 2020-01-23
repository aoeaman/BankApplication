using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class BankServices :IBankServices
    {
        public bool Create(List<Bank> banks,string bankName)
        {
            try
            {
                
                if (banks.Any(name => string.Equals(name.Name, bankName)) == false)
                {
                    Bank bank = new Bank
                    {
                        ID = bankName.Substring(0, 3) + DateTime.UtcNow.ToString("yyyyMMdd"),
                        Name = bankName,
                        ChargeForSameBank = new Dictionary<ChargeType, decimal>
                        {
                            {ChargeType.RTGS,0 },{ChargeType.IMPS,5}
                        },
                        ChargeForOtherBank = new Dictionary<ChargeType, decimal>
                        {
                            {ChargeType.RTGS,2 },{ChargeType.IMPS,6}
                        },
                        Currency = new Dictionary<string, decimal> { { "INR", 1 } },
                        Accounts = new List<Account>(),
                        AccountHolders=new List<AccountHolder>()
                    };
                    banks.Add(bank);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return Create(banks,bankName);
            }
        }

        public bool AddCurrency(Bank bank, string name, decimal exchangeValue)
        {
            if (!bank.Currency.ContainsKey(name))
            {
                bank.Currency.Add(name, exchangeValue);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeChargeSameBank(Bank bank, decimal RTGS, decimal IMPS)
        {
            bank.ChargeForSameBank[ChargeType.RTGS] = RTGS;
            bank.ChargeForSameBank[ChargeType.IMPS] = IMPS;
        }

        public void ChangeChargeOtherBank(Bank bank, decimal RTGS, decimal IMPS)
        {
            bank.ChargeForOtherBank[ChargeType.RTGS] = RTGS;
            bank.ChargeForOtherBank[ChargeType.IMPS] = IMPS;
      }

    }
}
