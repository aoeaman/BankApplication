using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankTask
{
    public interface IAccountServices
    {
        string Create(Bank bank, string name, string userName, string password);

        bool Delete(Bank bank, string userName);

        bool Update(Bank bank, string userName, string newUserName, string newPassword);

        decimal GetBalance(Account account);

    }
}
