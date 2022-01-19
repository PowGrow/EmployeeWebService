using Dapper;
using EmployeeWebService.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebService.DataAccess.Dapper
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {
            _config = config;
        }

        //создание объекта IDbConnection
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("EmployeesDbConnection"));
            }
        }

        public int AddEmployee(Employee employee)
        {
            try
            {
                using(IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var p = new DynamicParameters();
                    p.Add("id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    string query = @"INSERT INTO Employee(name, surname, phone, passporttype, passportnumber, companyId, departmentname, departmentphone) OUTPUT inserted.id VALUES (@name, @surname, @phone, @passporttype, @passportnumber, @companyid, @departmentname, @departmentphone)";
                    int newid = (int)dbConnection.ExecuteScalar(query, employee);
                    return newid;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                using(IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"DELETE FROM Employee WHERE id = @id";
                    dbConnection.Execute(query, new { id = id});
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                using(IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Employee";
                    return await dbConnection.QueryAsync<Employee>(query);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyAsync(int companyid)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Employee WHERE companyid = @companyid";
                    return await dbConnection.QueryAsync<Employee>(query, new { companyid = companyid });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string departmentname)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Employee WHERE departmentname = @departmentname";
                    return await dbConnection.QueryAsync<Employee>(query, new { departmentName = departmentname });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                using(IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Employee WHERE id = @id";
                    return await dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new { id = id });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"SELECT * FROM Employee WHERE id = @id";
                    return dbConnection.QueryFirstOrDefault<Employee>(query, new { id = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using(IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    string query = @"UPDATE Employee SET name = @name, surname = @surname, phone = @phone, passporttype = @passporttype, passportnumber = @passportnumber, companyid = @companyid, departmentname = @departmentname, departmentphone = @departmentphone WHERE id = @id";
                    dbConnection.Execute(query, employee);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
