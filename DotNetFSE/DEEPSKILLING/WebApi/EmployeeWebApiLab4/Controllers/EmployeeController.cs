using Microsoft.AspNetCore.Mvc;
using EmployeeWebApiLab4.Models;

namespace EmployeeWebApiLab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = GetEmployeeList();

        private static List<Employee> GetEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "John",
                    Salary = 50000,
                    Permanent = true,
                    DateOfBirth = new DateTime(1998,5,10),

                    Department = new Department
                    {
                        Id = 101,
                        Name = "IT"
                    },

                    Skills = new List<Skill>
                    {
                        new Skill{ Id=1, Name="C#"},
                        new Skill{ Id=2, Name="SQL"}
                    }
                },

                new Employee
                {
                    Id = 2,
                    Name = "David",
                    Salary = 60000,
                    Permanent = false,
                    DateOfBirth = new DateTime(1997,10,20),

                    Department = new Department
                    {
                        Id = 102,
                        Name = "HR"
                    },

                    Skills = new List<Skill>
                    {
                        new Skill{ Id=3, Name="Java"},
                        new Skill{ Id=4, Name="Python"}
                    }
                }
            };
        }

        // GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return Ok(employees);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Employee> AddEmployee([FromBody] Employee employee)
        {
            employees.Add(employee);
            return Created("", employee);
        }

        // PUT
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> UpdateEmployee([FromBody] Employee employee)
        {
            if (employee.Id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var existingEmployee = employees.FirstOrDefault(e => e.Id == employee.Id);

            if (existingEmployee == null)
            {
                return BadRequest("Invalid employee id");
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Permanent = employee.Permanent;
            existingEmployee.Department = employee.Department;
            existingEmployee.Skills = employee.Skills;
            existingEmployee.DateOfBirth = employee.DateOfBirth;

            return Ok(existingEmployee);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEmployee(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var employee = employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return BadRequest("Invalid employee id");
            }

            employees.Remove(employee);

            return Ok("Employee deleted successfully");
        }
    }
}