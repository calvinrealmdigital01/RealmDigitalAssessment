using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using RealmDigitalAssessment.DataAccess.Interfaces;
using RealmDigitalAssessment.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigitalAssessment.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public HttpClient _Client { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeeRepository()
        {
            _Client = new HttpClient();
            string url = ConfigurationManager.AppSettings["ApiUrl"];
            _Client.BaseAddress = new Uri(url);
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Employees</returns>
        public async Task<List<Employee>> GetAllEmployees() {
            try
            {
                HttpResponseMessage result = await _Client.GetAsync($"/api/Employees");
                result.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<List<Employee>>(result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get birthday wish exclusions
        /// </summary>
        /// <returns>Employee Ids</returns>
        public async Task<List<int>> GetBirthdayWishExclusions()
        {
            try
            {
                HttpResponseMessage result = await _Client.GetAsync($"/api/BirthdayWishExclusions");
                result.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<List<int>>(result.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
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
