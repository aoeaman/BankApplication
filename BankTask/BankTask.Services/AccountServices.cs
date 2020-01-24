using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class AccountServices :IAccountServices
    {
        public Tuple<Account,AccountHolder> Create(Account account,AccountHolder accountHolder,string name)
        {
            string ID = new UtilityTools().GenerateID(name);
            account.ID = ID.ToUpper();
            accountHolder.ID = ID.ToUpper();
            return new Tuple<Account, AccountHolder>(account, accountHolder);
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

        public AccountHolder Update(AccountHolder holder,string newUserName, string newPassword)
        {           
                holder.UserName = newUserName;
                holder.Password = newPassword;
                return holder;          
        }

        public decimal GetBalance(Account userAccount)
        {
            return userAccount.Funds;
        }
    }
}
