using DotNetCoreWebApiCURD.Controllers;
using DotNetCoreWebApiCURD.Models;
using DotNetCoreWebApiCURD.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeTesting
{
    public class EmployeeControllerTest
    {
        [Fact]
        public async Task GetEmployees_Return_The_Correct_Number_Of_Employees()
        {
            //Arrange
            int Count = 8;
            var fakeEmployee = A.CollectionOfDummy<Employee>(Count).AsEnumerable();
            var dataStore = A.Fake<EmployeeService>();
            //A.CallTo(() => dataStore.Get()).Returns(Task.FromResult(fakeEmployee));
            var controller = new EmployeeController(dataStore);
            //Act
            var actionResult = await controller.Get();

            //Assert
            var result = actionResult.Result as OkObjectResult;
            var returnEmployee = result.Value as IEnumerable<Employee>;
            Assert.Equal(Count, returnEmployee.Count());
        }
    }
}