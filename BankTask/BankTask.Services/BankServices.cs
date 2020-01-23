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
                        Currencies=new List<Models.Currency>() ,
                        Accounts = new List<Account>(),
                        AccountHolders=new List<AccountHolder>(),
                        Employees=new List<Employee>()
                    };
                    bank.Currencies.Add(new Models.Currency {Name="INR",Exchangerate=1 });
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
                return false;
            }
        }

        public bool AddCurrency(Bank bank, string name, decimal exchangeValue)
        {
            if (!bank.Currencies.Any(Element=>Element.Name==name))
            {
                bank.Currencies.Add(new Models.Currency {Name=name,Exchangerate=exchangeValue });
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
