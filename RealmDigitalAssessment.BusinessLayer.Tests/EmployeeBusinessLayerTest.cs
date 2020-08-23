using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealmDigitalAssessment.BusinessLayer.Repositories;

namespace RealmDigitalAssessment.BusinessLayer.Tests
{
    [TestClass]
    public class EmployeeBusinessLayerTest
    {
        /// <summary>
        /// Test send birthday wishes
        /// </summary>
        [TestMethod]
        public void SendBirthdayWishesTest()
        {
            // Arrange
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            bool isSuccessful;

            // Act
            isSuccessful = employeeBusinessLayer.SendBirthdayWishes();

            // Assert
            Assert.IsTrue(isSuccessful);
        }
    }
}
