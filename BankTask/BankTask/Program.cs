using System;
using System.Collections.Generic;
using System.Linq;


namespace BankTask
{
    class Program
    {


        static void Main(string[] args)
        {

            List<Bank> Banks = new List<Bank>();
            BankServices bankService = new BankServices();
            TransactionServices transactionServices = new TransactionServices();
            EmployeeServices employeeServices = new EmployeeServices();
            AccountServices accountService = new AccountServices();
            UtilityTools Tools = new UtilityTools();

            while (true)
            {
                Console.WriteLine("-----Welcome to Bank Management System-----");
                Console.WriteLine("Enter Your Choice");
                Console.WriteLine("1.Create Bank");
                Console.WriteLine("2.Create Employee Account");
                Console.WriteLine("3.Login");
                Console.WriteLine("4.Exit");
                int SelectedChoice = Tools.InputIntegerOnly();
                switch (SelectedChoice)
                {
                    case 1:
                    {
                        Console.Clear();
                        Console.WriteLine("Enter Bank Name:");
                        string BankName = Console.ReadLine().ToUpper();
                        bool status=bankService.Create(Banks,BankName);
                        if (status)
                        {
                            Console.WriteLine("Bank Created");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("No Bank Exists \nPlease Proceed to Setup New Bank");
                            Console.ReadKey();                               
                        }
                        break;
                    }                       
                    case 2:
                    {
                        Console.Clear();
                        if (Banks.Count == 0)
                        {
                            Console.WriteLine("No Bank Exists \nPlease Proceed to Setup New Bank");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Select Bank:");
                        Banks.ForEach(name => Console.WriteLine(Banks.IndexOf(name) + 1 + ". " + name.Name + " ID: "+ name.ID) );
                        int SelectedBank = Tools.InputIntegerOnly();
                        Console.WriteLine("Enter Name");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Create Username");
                        string Username = Console.ReadLine();
                        Console.WriteLine("Create Passowrd");
                        string Password = Tools.ReadPassword();
                        bool Status=employeeServices.CreateEmployee(Banks[SelectedBank-1],Name,Username,Password);
                        if (!Status)
                        {
                            Console.WriteLine("Username Already Exists:");
                            Console.ReadKey();
                        }
                        break;
                    }                       
                    case 3:
                    {
                        Console.Clear();
                        if (Banks.Count == 0)
                        {
                            Console.WriteLine("No Bank Exists \nPlease Proceed to Setup New Bank");
                            Console.ReadKey();
                            break;
                        }
                        Console.WriteLine("Select Bank:");
                        Banks.ForEach(name => Console.WriteLine(Banks.IndexOf(name) + 1 + ". " + name.Name));
                        int Choice = Tools.InputIntegerOnly();
                        Bank CurrentBank = Banks[Choice - 1];
                        Console.WriteLine("Enter Your Choice");
                        Console.WriteLine("1.Employee Login");
                        Console.WriteLine("2.Account Holder Login");

                        int choice = Tools.InputIntegerOnly();
                        Console.WriteLine();

                        Console.WriteLine("Username");
                        string UserName = Console.ReadLine();
                        Console.WriteLine("Passowrd");
                        string Password = Tools.ReadPassword();

                        switch (choice)
                        {
                            case 1:
                            {
                                Employee employee = Banks[Choice - 1].Employees.Find(name => string.Equals(name.UserName, UserName) && string.Equals(name.Password, Password));
                                if (employee!=null)
                                {
                                    {
                                        int Option = 0;
                                        while (Option != 8)
                                        {
                                        Console.Clear();
                                        Console.WriteLine("\n1. Create new account");
                                        Console.WriteLine("2. Update / Delete account");
                                        Console.WriteLine("3. Add new Accepted currency with exchange rate");
                                        Console.WriteLine("4. Change service charge for same bank account");
                                        Console.WriteLine("5. Change service charge for other bank account");
                                        Console.WriteLine("6. View account transaction history");
                                        Console.WriteLine("7. Revert transaction");
                                        Console.WriteLine("8. Logout");
                                        

                                            Option = Tools.InputIntegerOnly();
                                            switch (Option)
                                            {
                                                    
                                                case 1:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Name");
                                                    string Name = Console.ReadLine();
                                                    Console.WriteLine("Create Username");
                                                    string Username = Console.ReadLine();
                                                    Console.WriteLine("Create Passowrd");
                                                    string passWord = Tools.ReadPassword();
                                                    Console.WriteLine();
                                                    string GetUserID = accountService.CreateAccount(CurrentBank, Name, Username, passWord);
                                                    if (GetUserID == null)
                                                    {
                                                        Console.WriteLine("Username Already Exists:");
                                                        Console.ReadKey();
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Account Created Successfully \nAccount ID :" + GetUserID + "\nPress any key to continue...");
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                }
                                                case 2:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Username");
                                                    string Username = Console.ReadLine();

                                                    Console.WriteLine("Select Operation");
                                                    Console.WriteLine("1.Update account");
                                                    Console.WriteLine("2.Delete account");
                                                    int SelectedOperation = Tools.InputIntegerOnly();
                                                    switch (SelectedOperation)
                                                    {
                                                        case 1:
                                                        {
                                                            Console.WriteLine("Username");
                                                            string NewUserName = Console.ReadLine();
                                                            Console.WriteLine("Passowrd");
                                                            string NewPassword = Tools.ReadPassword();
                                                            bool Status = accountService.UpdateAccount(CurrentBank, Username, NewUserName, NewPassword);
                                                            if (Status)
                                                            {
                                                                Console.WriteLine("Successfully Updated\nPress any key to continue...");
                                                                Console.ReadLine();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("No User Found\nPress any key to continue...");
                                                                Console.ReadLine();
                                                            }
                                                            break;
                                                        }
                                                        case 2:
                                                        {
                                                            bool Status=accountService.DeleteAccount(CurrentBank, Username);
                                                            if (Status)
                                                            {
                                                                Console.WriteLine("Successfully Deleted\nPress any key to continue...");
                                                                Console.ReadLine();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("No User Found\nPress any key to continue...");
                                                                Console.ReadLine();
                                                            }
                                                                    break;
                                                        }
                                                    }
                                                    break;
                                                }
                                                case 3:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Currency Name");
                                                    string Currency = Console.ReadLine().ToUpper();
                                                    Console.WriteLine("Add new Accepted currency exchange rate in INR");
                                                    decimal ExchValue = decimal.Parse(Console.ReadLine());
                                                    bool status = bankService.AddCurrency(CurrentBank, Currency, ExchValue);
                                                    if (!status)
                                                    {
                                                        Console.WriteLine("Currency Already Added:\nPress any key to continue...");
                                                        Console.ReadKey();
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Currency Added:\nPress any key to continue...");
                                                        Console.ReadKey();
                                                    }
                                                    break;
                                                }
                                                case 4:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter RTGS");
                                                    decimal RTGS = decimal.Parse(Console.ReadLine());
                                                    Console.WriteLine("Enter IMPS");
                                                    decimal IMPS = decimal.Parse(Console.ReadLine());
                                                    bankService.ChangeChargeSameBank(CurrentBank, RTGS, IMPS);
                                                    Console.WriteLine("\nService Charge Updated\nPress any key to continue...");
                                                    Console.ReadKey();
                                                    break;
                                                }
                                                case 5:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter RTGS");
                                                    decimal RTGS = decimal.Parse(Console.ReadLine());
                                                    Console.WriteLine("Enter IMPS");
                                                    decimal IMPS = decimal.Parse(Console.ReadLine());
                                                    bankService.ChangeChargeOtherBank(CurrentBank, RTGS, IMPS);
                                                    Console.WriteLine("\nService Charge Updated\nPress any key to continue");
                                                    Console.ReadKey();
                                                    break;
                                                }
                                                case 6:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Account ID");
                                                    string AccountID = Console.ReadLine();
                                                    Account Account = CurrentBank.Accounts.FirstOrDefault(name => name.ID.Equals(AccountID));
                                                    if (Account == null)
                                                    {
                                                        Console.WriteLine("No User found:\nPress any key to Continue...");
                                                        Console.ReadKey();
                                                        break;
                                                    }
                                                    List<Transaction> transactions = transactionServices.TransactionHistory(Account);
                                                    if (transactions.Count == 0)
                                                    {
                                                        Console.WriteLine("No Transactions found:\nPress any key to Continue...");
                                                        Console.ReadKey();
                                                    }
                                                    else
                                                    {
                                                        transactions.ForEach(name => Console.WriteLine(name));
                                                    }
                                                    Console.ReadKey();
                                                    break;
                                                }
                                                case 7:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Transaction ID");
                                                    string TransactionID = Console.ReadLine();
                                                    Account user = null;
                                                    Transaction transaction = null;
                                                    bool status = false;
                                                    foreach (var bank in Banks)
                                                    {
                                                        foreach (var account in bank.Accounts)
                                                        {
                                                            if (account.Transactions.Any(name => name.ID.Equals(TransactionID)))
                                                            {
                                                                user = account;
                                                                transaction = account.Transactions.Find(name => name.ID.Equals(TransactionID));
                                                                status = true;
                                                                break;
                                                            }
                                                        }
                                                        if (status)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                    if (user == null)
                                                    {
                                                        Console.WriteLine("No such Transaction found\nPress any key to continue");
                                                        Console.ReadKey();
                                                        break;
                                                    }
                                                    TransactionType type = transaction.Type;
                                                    switch (type)
                                                    {
                                                        case TransactionType.Deposit:
                                                        {
                                                            if (!transactionServices.RevertDeposit(user, transaction))
                                                            {
                                                                Console.WriteLine("Low Balance\nCannot Revert\npress any key to continue");
                                                                Console.ReadKey();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Successfully Reverted");
                                                                Console.ReadKey();
                                                            }
                                                            break;
                                                        }
                                                        case TransactionType.Withdraw:
                                                        {
                                                            transactionServices.RevertWithdraw(user,transaction);
                                                            Console.WriteLine("Successfully Reverted \nPress any key to Continue");
                                                            Console.ReadKey();
                                                            break;
                                                        }
                                                        case TransactionType.FundTransfer:
                                                        {
                                                            if(transactionServices.RevertFundTransfer(Banks, transaction))
                                                            {
                                                                Console.WriteLine("Successfully Reverted \nPress any key to Continue");
                                                                Console.ReadKey();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Error Occoured\nCannot Revert\npress any key to continue");
                                                                Console.ReadKey();
                                                            }                                                                                                                     
                                                        }
                                                        break;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("!!!!!No Such Account Exists!!!!!");
                                    Console.WriteLine("Press Any Key to Continue...");
                                    Console.ReadKey();
                                    break;
                                }
                                break;
                            }
                            case 2:
                            {
                                AccountHolder Holder = CurrentBank.AccountHolders.Find(name => string.Equals(name.UserName, UserName) && string.Equals(name.Password, Password));
                                if (Holder!=null)
                                {
                                    int Option = 0;
                                    while (Option != 6)
                                    {
                                        Console.Clear();
                                    Account UserAccount = CurrentBank.Accounts.Find(name => name.ID.Equals(Holder.ID));
                                    Console.WriteLine("\n1. Deposit Amount\n" +
                                    "2. Withdraw Amount(INR only)\n" +
                                    "3. Transfer Funds(INR only)\n" +
                                    "4. Account Balance\n" +
                                    "5. Transaction history\n" +
                                    "6. Logout");
                                    
                                        Option = Tools.InputIntegerOnly();
                                        switch (Option)
                                        {
                                            case 1:
                                            {
                                                bool status = true;
                                                while (status)
                                                {
                                                    
                                                    Console.Clear();
                                                    Console.WriteLine("Enter Amount:");
                                                    int Amount = Tools.InputIntegerOnly();
                                                    Console.WriteLine("Enter Currency:");
                                                    string currency = Console.ReadLine().ToUpper();
                                                    try
                                                    {
                                                        decimal CurrencyRate = CurrentBank.Currency[currency];
                                                        string ID = transactionServices.Deposit(UserAccount, Amount, CurrencyRate);
                                                        Console.WriteLine("Transaction ID :   " + ID + "\nPress any key to Continue...");
                                                        status = false;
                                                        Console.ReadKey();
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Entered Currency Not Accepted\npress any key to Re-enter Values...");
                                                        Console.ReadKey();
                                                    }
                                                    
                                                }
                                                break;
                                            }
                                            case 2:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Enter Amount:");
                                                int Amount = Tools.InputIntegerOnly();
                                                string ID = transactionServices.Withdraw(UserAccount, Amount);
                                                if (ID == null)
                                                {
                                                    Console.WriteLine("Low Balance\nPress any key to Continue");
                                                    Console.ReadKey();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Transaction ID :  " + ID + "\nPress any key to Continue");
                                                    Console.ReadKey();
                                                }
                                                break;
                                            }
                                            case 3:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Enter Account ID of the reciepent");
                                                string recieverID = Console.ReadLine();
                                                Console.WriteLine("Enter Amount to Transfer");
                                                int Amount = Tools.InputIntegerOnly(); ;
                                                Account RecieverAccount = null;
                                                ChargeType TypeOfCharge =ChargeType.RTGS;
                                                if (Amount > 100000)
                                                {
                                                    TypeOfCharge =ChargeType.IMPS;
                                                }
                                                if (UserAccount.Funds < Amount)
                                                {
                                                    Console.WriteLine("Funds Not Aailable\nPress any key to continue...");
                                                    Console.ReadLine();
                                                    break;
                                                }
                                                foreach (var bank in Banks)
                                                {
                                                    RecieverAccount = bank.Accounts.Find(name => name.ID == recieverID);
                                                    if (RecieverAccount != null)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (RecieverAccount == null)
                                                {
                                                    Console.WriteLine("No user found\npress any key to continue:");
                                                    Console.ReadKey();
                                                    break;
                                                }
                                                transactionServices.FundTransfer(CurrentBank, UserAccount, RecieverAccount, TypeOfCharge, Amount);
                                                Console.WriteLine("Transaction Successful :  \nPress any key to Continue");
                                                Console.ReadKey();
                                                        
                                            break;
                                            }
                                            case 4:
                                            {
                                                Console.Clear();
                                                Console.WriteLine(accountService.CheckFund(UserAccount));
                                                Console.ReadKey();
                                                break;
                                            }
                                            case 5:
                                            {
                                                Console.Clear();
                                                List<Transaction> transactions = transactionServices.TransactionHistory(UserAccount);
                                                if (transactions.Count == 0)
                                                {
                                                    Console.WriteLine("No Transactions found:\nPress any key to Continue...");
                                                    Console.ReadKey();
                                                }
                                                else
                                                {
                                                    transactions.ForEach(name => Console.WriteLine(name));
                                                    Console.ReadKey();
                                                }
                                                
                                                break;
                                            }
                                        }
                                    }
                                                

                                }
                                else
                                {
                                    Console.WriteLine("!!!!!No Such Account Exists!!!!!");
                                    Console.WriteLine("Press Any Key to Continue...");
                                    Console.ReadKey();
                                    break;
                                }
                                break;
                            }
                        }
                        break;
                    }
                    case 4:
                    return;
                }
            }
        }
    }
}
