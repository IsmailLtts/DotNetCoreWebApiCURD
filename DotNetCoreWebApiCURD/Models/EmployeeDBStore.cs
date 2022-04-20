using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApiCURD.Models
{
    public class EmployeeDBStore : IEmployeeDBStore
    {
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
        public string CollectionName { get; set; }
    }

    public interface IEmployeeDBStore
    {
        public string ConnectionString { get; set; }
        public string DBName { get; set; }
        public string CollectionName { get; set; }

    }
}
