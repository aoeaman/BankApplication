# BankApplication
A console application that Simulate a bank account which supports creation of account, closing an account, withdrawals, deposits, transfer funds

Use cases to be taken into consideration while developing the application:
  Setup new Bank
  Set Default RTGS and IMPS charges for same bank 
    RTGS-0%
    IMPS-5%
  Set Default RTGS and IMPS charges for other bank 
    RTGS- 2%
    IMPS- 6%
  Add default accepted currency as INR
  A page where user will get options to login as account holder or bank staff
    If User is bank staff then he can perform following actions
      Create new account and give username and password to account holder
      Update / Delete account at any time
      Add new Accepted currency with exchange rate 
      Add service charge for same bank account
      RTGS
      IMPS
      Add service charge for other bank account
      RTGS
      IMPS
      Can view account transaction history
      Can revert any transaction
    If User is account holder he can perform following actions 
      Deposit amount (any currency but bank will convert it to INR and will accept the deposit)
      Withdraw amount (INR only)
      Transfer funds (INR only)
      Can view his transaction history
