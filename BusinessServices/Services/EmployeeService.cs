using EFDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Services
{
    public class EmployeeService
    {
        public List<Employee> CreateData()
        {
            List<Employee> Employees = new(); // C# 9 Syntax  

            Employees.Add(new Employee { Id = 1, Name = "Jay", Role = "Developer", City = "Hyderabad", Pincode = 500072 });
            Employees.Add(new Employee { Id = 2, Name = "Chaitanya ", Role = "Developer", City = "Bangalore", Pincode = 500073 });
            Employees.Add(new Employee { Id = 3, Name = "Bobby Kalyan", Role = "Developer", City = "Chennai", Pincode = 500074 });
            Employees.Add(new Employee { Id = 4, Name = "Praveen", Role = "Developer", City = "Vizag", Pincode = 500075 });
            Employees.Add(new Employee { Id = 5, Name = "Naidu", Role = "Developer", City = "Cochin", Pincode = 500076 });
            Employees.Add(new Employee { Id = 6, Name = "Yateesh", Role = "Developer", City = "Tirupati", Pincode = 500077 });
            Employees.Add(new Employee { Id = 7, Name = "Priyanka", Role = "Developer", City = "Khammam", Pincode = 500064 });
            Employees.Add(new Employee { Id = 8, Name = "Jisha", Role = "QA", City = "Kurnool", Pincode = 500078 });
            Employees.Add(new Employee { Id = 9, Name = "Aravind", Role = "QA", City = "Anakapalli", Pincode = 500214 });
            Employees.Add(new Employee { Id = 10, Name = "Manikanta", Role = "QA", City = "Tuni", Pincode = 500443 });
            Employees.Add(new Employee { Id = 11, Name = "Chinna", Role = "QA", City = "Srikakulam", Pincode = 500534 });
            Employees.Add(new Employee { Id = 12, Name = "Samuel", Role = "QA", City = "Bhimavaram", Pincode = 500654 });
            Employees.Add(new Employee { Id = 13, Name = "John", Role = "QA", City = "Kharagpur", Pincode = 5000765 });
            Employees.Add(new Employee { Id = 14, Name = "Edward", Role = "QA", City = "Mumbai", Pincode = 5000224 });
            Employees.Add(new Employee { Id = 15, Name = "Nihaan", Role = "QA", City = "Mangalore", Pincode = 500965 });
            return Employees;
        }

        public List<Employee> GetEmployees() => CreateData().ToList();
    }
}
