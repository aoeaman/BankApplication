using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTask
{
    public class UtilityTools
    {
        public int InputIntegerOnly()
        {
            int Choice;
            try
            {
                Choice = int.Parse(Console.ReadLine());
                return Choice;
            }
            catch (Exception)
            {
                return InputIntegerOnly();
            }
        }

        public string GenerateID(string name)
        {
            return name.Substring(0, 3).ToUpper() + DateTime.UtcNow.ToString("yyyyMMdd") + DateTime.UtcNow.Millisecond;
        }

        public string ReadPassword()
        {
            var result = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        return result.ToString();
                    case ConsoleKey.Backspace:
                        if (result.Length == 0)
                        {
                            continue;
                        }
                        result.Length--;
                        Console.Write("\b \b");
                        continue;
                    default:
                        result.Append(key.KeyChar);
                        Console.Write("*");
                        continue;
                }
            }
        }
    }
}
