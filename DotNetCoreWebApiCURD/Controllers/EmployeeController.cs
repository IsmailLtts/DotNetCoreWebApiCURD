using DotNetCoreWebApiCURD.Models;
using DotNetCoreWebApiCURD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCoreWebApiCURD.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeServices;
        public EmployeeController(EmployeeService employeeService) => _employeeServices = employeeService;

        /// <summary>
        /// Get all employees 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            //if (true)
            //{
            //    throw new ArgumentNullException("Invalid null argument", nameof(Get));
            //}
            try
            {
                List<Employee> lstemployees = await _employeeServices.Get();
                if (lstemployees is not null)
                {
                    return Ok(lstemployees);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound());
            }
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
        {
            try
            {
                Employee employee = await _employeeServices.Get(id);
                if (employee is not null)
                {
                    return new JsonResult(employee);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(NotFound());
            }
        }

        /// <summary>
        /// Create new employee 
        /// </summary>
        /// <param name="employee"></param>
        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            await _employeeServices.Create(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Update employee details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="upemployee"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Employee upemployee)
        {
            try
            {
                var employee = await _employeeServices.Get(id);
                if (employee is not null)
                {
                    upemployee.Id = id;
                    await _employeeServices.Update(id, upemployee);
                    return new JsonResult(upemployee);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }

        /// <summary>
        /// remove employeee details
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var employee = await _employeeServices.Get(id);
                if (employee is not null)
                {
                    await _employeeServices.Remove(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
