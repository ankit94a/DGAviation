namespace MasterApplication.Server.Authorization
{
    public class SessionManager(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public void ClearAllSessions()
        {

            foreach (var key in _httpContextAccessor.HttpContext.Session.Keys.ToList())
            {
                _httpContextAccessor.HttpContext.Session.Remove(key);
            }
        }


        private string GetOrSetValue(string key, string value = null)
        {
            if (value != null)
            {
                SetSessionValue(key, value);
            }
            var sessionValue = GetSessionValue(key);
            return sessionValue;
        }

        public long UserId
        {
            get
            {
                var value = GetOrSetValue("UserId");
                if (long.TryParse(value, out var intValue))
                {
                    return intValue;
                }
                else
                {

                    Logout();
                    return 0;
                }
            }
            set => GetOrSetValue("UserId", value.ToString());
        }

        public string UserName
        {
            get => GetOrSetValue("UserName");
            set => GetOrSetValue("UserName", value);
        }
        public string Access_Token
        {
            get => GetOrSetValue("access_token");
            set => GetOrSetValue("access_token", value);
        }
        public string SessionId
        {
            get
            {
                var sessionId = GetOrSetValue("SessionId");
                if (string.IsNullOrEmpty(sessionId))
                {
                    sessionId = Guid.NewGuid().ToString();
                    SetSessionValue("SessionId", sessionId);
                }
                return sessionId;
            }
            set => GetOrSetValue("SessionId", value);
        }
        public string RoleId
        {
            get => GetOrSetValue("RoleId");
            set => GetOrSetValue("RoleId", value);

        }

        public string RoleType
        {
            get => GetOrSetValue("RoleType");
            set => GetOrSetValue("RoleType", value);
        }

        public string Logout()
        {

            ClearAllSessions();
            return "User logged out successfully";

        }


        private string GetSessionValue(string key) => _httpContextAccessor.HttpContext.Session.GetString(key);

        private void SetSessionValue(string key, string value) => _httpContextAccessor.HttpContext.Session.SetString(key, value);

    }
}
