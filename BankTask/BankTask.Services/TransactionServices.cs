using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class TransactionServices :ITransactionService
    {
        public Transaction GetTransactionID(Transaction transaction,string bankId,string userId)
        {
            transaction.ID = GenerateID(bankId, userId);
            return transaction;
        }

        public List<Transaction> TransactionHistory(Account account)
        {
            return account.Transactions;
        }

        public string GenerateID(string bankID,string iD)
        {
            return "TXN" + bankID + iD + DateTime.UtcNow.ToString("yyyyMMdd") + DateTime.UtcNow.Millisecond;
        }

        
    }
}