using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class BankServices :IBankServices
    {
        public Bank Create(Bank bank,string bankName)
        {
            bank.ID = bankName.Substring(0, 3) + DateTime.UtcNow.ToString("yyyyMMdd");
            return bank;
        }

        public void AddCurrency(Bank bank,Currency newCurrency)
        {
            bank.Currencies.Add(newCurrency);
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
