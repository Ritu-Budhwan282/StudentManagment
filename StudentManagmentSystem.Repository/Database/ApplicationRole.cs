using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem.Repository.Database
{
    public class ApplicationRole
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<ApplicationUserProfile> ApplicationUserProfiles { get; set; } = new List<ApplicationUserProfile>();
    }
}
