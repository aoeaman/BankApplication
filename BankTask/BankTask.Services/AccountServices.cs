using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class AccountServices :IAccountServices
    {
        public string Create(Bank bank, string name, string userName, string password)
        {
            if (bank.AccountHolders.Exists(Element => string.Equals(Element.UserName, userName)) == false)
            {
                string ID = new UtilityTools().GenerateID(name);

                bank.Accounts.Add(new Account()
                {
                    BankID = bank.ID.ToUpper(),
                    ID = ID.ToUpper(),
                    Funds = 0,
                    Transactions = new List<Transaction>()
                });

                bank.AccountHolders.Add(new AccountHolder()
                {
                    ID = ID,
                    Password = password,
                    UserName = userName,
                    Name = name,
                    BankID = bank.ID
                });

                return ID;
            }
            else
            {
                return null;

            }
        }

        public bool Delete(Bank bank,string userName)
        {
            AccountHolder holder = bank.AccountHolders.Find(Element => Element.UserName == userName);
            if (holder != null)
            {
                bank.AccountHolders.Remove(holder);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Update(Bank bank, string userName,string newUserName, string newPassword)
        {
            AccountHolder Holder = bank.AccountHolders.Find(Element => Element.UserName == userName);
            if (Holder != null)
            {
                Holder.UserName = newUserName;
                Holder.Password = newPassword;
                return true;
            }
            else
            {
                return false;
            }
        }

        public decimal GetBalance(Account userAccount)
        {
            return userAccount.Funds;
        }
    }
}
