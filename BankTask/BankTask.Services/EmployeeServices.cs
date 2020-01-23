using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class EmployeeServices: IEmployeeService
    {
        public bool CreateEmployee(Bank selectedBank,string name,string username,string password)
        {
            if (selectedBank.Employees.Any(Element => Element.UserName==username) == false)
            {
                selectedBank.Employees.Add(new Employee
                {
                    Name = name,
                    ID = new UtilityTools().GenerateID(name),
                    Password = password,
                    UserName = username                   
                 });

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
