

using Arib.EmployeeTaskManagement.Infrastructure.Models;

namespace Arib.EmployeeTaskManagement.Infrastructure.Data.DataSeeder
{
    public static class InitialData
    {
        public static List<Task_Status> TaskStatus => new()
        {
            new Task_Status { Id = 1, Name = "Pending" },
            new Task_Status { Id = 2, Name = "InProgress" },
            new Task_Status { Id = 3, Name = "Completed" }
        }; 
        public static List<Department> Departments => new()
        {
            new Department { Id = 1, Name = "HR" },
            new Department { Id = 2, Name = "IT" },
            new Department { Id = 3, Name = "Finance" }
        };

        public static List<User> Users => new()
        {
            new User
            {
                Id = 1,
                UserName = "admin",
                Password = "123456",
                Role = "Admin",
                EmployeeId = 1
            },
            new User
            {
                Id = 2,
                UserName = "Rizk",
                Password = "123456",
                Role = "Manager",
                EmployeeId = 4
            }
        };


        public static List<Employee> Employees => new()
        {
            new Employee
            {
                Id = 1,
                FirstName = "Ahmed",
                LastName = "Ali",
                Salary = 15000,
                DepartmentId = 1,
                ImagePath = "images/employees/ahmed.jpg",
                CreateDate = DateTime.Now,
            },
            new Employee
            {
                Id = 2,
                FirstName = "Sara",
                LastName = "Mostafa",
                Salary = 20000,
                DepartmentId = 2,
                ImagePath = "images/employees/sara.jpg",
                ManagerId = 1,
                CreateDate = DateTime.Now,
            },
            new Employee
            {
                Id = 3,
                FirstName = "Omar",
                LastName = "Youssef",
                Salary = 18000,
                DepartmentId = 2,
                ImagePath = "images/employees/omar.jpg",
                ManagerId = 2,
                CreateDate = DateTime.Now,
            },
            new Employee
            {
                Id = 4,
                FirstName = "Rizk",
                LastName = "Ahmed",
                Salary = 35000,
                DepartmentId = 2,
                ImagePath = "images/employees/rizk.jpg",
                CreateDate = DateTime.Now,
            }
        };

        

    }

}
