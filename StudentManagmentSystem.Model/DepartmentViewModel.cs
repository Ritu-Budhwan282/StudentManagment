﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Model
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool isDelete { get; set; }
    }
}
