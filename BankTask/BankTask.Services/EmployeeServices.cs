using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class EmployeeServices: IEmployeeService
    {
        public Employee Create(Employee employee,string name)
        {
            employee.ID = new UtilityTools().GenerateID(name);
            return employee;
        }

    }
}
