using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebService.Domain
{
    public class Employee
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? surname { get; set; }
        public string? phone { get; set; }
        public string? passportType { get; set; }
        public string? passportNumber { get; set; }
        public int? companyId { get; set; }
        public string? departmentName { get; set; }
        public string? departmentPhone { get; set; }


    }
}
