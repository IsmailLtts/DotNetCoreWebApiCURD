using DotNetCoreWebApiCURD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiCURD.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employees;
        public EmployeeService(IEmployeeDBStore dbsettings)
        {
            var client = new MongoClient(dbsettings.ConnectionString);
            var database = client.GetDatabase(dbsettings.DBName);
            _employees = database.GetCollection<Employee>(dbsettings.CollectionName);
        }
        public async Task<List<Employee>> Get() =>
           await _employees.Find(emp => true).ToListAsync();

        public async Task<Employee?> Get(string id) =>
            await _employees.Find<Employee>(emp => emp.Id == id).FirstOrDefaultAsync();
        public async Task Create(Employee employee) =>
        await _employees.InsertOneAsync(employee);

        public async Task Update(string id, Employee employee) =>
            await _employees.ReplaceOneAsync(x => x.Id == id, employee);

        public async Task Remove(string id) =>
            await _employees.DeleteOneAsync(x => x.Id == id);


    }
}
