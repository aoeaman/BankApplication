using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankTask
{
    public interface IAccountServices
    {
        Tuple<Account, AccountHolder> Create(Account account, AccountHolder accountHolder, string name);

        bool Delete(Bank bank, string userName);

        AccountHolder Update(AccountHolder holder, string newUserName, string newPassword);

        decimal GetBalance(Account account);

    }
}
