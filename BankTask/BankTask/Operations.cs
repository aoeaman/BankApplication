using System;
using System.Collections.Generic;
using System.Linq;
using BankTask;

public class Operations
{
    
    public IBankServices bankService = new BankServices();
    public ITransactionService transactionServices = new TransactionServices();
    public IEmployeeService employeeServices = new EmployeeServices();
    public IAccountServices accountService = new AccountServices();
    readonly UtilityTools Tools = new UtilityTools();

    public bool CreateBank(List<Bank> banks,string bankName)
    {
        try
        {

            if (banks.Any(name => string.Equals(name.Name, bankName)) == false)
            {
                Bank bank = new Bank
                {
                    Name = bankName,
                    ChargeForSameBank = new Dictionary<ChargeType, decimal>
                        {
                            {ChargeType.RTGS,0 },{ChargeType.IMPS,5}
                        },
                    ChargeForOtherBank = new Dictionary<ChargeType, decimal>
                        {
                            {ChargeType.RTGS,2 },{ChargeType.IMPS,6}
                        },
                    Currencies = new List<Currency>(),
                    Accounts = new List<Account>(),
                    AccountHolders = new List<AccountHolder>(),
                    Employees = new List<Employee>()
                };
                bank.Currencies.Add(new Currency { Name = "INR", Exchangerate = 1 });
                banks.Add(bankService.Create(bank,bankName));
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool AddCurrency(Bank bank, string name, decimal exchangeValue)
    {
        if (!bank.Currencies.Any(Element => Element.Name == name))
        {
            Currency NewCurrency= new Currency { Name = name, Exchangerate = exchangeValue };
            bankService.AddCurrency(bank,NewCurrency);
            return true;
        }
        else
        {
            return false;
        }
    }
    public string CreateAccount(Bank bank, string name, string userName, string password)
    {
        if (bank.AccountHolders.Exists(Element => string.Equals(Element.UserName, userName)) == false)
        {
            Account account= new Account()
            {
                BankID = bank.ID.ToUpper(),
                Funds = 0,
                Transactions = new List<Transaction>()
            };

            AccountHolder holder= new AccountHolder()
            {
                Password = password,
                UserName = userName,
                Name = name,
                BankID=bank.ID
            };
            Tuple<Account,AccountHolder> AccountDetails= accountService.Create(account,holder,name);
            bank.Accounts.Add(AccountDetails.Item1);
            bank.AccountHolders.Add(AccountDetails.Item2);
            return AccountDetails.Item1.ID;
        }
        else
        {
            return null;

        }
    }
    public bool UpdateAccount(Bank bank, string userName, string newUserName, string newPassword)
    {
        AccountHolder Holder = bank.AccountHolders.Find(Element => Element.UserName == userName);
        if (Holder != null)
        {
            Holder = accountService.Update(Holder, newUserName, newPassword);           
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CreateEmployee(Bank selectedBank, string name, string username, string password)
    {
        if (selectedBank.Employees.Any(Element => Element.UserName == username) == false)
        {
            Employee employee=  new Employee()
            {
                Name = name,                
                Password = password,
                UserName = username
            };
            employee = employeeServices.Create(employee, name);
            selectedBank.Employees.Add(employee);
            return true;
        }
        else
        {
            return false;
        }
    }
    public string UpdatingTransactionHistory(string senderID, Account account, decimal amount, TransactionType type, decimal charges, string reciverID)
    {
        Transaction transaction=new Transaction()
        {
            Amount = amount,
            Charges = charges,           
            SenderID = senderID,
            RecieverID = reciverID,
            Type = type,
            FundsAvailable = account.Funds
        };
        transaction = transactionServices.GetTransactionID(transaction, account.BankID, senderID);
        account.Transactions.Add(transaction);
        return transaction.ID;
    }
    public string Withdraw(Account account, int amount)
    {
        if (amount > account.Funds)
        {
            return null;
        }
        else
        {
            account.Funds -= amount;
            return UpdatingTransactionHistory(account.ID, account, amount, TransactionType.Deposit, 0, Enum.GetName(typeof(TransactionType), 0));       
        }
    }
    public string Deposit(Account account, int amount, decimal rate)
    {
        account.Funds += amount * rate;
        return UpdatingTransactionHistory(account.ID, account, amount, TransactionType.Deposit, 0, Enum.GetName(typeof(TransactionType), 0));      
    }
    public void FundTransfer(Bank bank, Account sender, Account reciever, ChargeType chargeType, decimal amount)
    {
        decimal Chargerate;

        if (reciever.BankID == sender.BankID)
        {
            Chargerate = bank.ChargeForSameBank[chargeType];
        }
        else
        {
            Chargerate = bank.ChargeForOtherBank[chargeType];
        }

        decimal Charge = amount * Chargerate / 100;
        sender.Funds -= amount + Charge;
        reciever.Funds += amount;
        UpdatingTransactionHistory(sender.ID, sender, amount, TransactionType.FundTransfer, Charge, reciever.ID);
        UpdatingTransactionHistory( sender.ID, reciever, amount, TransactionType.FundTransfer, 0, reciever.ID);
    }
    public string RevertDeposit(Account user, Transaction transaction)
    {
        if (user.Funds < transaction.Amount)
        {
            return null;
        }
        else
        {
            user.Funds -= transaction.Amount;
            return UpdatingTransactionHistory(user.ID, user, transaction.Amount, TransactionType.Revert, transaction.Charges, transaction.RecieverID);             
        }
    }
    public string RevertWithdraw(Account user, Transaction transaction)
    {
        user.Funds += transaction.Amount;       
        return UpdatingTransactionHistory(user.ID, user, transaction.Amount, TransactionType.Revert, transaction.Charges, transaction.RecieverID);

    }
    public bool RevertFundTransfer(List<Bank> banks, Transaction transaction)
    {
        Account Reciever = null;
        Account Sender = null;
        foreach (var bank in banks)
        {
            if (Reciever == null)
            {
                Reciever = bank.Accounts.FirstOrDefault(name => name.ID == transaction.RecieverID);
            }
            if (Sender == null)
            {
                Sender = bank.Accounts.FirstOrDefault(name => name.ID == transaction.SenderID);
            }
            if (Reciever != null && Sender != null)
            {
                break;
            }
        }
        if (Reciever == null || Sender == null || Reciever.Funds < transaction.Amount)
        {
            return false;
        }
        else
        {
            Sender.Funds += transaction.Amount + transaction.Charges;
            Reciever.Funds -= transaction.Amount;
            UpdatingTransactionHistory(Sender.ID, Sender, transaction.Amount, TransactionType.Revert, transaction.Charges, Reciever.ID);
            UpdatingTransactionHistory(Sender.ID, Reciever, transaction.Amount, TransactionType.Revert, 0, Reciever.ID);
            return true;
        }

    }
}
