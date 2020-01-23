using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public enum TransactionType
     {
          self,
          Deposit,
          Withdraw,
          FundTransfer,
          Revert
     }

    public enum ChargeType
    {
         RTGS,
         IMPS
    }
}
