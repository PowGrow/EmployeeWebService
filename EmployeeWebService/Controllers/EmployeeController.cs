using EmployeeWebService.Domain;
using EmployeeWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        protected readonly IEmployeeService _employeeSerivce;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeSerivce = employeeService;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _employeeSerivce.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployeesByCompanyAsync(int CompanyId)
        {
            var employees = await _employeeSerivce.GetEmployeesByCompanyAsync(CompanyId);
            return Ok(employees);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployeesByDepartmentAsync(string DepartmentName)
        {
            var employees = await _employeeSerivce.GetEmployeesByDepartmentAsync(DepartmentName);
            return Ok(employees);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            var employees = await _employeeSerivce.GetEmployeeByIdAsync(id);
            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            employee.id  = _employeeSerivce.AddEmployee(employee);        
            return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = employee.id }, employee);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            _employeeSerivce.UpdateEmployee(employee);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteEmployee(int id)
        {
            _employeeSerivce.DeleteEmployee(id);
            return Ok();
        }
    }
}
