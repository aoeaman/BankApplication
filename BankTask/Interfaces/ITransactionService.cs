using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface ITransactionService
    {
        Transaction GetTransactionID(Transaction transaction, string bankId, string userId);
       
        List<Transaction> TransactionHistory(Account account);
    } 
}
