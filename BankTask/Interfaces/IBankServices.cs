using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface IBankServices
    {

        void AddCurrency(Bank bank, Currency newCurrency);

        void ChangeChargeSameBank(Bank bank, decimal RTGS, decimal IMPS);

        void ChangeChargeOtherBank(Bank bank, decimal RTGS, decimal IMPS);

        Bank Create(Bank bank,string bankName);
    }
}
