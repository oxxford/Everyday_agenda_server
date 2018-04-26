using System;
using Everyday_agend_server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string username = "1";
            string password = "1";

            int useridExpected = DatabaseAdapter.getUserId(username, password);

            //Act
            string token = AuthenticationHelper.ValidateUser(username, password);
            int useridFinal = AuthenticationHelper.GetUserFromApiKey(token).Id;

            //Assert
            Assert.AreEqual(useridExpected, useridFinal);
        }


        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            string username = "1";
            string password = "1";

            int userid = DatabaseAdapter.getUserId(username, password);

            //Act
            string token = AuthenticationHelper.ValidateUser(username, password);
            AuthenticationHelper.RemoveApiKey(token);
            Identity userIdentity = AuthenticationHelper.GetUserFromApiKey(token);

            //Assert
            Assert.AreEqual(userIdentity, null);
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            string username = "10";
            string password = "10";

            //Act
            AuthenticationHelper.CreateUser(username, password);
            string token = AuthenticationHelper.ValidateUser(username, password);
            int userIdFinal = AuthenticationHelper.GetUserFromApiKey(token).Id;

            int useridExpected = DatabaseAdapter.getUserId(username, password);

            //Assert
            Assert.AreEqual(userIdFinal, useridExpected);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            string username = "10";
            string password = "10";

            //Act
            AuthenticationHelper.CreateUser(username, password);
            string token = AuthenticationHelper.ValidateUser(username, password);
            int userIdFinal = AuthenticationHelper.GetUserFromApiKey(token).Id;

            int useridExpected = DatabaseAdapter.getUserId(username, password);

            //Assert
            Assert.AreEqual(userIdFinal, useridExpected);
        }
    }
}
