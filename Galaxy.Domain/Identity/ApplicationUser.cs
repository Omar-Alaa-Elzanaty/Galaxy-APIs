using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace Galaxy.Domain.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string EmployeeId { get; set; }
        public string Name {  get; set; }
        public Gender Gender { get; set; }
    }
}
