using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface IEmployeeService
    {
        bool CreateEmployee(Bank selectedBank, string name, string username, string password);

        
    }
}
