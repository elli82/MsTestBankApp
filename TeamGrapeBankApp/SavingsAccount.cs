﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TeamGrapeBankApp
{
    internal class SavingsAccount : BankAccount
    {
        //Properties
        public decimal Interest { get; set; }

        //Constructor
        public SavingsAccount(string accountNumber, string owner, string currency, decimal balance, decimal interest) : base(accountNumber, owner, currency, balance)
        {
            Interest = interest;
        }

        //List to hold savings accounts
        public static List<SavingsAccount> savingsAccounts = new List<SavingsAccount>();


        public static void GenerateSavingsAccounts()
        {
            //Hardcode saving accounts and add to list (should change to database later)
            SavingsAccount Acc1 = new SavingsAccount("1234-1234", "billgates", "SEK", 1000.345m, Admin.savingsDict[12]);
            SavingsAccount Acc2 = new SavingsAccount("5555-1234", "annasvensson", "SEK", 145000, Admin.savingsDict[12]);
            SavingsAccount Acc3 = new SavingsAccount("5555-0000", "hermessaliba", "SEK", 7445.43m, Admin.savingsDict[12]);

            savingsAccounts.Add(Acc1);
            savingsAccounts.Add(Acc2);
            savingsAccounts.Add(Acc3);
        }

        //Method to open new savings account
        internal static void OpenNewSavingsAccount(User loggedInCustomer)
        {
            Console.Clear();

            Console.WriteLine("Open a new savings account\n");

            //Get user input for type of account
            Console.WriteLine("The current rates are:");
            foreach (var item in Admin.savingsDict)
            {
                Console.WriteLine($"Months locked from withdrawal: {item.Key}\nCurrent interest rate: {ConvertInterestToString(item.Value)}%\n");
            }
            bool parseSuccessType = false;
            int userInputType;
            do
            {
                Console.Write("Enter months to hold your savings: ");
                parseSuccessType = int.TryParse(Console.ReadLine(), out userInputType);
            } while (!parseSuccessType || !Admin.savingsDict.ContainsKey(userInputType));

            //Get user input for currency
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

            //Get user input for amount to deposit
            bool parseSuccessAmount = false;
            decimal userInputAmount;
            do
            {
                Console.Write("Enter a valid amount to deposit: ");
                parseSuccessAmount = decimal.TryParse(Console.ReadLine(), out userInputAmount);
            } while (!parseSuccessAmount || userInputAmount < 0);

            string accountNumber = "1232-3211";

            //Create a new instance of SavingsAccount and add to list
            savingsAccounts.Add(new SavingsAccount(accountNumber, loggedInCustomer.Username, userInputCurrency, userInputAmount, Admin.savingsDict[userInputType]));

            Console.WriteLine($"\nCongratulations! You just opened a new savings account that will generate {RoundTwoDecimals(userInputAmount * Admin.savingsDict[userInputType] - userInputAmount)} " +
                $"{userInputCurrency} every {userInputType} months\n\nPress a key to return to main menu");
            Console.ReadKey();
        }

        //Method to convert and return interest decimal to percent format string
        internal static string ConvertInterestToString(decimal interest)
        {
            string inputString = interest.ToString();
            StringBuilder toReturn = new StringBuilder();
            for (int i = 2; i < inputString.Length; i++)
            {
                if (inputString[i] != '0' && i == 2) //1.15 format
                {
                    toReturn.Append(inputString[i]);
                    toReturn.Append(inputString[i + 1]);
                    toReturn.Append(",");
                    toReturn.Append(inputString.Substring(i + 2));
                    return toReturn.ToString();
                }
                else if (inputString[i] != '0' && i == 3) //1.015 format
                {
                    toReturn.Append(inputString[i]);
                    toReturn.Append(",");
                    toReturn.Append(inputString.Substring(i + 1));
                    return toReturn.ToString();
                }
                else if (inputString[i] != '0' && i > 3) //1.005 format
                {
                    toReturn.Append("0,");
                    toReturn.Append(inputString[i]);
                    toReturn.Append(inputString.Substring(i + 1));
                    return toReturn.ToString();
                }
            }
            return null;
        }

        //Method to show decimal numbers rounded to two decimals without changing the accual input
        internal static string RoundTwoDecimals(decimal input)
        {
            //decimal roundedDecimal = Math.Round(input, 2);
            return Math.Round(input, 2).ToString("0.00");
        }
    }
}
