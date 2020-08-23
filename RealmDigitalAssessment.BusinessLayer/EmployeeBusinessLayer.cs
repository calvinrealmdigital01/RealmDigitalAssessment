using Microsoft.Win32.SafeHandles;
using RealmDigitalAssessment.BusinessLayer.Helpers;
using RealmDigitalAssessment.BusinessLayer.Interfaces;
using RealmDigitalAssessment.DataAccess.Models;
using RealmDigitalAssessment.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigitalAssessment.BusinessLayer.Repositories
{
    public class EmployeeBusinessLayer : IEmployeeBusinessLayer, IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeBusinessLayer() { 
        
        }

        /// <summary>
        /// Send birthday wishes
        /// </summary>
        /// <returns>true or false</returns>
        public bool SendBirthdayWishes() {
            using (EmployeeRepository dataAccess = new EmployeeRepository())
            {
                DateTime today = DateTime.Now;//Convert.ToDateTime("1955-10-28T00:00:00");

                List<int> employeeIdsToExclude = dataAccess.GetBirthdayWishExclusions().Result;
                List<Employee> employees = dataAccess.GetAllEmployees().Result;

                //Excluede employees that are configured to not receive birthday wishes.
                List<Employee> employeesHBD = employees.Where(e => !employeeIdsToExclude.Contains(e.Id) && e.DateOfBirth.Month == today.Month && e.DateOfBirth.Day == today.Day).ToList();

                //Check if today is 28 Feb and is not leap year
                if (DateTime.Now.Month == 2 && DateTime.Now.Day == 28 && !DateTime.IsLeapYear(DateTime.Now.Year))
                {
                    //Include leapers that are configured to receive birthday wishes.
                    List<Employee> leapers = employees.Where(e => !employeeIdsToExclude.Contains(e.Id) && e.DateOfBirth.Month == 2 && e.DateOfBirth.Day == 29).ToList();
                    employeesHBD.AddRange(leapers);
                }                    
             
                //Exclude employees that no longer works for ACME and employees that has not started working for ACME
                List<Employee> activeEmployees = employeesHBD.Where(e => e.EmploymentEndDate == null && e.EmploymentStartDate < DateTime.Now).ToList();

                foreach (var employee in activeEmployees)
                {
                    SendEmail sendEmail = new SendEmail();
                    sendEmail.SendBirthdayEmail(employee);
                }

                return true;
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
            }

            disposed = true;
        }
    }
}
