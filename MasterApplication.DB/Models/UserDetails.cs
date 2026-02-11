using System;
using System.Collections.Generic;
using System.Text;
using static MasterApplication.DB.Enum.Enum;

namespace MasterApplication.DB.Models
{
    public class UserDetails : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public int RoleType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool isLoggedIn { get; set; }
    }
    public class RolePermission : BaseModel
    {
        public long RoleId { get; set; }
        public PermissionAction PermissionAction { get; set; }
        public PermissionItem PermissionName { get; set; }

        public RolePermission Clone()
        {
            return (RolePermission)MemberwiseClone();
        }
    }

    public static class CaptchaStore
    {
        public static Dictionary<string, string> Captchas = new();
    }
    public class CaptchaRequest
    {
        public string Token { get; set; }
        public string Code { get; set; }
    }
}
