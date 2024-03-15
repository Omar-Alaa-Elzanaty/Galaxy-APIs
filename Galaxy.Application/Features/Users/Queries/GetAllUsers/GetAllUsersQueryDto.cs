﻿namespace Galaxy.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gander { get; set; }
        public string EmployeeId { get; set; }
    }
}