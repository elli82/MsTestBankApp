using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TeamGrapeBankApp;

namespace BankTesting
{
    [TestClass]
    public class BankAccountTest
    {

        [TestMethod]
        public void InternalTransaction_ShouldTransferMoneyFromOneAccountToTheOther()
        {
            //Arrange
            List<BankAccount> bankAccounts = new List<BankAccount>();
            List<User> customers = new List<User>();
            User user = new Customer(6, "elli", "sommar", "Elin", "Lind", "Gården 5", "eli@spray.se", "+46702222222", false);
            BankAccount testAccount = new BankAccount("Savings", "33-44", "elli", "SEK", 1000000m);
            BankAccount testAccount2 = new BankAccount("Salary", "50-90", "elli", "SEK", 50000m);

            customers.Add(user);
            bankAccounts.Add(testAccount);
            bankAccounts.Add(testAccount2);


            var FromAccountBeforeTransfer = testAccount.Balance;
            var ToAccountBeforeTransfer = testAccount2.Balance;
            //Act
            BankAccount.InternalTransaction("elli", testAccount, testAccount2, 50000m);


            //Assert
            Assert.AreNotEqual(FromAccountBeforeTransfer, testAccount.Balance);
            Assert.AreNotEqual(ToAccountBeforeTransfer, testAccount2.Balance);

        }
    }
}
