using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Repository.Database
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public virtual ApplicationUserProfile IdNavigation { get; set; } = null!;
    }
}
