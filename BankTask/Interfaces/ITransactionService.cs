using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface ITransactionService
    {
        void UpdatingTransactionHistory(string id, string senderID, Account account, decimal amount, TransactionType type, decimal charges, string reciverID);

        bool RevertFundTransfer(List<Bank> banks, Transaction transaction);

        void RevertWithdraw(Account user, Transaction transaction);

        bool RevertDeposit(Account user, Transaction transaction);

        string Withdraw(Account account, int amount);

        string Deposit(Account account, int amount, decimal rate);

        void FundTransfer(Bank bank, Account sender, Account reciever, ChargeType chargeType, decimal amount);

        List<Transaction> TransactionHistory(Account account);
    } 
}
