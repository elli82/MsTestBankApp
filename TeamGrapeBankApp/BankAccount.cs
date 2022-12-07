﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class BankAccount
    {
        protected string AccountName { get; set; }
        protected string AccountNumber { get; set; }
        protected string Owner  { get; set; }
        protected string Currency { get; set; }
        protected decimal Balance { get; set; }

        public static List<BankAccount> bankAccounts = new List<BankAccount>();
        public BankAccount(string accountName, string accountNumber, string owner, string currency, decimal balance)
        {
            AccountName = accountName;
            AccountNumber = accountNumber;
            Owner = owner;
            Currency = currency;
            Balance = balance;
        }

        public static void GenerateBankAccounts()
        {
            //Hardcode some bankaccounts and adds them to list (should change to database later)
            BankAccount Acc1 = new BankAccount("Salary Account","4444-5555", "billgates", "SEK", 1000000.345m);
            BankAccount Acc2 = new BankAccount("Bills","4444-3577", "billgates", "SEK", 50043);
            BankAccount Acc3 = new BankAccount("For Emergency","4444-2644", "billgates", "SEK", 3205);

            BankAccount Acc4 = new BankAccount("Salary Account","5555-2644", "annasvensson", "SEK", 45000);
            BankAccount Acc5 = new BankAccount("Bills","5555-1533", "annasvensson", "SEK", 2300);

            BankAccount Acc6 = new BankAccount("Salary Account","6666-7533", "hermessaliba", "SEK", 74442.43m);
            BankAccount Acc7 = new BankAccount("Bills","6666-2685", "hermessaliba", "USD", 25114.79m);

            bankAccounts.Add(Acc1);
            bankAccounts.Add(Acc2);
            bankAccounts.Add(Acc3);
            bankAccounts.Add(Acc4);
            bankAccounts.Add(Acc5);
            bankAccounts.Add(Acc6);
            bankAccounts.Add(Acc7);
        }

        public override string ToString()
        {
            return $"AccountName: {AccountName}\nAccountnumber: {AccountNumber}\nBalance: {RoundTwoDecimals(Balance)}{Currency}\n";
        }

        public static void ListBankaccounts(string username)
        {
            Console.Clear();
            Console.WriteLine("Bankaccounts");
            //Creates a new list and adds bankaccounts owned by loggedInUser
            List<BankAccount> userBankaccount = bankAccounts.FindAll(x => x.Owner == username);

            foreach(BankAccount b in userBankaccount)
            {
                Console.WriteLine(b);
            }

            List<SavingsAccount> userSavingsAccount = SavingsAccount.savingsAccounts.FindAll(x => x.Owner == username);
            if(userSavingsAccount.Count() > 0)
            {
                Console.WriteLine("Savingsaccount");
                foreach(SavingsAccount h in userSavingsAccount)
                {
                    Console.WriteLine(h);
                }
            }
            Console.WriteLine("All accounts listed, please press a key to return to menu");
            Console.ReadKey();
        }

        //Method to open a new account
        public static void OpenNewAccount(User loggedInCustomer)
        {
            Console.Clear();

            Console.WriteLine("Open a new bank account\n");

            string accountNumber;
            accountNumber = GenerateAccountNumber();

            Console.Write("Enter account name: ");
            string accountName = Console.ReadLine();

            Console.WriteLine("\nThe available currencies are:");
            foreach (var item in Admin.currencyDict)
            {
                Console.WriteLine(item.Key);
            }
            string userInputCurrency;
            do
            {
                Console.Write("Enter a valid currency: ");
                userInputCurrency = Console.ReadLine().ToUpper();
            } while (!Admin.currencyDict.ContainsKey(userInputCurrency));

            bool parseSuccess;
            decimal balance;
            do
            {
                Console.Write("Enter amount to deposit: ");
                parseSuccess = decimal.TryParse(Console.ReadLine(), out balance);
            } while (!parseSuccess || balance < 0);
          
            
            BankAccount newBankAccount = new BankAccount(accountName, accountNumber, loggedInCustomer.Username, userInputCurrency, balance);
            BankAccount.bankAccounts.Add(newBankAccount);
            Console.WriteLine("Account succesfully created: " + "\n" + newBankAccount + "\n");
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }

        public static string GenerateAccountNumber()
        {
            string newAccountNumber;
            do
            {
                Random ranAccount = new Random();
                Random ranAccount2 = new Random();
                int randAccount = ranAccount.Next(9999);
                int randAccount2 = ranAccount2.Next(9999);
                newAccountNumber = randAccount.ToString() + "-" + randAccount2.ToString();
            }
            while (BankAccount.bankAccounts.Any(x => x.AccountNumber == newAccountNumber) && SavingsAccount.savingsAccounts.Any(x => x.AccountNumber == newAccountNumber));

            return newAccountNumber;
        }

        public static void internalTransaction(string username)
        {

            //Write out the accounts
            Console.Clear();
            Console.WriteLine("Bankaccounts");
            List<BankAccount> userBankaccount = bankAccounts.FindAll(x => x.Owner == username);
            


            for(int i=0; i < userBankaccount.Count; i++)
            {
                BankAccount bankAccount = userBankaccount[i];   
                Console.WriteLine("" + (i+1) + "." + bankAccount);
            }
          
           Console.WriteLine("What account do you want to move money from? ");

            int AccountNumberFrom = GetUserAccountSelection(0, userBankaccount.Count,-1);
            BankAccount AccountFrom = userBankaccount[AccountNumberFrom-1];

            Console.WriteLine("What account do you want to move money to? ");
            int AccountNumberTo = GetUserAccountSelection(0, userBankaccount.Count,AccountNumberFrom);
            
            BankAccount AccountTo = userBankaccount[AccountNumberTo - 1];

            bool parseSuccess;
            decimal AmmountMove;
            do {

                Console.WriteLine("How much money do you want to move? ");
                parseSuccess = decimal.TryParse(Console.ReadLine(), out AmmountMove);
            } while (!parseSuccess);
           
            if (AmmountMove > AccountFrom.Balance)
            {

                Console.WriteLine("Not enough money on account.... ");
                Console.ReadKey();
                return; //Can call on method internalTrancactions again..
            }

            //Logic to handle same and different currency transfers
            if (AccountFrom.Currency == AccountTo.Currency)
            {
                AccountFrom.Balance -= AmmountMove;
                AccountTo.Balance += AmmountMove;
                Console.WriteLine($"{AmmountMove} {AccountFrom.Currency} transferred from account {AccountFrom.AccountNumber} to account {AccountTo.AccountNumber}");
            }
            else
            {
                if (AccountFrom.Currency == "SEK")
                {
                    AccountFrom.Balance -= AmmountMove;
                    AccountTo.Balance += AmmountMove / Admin.currencyDict[AccountTo.Currency];
                    Console.WriteLine($"{AmmountMove} {AccountFrom.Currency} transferred from account {AccountFrom.AccountNumber} to account {AccountTo.AccountNumber} " +
                        $"at the exchange rate 1 / {Admin.currencyDict[AccountTo.Currency]}");
                }
                else
                {
                    AccountFrom.Balance -= AmmountMove;
                    AccountTo.Balance += AmmountMove * Admin.currencyDict[AccountFrom.Currency];
                    Console.WriteLine($"{AmmountMove} {AccountFrom.Currency} transferred from account {AccountFrom.AccountNumber} to account {AccountTo.AccountNumber} " +
                        $"at the exchange rate 1 * {Admin.currencyDict[AccountFrom.Currency]}");
                }
            }


            foreach (BankAccount b in userBankaccount)
            {
                
                Console.WriteLine(b);
            }

            Console.WriteLine("Transaction went through.... ");
            Console.WriteLine("Press any key to return to menu ");
            Console.ReadKey();
        }

        private static int GetUserAccountSelection(int minValue, int maxValue, int previousValue)
        {
            bool ValidSelection = false;
            int Selection = -1;

            while(!ValidSelection)
            {

                  int.TryParse(Console.ReadLine(), out Selection);

                    if(Selection > minValue && Selection <= maxValue && Selection != previousValue)
                    {
                        ValidSelection = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Account Selection, Please try again ");
                    }
            }
            return Selection;
        }
        
        //Method to show decimal numbers rounded to two decimals without changing the accual input
        internal static string RoundTwoDecimals(decimal input)
        {
            //decimal roundedDecimal = Math.Round(input, 2);
            return Math.Round(input, 2).ToString("0.00");
        }
    }
}
