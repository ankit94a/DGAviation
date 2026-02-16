using MasterApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Interface
{
    public interface IUserDB
    {
        public bool UpdatePassword(long id, string plainPassword);
        public Task<UserDetails> GetUserByUserName(string userName);
        public Task<bool> UpsertUserSession(long userId,Guid sessionId, string ipAddress, string userAgent);
        public Task<bool> ValidateUserSession(long  userId,Guid sessionId,string ipAddress,string userAgent);
        public Task<bool> DeleteUserSession(long userId, Guid sessionId);
    }
}
