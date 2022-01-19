using EmployeeWebService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebService.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyid);
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string depratmentname);
        Task<Employee> GetEmployeeByIdAsync(int id);

        int AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
