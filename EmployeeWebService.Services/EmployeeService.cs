using EmployeeWebService.DataAccess.Dapper;
using EmployeeWebService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebService.Services
{
    public class EmployeeService : IEmployeeService
    {
        protected readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public int AddEmployee(Employee employee)
        {
            int id = _employeeRepository.AddEmployee(employee);
            return id;
        }

        public void DeleteEmployee(int id)
        {
            _employeeRepository.DeleteEmployee(id);
        }



        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return _employeeRepository.GetAllEmployeesAsync();
        }
        public Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyid)
        {
            return _employeeRepository.GetEmployeesByCompanyAsync(companyid);
        }

        public Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string departmentname)
        {
            return _employeeRepository.GetEmployeesByDepartmentAsync(departmentname);
        }

        public Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return _employeeRepository.GetEmployeeByIdAsync(id);
        }


        public void UpdateEmployee(Employee employee)
        {
            //Получаем текущее состояние объекта
            Employee actualEmployee = _employeeRepository.GetEmployeeById(employee.id);

            //Проходим цикл по всем полям объекта и заменяем пустые значения
            foreach (FieldInfo fieldInfo in actualEmployee.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if(fieldInfo.GetValue(employee) != null)
                {
                    var actualField = actualEmployee.GetType().GetField(fieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    var putValue = employee.GetType().GetField(fieldInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(employee);
                    actualField.SetValue(actualEmployee, putValue);
                }
            }
            _employeeRepository.UpdateEmployee(actualEmployee);
        }

    }
}
