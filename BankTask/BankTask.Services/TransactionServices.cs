using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class TransactionServices :ITransactionService
    {
        public void UpdatingTransactionHistory(string id,string senderID, Account account, decimal amount, TransactionType type, decimal charges, string reciverID)
        {
                account.Transactions.Add(new Transaction()
                {
                    Amount = amount,
                    Charges = charges,
                    ID = id.ToUpper(),
                    SenderID = senderID,
                    RecieverID = reciverID,
                    Type = type,
                    FundsAvailable = account.Funds
                });
        }

        public List<Transaction> TransactionHistory(Account account)
        {
            return account.Transactions;
        }

        public string GenerateID(string bankID,string iD)
        {
            return "TXN" + bankID + iD + DateTime.UtcNow.ToString("yyyyMMdd") + DateTime.UtcNow.Millisecond;
        }

        public string Withdraw(Account account,int amount)
        {
            if (amount > account.Funds)
            {
                return null;
            }
            else
            {
                account.Funds -= amount;
                string ID =GenerateID(account.BankID, account.ID);
                UpdatingTransactionHistory(ID,account.ID, account, amount,TransactionType.Deposit, 0, Enum.GetName(typeof(TransactionType), 0));
                return ID;
            }
        }

        public string Deposit(Account account, int amount,decimal rate)
        {
            account.Funds += amount * rate;
            string ID =GenerateID(account.BankID, account.ID);
            UpdatingTransactionHistory(ID, account.ID,account, amount, TransactionType.Deposit, 0, Enum.GetName(typeof(TransactionType), 0));
            return ID;
        }

        public void FundTransfer(Bank bank,Account sender,Account reciever,ChargeType chargeType,decimal amount)
        {
            decimal Chargerate;

            if (reciever.BankID == sender.BankID)
            {
                Chargerate = bank.ChargeForSameBank[chargeType];
            }
            else
            {
                Chargerate = bank.ChargeForOtherBank[chargeType];
            }

            decimal Charge = amount * Chargerate / 100;
            sender.Funds -= amount + Charge;
            reciever.Funds += amount;
            UpdatingTransactionHistory(GenerateID(sender.ID, sender.ID),sender.ID ,sender, amount, TransactionType.FundTransfer, Charge, reciever.ID);
            UpdatingTransactionHistory(GenerateID(reciever.ID, reciever.ID),sender.ID, reciever, amount, TransactionType.FundTransfer, 0, reciever.ID);
        }


        public bool RevertDeposit(Account user, Transaction transaction)
        {
            if (user.Funds < transaction.Amount)
            {
                return false;
            }
            else
            {
                user.Funds -= transaction.Amount;
                string ID = new TransactionServices().GenerateID(user.BankID, user.ID);
                new TransactionServices().UpdatingTransactionHistory(ID, user.ID, user, transaction.Amount, TransactionType.Revert, transaction.Charges, transaction.RecieverID);
                return true;
            }
        }

        public void RevertWithdraw(Account user, Transaction transaction)
        {
            user.Funds += transaction.Amount;
            string ID = new TransactionServices().GenerateID(user.BankID, user.ID);
            new TransactionServices().UpdatingTransactionHistory(ID, user.ID, user, transaction.Amount, TransactionType.Revert, transaction.Charges, transaction.RecieverID);

        }

        public bool RevertFundTransfer(List<Bank> banks, Transaction transaction)
        {
            Account Reciever = null;
            Account Sender = null;
            foreach (var bank in banks)
            {
                if (Reciever == null)
                {
                    Reciever = bank.Accounts.FirstOrDefault(name => name.ID == transaction.RecieverID);
                }
                if (Sender == null)
                {
                    Sender = bank.Accounts.FirstOrDefault(name => name.ID == transaction.SenderID);
                }
                if (Reciever != null && Sender != null)
                {
                    break;
                }
            }
            if (Reciever == null || Sender == null || Reciever.Funds < transaction.Amount)
            {
                return false;
            }
            else
            {
                Sender.Funds += transaction.Amount + transaction.Charges;
                Reciever.Funds -= transaction.Amount;
                new TransactionServices().UpdatingTransactionHistory(new TransactionServices().GenerateID(Sender.BankID, Sender.ID), Sender.ID, Sender, transaction.Amount, TransactionType.Revert, transaction.Charges, Reciever.ID);
                new TransactionServices().UpdatingTransactionHistory(new TransactionServices().GenerateID(Reciever.BankID, Reciever.ID), Sender.ID, Reciever, transaction.Amount, TransactionType.Revert, 0, Reciever.ID);
                return true;
            }

        }
    }
}