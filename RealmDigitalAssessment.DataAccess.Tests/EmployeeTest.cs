using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealmDigitalAssessment.DataAccess.Models;
using RealmDigitalAssessment.DataAccess.Repositories;

namespace RealmDigitalAssessment.DataAccess.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        /// <summary>
        /// Test get all employees
        /// </summary>
        [TestMethod]
        public void GetAllEmployeesTest()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<Employee> employees = new List<Employee>();

            // Act
            employees = employeeRepository.GetAllEmployees().Result;

            // Assert
            int actual = 130;
            Assert.AreEqual(employees.Count, actual, 0, "Employees not found");
        }

        /// <summary>
        /// Test get birthday wish exclusions
        /// </summary>
        [TestMethod]
        public void GetBirthdayWishExclusionsTest()
        {
            // Arrange
            EmployeeRepository employeeRepository = new EmployeeRepository();
            List<int> employeeIds = new List<int>();

            // Act
            employeeIds = employeeRepository.GetBirthdayWishExclusions().Result;
            // Assert
            int actual = 3;
            Assert.AreEqual(employeeIds.Count, actual, 0, "Employees not found");
        }
    }
}
