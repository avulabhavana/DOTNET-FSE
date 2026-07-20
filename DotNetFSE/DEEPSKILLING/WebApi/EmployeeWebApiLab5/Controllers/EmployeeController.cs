
using EmployeeWebApiLab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApiLab5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
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
                    new Skill{ Id = 1, Name = "C#" },
                    new Skill{ Id = 2, Name = "SQL" }
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
                    new Skill{ Id = 3, Name = "Java" },
                    new Skill{ Id = 4, Name = "Python" }
                }
            }
        };

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployees()
        {
            return Ok(employees);
        }
    }
}