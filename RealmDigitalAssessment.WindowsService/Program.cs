using RealmDigitalAssessment.BusinessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigitalAssessment.WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);

            SendBirthdayWishes();
        }

        /// <summary>
        /// Send birthday wishes
        /// </summary>
        /// <returns></returns>
        private static bool SendBirthdayWishes() {
            bool isSuccessful = false;
            try
            {
                using (EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer())
                {
                    employeeBusinessLayer.SendBirthdayWishes();
                }
                return isSuccessful;
            }
            catch (Exception ex)
            {
                throw ex;
            }             
        }
    }
}
