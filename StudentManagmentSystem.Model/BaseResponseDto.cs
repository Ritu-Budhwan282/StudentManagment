using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentManagmentSystem.Model
{
    public class BaseResponseDto<T>
    {
        public T? data { get; set; }
        public string? Message {get; set;}
        public bool? isSuccess { get; set; }

    }
}
