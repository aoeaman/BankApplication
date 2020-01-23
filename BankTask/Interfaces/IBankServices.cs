using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface IBankServices
    {

        bool AddCurrency(Bank bank, string name, decimal exchangeValue);

        void ChangeChargeSameBank(Bank bank, decimal RTGS, decimal IMPS);

        void ChangeChargeOtherBank(Bank bank, decimal RTGS, decimal IMPS);

        bool Create(List<Bank> banks, string bankName);
    }
}
