using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Services
{
    public class MasterConstant
    {
        public const string IpAddress = "ipaddress";
        public const string UserId = "userid";
        public const string UserAgent = "useragent";
        public const string RoleType = "roletype";
        public const string SessionId = "sessionid";
        public const string Name = "name";
    }

    public class UserClaims
    {
        public string SessionId { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public string RoleType { get; set; }
    }
}
