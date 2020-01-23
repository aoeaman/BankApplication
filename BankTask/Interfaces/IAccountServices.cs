using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankTask
{
    public interface IAccountServices
    {
        string CreateAccount(Bank bank, string name, string userName, string password);

        bool DeleteAccount(Bank bank, string userName);

        bool UpdateAccount(Bank bank, string userName, string newUserName, string newPassword);

        decimal CheckFund(Account account);

    }
}
