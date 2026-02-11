using Dapper;
using MasterApplication.DB.Interface;
using MasterApplication.DB.Models;
using MasterApplication.DB.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterApplication.DB.Implements
{
    public class UserDB : BaseDB, IUserDB
    {
        public UserDB(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<UserDetails> GetUserByEmail(string userName)
        {
            try
            {
                string query = string.Format(@"select * from userdetails where username=@username");
                var result = await connection.QueryFirstAsync<UserDetails>(query, new { username = userName });
                return result;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=UserDB,method=GetUserByEmail");
                throw;
            }
        }
        public bool UpdatePassword(long Id, string Password)
        {
            try
            {
                string query = string.Format(@"update userdetails set password=@password where id=@id");
                var result = connection.Query<UserDetails>(query, new { password = Password, id = Id }).FirstOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=UserDB,method=UpdatePassword");
                throw;
            }
        }
        public async Task<bool> UpsertUserSession(long userId, Guid sessionId,string ipAddress, string userAgent)
        {
            try
            {
                string query = string.Format(@"IF EXISTS (SELECT 1 FROM UserSessions WHERE userid = @userid)
                                               BEGIN UPDATE UserSessions SET sessionid = @sessionid,lastlogin = GETDATE(),ipaddress=@ipaddress,useragent=@useragent WHERE userid = @userid; END
                                               ELSE
                                               BEGIN INSERT INTO UserSessions (userid, sessionid, lastlogin,ipaddress,useragent) VALUES (@userid, @sessionid, GETDATE(),@ipaddress,@useragent); END
                                             ");
                int affectedRows = await connection.ExecuteAsync(query,new { userid = userId, sessionid = sessionId,ipaddress=ipAddress,useragent=userAgent });
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex, "Class=UserDB,method=UpsertUserSession");
                throw;
            }
        }
        public async Task<bool> ValidateUserSession(long userId,Guid sessionId,string ipAddress,string userAgent)
        {
            try
            {
                string query = "SELECT sessionid FROM usersessions WHERE userid = @userid and sessionid = @sessionid and ipaddress=@ipaddress and useragent=@useragent";
                return await connection.ExecuteScalarAsync<int?>(query,new { userid = userId, sessionid = sessionId, ipaddress = ipAddress, useragent = userAgent }) == 1;
            }
            catch (SqlException ex)
            {
                MasterLogger.Error(ex,"DB error in UserDB.GetUserSessionId | UserId={UserId}", userId);
                throw new ApplicationException("Unable to retrieve your session information at the moment. Please try again.");
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex,"Unexpected error in UserDB.GetUserSessionId | UserId={UserId}", userId);
                throw new ApplicationException("Something went wrong while retrieving session details.");
            }
        }
        public async Task<bool> DeleteUserSession(long userId,long sessionId)
        {
            try
            {
                string query = "DELETE FROM UserSessions WHERE userid = @userid and sessionid = @sessionid";
                int affectedRows = await connection.ExecuteAsync( query,new { userid = userId,sessionid = sessionId });
                return affectedRows > 0;
            }
            catch (SqlException ex) 
            {
                MasterLogger.Error(ex,"DB error in UserDB.DeleteUserSession | UserId={UserId}",userId);
                throw new ApplicationException("We couldn't end your session at the moment. Please try again.");
            }
            catch (Exception ex)
            {
                MasterLogger.Error(ex,"Unexpected error in UserDB.DeleteUserSession | UserId={UserId}", userId);
                throw new ApplicationException("Something went wrong while logging you out. Please try again.");
            }
        }

    }
}
