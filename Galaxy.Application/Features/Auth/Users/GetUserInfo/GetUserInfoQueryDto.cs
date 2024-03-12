using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Constants;

namespace Galaxy.Application.Features.Auth.Users.GetUserInfo
{
    public class GetUserInfoQueryDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gander { get; set; }
        public string EmployeeId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
