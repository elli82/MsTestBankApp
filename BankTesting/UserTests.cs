using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TeamGrapeBankApp;

namespace BankTesting
{
    [TestClass]
    public class UserTests
    {             
        [TestMethod]
        public void Login_With_Correct_User_Name()
        {
            //Arrange
            string expectedName = "Elin";
            User user = new User(10, "Elin", "sommar", "Elin", "Lind", false);
            //Act
            string actualName = user.Username;
            //Assert
            Assert.AreEqual(expectedName, actualName);
        }
        [TestMethod]
        public void Login_With_Correct_Password()
        {
            //Arrange
            string expPassword = "sommar";
            User user = new User(10, "Elin", "sommar", "Elin", "Lind", false);
            //Act
            string actualPassword= user.Password;
            //Assert
            Assert.AreEqual(expPassword, actualPassword);
        }
        [TestMethod]
        public void Login_With_Correct_User_And_Password()
        {
            //Arrange
            string expectedName = "Elin";
            string expPassword = "sommar";
            User user = new User(10, "Elin", "sommar", "Elin", "Lind", false);
            //Act
            string actualName = user.Username;
            string actualPassword = user.Password;
            //Assert
            Assert.AreEqual(expectedName, actualName);
            Assert.AreEqual(expPassword, actualPassword); 
        }        
    }
}
