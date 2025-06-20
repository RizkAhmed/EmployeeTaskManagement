﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Models
{
    public class Department:BaseEntity
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Employee>? Employees { get; set; }
    }

}
