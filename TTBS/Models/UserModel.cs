﻿namespace TTBS.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public IEnumerable<RoleModel> Roles { get; set; }
        public string Token { get; set; }

    }
}
