using DMAWS_T2203E_LaVietAnh.Context;
using DMAWS_T2203E_LaVietAnh.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2203E_LaVietAnh.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;
        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToArray();
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Created($"/get-by-id?id={employee.EmployeeId}", employee);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var employeeDelete = _context.Employees.Find(id);
            if (employeeDelete == null)
                return NotFound();
            _context.Employees.Remove(employeeDelete);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpGet("SearchEmployees/{employeeName}")]
        public IActionResult SearchEmployees(string employeeName)
        {
            var employees = _context.Employees
                .Where(p => p.EmployeeName.Contains(employeeName))
                .ToList();

            return Ok(employees);
        }

        [HttpGet("SearchEmployeesByDob")]
        public IActionResult EmployeeDOBFromDate(DateTime employeeDOBFromDate, DateTime employeeDOBToDate)
        {
            var employees = _context.Employees
               .Where(e => e.EmployeeDoB >= employeeDOBFromDate && e.EmployeeDoB <= employeeDOBToDate)
               .ToList();

            return Ok(employees);
        }

        [HttpGet]
        [Route("DetaisEmployees")]
        public IActionResult Get(int id)
        {
            var employees = _context.Employees.Where(e => e.EmployeeId == id).Include(e => e.ProjectEmployees);
            if (employees == null)
                return NotFound();
            return Ok(employees);
        }
    }
}
