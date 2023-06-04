using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using TeamGrapeBankApp;
using Moq;
using NuGet.Frameworks;

namespace BankTesting
{
    [TestClass]
    public class TransactionTests
    {
        [TestMethod]
        public void ProcessTransaction_Balance_Should_Be_Correct()
        {
            //Arrange
            BankAccount fromAccount = new BankAccount("From Account", "55-34", "Gulu", "SEK", 12000);
            BankAccount toAccount = new BankAccount("To Account", "77-23", "Gulu", "SEK", 4000);
            User fromOwner = new User(5, "elli", "sommar", "Elin", "Lind", false);
            User toOwner = new User(6, "elcl", "apple", "Elisabeth", "Claesson", false);
            decimal amount = 2000;
            //Act
            Transaction.ProcessTransaction(fromAccount, toAccount, fromOwner, toOwner, amount);
            //Assert
            Assert.IsTrue(toAccount.Balance ==6000);
        }

        [TestMethod]
        public void ProcessTransaction_Balance_Should_Not_Be_Negative()
        {
            //Arrange
            BankAccount fromAccount = new BankAccount("From Account", "55-34", "Gulu", "SEK", 2000);
            BankAccount toAccount = new BankAccount("To Account", "77-23", "Gulu", "SEK", 10000);
            User fromOwner = new User(5, "elli", "sommar", "Elin", "Lind", false);
            User toOwner = new User(6, "elcl", "apple", "Elisabeth", "Claesson", false);
            decimal amount = 2000;
            //Act
            Transaction.ProcessTransaction(fromAccount, toAccount, fromOwner, toOwner, amount);
            //Assert
            Assert.IsTrue(fromAccount.Balance >= 0);
        }     
    }
}
