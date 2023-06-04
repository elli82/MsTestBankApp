using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TeamGrapeBankApp;

namespace BankTesting
{
    [TestClass]
    public class LoanAccountTests
    {
        [TestMethod]
        public void LoanLimit_Should_Not_Be_Able_To_Loan_Over_Limit()
        {
            //Arrange
            List<BankAccount> bankAccounts = new List<BankAccount>();
            User user = new Customer(6, "elli", "sommar", "Elin", "Lund", "Gården5", "el@hot.com", "8067", false);
            BankAccount savings = new BankAccount("savings", "22-22", "elli", "SEK", 1000m);
            bankAccounts.Add(savings);
            //Act
            decimal actual = LoanAccount.LoanLimit(user,bankAccounts);
            decimal exp = 5000m;
            //Assert
            Assert.AreEqual(exp, actual);
        }
    }
}
