using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public interface IEmployeeService
    {
        Employee Create(Employee employee , string name);       
    }
}
