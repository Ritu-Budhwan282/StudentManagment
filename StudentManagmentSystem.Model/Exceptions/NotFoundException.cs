using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Model.Exceptions
{
    public class NotFoundException(string exceptionMessage) : Exception
    {
        public string ExceptionMessage { get; set; } = exceptionMessage;
    }
}
